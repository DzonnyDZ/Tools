Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing

Namespace GUI
    <Bindable(False)> _
    Public Class CloseButton : Inherits ButtonBase
        Private rNormal As New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Window.CloseButton.Normal)
        Private rHot As New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Window.CloseButton.Hot)
        Private rPush As New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Window.CloseButton.Pressed)
        Private rDisabled As New VisualStyles.VisualStyleRenderer(VisualStyles.VisualStyleElement.Window.CloseButton.Disabled)
        Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
        End Sub
        ''' <summary>Processes Windows messages.</summary>
        ''' <param name="m">The Windows <see cref="System.Windows.Forms.Message"/> to process.</param>
        ''' <remarks>Handles messages:
        ''' <list type="table">
        ''' <listheader><term>Message</term><description>Action taken</description></listheader>
        ''' </list>
        ''' </remarks>
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            MyBase.WndProc(m)
            Select Case m.Msg
                Case &H31A, &H15 'WM_THEMECHANGED, WM_SYSCOLORCHANGE
                    Me.Invalidate()
            End Select
        End Sub
        'Public Overrides Function GetPreferredSize(ByVal proposedSize As System.Drawing.Size) As System.Drawing.Size
        '    Return New Size(Math.Max(proposedSize.Width, MinimumSize.Width), Math.Max(proposedSize.Height, MinimumSize.Height))
        'End Function

        Private ReadOnly Property PrefferedSize() As Size
            Get
                'Using g As Graphics = Me.CreateGraphics
                '    Return rNormal.GetPartSize(g, VisualStyles.ThemeSizeType.True)
                'End Using
                Return New Size(21, 21)
            End Get
        End Property

        Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
            Dim vsr As VisualStyles.VisualStyleRenderer
            If Not Me.Enabled Then
                vsr = rDisabled
            ElseIf Me.RectangleToScreen(Me.ClientRectangle).Contains(Control.MousePosition) AndAlso ((Control.MouseButtons And Windows.Forms.MouseButtons.Left) = Windows.Forms.MouseButtons.Left) Then
                vsr = rPush
            ElseIf Me.RectangleToScreen(Me.ClientRectangle).Contains(Control.MousePosition) Then
                vsr = rHot
            Else
                vsr = rNormal
            End If
            Dim sizeT As Size = vsr.GetPartSize(e.Graphics, VisualStyles.ThemeSizeType.True)
            If Me.AutoSize AndAlso Me.ClientSize <> sizeT Then Me.ClientSize = sizeT
            vsr.DrawBackground(e.Graphics, Me.ClientRectangle, e.ClipRectangle)
        End Sub
        Protected Overrides Sub OnGotFocus(ByVal e As System.EventArgs)
            MyBase.OnGotFocus(e)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnAutoSizeChanged(ByVal e As System.EventArgs)
            MyBase.OnAutoSizeChanged(e)
            If Me.IsHandleCreated Then Me.Invalidate()
        End Sub
        Protected Overrides Sub OnLostFocus(ByVal e As System.EventArgs)
            MyBase.OnLostFocus(e)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnMouseEnter(ByVal eventargs As System.EventArgs)
            MyBase.OnMouseEnter(eventargs)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnMouseLeave(ByVal eventargs As System.EventArgs)
            MyBase.OnMouseLeave(eventargs)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnMouseDown(ByVal mevent As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseDown(mevent)
            Me.Invalidate()
        End Sub
        Protected Overrides Sub OnMouseUp(ByVal mevent As System.Windows.Forms.MouseEventArgs)
            MyBase.OnMouseUp(mevent)
            Me.Invalidate()
        End Sub
        'Public Overrides Property MinimumSize() As System.Drawing.Size
        '    Get
        '        Return MyBase.MinimumSize
        '    End Get
        '    Set(ByVal value As System.Drawing.Size)
        '        Dim min As Size = MinimumSizeInternal
        '        value.Width = Math.Max(value.Width, min.Width)
        '        value.Height = Math.Max(value.Height, min.Height)
        '        MyBase.MinimumSize = value
        '    End Set
        'End Property
        'Private ReadOnly Property MinimumSizeInternal() As Size
        '    Get
        '        Using g As Graphics = Me.CreateGraphics
        '            Return rNormal.GetPartSize(g, VisualStyles.ThemeSizeType.Minimum)
        '        End Using
        '    End Get
        'End Property
        'Private Function ShouldSerializeMinimumSize() As Boolean
        '    Return Me.MinimumSize <> MinimumSizeInternal
        'End Function
        'Private Sub ResetMinimumsize()
        '    Me.MinimumSize = MinimumSizeInternal
        'End Sub
        Public Sub New()
            Text = ""
            'ResetMinimumsize()
            AutoSize = True
            ResetSize()
        End Sub
        Public Overrides Function GetPreferredSize(ByVal proposedSize As System.Drawing.Size) As System.Drawing.Size
            Return PrefferedSize
        End Function

        Private Sub ResetSize()
            Me.ClientSize = PrefferedSize
        End Sub
        Private Function ShouldSerializeSize() As Boolean
            Return Me.Size <> PrefferedSize
        End Function


        <DefaultValue(True)> _
        Public Overrides Property AutoSize() As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(ByVal value As Boolean)
                MyBase.AutoSize = value
            End Set
        End Property
#Region "Hide properties"
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property TextImageRelation() As TextImageRelation
            Get
                Return MyBase.TextImageRelation
            End Get
            Set(ByVal value As TextImageRelation)
                MyBase.TextImageRelation = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property ImageIndex() As Integer
            Get
                Return MyBase.ImageIndex
            End Get
            Set(ByVal value As Integer)
                MyBase.ImageIndex = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property ImageList() As ImageList
            Get
                Return MyBase.ImageList
            End Get
            Set(ByVal value As ImageList)
                MyBase.ImageList = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property ImageKey() As String
            Get
                Return MyBase.ImageKey
            End Get
            Set(ByVal value As String)
                MyBase.ImageKey = value
            End Set
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows Property ImageAlign() As ContentAlignment
            Get
                Return MyBase.ImageAlign
            End Get
            Set(ByVal value As ContentAlignment)
                MyBase.ImageAlign = value
            End Set
        End Property

        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows ReadOnly Property FlatAppearance() As FlatButtonAppearance
            Get
                Return MyBase.FlatAppearance
            End Get
        End Property


        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property AutoElipsis() As Boolean
            Get
                Return MyBase.AutoEllipsis
            End Get
            Set(ByVal value As Boolean)
                MyBase.AutoEllipsis = value
            End Set
        End Property
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property Padding() As Padding
            Get
                Return MyBase.Padding
            End Get
            Set(ByVal value As Padding)
                MyBase.Padding = value
            End Set
        End Property
        <DefaultValue(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                MyBase.Text = value
            End Set
        End Property


        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property BackgroundImage() As System.Drawing.Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(ByVal value As System.Drawing.Image)
                MyBase.BackgroundImage = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Property Image() As Image
            Get
                Return MyBase.Image
            End Get
            Set(ByVal value As Image)
                MyBase.Image = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property BackgroundImageLayout() As System.Windows.Forms.ImageLayout
            Get
                Return MyBase.BackgroundImageLayout
            End Get
            Set(ByVal value As System.Windows.Forms.ImageLayout)
                MyBase.BackgroundImageLayout = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property TextAlign() As System.Drawing.ContentAlignment
            Get
                Return MyBase.TextAlign
            End Get
            Set(ByVal value As System.Drawing.ContentAlignment)
                MyBase.TextAlign = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property BackColor() As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                MyBase.BackColor = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property ForeColor() As System.Drawing.Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                MyBase.ForeColor = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property Font() As System.Drawing.Font
            Get
                Return MyBase.Font
            End Get
            Set(ByVal value As System.Drawing.Font)
                MyBase.Font = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Property UseMnemonic() As Boolean
            Get
                Return MyBase.UseMnemonic
            End Get
            Set(ByVal value As Boolean)
                MyBase.UseMnemonic = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Property UseVisualStyleBackColor() As Boolean
            Get
                Return MyBase.UseVisualStyleBackColor
            End Get
            Set(ByVal value As Boolean)
                MyBase.UseVisualStyleBackColor = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Browsable(False)> _
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Property UseCompatibleTextRendering() As Boolean
            Get
                Return MyBase.UseCompatibleTextRendering
            End Get
            Set(ByVal value As Boolean)
                MyBase.UseCompatibleTextRendering = value
            End Set
        End Property
#End Region
    End Class
End Namespace