Imports Tools.WindowsT.FormsT
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox

Namespace Data
    ''' <summary>Wizard step for synchronize with database process to select database</summary>
    Friend Class SelectDatabaseStep
        Implements IWizardControl
        ''' <summary>Contains value of the <see cref="ConnectionString"/> property</summary>
        Private _ConnectionString As New SqlClient.SqlConnectionStringBuilder(My.Settings.MetanolConnectionString)
        ''' <summary>Gets connection sring slected by user in this step</summary>
        Public ReadOnly Property ConnectionString() As SqlClient.SqlConnectionStringBuilder
            Get
                Return _ConnectionString
            End Get
        End Property
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            ApplyConnectionString()
        End Sub
        ''' <summary>Shows <see cref="ConnectionString"/> in controls</summary>
        Private Sub ApplyConnectionString()
            txtServer.Text = ConnectionString.DataSource
            If ConnectionString.UserInstance Then
                optDBFile.Checked = True
                txtPath.Text = ConnectionString.AttachDBFilename
            Else
                txtDBName.Text = ConnectionString.InitialCatalog
                optDBDatabase.Checked = True
            End If
        End Sub
        ''' <summary>Asks wizard control for control that follows after it.</summary>
        ''' <returns>New instance of <see cref="SelectFoldersStep"/></returns>
        Public Function GetNext() As System.Windows.Forms.Control Implements WindowsT.FormsT.IWizardControl.GetNext
            Return New SelectFoldersStep(Me)
        End Function
        ''' <summary>COntains value of the <see cref="Wizard"/> property</summary>
        Private WithEvents _Wizard As Wizard
        ''' <summary>This property is being set by <see cref="Wizard"/> when control is added to <see cref="Wizard"/>.</summary>
        ''' <remarks>In setter of this property the wizard control should subscribe to wizard events.</remarks>
        Public Property Wizard() As WindowsT.FormsT.Wizard Implements WindowsT.FormsT.IWizardControl.Wizard
            Get
                Return _Wizard
            End Get
            Set(ByVal value As WindowsT.FormsT.Wizard)
                _Wizard = value
            End Set
        End Property

        Private Sub cmdPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPath.Click
            If optDBFile.Checked Then
                If ofdOpen.ShowDialog = DialogResult.OK Then
                    txtPath.Text = ofdOpen.FileName
                    txtPath.Focus()
                End If
            ElseIf optDBNewFile.Checked Then
                If sfdNewFile.ShowDialog = DialogResult.OK Then
                    Try
                        If IO.File.Exists(sfdNewFile.FileName) Then IO.File.Delete(sfdNewFile.FileName)
                        CreateNewDatabaseFile(sfdNewFile.FileName)
                        txtPath.Text = sfdNewFile.FileName
                        optDBFile.Checked = True
                        MsgBox(My.Resources.FileSuccessfullyCreated, MsgBoxStyle.Information, My.Resources.CreateNewdatabaseFile)
                    Catch ex As Exception
                        iMsg.Error_X(ex)
                    End Try
                    txtPath.Focus()
                End If
            End If
        End Sub

        Private Sub optDB_CheckedChanged(ByVal sender As RadioButton, ByVal e As System.EventArgs) Handles optDBFile.CheckedChanged, optDBDatabase.CheckedChanged, optDBNewFile.CheckedChanged
            lblPath.Enabled = optDBFile.Checked OrElse optDBNewFile.Checked
            txtPath.Enabled = optDBFile.Checked OrElse optDBNewFile.Checked
            cmdPath.Enabled = optDBFile.Checked OrElse optDBNewFile.Checked
            lblDBName.Enabled = optDBDatabase.Checked
            txtDBName.Enabled = optDBDatabase.Checked
            If sender Is optDBNewFile AndAlso sender.Checked Then
                cmdPath.PerformClick()
            ElseIf sender Is optDBDatabase AndAlso sender.Checked Then
                ConnectionString.UserInstance = False
                ConnectionString.AttachDBFilename = ""
                ConnectionString.InitialCatalog = txtDBName.Text
            ElseIf sender Is optDBFile AndAlso sender.Checked Then
                ConnectionString.UserInstance = True
                ConnectionString.InitialCatalog = ""
                ConnectionString.AttachDBFilename = txtPath.Text
            End If
        End Sub

        Private Sub txtPath_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPath.Validating
            ConnectionString.AttachDBFilename = txtPath.Text
        End Sub

        Private Sub txtDBName_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtDBName.Validating
            ConnectionString.InitialCatalog = txtDBName.Text
        End Sub

        Private Sub txtServer_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtServer.Validating
            ConnectionString.DataSource = txtServer.Text
        End Sub

        Private Sub cmdDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDetails.Click
            Dim ed As New ConnectionStringEditor(ConnectionString)
            ed.ShowDialog(Me)
            ApplyConnectionString()
        End Sub

        Private Sub cmdTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTest.Click
            Try
                TryConnectDoDatabase()
            Catch ex As Exception
                iMsg.Error_X(ex)
                Exit Sub
            End Try
            iMsg.Modal_PTI(My.Resources.TestSucceeded, My.Resources.TestConnection, WindowsT.IndependentT.MessageBox.MessageBoxIcons.OK)
        End Sub
        ''' <summary>Tries to connect do database using <see cref="ConnectionString"/></summary>
        ''' <exception cref="System.InvalidOperationException">Cannot open a connection without specifying a data source or server.  or The connection is already open.</exception>
        ''' <exception cref="System.Data.SqlClient.SqlException">A connection-level error occurred while opening the connection. If the <see cref="System.Data.SqlClient.SqlException.Number" /> property contains the value 18487 or 18488, this indicates that the specified password has expired or must be reset. See the <see cref="M:System.Data.SqlClient.SqlConnection.ChangePassword(System.String,System.String)" /> method for more information.</exception>
        Private Sub TryConnectDoDatabase()
            Dim con As New SqlClient.SqlConnection(Me.ConnectionString.ConnectionString)
            con.Open()
        End Sub

        Private Sub Wizard_NextStep(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles _Wizard.NextStep
            Try
                TryConnectDoDatabase()
            Catch ex As Exception
                iMsg.Error_XT(ex, My.Resources.TestConnectionFailed)
                e.Cancel = True
            End Try
        End Sub
    End Class
End Namespace