Imports System.ComponentModel
Imports Microsoft.DirectX.DirectInput
Imports System.Windows.Forms

Namespace DevicesT.JoystickT
    ''' <summary>Křeslo napojené na joystick</summary>
    Public Class JoyChair : Inherits UserControl : Implements IChair
        Private AxesControls As JoyAxe()
        Private ButtonsControls As ToolStripButton()
        Private POVsControls As JoyAxe()

#Region "IKřeslo"
        Public ReadOnly Property AxeCap() As AxeCap() Implements IChair.AxeCap
            Get
                If Joystick IsNot Nothing Then
                    Dim ret(SetupAxes.Count - 1 + SetupPOVs.Count) As AxeCap
                    For i As Integer = 0 To SetupAxes.Count - 1
                        ret(i) = New AxeCap(AxesControls(i).Minimum, AxesControls(i).Maximum, False)
                    Next i
                    For i As Integer = 0 To SetupPOVs.Count - 1
                        ret(i + SetupAxes.Count) = New AxeCap(-1, 36000, True)
                    Next i
                    Return ret
                Else
                    Return New AxeCap() {}
                End If
            End Get
        End Property

        ''' <summary>Nastane ihned po změně polohy osy</summary>    
        Public Event AxeChanged(ByVal sender As IChair, ByVal e As AxeEventArgs) Implements IChair.AxeChanged

        Protected Overridable Sub OnAxeChanged(ByVal e As AxeEventArgs)
            RaiseEvent AxeChanged(Me, e)
        End Sub

        Public ReadOnly Property AxesCount() As Byte Implements IChair.AxesCount
            Get
                If Joystick Is Nothing Then Return 0 Else Return SetupAxes.Count + SetupPOVs.Count
            End Get
        End Property

        ''' <summary>Nastane ihned po stlačení tlačítka</summary>    
        Public Event ButtonDown(ByVal sender As IChair, ByVal e As ButtonEventArgs) Implements IChair.ButtonDown
        Protected Overridable Sub OnButtonDown(ByVal e As ButtonEventArgs)
            RaiseEvent ButtonDown(Me, e)
        End Sub


        Public ReadOnly Property ButtonsCount() As Byte Implements IChair.ButtonsCount
            Get
                If Joystick Is Nothing Then Return 0 Else Return Joystick.Caps.NumberButtons
            End Get
        End Property

        Public ReadOnly Property ButtonStatus(ByVal index As Byte) As ButtonAction Implements IChair.ButtonStatus
            Get
                If Joystick Is Nothing Then Throw New IndexOutOfRangeException
                Return Tools.VisualBasicT.iif(OldState.Buttons(index), ButtonAction.Down, ButtonAction.Up)
            End Get
        End Property

        ''' <summary>Nastane ihned po uvolnění tlačítka</summary>    
        Public Event ButtonUp(ByVal sender As IChair, ByVal e As ButtonEventArgs) Implements IChair.ButtonUp
        Protected Overridable Sub OnButtonUp(ByVal e As ButtonEventArgs)
            RaiseEvent ButtonUp(Me, e)
        End Sub

        ''' <summary>ID křesla v rámci serveru</summary>    
        Private ReadOnly Property ID1() As ULong Implements IChair.ID
            Get
                Return ID
            End Get
        End Property

        Public ReadOnly Property MultiUse() As Boolean Implements IChair.MultiUse
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property Position(ByVal index As Byte) As Integer Implements IChair.Position
            Get
                If Joystick Is Nothing Then Throw New IndexOutOfRangeException
                If index < SetupAxes.Count Then
                    Return OldState.Axes(index)
                Else
                    Return OldState.POVs(index - SetupAxes.Count)
                End If
            End Get
        End Property


#End Region
#Region "Joy"
        ''' <summary>Loads list of joysticks</summary>
        Private Sub LoadJoysticks()
            tscJoy.Items.Clear()
            tscJoy.ComboBox.DisplayMember = "InstanceName"
            For Each device As DeviceInstance In Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly)
                tscJoy.Items.Add(device)
            Next device
            If tscJoy.Items.Count > 0 Then tscJoy.SelectedIndex = 0
        End Sub
        ''' <summary>Identiti of currently selected joystick</summary>
        Private ReadOnly Property JoystickInstance() As Nullable(Of DeviceInstance)
            Get
                If Me.InvokeRequired Then
                    Dim SelectedIndex As New dGetSelectedIndex(AddressOf GetSelectedIndex)
                    Dim SelectedItem As New dGetSelectedItem(AddressOf GetSelectedItem)
                    If CInt(Me.Invoke(SelectedIndex)) >= 0 Then Return Me.Invoke([SelectedItem]) Else Return Nothing
                Else
                    If tscJoy.SelectedIndex >= 0 Then Return tscJoy.SelectedItem Else Return Nothing
                End If
            End Get
        End Property

        Private Delegate Function dGetSelectedIndex() As Integer
        Private Function GetSelectedIndex() As Integer
            Return tscJoy.SelectedIndex
        End Function
        Private Delegate Function dGetSelectedItem() As Object
        Private Function GetSelectedItem() As Object
            Return tscJoy.SelectedIndex
        End Function

        ''' <summary>Initializes Joystick</summary>
        Private ReadOnly Property NewJoystick() As Device
            Get
                Dim Joy As Nullable(Of DeviceInstance) = JoystickInstance
                If Not Joy.HasValue Then
                    tmr10ms.Enabled = False
                    Return Nothing
                Else
                    Dim j As New Device(Joy.Value.InstanceGuid)
                    If j Is Nothing Then
                        MsgBox(My.Resources.CannotUseSelectedJoystick, MsgBoxStyle.Critical, My.Resources.Error_)
                        tscJoy.SelectedIndex = -1
                        tmr10ms.Enabled = False
                        Return Nothing
                    End If
                    j.SetDataFormat(DeviceDataFormat.Joystick)
                    SetupAxes.Clear()
                    SetupPOVs.Clear()
                    Dim AxC As Integer = 0
                    Dim PovC As Integer = 0
                    For Each doi As DeviceObjectInstance In j.Objects
                        If AxC < j.Caps.NumberAxes AndAlso (doi.ObjectId And DeviceObjectTypeFlags.Axis) <> 0 Then
                            Try
                                j.Properties.SetRange(ParameterHow.ById, doi.ObjectId, New InputRange(0, 100))
                                SetupAxes.Add(doi)
                                AxC += 1
                            Catch ex As UnsupportedException
                                Try
                                    j.Properties.GetRange(ParameterHow.ById, doi.ObjectId)
                                    SetupAxes.Add(doi)
                                    AxC += 1
                                Catch
                                End Try
                            Catch ex As Exception
                            End Try
                        End If
                        If PovC < j.Caps.NumberPointOfViews AndAlso (doi.ObjectId And DeviceObjectTypeFlags.Pov) <> 0 Then
                            SetupPOVs.Add(doi)
                            PovC += 1
                        End If
                    Next doi
                    j.Properties.AxisModeAbsolute = True
                    Try
                        j.SetCooperativeLevel(Me.ParentForm, CooperativeLevelFlags.NonExclusive Or CooperativeLevelFlags.Background)
                    Catch ex As InputException
                        j.SetCooperativeLevel(Me.ParentForm, CooperativeLevelFlags.Exclusive Or CooperativeLevelFlags.Background)
                    End Try
                    j.Acquire()
                    If Not tmr10ms.Enabled Then tmr10ms.Enabled = True
                    Return j
                End If
            End Get
        End Property
        ''' <summary>Successfully seted up axes</summary>
        Private SetupAxes As New List(Of DeviceObjectInstance)
        ''' <summary>successfully setted up POVs</summary>
        Private SetupPOVs As New List(Of DeviceObjectInstance)
        ''' <summary>State of joystick</summary>
        Private Structure JoyInfo
            ''' <summary>Buttons</summary>
            Public Buttons As BitArray
            ''' <summary>Axes</summary>
            Public Axes As Integer()
            ''' <summary>Points of view</summary>
            Public POVs As Integer()
        End Structure
        ''' <summary>Show current state of joystick</summary>
        Private Sub ShowState(ByVal State As JoyInfo)
            For i As Integer = 0 To State.Buttons.Length - 1
                ButtonsControls(i).Checked = State.Buttons(i)
            Next i
            For i As Integer = 0 To State.Axes.Length - 1
                AxesControls(i).Value = State.Axes(i)
            Next i
            For i As Integer = 0 To State.POVs.Length - 1
                If State.POVs(i) >= 0 Then
                    POVsControls(i).Value = State.POVs(i)
                    POVsControls(i).pgbProgress.Style = ProgressBarStyle.Continuous
                    POVsControls(i).pgbProgress.AutoText = True
                Else
                    POVsControls(i).Value = 0
                    POVsControls(i).pgbProgress.AutoText = False
                    POVsControls(i).pgbProgress.Text = -1
                End If
            Next i
        End Sub
        Private Delegate Sub dSetSelectedIndex(ByVal index As Integer)
        Private Sub SetSelectedIndex(ByVal index As Integer)
            tscJoy.SelectedIndex = index
        End Sub
        ''' <summary>Gets state of joystick</summary>
        Private Function Acquire() As JoyInfo
            Dim js As JoystickState
            Try
                Joystick.Poll()
                js = Joystick.CurrentJoystickState
            Catch ex As InputLostException
                Dim SelectedIndex As New dSetSelectedIndex(AddressOf SetSelectedIndex)
                Me.Invoke(SelectedIndex, -1)
                Return OldState
            End Try

            Dim ret As New JoyInfo
            Dim But As Byte() = js.GetButtons
            ret.Buttons = New BitArray(Joystick.Caps.NumberButtons)
            For i As Integer = 0 To Joystick.Caps.NumberButtons - 1
                ret.Buttons(i) = But(i) <> 0
            Next i
            Dim POV As Integer() = js.GetPointOfView
            ReDim ret.POVs(SetupPOVs.Count - 1)
            For i As Integer = 0 To Math.Min(POV.Length - 1, SetupPOVs.Count - 1)
                ret.POVs(i) = Tools.VisualBasicT.iif(POV(i) >= -1 AndAlso POV(i) <= 36000, POV(i), -1)
            Next i
            ReDim ret.Axes(SetupAxes.Count - 1)
            Dim slider As Integer = 0
            For i As Integer = 0 To SetupAxes.Count - 1
                'ret.Axes(i) = AxArr(i)
                Select Case SetupAxes(i).ObjectType
                    Case ObjectTypeGuid.XAxis
                        Select Case CType(SetupAxes(i).Flags, ObjectInstanceFlags) And ObjectInstanceFlags.Mask
                            Case ObjectInstanceFlags.Force
                                ret.Axes(i) = js.FX
                            Case ObjectInstanceFlags.Acceleration
                                ret.Axes(i) = js.AX
                            Case ObjectInstanceFlags.Velocity
                                ret.Axes(i) = js.VX
                            Case ObjectInstanceFlags.Position
                                ret.Axes(i) = js.X
                        End Select
                    Case ObjectTypeGuid.YAxis
                        Select Case CType(SetupAxes(i).Flags, ObjectInstanceFlags) And ObjectInstanceFlags.Mask
                            Case ObjectInstanceFlags.Force
                                ret.Axes(i) = js.FY
                            Case ObjectInstanceFlags.Acceleration
                                ret.Axes(i) = js.AY
                            Case ObjectInstanceFlags.Velocity
                                ret.Axes(i) = js.VY
                            Case ObjectInstanceFlags.Position
                                ret.Axes(i) = js.Y
                        End Select
                    Case ObjectTypeGuid.ZAxis
                        Select Case CType(SetupAxes(i).Flags, ObjectInstanceFlags) And ObjectInstanceFlags.Mask
                            Case ObjectInstanceFlags.Force
                                ret.Axes(i) = js.FZ
                            Case ObjectInstanceFlags.Acceleration
                                ret.Axes(i) = js.AZ
                            Case ObjectInstanceFlags.Velocity
                                ret.Axes(i) = js.VZ
                            Case ObjectInstanceFlags.Position
                                ret.Axes(i) = js.Z
                        End Select
                    Case ObjectTypeGuid.RxAxis
                        Select Case CType(SetupAxes(i).Flags, ObjectInstanceFlags) And ObjectInstanceFlags.Mask
                            Case ObjectInstanceFlags.Force
                                ret.Axes(i) = js.FRx
                            Case ObjectInstanceFlags.Acceleration
                                ret.Axes(i) = js.ARx
                            Case ObjectInstanceFlags.Velocity
                                ret.Axes(i) = js.VRx
                            Case ObjectInstanceFlags.Position
                                ret.Axes(i) = js.Rx
                        End Select
                    Case ObjectTypeGuid.RyAxis
                        Select Case CType(SetupAxes(i).Flags, ObjectInstanceFlags) And ObjectInstanceFlags.Mask
                            Case ObjectInstanceFlags.Force
                                ret.Axes(i) = js.FRy
                            Case ObjectInstanceFlags.Acceleration
                                ret.Axes(i) = js.ARy
                            Case ObjectInstanceFlags.Velocity
                                ret.Axes(i) = js.VRy
                            Case ObjectInstanceFlags.Position
                                ret.Axes(i) = js.Ry
                        End Select
                    Case ObjectTypeGuid.RzAxis
                        Select Case CType(SetupAxes(i).Flags, ObjectInstanceFlags) And ObjectInstanceFlags.Mask
                            Case ObjectInstanceFlags.Force
                                ret.Axes(i) = js.FRz
                            Case ObjectInstanceFlags.Acceleration
                                ret.Axes(i) = js.ARz
                            Case ObjectInstanceFlags.Velocity
                                ret.Axes(i) = js.VRz
                            Case ObjectInstanceFlags.Position
                                ret.Axes(i) = js.Rz
                        End Select
                    Case ObjectTypeGuid.Slider
                        Select Case CType(SetupAxes(i).Flags, ObjectInstanceFlags) And ObjectInstanceFlags.Mask
                            Case ObjectInstanceFlags.Force
                                ret.Axes(i) = js.GetFSlider(slider)
                            Case ObjectInstanceFlags.Acceleration
                                ret.Axes(i) = js.GetASlider(slider)
                            Case ObjectInstanceFlags.Velocity
                                ret.Axes(i) = js.GetVSlider(slider)
                            Case ObjectInstanceFlags.Position
                                ret.Axes(i) = js.GetSlider(slider)
                        End Select
                        slider += 1
                End Select
            Next i
            Return ret
        End Function
        ''' <summary>Configures GUI according to selected device</summary>
        Private Sub ApplyDevice()
            tosButtons.Items.Clear()
            flpAxes.Controls.Clear()
            If Joystick IsNot Nothing Then
                ReDim AxesControls(SetupAxes.Count - 1)
                For i As Integer = 0 To SetupAxes.Count - 1
                    AxesControls(i) = NewAxe(SetupAxes(i).Name)
                    Dim r As InputRange = Joystick.Properties.GetRange(ParameterHow.ById, SetupAxes(i).ObjectId)
                    AxesControls(i).Minimum = r.Min
                    AxesControls(i).Maximum = r.Max
                Next i
                ReDim POVsControls(SetupPOVs.Count - 1)
                For i As Integer = 0 To SetupPOVs.Count - 1
                    POVsControls(i) = NewPOV(SetupPOVs(i).Name)
                    POVsControls(i).Minimum = 0
                    POVsControls(i).Maximum = 36000
                Next i
                ReDim ButtonsControls(Joystick.Caps.NumberButtons - 1)
                For i As Integer = 0 To Joystick.Caps.NumberButtons - 1
                    ButtonsControls(i) = NewButton(i)
                Next i
            End If
        End Sub
        ''' <summary>Initializes control of button</summary>
        Private Function NewButton(ByVal text As String) As ToolStripButton
            Dim b As New ToolStripButton(text)
            tosButtons.Items.Add(b)
            Return b
        End Function
        ''' <summary>Initializes control of axe</summary>
        Private Function NewAxe(ByVal text As String) As JoyAxe
            Dim a As New JoyAxe
            a.Text = text
            flpAxes.Controls.Add(a)
            Return a
        End Function
        ''' <summary>Initializes control of POV</summary>
        Private Function NewPOV(ByVal text As String) As JoyAxe
            Dim POV As JoyAxe = NewAxe(text)
            POV.Text = "POV " & POV.Text
            Return POV
        End Function
        <EditorBrowsable(EditorBrowsableState.Never)>
        Private _Joystick As Device
        ''' <summary>Currently selected joystick</summary>
        Protected Property Joystick() As Device
            Get
                If _Joystick Is Nothing Then _Joystick = NewJoystick
                Return _Joystick
            End Get
            Private Set(ByVal value As Device)
                If value Is Nothing Then tmr10ms.Enabled = False
                _Joystick = value
            End Set
        End Property
#End Region
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ID = _ID 'Aby se zobrzilo
            LoadJoysticks()
        End Sub
        ''' <summary>Obsahuje hodnotu vlastnosti <see cref="ID"/></summary>
        Private _ID As ULong = (New Random().Next(Integer.MinValue, Integer.MaxValue) + CULng(-CLng(Integer.MinValue))) * (New Random().Next(Integer.MinValue, Integer.MaxValue) + CULng(-CLng(Integer.MinValue)))
        ''' <summary>ID křesla v rámci serveru</summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)>
        Public Property ID() As ULong
            Get
                Return _ID
            End Get
            Set(ByVal value As ULong)
                _ID = value
                tlbID.Text = value
            End Set
        End Property

        Private Sub tsbJoyRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbJoyRefresh.Click
            LoadJoysticks()
        End Sub

        Private Sub tscJoy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tscJoy.SelectedIndexChanged
            If tscJoy.SelectedIndex < 0 Then
                panJoy.Enabled = False
            Else
                panJoy.Enabled = True
            End If
            If Joystick IsNot Nothing Then Try : Joystick.Unacquire() : Catch : End Try
            Joystick = Nothing
            ApplyDevice()
            If Joystick IsNot Nothing Then OldState = Acquire()
        End Sub


        Private Sub panAxes_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles panAxes.Resize
            flpAxes.MaximumSize = New Drawing.Size(panAxes.ClientSize.Width, 0)
        End Sub
        Dim OldState As JoyInfo
        Private Sub tmr10ms_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr10ms.Tick
            If Joystick Is Nothing Then tmr10ms.Enabled = False : Exit Sub
            Dim NewState As JoyInfo = Acquire()
            ShowState(NewState)
            For i As Integer = 0 To NewState.Buttons.Length - 1
                If OldState.Buttons(i) <> NewState.Buttons(i) Then
                    If NewState.Buttons(i) Then OnButtonDown(New ButtonEventArgs(i)) Else OnButtonUp(New ButtonEventArgs(i))
                End If
            Next i
            For i As Integer = 0 To NewState.Axes.Length - 1
                If OldState.Axes(i) <> NewState.Axes(i) Then
                    OnAxeChanged(New AxeEventArgs(i, NewState.Axes(i), NewState.Axes(i) - OldState.Axes(i)))
                End If
            Next i
            For i As Integer = 0 To NewState.POVs.Length - 1
                If OldState.POVs(i) <> NewState.POVs(i) Then
                    Dim Δn As Integer = NewState.POVs(i) - OldState.POVs(i)
                    Dim Δb As Integer = NewState.POVs(i) + 36000 - OldState.POVs(i)
                    Dim Δ As Integer
                    If NewState.POVs(i) = -1 OrElse OldState.POVs(i) = -1 Then
                        Δ = 0
                    ElseIf Math.Abs(Δn) <= Math.Abs(Δb) Then
                        Δ = Δn
                    Else
                        Δ = Δb
                    End If
                    OnAxeChanged(New AxeEventArgs(i + SetupAxes.Count, NewState.POVs(i), Δ))
                End If
            Next i
            OldState = NewState
        End Sub

#Region "Buffered events"
        ''' <summary>Posluchači bufferivaných událostí</summary>
        Private BufferedSubscribers As New List(Of EventBuffer)
        ''' <summary>Přihlásit se k odběru bufferovaných událostí</summary>
        ''' <param name="Handler">Metoda, která bude události odebírat</param>
        ''' <param name="Interval">Interval odběru</param>
        Public Sub SubscribeBufferedEvents(ByVal Handler As dBufferedEvent, ByVal Interval As UInteger) Implements IChair.SubscribeBufferedEvents
            BufferedSubscribers.Add(New EventBuffer(Handler, Me, Interval))
        End Sub
        ''' <summary>Odhlásit se od odběrtu bufferovaných událostí</summary>
        ''' <param name="Handler">Metoda, která události odebírala</param>
        Public Sub UnsubscribeBufferedEvents(ByVal Handler As dBufferedEvent) Implements IChair.UnsubscribeBufferedEvents
            Dim Subscriber As EventBuffer = Nothing
            For Each SBS As EventBuffer In BufferedSubscribers
                If SBS.HasHandler(Handler) Then
                    Subscriber = SBS
                    Exit For
                End If
            Next SBS
            If Subscriber IsNot Nothing Then
                BufferedSubscribers.Remove(Subscriber)
                Subscriber.Dispose()
            End If
        End Sub
#End Region
        Private Sub tlbID_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tlbID.MouseDown
            If e.Button = Windows.Forms.MouseButtons.Right Then
                cmsCopy.Show(stsStatus, e.Location)
            End If
        End Sub

        Private Sub tmiCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiCopy.Click
            My.Computer.Clipboard.SetText(tlbID.Text)
        End Sub
    End Class
End Namespace