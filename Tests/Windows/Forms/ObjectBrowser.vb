Imports System.Reflection, Tools.WindowsT.FormsT, Tools.ReflectionT
#If Config <= Nightly Then
Namespace WindowsT.FormsT
    ''' <summary>Tests <see cref="Tools.WindowsT.FormsT.ObjectBrowser"/></summary>
    Public Class frmObjectBrowser
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmObjectBrowser
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon

            obTest.Objects.Add(GetType(frmObjectBrowser).Assembly)
            obTest.Objects.Add(GetType(ObjectBrowser).Assembly)
            obTest.Objects.Add(GetType(Long).Assembly)


            Dim a As Object() = New Object() {Nothing}
            Dim b As String() = New String() {"a"}
            a = b

            Dim values = [Enum].GetValues(GetType(Objects))
            Dim ObjArr(values.Length - 1) As Object
            Array.Copy(values, ObjArr, values.Length)
            'Dim ObjArr As Object() = TryCast(Objects, Object())

            cmbType.Items.AddRange(ObjArr)
            cmbAccess.Items.AddRange(New Object() {ObjectModifiers.Public, ObjectModifiers.Friend, ObjectModifiers.Protected, ObjectModifiers.ProtectedFriend, ObjectModifiers.FriendProtected, ObjectModifiers.Private, ObjectModifiers.None})
            cmbType.SelectedIndex = 0
            cmbAccess.SelectedIndex = 0
        End Sub

        Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
            Dim ObjectType As Objects = cmbType.SelectedItem
            Dim ObjectModifiers As ObjectModifiers = Me.cmbAccess.SelectedItem
            If chkSealed.Checked Then ObjectModifiers = ObjectModifiers Or CodeImages.ObjectModifiers.Sealed
            If chkShortcut.Checked Then ObjectModifiers = ObjectModifiers Or CodeImages.ObjectModifiers.Shortcut
            If chkStatic.Checked Then ObjectModifiers = ObjectModifiers Or CodeImages.ObjectModifiers.Static
            Dim img = GetImage(ObjectType, ObjectModifiers)
            picBig.Image = img
            picSmall.Image = img
        End Sub
    End Class
End Namespace
#End If