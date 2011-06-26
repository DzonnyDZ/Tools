Imports Tools.TextT.UnicodeT
Imports mBox = Tools.WindowsT.IndependentT.MessageBox
Namespace TextT.UnicodeT
    ''' <summary>Containes manual tests for <see cref="UnicodeCharacterDatabase"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class frmUnicodeCharacterDatabaseTest
        ''' <summary>Lanunches the test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmUnicodeCharacterDatabaseTest
            frm.ShowDialog()
        End Sub

        Private Sub cmdGetXml_Click(sender As System.Object, e As System.EventArgs) Handles cmdGetXml.Click
            Try
                Dim doc = UnicodeCharacterDatabase.GetXml()
                If fsdSaveXml.ShowDialog = DialogResult.OK Then
                    doc.Save(fsdSaveXml.FileName)
                End If
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
        End Sub
    End Class
End Namespace