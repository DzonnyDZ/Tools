Imports System.ComponentModel, System.Drawing.Design, System.Windows.Forms, System.Drawing
#If Config <= Nightly Then
Namespace DrawingT.DesignT
    ''' <summary><see cref="UITypeEditor"/> capable of creating new instance either from <see cref="DefaultValueAttribute"/> (preffered if available) or by parameterless CTor</summary>
    ''' <remarks>The <see cref="DefaultValueAttribute"/> used can be applyed either on property (preffered) or on type of the property</remarks>
    Public Class NewEditor 'ASAP: XML-Doc, Wiki, Forum, Mark
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
        ''' <returns>New value of type of property  obtained either via <see cref="DefaultValueAttribute"/> or via default CTor</returns>
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
        ''' <summary>The context parameter of <see cref="EditValue"/> used by <see cref="Lbl_Click"/></summary>
        Private Context As ITypeDescriptorContext
        ''' <summary>service obtained from provider parameter of <see cref="EditValue"/> used by <see cref="Lbl_Click"/></summary>
        Private Service As Windows.Forms.Design.IWindowsFormsEditorService
        ''' <summary>Handles <see cref="Label.Click"/> event of label used to provide drop-down UI</summary>
        ''' <param name="sender">The <see cref="Label"/></param>
        ''' <param name="e">Event params</param>
        Private Sub Lbl_Click(ByVal sender As Object, ByVal e As [EventArgs])
            With DirectCast(sender, Label)
                Try
                    Dim DVA As DefaultValueAttribute = Context.PropertyDescriptor.Attributes(GetType(DefaultValueAttribute))
                    If DVA Is Nothing Then Try : DVA = Context.PropertyDescriptor.PropertyType.GetCustomAttributes(GetType(DefaultValueAttribute), True)(0) : Catch : End Try
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