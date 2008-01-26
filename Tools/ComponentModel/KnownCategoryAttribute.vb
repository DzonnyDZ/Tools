#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModelT
    ''' <summary><see cref="CategoryAttribute"/> which's value is one of known values</summary>
    <Author("Ðonny", eMail:="dzonny@dzonny.cz", WWW:="http://dzonny.cz")> _
    <Version(1, 0, GetType(KnownCategoryAttribute), LastChange:="10/02/2007")> _
    <FirstVersion("10/02/2007")> _
    Public Class KnownCategoryAttribute : Inherits CategoryAttribute
        ''' <summary>Represents shared (static) properties of <see cref="CategoryAttribute"/></summary>
        Public Enum KnownCategories As Byte
            ''' <summary><see cref="CategoryAttribute.Action"/></summary>
            Action
            ''' <summary><see cref="CategoryAttribute.Appearance"/></summary>
            Appearance
            ''' <summary><see cref="CategoryAttribute.Asynchronous"/></summary>
            Asynchronous
            ''' <summary><see cref="CategoryAttribute.Behavior"/></summary>
            Behavior
            ''' <summary><see cref="CategoryAttribute.Data"/></summary>
            Data
            ''' <summary><see cref="CategoryAttribute.[Default]"/></summary>
            [Default]
            ''' <summary><see cref="CategoryAttribute.Design"/></summary>
            Design
            ''' <summary><see cref="CategoryAttribute.DragDrop"/></summary>
            DragDrop
            ''' <summary><see cref="CategoryAttribute.Focus"/></summary>
            Focus
            ''' <summary><see cref="CategoryAttribute.Format"/></summary>
            Format
            ''' <summary><see cref="CategoryAttribute.Key"/></summary>
            Key
            ''' <summary><see cref="CategoryAttribute.Layout"/></summary>
            Layout
            ''' <summary><see cref="CategoryAttribute.Mouse"/></summary>
            Mouse
            ''' <summary><see cref="CategoryAttribute.WindowStyle"/></summary>
            WindowStyle
        End Enum
        ''' <summary>CTor from value preconfigured in .NET framework as shared (static) property of <see cref="CategoryAttribute"/></summary>
        ''' <param name="KnownCategory">Known value for <see cref="CategoryAttribute"/></param>
        Public Sub New(ByVal KnownCategory As KnownCategories)
            MyBase.New(KnownAttribute(KnownCategory).Category)
        End Sub
        ''' <summary>CTor from value represented as instance of <see cref="WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Value"/></summary>
        ''' <param name="KnownCategory">Instance of <see cref="WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Value"/></param>
        ''' <remarks>Thsi CTor is hint for intellisense only. However it is fully functional you will probably never use it.</remarks>
        Public Sub New(ByVal KnownCategory As WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Value)
            Me.New(KnownCategory.Value)
        End Sub
        ''' <summary>CTor from any <see cref="String"/></summary>
        ''' <param name="AnyCategory">Category to be passed to CTor of <see cref="CategoryAttribute"/></param>
        ''' <remarks>
        ''' This CTor allows you to pass any <see cref="String"/> to this class. This is CTor that is used instead of that one that takes <see cref="WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Value"/>. This is done becose of it is the way how you can tell intellisense to list values for you.
        ''' This ctor should be used with constants that are members of <see cref="WindowsT.FormsT.UtilitiesT.CategoryAttributeValues"/> (but there is no chceck).
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Sub New(ByVal AnyCategory As String)
            MyBase.New(AnyCategory)
        End Sub

        ''' <summary></summary>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="KnownCategory"/> is not member of <see cref="KnownCategories"/></exception>
        Public Shared Function KnownAttribute(ByVal KnownCategory As KnownCategories) As CategoryAttribute
            Select Case KnownCategory
                Case KnownCategories.Action : Return CategoryAttribute.Action
                Case KnownCategories.Appearance : Return CategoryAttribute.Appearance
                Case KnownCategories.Asynchronous : Return CategoryAttribute.Asynchronous
                Case KnownCategories.Behavior : Return CategoryAttribute.Behavior
                Case KnownCategories.Data : Return CategoryAttribute.Data
                Case KnownCategories.Default : Return CategoryAttribute.Default
                Case KnownCategories.Design : Return CategoryAttribute.Design
                Case KnownCategories.DragDrop : Return CategoryAttribute.DragDrop
                Case KnownCategories.Focus : Return CategoryAttribute.Focus
                Case KnownCategories.Format : Return CategoryAttribute.Format
                Case KnownCategories.Key : Return CategoryAttribute.Key
                Case KnownCategories.Layout : Return CategoryAttribute.Layout
                Case KnownCategories.Mouse : Return CategoryAttribute.Mouse
                Case KnownCategories.WindowStyle : Return CategoryAttribute.WindowStyle
                Case Else : Throw New InvalidEnumArgumentException("KnownCategory", KnownCategory, GetType(KnownCategories))
            End Select
        End Function
    End Class
End Namespace
#End If

