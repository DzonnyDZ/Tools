Imports System.Windows.Forms, tools.Windows.Forms.Utilities, System.Text, Tools.Collections.Generic
#If Config <= Nightly Then 'Stage: Nightly
Namespace Windows.Forms
    ''' <summary>Control taht allows user to chose from available encoding</summary>
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    <Drawing.ToolboxBitmap("Encoding.bmp")> _
    <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChange:="1/21/2007")> _
    <DefaultEvent("SelectedIndexChanged")> _
    Public Class EncodingSelector : Inherits UserControl
#Region "Designer generated"
        Protected WithEvents cmbEncoding As System.Windows.Forms.ComboBox
        Protected WithEvents lstEncoding As System.Windows.Forms.ListBox
        Private Sub InitializeComponent()
            Me.lstEncoding = New System.Windows.Forms.ListBox
            Me.cmbEncoding = New System.Windows.Forms.ComboBox
            Me.SuspendLayout()
            '
            'lstEncoding
            '
            Me.lstEncoding.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstEncoding.FormattingEnabled = True
            Me.lstEncoding.Location = New System.Drawing.Point(0, 0)
            Me.lstEncoding.Margin = New System.Windows.Forms.Padding(0)
            Me.lstEncoding.Name = "lstEncoding"
            Me.lstEncoding.Size = New System.Drawing.Size(434, 17)
            Me.lstEncoding.Sorted = True
            Me.lstEncoding.TabIndex = 0
            Me.lstEncoding.Visible = False
            '
            'cmbEncoding
            '
            Me.cmbEncoding.Dock = System.Windows.Forms.DockStyle.Fill
            Me.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbEncoding.FormattingEnabled = True
            Me.cmbEncoding.Location = New System.Drawing.Point(0, 0)
            Me.cmbEncoding.Margin = New System.Windows.Forms.Padding(0)
            Me.cmbEncoding.Name = "cmbEncoding"
            Me.cmbEncoding.Size = New System.Drawing.Size(434, 21)
            Me.cmbEncoding.Sorted = True
            Me.cmbEncoding.TabIndex = 1
            '
            'EncodingSelector
            '
            Me.Controls.Add(Me.cmbEncoding)
            Me.Controls.Add(Me.lstEncoding)
            Me.Name = "EncodingSelector"
            Me.Size = New System.Drawing.Size(434, 21)
            Me.ResumeLayout(False)

        End Sub
#End Region
        ''' <summary>CTor</summary>
        Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            RefreshEncodings()
        End Sub
        ''' <summary>Refreshes list of encodings</summary>
        Public Overridable Sub RefreshEncodings()
            lstEncoding.Items.Clear()
            cmbEncoding.Items.Clear()
            For Each inf As EncodingInfo In Encoding.GetEncodings()
                Dim dinf As New EncodingInfoToDisplay(inf)
                lstEncoding.Items.Add(dinf)
                cmbEncoding.Items.Add(dinf)
            Next inf
        End Sub
        ''' <summary>Styles of <see cref="EncodingSelector"/></summary>
        Public Enum EncodingSelectorStyle
            ''' <summary><see cref="EncodingSelector"/> is realized by <see cref="ComboBox"/></summary>
            ComboBox
            ''' <summary><see cref="EncodingSelector"/> is realized by <see cref="ListBox"/></summary>
            ListBox
        End Enum
        ''' <summary>Defines control used for showing encodings</summary>
        <DefaultValue(GetType(EncodingSelectorStyle), "ComboBox")> _
        <Category(CategoryAttributeValues.Appearance)> _
        <Description("Defines control used for showing encodings")> _
        Public Overridable Property Style() As EncodingSelectorStyle
            Get
                If cmbEncoding.Visible Then Return EncodingSelectorStyle.ComboBox Else Return EncodingSelectorStyle.ListBox
            End Get
            Set(ByVal value As EncodingSelectorStyle)
                lstEncoding.Visible = value = EncodingSelectorStyle.ListBox
                cmbEncoding.Visible = Not lstEncoding.Visible
            End Set
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
        Protected Class EncodingInfoToDisplay
            ''' <summary>Copntains value of the <see cref="Info"/> property</summary>
            Private _Info As EncodingInfo
            ''' <summary>CTor</summary>
            ''' <param name="Info"><see cref="EncodingInfo"/> to be wrapped</param>
            Public Sub New(ByVal Info As EncodingInfo)
                Me.Info = Info
            End Sub
            ''' <summary>Returns a <see cref="System.String"/> that represents the current <see cref="EncodingToDisplay"/>.</summary>
            ''' <returns><see cref="EncodingInfo.DisplayName"/> of <see cref="Info"/></returns>
            Public Overrides Function ToString() As String
                Return Info.DisplayName
            End Function
            ''' <summary>Gets or sets <see cref="EncodingInfo"/> being wrapped</summary>
            ''' <exception cref="ArgumentNullException">value is being set to null</exception>
            Public Property Info() As EncodingInfo
                Get
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

        Private Sub lstEncoding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstEncoding.SelectedIndexChanged
            If cmbEncoding.SelectedIndex <> lstEncoding.SelectedIndex Then cmbEncoding.SelectedIndex = lstEncoding.SelectedIndex
            OnSelectedIndexChanged()
        End Sub
        Private Sub cmbEncoding_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEncoding.SelectedIndexChanged
            If lstEncoding.SelectedIndex <> cmbEncoding.SelectedIndex Then lstEncoding.SelectedIndex = cmbEncoding.SelectedIndex
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
                Return lstEncoding.SelectedItem
            End Get
            Set(ByVal value As EncodingInfo)
                For i As Integer = 0 To lstEncoding.Items.Count - 1
                    If CType(lstEncoding.Items(i), EncodingInfo).CodePage = value.CodePage Then
                        lstEncoding.SelectedIndex = i
                        Exit For
                    End If
                Next i
                Throw New ArgumentException("Encoding with same codepage is not in list", "value")
            End Set
        End Property
        Public Property SelectedCodepage() As Integer
            Get

            End Get
            Set(ByVal value As Integer)

            End Set
        End Property
        Public Property SelectedName() As String
            Get

            End Get
            Set(ByVal value As String)

            End Set
        End Property
    End Class
End Namespace
#End If