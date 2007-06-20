Imports System.ComponentModel, System.Drawing.Design, System.Windows.Forms, System.Drawing
#If Config <= Nightly Then
Namespace DrawingT.DesignT
    ''' <summary><see cref="UITypeEditor"/> of <see cref="Byte()"/> capable to save bytes info file and load them from it</summary>
    Public Class EmbededFileEditor 'ASAP: XML-Doc, Wiki, Forum, Mark
        Inherits UITypeEditor
        ''' <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method.</summary>
        ''' <returns><see cref="UITypeEditorEditStyle.DropDown"/></returns>
        Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
            Return UITypeEditorEditStyle.DropDown
        End Function
        ''' <summary>Edits the specified object's value using the editor style indicated by the <see cref="System.Drawing.Design.UITypeEditor.GetEditStyle"/> method.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that can be used to gain additional context information.</param>
        ''' <param name="value">The object to edit.</param>
        ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
        ''' <returns>Content of loaded file or <paramref name="value"/> if no file is loaded</returns>
        Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
            Using Lst As New ListBox
                Me.Value = value
                Me.Context = context
                Lst.DisplayMember = "Title"
                If value IsNot Nothing Then
                    Dim Save As New SaveFileDialog
                    Save.Title = "Save data as..." 'Localize: dialog title
                    Lst.Items.Add(Save)
                End If
                Dim [ReadOnly] As ReadOnlyAttribute = context.PropertyDescriptor.Attributes(GetType(ReadOnlyAttribute))
                If [ReadOnly] Is Nothing OrElse [ReadOnly].IsReadOnly = False Then
                    Dim Embed As New OpenFileDialog
                    Lst.Items.Add(Embed)
                    Embed.Title = "Load from file..." 'Localize: dialog title
                    Lst.Items.Add("Clear") 'Localize: Item
                End If
                AddHandler Lst.Click, AddressOf Lst_Click
                Lst.Height = Lst.Items.Count * Lst.ItemHeight + (Lst.Height - Lst.ClientSize.Height)
                Service = provider.GetService(GetType(Windows.Forms.Design.IWindowsFormsEditorService))
                Service.DropDownControl(Lst)
                Return Me.Value
            End Using
        End Function
        ''' <summary>The context parameter of <see cref="EditValue"/> used by <see cref="Lst_Click"/></summary>
        Private Context As ITypeDescriptorContext
        ''' <summary>service obtained from provider parameter of <see cref="EditValue"/> used by <see cref="Lst_Click"/></summary>
        Private Service As Windows.Forms.Design.IWindowsFormsEditorService
        ''' <summary>Property value is passed through this field between <see cref="EditValue"/> and <see cref="Lst_Click"/></summary>
        Private Value As Byte()
        ''' <summary>Invoked when <see cref="ListBox"/> that provided drop-down UI is clicked</summary>
        ''' <param name="sender">The <see cref="ListBox"/></param>
        ''' <param name="e">Event params</param>
        Private Sub Lst_Click(ByVal sender As Object, ByVal e As [EventArgs])
            Dim Lst As ListBox = sender
            If Lst.SelectedItem IsNot Nothing Then
                If TypeOf Lst.SelectedItem Is SaveFileDialog Then
                    Dim dlg As SaveFileDialog = Lst.SelectedItem
                    If dlg.ShowDialog = DialogResult.OK Then
                        Try
                            My.Computer.FileSystem.WriteAllBytes(dlg.FileName, Value, False)
                        Catch ex As Exception
                            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.Message.GetType.Name)
                        End Try
                    End If
                ElseIf TypeOf Lst.SelectedItem Is OpenFileDialog Then
                    Dim dlg As OpenFileDialog = Lst.SelectedItem
                    If dlg.ShowDialog = DialogResult.OK Then
                        Try
                            Value = My.Computer.FileSystem.ReadAllBytes(dlg.FileName)
                        Catch ex As Exception
                            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.Message.GetType.Name)
                        End Try
                    End If
                Else
                    Value = Nothing
                End If
                Service.CloseDropDown()
            End If
        End Sub
    End Class


    ''' <summary><see cref="UITypeEditor"/> of <see cref="Byte()"/> capable to save and load image</summary>
    Public Class EmbededImageEditor 'ASAP: XML-Doc, Wiki, Forum, Mark
        Inherits BitmapEditor
        ''' <summary>Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method.</summary>
        ''' <returns><see cref="UITypeEditorEditStyle.DropDown"/></returns>
        Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
            Return UITypeEditorEditStyle.DropDown
        End Function
        ''' <summary>Edits the specified object's value using the editor style indicated by the <see cref="System.Drawing.Design.UITypeEditor.GetEditStyle"/> method.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that can be used to gain additional context information.</param>
        ''' <param name="value">The object to edit.</param>
        ''' <param name="provider">An <see cref="System.IServiceProvider"/> that this editor can use to obtain services.</param>
        ''' <returns>Content of loaded file or <paramref name="value"/> if no file is loaded</returns>
        Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
            Using Lst As New ListBox
                Me.Value = value
                Me.Context = context
                Lst.DisplayMember = "Title"
                Dim Filter As String = "Images (bmp,jpeg,png,gif,tiff)|*.bmp;*.jpeg;*.jpg;*.gif;*.tif;*.tiff;*.png" 'Localize "images"
                If value IsNot Nothing Then
                    Dim Save As New SaveFileDialog
                    Save.Title = "Save data as..." 'Localize: dialog title
                    Save.Filter = Filter
                    Save.DefaultExt = "bmp"
                    Lst.Items.Add(Save)
                End If
                Dim [ReadOnly] As ReadOnlyAttribute = context.PropertyDescriptor.Attributes(GetType(ReadOnlyAttribute))
                If [ReadOnly] Is Nothing OrElse [ReadOnly].IsReadOnly = False Then
                    Dim Embed As New OpenFileDialog
                    Lst.Items.Add(Embed)
                    Embed.Title = "Load from file..." 'Localize: dialog title
                    Embed.Filter = Filter
                    Embed.DefaultExt = "bmp"
                    Lst.Items.Add("Clear") 'Localize: Item
                End If
                AddHandler Lst.Click, AddressOf Lst_Click
                Lst.Height = Lst.Items.Count * Lst.ItemHeight + (Lst.Height - Lst.ClientSize.Height)
                Service = provider.GetService(GetType(Windows.Forms.Design.IWindowsFormsEditorService))
                Service.DropDownControl(Lst)
                Return Me.Value
            End Using
        End Function
        ''' <summary>The context parameter of <see cref="EditValue"/> used by <see cref="Lst_Click"/></summary>
        Private Context As ITypeDescriptorContext
        ''' <summary>service obtained from provider parameter of <see cref="EditValue"/> used by <see cref="Lst_Click"/></summary>
        Private Service As Windows.Forms.Design.IWindowsFormsEditorService
        ''' <summary>Property value is passed through this field between <see cref="EditValue"/> and <see cref="Lst_Click"/></summary>
        Private Value As Image
        ''' <summary>Invoked when <see cref="ListBox"/> that provided drop-down UI is clicked</summary>
        ''' <param name="sender">The <see cref="ListBox"/></param>
        ''' <param name="e">Event params</param>
        Private Sub Lst_Click(ByVal sender As Object, ByVal e As [EventArgs])
            Dim Lst As ListBox = sender
            If Lst.SelectedItem IsNot Nothing Then
                Try
                    If TypeOf Lst.SelectedItem Is SaveFileDialog Then
                        Dim dlg As SaveFileDialog = Lst.SelectedItem
                        If dlg.ShowDialog = DialogResult.OK Then
                            Try
                                Dim f As Drawing.Imaging.ImageFormat
                                Select Case System.IO.Path.GetExtension(dlg.FileName).ToLower
                                    Case ".bmp" : f = Drawing.Imaging.ImageFormat.Bmp
                                    Case ".png" : f = Drawing.Imaging.ImageFormat.Png
                                    Case ".jpg", ".jpeg" : f = Drawing.Imaging.ImageFormat.Jpeg
                                    Case ".tif", ".tiff" : f = Drawing.Imaging.ImageFormat.Tiff
                                    Case ".gif" : f = Drawing.Imaging.ImageFormat.Gif
                                    Case ".wmf" : f = Drawing.Imaging.ImageFormat.Wmf
                                    Case ".emf" : f = Drawing.Imaging.ImageFormat.Emf
                                    Case "ico" : f = Drawing.Imaging.ImageFormat.Icon
                                    Case Else
                                        MsgBox("Unknown image extension, image will not be saved.", MsgBoxStyle.Exclamation, "Error") 'Localize: Message
                                        Exit Sub
                                End Select
                                Value.Save(dlg.FileName, f)
                            Catch ex As Exception
                                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.Message.GetType.Name)
                            End Try
                        End If
                    ElseIf TypeOf Lst.SelectedItem Is OpenFileDialog Then
                        Dim dlg As OpenFileDialog = Lst.SelectedItem
                        If dlg.ShowDialog = DialogResult.OK Then
                            Try
                                Value = Drawing.Bitmap.FromFile(dlg.FileName)
                            Catch ex As Exception
                                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.Message.GetType.Name)
                            End Try
                        End If
                    Else
                        Value = Nothing
                    End If
                Finally
                    Service.CloseDropDown()
                End Try
            End If
        End Sub
    End Class
End Namespace
#End If