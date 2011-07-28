Imports CustomRating = Tools.MetadataT.IptcT.Iptc.CustomRating


''' <summary>Shows and edits value of type <see cref="CustomRating"/></summary>
Public Class Rating


#Region "Rating"
    ''' <summary>Gets or sets rating value</summary>
    Public Property Rating() As CustomRating
        <DebuggerStepThrough()> Get
            Return GetValue(RatingProperty)
        End Get
        <DebuggerStepThrough()> Set(ByVal value As CustomRating)
            SetValue(RatingProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="Rating"/> dependency property</summary>
    Public Shared ReadOnly RatingProperty As DependencyProperty =
                           DependencyProperty.Register("Rating", GetType(CustomRating), GetType(Rating),
                           New FrameworkPropertyMetadata(CustomRating.NotRated, AddressOf OnRatingChanged, AddressOf CoerceRatingValue))
    ''' <summary>Called when value of the <see cref="Rating"/> property changes for any <see cref="Rating"/></summary>
    ''' <param name="d">A <see cref="Rating"/> <see cref="Rating"/> has changed for</param>
    ''' <param name="e">Event arguments</param>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not <see cref="Rating"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    <DebuggerStepThrough()> _
    Private Shared Sub OnRatingChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is Rating Then Throw New Tools.TypeMismatchException("d", d, GetType(Rating))
        DirectCast(d, Rating).OnRatingChanged(e)
    End Sub
    ''' <summary>Called whan value of the <see cref="Rating"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnRatingChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
    End Sub
    ''' <summary>Called whenever a value of the <see cref="Rating"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="d">The object that the property exists on. When the callback is invoked, the property system passes this value.</param>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not of type <see cref="Rating"/> -or- <paramref name="baseValue"/> is not of type <see cref="CustomRating"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    Private Shared Function CoerceRatingValue(ByVal d As System.Windows.DependencyObject, ByVal baseValue As Object) As Object
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is Rating Then Throw New Tools.TypeMismatchException("d", d, GetType(Rating))
        If Not TypeOf baseValue Is CustomRating AndAlso Not baseValue Is Nothing Then Throw New Tools.TypeMismatchException("baseValue", baseValue, GetType(CustomRating))
        Return DirectCast(d, Rating).CoerceRatingValue(baseValue)
    End Function
    ''' <summary>Called whenever a value of the <see cref="Rating"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt, but ensured to be of correct type.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    Protected Overridable Function CoerceRatingValue(ByVal baseValue As CustomRating) As CustomRating
        If Not baseValue.IsDefined Then Return CustomRating.NotRated Else Return baseValue
    End Function
#End Region



    Private Sub btnNotRated_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnNotRated.Click
        Rating = CustomRating.NotRated
        e.Handled = True
    End Sub

    Private Sub btnRejected_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnRejected.Click
        Rating = CustomRating.Rejected
        e.Handled = True
    End Sub

    Private Sub btn1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn1.Click
        Rating = CustomRating.Star1
        e.Handled = True
    End Sub

    Private Sub btn2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn2.Click
        Rating = CustomRating.Star2
        e.Handled = True
    End Sub

    Private Sub btn3_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn3.Click
        Rating = CustomRating.Star3
        e.Handled = True
    End Sub

    Private Sub btn4_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn4.Click
        Rating = CustomRating.Star4
        e.Handled = True
    End Sub

    Private Sub btn5_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn5.Click
        Rating = CustomRating.Star5
        e.Handled = True
    End Sub
End Class
