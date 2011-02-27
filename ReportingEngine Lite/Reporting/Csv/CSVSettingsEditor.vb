Namespace ReportingT.ReportingEngineLite
    ''' <summary>Editor of CSV report properties</summary>
    Friend Class CsvSettingsEditor
        Inherits WindowsT.FormsT.PropertyGridEditor(Of CsvTemplateSettings)
        ''' <summary>CTor - initializes a new instance of the <see cref="CsvSettingsEditor"/> class</summary>
        Public Sub New()
            init()
        End Sub
        ''' <summary>CTor - initializes a new instance of the <see cref="CsvSettingsEditor"/> class with given settings</summary>
        ''' <param name="Settings">A settings to edit</param>
        Public Sub New(ByVal Settings As CsvTemplateSettings)
            MyBase.New(Settings)
            init()
        End Sub

        Private WithEvents tsbXls As New ToolStripButton

        ''' <summary>Initializes newly created instance of <see cref="CsvSettingsEditor"/></summary>
        Private Sub init()
            tsbXls.Text = My.Resources.txt_SetExcelFriendly
            tsbXls.Image = My.Resources.Excel.ToBitmap
            tsbXls.DisplayStyle = ToolStripItemDisplayStyle.Image
            tsbXls.AutoToolTip = True

            For Each ctl As Control In Me.Controls
                If TypeOf ctl Is ToolStrip Then
                    Dim tos As ToolStrip = ctl
                    tos.Items.Add(New ToolStripSeparator)
                    tos.Items.Add(tsbXls)
                    Exit For
                End If
            Next
        End Sub

        Private Sub tsbXls_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbXls.Click
            With Me.SelectedObject
                .MakeExcelFriendly()
            End With
            Me.Refresh()
        End Sub
    End Class
End Namespace