Imports System.Xml.Linq, Tools.CodeDomT, System.CodeDom, System.Xml.Schema.Extensions
Imports MBox = Tools.WindowsT.IndependentT.MessageBox
Imports System.Linq, Tools.LinqT, Tools.DevicesT.RawInputT
Imports System.Runtime.InteropServices

Namespace DevicesT.RawInputT
    ''' <summary>Contains tests for <see cref="Tools.DevicesT.RawInputT"/></summary>
    Public Class frmRawInput
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
                    SubNode.Tag = UsageType
                Next
            Next
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
                        prgDeviceInfo.SelectedObject = .GetDeviceInfo()
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
    End Class
End Namespace