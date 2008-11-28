Imports System.ComponentModel
Imports System.Windows.Forms
Namespace DevicesT.JoystickT
    Public Class JoyAxe
        <DefaultValue(True)> _
        Public Overrides Property AutoSize() As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(ByVal value As Boolean)
                MyBase.AutoSize = value
            End Set
        End Property
        <DefaultValue(GetType(AutoSizeMode), "GrowAndShrink")> _
        Shadows Property AutoSizeMode() As AutoSizeMode
            Get
                Return MyBase.AutoSizeMode
            End Get
            Set(ByVal value As AutoSizeMode)
                MyBase.AutoSizeMode = value
            End Set
        End Property
        <DefaultValue("X"), Browsable(True)> _
        Public Overrides Property Text() As String
            Get
                Return lblAxe.Text
            End Get
            Set(ByVal value As String)
                lblAxe.Text = value
            End Set
        End Property
        <DefaultValue(0I)> _
        Public Property Minimum() As Integer
            Get
                Return pgbProgress.Minimum
            End Get
            Set(ByVal value As Integer)
                pgbProgress.Minimum = value
                lblMin.Text = value
            End Set
        End Property
        <DefaultValue(100I)> _
        Public Property Maximum() As Integer
            Get
                Return pgbProgress.Maximum
            End Get
            Set(ByVal value As Integer)
                pgbProgress.Maximum = value
                lblMax.Text = value
            End Set
        End Property
        <DefaultValue(0I)> _
        Public Property Value() As Integer
            Get
                Return pgbProgress.Value
            End Get
            Set(ByVal value As Integer)
                pgbProgress.Value = value
            End Set
        End Property
    End Class

End Namespace