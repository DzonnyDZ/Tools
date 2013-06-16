Imports Tools.InteropT
Imports mBox = Tools.WindowsT.IndependentT.MessageBox

Namespace InteropT
    Public Class frmUnmanagedResources
        Private file$
        Public Sub New(dll As String)
            InitializeComponent()
            file = dll

            Me.Text &= " for " & IO.Path.GetFileName(file)
        End Sub

        Public Shared Sub Test()
            Dim dlg As New OpenFileDialog() With {
                .Filter = "Libraries (*.dll)|*.dll|Executables (*.exe)|*.exe|MUI Files (*.mui)|*.mui|ActiveX Components (*.ocx)|*.ocx|All files (*.*)|*.*"
                }
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim frm As New frmUnmanagedResources(dlg.FileName)
                frm.ShowDialog()
            End If
        End Sub

        Private mdl As UnmanagedModule
        Private Shadows shown As Boolean
        Protected Overrides Sub OnShown(e As System.EventArgs)
            MyBase.OnShown(e)
            If Not shown Then
                shown = True

                Try
                    mdl = UnmanagedModule.LoadLibraryAsDataFile(file)

                    For Each type In mdl.GetResourceTypes
                        Dim node = tvwTree.Nodes.Add(If(type.Name, type.Id.ToString))
                        node.Tag = type
                        For Each ident In mdl.GetResourceNames(type)
                            Dim n2 = node.Nodes.Add(ident.ToString)
                            n2.Tag = ident
                        Next
                    Next
                Catch ex As Exception
                    mBox.Error_X(ex)
                End Try
            End If
        End Sub

        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
            If mdl IsNot Nothing Then mdl.Dispose()
        End Sub

        Private Sub tvwTree_NodeMouseDoubleClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwTree.NodeMouseDoubleClick
            If e.Node.Parent IsNot Nothing Then
                Try
                    Using str = mdl.GetResourceStream(e.Node.Parent.Tag, e.Node.Tag)
                        Dim type As ResourceTypeIdentifier = e.Node.Parent.Tag
                        If (type.Id IsNot Nothing AndAlso type.Id = ResourceTypeId.String) OrElse (type.Name = "String") Then
                            Dim r As New IO.StreamReader(str, System.Text.Encoding.Unicode, True)
                            Dim stringData = r.ReadToEnd
                            Debug.Print(stringData.Replace(NullChar, " "c))
                            If mBox.MsgBox(stringData.Replace(NullChar, " "c) & vbCrLf & vbCrLf & "Save?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then str.Position = 0 : GoTo Save
                            'End Using
                        Else
Save:                       If sfdSave.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                                Using f = IO.File.OpenWrite(sfdSave.FileName)
                                    Dim buff(1024) As Byte
                                    Dim readNo%
                                    Do
                                        readNo = str.Read(buff, 0, buff.Length)
                                        f.Write(buff, 0, readNo)
                                    Loop While readNo > 0
                                    f.Flush()
                                End Using
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    mBox.Error_X(ex)
                End Try
            End If
        End Sub

        Private Sub tvwTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvwTree.AfterSelect
            If e.Node.Parent IsNot Nothing Then
                Try
                    Using str = mdl.GetResourceStream(e.Node.Parent.Tag, e.Node.Tag)
                        Using r As New IO.StreamReader(str, System.Text.Encoding.Unicode)
                            txtPreview.Text = r.ReadToEnd.Replace(vbNullChar, "")
                            txtPreview.ForeColor = SystemColors.WindowText
                        End Using
                    End Using
                Catch ex As Exception
                    txtPreview.Text = String.Format("{0}: {1}", ex.GetType.Name, ex.Message)
                    txtPreview.ForeColor = Color.Red
                End Try
            End If
        End Sub

        Private Sub cmdLoadString_Click(sender As Object, e As EventArgs) Handles cmdLoadString.Click
            Try
                txtPreview.Text = mdl.LoadString(CUInt(nudStringId.Value))
                txtPreview.ForeColor = SystemColors.WindowText
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
        End Sub
    End Class
End Namespace