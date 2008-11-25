Imports Tools.CollectionsT.GenericT, Tools, System.ComponentModel
Imports Tools.WindowsT.FormsT.UtilitiesT
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Drawing
Imports Tools.ComponentModelT


''' <summary><see cref="ToolStripLabel"/> postavený na <see cref="Label">Labelu</see></summary>
Public Class ToolStripLabelLabel
    Inherits ToolStripControlHost
    ''' <summary>CTor</summary>
    Public Sub New()
        MyBase.New(NewLabel)
        Me.BackColor = Color.Transparent
        With Label
            .Font = MyBase.Font
            .BackColor = Me.BackColor
            .ForeColor = MyBase.ForeColor
        End With
        AddHandlers()
        Me.Margin = New Padding(0, 3, 0, 3)
    End Sub
    ''' <summary>Vytvoøí novou instanci <see cref="Label">Labelu</see> pro CTor</summary>
    Private Shared Function NewLabel() As Label
        Dim lbl As New Label
        With lbl
            .AutoSize = True
            .Margin = New Padding(0, 0, 0, 0)
        End With
        Return lbl
    End Function
    ''' <summary>CTor s textem</summary>
    ''' <param name="Text">Text položky</param>
    Public Sub New(ByVal Text As String)
        Me.New()
        Me.Text = Text
    End Sub
    ''' <summary><see cref="Label"/> realizující tento <see cref="ToolStripLabelLabel"/></summary>
    <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public ReadOnly Property Label() As Label
        Get
            Return Me.Control
        End Get
    End Property
#Region "Overriden"
    ''' <summary>Gets the default margin of an item.</summary>
    ''' <returns>A <see cref="T:System.Windows.Forms.Padding"></see> representing the margin.</returns>
    Protected Overrides ReadOnly Property DefaultMargin() As System.Windows.Forms.Padding
        Get
            Return New Padding(0)
        End Get
    End Property
    ''' <summary>Gets a value indicating what is displayed on the <see cref="T:System.Windows.Forms.ToolStripItem"></see>.</summary>
    ''' <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle"></see> values. The default is <see cref="F:System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText"></see>.</returns>
    Protected Overrides ReadOnly Property DefaultDisplayStyle() As System.Windows.Forms.ToolStripItemDisplayStyle
        Get
            Return ToolStripItemDisplayStyle.Text
        End Get
    End Property
    ''' <summary>Gets the internal spacing characteristics of the item.</summary>
    ''' <returns>One of the <see cref="T:System.Windows.Forms.Padding"></see> values.</returns>
    Protected Overrides ReadOnly Property DefaultPadding() As System.Windows.Forms.Padding
        Get
            Return New Padding(0)
        End Get
    End Property
    ''' <summary>Barva pozadí</summary>
    <DefaultValue(GetType(Color), "Transparent")> _
    Public Overrides Property BackColor() As System.Drawing.Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            MyBase.BackColor = value
        End Set
    End Property

#End Region
#Region "Overloads"
    ''' <summary>Gets or sets the alignment of text in the label.</summary>
    ''' <returns>One of the <see cref="T:System.Drawing.ContentAlignment"></see> values. The default is <see cref="F:System.Drawing.ContentAlignment.TopLeft"></see>.</returns>
    ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment"></see> values. </exception>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance), DefaultValue(1), SRDescription("LabelTextAlignDescr"), Localizable(True)> _
        Public Overloads Property TextAlign() As System.Drawing.ContentAlignment
        Get
            Return Label.TextAlign
        End Get
        Set(ByVal value As System.Drawing.ContentAlignment)
            Label.TextAlign = value
            MyBase.TextAlign = value
        End Set
    End Property
    '''' <summary>Gets or sets a value indicating whether the control is automatically resized to display its entire contents.</summary>
    '''' <returns>true if the control adjusts its width to closely fit its contents; otherwise, false. The default is false.</returns>
    '<DefaultValue(True)> _
    '<SRDescription("LabelAutoSizeDescr"), KnownCategory(KnownCategoryAttribute.KnownCategories.Layout)> _
    '<RefreshProperties(RefreshProperties.All), Localizable(True)> _
    'Public Overloads Property AutoSize() As Boolean
    '    Get
    '        Return Label.AutoSize
    '    End Get
    '    Set(ByVal value As Boolean)
    '        Label.AutoSize = value
    '        MyBase.AutoSize = value
    '        If value = True Then
    '            Label.Dock = DockStyle.None
    '            Label.Left = 0
    '            Label.Top = 0
    '        Else
    '            Label.Dock = DockStyle.Fill
    '        End If
    '    End Set
    'End Property
#End Region
#Region "Label"
    ''' <summary>Gets or sets a value indicating whether the ellipsis character (...) appears at the right edge of the <see cref="T:System.Windows.Forms.Label"></see>, denoting that the <see cref="T:System.Windows.Forms.Label"></see> text extends beyond the specified length of the <see cref="T:System.Windows.Forms.Label"></see>.</summary>
    ''' <returns>true if the additional label text is to be indicated by an ellipsis; otherwise, false. The default is false.</returns>
    <SRDescription("LabelAutoEllipsisDescr"), KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior), DefaultValue(False)> _
    Public Property AutoEllipsis() As Boolean
        Get
            Return Label.AutoEllipsis
        End Get
        Set(ByVal value As Boolean)
            Label.AutoEllipsis = value
        End Set
    End Property
    ''' <summary>Gets or sets the border style for the control.</summary>
    ''' <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle"></see> values. The default is BorderStyle.None.</returns>
    ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle"></see> values. </exception>
    <SRDescription("LabelBorderDescr"), DefaultValue(0), KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    Public Property BorderStyle() As BorderStyle
        Get
            Return Label.BorderStyle
        End Get
        Set(ByVal value As BorderStyle)
            Label.BorderStyle = value
        End Set
    End Property
    ''' <summary>Gets or sets the flat style appearance of the label control.</summary>
    ''' <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle"></see> values. The default value is Standard.</returns>
    ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle"></see> values. </exception>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance), DefaultValue(2), SRDescription("ButtonFlatStyleDescr")> _
    Public Property FlatStyle() As FlatStyle
        Get
            Return Label.FlatStyle
        End Get
        Set(ByVal value As FlatStyle)
            Label.FlatStyle = value
        End Set
    End Property
    ''' <summary>Gets or sets the image that is displayed on a <see cref="T:System.Windows.Forms.Label"></see>.</summary>
    ''' <returns>An <see cref="T:System.Drawing.Image"></see> displayed on the <see cref="T:System.Windows.Forms.Label"></see>. The default is null.</returns>
    <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance), SRDescription("ButtonImageDescr"), Localizable(True)> _
        Public Property LabelImage() As Image
        Get
            Return Label.Image
        End Get
        Set(ByVal value As Image)
            Label.Image = value
        End Set
    End Property
    ''' <summary>Gets or sets the alignment of an image that is displayed in the control.</summary>
    ''' <returns>One of the <see cref="T:System.Drawing.ContentAlignment"></see> values. The default is ContentAlignment.MiddleCenter.</returns>
    ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment"></see> values. </exception>
    <Localizable(True), SRDescription("ButtonImageAlignDescr"), KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance), DefaultValue(&H20)> _
    Public Property LabelImageAlign() As ContentAlignment
        Get
            Return Label.ImageAlign
        End Get
        Set(ByVal value As ContentAlignment)
            Label.ImageAlign = value
        End Set
    End Property
    ''' <summary>Gets or sets the index value of the image displayed on the <see cref="T:System.Windows.Forms.Label"></see>.</summary>
    ''' <returns>A zero-based index that represents the position in the <see cref="T:System.Windows.Forms.ImageList"></see> control (assigned to the <see cref="P:System.Windows.Forms.Label.ImageList"></see> property) where the image is located. The default is -1.</returns>
    ''' <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than the lower bounds of the <see cref="P:System.Windows.Forms.Label.ImageIndex"></see> property. </exception>
    <SRDescription("ButtonImageIndexDescr"), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor)), DefaultValue(-1), KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance), RefreshProperties(RefreshProperties.Repaint), Localizable(True), TypeConverter(GetType(ImageIndexConverter))> _
    Public Property LabelImageIndex() As Integer
        Get
            Return Label.ImageIndex
        End Get
        Set(ByVal value As Integer)
            Label.ImageIndex = value
        End Set
    End Property
    ''' <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.Label.ImageList"></see>.</summary>
    ''' <returns>A string representing the key of the image.</returns>
    <DefaultValue(""), Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor)), Localizable(True), RefreshProperties(RefreshProperties.Repaint), TypeConverter(GetType(ImageKeyConverter)), SRDescription("ButtonImageIndexDescr"), KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    Public Property LabelImageKey() As String
        Get
            Return Label.ImageKey
        End Get
        Set(ByVal value As String)
            Label.ImageKey = value
        End Set
    End Property
    ''' <summary>Raises the <see cref="System.Windows.Forms.Control.ParentChanged"/> event.</summary>
    ''' <param name="oldParent">The original parent of the item.</param>
    ''' <param name="newParent">The new parent of the item.</param>
    Protected Overrides Sub OnParentChanged(ByVal oldParent As System.Windows.Forms.ToolStrip, ByVal newParent As System.Windows.Forms.ToolStrip)
        If newParent IsNot Nothing Then Label.ImageList = Parent.ImageList Else Label.ImageList = Nothing
        MyBase.OnParentChanged(oldParent, newParent)
    End Sub
    ''' <summary>Gets or sets a value indicating whether the control interprets an ampersand character (&amp;) in the control's <see cref="P:System.Windows.Forms.Control.Text"></see> property to be an access key prefix character.</summary>
    ''' <returns>true if the label doesn't display the ampersand character and underlines the character after the ampersand in its displayed text and treats the underlined character as an access key; otherwise, false if the ampersand character is displayed in the text of the control. The default is true.</returns>
    <DefaultValue(True), SRDescription("LabelUseMnemonicDescr"), KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
    Public Property UseMnemonic() As Boolean
        Get
            Return Label.UseMnemonic
        End Get
        Set(ByVal value As Boolean)
            Label.UseMnemonic = value
        End Set
    End Property
#End Region
#Region "Events"
    ''' <summary>Adds handlers of events of <see cref="Label"/></summary>
    Private Sub AddHandlers()
        AddHandler Label.MouseDown, AddressOf Label_MouseDown
        AddHandler Label.MouseEnter, AddressOf Label_MouseEnter
        AddHandler Label.MouseHover, AddressOf Label_MouseHover
        AddHandler Label.MouseLeave, AddressOf Label_MouseLeave
        AddHandler Label.MouseMove, AddressOf Label_MouseMove
        AddHandler Label.MouseUp, AddressOf Label_MouseUp
        AddHandler Label.Click, AddressOf Label_Click
        AddHandler Label.DoubleClick, AddressOf Label_DoubleClick
        AddHandler Label.KeyDown, AddressOf Label_KeyDown
        AddHandler Label.KeyUp, AddressOf Label_KeyUp
        AddHandler Label.KeyPress, AddressOf Label_KeyPress
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.MouseDown">MouseDown</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        OnMouseDown(TransformMouseEventArgs(e))
    End Sub
    ''' <summary>Transforms <see cref="MouseEventArgs"/> by transforming its coordinates from coordinates of <see cref="Label"/> to coorinates of <see cref="ToolStripLabel"/></summary>
    ''' <param name="e"><see cref="MouseEventArgs"/> to transform</param>
    ''' <returns>Transformed <paramref name="e"/></returns>
    Private Function TransformMouseEventArgs(ByVal e As MouseEventArgs) As MouseEventArgs
        Return New MouseEventArgs(e.Button, e.Clicks, _
        e.X + Label.Left - Me.Bounds.Left, _
        e.Y + Label.Top - Me.Bounds.Top, _
        e.Delta)
    End Function
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.MouseUp">MouseUp</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
        OnMouseUp(TransformMouseEventArgs(e))
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.MouseMove">MouseMove</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        OnMouseMove(TransformMouseEventArgs(e))
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.MouseEnter">MouseEnter</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        OnMouseEnter(e)
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.MouseHover">MouseHover</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_MouseHover(ByVal sender As Object, ByVal e As EventArgs)
        OnMouseHover(e)
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.MouseLeave">MouseLeave</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)
        OnMouseLeave(e)
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.Click">Click</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_Click(ByVal sender As Object, ByVal e As EventArgs)
        OnClick(e)
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.DoubleClick">DoubleClick</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        OnDoubleClick(e)
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.KeyDown">KeyDown</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        OnKeyDown(e)
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.KeyUp">KeyUp</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs)
        OnKeyUp(e)
    End Sub
    ''' <summary>Handles <see cref="Label"/>.<see cref="Windows.Forms.Label.KeyPress">KeyPress</see></summary>
    ''' <param name="sender">Source of event</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Label_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        OnKeyPress(e)
    End Sub
#End Region
End Class

''' <summary>Description attribute that loads ist value from system resources via <see cref="Tools.ResourcesT.SystemResources"/> class</summary>
Public Class SRDescriptionAttribute : Inherits DescriptionAttribute
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="SRKey"/></summary>
    Private ReadOnly _SRKey As String
    ''' <summary>CTor (klíè zadaný jako text)</summary>
    ''' <param name="SRKey">Klíè systémového zdroje</param>
    Public Sub New(ByVal SRKey As String)
        Me._SRKey = SRKey
    End Sub
    ''' <summary>CTor (klíè zadaný z výbìru)</summary>
    ''' <param name="SRKey">Klíè systémového zdroje</param>
    Public Sub New(ByVal SRKey As Tools.ResourcesT.SystemResources.KnownValues)
        Me.New(CStr(SRKey))
    End Sub
    ''' <summary>Klíè systémového zdroje pro <see cref="Tools.ResourcesT.SystemResources.Value"/></summary>
    Public ReadOnly Property SRKey() As String
        Get
            Return _SRKey
        End Get
    End Property
    ''' <summary>Popis odpovídající klíèi systémových zdrojù <see cref="SRKey"/></summary>
    Public Overrides ReadOnly Property Description() As String
        Get
            Return Tools.ResourcesT.SystemResources.Value(SRKey)
        End Get
    End Property
End Class