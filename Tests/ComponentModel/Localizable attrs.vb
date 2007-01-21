Imports Tools.ComponentModel, System.ComponentModel
Namespace ComponentModel
    ''' <summary>Tests <see cref="LCategoryAttribute"/>, <see cref="LDescriptionAttribute"/> and <see cref="LDisplayNameAttribute"/></summary>
    Public Class frmLocalizableAttributes : Inherits Form
        ''' <summary>CTor</summary>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
            prgMain.SelectedObject = New Obj
        End Sub
        ''' <summary>Shows test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmLocalizableAttributes
            frm.ShowDialog()
        End Sub

        ''' <summary>Objest used for testing. Diplayed in <see cref="PropertyGrid"/></summary>
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
