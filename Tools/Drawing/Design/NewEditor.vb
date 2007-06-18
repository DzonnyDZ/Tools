Imports System.ComponentModel, System.Drawing.Design, System.Windows.Forms, System.Drawing
#If Config <= Nightly Then
Namespace DrawingT.DesignT
    Public Class NewEditor 'ASAP: XML-Doc, Wiki, Forum, Mark
        Inherits UITypeEditor
        Public Overrides Function GetEditStyle(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.Drawing.Design.UITypeEditorEditStyle
            Return UITypeEditorEditStyle.DropDown
        End Function
        Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
            Using Lbl As New Label
                Lbl.BackColor = SystemColors.Window
                Lbl.ForeColor = SystemColors.WindowText
                Lbl.Text = "New ..." 'Localize: label text
                Lbl.Tag = context
                AddHandler Lbl.Click, AddressOf Lbl_Click
                Me.Context = context
                Service = provider.GetService(GetType(Windows.Forms.Design.IWindowsFormsEditorService))
                Service.DropDownControl(Lbl)
                If Lbl.Tag Is Nothing Then Return value Else Return Lbl.Tag
            End Using
        End Function
        Private Context As ITypeDescriptorContext
        Private Service As Windows.Forms.Design.IWindowsFormsEditorService
        Private Sub Lbl_Click(ByVal sender As Object, ByVal e As [EventArgs])
            With DirectCast(sender, Label)
                Try
                    Dim DVA As DefaultValueAttribute = Context.PropertyDescriptor.Attributes(GetType(DefaultValueAttribute))
                    If DVA IsNot Nothing Then
                        .Tag = DVA.Value
                    Else
                        .Tag = Activator.CreateInstance(Context.PropertyDescriptor.PropertyType)
                    End If
                Finally
                    Service.CloseDropDown()
                End Try
            End With
        End Sub
    End Class
End Namespace
#End If