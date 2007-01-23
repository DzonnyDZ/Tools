Imports System.Windows.Forms, tools.Windows.Forms.Utilities, System.Text, Tools.Collections.Generic
'#If Config <= Beta Then
'Stage: Beta
'Conditional compilation directive is commented out because its presence caused compiler warning.
'The conditionality of compilation of this file as well as of related files (which's name starts with 'LinkLabel.') is ensured by editing the Tools.vbproj file, where this file is marked as conditionally compiled.
'To edit the Tools.vbproj right-click the Tools project and select Unload Project. Then right-click it again and select Edit Tools.vbproj.
'Search for line like following:
'<Compile Include="Windows\Forms\LinkLabel.vb" Condition="$(Config)&lt;=$(Release)">
'Its preceded by comment.
Namespace Windows.Forms
    ''' <summary>Control taht allows user to chose from available encoding</summary>
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    <Drawing.ToolboxBitmap("Encoding.bmp")> _
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChange:="1/21/2007")> _
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
            lvwEncoding.ListViewItemSorter = New ListViewItemComparer()
            AddHandler Sorting.Changed, AddressOf lvwEncodingListViewItemSorter_Changed
            EncodingSelector_Resize(Me, EventArgs.Empty)
            MyBase.ResumeLayout()
        End Sub
        ''' <summary>Handles <see cref="ListViewItemComparer.Changed"/> event of <see cref="ListView.ListViewItemSorter"/> of <see cref="lvwEncoding"/></summary>
        Private Sub lvwEncodingListViewItemSorter_Changed(ByVal sender As IReportsChange, ByVal e As EventArgs)
            lvwEncoding.Sort()
        End Sub
        ''' <summary>Refreshes list of encodings</summary>
        ''' <remarks>Encodings are obtainded from <see cref="EncodingInfo.GetEncoding"/></remarks>
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
        ''' <summary>Removes first encoding with same <see cref="EncodingInfo.CodePage"/> as <paramref name="Encoding"/>'s one</summary>
        ''' <param name="Encoding">Encoding to remove</param>
        Public Overridable Sub RemoveEncoding(ByVal Encoding As EncodingInfo)
            If Encoding Is Nothing Then Return
            RemoveEncoding(Encoding.CodePage)
        End Sub
        ''' <summary>Removes first encoding with same <see cref="EncodingInfo.CodePage"/> as <paramref name="Encoding"/></summary>
        ''' <param name="CodePage">Code page to search for</param>
        Public Overridable Sub RemoveEncoding(ByVal CodePage As Integer)
            Dim i As Integer = 0
            For Each enc As EncodingInfoToDisplay In cmbEncoding.Items
                If enc.Info.CodePage = CodePage Then
                    cmbEncoding.Items.RemoveAt(i)
                    Exit For
                End If
                i += 1
            Next enc
            i = 0
            For Each enc As EncodingInfoToDisplay In lstEncoding.Items
                If enc.Info.CodePage = CodePage Then
                    lstEncoding.Items.RemoveAt(i)
                    Exit For
                End If
                i += 1
            Next enc
            i = 0
            For Each li As ListViewItem In lvwEncoding.Items
                If DirectCast(li.Tag, EncodingInfoToDisplay).Info.CodePage = CodePage Then
                    lvwEncoding.Items.RemoveAt(i)
                    Exit For
                End If
                i += 1
            Next li
        End Sub
        ''' <summary>Removes all encodings</summary>
        Public Overridable Sub Clear()
            lstEncoding.Items.Clear()
            lvwEncoding.Items.Clear()
            cmbEncoding.Items.Clear()
        End Sub
        ''' <summary>Adds given encoding (if encoding with same <see cref="EncodingInfo.CodePage"/> is not present in list)</summary>
        ''' <param name="Encoding">Encoding to add</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Encoding"/> is null</exception>
        Public Overridable Sub Add(ByVal Encoding As EncodingInfo)
            If Encoding Is Nothing Then _
                Throw New ArgumentNullException("Encoding cannot be added when its value is null", "Encoding")
            If Not ContainsCodePage(Encoding.CodePage) Then
                Dim dinf As New EncodingInfoToDisplay(Encoding)
                dinf.DisplayStyle = DisplayStyle
                lstEncoding.Items.Add(dinf)
                cmbEncoding.Items.Add(dinf)
                Dim itm As ListViewItem = lvwEncoding.Items.Add(Encoding.DisplayName)
                itm.Tag = dinf
                itm.SubItems.Add(Encoding.Name)
                itm.SubItems.Add(Encoding.CodePage)
            End If
        End Sub
        ''' <summary>Determines wheather encoding with <see cref="EncodingInfo.CodePage"/> equal to <paramref name="CP"/> is present in list</summary>
        ''' <param name="CP">Code page to search for</param>
        ''' <returns>True if encoding with same code page is found, otherwise not.</returns>
        Public Function ContainsCodePage(ByVal CP As Integer) As Boolean
            For Each enc As EncodingInfoToDisplay In lstEncoding.Items
                If enc.Info.CodePage = CP Then Return True
            Next enc
            Return False
        End Function
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
                        Throw New InvalidEnumArgumentException("value", value, GetType(EncodingSelectorStyle))
                End Select
                EncodingSelector_Resize(Me, EventArgs.Empty)
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
        ''' <summary>True while <see cref="ListView.SelectedItems"/> of <see cref="lvwEncoding"/> are being cleared. Prevents raising <see cref="ListView.SelectedIndexChanged"/> event multiple times</summary>
        Private ClearPending As Boolean = False

        Private Sub lstEncoding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstEncoding.SelectedIndexChanged
            If cmbEncoding.SelectedIndex >= cmbEncoding.Items.Count OrElse cmbEncoding.SelectedItem IsNot lstEncoding.SelectedItem Then cmbEncoding.SelectedItem = lstEncoding.SelectedItem
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
#Region "Events"
        Private Sub cmbEncoding_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEncoding.Click, lstEncoding.Click, lvwEncoding.Click
            OnClick(e)
        End Sub

        Private Sub cmbEncoding_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbEncoding.KeyDown, lstEncoding.KeyDown, lvwEncoding.KeyDown
            OnKeyDown(e)
            If e.KeyCode = Keys.Return AndAlso Me.SelectedEncoding IsNot Nothing Then
                RaiseEvent ItemDoubleClick(Me, New EncodingSelectorItemClickEventArgs(Me.SelectedEncoding))
            End If
        End Sub

        Private Sub cmbEncoding_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbEncoding.KeyPress, lstEncoding.KeyPress, lvwEncoding.KeyPress
            OnKeyPress(e)
        End Sub

        Private Sub cmbEncoding_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbEncoding.KeyUp, lstEncoding.KeyUp, lvwEncoding.KeyUp
            OnKeyUp(e)
        End Sub

        Private Sub cmbEncoding_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbEncoding.MouseDown, lstEncoding.MouseDown, lvwEncoding.MouseDown
            OnMouseDown(e)
        End Sub

        Private Sub cmbEncoding_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbEncoding.MouseMove, lstEncoding.MouseMove, lvwEncoding.MouseMove
            OnMouseMove(e)
        End Sub
        Private Sub cmbEncoding_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbEncoding.MouseUp, lstEncoding.MouseUp, lvwEncoding.MouseUp
            OnMouseUp(e)
        End Sub
#End Region
        Private Sub cmbEncoding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEncoding.SelectedIndexChanged
            If lstEncoding.SelectedItem IsNot cmbEncoding.SelectedItem Then lstEncoding.SelectedItem = cmbEncoding.SelectedItem
        End Sub

        Private Sub lvwMain_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvwEncoding.ColumnClick
            If Sorting.Column = e.Column Then
                Sorting.Descending = Not Sorting.Descending
            Else
                If e.Column = 2 Then
                    Sorting.Set(e.Column, , ListViewItemComparer.SortModes.Numeric)
                Else
                    Sorting.Set(e.Column)
                End If : End If
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
        <Category(CategoryAttributeValues.List), Description("raised after the SelectedIndex property is changed")> _
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
            ElseIf Style = EncodingSelectorStyle.ComboBox Then
                Me.Height = cmbEncoding.Height
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
#Region "Column headers"
#Region "Widths"
#Region "Name"
        ''' <summary>Defines width of <see cref="ColumnHeader"/> which displays <see cref="EncodingInfo.Name"/></summary>
        ''' <remarks>Applicable only when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/></remarks>
        <Description("Defines width of ColumnHeader which displays Name of encoding. Applicable only when Style is ListView")> _
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
        ''' <summary>Instructs designer if <see cref="NameColumnHeaderWidth"/> should be serialized</summary>
        ''' <returns>True if value differs from default</returns>
        ''' <remarks>User by Windows Forms Designer to determine wheater property should be initialized in designer-generated code or not</remarks>
        Private Function ShouldSerializeNameColumnHeaderWidth() As Boolean
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Return cohName.Width <> resources.GetString("cohName.Width")
        End Function
        ''' <summary>Resets <see cref="NameColumnHeaderWidth"/> to its default value</summary>
        ''' <remarks>Used by <see cref="PropertyGrid"/></remarks>
        Private Sub ResetNameColumnHeaderWidth()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            cohName.Width = resources.GetString("cohName.Width")
        End Sub
#End Region
#Region "DisplayName"
        ''' <summary>Defines width of <see cref="ColumnHeader"/> which displays <see cref="EncodingInfo.DisplayName"/></summary>
        ''' <remarks>Applicable only when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/></remarks>
        <Description("Defines width of ColumnHeader which displays DisplayName of encoding. Applicable only when Style is ListView")> _
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
        ''' <summary>Instructs designer if <see cref="DisplayNameColumnHeaderWidth"/> should be serialized</summary>
        ''' <returns>True if value differs from default</returns>
        ''' <remarks>User by Windows Forms Designer to determine wheater property should be initialized in designer-generated code or not</remarks>
        Private Function ShouldSerializeDisplayNameColumnHeaderWidth() As Boolean
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Return cohDisplayName.Width <> resources.GetString("cohDisplayName.Width")
        End Function
        ''' <summary>Resets <see cref="DisplayNameColumnHeaderWidth"/> to its default value</summary>
        ''' <remarks>Used by <see cref="PropertyGrid"/></remarks>
        Private Sub ResetDisplayNameColumnHeaderWidth()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            cohDisplayName.Width = resources.GetString("cohDisplayName.Width")
        End Sub
#End Region
#Region "CodePage"
        ''' <summary>Defines width of <see cref="ColumnHeader"/> which displays <see cref="EncodingInfo.CodePage"/></summary>
        ''' <remarks>Applicable only when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/></remarks>
        <Description("Defines width of ColumnHeader which displays CodePage of encoding. Applicable only when Style is ListView")> _
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
        ''' <summary>Instructs designer if <see cref="CodePageColumnHeaderWidth"/> should be serialized</summary>
        ''' <returns>True if value differs from default</returns>
        ''' <remarks>User by Windows Forms Designer to determine wheater property should be initialized in designer-generated code or not</remarks>
        Private Function ShouldSerializeCodePageColumnHeaderWidth() As Boolean
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Return cohCodePage.Width <> resources.GetString("cohCodePage.Width")
        End Function
        ''' <summary>Resets <see cref="CodePageColumnHeaderWidth"/> to its default value</summary>
        ''' <remarks>Used by <see cref="PropertyGrid"/></remarks>
        Private Sub ResetCodePageColumnHeaderWidth()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            cohCodePage.Width = resources.GetString("cohCodePage.Width")
        End Sub
#End Region
#End Region
#Region "Header texts"
#Region "Name"
        ''' <summary>Gets or sets text of <see cref="ColumnHeader"/> which displays <see cref="EncodingInfo.Name"/></summary>
        ''' <remarks>Applicable only when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/></remarks>
        <Description("Gets or sets text of ColumnHeader which displays Name of encoding. Applicable only when Style is ListView")> _
        <Category(CategoryAttributeValues.List), Localizable(True)> _
        Public Property NameColumnHeaderText() As String
            Get
                Return cohName.Text
            End Get
            Set(ByVal value As String)
                cohName.Text = value
            End Set
        End Property
        ''' <summary>Instructs designer if <see cref="NameColumnHeaderText"/> should be serialized</summary>
        ''' <returns>True if value differs from default</returns>
        ''' <remarks>User by Windows Forms Designer to determine wheater property should be initialized in designer-generated code or not</remarks>
        Private Function ShouldSerializeNameColumnHeaderText() As Boolean
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Return cohName.Text <> resources.GetString("cohName.Text")
        End Function
        ''' <summary>Resets <see cref="NameColumnHeaderText"/> to its default value</summary>
        ''' <remarks>Used by <see cref="PropertyGrid"/></remarks>
        Private Sub ResetNameColumnHeaderText()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            cohName.Text = resources.GetString("cohName.Text")
        End Sub
#End Region
#Region "Display Name"
        ''' <summary>Gets or sets text of <see cref="ColumnHeader"/> which displays <see cref="EncodingInfo.DisplayName"/></summary>
        ''' <remarks>Applicable only when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/></remarks>
        <Description("Gets or sets text of ColumnHeader which displays DisplayName of encoding. Applicable only when Style is ListView")> _
        <Category(CategoryAttributeValues.List), Localizable(True)> _
        Public Property DisplayNameColumnHeaderText() As String
            Get
                Return cohDisplayName.Text
            End Get
            Set(ByVal value As String)
                cohDisplayName.Text = value
            End Set
        End Property
        ''' <summary>Instructs designer if <see cref="DisplayNameColumnHeaderText"/> should be serialized</summary>
        ''' <returns>True if value differs from default</returns>
        ''' <remarks>User by Windows Forms Designer to determine wheater property should be initialized in designer-generated code or not</remarks>
        Private Function ShouldSerializeDisplayNameColumnHeaderText() As Boolean
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Return cohDisplayName.Text <> resources.GetString("cohDisplayName.Text")
        End Function
        ''' <summary>Resets <see cref="DisplayNameColumnHeaderText"/> to its default value</summary>
        ''' <remarks>Used by <see cref="PropertyGrid"/></remarks>
        Private Sub ResetDisplayNameColumnHeaderText()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            cohDisplayName.Text = resources.GetString("cohDisplayName.Text")
        End Sub
#End Region
#Region "CodePage"
        ''' <summary>Gets or sets text of <see cref="ColumnHeader"/> which displays <see cref="EncodingInfo.CodePage"/></summary>
        ''' <remarks>Applicable only when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/></remarks>
        <Description("Gets or sets text of ColumnHeader which displays CodePage of encoding. Applicable only when Style is ListView")> _
        <Category(CategoryAttributeValues.List), Localizable(True)> _
        Public Property CodePageColumnHeaderText() As String
            Get
                Return cohCodePage.Text
            End Get
            Set(ByVal value As String)
                cohCodePage.Text = value
            End Set
        End Property
        ''' <summary>Instructs designer if <see cref="CodePageColumnHeaderText"/> should be serialized</summary>
        ''' <returns>True if value differs from default</returns>
        ''' <remarks>User by Windows Forms Designer to determine wheater property should be initialized in designer-generated code or not</remarks>
        Private Function ShouldSerializeCodePageColumnHeaderText() As Boolean
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            Return cohCodePage.Text <> resources.GetString("cohCodePage.Text")
        End Function
        ''' <summary>Resets <see cref="CodePageColumnHeaderText"/> to its default value</summary>
        ''' <remarks>Used by <see cref="PropertyGrid"/></remarks>
        Private Sub ResetCodePageColumnHeaderText()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EncodingSelector))
            cohCodePage.Text = resources.GetString("cohCodePage.Text")
        End Sub
#End Region
#End Region
        ''' <summary>Specifies column and order of sorting</summary>
        ''' <remarks>Sorting can be changed by user by clicking column header. Sorting applies only when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/></remarks>
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Category(CategoryAttributeValues.List), Description("Specifies column and order of sorting. Applies only when Style is ListBox.")> _
        Public ReadOnly Property Sorting() As ListViewItemComparer
            Get
                Return lvwEncoding.ListViewItemSorter
            End Get
        End Property
        ''' <summary>Instructs designer if <see cref="Sorting"/> should be serialized</summary>
        ''' <returns>True if value differs from default</returns>
        ''' <remarks>User by Windows Forms Designer to determine wheater property should be initialized in designer-generated code or not</remarks>
        Private Function ShouldSerializeSorting() As Boolean
            Return Sorting.Column <> 0 OrElse Sorting.Descending
        End Function
        ''' <summary>Resets <see cref="Sorting"/> to its default value</summary>
        ''' <remarks>Used by <see cref="PropertyGrid"/></remarks>
        Private Sub ResetSorting()
            Sorting.Descending = False
            Sorting.Column = 0
        End Sub
        ''' <summary>Specifies all possible orders of columns</summary>
        ''' <remarks>
        ''' Column order specification is <see cref="Short"/> number divided into 3 4-bits long groups.
        ''' First group is position of DisplayName column, second is position of name column and third of code page column.
        ''' Positions in groups are 1-based.
        ''' </remarks>
        Public Enum enmColumnOrder As Short
            ''' <summary>Columns are ordered: display name - name - code page</summary>
            DisplayName_Name_CodePage = 1S Or 2S << 4 Or 3S << 8
            ''' <summary>Columns are ordered: display name - codepage - name</summary> 
            DisplayName_CodePage_Name = 1S Or 3S << 4 Or 2S << 8
            ''' <summary>Columns are ordered: name - display name - code page</summary>
            Name_DisplayName_CodePage = 2S Or 1S << 4 Or 3S << 8
            ''' <summary>Columns are ordered: name - code page - display name</summary>
            Name_CodePage_DisplayName = 3S Or 1S << 4 Or 2S << 8
            ''' <summary>Columns are ordered: code page - display name - name</summary>
            CodePage_DisplayName_Name = 2S Or 3S << 4 Or 1S << 8
            ''' <summary>Columns are ordered: code page - name - display name</summary>
            CodePage_Name_DisplayName = 3S Or 2S << 4 Or 1S << 8
        End Enum
        ''' <summary>Gets or setrs order of columns of <see cref="ListView"/> if <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListView"/></summary>
        <DefaultValue(GetType(enmColumnOrder), "DisplayName_Name_CodePage")> _
        <Category(CategoryAttributeValues.List), Description("Gets or sets order of columns of ListView when Style is ListView")> _
        Public Property ColumnOrder() As enmColumnOrder
            Get
                Return CShort(cohDisplayName.DisplayIndex + 1) Or CShort(cohName.DisplayIndex + 1) << 4 Or CShort(cohCodePage.DisplayIndex + 1) << 8
            End Get
            Set(ByVal value As enmColumnOrder)
                If [Enum].IsDefined(GetType(enmColumnOrder), value) Then
                    Dim DisplayName As Short = (value And &HFS) - 1
                    Dim Name As Short = ((value And &HF0S) >> 4) - 1
                    Dim CodePage As Short = ((value And &HF00S) >> 8) - 1
                    cohDisplayName.DisplayIndex = DisplayName
                    cohName.DisplayIndex = Name
                    cohCodePage.DisplayIndex = CodePage
                    lvwEncoding.Refresh()
                Else
                    Throw New InvalidEnumArgumentException("value", value, GetType(enmColumnOrder))
                End If
            End Set
        End Property
#End Region
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

        ''' <summary>Raised when <see cref="Style"/> is <see cref="EncodingSelectorStyle.ListBox"/> or <see cref="EncodingSelectorStyle.ListView"/> and user doubleclicks on item or raised when use presses the enter key and some item is selected not depending on <see cref="Style"/></summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        <Category(CategoryAttributeValues.Mouse), Description("Raised when Style is ListView or ListBox and user double clicks some item or raised when item is selected and user presses the enter key not depending on Style.")> _
        Public Event ItemDoubleClick(ByVal sender As EncodingSelector, ByVal e As EncodingSelectorItemClickEventArgs)

        ''' <summary>Erguments of the <see cref="ItemDoubleClick"/> event</summary>
        Public Class EncodingSelectorItemClickEventArgs : Inherits EventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Item">Encoding being selected when event occured</param>
            Public Sub New(ByVal Item As EncodingInfo)
                _Item = Item
            End Sub
            ''' <summary>Contains value of the <see cref="Item"/> property</summary>
            Private _Item As EncodingInfo
            ''' <summary>Encoding being selected when event occured</summary>
            Public ReadOnly Property Item() As EncodingInfo
                Get
                    Return _Item
                End Get
            End Property
        End Class

        Private Sub lvwEncoding_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvwEncoding.MouseDoubleClick
            If lvwEncoding.GetItemAt(e.X, e.Y) IsNot Nothing Then
                RaiseEvent ItemDoubleClick(Me, New EncodingSelectorItemClickEventArgs(DirectCast(lvwEncoding.GetItemAt(e.X, e.Y).Tag, EncodingInfoToDisplay)))
            End If
        End Sub
        Private Sub lstEncoding_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstEncoding.MouseDoubleClick
            If lstEncoding.SelectedIndex >= 0 Then
                Dim Rect As Drawing.Rectangle = lstEncoding.GetItemRectangle(lstEncoding.SelectedIndex)
                If e.X >= Rect.Left AndAlso e.X <= Rect.Right AndAlso e.Y >= Rect.Top AndAlso e.Y <= Rect.Bottom Then
                    RaiseEvent ItemDoubleClick(Me, New EncodingSelectorItemClickEventArgs(DirectCast(lstEncoding.Items(lstEncoding.SelectedIndex), EncodingInfoToDisplay)))
                End If
            End If
        End Sub
    End Class
End Namespace
'#End If 'See note at the beginning of this file