Imports System.Windows.Forms
Imports System.Runtime.CompilerServices

Namespace WindowsT.FormsT.UtilitiesT

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
    ''' <summary>Contains extension methods related to WinForms</summary>
    Public Module WinFormsExtensions
#Region "Controls"
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
            If Not tlp.Controls.Contains(OldControl) Then Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.MustBeMemberOf0CollectionOf2, "OldControl", "Controls", "tlp"))
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

        ''' <summary>Recursivelly searches for innermost focused control placed on given control</summary>
        ''' <param name="Control">Control to serach for child of</param>
        ''' <param name="DoNotExpand">Optional. Call back function delegate. The function returns true for controls that should be treated as controls withoud child controls. If such control contains has or focus it is returned. This ¨function is called for <paramref name="Control"/> (passed in paramater) as well.</param>
        ''' <returns>The inner most control that contain focus. Null when there is no such control and <paramref name="Control"/> also has not focus.</returns>
        <Extension()> Public Function FindActiveControl(ByVal Control As Control, Optional ByVal DoNotExpand As Func(Of Control, Boolean) = Nothing) As Control
            If DoNotExpand Is Nothing Then DoNotExpand = Function(c As Control) False
            If Not DoNotExpand(Control) Then
                For Each iControl As Control In Control.Controls
                    If iControl.ContainsFocus AndAlso Not DoNotExpand(iControl) Then
                        Return iControl.FindActiveControl
                    ElseIf iControl.Focused OrElse iControl.ContainsFocus Then
                        Return iControl
                    End If
                Next
            End If
            If Control.Focused OrElse Control.ContainsFocus Then Return Control Else Return Nothing
        End Function
#End Region

#Region "Screen"
        ''' <summary>Gets neighbouring screen to given screen in given direction</summary>
        ''' <param name="Screen">Screen to get neighbour of</param>
        ''' <param name="Direction">Direction in which (on which side) get neighbour</param>
        ''' <returns>Screen that directly neighbours with <paramref name="Screen"/> in given <paramref name="Direction"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Screen"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Direction"/> is not one of <see cref="Direction"/> values</exception>
        ''' <remarks>Maximal allowed gap or overlap between screens is 5px. For screen (B) to be considered neighbour with <paramref name="Screen"/> (A) screen edges must either share at least 25% of appropriate edge of <paramref name="Screen"/> (A) or screen (B) must share 100% of it's appropriate edge with <paramref name="Screen"/> (A).</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()>
        Public Function GetNeighbourScreen(ByVal Screen As System.Windows.Forms.Screen, ByVal Direction As Direction) As System.Windows.Forms.Screen
            If Screen Is Nothing Then Throw New ArgumentNullException("Screen")
            For Each scr In Screen.AllScreens
                If Screen Is scr Then Continue For
                Dim vDistance = If(scr.Bounds.Top < Screen.Bounds.Top, scr.Bounds.Bottom - scr.Bounds.Top, Screen.Bounds.Bottom - scr.Bounds.Top)
                Dim hDistance = If(scr.Bounds.Left < Screen.Bounds.Left, scr.Bounds.Right - Screen.Bounds.Left, Screen.Bounds.Right - scr.Bounds.Left)
                Select Case Direction
                    Case Direction.Left
                        If Math.Abs(scr.Bounds.Right - Screen.Bounds.Left) < 5 AndAlso vDistance >= Screen.Bounds.Height / 4 Then Return scr
                    Case Direction.Right
                        If Math.Abs(scr.Bounds.Left - Screen.Bounds.Right) < 5 AndAlso vDistance >= Screen.Bounds.Height / 4 Then Return scr
                    Case Direction.Top
                        If Math.Abs(scr.Bounds.Bottom - Screen.Bounds.Top) < 5 AndAlso hDistance >= Screen.Bounds.Width / 4 Then Return scr
                    Case Direction.Bottom
                        If Math.Abs(scr.Bounds.Top - Screen.Bounds.Bottom) < 5 AndAlso hDistance >= Screen.Bounds.Width / 4 Then Return scr
                    Case Else : Throw New InvalidEnumArgumentException("Direction", Direction, Direction.GetType)
                End Select
            Next
            Return Nothing
        End Function
#End Region
    End Module
End Namespace


'Yes, was intension to place this enum otside namespace
''' <summary>Neighbourhood directions</summary>
''' <version version="1.5.3">This enumeration is new in version 1.5.3</version>
Public Enum Direction
    ''' <summary>Left neighbour</summary>
    Left
    ''' <summary>Right neighbour</summary>
    Right
    ''' <summary>Top (front) neighbour</summary>
    Top
    ''' <summary>Bottom (back) neighbour</summary>
    Bottom
End Enum