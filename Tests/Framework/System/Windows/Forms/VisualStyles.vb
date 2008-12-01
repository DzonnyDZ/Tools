Imports Tools.CollectionsT.SpecializedT, System.Linq
Namespace Framework.SystemF.WindowsF.FormsF.VisualStylesF
    Public Class frmVisualStylesTest
        Public Sub New()
            InitializeComponent()
            imlImages.Images.Import(frmTests.imlImages.Images)
            Me.Icon = Tools.ResourcesT.ToolsIcon
            LoadType(tvwList.Nodes, GetType(VisualStyles.VisualStyleElement))
        End Sub
        Private Sub LoadType(ByVal Parent As TreeNodeCollection, ByVal t As Type)
            Dim node = Parent.Add(t.Name)
            node.Tag = t
            node.ImageKey = "Class"
            node.SelectedImageKey = node.ImageKey
            For Each t2 As Type In t.GetNestedTypes
                LoadType(node.Nodes, t2)
            Next
            For Each prp In t.GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
                Dim prpm = node.Nodes.Add(prp.Name)
                prpm.Tag = prp
                prpm.ImageKey = "Properties"
                prpm.SelectedImageKey = prpm.ImageKey
            Next
        End Sub
        Public Shared Sub Test()
            Dim frm As New frmVisualStylesTest
            frm.ShowDialog()
        End Sub

        Private Sub tvwList_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwList.AfterSelect
            If TypeOf e.Node.Tag Is Reflection.PropertyInfo Then
                Dim prp As Reflection.PropertyInfo = e.Node.Tag
                Dim value = prp.GetValue(Nothing, Nothing)
                If TypeOf value Is VisualStyles.VisualStyleElement Then
                    Try
                        Dim el As VisualStyles.VisualStyleElement = value
                        lblClassName.Text = el.ClassName
                        lblPart.Text = el.Part
                        lblState.Text = el.State
                        Renderer = New VisualStyles.VisualStyleRenderer(el)
                        flpDrwBg.Invalidate(True)
                        txtError.Visible = False
                        flpDrwBg.Invalidate(True)
                        panDrawEx.Invalidate()
                    Catch ex As Exception
                        txtError.BringToFront()
                        txtError.Visible = True
                        txtError.Text = ex.GetType.Name & vbCrLf & ex.Message
                    End Try
                End If
            End If
        End Sub
        Private Renderer As VisualStyles.VisualStyleRenderer

        Private Sub panBg_Paint(ByVal sender As Control, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panBg64.Paint, panBg32.Paint, panBg256.Paint, panBg16.Paint
            If Renderer IsNot Nothing Then
                Renderer.DrawBackground(e.Graphics, sender.ClientRectangle, e.ClipRectangle)
            End If
        End Sub
        Private Sub panDrawEx_Paint(ByVal sender As Panel, ByVal e As System.Windows.Forms.PaintEventArgs) Handles panDrawEx.Paint
            If Renderer IsNot Nothing Then
                Renderer.DrawBackground(e.Graphics, sender.ClientRectangle, e.ClipRectangle)
                Dim ContentArea = Renderer.GetBackgroundContentRectangle(e.Graphics, sender.ClientRectangle)
                'e.Graphics.FillRectangle(Brushes.White, ContentArea)
                e.Graphics.DrawRectangle(Pens.Black, ContentArea)

            End If
        End Sub

        Private Sub cmdPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPause.Click
            tmrPlay.Enabled = False
        End Sub

        Private Sub cmdPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlay.Click
            tmrPlay_Tick()
            tmrPlay.Enabled = True
        End Sub

        Private Sub tmrPlay_Tick() Handles tmrPlay.Tick
Again:      Dim nextNode = tvwList.SelectedNode
            Do
                If nextNode.Nodes.Count <> 0 Then
                    nextNode = nextNode.Nodes(0)
                ElseIf nextNode.NextNode IsNot Nothing Then
                    nextNode = nextNode.NextNode
                Else
                    Dim Parent = nextNode
                    Do
                        Parent = Parent.Parent
                        If Parent Is Nothing Then nextNode = nextNode.TreeView.Nodes(0) : GoTo Again
                    Loop Until Parent.NextNode IsNot Nothing
                    nextNode = Parent.NextNode
                End If
            Loop Until TypeOf nextNode.Tag Is Reflection.PropertyInfo
            tvwList.SelectedNode = nextNode
            nextNode.EnsureVisible()
        End Sub

        Private Sub nudSpeed_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSpeed.ValueChanged
            tmrPlay.Interval = nudSpeed.Value * 1000
        End Sub

       
    End Class
End Namespace