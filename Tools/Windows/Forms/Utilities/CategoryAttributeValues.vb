#If Config <= Nightly Then 'Stage: Nightly
Namespace Windows.Forms.Utilities
    ''' <summary>Common values used for <see cref="CategoryAttribute"/></summary>
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
        ''' <summary>Combines constants from <see cref="Properties"/> and <see cref="Events"/></summary>
        Public NotInheritable Class All : Inherits CategoryAttributeValues
            ''' <summary>In order not to be able to construct it</summary>
            Private Sub New()
            End Sub
            ''' <summary>Accesibility category</summary>
            ''' <remarks>Inherited from <see cref="Properties"/></remarks>
            Public Const Accessibility$ = Properties.Accessibility
            ''' <summary>Appearance category</summary>
            ''' <remarks>Inherited from <see cref="Properties"/></remarks>
            Public Const Appearance$ = Properties.Appearance
            ''' <summary>Design category</summary>
            ''' <remarks>Inherited from <see cref="Properties"/></remarks>
            Public Const Design$ = Properties.Design
            ''' <summary>Action category</summary>
            ''' <remarks>Inherited from <see cref="Events"/></remarks>
            Public Const Action$ = Events.Action
            ''' <summary>Drag Drop category</summary>
            ''' <remarks>Inherited from <see cref="Events"/></remarks>
            Public Const DragDrop$ = Events.DragDrop
            ''' <summary>Key (keyboard) category</summary>
            ''' <remarks>Inherited from <see cref="Events"/></remarks>
            Public Const Key$ = Events.Key
            ''' <summary>Mouse category</summary>
            ''' <remarks>Inherited from <see cref="Events"/></remarks>
            Public Const Mouse$ = Events.Mouse
            ''' <summary>Property changed category</summary>
            ''' <remarks>Inherited from <see cref="Events"/></remarks>
            Public Const PropertyChanged$ = Events.PropertyChanged
        End Class
    End Class
End Namespace
#End If
