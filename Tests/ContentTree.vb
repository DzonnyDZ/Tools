Imports Tools, Tools.ReflectionT, System.Linq
''' <summary>Browses visual tree of <see cref="Control"/></summary>
Public Class ContentTree : Inherits ContentTreeBase
    ''' <summary>Contains value of the <see cref="Root"/> property</summary>
    Private _Root As Control
    ''' <summary>Root <see cref="Control"/> - the <see cref="Control"/> to display tree for</summary>
    Public Property Root() As Control
        <DebuggerStepThrough()> Get
            Return _Root
        End Get
        Set(ByVal value As Control)
            If Root IsNot Nothing Then AttachHandlers(Root, False, True)
            tvwTree.Nodes.Clear()
            _Root = value
            If Root Is Nothing Then Exit Property
            Dim Node = CreateNode(Root)
            tvwTree.Nodes.Add(Node)
            ShowSubTree(Node, Root)
            AttachHandlers(Root, , True)
        End Set
    End Property
    ''' <summary>Creates <see cref="TreeNode"/> that represents given <see cref="Control"/></summary>
    ''' <param name="ctl"><see cref="Control"/> to create <see cref="TreeNode"/> for</param>
    ''' <returns><see cref="TreeNode"/> that represents <paramref name="ctl"/></returns>
    Private Function CreateNode(ByVal ctl As Control) As TreeNode
        Dim node As New TreeNode
        node.Text = String.Format("{0} {1}", ctl.Name, ctl.GetType.Name)
        node.Tag = ctl
        Dim Key = ctl.GetType.AssemblyQualifiedName
        If Not imlImages.Images.ContainsKey(Key) Then
            Dim bmp = ctl.GetType.GetToolBoxBitmap(, True)
            If bmp IsNot Nothing Then imlImages.Images.Add(Key, bmp)
        End If
        node.SelectedImageKey = Key
        node.ImageKey = Key
        Return node
    End Function
    ''' <summary>Creates visual representation of sub-tree for given <see cref="Control"/></summary>
    ''' <param name="Node"><see cref="TreeNode"/> to add newl ycreated <see cref="TreeNode">nodes</see> to</param>
    ''' <param name="Root"><see cref="Control"/> to create sub-tree for</param>
    Private Sub ShowSubTree(ByVal Node As TreeNode, ByVal Root As Control)
        Node.Nodes.Clear()
        For Each ctl As Control In Root.Controls
            Dim subnode = CreateNode(ctl)
            Node.Nodes.Add(subnode)
            ShowSubTree(subnode, ctl)
        Next
    End Sub
    ''' <summary>Attaches/detaches handlers of controls</summary>
    ''' <param name="control">Root control for attaching/detaching handlers</param>
    ''' <param name="Attach">True to attach handlers, false to detach handlers</param>
    ''' <param name="Recursive">True to attach handlers for <paramref name="control"/> as well as all its child controls</param>
    Private Sub AttachHandlers(ByVal control As Control, Optional ByVal Attach As Boolean = True, Optional ByVal Recursive As Boolean = False)
        If Attach Then
            AddHandler control.ControlAdded, AddressOf__control_ControlAdded
            AddHandler control.ControlRemoved, AddressOf__control_ControlRemoved
        Else
            RemoveHandler control.ControlAdded, AddressOf__control_ControlAdded
            RemoveHandler control.ControlRemoved, AddressOf__control_ControlRemoved
        End If
        If Recursive Then
            For Each ctl As Control In control.Controls
                AttachHandlers(ctl, Attach, Recursive)
            Next
        End If
    End Sub
    ''' <summary>Contains delegate of the <see cref="Control_ContolAdded"/> method</summary>
    Private AddressOf__control_ControlAdded As ControlEventHandler = AddressOf Control_ContolAdded
    ''' <summary>Contains delegate of the <see cref="Control_ControlRemoved"/> method</summary>
    Private AddressOf__control_ControlRemoved As ControlEventHandler = AddressOf Control_ControlRemoved
    ''' <summary>Handles <see cref="Control.ControlAdded"/> events for controls in tree</summary>
    ''' <param name="sender">Parent control</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Control_ContolAdded(ByVal sender As Control, ByVal e As ControlEventArgs)
        Dim node = FindNode(sender)
        Dim newnode = CreateNode(e.Control)
        node.Nodes.Add(newnode)
        ShowSubTree(newnode, e.Control)
        AttachHandlers(e.Control, , True)
    End Sub
    ''' <summary>Handles <see cref="Control.ControlRemoved"/> events for controls in tree</summary>
    ''' <param name="sender">Parent control</param>
    ''' <param name="e">Event arguments</param>
    Private Sub Control_ControlRemoved(ByVal sender As Control, ByVal e As ControlEventArgs)
        Dim node = FindNode(e.Control)
        node.Parent.Nodes.Remove(node)
        AttachHandlers(e.Control, False, True)
    End Sub
    ''' <summary>Finds node in <see cref="tvwTree"/> by <see cref="TreeNode.Tag"/></summary>
    ''' <param name="tag">Value of the <see cref="TreeNode.Tag"/> property to find node by</param>
    ''' <returns><see cref="TreeNode"/> which's <see cref="TreeNode.Tag">Tag</see> is same instance as <paramref name="tag"/>. Null if such node is not found in <see cref="tvwTree"/></returns>
    Private Function FindNode(ByVal tag As Control) As TreeNode
        Return FindNode(tag, tvwTree.Nodes)
    End Function
    ''' <summary>Recursively searches for <see cref="TreeNode"/> which's <see cref="TreeNode.Tag">Tag</see> is given <see cref="Control"/> in given <see cref="TreeNodeCollection"/></summary>
    ''' <param name="tag"><see cref="Control"/> to search in <see cref="TreeNode.Tag"/> for</param>
    ''' <param name="Nodes"><see cref="TreeNodeCollection"/> to search for <see cref="TreeNode"/> with <see cref="TreeNode.Tag">Tag</see> <paramref name="tag"/> within</param>
    ''' <returns><see cref="TreeNode"/> with same instance as <paramref name="tag"/> in its <see cref="TreeNode.Tag">Tag</see> property. Null if such node is not found.</returns>
    Private Function FindNode(ByVal tag As Control, ByVal Nodes As TreeNodeCollection) As TreeNode
        For Each Node As TreeNode In Nodes
            If Node.Tag Is tag Then Return Node
            Dim ret As TreeNode = FindNode(tag, Node.Nodes)
            If ret IsNot Nothing Then Return ret
        Next
        Return Nothing
    End Function

    Protected Overrides Sub tvwTree_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) 'Handles tvwTree.AfterSelect
        If tvwTree.SelectedNode Is Nothing Then
            prgProperty.SelectedObject = Nothing
            Exit Sub
        End If
        prgProperty.SelectedObject = tvwTree.SelectedNode.Tag
    End Sub
End Class