Imports System.ComponentModel
Imports System.Configuration
Imports System.Xml.Serialization

<XmlRoot()> _
Public Class AnyClass
    <XmlAttribute()> _
    Public A1 As Integer
    <XmlAttribute()> _
    Public A2 As String
End Class

<Serializable(), SettingsSerializeAs(SettingsSerializeAs.Xml)> _
Friend Class Setting_Monitor
    Private _Root As String = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
    Private _FolderMask As String = "*"
    Private _FileMask As String = "*.jpg;*.jpeg"
    Private _Interval As UShort = 5
    Private _RandomizeInterval As Byte = 5
    Private _BgColor As Color = Color.Black
    Private Label_TL As Settings_Label = New Settings_Label()
    Private Label_TC As Settings_Label
    Private Label_TR As Settings_Label
    Private Label_ML As Settings_Label
    Private Label_MC As Settings_Label
    Private Label_MR As Settings_Label
    Private Label_BL As Settings_Label
    Private Label_BC As Settings_Label
    Private Label_BR As Settings_Label
    Private _Alghoritm As SSAverAlghoritm = SSAverAlghoritm.Random
    <XmlElement(GetType(String))> _
    Public Property Root() As String
        Get
            Return _Root
        End Get
        Set(ByVal value As String)
            _Root = value
        End Set
    End Property
    <XmlElement(GetType(String))> _
    Public Property FolderMask() As String
        Get
            Return _FolderMask
        End Get
        Set(ByVal value As String)
            _FileMask = value
        End Set
    End Property
    <XmlElement(GetType(String))> _
    Public Property FileMask() As String
        Get
            Return _FileMask
        End Get
        Set(ByVal value As String)
            _FileMask = value
        End Set
    End Property
    <XmlElement(GetType(UShort))> _
    Public Property Interval() As UShort
        Get
            Return _Interval
        End Get
        Set(ByVal value As UShort)
            _Interval = value
        End Set
    End Property
    <XmlElement(GetType(Byte))> _
    Public Property RandomizeInterval() As Byte
        Get
            Return _RandomizeInterval
        End Get
        Set(ByVal value As Byte)
            _RandomizeInterval = value
        End Set
    End Property
    <XmlElement(GetType(Color))> _
    Public Property BgColor() As Color
        Get
            Return _BgColor
        End Get
        Set(ByVal value As Color)
            _BgColor = value
        End Set
    End Property
    Public Property Labels(ByVal Position As ContentAlignment) As Settings_Label.Structure
        Get
            Select Case Position
                Case ContentAlignment.BottomCenter : Return Label_BC
                Case ContentAlignment.BottomLeft : Return Label_BL
                Case ContentAlignment.BottomRight : Return Label_BR
                Case ContentAlignment.MiddleCenter : Return Label_MC
                Case ContentAlignment.MiddleLeft : Return Label_ML
                Case ContentAlignment.MiddleRight : Return Label_MR
                Case ContentAlignment.TopCenter : Return Label_TC
                Case ContentAlignment.TopLeft : Return Label_TL
                Case ContentAlignment.TopRight : Return Label_TR
                Case Else : Throw New InvalidEnumArgumentException("Position", Position, GetType(ContentAlignment))
            End Select
        End Get
        Set(ByVal value As Settings_Label.Structure)
            Select Case Position
                Case ContentAlignment.BottomCenter : Label_BC = value
                Case ContentAlignment.BottomLeft : Label_BL = value
                Case ContentAlignment.BottomRight : Label_BR = value
                Case ContentAlignment.MiddleCenter : Label_MC = value
                Case ContentAlignment.MiddleLeft : Label_ML = value
                Case ContentAlignment.MiddleRight : Label_MR = value
                Case ContentAlignment.TopCenter : Label_TC = value
                Case ContentAlignment.TopLeft : Label_TL = value
                Case ContentAlignment.TopRight : Label_TR = value
                Case Else : Throw New InvalidEnumArgumentException("Position", Position, GetType(ContentAlignment))
            End Select
        End Set
    End Property
    <XmlElement(GetType(SSAverAlghoritm))> _
    Public Property Alghoritm() As SSAverAlghoritm
        Get
            Return _Alghoritm
        End Get
        Set(ByVal value As SSAverAlghoritm)
            _Alghoritm = value
        End Set
    End Property

    Public Structure [Structure]
        Public Root As String
        Public FolderMask As String
        Public FileMask As String
        Public Interval As UShort
        Public RandomizeInterval As Byte
        Public BgColor As Color
        Public Label_TL As Settings_Label.Structure
        Public Label_TC As Settings_Label.Structure
        Public Label_TR As Settings_Label.Structure
        Public Label_ML As Settings_Label.Structure
        Public Label_MC As Settings_Label.Structure
        Public Label_MR As Settings_Label.Structure
        Public Label_BL As Settings_Label.Structure
        Public Label_BC As Settings_Label.Structure
        Public Label_BR As Settings_Label.Structure
        Public Alghoritm As SSAverAlghoritm
        Public Shared Widening Operator CType(ByVal a As Setting_Monitor) As [Structure]
            Dim ret As New [Structure]
            ret.Alghoritm = a.Alghoritm
            ret.BgColor = a.BgColor
            ret.FileMask = a.FileMask
            ret.FolderMask = a.FolderMask
            ret.Interval = a.Interval
            ret.Label_BC = a.Label_BC
            ret.Label_BL = a.Label_BL
            ret.Label_BR = a.Label_BR
            ret.Label_MC = a.Label_MC
            ret.Label_ML = a.Label_ML
            ret.Label_MR = a.Label_MR
            ret.Label_TC = a.Label_TC
            ret.Label_TL = a.Label_TL
            ret.Label_TR = a.Label_TR
            ret.RandomizeInterval = a.RandomizeInterval
            ret.Root = a.Root
            Return ret
        End Operator
        Public Shared Widening Operator CType(ByVal a As [Structure]) As Setting_Monitor
            Dim ret As New Setting_Monitor
            ret.Alghoritm = a.Alghoritm
            ret.BgColor = a.BgColor
            ret.FileMask = a.FileMask
            ret.FolderMask = a.FolderMask
            ret.Interval = a.Interval
            ret.Label_BC = a.Label_BC
            ret.Label_BL = a.Label_BL
            ret.Label_BR = a.Label_BR
            ret.Label_MC = a.Label_MC
            ret.Label_ML = a.Label_ML
            ret.Label_MR = a.Label_MR
            ret.Label_TC = a.Label_TC
            ret.Label_TL = a.Label_TL
            ret.Label_TR = a.Label_TR
            ret.RandomizeInterval = a.RandomizeInterval
            ret.Root = a.Root
            Return ret
        End Operator
        Public Property Labels(ByVal Position As ContentAlignment) As Settings_Label.Structure
            Get
                Select Case Position
                    Case ContentAlignment.BottomCenter : Return Label_BC
                    Case ContentAlignment.BottomLeft : Return Label_BL
                    Case ContentAlignment.BottomRight : Return Label_BR
                    Case ContentAlignment.MiddleCenter : Return Label_MC
                    Case ContentAlignment.MiddleLeft : Return Label_ML
                    Case ContentAlignment.MiddleRight : Return Label_MR
                    Case ContentAlignment.TopCenter : Return Label_TC
                    Case ContentAlignment.TopLeft : Return Label_TL
                    Case ContentAlignment.TopRight : Return Label_TR
                    Case Else : Throw New InvalidEnumArgumentException("Position", Position, GetType(ContentAlignment))
                End Select
            End Get
            Set(ByVal value As Settings_Label.Structure)
                Select Case Position
                    Case ContentAlignment.BottomCenter : Label_BC = value
                    Case ContentAlignment.BottomLeft : Label_BL = value
                    Case ContentAlignment.BottomRight : Label_BR = value
                    Case ContentAlignment.MiddleCenter : Label_MC = value
                    Case ContentAlignment.MiddleLeft : Label_ML = value
                    Case ContentAlignment.MiddleRight : Label_MR = value
                    Case ContentAlignment.TopCenter : Label_TC = value
                    Case ContentAlignment.TopLeft : Label_TL = value
                    Case ContentAlignment.TopRight : Label_TR = value
                    Case Else : Throw New InvalidEnumArgumentException("Position", Position, GetType(ContentAlignment))
                End Select
            End Set
        End Property
    End Structure
End Class

<Serializable(), SettingsSerializeAs(SettingsSerializeAs.Xml)> _
Friend Class Settings_Label
    Private _BgColor As Color = Color.Transparent
    Private _FgColor As Color = Color.Lime
    Private _Font As Font = DefaultFont
    Private _Text As String = My.Resources.DefaultText
    <XmlElement(GetType(Color))> _
    Public Property BgColor() As Color
        Get
            Return _BgColor
        End Get
        Set(ByVal value As Color)
            _BgColor = value
        End Set
    End Property
    <XmlElement(GetType(Color))> _
    Public Property FgColor() As Color
        Get
            Return _FgColor
        End Get
        Set(ByVal value As Color)
            _FgColor = value
        End Set
    End Property
    <XmlElement(GetType(Font))> _
    Public Property Font() As Font
        Get
            Return _Font
        End Get
        Set(ByVal value As Font)
            _Font = value
        End Set
    End Property
    <XmlElement(GetType(String))> _
    Public Property Text() As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value
        End Set
    End Property
    Private Shared ReadOnly Property DefaultFont() As Font
        Get
            Try : Return New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point) : Catch : Return Nothing : End Try
        End Get
    End Property

    Public Structure [Structure]
        Public BgColor As Color
        Public FgColor As Color
        Public Font As Font
        Public Text As String
        Public Enabled As Boolean
        Public Shared Widening Operator CType(ByVal a As Settings_Label) As [Structure]
            Dim ret As New [Structure]
            ret.Enabled = a IsNot Nothing
            If a Is Nothing Then Return ret
            ret.BgColor = a.BgColor
            ret.FgColor = a.FgColor
            ret.Font = a.Font
            ret.Text = a.Text
            Return ret
        End Operator
        Public Shared Widening Operator CType(ByVal a As [Structure]) As Settings_Label
            If Not a.Enabled Then Return Nothing
            Dim ret As New Settings_Label
            ret.BgColor = a.BgColor
            ret.FgColor = a.FgColor
            ret.Font = a.Font
            ret.Text = a.Text
            Return ret
        End Operator
    End Structure
End Class


