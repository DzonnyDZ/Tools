Namespace Data
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SelectDatabaseStep
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectDatabaseStep))
            Me.tlpLayout = New System.Windows.Forms.TableLayoutPanel
            Me.lblI = New System.Windows.Forms.Label
            Me.lblName = New System.Windows.Forms.Label
            Me.lblPath = New System.Windows.Forms.Label
            Me.txtPath = New System.Windows.Forms.TextBox
            Me.cmdPath = New System.Windows.Forms.Button
            Me.lblDBType = New System.Windows.Forms.Label
            Me.flpDBType = New System.Windows.Forms.FlowLayoutPanel
            Me.optDBFile = New System.Windows.Forms.RadioButton
            Me.optDBNewFile = New System.Windows.Forms.RadioButton
            Me.optDBDatabase = New System.Windows.Forms.RadioButton
            Me.lblDBName = New System.Windows.Forms.Label
            Me.txtDBName = New System.Windows.Forms.TextBox
            Me.lblServer = New System.Windows.Forms.Label
            Me.txtServer = New System.Windows.Forms.TextBox
            Me.cmdTest = New System.Windows.Forms.Button
            Me.cmdDetails = New System.Windows.Forms.Button
            Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
            Me.sfdNewFile = New System.Windows.Forms.SaveFileDialog
            Me.tlpLayout.SuspendLayout()
            Me.flpDBType.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpLayout
            '
            Me.tlpLayout.AccessibleDescription = Nothing
            Me.tlpLayout.AccessibleName = Nothing
            resources.ApplyResources(Me.tlpLayout, "tlpLayout")
            Me.tlpLayout.BackgroundImage = Nothing
            Me.tlpLayout.Controls.Add(Me.lblI, 0, 1)
            Me.tlpLayout.Controls.Add(Me.lblName, 0, 0)
            Me.tlpLayout.Controls.Add(Me.lblPath, 0, 3)
            Me.tlpLayout.Controls.Add(Me.txtPath, 1, 3)
            Me.tlpLayout.Controls.Add(Me.cmdPath, 2, 3)
            Me.tlpLayout.Controls.Add(Me.lblDBType, 0, 2)
            Me.tlpLayout.Controls.Add(Me.flpDBType, 1, 2)
            Me.tlpLayout.Controls.Add(Me.lblDBName, 0, 4)
            Me.tlpLayout.Controls.Add(Me.txtDBName, 1, 4)
            Me.tlpLayout.Controls.Add(Me.lblServer, 0, 5)
            Me.tlpLayout.Controls.Add(Me.txtServer, 1, 5)
            Me.tlpLayout.Controls.Add(Me.cmdTest, 0, 7)
            Me.tlpLayout.Controls.Add(Me.cmdDetails, 0, 6)
            Me.tlpLayout.Font = Nothing
            Me.tlpLayout.Name = "tlpLayout"
            '
            'lblI
            '
            Me.lblI.AccessibleDescription = Nothing
            Me.lblI.AccessibleName = Nothing
            resources.ApplyResources(Me.lblI, "lblI")
            Me.tlpLayout.SetColumnSpan(Me.lblI, 2)
            Me.lblI.Font = Nothing
            Me.lblI.Name = "lblI"
            '
            'lblName
            '
            Me.lblName.AccessibleDescription = Nothing
            Me.lblName.AccessibleName = Nothing
            resources.ApplyResources(Me.lblName, "lblName")
            Me.tlpLayout.SetColumnSpan(Me.lblName, 3)
            Me.lblName.Name = "lblName"
            '
            'lblPath
            '
            Me.lblPath.AccessibleDescription = Nothing
            Me.lblPath.AccessibleName = Nothing
            resources.ApplyResources(Me.lblPath, "lblPath")
            Me.lblPath.Font = Nothing
            Me.lblPath.Name = "lblPath"
            '
            'txtPath
            '
            Me.txtPath.AccessibleDescription = Nothing
            Me.txtPath.AccessibleName = Nothing
            resources.ApplyResources(Me.txtPath, "txtPath")
            Me.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
            Me.txtPath.BackgroundImage = Nothing
            Me.txtPath.Font = Nothing
            Me.txtPath.Name = "txtPath"
            '
            'cmdPath
            '
            Me.cmdPath.AccessibleDescription = Nothing
            Me.cmdPath.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdPath, "cmdPath")
            Me.cmdPath.BackgroundImage = Nothing
            Me.cmdPath.Font = Nothing
            Me.cmdPath.Name = "cmdPath"
            Me.cmdPath.UseVisualStyleBackColor = True
            '
            'lblDBType
            '
            Me.lblDBType.AccessibleDescription = Nothing
            Me.lblDBType.AccessibleName = Nothing
            resources.ApplyResources(Me.lblDBType, "lblDBType")
            Me.lblDBType.Font = Nothing
            Me.lblDBType.Name = "lblDBType"
            '
            'flpDBType
            '
            Me.flpDBType.AccessibleDescription = Nothing
            Me.flpDBType.AccessibleName = Nothing
            resources.ApplyResources(Me.flpDBType, "flpDBType")
            Me.flpDBType.BackgroundImage = Nothing
            Me.tlpLayout.SetColumnSpan(Me.flpDBType, 2)
            Me.flpDBType.Controls.Add(Me.optDBFile)
            Me.flpDBType.Controls.Add(Me.optDBNewFile)
            Me.flpDBType.Controls.Add(Me.optDBDatabase)
            Me.flpDBType.Font = Nothing
            Me.flpDBType.Name = "flpDBType"
            '
            'optDBFile
            '
            Me.optDBFile.AccessibleDescription = Nothing
            Me.optDBFile.AccessibleName = Nothing
            resources.ApplyResources(Me.optDBFile, "optDBFile")
            Me.optDBFile.BackgroundImage = Nothing
            Me.optDBFile.Checked = True
            Me.optDBFile.Font = Nothing
            Me.optDBFile.Name = "optDBFile"
            Me.optDBFile.TabStop = True
            Me.optDBFile.UseVisualStyleBackColor = True
            '
            'optDBNewFile
            '
            Me.optDBNewFile.AccessibleDescription = Nothing
            Me.optDBNewFile.AccessibleName = Nothing
            resources.ApplyResources(Me.optDBNewFile, "optDBNewFile")
            Me.optDBNewFile.BackgroundImage = Nothing
            Me.optDBNewFile.Font = Nothing
            Me.optDBNewFile.Name = "optDBNewFile"
            Me.optDBNewFile.TabStop = True
            Me.optDBNewFile.UseVisualStyleBackColor = True
            '
            'optDBDatabase
            '
            Me.optDBDatabase.AccessibleDescription = Nothing
            Me.optDBDatabase.AccessibleName = Nothing
            resources.ApplyResources(Me.optDBDatabase, "optDBDatabase")
            Me.optDBDatabase.BackgroundImage = Nothing
            Me.optDBDatabase.Font = Nothing
            Me.optDBDatabase.Name = "optDBDatabase"
            Me.optDBDatabase.UseVisualStyleBackColor = True
            '
            'lblDBName
            '
            Me.lblDBName.AccessibleDescription = Nothing
            Me.lblDBName.AccessibleName = Nothing
            resources.ApplyResources(Me.lblDBName, "lblDBName")
            Me.lblDBName.Font = Nothing
            Me.lblDBName.Name = "lblDBName"
            '
            'txtDBName
            '
            Me.txtDBName.AccessibleDescription = Nothing
            Me.txtDBName.AccessibleName = Nothing
            resources.ApplyResources(Me.txtDBName, "txtDBName")
            Me.txtDBName.BackgroundImage = Nothing
            Me.tlpLayout.SetColumnSpan(Me.txtDBName, 2)
            Me.txtDBName.Font = Nothing
            Me.txtDBName.Name = "txtDBName"
            '
            'lblServer
            '
            Me.lblServer.AccessibleDescription = Nothing
            Me.lblServer.AccessibleName = Nothing
            resources.ApplyResources(Me.lblServer, "lblServer")
            Me.lblServer.Font = Nothing
            Me.lblServer.Name = "lblServer"
            '
            'txtServer
            '
            Me.txtServer.AccessibleDescription = Nothing
            Me.txtServer.AccessibleName = Nothing
            resources.ApplyResources(Me.txtServer, "txtServer")
            Me.txtServer.BackgroundImage = Nothing
            Me.tlpLayout.SetColumnSpan(Me.txtServer, 2)
            Me.txtServer.Font = Nothing
            Me.txtServer.Name = "txtServer"
            '
            'cmdTest
            '
            Me.cmdTest.AccessibleDescription = Nothing
            Me.cmdTest.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdTest, "cmdTest")
            Me.cmdTest.BackgroundImage = Nothing
            Me.tlpLayout.SetColumnSpan(Me.cmdTest, 3)
            Me.cmdTest.Font = Nothing
            Me.cmdTest.Name = "cmdTest"
            Me.cmdTest.UseVisualStyleBackColor = True
            '
            'cmdDetails
            '
            Me.cmdDetails.AccessibleDescription = Nothing
            Me.cmdDetails.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdDetails, "cmdDetails")
            Me.cmdDetails.BackgroundImage = Nothing
            Me.tlpLayout.SetColumnSpan(Me.cmdDetails, 3)
            Me.cmdDetails.Font = Nothing
            Me.cmdDetails.Name = "cmdDetails"
            Me.cmdDetails.UseVisualStyleBackColor = True
            '
            'ofdOpen
            '
            Me.ofdOpen.DefaultExt = "mdf"
            resources.ApplyResources(Me.ofdOpen, "ofdOpen")
            '
            'sfdNewFile
            '
            Me.sfdNewFile.DefaultExt = "mdf"
            resources.ApplyResources(Me.sfdNewFile, "sfdNewFile")
            '
            'SelectDatabaseStep
            '
            Me.AccessibleDescription = Nothing
            Me.AccessibleName = Nothing
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Nothing
            Me.Controls.Add(Me.tlpLayout)
            Me.Font = Nothing
            Me.Name = "SelectDatabaseStep"
            Me.tlpLayout.ResumeLayout(False)
            Me.tlpLayout.PerformLayout()
            Me.flpDBType.ResumeLayout(False)
            Me.flpDBType.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents tlpLayout As System.Windows.Forms.TableLayoutPanel
        Private WithEvents lblName As System.Windows.Forms.Label
        Private WithEvents lblPath As System.Windows.Forms.Label
        Private WithEvents txtPath As System.Windows.Forms.TextBox
        Private WithEvents cmdPath As System.Windows.Forms.Button
        Private WithEvents lblDBType As System.Windows.Forms.Label
        Private WithEvents flpDBType As System.Windows.Forms.FlowLayoutPanel
        Private WithEvents optDBFile As System.Windows.Forms.RadioButton
        Private WithEvents optDBDatabase As System.Windows.Forms.RadioButton
        Private WithEvents lblDBName As System.Windows.Forms.Label
        Private WithEvents txtDBName As System.Windows.Forms.TextBox
        Private WithEvents lblServer As System.Windows.Forms.Label
        Private WithEvents txtServer As System.Windows.Forms.TextBox
        Private WithEvents cmdTest As System.Windows.Forms.Button
        Private WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
        Private WithEvents lblI As System.Windows.Forms.Label
        Private WithEvents cmdDetails As System.Windows.Forms.Button
        Private WithEvents optDBNewFile As System.Windows.Forms.RadioButton
        Private WithEvents sfdNewFile As System.Windows.Forms.SaveFileDialog

    End Class
End Namespace