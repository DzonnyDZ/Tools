Imports Tools.CollectionsT.GenericT
Namespace CollectionsT.GenericT
    '#If Config <= Nightly This conditional compilation is done in Tests.vbproj
    ''' <summary>Test Form for testing <see cref="HashTable(Of String)"/></summary>
    Friend Class frmHashTable
        ''' <summary>Test instance of <see cref="HashTable(Of String)"/></summary>
        Private Table As New HashTable(Of String)(New eqc)
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Simple case-insencitive string comparer</summary>
        Private Class eqc : Implements IEqualityComparer(Of String)
            ''' <summary>Determines whether the specified objects are equal.</summary>
            ''' <param name="y">The second <see cref="String"/> to compare.</param>
            ''' <param name="x">The first <see cref="String"/> to compare.</param>
            ''' <returns>true if the specified objects are equal; otherwise, false.</returns>
            Public Overloads Function Equals(ByVal x As String, ByVal y As String) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of String).Equals
                Return x.ToLower = y.ToLower
            End Function
            ''' <summary>Returns a hash code for the specified object.</summary>
            ''' <param name="obj">The System.Object for which a hash code is to be returned.</param>
            ''' <returns>A hash code for the specified object.</returns>
            ''' <exception cref="System.ArgumentNullException">The type of obj is a reference type and obj is null.</exception>
            Public Overloads Function GetHashCode(ByVal obj As String) As Integer Implements System.Collections.Generic.IEqualityComparer(Of String).GetHashCode
                Return obj.ToLower.GetHashCode
            End Function
        End Class

        Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
            Try
                Table.Add(txtAdd.Text)
            Catch ex As Exception
                MsgBox(ex.Message, , ex.GetType.FullName)
            End Try
            ShowTable()
        End Sub

        Private Sub cmdDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDel.Click
            Try
                Table.Remove(lstItems.SelectedItem)
            Catch ex As Exception
                MsgBox(ex.Message, , ex.GetType.FullName)
            End Try
            ShowTable()
        End Sub

        Private Sub txtAdd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAdd.KeyDown
            If e.KeyCode = Keys.Return Then cmdAdd.PerformClick()
        End Sub
        ''' <summary>Displays <see cref="Table"/> in <see cref="lstItems"/></summary>
        Private Sub ShowTable()
            lstItems.Items.Clear()
            For Each itm As String In Table
                lstItems.Items.Add(itm)
            Next itm
        End Sub
        ''' <summary>Displays test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmHashTable
            frm.ShowDialog()
        End Sub
    End Class
End Namespace