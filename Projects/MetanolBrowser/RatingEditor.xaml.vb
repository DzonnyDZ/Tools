Imports CustomRating = Tools.MetadataT.IptcT.Iptc.CustomRating
Imports mBoxButtons = Tools.WindowsT.IndependentT.MessageBox.MessageBoxButton.Buttons

''' <summary>Dialog for editing rating</summary>
Public Class RatingEditor
    ''' <summary>CTor - creates a new instance of the <see cref="RatingEditor"/> class</summary>
    ''' <param name="iptc">And IPTC metadata which contains the rating</param>
    Public Sub New(iptc As IptcInternal)
        InitializeComponent()
        Me.Iptc = iptc
    End Sub

    ''' <summary>Gets or sets current instance of IPTC metadata being edited</summary>
    Private Property Iptc As IptcInternal
        Get
            Return DataContext
        End Get
        Set(value As IptcInternal)
            DataContext = value
        End Set
    End Property

    Private Sub btnOK_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnOK.Click
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCancel.Click
        Me.DialogResult = False
        Me.Close()
    End Sub

    Private Sub rtgTech_RatingChangedByUser(sender As Object, e As Rating.RatingChangedByUserEventArgs) Handles rtgTech.RatingChangedByUser
        If GoForNextRating(sender, e) Then rtgArt.Focus()
        OnRatingChanged()
    End Sub
    Private Sub rtgArt_RatingChangedByUser(sender As Object, e As Rating.RatingChangedByUserEventArgs) Handles rtgArt.RatingChangedByUser
        If GoForNextRating(sender, e) Then rtgInfo.Focus()
        OnRatingChanged()
    End Sub
    Private Sub rtgInfo_RatingChangedByUser(sender As Object, e As Rating.RatingChangedByUserEventArgs) Handles rtgInfo.RatingChangedByUser
        If GoForNextRating(sender, e) Then rtgOverall.Focus()
        OnRatingChanged()
    End Sub
    Private Sub rtgOverall_RatingChangedByUser(sender As Object, e As Rating.RatingChangedByUserEventArgs) Handles rtgOverall.RatingChangedByUser
        If GoForNextRating(sender, e) Then rtgTech.Focus()
    End Sub
    ''' <summary>Gets value indicating if next rating control should be focused after change of value of previosu one</summary>
    ''' <param name="sender">Source of the event</param>
    ''' <param name="e">Event arguments of <see cref="Rating.RatingChangedByUser"></see> event</param>
    Private Function GoForNextRating(sender As Rating, e As Rating.RatingChangedByUserEventArgs) As Boolean
        If e.Keyboard Then
            If TypeOf e.InnerEvent Is KeyEventArgs Then
                Dim kea As KeyEventArgs = e.InnerEvent
                If Keyboard.Modifiers <> ModifierKeys.None Then Return True
                Select Case kea.Key
                    Case Key.Up : Return sender.Rating = CustomRating.Star5
                    Case Key.Down : Return sender.Rating = CustomRating.NotRated
                    Case Key.Left
                        If sender.FlowDirection = Windows.FlowDirection.LeftToRight Then Return sender.Rating = CustomRating.Star5
                        If sender.FlowDirection = Windows.FlowDirection.RightToLeft Then Return sender.Rating = CustomRating.NotRated
                    Case Key.Right
                        If sender.FlowDirection = Windows.FlowDirection.LeftToRight Then Return sender.Rating = CustomRating.NotRated
                        If sender.FlowDirection = Windows.FlowDirection.RightToLeft Then Return sender.Rating = CustomRating.Star5
                    Case Else : Return True
                End Select
            ElseIf TypeOf e.InnerEvent Is TextCompositionEventArgs Then
                Dim tcea As TextCompositionEventArgs = e.InnerEvent
                Select Case tcea.Text
                    Case "+", "-" : Return True
                    Case Else : Return False
                End Select
            End If
        End If
        Return False
    End Function

    ''' <summary>Called when sub-rating is changed by user</summary>
    Private Sub OnRatingChanged()
        If rtgOverall.Rating = CustomRating.NotRated AndAlso
                rtgTech.Rating <> CustomRating.NotRated AndAlso rtgArt.Rating <> CustomRating.NotRated AndAlso rtgInfo.Rating <> CustomRating.NotRated Then
            Dim pr As Func(Of CustomRating, Integer) = Function(rating) If(rating = CustomRating.Rejected, 0, rating)
            Dim value% = Math.Round((pr(rtgTech.Rating) + pr(rtgInfo.Rating) + pr(rtgArt.Rating)) / 3, MidpointRounding.AwayFromZero)
            rtgOverall.Rating = If(value = 0, CustomRating.Rejected, CType(value, CustomRating))
        End If
    End Sub

    Private Sub NextPage_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        DoNavigation(NavigationCommands.NextPage, e.Parameter)
    End Sub

    Private Sub PreviousPage_Executed(sender As System.Object, e As System.Windows.Input.ExecutedRoutedEventArgs)
        DoNavigation(NavigationCommands.PreviousPage, e.Parameter)
    End Sub

    Private Sub DoNavigation(navigationCommand As RoutedUICommand, parameter As Object)
        If TypeOf Owner Is BrowserWindow Then
            Dim BrowserWindow As BrowserWindow = DirectCast(Owner, BrowserWindow)
            If Iptc.IsChanged AndAlso
                BrowserWindow.SaveIptc(mBoxButtons.Ignore Or mBoxButtons.Retry Or mBoxButtons.Cancel) =
                    Forms.DialogResult.Cancel Then Exit Sub
            navigationCommand.Execute(parameter, Owner)
            DataContext = If(BrowserWindow.Metadata Is Nothing, Nothing, BrowserWindow.Metadata.Iptc)
            If DataContext Is Nothing Then
                Me.DialogResult = Nothing
                Me.Close()
            Else
                rtgTech.Focus()
            End If
        End If
    End Sub

    Private Sub PrevNextPage_CanExecute(sender As System.Object, e As System.Windows.Input.CanExecuteRoutedEventArgs) Handles cmdNextPage.CanExecute, cmdPreviousPage.CanExecute
        e.CanExecute = TypeOf Owner Is BrowserWindow
    End Sub

End Class
