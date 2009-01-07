Namespace Data
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SynchronizeStep
        Inherits System.Windows.Forms.UserControl

        'Form overrides dispose to clean up the component list.
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SynchronizeStep))
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.lblName = New System.Windows.Forms.Label
            Me.lblI = New System.Windows.Forms.Label
            Me.flpOnConfilct = New System.Windows.Forms.FlowLayoutPanel
            Me.optAskOverwrite = New System.Windows.Forms.RadioButton
            Me.optOverwriteDatabase = New System.Windows.Forms.RadioButton
            Me.optOverwriteFile = New System.Windows.Forms.RadioButton
            Me.cmdStart = New System.Windows.Forms.Button
            Me.prgProgress = New System.Windows.Forms.ProgressBar
            Me.lblFileName = New System.Windows.Forms.Label
            Me.flpRelocate = New System.Windows.Forms.FlowLayoutPanel
            Me.optRelocateAsk = New System.Windows.Forms.RadioButton
            Me.optRelocateIgnore = New System.Windows.Forms.RadioButton
            Me.optRelocate = New System.Windows.Forms.RadioButton
            Me.lblMode = New System.Windows.Forms.Label
            Me.flpMode = New System.Windows.Forms.FlowLayoutPanel
            Me.optModeSynchronizeAdd = New System.Windows.Forms.RadioButton
            Me.optModeSynchronizeExisting = New System.Windows.Forms.RadioButton
            Me.optModeAdd = New System.Windows.Forms.RadioButton
            Me.lblOnConflict = New System.Windows.Forms.Label
            Me.lblOnDuplicitName = New System.Windows.Forms.Label
            Me.bgwSynchronize = New System.ComponentModel.BackgroundWorker
            Me.tlpMain.SuspendLayout()
            Me.flpOnConfilct.SuspendLayout()
            Me.flpRelocate.SuspendLayout()
            Me.flpMode.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpMain
            '
            resources.ApplyResources(Me.tlpMain, "tlpMain")
            Me.tlpMain.Controls.Add(Me.cmdCancel, 0, 8)
            Me.tlpMain.Controls.Add(Me.lblName, 0, 0)
            Me.tlpMain.Controls.Add(Me.lblI, 0, 1)
            Me.tlpMain.Controls.Add(Me.flpOnConfilct, 1, 3)
            Me.tlpMain.Controls.Add(Me.cmdStart, 0, 5)
            Me.tlpMain.Controls.Add(Me.prgProgress, 0, 6)
            Me.tlpMain.Controls.Add(Me.lblFileName, 0, 7)
            Me.tlpMain.Controls.Add(Me.flpRelocate, 1, 4)
            Me.tlpMain.Controls.Add(Me.lblMode, 0, 2)
            Me.tlpMain.Controls.Add(Me.flpMode, 1, 2)
            Me.tlpMain.Controls.Add(Me.lblOnConflict, 0, 3)
            Me.tlpMain.Controls.Add(Me.lblOnDuplicitName, 0, 4)
            Me.tlpMain.Name = "tlpMain"
            '
            'cmdCancel
            '
            resources.ApplyResources(Me.cmdCancel, "cmdCancel")
            Me.tlpMain.SetColumnSpan(Me.cmdCancel, 2)
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'lblName
            '
            resources.ApplyResources(Me.lblName, "lblName")
            Me.tlpMain.SetColumnSpan(Me.lblName, 2)
            Me.lblName.Name = "lblName"
            '
            'lblI
            '
            resources.ApplyResources(Me.lblI, "lblI")
            Me.tlpMain.SetColumnSpan(Me.lblI, 2)
            Me.lblI.Name = "lblI"
            '
            'flpOnConfilct
            '
            resources.ApplyResources(Me.flpOnConfilct, "flpOnConfilct")
            Me.flpOnConfilct.Controls.Add(Me.optAskOverwrite)
            Me.flpOnConfilct.Controls.Add(Me.optOverwriteDatabase)
            Me.flpOnConfilct.Controls.Add(Me.optOverwriteFile)
            Me.flpOnConfilct.Name = "flpOnConfilct"
            '
            'optAskOverwrite
            '
            resources.ApplyResources(Me.optAskOverwrite, "optAskOverwrite")
            Me.optAskOverwrite.Checked = True
            Me.optAskOverwrite.Name = "optAskOverwrite"
            Me.optAskOverwrite.TabStop = True
            Me.optAskOverwrite.UseVisualStyleBackColor = True
            '
            'optOverwriteDatabase
            '
            resources.ApplyResources(Me.optOverwriteDatabase, "optOverwriteDatabase")
            Me.optOverwriteDatabase.Name = "optOverwriteDatabase"
            Me.optOverwriteDatabase.UseVisualStyleBackColor = True
            '
            'optOverwriteFile
            '
            resources.ApplyResources(Me.optOverwriteFile, "optOverwriteFile")
            Me.optOverwriteFile.Name = "optOverwriteFile"
            Me.optOverwriteFile.UseVisualStyleBackColor = True
            '
            'cmdStart
            '
            resources.ApplyResources(Me.cmdStart, "cmdStart")
            Me.tlpMain.SetColumnSpan(Me.cmdStart, 2)
            Me.cmdStart.Name = "cmdStart"
            Me.cmdStart.UseVisualStyleBackColor = True
            '
            'prgProgress
            '
            resources.ApplyResources(Me.prgProgress, "prgProgress")
            Me.tlpMain.SetColumnSpan(Me.prgProgress, 2)
            Me.prgProgress.Name = "prgProgress"
            '
            'lblFileName
            '
            resources.ApplyResources(Me.lblFileName, "lblFileName")
            Me.tlpMain.SetColumnSpan(Me.lblFileName, 2)
            Me.lblFileName.Name = "lblFileName"
            '
            'flpRelocate
            '
            resources.ApplyResources(Me.flpRelocate, "flpRelocate")
            Me.flpRelocate.Controls.Add(Me.optRelocateAsk)
            Me.flpRelocate.Controls.Add(Me.optRelocateIgnore)
            Me.flpRelocate.Controls.Add(Me.optRelocate)
            Me.flpRelocate.Name = "flpRelocate"
            '
            'optRelocateAsk
            '
            resources.ApplyResources(Me.optRelocateAsk, "optRelocateAsk")
            Me.optRelocateAsk.Checked = True
            Me.optRelocateAsk.Name = "optRelocateAsk"
            Me.optRelocateAsk.TabStop = True
            Me.optRelocateAsk.UseVisualStyleBackColor = True
            '
            'optRelocateIgnore
            '
            resources.ApplyResources(Me.optRelocateIgnore, "optRelocateIgnore")
            Me.optRelocateIgnore.Name = "optRelocateIgnore"
            Me.optRelocateIgnore.UseVisualStyleBackColor = True
            '
            'optRelocate
            '
            resources.ApplyResources(Me.optRelocate, "optRelocate")
            Me.optRelocate.Name = "optRelocate"
            Me.optRelocate.UseVisualStyleBackColor = True
            '
            'lblMode
            '
            resources.ApplyResources(Me.lblMode, "lblMode")
            Me.lblMode.Name = "lblMode"
            '
            'flpMode
            '
            resources.ApplyResources(Me.flpMode, "flpMode")
            Me.flpMode.Controls.Add(Me.optModeSynchronizeAdd)
            Me.flpMode.Controls.Add(Me.optModeSynchronizeExisting)
            Me.flpMode.Controls.Add(Me.optModeAdd)
            Me.flpMode.Name = "flpMode"
            '
            'optModeSynchronizeAdd
            '
            resources.ApplyResources(Me.optModeSynchronizeAdd, "optModeSynchronizeAdd")
            Me.optModeSynchronizeAdd.Name = "optModeSynchronizeAdd"
            Me.optModeSynchronizeAdd.TabStop = True
            Me.optModeSynchronizeAdd.UseVisualStyleBackColor = True
            '
            'optModeSynchronizeExisting
            '
            resources.ApplyResources(Me.optModeSynchronizeExisting, "optModeSynchronizeExisting")
            Me.optModeSynchronizeExisting.Name = "optModeSynchronizeExisting"
            Me.optModeSynchronizeExisting.TabStop = True
            Me.optModeSynchronizeExisting.UseVisualStyleBackColor = True
            '
            'optModeAdd
            '
            resources.ApplyResources(Me.optModeAdd, "optModeAdd")
            Me.optModeAdd.Name = "optModeAdd"
            Me.optModeAdd.TabStop = True
            Me.optModeAdd.UseVisualStyleBackColor = True
            '
            'lblOnConflict
            '
            resources.ApplyResources(Me.lblOnConflict, "lblOnConflict")
            Me.lblOnConflict.Name = "lblOnConflict"
            '
            'lblOnDuplicitName
            '
            resources.ApplyResources(Me.lblOnDuplicitName, "lblOnDuplicitName")
            Me.lblOnDuplicitName.Name = "lblOnDuplicitName"
            '
            'bgwSynchronize
            '
            Me.bgwSynchronize.WorkerReportsProgress = True
            Me.bgwSynchronize.WorkerSupportsCancellation = True
            '
            'SynchronizeStep
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.tlpMain)
            Me.Name = "SynchronizeStep"
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.flpOnConfilct.ResumeLayout(False)
            Me.flpOnConfilct.PerformLayout()
            Me.flpRelocate.ResumeLayout(False)
            Me.flpRelocate.PerformLayout()
            Me.flpMode.ResumeLayout(False)
            Me.flpMode.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblName As System.Windows.Forms.Label
        Friend WithEvents lblI As System.Windows.Forms.Label
        Friend WithEvents flpOnConfilct As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents optAskOverwrite As System.Windows.Forms.RadioButton
        Friend WithEvents optOverwriteDatabase As System.Windows.Forms.RadioButton
        Friend WithEvents optOverwriteFile As System.Windows.Forms.RadioButton
        Friend WithEvents cmdStart As System.Windows.Forms.Button
        Friend WithEvents cmdCancel As System.Windows.Forms.Button
        Friend WithEvents prgProgress As System.Windows.Forms.ProgressBar
        Friend WithEvents lblFileName As System.Windows.Forms.Label
        Private WithEvents bgwSynchronize As System.ComponentModel.BackgroundWorker
        Friend WithEvents flpRelocate As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents optRelocateAsk As System.Windows.Forms.RadioButton
        Friend WithEvents optRelocateIgnore As System.Windows.Forms.RadioButton
        Friend WithEvents optRelocate As System.Windows.Forms.RadioButton
        Friend WithEvents lblMode As System.Windows.Forms.Label
        Friend WithEvents flpMode As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents optModeSynchronizeAdd As System.Windows.Forms.RadioButton
        Friend WithEvents optModeAdd As System.Windows.Forms.RadioButton
        Friend WithEvents lblOnConflict As System.Windows.Forms.Label
        Friend WithEvents lblOnDuplicitName As System.Windows.Forms.Label
        Friend WithEvents optModeSynchronizeExisting As System.Windows.Forms.RadioButton
    End Class
End Namespace