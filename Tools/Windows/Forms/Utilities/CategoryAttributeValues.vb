#If Config <= Release Then
Namespace WindowsT.FormsT.UtilitiesT
    ''' <summary>Common values used for <see cref="CategoryAttribute"/></summary>
    ''' <remarks>
    ''' <para>This class contains values that when used for <see cref="CategoryAttribute"/> are recognized by the .NET Framework and localized to current language.</para>
    ''' <para>You can pass these constans either directly into <see cref="CategoryAttribute"/> or you can use <see cref="ComponentModelT.KnownCategoryAttribute"/>'s overloaded CTor that have better intellisense support.</para>
    ''' </remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(2, 1, GetType(CategoryAttributeValues), LastChMMDDYYYY:="05/14/2007")> _
    Public Class CategoryAttributeValues
        ''' <summary>Private in order not to pe possible to create instance or inherit from this class</summary>
        Private Sub New()
        End Sub
        ''' <summary>The Accessibility category</summary>
        Public Const Accessibility$ = "Accessibility"
        ''' <summary>The Action category</summary>
        Public Const Action$ = "Action"
        ''' <summary>The Appearance category</summary>
        Public Const Appearance$ = "Appearance"
        ''' <summary>The Asynchronous category</summary>
        Public Const Asynchronous$ = "Asynchronous"
        ''' <summary>The Behavior category</summary>
        Public Const Behavior$ = "Behavior"
        ''' <summary>The Configurations category</summary>
        Public Const Configurations$ = "Config"
        ''' <summary>The Data category</summary>
        Public Const Data$ = "Data"
        ''' <summary>The DDE category</summary>
        Public Const DDE$ = "DDE"
        ''' <summary>The Misc category</summary>
        Public Const Misc$ = "Default"
        ''' <summary>The Design category</summary>
        Public Const Design$ = "Design"
        ''' <summary>The DragDrop category</summary>
        Public Const DragDrop$ = "DragDrop"
        ''' <summary>The Focus category</summary>
        Public Const Focus$ = "Focus"
        ''' <summary>The Font category</summary>
        Public Const Font$ = "Font"
        ''' <summary>The Format category</summary>
        Public Const Format$ = "Format"
        ''' <summary>The Key category</summary>
        Public Const Key$ = "Key"
        ''' <summary>The Layout category</summary>
        Public Const Layout$ = "Layout"
        ''' <summary>The List category</summary>
        Public Const List$ = "List"
        ''' <summary>The Mouse category</summary>
        Public Const Mouse$ = "Mouse"
        ''' <summary>The Position category</summary>
        Public Const Position$ = "Position"
        ''' <summary>The Scale category</summary>
        Public Const Scale$ = "Scale"
        ''' <summary>The Text category</summary>
        Public Const Text$ = "Text"
        ''' <summary>The Window Style category</summary>
        Public Const WindowStyle$ = "WindowStyle"
        ''' <summary>The Colors category</summary>
        Friend Const Colors$ = "Colors"
        ''' <summary>The Display category</summary>
        Friend Const Display$ = "Display"
        ''' <summary>The Folder Browsing category</summary>
        Friend Const FolderBrowsing$ = "Folder Browsing"
        ''' <summary>The Items category</summary>
        Friend Const Items$ = "Items"
        ''' <summary>The Private category</summary>
        Friend Const Private$ = "Private"
        ''' <summary>The Property Chenged category</summary>
        Friend Const PropertyChanged$ = "Property Changed"
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
