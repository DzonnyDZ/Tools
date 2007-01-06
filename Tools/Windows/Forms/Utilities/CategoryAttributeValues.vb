#If Config <= Nightly Then 'Stage: Nightly
Namespace Windows.Forms.Utilities
    ''' <summary>Common values used for <see cref="CategoryAttribute"/></summary>
    ''' <remarks>Directly contains values used for both - events and properties</remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(CategoryAttributeValues), LastChange:="1/6/2007")> _
    Public Class CategoryAttributeValues
        ''' <summary>In order not to be able to construct it</summary>
        Private Sub New()
        End Sub
        ''' <summary>Behavior category</summary>
        Public Const Behavior$ = "Behavior"
        ''' <summary>Data category</summary>
        Public Const Data$ = "Data"
        ''' <summary>Focus category</summary>
        Public Const Focus$ = "Focus"
        ''' <summary>Lyout category</summary>
        Public Const Layout$ = "Layout"
        ''' <summary>Miscaleneous category</summary>
        Public Const Misc$ = "Misc"
        ''' <summary>Common values used for <see cref="CategoryAttribute"/> on properties</summary>
        <HideModuleName()> _
        Public NotInheritable Class Properties : Inherits CategoryAttributeValues
            ''' <summary>In order not to be able to construct it</summary>
            Private Sub New()
            End Sub
            ''' <summary>Accesibility category</summary>
            Public Const Accessibility$ = "Accessibility"
            ''' <summary>Appearance category</summary>
            Public Const Appearance$ = "Appearance"
            ''' <summary>Design category</summary>
            Public Const Design$ = "Design"
        End Class
        ''' <summary>Common values used for <see cref="CategoryAttribute"/> on events</summary>
        <HideModuleName()> _
        Public NotInheritable Class Events : Inherits CategoryAttributeValues
            ''' <summary>In order not to be able to construct it</summary>
            Private Sub New()
            End Sub
            ''' <summary>Action category</summary>
            Public Const Action$ = "Action"
            ''' <summary>Drag Drop category</summary>
            Public Const DragDrop$ = "Drag Drop"
            ''' <summary>Key (keyboard) category</summary>
            Public Const Key$ = "Key"
            ''' <summary>Mouse category</summary>
            Public Const Mouse$ = "Mouse"
            ''' <summary>Property changed category</summary>
            Public Const PropertyChanged$ = "Property Changed"
        End Class
    End Class
End Namespace
#End If
