Imports iMsg = Tools.WindowsT.IndependentT.MessageBox
Imports System.Reflection
Imports Tools.ReflectionT
Imports Tools.TestsT

Namespace TestsT
    Public Class frmStaticProperiesTest
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon
            For Each asm In GetType(frmStaticProperiesTest).Assembly.GetReferencedAssemblies
                obwSelectType.Objects.Add(Reflection.Assembly.Load(asm))
            Next
            tscCultures.ComboBox.DisplayMember = "DisplayName"
            tscCultures.Items.Add(Globalization.CultureInfo.InvariantCulture)
            Dim selc As Globalization.CultureInfo = Nothing
            For Each cul In Globalization.CultureInfo.GetCultures(Globalization.CultureTypes.NeutralCultures)
                tscCultures.Items.Add(cul)
                If cul.Equals(Threading.Thread.CurrentThread.CurrentUICulture) Then selc = cul
            Next
            If selc IsNot Nothing Then tscCultures.SelectedItem = selc
        End Sub
        Public Shared Sub Test()
            Dim frm As New frmStaticProperiesTest
            frm.ShowDialog()
        End Sub


        Private Sub tsbOpenAssembly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOpenAssembly.Click
            If ofdOpenAssembly.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    obwSelectType.Objects.Add(Reflection.Assembly.LoadFile(ofdOpenAssembly.FileName))
                Catch ex As Exception
                    iMsg.Error_X(ex)
                End Try
            End If
        End Sub
        Private WithEvents Tester As Tools.TestsT.StaticPropertiesTest
        Private Sub cmdTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTest.Click
            If TypeOf obwSelectType.SelectedItem Is Assembly Then
                Tester = New StaticPropertiesTest(DirectCast(obwSelectType.SelectedItem, Assembly))
            ElseIf TypeOf obwSelectType.SelectedItem Is [Module] Then
                Tester = New StaticPropertiesTest(DirectCast(obwSelectType.SelectedItem, [Module]))
            ElseIf TypeOf obwSelectType.SelectedItem Is NamespaceInfo Then
                Tester = New StaticPropertiesTest(DirectCast(obwSelectType.SelectedItem, NamespaceInfo))
            ElseIf TypeOf obwSelectType.SelectedItem Is Type Then
                Tester = New StaticPropertiesTest(DirectCast(obwSelectType.SelectedItem, Type))
            Else
                MsgBox("Select Assembly, Module, Namespace or Type.", MsgBoxStyle.Exclamation, "Wrong element selected")
                Exit Sub
            End If
            Select Case True
                Case optTPublic.Checked : Tester.TypeFlags = StaticPropertiesTest.TypeBindingAttributes.AllPublic
                Case optTFriend.Checked : Tester.TypeFlags = StaticPropertiesTest.TypeBindingAttributes.AllPublic Or StaticPropertiesTest.TypeBindingAttributes.AllAssembly
                Case optTAll.Checked : Tester.TypeFlags = StaticPropertiesTest.TypeBindingAttributes.none
            End Select
            Tester.PropertyBindingFlags = 0
            If chkPPublic.Checked Then Tester.PropertyBindingFlags = Tester.PropertyBindingFlags Or BindingFlags.Public
            If chkPPrivate.Checked Then Tester.PropertyBindingFlags = Tester.PropertyBindingFlags Or BindingFlags.NonPublic
            lvwResults.Items.Clear()
            Tester.RunTest()
            ApplyStat()
        End Sub

        Private Sub Tester_Error(ByVal sender As Object, ByVal e As StaticPropertiesTest.TestErrorEventArgs) Handles Tester.Error
            Dim itm = lvwResults.Items.Add(e.Property.Name)
            itm.Tag = e.Property
            itm.SubItems.Add(e.Property.DeclaringType.FullName)
            itm.SubItems.Add(e.Stage.ToString)
            If e.Exception IsNot Nothing Then
                itm.SubItems.Add(e.Exception.GetType.Name)
                itm.SubItems.Add(e.Exception.Message).Tag = e.Exception
            End If
            Select Case e.Stage
                Case StaticPropertiesTest.TestStages.GetterBeingInvoked : itm.BackColor = lblSGetterBeingInvokedI.BackColor
                Case StaticPropertiesTest.TestStages.InvokeGetter : itm.BackColor = lblSInvokeGetterI.BackColor
                Case StaticPropertiesTest.TestStages.GetGetMethod : itm.BackColor = lblSGetGetMethodI.BackColor
                Case StaticPropertiesTest.TestStages.IsIndexed : itm.BackColor = lblSIsIndexedI.BackColor
                Case StaticPropertiesTest.TestStages.HasGetMethod : itm.BackColor = lblSHasGetMethodI.BackColor
                Case StaticPropertiesTest.TestStages.ValueIsNull : itm.BackColor = lblSValueIsNullI.BackColor
            End Select
            ApplyStat()
        End Sub

        Private Sub Tester_Success(ByVal sender As Object, ByVal e As StaticPropertiesTest.TestSuccessEventArgs) Handles Tester.Success
            If Not chkSuccessfull.Checked Then ApplyStat() : Exit Sub
            Dim itm = lvwResults.Items.Add(e.Property.Name)
            itm.Tag = e.Property
            itm.SubItems.Add(e.Property.DeclaringType.FullName).Tag = e.Value
            itm.BackColor = Color.Lime
            ApplyStat()
        End Sub

        Private Sub tscCultures_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tscCultures.SelectedIndexChanged
            Threading.Thread.CurrentThread.CurrentUICulture = tscCultures.SelectedItem
        End Sub
        ''' <summary>Shows statistics</summary>
        Private Sub ApplyStat()
            lblSTested.Text = Tester.PropertiesTestedCount
            lblSSuccess.Text = Tester.SuccessCount
            lblSNonSuccess.Text = Tester.ErrorsCount
            lblSGeterBeingInvoked.Text = Tester.ErrorsCount(StaticPropertiesTest.TestStages.GetterBeingInvoked)
            lblSGetGetMethod.Text = Tester.ErrorsCount(StaticPropertiesTest.TestStages.GetGetMethod)
            lblSHasGetMethod.Text = Tester.ErrorsCount(StaticPropertiesTest.TestStages.HasGetMethod)
            lblSInvokeGetter.Text = Tester.ErrorsCount(StaticPropertiesTest.TestStages.InvokeGetter)
            lblSIsIndexed.Text = Tester.ErrorsCount(StaticPropertiesTest.TestStages.IsIndexed)
            lblSValueIsNull.Text = Tester.ErrorsCount(StaticPropertiesTest.TestStages.ValueIsNull)
        End Sub

        Private Sub lvwResults_ItemActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwResults.ItemActivate
            obwSelectType.SelectedItem = lvwResults.SelectedItems(0).Tag
            If lvwResults.SelectedItems(0).SubItems.Count > cohExceptionMessage.Index Then
                Dim msg As New System.Text.StringBuilder
                Dim ex As Exception = lvwResults.SelectedItems(0).SubItems(cohExceptionMessage.Index).Tag
                If ex Is Nothing Then Exit Sub
                Do While ex IsNot Nothing
                    If msg.Length <> 0 Then msg.AppendLine("=================================================================================")
                    msg.AppendLine(ex.Message & ":")
                    msg.AppendLine(ex.StackTrace)
                    ex = ex.InnerException
                Loop
                MsgBox(msg.ToString, MsgBoxStyle.Critical, lvwResults.SelectedItems(0).SubItems(cohExceptionMessage.Index).Tag.GetType.FullName)
            End If
        End Sub

        Private Sub lvwResults_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwResults.SelectedIndexChanged
            If lvwResults.SelectedItems.Count = 0 Then
                prgValueProperties.SelectedObject = 0
                lblType.Text = ""
                lblToString.Text = ""
            Else
                prgValueProperties.SelectedObject = lvwResults.SelectedItems(0).SubItems(1).Tag
                If prgValueProperties.SelectedObject IsNot Nothing Then
                    lblType.Text = prgValueProperties.SelectedObject.GetType.FullName
                    lblToString.Text = prgValueProperties.SelectedObject.ToString
                End If
                End If
        End Sub
    End Class
End Namespace