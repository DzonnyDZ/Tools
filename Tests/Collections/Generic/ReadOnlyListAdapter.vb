Imports Tools.Collections.Generic
Namespace Collections.Generic
    Public Class frmReadOnlyListAdapter
        Private RWList As List(Of clsRW)
        Private ROList As ReadOnlyListAdapter(Of clsRW, clsRO)

        Public Overloads Shared Sub Test()
            Dim frm As New frmReadOnlyListAdapter
            frm.ShowDialog()
        End Sub

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
        End Sub

        Private Sub lstRW_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstRW.KeyDown
            With CType(sender, ListBox)
                If .SelectedItem IsNot Nothing Then
                    RWList.Remove(.SelectedItem)
                    ShowRW()
                End If
            End With
        End Sub

        
        Private Sub tsbAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbAdd.Click
            If tstAdd.Text = "" Then
                MsgBox("Type something!")
                Exit Sub
            End If
            RWList.Add(New clsRW(tstAdd.Text))
            ShowRW()
        End Sub

        Private Class clsRO
            Inherits clsRW
            Public Sub New(ByVal I As String)
                MyBase.New(I)

            End Sub
            Public Overrides Function ToString() As String
                Return "RO " & I
            End Function
        End Class

        Private Class clsRW
            Protected I As String
            Public Sub New(ByVal I As String)
                Me.I = I
            End Sub
            Public Overrides Function ToString() As String
                Return "RW " & I
            End Function
        End Class
        Private Sub ShowRW()
            lstRW.Items.Clear()
            For Each itm As clsRW In RWList
                lstRW.Items.Add(itm)
            Next itm
        End Sub
        Private Sub ShowRO()
            lstRO.Items.Clear()
            For Each itm As clsRO In ROList
                lstRO.Items.Add(itm)
            Next itm
        End Sub

        Private Sub tswShowRO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbShowRO.Click
            ROList = New ReadOnlyListAdapter(Of clsRW, clsRO)(RWList)
            ShowRO()
        End Sub
    End Class

End Namespace