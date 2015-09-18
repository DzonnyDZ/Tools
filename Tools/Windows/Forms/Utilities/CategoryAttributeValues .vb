#If True
Namespace WindowsT.FormsT.UtilitiesT
    ''' <summary>Common values used for <see cref="CategoryAttribute"/></summary>
    ''' <remarks>
    ''' <para>This class contains values that when used for <see cref="CategoryAttribute"/> are recognized by the .NET Framework and localized to current language.</para>
    ''' <para>You can pass these constans either directly into <see cref="CategoryAttribute"/> or you can use <see cref="ComponentModelT.KnownCategoryAttribute"/>'s overloaded CTor that have better intellisense support.</para>
    ''' </remarks>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <author www="http://dzonny.cz">Đonny</author>
    <Obsolete("Members of this class were changed. They are no longer constant, so they cannot be used for CategoryAttribute. If you want to use this class use LCategoryAttribute, but you'd better using KnownCategoryAttribute")> _
    Public Class CategoryAttributeValues
        ''' <summary>Private in order not to pe possible to create instance or inherit from this class</summary>
        Private Sub New()
        End Sub
        ''' <summary>The Accessibility category</summary>
        Public Shared ReadOnly Accessibility$ = ResourcesT.Components.Accessibility_cat
        ''' <summary>The Action category</summary>
        Public Shared ReadOnly Action$ = ResourcesT.Components.Action_cat
        ''' <summary>The Appearance category</summary>
        Public Shared ReadOnly Appearance$ = ResourcesT.Components.Appearance_cat
        ''' <summary>The Asynchronous category</summary>
        Public Shared ReadOnly Asynchronous$ = ResourcesT.Components.Asynchronous_cat
        ''' <summary>The Behavior category</summary>
        Public Shared ReadOnly Behavior$ = ResourcesT.Components.Behavior_cat
        ''' <summary>The Configurations category</summary>
        Public Shared ReadOnly Configurations$ = ResourcesT.Components.Config_cat
        ''' <summary>The Data category</summary>
        Public Shared ReadOnly Data$ = ResourcesT.Components.Data_cat
        ''' <summary>The DDE category</summary>
        Public Shared ReadOnly DDE$ = ResourcesT.Components.DDE_cat
        ''' <summary>The Misc category</summary>
        Public Shared ReadOnly Misc$ = ResourcesT.Components.Default_cat
        ''' <summary>The Design category</summary>
        Public Shared ReadOnly Design$ = ResourcesT.Components.Design_cat
        ''' <summary>The DragDrop category</summary>
        Public Shared ReadOnly DragDrop$ = ResourcesT.Components.DragDrop_cat
        ''' <summary>The Focus category</summary>
        Public Shared ReadOnly Focus$ = ResourcesT.Components.Focus_cat
        ''' <summary>The Font category</summary>
        Public Shared ReadOnly Font$ = ResourcesT.Components.Font_cat
        ''' <summary>The Format category</summary>
        Public Shared ReadOnly Format$ = ResourcesT.Components.Format_cat
        ''' <summary>The Key category</summary>
        Public Shared ReadOnly Key$ = ResourcesT.Components.Key_cat
        ''' <summary>The Layout category</summary>
        Public Shared ReadOnly Layout$ = ResourcesT.Components.Layout_cat
        ''' <summary>The List category</summary>
        Public Shared ReadOnly List$ = ResourcesT.Components.List_cat
        ''' <summary>The Mouse category</summary>
        Public Shared ReadOnly Mouse$ = ResourcesT.Components.Mouse_cat
        ''' <summary>The Position category</summary>
        Public Shared ReadOnly Position$ = ResourcesT.Components.Position_cat
        ''' <summary>The Scale category</summary>
        Public Shared ReadOnly Scale$ = ResourcesT.Components.Scale_cat
        ''' <summary>The Text category</summary>
        Public Shared ReadOnly Text$ = ResourcesT.Components.Text_cat
        ''' <summary>The Window Style category</summary>
        Public Shared ReadOnly WindowStyle$ = ResourcesT.Components.WindowStyle_cat
        ''' <summary>The Colors category</summary>
        Friend Shared ReadOnly Colors$ = ResourcesT.Components.Colors_cat
        ''' <summary>The Display category</summary>
        Friend Shared ReadOnly Display$ = ResourcesT.Components.Display_cat
        ''' <summary>The Folder Browsing category</summary>
        Friend Shared ReadOnly FolderBrowsing$ = ResourcesT.Components.FolderBrowsing_cat
        ''' <summary>The Items category</summary>
        Friend Shared ReadOnly Items$ = ResourcesT.Components.Items_cat
        ''' <summary>The Private category</summary>
        Friend Shared ReadOnly Private$ = ResourcesT.Components.Private_cat
        ''' <summary>The Property Chenged category</summary>
        Friend Shared ReadOnly PropertyChanged$ = ResourcesT.Components.PropertyChanged_cat
        ''' <summary>Represents known value of <see cref="CategoryAttribute"/> as defined in <see cref="CategoryAttributeValues"/></summary>
        ''' <remarks>This structure is only hint for intellisense</remarks>
        ''' <completionlist cref="CategoryAttributeValues"/>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Structure Value
            ''' <summary>Name of category this instance represents</summary>
            Public Value As String
            ''' <summary>Converts <see cref="String"/> to <see cref="Value"/></summary>
            ''' <param name="a">A <see cref="String"/></param>
            ''' <returns>New <see cref="Value"/> whichs <see cref="Value.Value"/> is <paramref name="a"/></returns>
            Public Shared Widening Operator CType(ByVal a As String) As Value
                Dim ret As Value
                ret.Value = a
                Return ret
            End Operator
            ''' <summary>Converts <see cref="Value"/> to <see cref="String"/></summary>
            ''' <param name="a">A <see cref="Value"/></param>
            ''' <returns><paramref name="a"/>.<see cref="Value.Value">Value</see></returns>
            Public Shared Widening Operator CType(ByVal a As Value) As String
                Return a.Value
            End Operator
        End Structure
    End Class
End Namespace
#End If
