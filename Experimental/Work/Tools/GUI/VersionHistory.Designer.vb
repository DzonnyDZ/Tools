Namespace GUI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class VersionHistory
        Inherits System.Windows.Forms.Form

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
            Me.components = New System.ComponentModel.Container
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.chkOK = New System.Windows.Forms.CheckBox
            Me.cmdOK = New System.Windows.Forms.Button
            Me.webHistory = New System.Windows.Forms.WebBrowser
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.hlpHelp = New System.Windows.Forms.HelpProvider
            Me.tlpMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpMain
            '
            Me.tlpMain.ColumnCount = 1
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.Controls.Add(Me.chkOK, 0, 1)
            Me.tlpMain.Controls.Add(Me.cmdOK, 0, 2)
            Me.tlpMain.Controls.Add(Me.webHistory, 0, 0)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.hlpHelp.SetHelpKeyword(Me.tlpMain, "#")
            Me.hlpHelp.SetHelpNavigator(Me.tlpMain, System.Windows.Forms.HelpNavigator.Topic)
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 3
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.hlpHelp.SetShowHelp(Me.tlpMain, True)
            Me.tlpMain.Size = New System.Drawing.Size(485, 341)
            Me.tlpMain.TabIndex = 1
            '
            'chkOK
            '
            Me.chkOK.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkOK.AutoSize = True
            Me.hlpHelp.SetHelpKeyword(Me.chkOK, "chkOK")
            Me.hlpHelp.SetHelpNavigator(Me.chkOK, System.Windows.Forms.HelpNavigator.Topic)
            Me.chkOK.Location = New System.Drawing.Point(151, 292)
            Me.chkOK.Name = "chkOK"
            Me.hlpHelp.SetShowHelp(Me.chkOK, True)
            Me.chkOK.Size = New System.Drawing.Size(183, 17)
            Me.chkOK.TabIndex = 1
            Me.chkOK.Text = "Tento dialog pøíštì nezobrazovat"
            Me.totToolTip.SetToolTip(Me.chkOK, "Stejnì se zobrazí, až budou další novinky")
            Me.chkOK.UseVisualStyleBackColor = True
            '
            'cmdOK
            '
            Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdOK.AutoSize = True
            Me.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.hlpHelp.SetHelpKeyword(Me.cmdOK, "cmdOK")
            Me.hlpHelp.SetHelpNavigator(Me.cmdOK, System.Windows.Forms.HelpNavigator.Topic)
            Me.cmdOK.Location = New System.Drawing.Point(226, 315)
            Me.cmdOK.Name = "cmdOK"
            Me.hlpHelp.SetShowHelp(Me.cmdOK, True)
            Me.cmdOK.Size = New System.Drawing.Size(32, 23)
            Me.cmdOK.TabIndex = 2
            Me.cmdOK.Text = "&OK"
            Me.cmdOK.UseVisualStyleBackColor = True
            '
            'webHistory
            '
            Me.webHistory.AllowWebBrowserDrop = False
            Me.webHistory.Dock = System.Windows.Forms.DockStyle.Fill
            Me.hlpHelp.SetHelpKeyword(Me.webHistory, "webHistory")
            Me.hlpHelp.SetHelpNavigator(Me.webHistory, System.Windows.Forms.HelpNavigator.Topic)
            Me.webHistory.Location = New System.Drawing.Point(3, 3)
            Me.webHistory.MinimumSize = New System.Drawing.Size(20, 20)
            Me.webHistory.Name = "webHistory"
            Me.hlpHelp.SetShowHelp(Me.webHistory, True)
            Me.webHistory.Size = New System.Drawing.Size(479, 283)
            Me.webHistory.TabIndex = 0
            Me.webHistory.TabStop = False
            Me.webHistory.WebBrowserShortcutsEnabled = False
            '
            'hlpHelp
            '
            Me.hlpHelp.HelpNamespace = "http://mis.eosksicz.net/wiki/index.php/KolUNI:_Historie_verzí"
            '
            'frmVersionHistory
            '
            Me.AcceptButton = Me.cmdOK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.cmdOK
            Me.ClientSize = New System.Drawing.Size(485, 341)
            Me.Controls.Add(Me.tlpMain)
            Me.HelpButton = True
            Me.hlpHelp.SetHelpKeyword(Me, "#")
            Me.hlpHelp.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmVersionHistory"
            Me.hlpHelp.SetShowHelp(Me, True)
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Novinky v aplikaci"
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents chkOK As System.Windows.Forms.CheckBox
        Friend WithEvents cmdOK As System.Windows.Forms.Button
        Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents webHistory As System.Windows.Forms.WebBrowser
        Friend WithEvents hlpHelp As System.Windows.Forms.HelpProvider
    End Class
End Namespace