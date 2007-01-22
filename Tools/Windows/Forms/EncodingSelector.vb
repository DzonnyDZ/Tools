Imports System.Windows.Forms, tools.Windows.Forms.Utilities, System.Text, Tools.Collections.Generic
#If Config <= Alpha Then 'Stage: Alpha
Namespace Windows.Forms
    ''' <summary>Control taht allows user to chose from available encoding</summary>
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    <Drawing.ToolboxBitmap("Encoding.bmp")> _
    <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChange:="1/21/2007")> _
    <DefaultEvent("SelectedIndexChanged")> _
    <Localizable(True)> _
    Public Class EncodingSelector : Inherits UserControl
#Region "Designer generated"
        Protected WithEvents cmbEncoding As System.Windows.Forms.ComboBox
        Protected WithEvents lvwEncoding As System.Windows.Forms.ListView
        Friend WithEvents cohDisplayName As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohName As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohCodePage As System.Windows.Forms.ColumnHeader
        Protected WithEvents lstEncoding As System.Windows.Forms.ListBox
        <DebuggerNonUserCode()> _
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Me.lstEncoding = New System.Windows.Forms.ListBox
            Me.cmbEncoding = New System.Windows.Forms.ComboBox
            Me.lvwEncoding = New System.Windows.Forms.ListView
            Me.cohDisplayName = New System.Windows.Forms.ColumnHeader
            Me.cohName = New System.Windows.Forms.ColumnHeader
            Me.cohCodePage = New System.Windows.Forms.ColumnHeader
            Me.SuspendLayout()
            '
            'lstEncoding
            '
            resources.ApplyResources(Me.lstEncoding, "lstEncoding")
            Me.lstEncoding.FormattingEnabled = True
            Me.lstEncoding.Name = "lstEncoding"
            Me.lstEncoding.Sorted = True
            '
            'cmbEncoding
            '
            resources.ApplyResources(Me.cmbEncoding, "cmbEncoding")
            Me.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbEncoding.FormattingEnabled = True
            Me.cmbEncoding.Name = "cmbEncoding"
            Me.cmbEncoding.Sorted = True
            '
            'lvwEncoding
            '
            Me.lvwEncoding.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cohDisplayName, Me.cohName, Me.cohCodePage})
            resources.ApplyResources(Me.lvwEncoding, "lvwEncoding")
            Me.lvwEncoding.FullRowSelect = True
            Me.lvwEncoding.MultiSelect = False
            Me.lvwEncoding.Name = "lvwEncoding"
            Me.lvwEncoding.Sorting = System.Windows.Forms.SortOrder.Ascending
            Me.lvwEncoding.UseCompatibleStateImageBehavior = False
            Me.lvwEncoding.View = System.Windows.Forms.View.Details
            '
            'cohDisplayName
            '
            resources.ApplyResources(Me.cohDisplayName, "cohDisplayName")
            '
            'cohName
            '
            resources.ApplyResources(Me.cohName, "cohName")
            '
            'cohCodePage
            '
            resources.ApplyResources(Me.cohCodePage, "cohCodePage")
            '
            'EncodingSelector
            '
            Me.Controls.Add(Me.lvwEncoding)
            Me.Controls.Add(Me.cmbEncoding)
            Me.Controls.Add(Me.lstEncoding)
            Me.Name = "EncodingSelector"
            resources.ApplyResources(Me, "$this")
            Me.ResumeLayout(False)

        End Sub
#End Region
        ''' <summary>CTor</summary>
        Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            RefreshEncodings()
            BackColor = Drawing.SystemColors.Window
            ForeColor = Drawing.SystemColors.WindowText
        End Sub
        ''' <summary>Refreshes list of encodings</summary>
        Public Overridable Sub RefreshEncodings()
            Dim oldCP As Integer = SelectedCodepage
            lstEncoding.Items.Clear()
            cmbEncoding.Items.Clear()
            For Each inf As EncodingInfo In Encoding.GetEncodings()
                Dim dinf As New EncodingInfoToDisplay(inf)
                dinf.DisplayStyle = DisplayStyle
                lstEncoding.Items.Add(dinf)
                cmbEncoding.Items.Add(dinf)
                Dim itm As ListViewItem = lvwEncoding.Items.Add(inf.DisplayName)
                itm.Tag = dinf
                itm.SubItems.Add(inf.Name)
                itm.SubItems.Add(inf.CodePage)
            Next inf
            SelectedCodepage = oldCP
        End Sub
        ''' <summary>Contains value of the <see cref="DisplayStyle"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _DisplayStyle As String = "{0}"
        ''' <summary>Gets or sets format string used to display name of encodings in list</summary>
        ''' <remarks>
        ''' Property is ignored when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/>
        ''' <list>
        ''' <listheader>There are 3 parameters tah can be passed to format string:</listheader>
        ''' <item>{0} - <see cref="EncodingInfo.DisplayName"/></item>
        ''' <item>{1} - <see cref="EncodingInfo.Name"/></item>
        ''' <item>{2} - <see cref="EncodingInfo.CodePage"/></item>
        ''' </list>
        ''' <para>Expamples:
        ''' <example>{0}</example> displays user-friendly name of encoding
        ''' <example>{1} (CP {2})</example> displays IANA-registered name of encoding and its codepage.
        ''' </para>
        ''' <seealso cref="String.Format"/>
        ''' <seealso cref="EncodingInfoToDisplay.DisplayStyle"/>
        ''' </remarks>
        <DefaultValue("{0}"), Category(CategoryAttributeValues.Appearance)> _
        <Description("Gets or sets format string used to display name of encodings in list." & vbCrLf & "{0} is replaced by user friendly encoding name, {1} is replaced with IANA-registered name and {2} is replaced with code page number")> _
        Public Property DisplayStyle() As String
            <DebuggerStepThrough()> Get
                Return _DisplayStyle
            End Get
            Set(ByVal value As String)
                Dim Change As Boolean = value <> _DisplayStyle
                _DisplayStyle = value
                If Change Then RefreshEncodings()
            End Set
        End Property
        ''' <summary>Styles of <see cref="EncodingSelector"/></summary>
        Public Enum EncodingSelectorStyle
            ''' <summary><see cref="EncodingSelector"/> is realized by <see cref="ComboBox"/></summary>
            ComboBox
            ''' <summary><see cref="EncodingSelector"/> is realized by <see cref="ListBox"/></summary>
            ListBox
            ''' <summary><see cref="EncodingSelector"/> is realized by <see cref="ListView"/></summary>
            ListView
        End Enum
        ''' <summary>Defines control used for showing encodings</summary>
        ''' <exception cref="InvalidEnumArgumentException">Setting value that is not member of <see cref="EncodingSelectorStyle"/></exception>
        <DefaultValue(GetType(EncodingSelectorStyle), "ComboBox")> _
        <Category(CategoryAttributeValues.Appearance)> _
        <Description("Defines control used for showing encodings")> _
        Public Overridable Property Style() As EncodingSelectorStyle
            Get
                Select Case True
                    Case lstEncoding.Visible : Return EncodingSelectorStyle.ListBox
                    Case lvwEncoding.Visible : Return EncodingSelectorStyle.ListView
                    Case Else : Return EncodingSelectorStyle.ComboBox
                End Select
            End Get
            Set(ByVal value As EncodingSelectorStyle)
                Dim OldV As EncodingSelectorStyle = Style
                lstEncoding.Visible = False
                lvwEncoding.Visible = False
                cmbEncoding.Visible = False
                Select Case value
                    Case EncodingSelectorStyle.ComboBox
                        cmbEncoding.Visible = True
                    Case EncodingSelectorStyle.ListBox
                        lstEncoding.Visible = True
                    Case EncodingSelectorStyle.ListView
                        lvwEncoding.Visible = True
                    Case Else
                        Select Case OldV
                            Case EncodingSelectorStyle.ComboBox
                                cmbEncoding.Visible = True
                            Case EncodingSelectorStyle.ListBox
                                lstEncoding.Visible = True
                            Case EncodingSelectorStyle.ListView
                                lvwEncoding.Visible = True
                        End Select
                        Throw New InvalidEnumArgumentException("Unknown value of Style", value, GetType(EncodingSelectorStyle))
                End Select
                MaximumSize = MaximumSize
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="MaximumSize"/> property</summary>
        ''' <remarks>Actual <see cref="UserControl.MaximumSize"/> is equal to <see cref="_MaximumSize"/> only when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListBox"/> otherwise its <see cref="Drawing.Size.Height"/> is set to <see cref="ComboBox.Height"/> of <see cref="cmbEncoding"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MaximumSize As System.Drawing.Size
        ''' <summary>Gets or sets the size that is the upper limit that <see cref="System.Windows.Forms.Control.GetPreferredSize"/> can specify.</summary>
        ''' <returns>An ordered pair of type <see cref="System.Drawing.Size"/> representing the width and height of a rectangle.</returns>
        Public NotOverridable Overrides Property MaximumSize() As System.Drawing.Size
            Get
                Return _MaximumSize
            End Get
            Set(ByVal value As System.Drawing.Size)
                _MaximumSize = value
                If Me.Style = EncodingSelectorStyle.ComboBox Then
                    MyBase.MaximumSize = New Drawing.Size(value.Width, cmbEncoding.Height)
                Else
                    MyBase.MaximumSize = value
                End If
            End Set
        End Property
        ''' <summary>Gets the length and height, in pixels, that is specified as the default maximum size of a control.</summary>
        ''' <returns>A <see cref="System.Drawing.Size"/> representing the size of the control.</returns>
        ''' <remarks>If <see cref="Style"/> is <see cref="EncodingSelectorStyle.ComboBox"/> then height is limited to maximum height of <see cref="ComboBox"/></remarks>
        Protected Overrides ReadOnly Property DefaultMaximumSize() As System.Drawing.Size
            Get
                If cmbEncoding IsNot Nothing AndAlso Style = EncodingSelectorStyle.ComboBox Then
                    Return New Drawing.Size(MyBase.DefaultMaximumSize.Width, cmbEncoding.Height)
                Else
                    Return MyBase.DefaultMaximumSize
                End If
                Return MyBase.DefaultMaximumSize
            End Get
        End Property
        ''' <summary>Gets the length and height, in pixels, that is specified as the default minimum size of a control.</summary>
        ''' <returns>A <see cref="System.Drawing.Size"/> representing the size of the control.</returns>
        Protected Overrides ReadOnly Property DefaultMinimumSize() As System.Drawing.Size
            Get
                If cmbEncoding Is Nothing Then
                    Return MyBase.DefaultMinimumSize
                Else
                    Return New Drawing.Size(MyBase.DefaultMinimumSize.Width, cmbEncoding.Height)
                End If
            End Get
        End Property
        ''' <summary>Gets lis of actualy displayed <see cref="EncodingInfo"/>s</summary>
        ''' <remarks>Avoid using this property for getting count of encoding. Use <see cref="Count"/> instead</remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overridable ReadOnly Property Encodings() As IReadOnlyList(Of EncodingInfo)
            Get
                Dim l As New List(Of EncodingInfo)
                For Each itm As EncodingInfoToDisplay In lstEncoding.Items
                    l.Add(itm)
                Next itm
                Return New ReadOnlyListAdapter(Of EncodingInfo)(l)
            End Get
        End Property
        ''' <summary>Number of encodings displayed</summary>
        <Browsable(False)> _
        Public Overridable ReadOnly Property Count() As Integer
            Get
                Return lstEncoding.Items.Count
            End Get
        End Property
        ''' <summary>Wrapper class aroun <see cref="EncodingInfo"/> in order to be displayed in list</summary>
        <DebuggerDisplay("{ToString}")> _
        Protected Class EncodingInfoToDisplay
            ''' <summary>Copntains value of the <see cref="Info"/> property</summary>
            Private _Info As EncodingInfo
            ''' <summary>CTor</summary>
            ''' <param name="Info"><see cref="EncodingInfo"/> to be wrapped</param>
            Public Sub New(ByVal Info As EncodingInfo)
                Me.Info = Info
            End Sub
            ''' <summary>Contains value of the <see cref="DisplayStyle"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _DisplayStyle As String = "{0}"
            ''' <summary>Gets or sets format string used to display name of encoding in the <see cref="ToString"/> function</summary>
            ''' <remarks>
            ''' <list>
            ''' <listheader>There are 3 parameters tah can be passed to format string:</listheader>
            ''' <item>{0} - <see cref="EncodingInfo.DisplayName"/></item>
            ''' <item>{1} - <see cref="EncodingInfo.Name"/></item>
            ''' <item>{2} - <see cref="EncodingInfo.CodePage"/></item>
            ''' </list>
            ''' <seealso cref="String.Format"/>
            ''' </remarks>
            <DefaultValue("{0}")> _
            Public Property DisplayStyle() As String
                <DebuggerStepThrough()> Get
                    Return _DisplayStyle
                End Get
                <DebuggerStepThrough()> Set(ByVal value As String)
                    _DisplayStyle = value
                End Set
            End Property
            ''' <summary>Returns a <see cref="System.String"/> that represents the current <see cref="EncodingInfoToDisplay"/>.</summary>
            ''' <returns><see cref="EncodingInfo.DisplayName"/> of <see cref="Info"/></returns>
            <DebuggerStepThrough()> Public Overrides Function ToString() As String
                Return String.Format(DisplayStyle, Info.DisplayName, Info.Name, Info.CodePage)
            End Function
            ''' <summary>Gets or sets <see cref="EncodingInfo"/> being wrapped</summary>
            ''' <exception cref="ArgumentNullException">value is being set to null</exception>
            Public Property Info() As EncodingInfo
                <DebuggerStepThrough()> Get
                    Return _Info
                End Get
                Set(ByVal value As EncodingInfo)
                    If value Is Nothing Then Throw New ArgumentNullException("value", "Info cannot be null")
                    _Info = value
                End Set
            End Property
            ''' <summary>Converts <see cref="EncodingInfo"/> to <see cref="EncodingInfoToDisplay"/></summary>
            ''' <param name="a"><see cref="EncodingInfo"/> to be converted</param>
            ''' <returns>New instance of <see cref="EncodingInfoToDisplay"/> initialized with <paramref name="a"/> or null when <paramref name="a"/> is null.</returns>
            Public Shared Widening Operator CType(ByVal a As EncodingInfo) As EncodingInfoToDisplay
                If a Is Nothing Then
                    Return Nothing
                Else
                    Return New EncodingInfoToDisplay(a)
                End If
            End Operator
            ''' <summary>Converts <see cref="EncodingInfoToDisplay"/> to <see cref="EncodingInfo"/></summary>
            ''' <param name="a"><see cref="EncodingInfoToDisplay"/> to be converted</param>
            ''' <returns>Value of the <see cref="Info"/> property of <paramref name="a"/> or null when <paramref name="a"/> is null.</returns>
            Public Shared Widening Operator CType(ByVal a As EncodingInfoToDisplay) As EncodingInfo
                If a Is Nothing Then
                    Return Nothing
                Else
                    Return a.Info
                End If
            End Operator
        End Class

        Private ClearPending As Boolean = False

        Private Sub lstEncoding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstEncoding.SelectedIndexChanged
            If cmbEncoding.SelectedItem IsNot lstEncoding.SelectedItem Then cmbEncoding.SelectedItem = lstEncoding.SelectedItem
            If lstEncoding.SelectedItem IsNot Nothing Then
                If (lvwEncoding.SelectedItems.Count > 0 AndAlso lstEncoding.SelectedItem IsNot lvwEncoding.SelectedItems(0).Tag) OrElse lvwEncoding.SelectedItems.Count = 0 Then
                    ClearPending = True
                    Try : lvwEncoding.SelectedItems.Clear()
                    Finally : ClearPending = False : End Try
                    For i As Integer = 0 To lvwEncoding.Items.Count - 1
                        If lvwEncoding.Items(i).Tag Is cmbEncoding.SelectedItem Then
                            lvwEncoding.SelectedIndices.Add(i)
                            Exit For
                        End If
                    Next i
                End If
            Else
                lvwEncoding.SelectedItems.Clear()
            End If
            OnSelectedIndexChanged()
        End Sub
        Private Sub cmbEncoding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEncoding.SelectedIndexChanged
            If lstEncoding.SelectedItem IsNot cmbEncoding.SelectedItem Then lstEncoding.SelectedItem = cmbEncoding.SelectedItem
        End Sub

        Private Sub lvwMain_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvwEncoding.ColumnClick
            If lvwEncoding.ListViewItemSorter IsNot Nothing AndAlso TypeOf lvwEncoding.ListViewItemSorter Is ListViewItemComparer AndAlso CType(lvwEncoding.ListViewItemSorter, ListViewItemComparer).Column = e.Column Then
                With CType(lvwEncoding.ListViewItemSorter, ListViewItemComparer)
                    .Descending = Not .Descending
                End With
                lvwEncoding.Sort()
            Else
                lvwEncoding.ListViewItemSorter = New ListViewItemComparer(e.Column)
                lvwEncoding.Sorting = SortOrder.Ascending
            End If
        End Sub
        Private Sub lvwMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwEncoding.SelectedIndexChanged
            If ClearPending Then Exit Sub
            If lvwEncoding.SelectedItems.Count > 0 Then
                If lvwEncoding.SelectedItems(0).Tag IsNot lstEncoding.SelectedItem Then lstEncoding.SelectedItem = lvwEncoding.SelectedItems(0).Tag
            Else
                lstEncoding.SelectedItem = Nothing
            End If
        End Sub


        ''' <summary>Raised after the <see cref="SelectedIndex"/> property is changed</summary>
        ''' <param name="sender">The source of the evemt</param> 
        ''' <param name="e">Arguments</param>
        Public Event SelectedIndexChanged(ByVal sender As EncodingSelector, ByVal e As EventArgs)

        ''' <summary>Raises the <see cref="SelectedIndexChanged"/> event</summary>
        ''' <remarks>Note to inheritors: Call base-class method <see cref="OnSelectedIndexChanged"/> in order the <see cref="SelectedIndexChanged"/> event to be raised</remarks>
        Protected Overridable Sub OnSelectedIndexChanged()
            RaiseEvent SelectedIndexChanged(Me, New EventArgs)
        End Sub
        ''' <summary>Gets or sets index of currently selected encoding or -1 if no encoding is selected</summary>
        ''' <exception cref="System.ArgumentOutOfRangeException">The assigned value is less than -1 or greater than or equal to <see cref="Count"/>.</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property SelectedIndex() As Integer
            Get
                Return lstEncoding.SelectedIndex
            End Get
            Set(ByVal value As Integer)
                lstEncoding.SelectedIndex = value
            End Set
        End Property
        ''' <summary>Gets or sets currently selected encoding</summary>
        ''' <exception cref="ArgumentException">Attempt to select encoding thet is not in list</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
         Public Property SelectedEncoding() As EncodingInfo
            Get
                Return CType(lstEncoding.SelectedItem, EncodingInfoToDisplay)
            End Get
            Set(ByVal value As EncodingInfo)
                For i As Integer = 0 To lstEncoding.Items.Count - 1
                    If CType(lstEncoding.Items(i), EncodingInfo).CodePage = value.CodePage Then
                        lstEncoding.SelectedIndex = i
                        Exit Property
                    End If
                Next i
                Throw New ArgumentException("Encoding with same codepage is not in list", "value")
            End Set
        End Property
        ''' <summary>Gets code page identifier of selected encoding or tries to select encoding with given code page</summary>
        ''' <value>Searches for encoding with given codepage. If found selects it. If <paramref name="value"/> is -1 then encoding is unselected.</value>
        ''' <returns>Code page identifier of selected encoding</returns>
        ''' <remarks><seealso cref="EncodingInfo.CodePage"/></remarks>
        <Description("Gets code page identifier of selected encoding or tries to select encoding with given code page")> _
        <Category(CategoryAttributeValues.List)> _
        <DefaultValue(-1I)> _
        Public Property SelectedCodepage() As Integer
            Get
                If Me.SelectedEncoding Is Nothing Then
                    Return -1
                Else
                    Return Me.SelectedEncoding.CodePage
                End If
            End Get
            Set(ByVal value As Integer)
                If value = -1 Then
                    lstEncoding.SelectedIndex = -1
                Else
                    For i As Integer = 0 To lstEncoding.Items.Count - 1
                        If CType(DirectCast(lstEncoding.Items(i), EncodingInfoToDisplay), EncodingInfo).CodePage = value Then
                            lstEncoding.SelectedIndex = i
                            Exit For
                        End If
                    Next i
                End If
            End Set
        End Property
        ''' <summary>Gets The Internet Assigned Numbers Authority (IANA) name of selected encoding or tries to select encoding with given name</summary>
        ''' <value>Searches for encoding with given name. If found selects it. If <paramref name="value"/> is an empty string then encoding is unselected.</value>
        ''' <returns>The Internet Assigned Numbers Authority (IANA) name of selected encoding</returns>
        ''' <remarks><seealso cref="EncodingInfo.Name"/></remarks>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <Category(CategoryAttributeValues.List)> _
        <Description("Gets The Internet Assigned Numbers Authority (IANA) name of selected encoding or tries to select encoding with given name")> _
        <DefaultValue("")> _
        Public Property SelectedName() As String
            Get
                If Me.SelectedEncoding Is Nothing Then
                    Return ""
                Else
                    Return Me.SelectedEncoding.Name
                End If
            End Get
            Set(ByVal value As String)
                If value = "" Then
                    lstEncoding.SelectedIndex = -1
                Else
                    For i As Integer = 0 To lstEncoding.Items.Count - 1
                        If CType(DirectCast(lstEncoding.Items(i), EncodingInfoToDisplay), EncodingInfo).Name = value Then
                            lstEncoding.SelectedIndex = i
                            Exit For
                        End If
                    Next i
                End If
            End Set
        End Property

        Private Sub EncodingSelector_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
            If Style = EncodingSelectorStyle.ListBox Then
                lstEncoding.Height = Me.Height
                If Me.Height > lstEncoding.Height Then Me.Height = lstEncoding.Height
            End If
        End Sub
        ''' <summary>Gets or sets the background color for the control.</summary>
        ''' <returns>A System.Drawing.Color that represents the background color of the control</returns>
        <DefaultValue(GetType(Drawing.Color), "Window")> _
        Public Overrides Property BackColor() As System.Drawing.Color
            Get
                Return MyBase.BackColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                MyBase.BackColor = value
                lvwEncoding.BackColor = value
                cmbEncoding.BackColor = value
                lstEncoding.BackColor = value
            End Set
        End Property
        ''' <summary>Gets or sets the foreground color of the control.</summary>
        ''' <returns>The foreground <see cref="System.Drawing.Color"/> of the control.</returns>
        <DefaultValue(GetType(Drawing.Color), "WindowText")> _
        Public Overrides Property ForeColor() As System.Drawing.Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(ByVal value As System.Drawing.Color)
                MyBase.ForeColor = value
                lvwEncoding.ForeColor = value
                cmbEncoding.ForeColor = value
                lstEncoding.ForeColor = value
            End Set
        End Property
        ''' <summary>Gets or sets the background image displayed in the control.</summary>
        ''' <returns>An <see cref="System.Drawing.Image"/> that represents the image to display in the background of the control.</returns>
        ''' <remarks>Applies only when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/></remarks>
        Public Overrides Property BackgroundImage() As System.Drawing.Image
            Get
                Return lvwEncoding.BackgroundImage
            End Get
            Set(ByVal value As System.Drawing.Image)
                lvwEncoding.BackgroundImage = value
            End Set
        End Property
        ''' <summary>Gets or sets an <see cref="System.Windows.Forms.ImageLayout"/> value.</summary>
        ''' <returns>One of the <see cref="System.Windows.Forms.ImageLayout"/> values.</returns>
        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value specified is not one of the <see cref="System.Windows.Forms.ImageLayout"/> values</exception>
        Public Overrides Property BackgroundImageLayout() As System.Windows.Forms.ImageLayout
            Get
                Return lvwEncoding.BackgroundImageLayout
            End Get
            Set(ByVal value As System.Windows.Forms.ImageLayout)
                lvwEncoding.BackgroundImageLayout = value
            End Set
        End Property
#Region "Column headers" 'TODO: Comments and descriptions, maybe another properties
        'TODO: ShouldSerialize / Reset for widths
        <Category(CategoryAttributeValues.List)> _
        <DefaultValue(70), Localizable(True)> _
        Public Property NameColumnHeaderWidth() As Integer
            Get
                Return cohName.Width
            End Get
            Set(ByVal value As Integer)
                cohName.Width = value
            End Set
        End Property
        <Category(CategoryAttributeValues.List)> _
        <DefaultValue(120), Localizable(True)> _
        Public Property DisplayNameColumnHeaderWidth() As Integer
            Get
                Return cohDisplayName.Width
            End Get
            Set(ByVal value As Integer)
                cohDisplayName.Width = value
            End Set
        End Property
        <Category(CategoryAttributeValues.List)> _
        <DefaultValue(70), Localizable(True)> _
        Public Property CodePageColumnHeaderWidth() As Integer
            Get
                Return cohCodePage.Width
            End Get
            Set(ByVal value As Integer)
                cohCodePage.Width = value
            End Set
        End Property

        <Category(CategoryAttributeValues.List), Localizable(True)> _
       Public Property NameColumnHeaderText() As String
            Get
                Return cohName.Text
            End Get
            Set(ByVal value As String)
                cohName.Text = value
            End Set
        End Property
        Private Function ShouldSerializeNameColumnHeaderText() As Boolean
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Return cohName.Text <> resources.GetString("cohName.Text")
        End Function
        Private Sub ResetNameColumnHeaderText()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            cohName.Text = resources.GetString("cohName.Text")
        End Sub
        <Category(CategoryAttributeValues.List), Localizable(True)> _
        Public Property DisplayNameColumnHeaderText() As String
            Get
                Return cohDisplayName.Text
            End Get
            Set(ByVal value As String)
                cohDisplayName.Text = value
            End Set
        End Property
        Private Function ShouldSerializeDisplayNameColumnHeaderText() As Boolean
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Return cohDisplayName.Text <> resources.GetString("cohDisplayName.Text")
        End Function
        Private Sub ResetDisplayNameColumnHeaderText()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            cohDisplayName.Text = resources.GetString("cohDisplayName.Text")
        End Sub
        <Category(CategoryAttributeValues.List), Localizable(True)> _
        Public Property CodePageColumnHeaderText() As String
            Get
                Return cohCodePage.Text
            End Get
            Set(ByVal value As String)
                cohCodePage.Text = value
            End Set
        End Property
        Private Function ShouldSerializeCodePageColumnHeaderText() As Boolean
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Return cohCodePage.Text <> resources.GetString("cohCodePage.Text")
        End Function
        Private Sub ResetCodePageColumnHeaderText()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            cohCodePage.Text = resources.GetString("cohCodePage.Text")
        End Sub
#End Region
    End Class
End Namespace
#End If