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
            AddHandler ReflectionT.ImageRequested, AddressOf CodeImages_ImageRequested
        End Sub

        Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
            Dim ObjectType As Objects = cmbType.SelectedItem
            Dim ObjectModifiers As ObjectModifiers = Me.cmbAccess.SelectedItem
            If chkSealed.Checked Then ObjectModifiers = ObjectModifiers Or CodeImages.ObjectModifiers.Sealed
            If chkShortcut.Checked Then ObjectModifiers = ObjectModifiers Or CodeImages.ObjectModifiers.Shortcut
            If chkStatic.Checked Then ObjectModifiers = ObjectModifiers Or CodeImages.ObjectModifiers.Static
            If chkExtension.CheckAlign Then ObjectModifiers = ObjectModifiers Or CodeImages.ObjectModifiers.Extension
            Dim img = GetImage(ObjectType, ObjectModifiers)
            picBig.Image = img
            picSmall.Image = img
            txtKey.Text = LastCode
        End Sub
        ''' <summary>Kye of last image caught by <see cref="CodeImages_ImageRequested"/></summary>
        Private LastCode As String
        ''' <summary>handles the <see cref="ReflectionT.ImageRequested"/> event</summary>
        ''' <param name="Image">Image to be returned</param>
        ''' <param name="ObjectType">Type of object for image</param>
        ''' <param name="Modifiers">Object modifiers for  image</param>
        Private Sub CodeImages_ImageRequested(ByVal Image As Image, ByVal ObjectType As Objects, ByVal Modifiers As ObjectModifiers)
            LastCode = String.Format("{0:d}_{0:d}", ObjectType, Modifiers)
        End Sub

        Private Sub frmObjectBrowser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim frm As New FloatingPropertyGrid(obTest)
            frm.Show(Me)
        End Sub
    End Class
End Namespace
#End If