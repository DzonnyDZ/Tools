Imports Tools.MetadataT.ExifT, Tools.DrawingT.DrawingIOt.JPEG, Tools.IOt.StreamTools
Imports MBox = Tools.WindowsT.IndependentT.MessageBox
Namespace MetadataT
    ''' <summary>Tests for <see cref="Tools.MetadataT.ExifT"/></summary>
    Public Class frmExif
        ''' <summary>Current Exif data</summary>
        Private WithEvents Exif As Exif
        ''' <summary>Name of current file</summary>
        Private FileName As String
        ''' <summary>Indicates if file was changed since loaded</summary>
        Private Changed As Boolean
        Dim OldType As Integer

        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub Test()
            Dim frm As New frmExif
            frm.ShowDialog()
        End Sub

        Private Sub tmiExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiExit.Click
            Me.Close()
        End Sub

        Private Sub tmiOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiOpen.Click
            If ofdOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    Select Case ofdOpen.FilterIndex
                        Case 1  'Jpeg
                            Using Jpeg As New JPEGReader(ofdOpen.FileName)
                                If ofdOpen.ReadOnlyChecked Then Exif = Exif.Load(Jpeg) _
                                Else Exif = Exif.LoadForUpdating(Jpeg)
                            End Using
                        Case 2       'Exif
                            Using f = IO.File.Open(ofdOpen.FileName, IO.FileMode.Open, IO.FileAccess.Read)
                                If ofdOpen.ReadOnlyChecked Then Exif = Exif.Load(f) _
                                Else Exif = Exif.LoadForUpdating(f)
                            End Using
                        Case 3            'Bin
                            Using f = IO.File.Open(ofdOpen.FileName, IO.FileMode.Open, IO.FileAccess.Read)
                                Dim fom As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
                                Exif = fom.Deserialize(f)
                            End Using
                    End Select
                    OldType = ofdOpen.FilterIndex
                    Changed = False
                    tslChanged.Text = ""
                    tslFileName.Text = IO.Path.GetFileName(ofdOpen.FileName)
                Catch ex As Exception
                    MBox.Error_X(ex)
                End Try
            End If
        End Sub

        Private Sub Exif_Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Handles Exif.Changed
            Changed = True
            tslChanged.Text = "*"
        End Sub

        Private Sub tmiSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiSave.Click
            Try
                Dim Jpeg As New JPEGReader(FileName)
                Exif.Update(Jpeg)
            Catch ex As Exception
                MBox.Error_X(ex)
            End Try
        End Sub

        Private Sub tmiSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiSaveAs.Click
            If sfdSave.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    Select Case sfdSave.FilterIndex
                        Case 1 '1:1
                            Using file = IO.File.Open(sfdSave.FileName, IO.FileMode.Create, IO.FileAccess.Write)
                                Select Case OldType
                                    Case 1 : Using old As New JPEGReader(FileName)
                                            file.Write(old.GetExifStream())
                                        End Using
                                    Case 2 : Using old = IO.File.Open(FileName, IO.FileMode.Open, IO.FileAccess.Read)
                                            file.Write(old)
                                        End Using
                                    Case Else : Throw New NotSupportedException("When Exif was dererialized, it cannot be written 1:1")
                                End Select
                            End Using
                        Case 2 'Preserve
                            Using file = IO.File.Open(sfdSave.FileName, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                                Exif.Update(file)
                            End Using
                        Case 3 'Save
                            Using file = IO.File.Open(sfdSave.FileName, IO.FileMode.Create, IO.FileAccess.Write)
                                Exif.Save(file)
                            End Using
                        Case 4 'Jpeg 1:1
                            Using Jpeg As New JPEGReader(sfdSave.FileName, True)
                                Dim StreamToWrite As IO.Stream = Jpeg.GetExifStream
                                Dim memory = False
                                If StreamToWrite Is Nothing Then
                                    StreamToWrite = New IO.MemoryStream
                                    memory = True
                                End If
                                Try
                                    Select Case OldType
                                        Case 1 : Using old As New JPEGReader(FileName)
                                                StreamToWrite.Write(old.GetExifStream())
                                            End Using
                                        Case 2 : Using old = IO.File.Open(FileName, IO.FileMode.Open, IO.FileAccess.Read)
                                                StreamToWrite.Write(old)
                                            End Using
                                        Case Else : Throw New NotSupportedException("When Exif was dererialized, it cannot be written 1:1")
                                    End Select
                                    If memory Then
                                        Dim Buff = DirectCast(StreamToWrite, IO.MemoryStream).GetBuffer
                                        If Buff.Length <> StreamToWrite.Length Then
                                            Dim Buff2(StreamToWrite.Length - 1) As Byte
                                            Array.ConstrainedCopy(Buff, 0, Buff2, 0, Buff2.Length)
                                            Buff = Buff2
                                        End If
                                        Jpeg.ExifEmbded(Buff)
                                    End If
                                Finally
                                    If memory Then StreamToWrite.Close()
                                End Try
                            End Using
                        Case 5 'Jpeg preserve
                            Throw New NotSupportedException 'TODO:Implement
                        Case 6 'Jpeg save
                            Throw New NotSupportedException 'TODO:Implement
                        Case 7 'Serialize
                            Using file = IO.File.Open(sfdSave.FileName, IO.FileMode.Create, IO.FileAccess.Write)
                                Dim formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter
                                formatter.Serialize(file, Exif)
                            End Using
                    End Select
                Catch ex As Exception
                    MBox.Error_X(ex)
                End Try
            End If
        End Sub
    End Class
End Namespace