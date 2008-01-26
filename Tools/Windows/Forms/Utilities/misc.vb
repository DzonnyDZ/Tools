Imports System.Windows.Forms
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
    End Module
#End If
End Namespace