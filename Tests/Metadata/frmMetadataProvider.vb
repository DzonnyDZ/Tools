Imports Tools.DrawingT.DrawingIOt, Tools.MetadataT
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox
Namespace MetadataT
    ''' <summary>Tests <see cref="Tools.MetadataT.IMetadataProvider"/> implementations</summary>
    Public Class frmMetadataProvider
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub test()
            Dim frm As New frmMetadataProvider
            frm.ShowDialog()
        End Sub

        Private _Provider As IMetadataProvider
        Private _Metadata As IMetadata
        Private Property Provider() As IMetadataProvider
            Get
                Return _Provider
            End Get
            Set(ByVal value As IMetadataProvider)
                If value IsNot Provider Then Metadata = Nothing
                _Provider = value
                fraProvider.Enabled = Provider IsNot Nothing
                If Provider IsNot Nothing Then LoadProvider()
            End Set
        End Property
        Private Sub LoadProvider()
            lblProviderName.Text = Provider.GetType.FullName
            lstSupportedMetadata.Items.Clear()
            lstCurrentMetadata.Items.Clear()
            For Each mName In Provider.GetSupportedMetadataNames
                lstSupportedMetadata.Items.Add(mName)
            Next
            For Each mName In Provider.GetContainedMetadataNames
                lstCurrentMetadata.Items.Add(mName)
            Next
        End Sub

        Private Property Metadata() As IMetadata
            Get
                Return _Metadata
            End Get
            Set(ByVal value As IMetadata)
                _Metadata = value
                fraMatadata.Enabled = Metadata IsNot Nothing
                If Metadata IsNot Nothing Then LoadMetadata()
            End Set
        End Property
        Private Sub LoadMetadata()
            lvwCurrentKeys.Items.Clear()
            lstPredefinedKeys.Items.Clear()
            lstPredefinedNames.Items.Clear()
            lblMetadataProviderName.Text = Metadata.Name
            lblMetadataName.Text = Metadata.GetType.FullName
            For Each pKey In Metadata.GetPredefinedKeys
                lstPredefinedKeys.Items.Add(pKey)
            Next
            For Each pName In Metadata.GetPredefinedNames
                lstPredefinedNames.Items.Add(pName)
            Next
            For Each cKey In Metadata.GetContainedKeys
                Dim item = lvwCurrentKeys.Items.Add(cKey)
                item.SubItems.Add(Metadata.GetNameOfKey(cKey))
                item.SubItems.Add(Metadata.GetHumanName(cKey))
                item.SubItems.Add(Metadata.GetStringValue(cKey))
                Dim Value = Metadata(cKey)
                item.SubItems.Add(If(Value Is Nothing, "", Value.GetType.FullName))
                item.SubItems.Add(Metadata.GetDescription(cKey))
            Next
        End Sub
        Private Sub cmdJpeg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdJpeg.Click
            If ofdJPEG.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Try
                    Provider = New Tools.DrawingT.DrawingIOt.JPEG.JPEGReader(ofdJPEG.FileName)
                Catch ex As Exception
                    iMsg.Error_X(ex)
                End Try
            End If
        End Sub

        Private Sub lstMetadata_DoubleClick(ByVal sender As ListBox, ByVal e As System.EventArgs) Handles lstCurrentMetadata.DoubleClick, lstSupportedMetadata.DoubleClick
            If sender.SelectedItem Is Nothing Then Return
            txtMetadataName.Text = sender.SelectedItem.ToString
            cmdGetMetadata.PerformClick()
        End Sub

        Private Sub cmdCheckMetadata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckMetadata.Click
            Try
                If Provider.Contains(txtMetadataName.Text) Then
                    iMsg.ModalF_PTIa("Metadata {0} are present.", "Check metadata", iMsg.MessageBoxIcons.OK, txtMetadataName.Text)
                Else
                    iMsg.ModalF_PTIa("Metadata {0} are not present.", "Check metadata", iMsg.MessageBoxIcons.Hand, txtMetadataName.Text)
                End If
            Catch ex As Exception
                iMsg.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdGetMetadata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetMetadata.Click
            Try
                Metadata = Provider(txtMetadataName.Text)
            Catch ex As Exception
                iMsg.Error_X(ex)
                Exit Sub
            End Try
            If Metadata Is Nothing Then iMsg.ModalF_PTIa("Metadata {0} are not present.", "Get metadata", Tools.WindowsT.IndependentT.MessageBox.MessageBoxIcons.Warning, txtMetadataName.Text)
        End Sub

        Private Sub lstKeysNames_DoubleClick(ByVal sender As ListBox, ByVal e As System.EventArgs) Handles lstPredefinedKeys.DoubleClick, lstPredefinedNames.DoubleClick
            If sender.SelectedItem Is Nothing Then Return
            txtKeyName.Text = sender.SelectedItem.ToString
            cmdGetValue.PerformClick()
        End Sub

        Private Sub lvwCurrentKeys_ItemActivate(ByVal sender As ListView, ByVal e As System.EventArgs) Handles lvwCurrentKeys.ItemActivate
            txtKeyName.Text = sender.SelectedItems(0).Text
            cmdGetValue.PerformClick()
        End Sub

        Private Sub cmdCheckKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckKey.Click
            Try
                If Metadata.ContainsKey(txtKeyName.Text) Then
                    iMsg.ModalF_PTIa("Metadata item {0} is present.", "Check key or name", iMsg.MessageBoxIcons.OK, txtMetadataName.Text)
                Else
                    iMsg.ModalF_PTIa("Metadata item {0} is not present.", "Check key or name", iMsg.MessageBoxIcons.Hand, txtMetadataName.Text)
                End If
            Catch ex As Exception
                iMsg.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdGetValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetValue.Click
            Try
                If Metadata(txtKeyName.Text) Is Nothing Then
                    iMsg.ModalF_PTIa("Item {0} is not set.", "Get value", iMsg.MessageBoxIcons.Warning, txtKeyName.Text)
                    Exit Sub
                End If
                iMsg.ModalF_PTIa("Value of {0} is ""{1}"".", "Get value", iMsg.MessageBoxIcons.Information, txtKeyName.Text, Metadata(txtKeyName.Text))
            Catch ex As Exception
                iMsg.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdDisplayName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDisplayName.Click
            Try
                Dim Name As String = Metadata.GetHumanName(txtKeyName.Text)
                If Name Is Nothing Then
                    iMsg.ModalF_PTIa("Display name of {0} not found.", "Display name", iMsg.MessageBoxIcons.Warning, txtKeyName.Text)
                    Exit Sub
                End If
                iMsg.ModalF_PTIa("Display name of {0} is {1}.", "Display name", iMsg.MessageBoxIcons.Information, txtKeyName.Text, Name)
            Catch ex As Exception
                iMsg.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdDescription_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDescription.Click
            Try
                Dim Description As String = Metadata.GetDescription(txtKeyName.Text)
                If Description Is Nothing Then
                    iMsg.ModalF_PTIa("Description of {0} not found.", "Description", iMsg.MessageBoxIcons.Warning, txtKeyName.Text)
                    Exit Sub
                End If
                iMsg.ModalF_PTIa("Description of {0} is {1}.", "Description", iMsg.MessageBoxIcons.Information, txtKeyName.Text, Description)
            Catch ex As Exception
                iMsg.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdNameToKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNameToKey.Click
            Try
                Dim Key As String = Metadata.GetKeyOfName(txtKeyName.Text)
                If Key Is Nothing Then
                    iMsg.ModalF_PTIa("Key of name {0} not found.", "Key of name", iMsg.MessageBoxIcons.Warning, txtKeyName.Text)
                    Exit Sub
                End If
                iMsg.ModalF_PTIa("Key of {0} is {1}.", "Key of name", iMsg.MessageBoxIcons.Information, txtKeyName.Text, Key)
            Catch ex As Exception
                iMsg.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdKeyToName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKeyToName.Click
            Try
                Dim Name As String = Metadata.GetNameOfKey(txtKeyName.Text)
                If Name Is Nothing Then
                    iMsg.ModalF_PTIa("Name of key {0} not found.", "Name of key", iMsg.MessageBoxIcons.Warning, txtKeyName.Text)
                    Exit Sub
                End If
                iMsg.ModalF_PTIa("Name of {0} is {1}.", "Name of key", iMsg.MessageBoxIcons.Information, txtKeyName.Text, Name)
            Catch ex As Exception
                iMsg.Error_X(ex)
            End Try
        End Sub
    End Class
End Namespace