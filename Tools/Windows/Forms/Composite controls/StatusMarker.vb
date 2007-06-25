#If Config <= Nightly Then
'ASAP:Mark,Forum,WiKi,Conditional file, Bitmap
Namespace WindowsT.FormsT
    ''' <summary>Marks state of data item</summary>
    <DefaultProperty("Status")> _
    <DefaultBindingProperty("Status")> _
    Public Class StatusMarker
        ''' <summary>Supported states of <see cref="StatusMarker"/></summary>
        Public Enum Statuses
            ''' <summary>Normal state (data are in sync)</summary>
            Normal
            ''' <summary>Null (data are not available)</summary>
            Null
            ''' <summary>New (data was null and now it is not null)</summary>
            [New]
            ''' <summary>Changed (data was changed since last save)</summary>
            Changed
            ''' <summary>Deleted (data was not null and now it is null)</summary>
            Deleted
            ''' <summary>Data error</summary>
            [Error]
            ''' <summary>Data status unkown</summary>
            NA
        End Enum
        ''' <summary>Contains value of the <see cref="Status"/> property</summary>
        Private _Status As Statuses = Statuses.Normal
        ''' <summary>Gets or sets shown status of the control</summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="Statuses"/></exception>
        <DefaultValue(GetType(Statuses), "Normal")> _
        <Description("Sown status of control")> _
        <Category(UtilitiesT.CategoryAttributeValues.Data)> _
        <Bindable(True)> _
        Public Overridable Property Status() As Statuses 'Localize: Description
            Get
                Return _Status
            End Get
            Set(ByVal value As Statuses)
                Select Case value
                    'Localize: Tooltips
                    Case Statuses.Deleted : cmdStatus.ImageKey = "Deleted" : totToolTip.SetToolTip(cmdStatus, "Deleted")
                    Case Statuses.Error : cmdStatus.ImageKey = "Error" : totToolTip.SetToolTip(cmdStatus, "Error")
                    Case Statuses.Changed : cmdStatus.ImageKey = "Changed" : totToolTip.SetToolTip(cmdStatus, "Changed")
                    Case Statuses.NA : cmdStatus.ImageKey = "N/A" : totToolTip.SetToolTip(cmdStatus, "N/A")
                    Case Statuses.[New] : cmdStatus.ImageKey = "New" : totToolTip.SetToolTip(cmdStatus, "New")
                    Case Statuses.Normal : cmdStatus.ImageKey = "Normal" : totToolTip.SetToolTip(cmdStatus, "Normal")
                    Case Statuses.Null : cmdStatus.ImageKey = "Null" : totToolTip.SetToolTip(cmdStatus, "Null")
                    Case Else : Throw New InvalidEnumArgumentException("value", value, GetType(Statuses))
                End Select
                _Status = value
                OnStatusChanged(EventArgs.Empty)
            End Set
        End Property
        ''' <summary>Raised after <see cref="Status"/> changes</summary>
        Public Event StatusChanged As EventHandler
        ''' <summary>Raises the <see cref="StatusChanged"/> event</summary>
        ''' <param name="e">Event parameters (<see cref="EventArgs.Empty"/>)</param>
        Protected Overridable Sub OnStatusChanged(ByVal e As EventArgs)
            RaiseEvent StatusChanged(Me, e)
        End Sub
        ''' <summary>Gets or set state of <see cref="tmiDelete"/></summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(UtilitiesT.ControlState), "Enabled")> _
        <Description("State of the Delete menu item")> _
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property DeleteMenuState() As UtilitiesT.ControlState 'Localize: Description
            Get
                Return MenuState(tmiDelete)
            End Get
            Set(ByVal value As UtilitiesT.ControlState)
                MenuState(tmiDelete) = value
            End Set
        End Property
        ''' <summary>Gets or set state of <see cref="tmiMarkAsChanged"/></summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(UtilitiesT.ControlState), "Hidden")> _
        <Description("State of the Delete menu item")> _
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property MarkAsChangedMenuState() As UtilitiesT.ControlState 'Localize: Description
            Get
                Return MenuState(tmiMarkAsChanged)
            End Get
            Set(ByVal value As UtilitiesT.ControlState)
                MenuState(tmiMarkAsChanged) = value
            End Set
        End Property
        ''' <summary>Gets or set state of <see cref="tmiReset"/></summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(UtilitiesT.ControlState), "Enabled")> _
        <Description("State of the Reset menu item")> _
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property ResetMenuState() As UtilitiesT.ControlState 'Localize: Description
            Get
                Return MenuState(tmiReset)
            End Get
            Set(ByVal value As UtilitiesT.ControlState)
                MenuState(tmiReset) = value
            End Set
        End Property
        ''' <summary>Gets or sets state of menu item</summary>
        ''' <param name="Menu">Menu item to get or set state for</param>
        Protected Property MenuState(ByVal Menu As Windows.Forms.ToolStripMenuItem) As UtilitiesT.ControlState
            Get
                Return Menu.Tag
            End Get
            Set(ByVal value As UtilitiesT.ControlState)
                With Menu
                    Select Case value
                        Case UtilitiesT.ControlState.Disabled
                            .Visible = True
                            .Enabled = False
                        Case UtilitiesT.ControlState.Enabled
                            .Visible = True
                            .Enabled = True
                        Case UtilitiesT.ControlState.Hidden
                            .Enabled = False
                            .Visible = False
                        Case Else
                            Throw New InvalidEnumArgumentException("value", value, GetType(UtilitiesT.ControlState))
                    End Select
                    .Tag = value
                End With
            End Set
        End Property
        ''' <summary>Gets or set state of <see cref="tmiAdd"/></summary>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        <DefaultValue(GetType(UtilitiesT.ControlState), "Hidden")> _
        <Description("State of the Add menu item")> _
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property AddMenuState() As UtilitiesT.ControlState 'Localize: Description
            Get
                Return MenuState(tmiAdd)
            End Get
            Set(ByVal value As UtilitiesT.ControlState)
                MenuState(tmiAdd) = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="AutoChanged"/> property</summary>
        <EditorBrowsableAttribute(EditorBrowsableState.Never)> _
        Private _AutoChanged As Boolean

        ''' <summary>Gets or sets value indicating if <see cref="Status"/> automatically changes to <see cref="Statuses.Changed"/> when <see cref="tmiMarkAsChanged"/> is clicked</summary>
        <DefaultValue(True)> _
        <Description("Gets or sets value indicating if Status automatically changes to Statuses.Changed when tmiMarkAsChanged is clicked")> _
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property AutoChanged() As Boolean
            Get
                Return _AutoChanged
            End Get
            Set(ByVal value As Boolean)
                _AutoChanged = value
            End Set
        End Property
        Private Sub tmiAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmiAdd.Click
            OnAdd(e)
        End Sub

        Private Sub tmiDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmiDelete.Click
            OnDelete(e)
        End Sub

        Private Sub tmiReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmiReset.Click
            OnReset(e)
        End Sub
        Private Sub tmiMarkAsChanged_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmiMarkAsChanged.Click
            OnMarkAsChanged(e)
        End Sub
        ''' <summary>Raised after <see cref="tmiAdd"/> is clicked</summary>
        Public Event Add As EventHandler
        ''' <summary>Raises the <see cref="Add"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnAdd(ByVal e As EventArgs)
            RaiseEvent Add(Me, e)
        End Sub
        ''' <summary>Raised after <see cref="tmiMarkAsChanged"/> is clicked</summary>
        Public Event MarkAsChanged As EventHandler
        ''' <summary>Raises the <see cref="MarkAsChanged"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnMarkAsChanged(ByVal e As EventArgs)
            If AutoChanged Then Me.Status = Statuses.Changed
            RaiseEvent MarkAsChanged(Me, e)
        End Sub
        ''' <summary>Raised after <see cref="tmiReset"/> is clicked</summary>
        Public Event Reset As EventHandler
        ''' <summary>Raises the <see cref="Reset"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnReset(ByVal e As EventArgs)
            RaiseEvent Reset(Me, e)
        End Sub
        ''' <summary>Raised after <see cref="tmiReset"/> is clicked</summary>
        Public Event Delete As EventHandler
        ''' <summary>Raises the <see cref="Delete"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnDelete(ByVal e As EventArgs)
            RaiseEvent Delete(Me, e)
        End Sub

        Private Sub cmdStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStatus.Click
            OnButtonClick()
        End Sub
        ''' <summary>Shows <see cref="cmsStatus"/></summary>
        Protected Overridable Sub OnButtonClick()
            cmsStatus.Show(cmdStatus, 0, cmdStatus.Height)
        End Sub
        ''' <summary>Gets or sets if this control automatically sizes by its content</summary>
        <DefaultValue(True), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Overrides Property AutoSize() As Boolean
            Get
                Return MyBase.AutoSize
            End Get
            Set(ByVal value As Boolean)
                MyBase.AutoSize = value
            End Set
        End Property


    End Class
End Namespace
#End If
