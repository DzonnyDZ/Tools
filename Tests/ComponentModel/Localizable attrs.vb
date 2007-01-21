Imports Tools.ComponentModel
Namespace ComponentModel
    Public Class frmLocalizableAttributes : Inherits Form
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
            prgMain.SelectedObject = New Obj
        End Sub
        Public Shared Sub Test()
            Dim frm As New frmLocalizableAttributes
            frm.ShowDialog()
        End Sub

        Private Class Obj
            <LDescription(GetType(ParamSource), "PublicRO", "Alternative")> _
            Public ReadOnly Property PropertyPublicRO() As String
                Get
                    Return ParamSource.PublicRO
                End Get
            End Property
            <LDescription(GetType(ParamSource), "PublicRW", "Alternative")> _
            Public ReadOnly Property PropertyPublicRW() As String
                Get
                    Return ParamSource.PublicRW
                End Get
            End Property
            <LDescription(GetType(ParamSource), "PrivateRW", "Alternative")> _
            Public ReadOnly Property PropertyPrivateRW() As String
                Get
                    Return "PrivateRW"
                End Get
            End Property
            <LDescription(GetType(ParamSource), "PrivateRO", "Alternative")> _
            Public ReadOnly Property PropertyPrivateRO() As String
                Get
                    Return "PrivateRO"
                End Get
            End Property
            <LDescription(GetType(ParamSource), "PublicWO", "Alternative")> _
            Public ReadOnly Property PropertyPublicWO() As String
                Get
                    Return "PublicWO"
                End Get
            End Property
            <LDescription(GetType(ParamSource), "Instance", "Alternative")> _
            Public ReadOnly Property PropertyInstance() As String
                Get
                    Return "Instance"
                End Get
            End Property
            <LDescription(GetType(StructureParamSource), "StructureProperty", "Alternative")> _
            Public ReadOnly Property PropertyStructureProperty() As String
                Get
                    Return StructureParamSource.StructureProperty
                End Get
            End Property
            <LDescription(GetType(ModuleParamSource), "ModuleProperty", "Alternative")> _
            Public ReadOnly Property PropertyModuleProperty() As String
                Get
                    Return "ModuleProperty"
                End Get
            End Property
        End Class
#Region "Sources"
        Private Class ParamSource
            Public Shared ReadOnly Property PublicRO() As String
                Get
                    Return "PublicRO"
                End Get
            End Property
            Public Shared Property PublicRW() As String
                Get
                    Return "PublicRW"
                End Get
                Set(ByVal value As String)
                End Set
            End Property
            Private Shared Property PrivateRW() As String
                Get
                    Return "PrivateRW"
                End Get
                Set(ByVal value As String)
                End Set
            End Property
            Private Shared ReadOnly Property PrivateRO() As String
                Get
                    Return "PrivateRO"
                End Get
            End Property
            Public Shared WriteOnly Property PublicWO() As String
                Set(ByVal value As String)
                End Set
            End Property
            Public ReadOnly Property Instance() As String
                Get
                    Return "Instance"
                End Get
            End Property
        End Class
        Private Structure StructureParamSource
            Private x As Boolean
            Friend Shared ReadOnly Property StructureProperty() As String
                Get
                    Return "StructureProperty"
                End Get
            End Property
        End Structure
#End Region
    End Class
    Friend Module ModuleParamSource
        Private ReadOnly Property ModuleProperty() As String
            Get
                Return "ModuleProperty"
            End Get
        End Property
    End Module
End Namespace
