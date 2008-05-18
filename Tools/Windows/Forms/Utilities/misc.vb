Imports System.Windows.Forms
Imports System.Runtime.CompilerServices

Namespace WindowsT.FormsT.UtilitiesT
#If Config <= Nightly Then
    'ASAP:Amrk,Forum,WiKi
    ''' <summary>Staes of controls</summary>
    Public Enum ControlState
        ''' <summary>Enabled and visible</summary>
        Enabled
        ''' <summary>Disabled and visible</summary>
        Disabled
        ''' <summary>Disabled and hidden</summary>
        Hidden
    End Enum
    ''' <summary>Miscaleneous small Windows Forms realetd tools</summary>
    Public Module Misc
        'ASAP:Mark, wiki, forum members
        ''' <summary>Applyes <see cref="UtilitiesT.ControlState"/> on given <see cref="Control"/> or gets its state</summary>
        ''' <param name="Control">Control to get or set value for</param>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="UtilitiesT.ControlState"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Control"/> is null</exception>
        Public Property ControlState(ByVal Control As Control) As ControlState
            Get
                If Control Is Nothing Then Throw New ArgumentNullException("Control")
                If Not Control.Visible Then
                    Return UtilitiesT.ControlState.Hidden
                ElseIf Control.Enabled Then
                    Return UtilitiesT.ControlState.Enabled
                Else
                    Return UtilitiesT.ControlState.Disabled
                End If
            End Get
            Set(ByVal value As ControlState)
                If Control Is Nothing Then Throw New ArgumentNullException("Control")
                With Control
                    Select Case value
                        Case UtilitiesT.ControlState.Disabled
                            .Enabled = False
                            .Visible = True
                        Case UtilitiesT.ControlState.Enabled
                            .Enabled = True
                            .Visible = True
                        Case UtilitiesT.ControlState.Hidden
                            .Enabled = False
                            .Visible = False
                        Case Else : Throw New InvalidEnumArgumentException("value", value, GetType(ControlState))
                    End Select
                End With
            End Set
        End Property
        ''' <summary>Removes control from parent its control</summary>
        ''' <param name="Control">Control to be removed</param>
        ''' <remarks>If <paramref name="Control"/>.<see cref="Control.Parent">Parent</see> is null, nozhing happens</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Control"/> is null</exception>
        <Extension()> Public Sub Remove(ByVal Control As Control)
            If Control Is Nothing Then Throw New ArgumentNullException("Control")
            If Control.Parent Is Nothing Then Exit Sub
            Control.Parent.Controls.Remove(Control)
        End Sub

        ''' <summary>Replaces one <see cref="Control"/> in <see cref="TableLayoutPanel"/> with another</summary>
        ''' <param name="tlp">A <see cref="TableLayoutPanel"/> to perform replacement in</param>
        ''' <param name="OldControl">A <see cref="Control"/> to be replaced</param>
        ''' <param name="NewControl">A <see cref="Control"/> to replace <paramref name="OldControl"/> with. If null <paramref name="OldControl"/> is just removed from <paramref name="tlp"/></param>
        ''' <remarks>
        ''' <paramref name="OldControl"/> is replaced with <paramref name="NewControl"/>.
        ''' <paramref name="NewControl"/> inherits <see cref="TableLayoutPanel.GetRow">Row</see>, <see cref="TableLayoutPanel.GetColumn">Column</see>, <see cref="TableLayoutPanel.GetColumnSpan">ColumnSpan</see>, <see cref="TableLayoutPanel.GetRowSpan">RowSpan</see> and <see cref="Control.TabIndex"/> from <paramref name="OldControl"/>.
        ''' <paramref name="OldControl"/> is removed from <paramref name="tlp"/>.<see cref="TableLayoutPanel.Controls">Controls.</see>.
        ''' <paramref name="NewControl"/> is removed from its old <see cref="Control.Parent">Parent</see>.
        ''' </remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="tlp"/> is null -or- <paramref name="OldControl"/> is null</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="OldControl"/> is not contained within <paramref name="tlp"/>.<see cref="TableLayoutPanel.Controls">Controls</see>.</exception>
        <Extension()> Public Sub ReplaceControl(ByVal tlp As TableLayoutPanel, ByVal OldControl As Control, ByVal NewControl As Control)
            If tlp Is Nothing Then Throw New ArgumentNullException("tlp")
            If OldControl Is Nothing Then Throw New ArgumentNullException("OldControl")
            If Not tlp.Controls.Contains(OldControl) Then Throw New InvalidOperationException("OldControl must be member of Controls collection of tlp") 'Localize:Exception
            If NewControl Is Nothing Then
                OldControl.Remove()
                Exit Sub
            End If
            Dim Row = tlp.GetRow(OldControl)
            Dim Column = tlp.GetColumn(OldControl)
            Dim RowSpan = tlp.GetRowSpan(OldControl)
            Dim columnSpan = tlp.GetColumnSpan(OldControl)
            Dim TabIndex = OldControl.TabIndex
            OldControl.Remove()
            NewControl.Remove()
            tlp.Controls.Add(NewControl, Row, Column)
            tlp.SetColumnSpan(NewControl, columnSpan)
            tlp.SetRowSpan(NewControl, RowSpan)
            NewControl.TabIndex = TabIndex
        End Sub
    End Module
#End If
End Namespace