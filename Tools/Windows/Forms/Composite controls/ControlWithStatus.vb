Imports Tools.WindowsT.FormsT
Namespace WindowsT.FormsT
    '#If Config <= Nightly Then set in Tools.vbproj
    'Stage: Nightly
    ''' <summary>Common base for controls with <see cref="StatusMarker"/></summary>
    <DefaultEvent("StatusChanged"), DefaultProperty("Status")> _
    <ToolboxItem(False)> _
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(StatusMarker), LastChMMDDYYYY:="07/22/2007")> _
    <MainTool(FirstVerMMDDYYYY:="07/22/2007")> _
    <Microsoft.VisualBasic.CompilerServices.DesignerGeneratedAttribute()> _
    Public Class ControlWithStatus
        Inherits System.Windows.Forms.UserControl
        Implements IControlWithStatus
        ''' <summary>Raises the <see cref="Add"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnAdd(ByVal e As EventArgs)
            RaiseEvent Add(Me, e)
        End Sub
        ''' <summary>Raised after add menu item is clicked</summary>        
        <Category(UtilitiesT.CategoryAttributeValues.Action)> _
        <Description("Raised after Add menu item is clicked")> _
        Public Event Add(ByVal sender As Object, ByVal e As System.EventArgs) Implements IControlWithStatus.Add 'Localize: Description

        ''' <summary>state of add menu item</summary>        
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        <DefaultValue(GetType(UtilitiesT.ControlState), "Hidden")> _
        <Description("State of the Add menu item")> _
        Public Overridable Property AddMenuState() As UtilitiesT.ControlState Implements IControlWithStatus.AddMenuState
            Get
                Return stmStatus.AddMenuState
            End Get
            Set(ByVal value As UtilitiesT.ControlState)
                stmStatus.AddMenuState = value
            End Set
        End Property

        ''' <summary>Indicates if <see cref="Status"/> automatically chnages to <see cref="StatusMarker.Statuses.Changed"/> and <see cref="StatusMarker.Statuses.[New]"/> if user takes appropriate action</summary>        
        <DefaultValue(True)> _
        <Description("Gets or sets value indicating if Status automatically changes to Statuses.Changed when tmiMarkAsChanged is clicked")> _
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property AutoChanged() As Boolean Implements IControlWithStatus.AutoChanged 'Localize: Description
            Get
                Return stmStatus.AutoChanged
            End Get
            Set(ByVal value As Boolean)
                stmStatus.AutoChanged = value
            End Set
        End Property
        ''' <summary>Raises the <see cref="Delete"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnDelete(ByVal e As EventArgs)
            RaiseEvent Delete(Me, e)
        End Sub
        ''' <summary>Raised after delete menu item is clcked</summary>        
        <Category(UtilitiesT.CategoryAttributeValues.Action)> _
        <Description("Raised after Delete menu item is clicked")> _
        Public Event Delete(ByVal sender As Object, ByVal e As System.EventArgs) Implements IControlWithStatus.Delete 'Localize: Description

        ''' <summary>State of delete menu item</summary>        
        <DefaultValue(GetType(UtilitiesT.ControlState), "Enabled")> _
        <Description("State of the Delete menu item")> _
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Overridable Property DeleteMenustate() As UtilitiesT.ControlState Implements IControlWithStatus.DeleteMenustate 'Localize: Description
            Get
                Return stmStatus.DeleteMenuState
            End Get
            Set(ByVal value As UtilitiesT.ControlState)
                stmStatus.DeleteMenuState = value
            End Set
        End Property
        ''' <summary>Raises the <see cref="MarkasChanged"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnMarkAsChanged(ByVal e As EventArgs)
            RaiseEvent MarkAsChanged(Me, e)
        End Sub

        ''' <summary>Raised after mark-as-changed menu item is clicked</summary>        
        <Category(UtilitiesT.CategoryAttributeValues.Action)> _
        <Description("Raised after Mark-as-changed menu item is clicked")> _
        Public Event MarkAsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IControlWithStatus.MarkAsChanged 'Localize: Description

        ''' <summary>State of mar-as-changed menu item</summary>        
        <DefaultValue(GetType(UtilitiesT.ControlState), "Hidden")> _
        <Description("State of the Mark-as-changed menu item")> _
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Overridable Property MarkAsChangedMenuState() As UtilitiesT.ControlState Implements IControlWithStatus.MarkAsChangedMenuState 'Localize: Description
            Get
                Return stmStatus.MarkAsChangedMenuState
            End Get
            Set(ByVal value As UtilitiesT.ControlState)
                stmStatus.MarkAsChangedMenuState = value
            End Set
        End Property
        ''' <summary>Raises the <see cref="Reset"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnReset(ByVal e As EventArgs)
            RaiseEvent Reset(Me, e)
        End Sub
        ''' <summary>Raised after reset menu item is clicked</summary>        
        <Category(UtilitiesT.CategoryAttributeValues.Action)> _
        <Description("Raised after Reset menu item is clicked")> _
        Public Event Reset(ByVal sender As Object, ByVal e As System.EventArgs) Implements IControlWithStatus.Reset 'Localize: Description

        ''' <summary>state of reset menu item</summary>        
        <DefaultValue(GetType(UtilitiesT.ControlState), "Enabled")> _
        <Description("State of the Reset menu item")> _
        <Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Overridable Property ResetMenuState() As UtilitiesT.ControlState Implements IControlWithStatus.ResetMenuState 'Localize: Description
            Get
                Try
                    Return stmStatus.ResetMenuState
                Catch ex As Exception

                    Throw
                End Try
            End Get
            Set(ByVal value As UtilitiesT.ControlState)
                stmStatus.ResetMenuState = value
            End Set
        End Property

        ''' <summary>Current status of control</summary>        
        <DefaultValue(GetType(StatusMarker.Statuses), "Normal")> _
        <Description("Sown status of control")> _
        <Category(UtilitiesT.CategoryAttributeValues.Data)> _
        <Bindable(True)> _
        Public Overridable Property Status() As StatusMarker.Statuses Implements IControlWithStatus.Status 'Localize: Description
            Get
                Return stmStatus.Status
            End Get
            Set(ByVal value As StatusMarker.Statuses)
                stmStatus.Status = value
            End Set
        End Property
        ''' <summary>Raises the <see cref="StatusChanged"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnStatusChanged(ByVal e As EventArgs)
            RaiseEvent StatusChanged(Me, e)
        End Sub
        ''' <summary>Raised after <see cref="Status"/> is changed</summary>        
        <Category(UtilitiesT.CategoryAttributeValues.PropertyChanged)> _
        <Description("Raised after the Status property changes")> _
        Public Event StatusChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements IControlWithStatus.StatusChanged 'Localize: Description
#Region "Designer generated"
        Private Sub InitializeComponent()
            Me.stmStatus = New Tools.WindowsT.FormsT.StatusMarker
            Me.SuspendLayout()
            '
            'stmStatus
            '
            Me.stmStatus.Location = New System.Drawing.Point(0, 0)
            Me.stmStatus.Name = "stmStatus"
            Me.stmStatus.Size = New System.Drawing.Size(24, 24)
            Me.stmStatus.TabIndex = 0
            Me.stmStatus.TabStop = False
            '
            'ControlWithStatus
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.stmStatus)
            Me.Name = "ControlWithStatus"
            Me.Size = New System.Drawing.Size(24, 24)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Protected WithEvents stmStatus As Tools.WindowsT.FormsT.StatusMarker
#End Region
#Region "Event handlers"
        Private Sub stmStatus_Add(ByVal sender As Object, ByVal e As System.EventArgs) Handles stmStatus.Add
            OnAdd(e)
        End Sub

        Private Sub stmStatus_Delete(ByVal sender As Object, ByVal e As System.EventArgs) Handles stmStatus.Delete
            OnDelete(e)
        End Sub

        Private Sub stmStatus_MarkAsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles stmStatus.MarkAsChanged
            OnMarginChanged(e)
        End Sub

        Private Sub stmStatus_Reset(ByVal sender As Object, ByVal e As System.EventArgs) Handles stmStatus.Reset
            OnReset(e)
        End Sub

        Private Sub stmStatus_StatusChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles stmStatus.StatusChanged
            OnStatusChanged(e)
        End Sub
#End Region
    End Class
End Namespace