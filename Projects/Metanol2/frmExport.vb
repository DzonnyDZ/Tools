Imports Tools.MetadataT
Imports mBox = Tools.WindowsT.IndependentT.MessageBox

''' <summary>Configures and launches metadata export</summary>
Friend Class frmExport
    ''' <summary>Metadata to be exported</summary>
    Private files As IEnumerable(Of MetadataItem)
    ''' <summary>CTor - creates a new instance of the <see cref="frmExport"/> class</summary>
    ''' <param name="files">Provides metadata to be exported</param>
    Public Sub New(ByVal files As IEnumerable(Of MetadataItem))
        If files Is Nothing Then Throw New ArgumentNullException("files")
        Me.files = files
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        cmbFormat.Items.Add(New CsvFormatConfigurator)
        cmbFormat.SelectedIndex = 0
        FillTree()
    End Sub


    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim configurator As IExportFormatConfigurator = cmbFormat.SelectedItem
        sfdSave.Filter = configurator.FileFilter
        Dim fileName$
        If sfdSave.ShowDialog = Windows.Forms.DialogResult.OK Then
            fileName = sfdSave.FileName
        Else
            Exit Sub
        End If

        Dim metada As New Dictionary(Of String, List(Of String))
        For Each l1 As TreeNode In tvwFields.Nodes
            Dim list As New List(Of String)
            Dim added As Boolean = False
            For Each l2 As TreeNode In l1.Nodes
                If l2.Checked Then
                    list.Add(l2.Tag)
                    added = True
                End If
            Next
            If added Then metada.Add(l1.Tag, list)
        Next
        Try
            Using f = IO.File.Open(fileName, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
                Try
                    configurator.Save(f, metada, files)
                Catch ex As Exception
                    mBox.Error_XPTIBWO(ex, "Error while exporting metadata:", ex.GetType.Name)
                    Exit Sub
                End Try
            End Using
        Catch ex As Exception
            mBox.Error_X(ex)
            Exit Sub
        End Try
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>Loads metadata fileds to tree view</summary>
    Private Sub FillTree()
        For Each mType In New IMetadata() {New ExifT.Exif, New IptcT.Iptc, New MetadataT.SystemMetadata, New MetadataT.ImageMetadata}
            Dim rNode As New TreeNode(mType.Name)
            rNode.Checked = True
            rNode.Tag = mType.Name
            tvwFields.Nodes.Add(rNode)
            Dim childNodes As New List(Of TreeNode)
            For Each field In mType.GetPredefinedNames
                Dim sNode As New TreeNode(mType.GetHumanName(field))
                sNode.Tag = field
                childNodes.Add(sNode)
            Next
            rNode.Nodes.AddRange((From n In childNodes Order By n.Text).ToArray)
        Next mType
    End Sub

    Private Sub cmbFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFormat.SelectedIndexChanged
        pnlFormatHolder.Controls.Clear()
        pnlFormatHolder.Controls.Add(cmbFormat.SelectedItem)
    End Sub

    Private Sub tvwFields_BeforeCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwFields.BeforeCheck
        If e.Node.Level = 0 Then _
            e.Cancel = True
    End Sub
End Class