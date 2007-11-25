Imports Tools.CollectionsT.GenericT, Tools.ResourcesT
Namespace CollectionsT.GenericT
    ''' <summary>Tests selected functionality of <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/></summary>
    Friend Class frmReadOnlyListAdapter
        ''' <summary>List with read-write access</summary>
        Private RWList As New List(Of clsRW)
        ''' <summary>List awith read-only acces</summary>
        Private ROList As IReadOnlyList(Of clsRO) = New ReadOnlyListAdapter(Of clsRW, clsRO)(New List(Of clsRW))

        ''' <summary>Shows test form for testing <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/></summary>
        Public Overloads Shared Sub Test()
            Dim frm As New frmReadOnlyListAdapter
            frm.ShowDialog()
        End Sub

        ''' <summary>Initializes new instance test form</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = ToolsIcon
        End Sub

        ''' <summary>Deletes selected item from <see cref="RWList"/></summary>
        Private Sub lstRW_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstRW.KeyDown
            With CType(sender, ListBox)
                If .SelectedItem IsNot Nothing Then
                    RWList.Remove(.SelectedItem)
                    ShowRW()
                End If
            End With
        End Sub

        ''' <summary>Adds new item to <see cref="RWList"/></summary>
        Private Sub tsbAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbAdd.Click
            If tstAdd.Text = "" Then
                MsgBox("Type something!")
                Exit Sub
            End If
            RWList.Add(New clsRW(tstAdd.Text))
            ShowRW()
        End Sub

        ''' <summary>Type of items of <see cref="ROList"/></summary>
        Private Class clsRO
            ''' <summary>Value that represents the item</summary>
            Protected I As String
            ''' <summary>CTor</summary>
            ''' <param name="I">Value to identify instance</param>
            Public Sub New(ByVal I As String)
                Me.I = I
            End Sub
            ''' <summary>Shows walue and the 'RO' prefix</summary>
            Public Overrides Function ToString() As String
                Return "RO " & I
            End Function
            ''' <summary>Value that this instance contains</summary>
            Public Property val() As String
                Get
                    Return I
                End Get
                Set(ByVal value As String)
                    I = value
                End Set
            End Property
        End Class
        ''' <summary>Type of items of <see cref="RWList"/></summary>
        Private Class clsRW : Inherits clsRO
            ''' <summary>CTor</summary>
            ''' <param name="I">Value to identify instance</param>
            Public Sub New(ByVal I As String)
                MyBase.New(I)
            End Sub
            ''' <summary>Shows walue and the 'RW' prefix</summary>
            Public Overrides Function ToString() As String
                Return "RW " & I
            End Function
        End Class
        ''' <summary>Views content of <see cref="RWList"/></summary>
        Private Sub ShowRW()
            lstRW.Items.Clear()
            If RWList IsNot Nothing Then
                For Each itm As clsRW In RWList
                    lstRW.Items.Add(itm)
                Next itm
            End If
        End Sub
        ''' <summary>Views content of <see cref="ROList"/></summary>
        Private Sub ShowRO()
            lstRO.Items.Clear()
            For Each itm As clsRO In ROList
                lstRO.Items.Add(itm)
            Next itm
        End Sub

        ''' <summary>Creates new instance of <see cref="ReadOnlyListAdapter(Of clsRW, clsRO)"/></summary>
        Private Sub tswShowRO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbShowRO.Click
            ROList = New ReadOnlyListAdapter(Of clsRW, clsRO)(RWList)
            ShowRO()
        End Sub

        ''' <summary>Searches for first item in <see cref="IReadOnlyList(Of clsRO)"/> using <see cref="TextMatch"/></summary>
        Private Sub cmdSrch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSrch.Click
            Dim ix As Integer = ROList.FindIndex(AddressOf TextMatch)
            If ix >= 0 Then
                lstRO.SelectedIndex = ix
            Else
                MsgBox("Not found")
            End If
        End Sub

        ''' <summary>Simple match of item of type <see cref="clsRO"/> and value of <see cref="tstSrch"/>'s <see cref="ToolStripTextBox.Text"/> property.</summary>
        Private Function TextMatch(ByVal a As clsRO) As Boolean
            Return a.val = tstSrch.Text
        End Function

        ''' <summary>Determines if each item in <see cref="ROList"/> contains character '?' using predicate <see cref="QPred"/></summary>
        Private Sub tsbQAll1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbQAll.Click
            MsgBox(ROList.TrueForAll(AddressOf QPred))
        End Sub
        ''' <summary>Determines if value contains character '?'</summary>
        ''' <param name="a"><see cref="clsRO"/> which's <see cref="clsRO.val"/> should contain the '?' character</param>
        ''' <returns>True if <paramref name="a"/>'s <see cref="clsRO.val"/> contains the '?' character.</returns>
        Private Function QPred(ByVal a As clsRO) As Boolean
            Return a.val.Contains("?"c)
        End Function

    End Class

End Namespace