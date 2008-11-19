Imports System.Xml.Linq, Tools.CodeDomT, System.CodeDom, System.Xml.Schema.Extensions
Imports MBox = Tools.WindowsT.IndependentT.MessageBox
Imports System.Linq, Tools.LinqT, Tools.DevicesT.RawInputT
Imports System.Runtime.InteropServices
Imports Tools.CollectionsT.GenericT

Namespace DevicesT.RawInputT
    ''' <summary>Contains tests for <see cref="Tools.DevicesT.RawInputT"/></summary>
    Public Class frmRawInput
        Implements API.Messages.IWindowsMessagesProviderRef
        ''' <summary>Provides raw-input device events</summary>
        Private WithEvents provider As New RawInputEventProvider(Me)
        ''' <summary>List of registered devices / devices to register</summary>
        Private RegistrationList As New ListWithEvents(Of RawInputDeviceRegistration)
        ''' <summary>Raw logged input events</summary>
        Private EventList As New ListWithEvents(Of RawInputEventArgs)
        ''' <summary>Performs test</summary>
        Public Shared Sub Test()
            Dim inst As New frmRawInput
            inst.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon

            For Each value As UsagePages In [Enum].GetValues(GetType(UsagePages))
                Dim Node As TreeNode = tvwHid.Nodes.Add(value.GetName)
                Node.Tag = value
                Dim Usages = value.GetUsages
                For Each Usage As [Enum] In Usages
                    Dim SubNode As TreeNode = Node.Nodes.Add(Usage.GetName)
                    Dim UsageType As UsageTypes = RawInputExtensions.GetUsageType(Usage)
                    Dim UsageTypeStr$
                    If UsageType = 0 Then UsageTypeStr = "" Else UsageTypeStr = String.Format(" {0:F}", UsageType)
                    SubNode.Text &= UsageTypeStr
                    SubNode.Tag = Usage
                Next
            Next
            dgwRegistration.DataSource = RegistrationList
            dgwEvents.DataSource = EventList
        End Sub


        Private Sub cmdGetRawInputDeviceList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetRawInputDeviceList.Click
            Dim Devices As InputDevice()
            Try
                Devices = InputDevice.GetAllDevices
            Catch ex As Exception
                MBox.Error_X(ex)
                Exit Sub
            End Try
            lstDevices.Items.Clear()
            For Each Device In Devices
                lstDevices.Items.Add(Device)
            Next
        End Sub

        Private Sub lstDevices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDevices.SelectedIndexChanged
            If lstDevices.SelectedItem Is Nothing Then
                lblDeviceName.Text = ""
                prgDeviceInfo.SelectedObject = Nothing
                prgName.SelectedObject = Nothing
                lblDeviceDescription.Text = ""
            Else
                With DirectCast(lstDevices.SelectedItem, InputDevice)
                    'Name
                    Try
                        lblDeviceName.Text = .GetDeviceNameString
                    Catch ex As Exception
                        lblDeviceName.Text = ""
                        MBox.Error_XT(ex, "GetDeviceName")
                    End Try
                    'Info
                    Try
                        Dim di = .GetDeviceInfo()
                        nudUsage.Value = di.Usage
                        nudUsagePage.Value = di.UsagePage
                        prgDeviceInfo.SelectedObject = di
                    Catch ex As Exception
                        prgDeviceInfo.SelectedObject = ex
                        MBox.Error_XT(ex, "GetDeviceInfo")
                    End Try
                    'Name
                    Try
                        prgName.SelectedObject = .GetDeviceName
                    Catch ex As Exception
                        prgName.SelectedObject = ex
                        MBox.Error_XT(ex, "GetDeviceInfo")
                    End Try
                    'Desc
                    Try
                        lblDeviceDescription.Text = .GetDeviceName.GetDeviceDescription
                    Catch ex As Exception
                        lblDeviceDescription.Text = ""
                        MBox.Error_XT(ex, "GetDeviceInfo")
                    End Try
                End With
            End If
        End Sub

        Private Sub tvwHid_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwHid.AfterSelect
            If TypeOf e.Node.Tag Is [Enum] AndAlso Not TypeOf e.Node.Tag Is UsagePages Then
                nudUsagePage.Value = e.Node.Parent.Tag
                nudUsage.Value = e.Node.Tag
            End If
        End Sub

        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            RaiseEvent WndProcEvent(Me, m)
            MyBase.WndProc(m)
        End Sub

        Private Event WndProcEvent(ByVal sender As Object, ByRef msg As System.Windows.Forms.Message) Implements API.Messages.IWindowsMessagesProviderRef.WndProc

        Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
            RegistrationList.Clear()
        End Sub

        Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
            RegistrationList.Add(New RawInputDeviceRegistration(CType(nudUsagePage.Value, UsagePages), CInt(nudUsage.Value)))
        End Sub

        Private Sub cmdLoadRegistered_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadRegistered.Click
            RegistrationList.Clear()
            Try
                RegistrationList.AddRange(provider.GetRegisteredDevices(If(MsgBox("Get all devices?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Get registered devices") = MsgBoxResult.Yes, True, False)))
            Catch ex As Exception
                MBox.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
            Try
                provider.Register(RegistrationList)
            Catch ex As Exception
                MBox.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdUnregister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnregister.Click
            Try
                provider.UnRegister(RegistrationList)
            Catch ex As Exception
                MBox.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdUnregisterAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnregisterAll.Click
            Try
                provider.UnregisterAll()
            Catch ex As Exception
                MBox.Error_X(ex)
            End Try
        End Sub

        Private Sub provider_Input(ByVal sender As Object, ByVal e As RawInputEventArgs) Handles provider.Input
            If TypeOf e Is RawKeyboardEventArgs AndAlso Not chkKeyboard.Checked Then Exit Sub
            If TypeOf e Is RawMouseEventArgs AndAlso Not chkMouse.Checked Then Exit Sub
            If TypeOf e Is RawHidEventArgs AndAlso Not chkHID.Checked Then Exit Sub
            EventList.Add(e)
        End Sub

        Private Sub cmdClearEventLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearEventLog.Click
            EventList.Clear()
        End Sub

        Private Sub dgwEvents_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgwEvents.SelectionChanged
            If dgwEvents.SelectedRows.Count = 0 Then
                prgEvent.SelectedObject = Nothing
            Else
                prgEvent.SelectedObject = dgwEvents.SelectedRows(0).DataBoundItem
            End If
        End Sub

        Private Sub dgwEvents_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgwEvents.RowsAdded
            For i As Integer = e.RowIndex To e.RowIndex + e.RowCount - 1
                dgwEvents.Rows(i).Cells(txcEvent.Index).Value = DirectCast(dgwEvents.Rows(i).DataBoundItem, RawInputEventArgs).ToString
            Next
        End Sub
    End Class
End Namespace