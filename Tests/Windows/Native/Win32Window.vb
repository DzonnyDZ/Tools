#If Config <= Nightly Then
Imports Tools.WindowsT.NativeT, System.Linq
Namespace WindowsT.NativeT

    ''' <summary>Test for <see cref="Tools.IOt.FileSystemEnumerator"/></summary>
    Public Class frmWin32Window
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub Test()
            Dim f As New frmWin32Window
            f.ShowDialog()
        End Sub

        Private Sub frmWin32Window_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            cmbObject.Items.Clear()
            Dim top As Integer = Me.Top
            For i = 1 To 2
                Dim frm As New frmTestForm With {.Text = String.Format("TestForm{0}", i), .Name = .Text}
                cmbObject.Items.Add(New Win32Window(frm))
                For Each Control As Control In frm.Controls
                    Control.Text &= i
                    Control.Name = Control.Text
                    cmbObject.Items.Add(New Win32Window(Control))
                Next Control
                frm.Show(Me)
                frm.Left = Me.Right
                frm.Top = top
                top += frm.Height
            Next i
            'cmbObject.DisplayMember = "Text"
            cmbSetParent.Items.Clear()
            cmbSetParent.Items.Add("<nothing>")
            cmbSetParent.Items.AddRange(cmbObject.Items.OfType(Of Object).ToArray)
            cmbObject.SelectedIndex = 0
            cmbSetParent.SelectedIndex = 0
            'cmbSetParent.DisplayMember = "Text"
        End Sub

        Private Sub cmdSetParent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetParent.Click
            Try
                If cmbSetParent.SelectedIndex > 0 Then
                    DirectCast(prgProperties.SelectedObject, Win32Window).Parent = cmbSetParent.SelectedItem
                    SelectedObjectChanged()
                ElseIf cmbSetParent.SelectedIndex = 0 Then
                    DirectCast(prgProperties.SelectedObject, Win32Window).Parent = Nothing
                    SelectedObjectChanged()
                Else
                    MsgBox("You must select parent!")
                End If
            Catch ex As API.Win32APIException
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub

        Private Sub cmbObject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbObject.SelectedIndexChanged
            If cmbObject.SelectedIndex < 0 Then Exit Sub
            prgProperties.SelectedObject = cmbObject.SelectedItem
            SelectedObjectChanged()
        End Sub

        Private Sub SelectedObjectChanged()
            If cmbObject.Items.Contains(prgProperties.SelectedObject) Then
                cmbObject.SelectedItem = prgProperties.SelectedObject
            Else
                cmbObject.SelectedIndex = -1
            End If
            If lstWindowList.Items.Contains(prgProperties.SelectedObject) Then
                lstWindowList.SelectedItem = prgProperties.SelectedObject
            Else
                lstWindowList.SelectedIndex = -1
            End If
            With DirectCast(prgProperties.SelectedObject, Win32Window)
                If .Parent Is Nothing Then
                    cmbSetParent.SelectedIndex = 0
                ElseIf cmbSetParent.Items.Contains(.Parent) Then
                    cmbSetParent.SelectedItem = .Parent
                Else
                    cmbSetParent.SelectedIndex = -1
                End If
            End With
        End Sub


        Private Sub cmdLoadParent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadParent.Click
            With DirectCast(prgProperties.SelectedObject, Win32Window)
                If .Parent Is Nothing Then
                    MsgBox("Current object has no parent")
                Else
                    prgProperties.SelectedObject = .Parent
                    SelectedObjectChanged()
                End If
            End With
        End Sub

        Private Sub cmdLoadDesktop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadDesktop.Click
            prgProperties.SelectedObject = Win32Window.Desktop
            SelectedObjectChanged()
        End Sub


        Private Sub cmdLoadTopLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadTopLevel.Click
            lstWindowList.Items.Clear()
            Try
                lstWindowList.Items.AddRange(Win32Window.TopLevelWindows.OfType(Of Object).ToArray)
                'lstWindowList.DisplayMember = "Text"
            Catch ex As API.Win32APIException
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub

        Private Sub cmdLoadChildren_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadChildren.Click
            lstWindowList.Items.Clear()
            Try
                lstWindowList.Items.AddRange(DirectCast(prgProperties.SelectedObject, Win32Window).Children.OfType(Of Object).ToArray)
                'lstWindowList.DisplayMember = "Text"
            Catch ex As API.Win32APIException
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub

        Private Sub lstWindowList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWindowList.SelectedIndexChanged
            If lstWindowList.SelectedIndex < 0 Then Exit Sub
            prgProperties.SelectedObject = lstWindowList.SelectedItem
            SelectedObjectChanged()
        End Sub
    End Class
End Namespace
#End If