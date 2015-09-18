Imports Tools.ComponentModelT, System.ComponentModel, Tools.ResourcesT
Namespace ComponentModelT
    '#If TrueStage conditional compilation of this file is set in Tests.vbproj
    ''' <summary>Tests <see cref="LCategoryAttribute"/>, <see cref="LDescriptionAttribute"/> and <see cref="LDisplayNameAttribute"/></summary>
    Friend Class frmLocalizableAttributes : Inherits Form
        ''' <summary>CTor</summary>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = ToolsIcon
            prgMain.SelectedObject = New Obj
        End Sub
        ''' <summary>Shows test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmLocalizableAttributes
            frm.ShowDialog()
        End Sub

        ''' <summary>Object used for testing. Diplayed in <see cref="PropertyGrid"/></summary>
        Private Class Obj
            ''' <summary>Test only property</summary>
            <LDescription(GetType(ParamSource), "PublicRO", "Alternative")> _
            <LDisplayName(GetType(ParamSource), "PublicRO", "Alternative")> _
            <LCategory(GetType(CategorySource), "Ctg1", "Category1 alternative")> _
            Public ReadOnly Property PropertyPublicRO() As String
                Get
                    Return ParamSource.PublicRO & " Category: Category1 (resource first)"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <LDescription(GetType(ParamSource), "PublicRW", "Alternative")> _
            <LDisplayName(GetType(ParamSource), "PublicRW", "Alternative")> _
            <LCategory(GetType(CategorySource), "LayoutPrp", "Layout")> _
            Public ReadOnly Property PropertyPublicRW() As String
                Get
                    Return ParamSource.PublicRW & " Category: Layout (resource first)"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <LDescription(GetType(ParamSource), "PrivateRW", "Alternative")> _
            <LDisplayName(GetType(ParamSource), "PrivateRW", "Alternative")> _
            <LCategory(GetType(CategorySource), "LayoutPrp", "Font", LCategoryAttribute.enmLookUpOrder.ResourceOnly)> _
            Public ReadOnly Property PropertyPrivateRW() As String
                Get
                    Return "PrivateRW Category: Layout/Font (resource only)"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <LDescription(GetType(ParamSource), "PrivateRO", "Alternative")> _
            <LDisplayName(GetType(ParamSource), "PrivateRO", "Alternative")> _
            <LCategory(GetType(CategorySource), "LayoutPrp", "Config", LCategoryAttribute.enmLookUpOrder.NETFirst)> _
            Public ReadOnly Property PropertyPrivateRO() As String
                Get
                    Return "PrivateRO Category: Layout/Config (.NET first)"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <LDescription(GetType(ParamSource), "PublicWO", "Alternative")> _
            <LDisplayName(GetType(ParamSource), "PublicWO", "Alternative")> _
            <LCategory(GetType(CategorySource), "Acs", "Mouse")> _
            Public ReadOnly Property PropertyPublicWO() As String
                Get
                    Return "PublicWO Category: Mouse (resource first)"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <LDescription(GetType(ParamSource), "Instance", "Alternative")> _
            <LDisplayName(GetType(ParamSource), "Instance", "Alternative")> _
            <LCategory(GetType(CategorySource), "Acs", "Behavior", LCategoryAttribute.enmLookUpOrder.ResourceOnly)> _
            Public ReadOnly Property PropertyInstance() As String
                Get
                    Return "Instance Category: Behavior (resource only)"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <LDescription(GetType(StructureParamSource), "StructureProperty", "Alternative")> _
            <LDisplayName(GetType(StructureParamSource), "StructureProperty", "Alternative")> _
            <LCategory(GetType(CategorySource), "Acs", "Appearance", LCategoryAttribute.enmLookUpOrder.NETFirst)> _
            Public ReadOnly Property PropertyStructureProperty() As String
                Get
                    Return StructureParamSource.StructureProperty & " Category: Appearance (.NET First)"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <LDescription(GetType(ModuleParamSource), "ModuleProperty", "Alternative")> _
            <LDisplayName(GetType(ModuleParamSource), "ModuleProperty", "Alternative")> _
            <LCategory(GetType(CategorySource), "xxx", "Key", LCategoryAttribute.enmLookUpOrder.ResourceOnly)> _
            Public ReadOnly Property PropertyModuleProperty() As String
                Get
                    Return "ModuleProperty Category:Key (resource only)"
                End Get
            End Property
#Region "Advanced"
            ''' <summary>Test only property</summary>
            <Category("x-Advanced"), LDisplayName(GetType(AdvancedSource), "DimReadOnly")> _
            ReadOnly Property DimReadOnly() As String
                Get
                    Return "Public Shared ReadOnly"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <Category("x-Advanced"), LDisplayName(GetType(AdvancedSource), "DimReadOnly")> _
            ReadOnly Property DimNormal() As String
                Get
                    Return "Public Shared DimNormal"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <Category("x-Advanced"), LDisplayName(GetType(AdvancedSource), "DimReadOnly")> _
            ReadOnly Property DimWithEvents() As String
                Get
                    Return "Public Shared WithEvents DimWithEvents"
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <Category("x-Advanced"), LDisplayName(GetType(AdvancedSource), "DimReadOnly")> _
            ReadOnly Property Void() As String
                Get
                    Return "Return """""
                End Get
            End Property
            ''' <summary>Test only property</summary>
            <Category("x-Advanced"), LDisplayName(GetType(AdvancedSource), "Int")> _
            ReadOnly Property Int() As String
                Get
                    Return "Integer"
                End Get
            End Property
            ''' <summary>Data source for advanced testing</summary>
            Private Class AdvancedSource
                ''' <summary>Test only field</summary>
                Public Shared ReadOnly DimReadOnly As String = "DimReadOnly read"
                ''' <summary>Test only field</summary>
                Public Shared DimNormal As String = "DimNormal read"
                ''' <summary>Test only field</summary>
                Public Shared WithEvents DimWithEvents As String = "DimWithEvents read"
                ''' <summary>Test only property</summary>
                Public ReadOnly Property Void() As String
                    Get
                        Return ""
                    End Get
                End Property
                ''' <summary>Test only property</summary>
                Public ReadOnly Property Int() As Integer
                    Get
                        Return 14
                    End Get
                End Property
            End Class
#End Region
#Region "DeafultValue"
            ''' <summary>Contains value of the <see cref="WithDefaultValue1"/></summary>
            Private _WithDefaultValue1 As String = "This is not default value"
            ''' <summary>Test property</summary>
            <Category("DafaultValue"), LDefaultValue(GetType(DVSource), "DW1", "Alternative default value")> _
            <DisplayName("Public")> _
            Public Property WithDefaultValue1() As String
                Get
                    Return _WithDefaultValue1
                End Get
                Set(ByVal value As String)
                    _WithDefaultValue1 = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="WithDefaultValue2"/></summary>
            Private _WithDefaultValue2 As String = "This is not default value"
            ''' <summary>Test property</summary>
            <Category("DafaultValue"), LDefaultValue(GetType(DVSource), "DW2", "Alternative default value")> _
            <DisplayName("Protected")> _
            Public Property WithDefaultValue2() As String
                Get
                    Return _WithDefaultValue2
                End Get
                Set(ByVal value As String)
                    _WithDefaultValue2 = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="WithDefaultValue3"/></summary>
            Private _WithDefaultValue3 As String = "This is not default value"
            ''' <summary>Test property</summary>
            <Category("DafaultValue"), LDefaultValue(GetType(DVSource), "DW3", "Alternative default value")> _
            <DisplayName("Private")> _
            Public Property WithDefaultValue3() As String
                Get
                    Return _WithDefaultValue3
                End Get
                Set(ByVal value As String)
                    _WithDefaultValue3 = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="WithDefaultValue4"/></summary>
            Private _WithDefaultValue4 As String = "This is not default value"
            ''' <summary>Test property</summary>
            <Category("DafaultValue"), LDefaultValue(GetType(DVSource), "DW4", "Alternative default value")> _
            <DisplayName("Optionally indexed")> _
            Public Property WithDefaultValue4() As String
                Get
                    Return _WithDefaultValue4
                End Get
                Set(ByVal value As String)
                    _WithDefaultValue4 = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="WithDefaultValue3"/></summary>
            Private _WithDefaultValue5 As Uri = New Uri("http://This_is_not_default_value.com")
            ''' <summary>Test property</summary>
            <Category("DafaultValue"), LDefaultValue(GetType(DVSource), "DW5", GetType(Uri), "http://Alternative_default_value.com")> _
            <DisplayName("URI")> _
            Public Property WithDefaultValue5() As Uri
                Get
                    Return _WithDefaultValue5
                End Get
                Set(ByVal value As Uri)
                    _WithDefaultValue5 = value
                End Set
            End Property
#End Region
        End Class
#Region "Sources"
        ''' <summary>Source for testing <see cref="LCategoryAttribute"/></summary>
        Private MustInherit Class CategorySource
            ''' <summary>Value for <see cref="LCategoryAttribute"/></summary>
            Public Shared ReadOnly Property Ctg1() As String
                Get
                    Return "Category1"
                End Get
            End Property
            ''' <summary>Value for <see cref="LCategoryAttribute"/></summary>
            Public Shared ReadOnly Property LayoutPrp() As String
                Get
                    Return "Layout from property"
                End Get
            End Property
        End Class
        ''' <summary>Main source for testing <see cref="LDescriptionAttribute"/> and <see cref="LDisplayNameAttribute"/></summary>
        Private Class ParamSource
            ''' <summary>Value of attribute</summary>
            Public Shared ReadOnly Property PublicRO() As String
                Get
                    Return "PublicRO"
                End Get
            End Property
            ''' <summary>Value of attribute</summary>
            Public Shared Property PublicRW() As String
                Get
                    Return "PublicRW"
                End Get
                Set(ByVal value As String)
                End Set
            End Property
            ''' <summary>Value of attribute</summary>
            Private Shared Property PrivateRW() As String
                Get
                    Return "PrivateRW"
                End Get
                Set(ByVal value As String)
                End Set
            End Property
            ''' <summary>Value of attribute</summary>
            Private Shared ReadOnly Property PrivateRO() As String
                Get
                    Return "PrivateRO"
                End Get
            End Property
            ''' <summary>Value of attribute</summary>
            Public Shared WriteOnly Property PublicWO() As String
                Set(ByVal value As String)
                End Set
            End Property
            ''' <summary>Value of attribute</summary>
            Public ReadOnly Property Instance() As String
                Get
                    Return "Instance"
                End Get
            End Property
        End Class
        ''' <summary>Source for testing <see cref="LDescriptionAttribute"/> and <see cref="LDisplayNameAttribute"/> attributes' compatibility with structures</summary>
        Private Structure StructureParamSource
            ''' <summary>This is here only because there must be something in order <see cref="StructureParamSource"/> to be valid structure</summary>
            <System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
            Private x As Boolean
            ''' <summary>Value of attribute</summary>
            Friend Shared ReadOnly Property StructureProperty() As String
                Get
                    Return "StructureProperty"
                End Get
            End Property
        End Structure
#End Region
#Region "DVSources"
        ''' <summary>Source for testing <see cref="LDefaultValueAttribute"/></summary>
        Private Class DVSource
            ''' <summary>Test source property</summary>
            Public Shared ReadOnly Property DW1() As String
                Get
                    Return "This is default value"
                End Get
            End Property
            ''' <summary>Test source property</summary>
            Protected Shared ReadOnly Property DW2() As String
                Get
                    Return DW1
                End Get
            End Property
            ''' <summary>Test source property</summary>
            Private Shared ReadOnly Property DW3() As String
                Get
                    Return DW1
                End Get
            End Property
            ''' <summary>Test source property</summary>
            Public Shared ReadOnly Property DW4(Optional ByVal index As Long = 0) As String
                Get
                    Return DW1 & " " & index
                End Get
            End Property
            ''' <summary>Test source property</summary>
            Public Shared ReadOnly Property DW5() As Uri
                Get
                    Return New Uri("http://This_is_default_value.com")
                End Get
            End Property
        End Class
#End Region

        Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
            prgMain.ResetSelectedProperty()
        End Sub
    End Class
    ''' <summary>Source for testing <see cref="LDescriptionAttribute"/> and <see cref="LDisplayNameAttribute"/> attributes' compatibility with modules</summary>
    Friend Module ModuleParamSource
        ''' <summary>Value of attribute</summary>
        Private ReadOnly Property ModuleProperty() As String
            Get
                Return "ModuleProperty"
            End Get
        End Property
    End Module
End Namespace
