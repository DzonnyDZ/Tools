Imports System.Xml.Linq, Tools.CodeDomT, System.CodeDom, System.Xml.Schema.Extensions
Imports MBox = Tools.WindowsT.IndependentT.MessageBox
Imports System.Linq, Tools.LinqT, Tools.DevicesT.RawInputT
Imports System.Runtime.InteropServices
Imports Tools.CollectionsT.GenericT, Tools.ExtensionsT
Imports Microsoft.Win32.SafeHandles

Namespace DevicesT.RawInputT
    ''' <summary>Contains tests for <see cref="Tools.DevicesT.RawInputT"/></summary>
    Public Class frmRawInput
        Inherits Tools.WindowsT.FormsT.ExtendedForm
        Implements API.Messages.IWindowsMessagesProviderRef
        ''' <summary>Provides raw-input device events</summary>
        Private WithEvents provider As New RawInputEventProvider(Me)
        ''' <summary>List of registered devices / devices to register</summary>
        Private RegistrationList As New ListWithEvents(Of RawInputDeviceRegistration)
        ''' <summary>Raw logged input events</summary>
        Private EventList As New ListWithEvents(Of RawInputEventArgs)
        ''' <summary>Control keyboard hook</summary>
        Private WithEvents kbdHook As New Tools.DevicesT.LowLevelKeyboardHook(True)
        ''' <summary>Control mouse hook</summary>
        Private WithEvents mosHook As New Tools.DevicesT.LowLevelMouseHook(True)
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
            provider.RaiseMediaCenterRemoteEvents = True
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
                prgDevCap.SelectedObject = Nothing
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
                    'Capabilities
                    Try
                        prgDevCap.SelectedObject = .GetCapabilities
                    Catch ex As Exception
                        prgDevCap.SelectedObject = ex
                        MBox.Error_XT(ex, "GetCapabilities")
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

        ''' <summary>Processes Windows messages.</summary>
        ''' <param name="m">The Windows <see cref="Message"/> to process. </param>
        ''' <remarks>Note for inheritors: Always call base class's method <see cref="WndProc"/> unless you should block certain base class's functionality</remarks>
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
            RaiseEvent WndProcEvent(Me, m)
            MyBase.WndProc(m)
        End Sub
        ''' <summary />
        ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
        Protected Overrides Sub OnHandleDestroyed(ByVal e As System.EventArgs)
            MyBase.OnHandleDestroyed(e)
            provider.Dispose()
        End Sub

        ''' <summary>Raised when window message processing is required</summary>
        ''' <remarks>Caller must pass <paramref name="msg"/>.<see cref="Message.Result">Result</see> as result of message (unless it does own processing of the message and passes own result).
        ''' <para>When this event is raised from <see cref="Control.WndProc"/> than just raising it is enough (unless event rais method is implemented in non-standard way), because it if passed by reference.</para>
        ''' <para>Calle may require to <paramref name="sender"/> be caller and <paramref name="e"/>.<see cref="Message.HWnd">hWnd</see> to be <see cref="Handle"/>.</para></remarks>
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
            If cmdWhich.Text = "←" Then prgEvent.SelectedObject = Nothing
        End Sub

        Private Sub dgwEvents_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles dgwEvents.SelectionChanged, dgwEvents.Enter
            If dgwEvents.Focused Then
                If dgwEvents.SelectedRows.Count = 0 Then
                    prgEvent.SelectedObject = Nothing
                Else
                    prgEvent.SelectedObject = dgwEvents.SelectedRows(0).DataBoundItem
                End If
                cmdWhich.Text = "←"
            End If
        End Sub

        Private Sub dgwEvents_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgwEvents.RowsAdded
            For i As Integer = e.RowIndex To e.RowIndex + e.RowCount - 1
                dgwEvents.Rows(i).Cells(txcEvent.Index).Value = DirectCast(dgwEvents.Rows(i).DataBoundItem, RawInputEventArgs).ToString
                dgwEvents.Rows(i).Cells(txcTime.Index).Value = Now
            Next
            If chkEvAutoScroll.Checked AndAlso dgwEvents.FirstDisplayedScrollingRowIndex + dgwEvents.DisplayedRowCount(False) < dgwEvents.Rows.Count Then _
                dgwEvents.FirstDisplayedScrollingRowIndex = dgwEvents.Rows.Count - 1
        End Sub

        Private Sub cmdMediaCenterRemote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMediaCenterRemote.Click
            RegistrationList.Clear()
            RegistrationList.AddRange(RawInputDeviceRegistration.MediaCenterRemote.SetBackgroundEvents(BackgroundEvents.Background))
        End Sub

        Private Sub chkMediaCenter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMediaCenter.CheckedChanged
            provider.RaiseMediaCenterRemoteEvents = chkMediaCenter.Checked
        End Sub

        Private Sub chkHex_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHex.CheckedChanged
            nudUsage.Hexadecimal = chkHex.Checked
            nudUsagePage.Hexadecimal = chkHex.Checked
        End Sub
#Region "Non raw"
        Private Sub cmdClearNonRaw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearNonRaw.Click
            dgwNonRawEventLog.Rows.Clear()
            If cmdWhich.Text = "→" Then prgEvent.SelectedObject = Nothing
        End Sub

        Private Sub frmRawInput_ApplicationCommand(ByVal sender As Object, ByVal e As Tools.WindowsT.FormsT.ApplicationCommandEventArgs) Handles Me.ApplicationCommand
            If chkNonRawApp.Checked Then AddEventLog(e, "AppCommand", "{0} from {1}".f(e.Command, e.Device))
        End Sub

        Private Sub AddEventLog(ByVal e As EventArgs, ByVal Name$, ByVal Details$)
            dgwNonRawEventLog.Rows(dgwNonRawEventLog.Rows.Add(Now, Name, Details)).Tag = e
            If chkNonRawApp.Checked AndAlso dgwNonRawEventLog.FirstDisplayedScrollingRowIndex + dgwNonRawEventLog.DisplayedRowCount(False) < dgwNonRawEventLog.Rows.Count Then _
                dgwNonRawEventLog.FirstDisplayedScrollingRowIndex = dgwNonRawEventLog.Rows.Count - 1
        End Sub

        Private Sub kbdHook_KeyEvent(ByVal sender As Tools.DevicesT.LowLevelKeyboardHook, ByVal e As Tools.DevicesT.LowLevelKeyEventArgs) Handles kbdHook.KeyEvent
            If chkNonRawKyeboard.Checked Then AddEventLog(e, e.Action.ToString, e.Key.ToString)
        End Sub

        Private Sub mosHook_ButtonEvent(ByVal sender As Tools.DevicesT.LowLevelMouseHook, ByVal e As Tools.DevicesT.LowLevelMouseButtonEventArgs) Handles mosHook.ButtonEvent
            If chkNonRawMouse.Checked Then AddEventLog(e, If(e.MouseUp, "MouseUp", "MouseDown"), e.Button.ToString)
        End Sub

        Private Sub mosHook_MouseMove(ByVal sender As Tools.DevicesT.LowLevelMouseHook, ByVal e As Tools.DevicesT.LowLevelMouseEventArgs) Handles mosHook.MouseMove
            If chkNonRawMouse.Checked Then AddEventLog(e, "MouseMove", e.Location.ToString)
        End Sub

        Private Sub mosHook_Wheel(ByVal sender As Tools.DevicesT.LowLevelMouseHook, ByVal e As Tools.DevicesT.LowLevelMouseWheelEventArgs) Handles mosHook.Wheel
            If chkNonRawMouse.Checked Then AddEventLog(e, "MouseWheel", e.Delta)
        End Sub
        Private Sub dgwNonRawEventLog_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles dgwNonRawEventLog.SelectionChanged, dgwNonRawEventLog.Enter
            If dgwNonRawEventLog.Focused Then
                If dgwNonRawEventLog.SelectedRows.Count > 0 Then
                    prgEvent.SelectedObject = dgwNonRawEventLog.SelectedRows(0).Tag
                Else
                    prgEvent.SelectedObject = Nothing
                End If
                cmdWhich.Text = "→"
            End If
        End Sub
#End Region

       
        Private Sub cmdWhich_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWhich.Click
            If cmdWhich.Text = "←" Then
                If dgwNonRawEventLog.SelectedRows.Count > 0 Then
                    prgEvent.SelectedObject = dgwNonRawEventLog.SelectedRows(0).Tag
                Else
                    prgEvent.SelectedObject = Nothing
                End If
                cmdWhich.Text = "→"
            Else
                If dgwEvents.SelectedRows.Count = 0 Then
                    prgEvent.SelectedObject = Nothing
                Else
                    prgEvent.SelectedObject = dgwEvents.SelectedRows(0).DataBoundItem
                End If
                cmdWhich.Text = "←"
            End If
        End Sub

        
    End Class
End Namespace