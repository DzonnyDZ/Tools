Imports CustomRating = Tools.MetadataT.IptcT.Iptc.CustomRating
Imports System.ComponentModel

''' <summary>Control for rating</summary>
''' <version version="2.0.6">This class is new in version 2.0.6</version>
Public Class Rating : Inherits ComboBox
    ''' <summary>CTor - creates a new instance of the <see cref="Rating"/> class</summary>
    Public Sub New()
        Me.DropDownStyle = ComboBoxStyle.DropDownList
        Me.Items.Add(My.Resources.NotRated)
        Me.Items.Add(My.Resources.Rejected)
        For i = 1 To 5
            Me.Items.Add(New String("★"c, i) & New String("☆"c, 5 - i) & i)
        Next
    End Sub

    ''' <summary>Gets or sets the rating value</summary>
    ''' <exception cref="InvalidEnumArgumentException">Value being set is not one of <see cref="CustomRating"/> values</exception>
    Public Property Rating As CustomRating
        Get
            Select Case SelectedIndex
                Case -1, 0 : Return CustomRating.NotRated
                Case 1 : Return CustomRating.Rejected
                Case Else : Return SelectedIndex - 1
            End Select
        End Get
        Set(value As CustomRating)
            If Not value.IsDefined() Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
            If value <> Rating OrElse SelectedIndex = -1 Then
                Select Case value
                    Case CustomRating.NotRated : SelectedIndex = 0
                    Case CustomRating.Rejected : SelectedIndex = 1
                    Case Else : SelectedIndex = value + 1
                End Select
            End If
        End Set
    End Property


    ''' <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
    ''' <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
    Protected Overrides Sub OnKeyDown(e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        If Not e.Handled AndAlso Not e.Shift AndAlso Not e.Control AndAlso Not e.Alt Then
            Dim kc = e.KeyCode
            If RightToLeft Then
                Select Case kc
                    Case Keys.Left : kc = Keys.Right
                    Case Keys.Right : kc = Keys.Left
                End Select
            End If
            Select Case kc
                Case Keys.Right, Keys.Up, Keys.Add, Keys.Multiply
                    If Rating <= 0 Then
                        Rating = CustomRating.Star1
                    ElseIf Rating < CustomRating.Star5 Then
                        Rating += 1
                    End If
                Case Keys.Left, Keys.Down, Keys.Subtract
                    If Rating > CustomRating.Star1 Then
                        Rating -= 1
                    Else
                        Rating = CustomRating.Rejected
                    End If
                Case Keys.Delete : Rating = CustomRating.NotRated
                Case Keys.NumPad0, Keys.D0 : Rating = CustomRating.Rejected
                Case Keys.NumPad1, Keys.D1 : Rating = CustomRating.Star1
                Case Keys.NumPad2, Keys.D2 : Rating = CustomRating.Star2
                Case Keys.NumPad3, Keys.D3 : Rating = CustomRating.Star3
                Case Keys.NumPad4, Keys.D4 : Rating = CustomRating.Star4
                Case Keys.NumPad5, Keys.D5 : Rating = CustomRating.Star5
                Case Else : Exit Sub
            End Select
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub
    ''' <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
    ''' <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
    Protected Overrides Sub OnKeyPress(e As System.Windows.Forms.KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        If Not e.Handled Then
            Select Case e.KeyChar
                Case "0"c, "٠"c : Rating = CustomRating.Rejected
                Case "1"c, "١"c : Rating = CustomRating.Star1
                Case "2"c, "٢"c : Rating = CustomRating.Star2
                Case "3"c, "٣"c : Rating = CustomRating.Star3
                Case "4"c, "٤"c : Rating = CustomRating.Star4
                Case "5"c, "٥"c : Rating = CustomRating.Star5
                Case "+"c, "*"c
                    If Rating <= 0 Then
                        Rating = CustomRating.Star1
                    ElseIf Rating < CustomRating.Star5 Then
                        Rating += 1
                    End If
                Case "-"
                    If Rating > CustomRating.Star1 Then
                        Rating -= 1
                    Else
                        Rating = CustomRating.Rejected
                    End If
                Case Else : Exit Sub
            End Select
            e.Handled = True
        End If
    End Sub
End Class
