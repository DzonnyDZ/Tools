Imports Tools.ResourcesT

#If True
Namespace ComponentModelT
    ''' <summary><see cref="CategoryAttribute"/> which's value is one of known values</summary>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class KnownCategoryAttribute : Inherits CategoryAttribute
        ''' <summary>Represents shared (static) properties of <see cref="CategoryAttribute"/></summary>
        ''' <remarks>This enumeration represents categories predefined by the .NET framework. <para>Values of <see cref="AnotherCategories"/> and <see cref="KnownCategories"/> are distinct. With exception of <see cref="AnotherCategories.Misc"/> = <see cref="KnownCategories.[Default]"/>.</para></remarks>
        ''' <seelaso cref="AnotherCategories"/>
        Public Enum KnownCategories As Byte
            ''' <summary><see cref="CategoryAttribute.Action"/></summary>
            Action = 1
            ''' <summary><see cref="CategoryAttribute.Appearance"/></summary>
            Appearance = 2
            ''' <summary><see cref="CategoryAttribute.Asynchronous"/></summary>
            Asynchronous = 3
            ''' <summary><see cref="CategoryAttribute.Behavior"/></summary>
            Behavior = 4
            ''' <summary><see cref="CategoryAttribute.Data"/></summary>
            Data = 5
            ''' <summary><see cref="CategoryAttribute.[Default]"/></summary>
            [Default] = 0
            ''' <summary><see cref="CategoryAttribute.Design"/></summary>
            Design = 1
            ''' <summary><see cref="CategoryAttribute.DragDrop"/></summary>
            DragDrop = 2
            ''' <summary><see cref="CategoryAttribute.Focus"/></summary>
            Focus = 3
            ''' <summary><see cref="CategoryAttribute.Format"/></summary>
            Format = 4
            ''' <summary><see cref="CategoryAttribute.Key"/></summary>
            Key = 5
            ''' <summary><see cref="CategoryAttribute.Layout"/></summary>
            Layout = 6
            ''' <summary><see cref="CategoryAttribute.Mouse"/></summary>
            Mouse = 7
            ''' <summary><see cref="CategoryAttribute.WindowStyle"/></summary>
            WindowStyle = 8
        End Enum
        ''' <summary>Defines more categories commonly used by <see cref="CategoryAttribute"/>, but not predefined by the .NET framework as shared properties of the <see cref="CategoryAttribute"/></summary>
        ''' <seelaso cref="KnownCategories"/>
        ''' <remarks>Values of <see cref="AnotherCategories"/> and <see cref="KnownCategories"/> are distinct. With exception of <see cref="AnotherCategories.Misc"/> = <see cref="KnownCategories.[Default]"/>.</remarks>
        Public Enum AnotherCategories As Byte
            ''' <summary>The Accessibility category</summary>
            Accessibility = 128
            ''' <summary>The Configurations category</summary>
            Configurations = 129
            ''' <summary>The DDE category</summary>
            DDE = 130
            ''' <summary>The Misc category</summary>
            Misc = 0
            ''' <summary>The Focus category</summary>
            Font = 132
            ''' <summary>The List category</summary>
            List = 133
            ''' <summary>The Mouse category</summary>
            Position = 134
            ''' <summary>The Scale category</summary>
            Scale = 135
            ''' <summary>The Text category</summary>
            Text = 136
            ''' <summary>The Colors category</summary>
            Colors = 137
            ''' <summary>The Display category</summary>
            Display = 138
            ''' <summary>The Folder Browsing category</summary>
            FolderBrowsing = 139
            ''' <summary>The Items category</summary>
            Items = 140
            ''' <summary>The Private category</summary>
            [Private] = 141
            ''' <summary>The Property Changed category</summary>
            PropertyChanged = 142
        End Enum
        ''' <summary>CTor from value preconfigured in .NET framework as shared (static) property of <see cref="CategoryAttribute"/></summary>
        ''' <param name="KnownCategory">Known value for <see cref="CategoryAttribute"/>. This parameter does <strong>not</strong> accept <see cref="AnotherCategories"/> values.</param>
        ''' <remarks>Names of <see cref="KnownCategories"/> categories are localized by .NET framework itself</remarks>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="KnownCategory"/> is not member of <see cref="KnownCategories"/></exception>
        Public Sub New(ByVal KnownCategory As KnownCategories)
            MyBase.New(KnownAttribute(KnownCategory).Category)
        End Sub
        ''' <summary>CTor from value represented as instance of <see cref="WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Value"/></summary>
        ''' <param name="KnownCategory">Instance of <see cref="WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Value"/></param>
        ''' <remarks>Thsi CTor is hint for IntelliSense only. However it is fully functional you will probably never use it.</remarks>
        Public Sub New(ByVal KnownCategory As WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Value)
            Me.New(KnownCategory.Value)
        End Sub
        ''' <summary>CTor from any <see cref="String"/></summary>
        ''' <param name="AnyCategory">Category to be passed to CTor of <see cref="CategoryAttribute"/></param>
        ''' <remarks>
        ''' This CTor allows you to pass any <see cref="String"/> to this class. This is CTor that is used instead of that one that takes <see cref="WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Value"/>. This is done becose of it is the way how you can tell intellisense to list values for you.
        ''' This CTor should be used with constants that are members of <see cref="WindowsT.FormsT.UtilitiesT.CategoryAttributeValues"/> (but there is no check).
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Sub New(ByVal AnyCategory As String)
            MyBase.New(AnyCategory)
        End Sub
        ''' <summary>CTor form one of <see cref="AnotherCategories"/> values.</summary>
        ''' <param name="KnownCategory">One of <see cref="AnotherCategories"/> values which determines string name of category. <see cref="KnownCategories"/> values are also valid.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="KnownCategory"/> is neither one of <see cref="KnownCategories"/> nor <see cref="AnotherCategories"/> values</exception>
        ''' <remarks><see cref="KnownCategories"/> are localized by .NET framework itself, <see cref="AnotherCategories"/> are localized by ÐTools. Availability of localized string depends on availability of such string in localizaion source.</remarks>
        Public Sub New(ByVal KnownCategory As AnotherCategories)
            MyBase.New(CategoryText(KnownCategory))
        End Sub
        ''' <summary>Gets localized text - name of given known category</summary>
        ''' <param name="KnownCategory">One of <see cref="KnownCategories"/> or <see cref="AnotherCategories"/> values</param>
        ''' <returns>String representing localized name of given category</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="KnownCategory"/> is neither one of <see cref="KnownCategories"/> nor <see cref="AnotherCategories"/> values</exception>
        ''' <remarks><see cref="KnownCategories"/> are localized by .NET framework itself, <see cref="AnotherCategories"/> are localized by ÐTools. Availability of localized string depends on availability of such string in localizaion source.</remarks>
        ''' <version version="1.5.2">Fixed: Categories <see cref="AnotherCategories.Configurations"/>, <see cref="AnotherCategories.Position"/>, <see cref="AnotherCategories.[Private]"/>, <see cref="AnotherCategories.PropertyChanged"/>, <see cref="AnotherCategories.Scale"/> and <see cref="AnotherCategories.Text"/> are not recognized and leads to <see cref="InvalidEnumArgumentException"/>.</version>
        Public Shared Function CategoryText(ByVal KnownCategory As AnotherCategories) As String
            If KnownCategory <> AnotherCategories.Misc AndAlso [Enum].IsDefined(GetType(KnownCategories), CByte(KnownCategory)) Then Return CategoryText(CType(KnownCategory, KnownCategories))
            Select Case KnownCategory
                Case AnotherCategories.Accessibility : Return Components.Accessibility_cat
                Case AnotherCategories.Colors : Return Components.Colors_cat
                Case AnotherCategories.DDE : Return Components.DDE_cat
                Case AnotherCategories.Display : Return Components.Display_cat
                Case AnotherCategories.FolderBrowsing : Return Components.FolderBrowsing_cat
                Case AnotherCategories.Font : Return Components.Font_cat
                Case AnotherCategories.Items : Return Components.Items_cat
                Case AnotherCategories.List : Return Components.List_cat
                Case AnotherCategories.Misc : Return ResourcesT.Components.Misc_cat
                Case AnotherCategories.Configurations : Return ResourcesT.Components.Config_cat
                Case AnotherCategories.Position : Return ResourcesT.Components.Position_cat
                Case AnotherCategories.Private : Return ResourcesT.Components.Private_cat
                Case AnotherCategories.PropertyChanged : Return ResourcesT.Components.PropertyChanged_cat
                Case AnotherCategories.Scale : Return ResourcesT.Components.Scale_cat
                Case AnotherCategories.Text : Return ResourcesT.Components.Text_cat
            End Select
            Throw New InvalidEnumArgumentException(String.Format(ResourcesT.Exceptions.MustBeOneOf1Or2Values, "KnownCategory", "KnownCategories", "AnotherCategories"))
        End Function

        ''' <summary>Gets localized text - name of given known category</summary>
        ''' <param name="KnownCategory">One of <see cref="KnownCategories"/> or <see cref="AnotherCategories"/> values</param>
        ''' <returns>String representing localized name of given category</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="KnownCategory"/> is neither one of <see cref="KnownCategories"/> nor <see cref="AnotherCategories"/> values</exception>
        ''' <remarks><see cref="KnownCategories"/> are localized by .NET framework itself, <see cref="AnotherCategories"/> are localized by ÐTools. Availability of localized string depends on availability of such string in localizaion source.</remarks>
        Public Shared Function CategoryText(ByVal KnownCategory As KnownCategories) As String
            If KnownCategory <> KnownCategories.Default AndAlso [Enum].IsDefined(GetType(AnotherCategories), CByte(KnownCategory)) Then Return CategoryText(CType(KnownCategory, AnotherCategories))
            Return KnownAttribute(KnownCategory).Category
        End Function

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

