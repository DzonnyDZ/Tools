Imports System.Reflection, Tools.TypeTools
Imports System.ComponentModel

Partial Friend Class frmProperties
    Private Shared ExifProperties As IEnumerable(Of PropertyInfoDisplay)
    Private Shared IPTCProperties As IEnumerable(Of PropertyInfoDisplay)
    Private Shared SysProperties As IEnumerable(Of PropertyInfoDisplay)
    Private Shared Instance As frmProperties
    Public Shared ReadOnly Property IsInstance() As Boolean
        Get
            Return Instance IsNot Nothing
        End Get
    End Property

    Public Shared Function ShowInstance(ByVal txt As TextBox) As frmProperties
        If Instance Is Nothing Then Instance = New frmProperties()
        Instance.txt = txt
        If Instance.Visible Then
            Instance.BringToFront()
        Else
            Instance.Show(txt.FindForm)
            AddHandler txt.FindForm.FormClosed, AddressOf Instance.OwnerClose
        End If
        Return Instance
    End Function

    Private Sub OwnerClose(ByVal sender As Form, ByVal e As EventArgs)
        Me.Close()
        Instance = Nothing
    End Sub


    Private Sub New()
        InitializeComponent()
        If ExifProperties Is Nothing Then ExifProperties = PropertyInfoDisplay.GetProperties(GetType(Tools.MetadataT.ExifT.IfdExif))
        If IPTCProperties Is Nothing Then IPTCProperties = PropertyInfoDisplay.GetProperties(GetType(Tools.MetadataT.IptcT.Iptc))
        If SysProperties Is Nothing Then SysProperties = PropertyInfoDisplay.GetProperties(GetType(SysInfo))
        lstExif.Items.AddRange(ExifProperties.ToArray)
        lstIPTC.Items.AddRange(IPTCProperties.ToArray)
        lstOther.Items.AddRange(SysProperties.ToArray)
    End Sub

    Private Sub lst_Enter(ByVal sender As ListBox, ByVal e As System.EventArgs) _
        Handles lstOther.Enter, lstIPTC.Enter, lstExif.Enter
        DisplayInfo(sender)
    End Sub

    Private Sub DisplayInfo(ByVal sender As ListBox)
        If sender.SelectedIndex >= 0 Then
            txtInfo.Text = CType(sender.SelectedItem, PropertyInfoDisplay).Description
            fraInfo.Text = String.Format("Category: {0}, Type: {1}", DirectCast(sender.SelectedItem, PropertyInfoDisplay).Category, DirectCast(sender.SelectedItem, PropertyInfoDisplay).Type.Name)
        Else
            fraInfo.Text = "Info"
            txtInfo.Text = ""
        End If
    End Sub

    Private Sub lst_SelectedIndexChanged(ByVal sender As ListBox, ByVal e As System.EventArgs) _
        Handles lstOther.SelectedIndexChanged, lstIPTC.SelectedIndexChanged, lstExif.SelectedIndexChanged
        If sender.Focused Then DisplayInfo(sender)
    End Sub

    Private txt As TextBox
    Private Sub lst_MouseDoubleClick(ByVal sender As ListBox, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles lstOther.MouseDoubleClick, lstIPTC.MouseDoubleClick, lstExif.MouseDoubleClick
        Dim Namespace$
        If sender Is lstExif Then : Namespace$ = "Exif"
        ElseIf sender Is lstIPTC Then : [Namespace] = "IPTC"
        Else : [Namespace] = "Sys"
        End If
        txt.SelectedText = String.Format("<{0}:{1}:{2}>", [Namespace], CType(sender.SelectedItem, PropertyInfoDisplay).Name, "{0}")
    End Sub

    Private Sub frmProperties_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Instance Is Me Then Instance = Nothing
    End Sub
End Class

Friend Class PropertyInfoDisplay
    Public ReadOnly [Property] As PropertyInfo
    Public Sub New(ByVal [Property] As PropertyInfo)
        Me.Property = [Property]
    End Sub
    Public Overrides Function ToString() As String
        Return DisplayName
    End Function
    Public ReadOnly Property DisplayName$()
        Get
            Dim a = [Property].GetAttribute(Of DisplayNameAttribute)(True)
            Return If(a IsNot Nothing, a.DisplayName, Me.Property.Name)
        End Get
    End Property
    Public ReadOnly Property Name$()
        Get
            Return Me.Property.Name
        End Get
    End Property
    Public ReadOnly Property Type() As Type
        Get
            Return Me.Property.PropertyType
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Dim a = Me.Property.GetAttribute(Of DescriptionAttribute)(True)
            Return If(a Is Nothing, "", a.Description)
        End Get
    End Property
    Public ReadOnly Property Browsable() As Boolean
        Get
            Dim a = Me.Property.GetAttribute(Of BrowsableAttribute)(True)
            Return a Is Nothing OrElse a.Browsable
        End Get
    End Property
    Public ReadOnly Property Category$()
        Get
            Dim a = Me.Property.GetAttribute(Of CategoryAttribute)(True)
            Return If(a Is Nothing, "Misc", a.Category)
        End Get
    End Property
    Public Shared Function GetProperties(ByVal T As Type) As IEnumerable(Of PropertyInfoDisplay)
        Return From pinfo In T.GetProperties _
            Where pinfo.CanRead AndAlso Not pinfo.GetGetMethod.IsStatic AndAlso If(pinfo.GetGetMethod.GetParameters, New ParameterInfo() {}).Length = 0 _
            Select it = New PropertyInfoDisplay(pinfo) _
            Order By it.Category, it.DisplayName Ascending _
            Where it.Browsable
    End Function
End Class