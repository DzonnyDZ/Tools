Imports mBox = Tools.WindowsT.IndependentT.MessageBox
Imports Tools.WindowsT.InteropT
Imports System.Globalization.CultureInfo
Imports System.Linq
Imports <xmlns="http://www.unicode.org/ns/2003/ucd/1.0">
Imports <xmlns:nl="http://unicode.org/Public/UNIDATA/NamesList.txt">
Imports Tools.InteropT
Imports Tools.TextT.UnicodeT
Imports System.Xml.Linq
Imports Tools.LinqT

Namespace TextT.UnicodeT

    ''' <summary>This form is used to create a localized version of Unicode Character Database XML</summary>
    Public Class frmUcdTranslate

        Public Shared Sub Go()
            Dim frm As New frmUcdTranslate
            frm.ShowDialog()
        End Sub

        Protected Overrides Sub OnLoad(e As System.EventArgs)
            MyBase.OnLoad(e)

            cmbCharsLanguage.DisplayMember = "Name"
            cmbCharsLanguage.ValueMember = "Path"

            Dim system32 = Environment.GetFolderPath(Environment.SpecialFolder.System)
            If IO.File.Exists(IO.Path.Combine(system32, "getuname.dll")) Then
                cmbCharsLanguage.Items.Add(New With {.Name = "Default", .Path = IO.Path.Combine(system32, "getuname.dll")})
            End If
            For Each fld In IO.Directory.EnumerateDirectories(system32)
                If IO.File.Exists(IO.Path.Combine(fld, "getuname.dll.mui")) Then
                    cmbCharsLanguage.Items.Add(New With {.Name = IO.Path.GetFileName(fld), .Path = IO.Path.Combine(fld, "getuname.dll.mui")})
                End If
            Next

            If cmbCharsLanguage.Items.Count > 0 Then cmbCharsLanguage.SelectedIndex = 0

        End Sub

        Private Sub cmdTargetPathBrowse_Click(sender As System.Object, e As System.EventArgs) Handles cmdTargetPathBrowse.Click
            If sfdSaveXml.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                txtTarget.Text = sfdSaveXml.FileName
            End If
        End Sub

        Private Sub chkChars_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkChars.CheckedChanged
            lblCharsLanguage.Enabled = chkChars.Checked
            cmbCharsLanguage.Enabled = chkChars.Checked
            cmdAddMui.Enabled = chkChars.Checked
        End Sub

        Private Sub cmdGo_Click(sender As System.Object, e As System.EventArgs) Handles cmdGo.Click
            If Not chkBlocks.Checked AndAlso Not chkChars.Checked Then
                mBox.MsgBox("No translate option selected", MsgBoxStyle.Exclamation, , Me)
                Return
            End If

            Dim ret = <?xml version="1.0"?>
                      <ucd nl:language=<%= txtLanguage.Text %>>
                          <repertoire/>
                      </ucd>

            Try
                If chkChars.Checked Then
                    If cmbCharsLanguage.SelectedIndex < 0 Then
                        mBox.MsgBox("Select source language for character names", MsgBoxStyle.Information, , Me)
                        Return
                    End If

                    Dim ucdNames As New Dictionary(Of UInteger, String)
                    If chkComments.Checked Then
                        For Each ch In UnicodeCharacterDatabase.Default.CodePoints
                            If ch.CodePoint.HasValue Then ucdNames.Add(ch.CodePoint, ch.UniversalName)
                        Next
                    End If
                    Dim origNames As New Dictionary(Of UInteger, String)
                    If txtCheck.Text <> "" Then
                        Dim ox = XDocument.Load(txtCheck.Text)
                        For Each cpoint In ox.<ucd>.<repertoire>.<char>
                            If cpoint.@cp = "" Then Continue For
                            If cpoint.<nl:alias>.IsEmpty Then Continue For
                            origNames.Add(UInteger.Parse(cpoint.@cp, Globalization.NumberStyles.HexNumber, InvariantCulture), cpoint.<nl:alias>.Value)
                        Next
                    End If

                    Using m = UnmanagedModule.LoadLibraryAsDataFile(cmbCharsLanguage.SelectedItem.Path)
                        Dim rnames = m.GetResourceNames(ResourceTypeId.String)
                        Dim rni% = 0
                        For Each rname In rnames
                            Using data = m.GetResourceStream(ResourceTypeId.String, rname), br = New IO.BinaryReader(data, System.Text.Encoding.Unicode)
                                Dim blockn = If(rname.Id, Integer.Parse(rname.Name.Substring(1), InvariantCulture))
                                Dim i% = 0
                                While data.Position < data.Length
                                    Try
                                        Dim len = br.ReadInt16
                                        Dim str = New String(br.ReadChars(len))
                                        If str = "" Then Continue While
                                        Dim cp As Integer = (blockn - 1) * 16 + i
                                        If txtCheck.Text <> "" Then
                                            Dim origName$ = Nothing
                                            If origNames.TryGetValue(cp, origName) Then
                                                If origName = str Then Continue While
                                            End If
                                        End If
                                        If chkComments.Checked Then
                                            Dim ucdName$ = Nothing
                                            Dim ucdName2$ = "???"
                                            If Not ucdNames.TryGetValue(cp, ucdName) Then
                                                ucdName = String.Format("Unknown character #{0:X4}", cp)
                                            Else
                                                ucdName2 = ucdName
                                            End If
                                            ret.<ucd>.<repertoire>.First().Add(New XComment(ucdName))
                                            Debug.Print("{0:X4}: {1} >> {2}", cp, ucdName2, str)
                                        End If
                                        ret.<ucd>.<repertoire>.First.Add(
                                            <char cp=<%= cp.ToString("X4", InvariantCulture) %>>
                                                <nl:alias><%= str %></nl:alias>
                                            </char>
                                        )
                                    Finally
                                        i += 1
                                    End Try
                                End While
                            End Using
                            rni += 1
                            Debug.Print("{0:0.00%}", rni / rnames.Count)
                        Next
                    End Using
                End If

                If chkBlocks.Checked Then
                    Dim data = From b In UnicodeCharacterDatabase.Default.Blocks
                               Select New TranslateData With {.UcdName = b.Name, .LocalizedName = "", .FirstCodePoint = b.FirstCodePoint.CodePoint, .LastCodePoint = b.LastCodePoint.CodePoint}
                    Dim d2 = data.ToList
                    Dim win = New winTranslateBlockNames(d2)
                    If win.ShowDialog(Me) <> True Then Return

                    ret.<ucd>.First.Add(
                        <blocks><%= (
                                    From itm In d2 Where itm.LocalizedName <> ""
                                    Select New XNode() {
                                    If(chkComments.Checked, New XComment(itm.UcdName), Nothing),
                                    <block
                                        first-cp=<%= itm.FirstCodePoint.ToString("X4", InvariantCulture) %>
                                        last-cp=<%= itm.LastCodePoint.ToString("X4", InvariantCulture) %>
                                        name=<%= itm.LocalizedName %>
                                    />
                                    }).UnionAll %>
                        </blocks>
                        )
                End If

                If IO.File.Exists(txtTarget.Text) Then
                    If mBox.MsgBoxFW("File {0} already exists. Replace?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, Nothing, Me, txtTarget.Text) <> MsgBoxResult.Yes Then Return
                End If
                ret.Save(txtTarget.Text)
            Catch ex As Exception
                mBox.Error_XW(ex, Me)
            End Try
        End Sub

        Private Sub cmdCheckAgainst_Click(sender As System.Object, e As System.EventArgs) Handles cmdCheckAgainst.Click
            If ofdOpenXml.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                txtCheck.Text = ofdOpenXml.FileName
            End If
        End Sub

        Private Sub cmdAddMui_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddMui.Click
            If ofdMui.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                cmbCharsLanguage.Items.Add(New With {.Name = IO.Path.GetFileName(ofdMui.FileName), .Path = ofdMui.FileName})
                cmbCharsLanguage.SelectedIndex = cmbCharsLanguage.Items.Count - 1
            End If
        End Sub
    End Class
End Namespace