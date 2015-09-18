Imports System.ComponentModel
Imports System.Windows.Forms
#If True
Namespace WindowsT.FormsT
    ''' <summary>Wizard form</summary>
    ''' <remarks>Typically you implement wizard steps as <see cref="Control">Controls</see> which implements <see cref="IWizardControl"/>.
    ''' However step of wizard can be any <see cref="Control"/>. In such case you can obtain wizard as YourControl.<see cref="Control.FindForm">FindForm</see>.</remarks>
    ''' <version version="1.5.2" stage="Nightly">Class moved from experimental (namespace <see cref="N:Tools.Experimental.GUI"/>)</version>
    Public Class Wizard
        Inherits Form
        ''' <summary>Default text for previos step button</summary>
        Public Shared ReadOnly DefaultPrevButtonText$
        ''' <summary>Default text for next step button</summary>
        Public Shared ReadOnly DefaultNextButtonText$
        ''' <summary>Default text for cancel button</summary>
        Public Shared ReadOnly DefaultCancelButtonText$
        ''' <summary>Initializer</summary>
        Shared Sub New()
            Dim Temp As New Wizard(New Control)
            DefaultPrevButtonText = Temp.PrevButton.Text
            DefaultNextButtonText = Temp.NextButton.Text
            DefaultCancelButtonText = Temp.CancelWizardButton.Text
        End Sub

        ''' <summary>CTor</summary>
        ''' <param name="FirstStep">First step of wizard - it can be any control, but typically it vderives from <see cref="IWizardControl"/></param>
        Public Sub New(ByVal FirstStep As Control)
            InitializeComponent()
            CurrentStep = FirstStep
            _Steps.Add(CurrentStep)
            _CurrentStepIndex = 0
            PrevButton.Enabled = False
            'GUI.Misc.HandlePaint(Me, tlpMain)
        End Sub
        ''' <summary>Raises the <see cref="System.Windows.Forms.Form.Shown"/> event.</summary>
        ''' <param name="e">A <see cref="System.EventArgs"/> that contains the event data</param>
        Protected Overrides Sub OnShown(ByVal e As System.EventArgs)
            If Me.Owner IsNot Nothing Then Me.Icon = Owner.Icon
            MyBase.OnShown(e)
        End Sub
        ''' <summary>Gets or sets current step of the wizard.</summary>
        ''' <returns>Current step of the wizard</returns>
        ''' <value>Typically use <see cref="GoNext"/> instead of setting this property</value>
        ''' <remarks>
        ''' This property does not tracks Previous/Back steps. 
        ''' Note for inheritors: Value of this property is defined as first control of <see cref="panControlHost"/> (which is <see cref="WizardHostControl"/>).
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overridable Property CurrentStep() As Control
            Get
                If panControlHost.Controls.Count > 0 Then
                    Return panControlHost.Controls(0)
                Else
                    Return Nothing
                End If
            End Get
            Protected Set(ByVal value As Control)
                If value Is CurrentStep Then Exit Property
                panControlHost.Controls.Clear()
                If value IsNot Nothing Then
                    panControlHost.Controls.Add(value)
                    Try
                        value.Dock = DockStyle.Fill
                    Catch : End Try
                    If TypeOf value Is IWizardControl AndAlso DirectCast(value, IWizardControl).Wizard IsNot Me Then
                        DirectCast(value, IWizardControl).Wizard = Me
                    End If
                    RaiseEvent StepEnter(Me, New StepEventArgs(value))
                End If
                OnCurrentStepChanged()
            End Set
        End Property
        ''' <summary>Raises the <see cref="CurrentStepChanged"/> event</summary>
        Protected Overridable Sub OnCurrentStepChanged()
            RaiseEvent CurrentStepChanged(Me, New System.EventArgs)
        End Sub
        ''' <summary>raised after <see cref="CurrentStep"/> property changes</summary>
        Public Event CurrentStepChanged As EventHandler
        ''' <summary>Advances wizard to next step. Remembers current step. Typically use this method instead of setting the <see cref="CurrentStep"/> proprty directly.</summary>
        ''' <param name="Control">Next step of wizard. It can be any control, but typically it implements <see cref="IWizardControl"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="Control"/> is null</exception>
        ''' <exception cref="InvalidOperationException"><see cref="CurrentStepIndex"/> &lt; <see cref="StepsCount"/> - 1 (Current step is not lats known step)</exception>
        Public Sub GoNext(ByVal Control As Control)
            If Control Is Nothing Then Throw New ArgumentNullException("Control")
            If CurrentStepIndex < StepsCount - 1 Then Throw New InvalidOperationException("Wizard can advance to new next step only if it is at the end of known stapes.")
            _Steps.Add(Control)
            CurrentStepIndex = StepsCount - 1
        End Sub
        ''' <summary>Clears list of konown steps from given index</summary>
        ''' <param name="From">Index of first step to delete. Must be greater than <see cref="CurrentStepIndex"/></param>
        ''' <exception cref="ArgumentException"><paramref name="From"/> &lt;= <see cref="CurrentStepIndex"/> (cannot delete current or already passed step)</exception>
        Public Sub ClearStepList(ByVal From As Integer)
            If From <= CurrentStepIndex Then Throw New ArgumentException("Cannot delete current or already passed step.")
            _Steps.RemoveRange(From, _Steps.Count - From)
        End Sub
#Region "Wizard form controls"
        ''' <summary>Gets control that hosts wizard control (<see cref="CurrentStep"/>. This control becomes <see cref="Control.Parent"/> of <see cref="CurrentStep"/>.</summary>
        ''' <remarks>This is <see cref="panControlHost"/></remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property WizardHostControl() As Control
            Get
                Return panControlHost
            End Get
        End Property
        ''' <summary>Button for going to previous step.</summary>
        ''' <remarks>Use this property to hide/show or enable/disable the button or change text etc.
        ''' Thi is <see cref="cmdBack"/>.</remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property PrevButton() As Button
            Get
                Return cmdBack
            End Get
        End Property
        ''' <summary>Button for going to next step.</summary>
        ''' <remarks>Use this property to hide/show or enable/disable the button or change text etc.
        ''' This is <see cref="cmdNext"/>.</remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property NextButton() As Button
            Get
                Return cmdNext
            End Get
        End Property
        ''' <summary>Button for going cancelling wizard.</summary>
        ''' <remarks>Use this property to hide/show or enable/disable the button or change text etc.
        ''' This is <see cref="cmdCancel"/></remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property CancelWizardButton() As Button
            Get
                Return cmdCancel
            End Get
        End Property
#End Region
#Region "Wizard events"
        ''' <summary>List of handler for the <see cref="NextStep"/> event</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _NextStep As New List(Of EventHandler(Of CancelEventArgs))
        ''' <summary>Raised before advancing to next step. Can be canceled.</summary>
        ''' <remarks>If your step does not implement <see cref="IWizardControl"/>, you should advance to next step in handler of this event manually - but only if current step is last step (<see cref="CurrentStepIndex"/> = <see cref="StepsCount"/> -1).
        ''' If event handler is set to instance method of <see cref="Control"/> - derived class the event is raised for such handler only when <see cref="CurrentStep"/> is the instance.
        ''' This event is intended to be handled by control that implements wizard step, not by any control, bacause on <see cref="Control">Controls</see> it is raised only it the <see cref="Control">Control</see> is <see cref="CurrentStep"/>.</remarks>
        Public Custom Event NextStep As EventHandler(Of CancelEventArgs)
            <DebuggerStepThrough()> _
            AddHandler(ByVal value As EventHandler(Of CancelEventArgs))
                _NextStep.Add(value)
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As EventHandler(Of CancelEventArgs))
                _NextStep.Remove(value)
            End RemoveHandler
            <DebuggerStepThrough()> _
            RaiseEvent(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
                RaiseStepEvent(sender, e, _NextStep)
            End RaiseEvent
        End Event
        ''' <summary>List of handler for the <see cref="PrevStep"/> event</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _PrevStep As New List(Of EventHandler(Of CancelEventArgs))
        ''' <summary>Raised before moving to previous step. Can be canceled.</summary>
        ''' <remarks>If event handler is set to instance method of <see cref="Control"/> - derived class the event is raised for such handler only when <see cref="CurrentStep"/> is the instance.
        ''' This event is intended to be handled by control that implements wizard step, not by any control, bacause on <see cref="Control">Controls</see> it is raised only it the <see cref="Control">Control</see> is <see cref="CurrentStep"/>.</remarks>
        Public Custom Event PrevStep As EventHandler(Of CancelEventArgs)
            <DebuggerStepThrough()> _
            AddHandler(ByVal value As EventHandler(Of CancelEventArgs))
                _PrevStep.Add(value)
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As EventHandler(Of CancelEventArgs))
                _PrevStep.Remove(value)
            End RemoveHandler
            <DebuggerStepThrough()> _
            RaiseEvent(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
                RaiseStepEvent(sender, e, _PrevStep)
            End RaiseEvent
        End Event
        ''' <summary>List of handler for the <see cref="CancelWizard"/> event</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CancelWizard As New List(Of EventHandler(Of CancelEventArgs))
        ''' <summary>Raised before canceling the wizard (by closing the window, pressing cancel button or calling <see cref="DoCancel"/>. Can be calceled.</summary>
        ''' <remarks>If event handler is set to instance method of <see cref="Control"/> - derived class the event is raised for such handler only when <see cref="CurrentStep"/> is the instance.
        ''' This event is intended to be handled by control that implements wizard step, not by any control, bacause on <see cref="Control">Controls</see> it is raised only it the <see cref="Control">Control</see> is <see cref="CurrentStep"/>.</remarks>
        Public Custom Event CancelWizard As EventHandler(Of CancelEventArgs)
            <DebuggerStepThrough()> _
            AddHandler(ByVal value As EventHandler(Of CancelEventArgs))
                _CancelWizard.Add(value)
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As EventHandler(Of CancelEventArgs))
                _CancelWizard.Remove(value)
            End RemoveHandler
            <DebuggerStepThrough()> _
            RaiseEvent(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
                RaiseStepEvent(sender, e, _CancelWizard)
            End RaiseEvent
        End Event
        ''' <summary>List of handler for the <see cref="StepEnter"/> event</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _StepEnter As New List(Of EventHandler(Of StepEventArgs))
        ''' <summary>raised when control becomes active step.</summary>
        ''' <remarks>If event handler is set to instance method of <see cref="Control"/> - derived class the event is raised for such handler only when <see cref="CurrentStep"/> is the instance.
        ''' This event is intended to be handled by control that implements wizard step, not by any control, bacause on <see cref="Control">Controls</see> it is raised only it the <see cref="Control">Control</see> is <see cref="CurrentStep"/>.</remarks>
        Public Custom Event StepEnter As EventHandler(Of StepEventArgs)
            <DebuggerStepThrough()> _
            AddHandler(ByVal value As EventHandler(Of StepEventArgs))
                _StepEnter.Add(value)
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As EventHandler(Of StepEventArgs))
                _StepEnter.Remove(value)
            End RemoveHandler
            <DebuggerStepThrough()> _
            RaiseEvent(ByVal sender As Object, ByVal e As StepEventArgs)
                RaiseStepEvent(sender, e, _StepEnter)
            End RaiseEvent
        End Event
        ''' <summary>Raises event on current step</summary>
        ''' <typeparam name="T">Type of event arguments</typeparam>
        ''' <param name="sender">Source of event. Must be current instance</param>
        ''' <param name="e">Event arguments</param>
        ''' <param name="List">List of delegates to call</param>
        ''' <exception cref="ArgumentException"><paramref name="sender"/> is not current instace</exception>
        ''' <remarks>Delegate from <paramref name="List"/> collection is called only when <see cref="[Delegate].Target"/> is null, <see cref="[Delegate].Target"/> is not instance of <see cref="Control"/> or <see cref="[Delegate].Target"/> is <see cref="CurrentStep"/></remarks>
        Private Sub RaiseStepEvent(Of T As EventArgs)(ByVal sender As Object, ByVal e As T, ByVal List As IEnumerable(Of EventHandler(Of T)))
            If sender IsNot Me Then Throw New ArgumentException("sender must be current instance.", "sender")
            Dim Handlers As New Queue(Of EventHandler(Of T))
            'Cannot iterate through list collection while raising events, bacause it can be altered by handler
            For Each handler As EventHandler(Of T) In List
                If handler Is Nothing Then Continue For
                If handler.Target Is Nothing OrElse Not TypeOf handler.Target Is Control OrElse handler.Target Is CurrentStep Then
                    Handlers.Enqueue(handler)
                End If
            Next handler
            While Handlers.Count > 0
                Handlers.Dequeue.Invoke(sender, e)
            End While
        End Sub
#End Region
#Region "Step properties"
        ''' <summary>Pesudo-collection of all known steps. This is used to auto-navigation between steps.</summary>
        ''' <param name="index">Index of step to obtain</param>
        ''' <returns>Step at given index</returns>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is equal to or greater than <see cref="StepsCount"/>.</exception>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property Steps(ByVal index As Integer) As Control
            Get
                Return _Steps(index)
            End Get
        End Property
        ''' <summary>Number of steps available in <see cref="Steps"/> pseudo-collection.</summary>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property StepsCount() As Integer
            Get
                Return _Steps.Count
            End Get
        End Property
        ''' <summary>Enables enumeration through <see cref="Steps"/> pseudo-collection</summary>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property Steps() As StepsIEnumerable
            Get
                Return New StepsIEnumerable(Me)
            End Get
        End Property
        ''' <summary>Implements <see cref="IEnumerable(Of Control)"/> for <see cref="Steps"/> property</summary>
        Public Class StepsIEnumerable : Implements IEnumerable(Of Control)
            ''' <summary>Owner of this instance</summary>
            Private ReadOnly Parent As Wizard
            ''' <summary>CTor</summary>
            ''' <param name="Parent">Owner of this instance</param>
            Friend Sub New(ByVal Parent As Wizard)
                Me.Parent = Parent
            End Sub
            ''' <summary>Gets enumerator to iterate through <see cref="Steps"/> pseudo-collection</summary>
            Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.Windows.Forms.Control) Implements System.Collections.Generic.IEnumerable(Of System.Windows.Forms.Control).GetEnumerator
                Return Parent._Steps.GetEnumerator
            End Function
            ''' <summary>Gets enumerator to iterate through <see cref="Steps"/> pseudo-collection</summary>
            ''' <remarks>Use type-safe <see cref="GetEnumerator"/> instead</remarks>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            <Obsolete("Use type-safe GetEnumerator instead")> _
            Private Function _GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
                Return GetEnumerator()
            End Function
            ''' <summary>Gets number of items in <see cref="Steps"/> pseudo-collection</summary>
            Public ReadOnly Property Count() As Integer
                Get
                    Return Parent.StepsCount
                End Get
            End Property
        End Class
        ''' <summary>Gets or sets index of current step</summary>
        ''' <returns>Index of current step as stored. Can return -1 if value is not property initialized.</returns>
        ''' <value>New index. Must be in range -1 ÷ <see cref="StepsCount"/> -1. It changes <see cref="CurrentStep"/>.
        ''' If value being set is -1 <see cref="CurrentStep"/> is not changed.</value>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is less than -1 or greater than or equal to <see cref="StepsCount"/></exception>
        ''' <remarks>Value of this property is stored sseparatelly from <see cref="CurrentStep"/> and thus it is possible to <see cref="Steps">Steps</see>[<see cref="CurrentStepIndex">CurrentStepIndex</see>] to differ from <see cref="CurrentStep"/> if control is used in not-recomended way.</remarks>
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property CurrentStepIndex() As Integer
            Get
                Return _CurrentStepIndex
            End Get
            Set(ByVal value As Integer)
                If value = -1 Then
                    _CurrentStepIndex = value
                ElseIf value < StepsCount Then
                    _CurrentStepIndex = value
                    PrevButton.Enabled = value > 0
                    CurrentStep = Steps(value)
                Else
                    Throw New ArgumentOutOfRangeException("Value of step index was out of range of steps")
                End If
            End Set
        End Property
        ''' <summary>Contains value of <see cref="Steps"/> property</summary>
        Private _Steps As New List(Of Control)
        ''' <summary>Contains value of <see cref="CurrentStepIndex"/></summary>
        Private _CurrentStepIndex As Integer = -1
#End Region
#Region "Navigation"
        ''' <summary>Advances wizard to next step. Before it raises the <see cref="NextStep"/> event.</summary>
        ''' <remarks>If <see cref="CurrentStep"/> is last known step (<see cref="CurrentStepIndex"/> = <see cref="StepsCount"/> -1) and it implements <see cref="IWizardControl"/> <see cref="IWizardControl.GetNext"/> is called in order to obrain next step.
        ''' If current step is not last step wizard activates next known step.
        ''' If next step cannot be obtained (because of <see cref="CurrentStep"/> does not implements <see cref="IWizardControl"/> or <see cref="IWizardControl.GetNext"/> return snull) nothing happens.
        ''' So, when your control does not implement <see cref="IWizardControl"/> you must manually advance to next step in handler of <see cref="NextStep"/> event - but only when <see cref="CurrentStep"/> is last step.</remarks>
        Public Overridable Sub GoNext()
            Dim e As New CancelEventArgs
            RaiseEvent NextStep(Me, e)
            If Not e.Cancel Then
                If CurrentStepIndex < StepsCount - 1 Then
                    CurrentStepIndex += 1
                ElseIf TypeOf CurrentStep Is IWizardControl Then
                    Dim NextControl As Control = DirectCast(CurrentStep, IWizardControl).GetNext
                    If NextControl IsNot Nothing Then
                        GoNext(NextControl)
                    End If
                End If
            End If
        End Sub
        ''' <summary>Navigates to previous step of wizard. Before it raises the <see cref="PrevStep"/> event.</summary>
        ''' <exception cref="InvalidOperationException"><see cref="CurrentStepIndex"/> is 0</exception>
        ''' <remarks>If <see cref="CurrentStepIndex"/> is -1 nothing happens</remarks>
        Public Overridable Sub GoPrev()
            If CurrentStepIndex = 0 Then Throw New InvalidOperationException("Cannot move to previous step, because current step is first.")
            If CurrentStepIndex < 0 Then Exit Sub
            Dim e As New CancelEventArgs
            RaiseEvent PrevStep(Me, e)
            If Not e.Cancel Then CurrentStepIndex -= 1
        End Sub
        ''' <summary>Cancels wizard dialog. before it raises the <see cref="CancelWizard"/> event</summary>
        ''' <returns>True if dialog was closed</returns>
        Public Overridable Function DoCancel() As Boolean
            Dim e As New CancelEventArgs
            RaiseEvent CancelWizard(Me, e)
            If Not e.Cancel Then ForceCloseDialog()
            Return Not e.Cancel
        End Function
        ''' <summary>If true <see cref="OnFormClosing"/> never cancels closing of form (unless it is canceled by <see cref="Form.OnFormClosing"/></summary>
        Private ForceClosing As Boolean = False
        ''' <summary>Raises the <see cref="Form.FormClosing"/> event. It the event does not cancel form closing calls <see cref="DoCancel"/> (raises the <see cref="CancelWizard"/> event) where closing of form can be canceled, to.</summary>
        Protected NotOverridable Overrides Sub OnFormClosing(ByVal e As System.Windows.Forms.FormClosingEventArgs)
            MyBase.OnFormClosing(e)
            If e.Cancel Then Exit Sub
            If Not ForceClosing Then
                e.Cancel = Not DoCancel()
            End If
        End Sub
        ''' <summary>Closes wizard dialog without option to cancel the process and without noticing wizard control of current step.</summary>
        Public Sub ForceCloseDialog()
            ForceClosing = True
            Me.Close()
        End Sub
#End Region
#Region "Button events"
        Private Sub cmdBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBack.Click
            GoPrev()
        End Sub

        Private Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
            GoNext()
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            DoCancel()
        End Sub
#End Region
    End Class

    ''' <summary>Arguments of wizard events related to spets</summary>
    ''' <version version="1.5.2" stage="Nightly">Class moved from experimental (namespace <see cref="N:Tools.Experimental.GUI"/>)</version>
    Public Class StepEventArgs : Inherits EventArgs
        ''' <summary>CTor</summary>
        ''' <param name="Control">Contro of the step</param>
        Public Sub New(ByVal Control As Control)
            Me.StepControl = Control
        End Sub
        ''' <summary>Control of the step</summary>
        Public ReadOnly StepControl As Control
    End Class

    ''' <summary>Provides basic interface for wizard steps of <see cref="Wizard"/>.</summary>
    ''' <version version="1.5.2" stage="Nightly">Interface moved from experimental (namespace <see cref="N:Tools.Experimental.GUI"/>)</version>
    Public Interface IWizardControl
        ''' <summary>This property is being set by <see cref="Wizard"/> when control is added to <see cref="Wizard"/>.</summary>
        ''' <returns>Owning wizard of this instance</returns>
        ''' <value>Owning wizard initializes this property</value>
        ''' <remarks>In setter of this property the wizard control should subscribe to wizard events.</remarks>
        Property Wizard() As Wizard
        ''' <summary>Asks wizard control for control that follows after it.</summary>
        ''' <remarks>Control returned by this function should implement <see cref="IWizardControl"/>.</remarks>
        Function GetNext() As Control
    End Interface
End Namespace
#End If