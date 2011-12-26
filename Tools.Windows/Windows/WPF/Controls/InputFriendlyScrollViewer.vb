Imports System.Windows.Controls
Imports System.Linq
Imports Tools.WindowsT.WPF.InputT
Imports System.Windows.Input

Namespace WindowsT.WPF.ControlsT

    ''' <summary>Build-in <see cref="ScrollViewer"/> eats all mouse-wheel events so you cannot use <see cref="MouseWheelGesture"/> on window whare <see cref="ScrollViewer"/> is used. This <see cref="ScrollViewer"/>-derived class solves this issue.</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class InputFriendlyScrollViewer
        Inherits ScrollViewer

        ''' <summary>CTor - creates a new instance of the <see cref="InputFriendlyScrollViewer"/> class and initializes command bindings</summary>
        Public Sub New()
            Me.New(True)
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="InputFriendlyScrollViewer"/> class and optionally initializes command bindings</summary>
        ''' <param name="initializeBindings">True to initialize <see cref="InputBindings"/> and <see cref="CommandBindings"/>; false to just clear them</param>
        Protected Sub New(initializeBindings As Boolean)
            InputBindings.Clear()
            If initializeBindings Then
                InputBindings.Add(New FreeInputBinding() With {.Command = UICommands.ScrollDown, .Gesture = UICommands.ScrollDown.InputGestures(0)})
                InputBindings.Add(New FreeInputBinding() With {.Command = UICommands.ScrollUp, .Gesture = UICommands.ScrollUp.InputGestures(0)})
                InputBindings.Add(New FreeInputBinding() With {.Command = UICommands.ScrollRight, .Gesture = UICommands.ScrollRight.InputGestures(0)})
                InputBindings.Add(New FreeInputBinding() With {.Command = UICommands.ScrollLeft, .Gesture = UICommands.ScrollLeft.InputGestures(0)})
            End If

            CommandBindings.Clear()
            If initializeBindings Then
                CommandBindings.Add(New CommandBinding(UICommands.ScrollDown, AddressOf OnScrollDown, AddressOf CanScrollDown))
                CommandBindings.Add(New CommandBinding(UICommands.ScrollUp, AddressOf OnScrollUp, AddressOf CanScrollUp))
                CommandBindings.Add(New CommandBinding(UICommands.ScrollRight, AddressOf OnScrollRight, AddressOf CanScrollRight))
                CommandBindings.Add(New CommandBinding(UICommands.ScrollLeft, AddressOf OnScrollLeft, AddressOf CanScrollLeft))
            End If
        End Sub

        Protected Overrides Sub OnMouseWheel(e As MouseWheelEventArgs)
            'MyBase.OnMouseWheel(e)
        End Sub

#Region "ScrollDown"
        ''' <summary>Called to detect if the <see cref="ComponentCommands.ScrollPageDown"/> command can be executed</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        Private Sub CanScrollDown(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            e.CanExecute = CanScrollDown()
        End Sub
        ''' <summary>Indicates if this <see cref="ScrollViewer"/> can scroll down</summary>
        ''' <returns></returns>
        Protected Overridable Function CanScrollDown() As Boolean
            Return ScrollInfo IsNot Nothing AndAlso ScrollInfo.CanVerticallyScroll AndAlso ScrollInfo.VerticalOffset < ScrollInfo.ExtentHeight
        End Function
        ''' <summary>Called to execute the <see cref="ComponentCommands.ScrollPageDown"/> command</summary>
        ''' <param name="sender">Event source</param>
        ''' <param name="e">Event arguments</param>
        Private Sub OnScrollDown(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            OnScrollDown(e)
        End Sub
        ''' <summary>Performs scrolling down</summary>
        Protected Overridable Sub OnScrollDown(e As ExecutedRoutedEventArgs)
               If Me.ScrollInfo IsNot Nothing Then Me.ScrollInfo.MouseWheelDown()
        End Sub
#End Region

#Region "ScrollUp"
        ''' <summary>Called to detect if the <see cref="ComponentCommands.ScrollPageUp"/> command can be executed</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        Private Sub CanScrollUp(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            e.CanExecute = CanScrollUp()
        End Sub
        ''' <summary>Indicates if this <see cref="ScrollViewer"/> can scroll up</summary>
        ''' <returns></returns>
        Protected Overridable Function CanScrollUp() As Boolean
            Return ScrollInfo IsNot Nothing AndAlso ScrollInfo.CanVerticallyScroll AndAlso ScrollInfo.VerticalOffset > 0
        End Function
        ''' <summary>Called to execute the <see cref="ComponentCommands.ScrollPageUp"/> command</summary>
        ''' <param name="sender">Event source</param>
        ''' <param name="e">Event arguments</param>
        Private Sub OnScrollUp(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            OnScrollUp(e)
        End Sub
        ''' <summary>Performs scrolling up</summary>
        Protected Overridable Sub OnScrollUp(e As ExecutedRoutedEventArgs)
            If ScrollInfo IsNot Nothing Then ScrollInfo.MouseWheelUp()
        End Sub
#End Region

#Region "ScrollRight"
        ''' <summary>Called to detect if the <see cref="ComponentCommands.ScrollPageRight"/> command can be executed</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        Private Sub CanScrollRight(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            e.CanExecute = CanScrollRight()
        End Sub
        ''' <summary>Indicates if this <see cref="ScrollViewer"/> can scroll right</summary>
        ''' <returns></returns>
        Protected Overridable Function CanScrollRight() As Boolean
            Return ScrollInfo IsNot Nothing AndAlso ScrollInfo.CanHorizontallyScroll AndAlso ScrollInfo.HorizontalOffset < ScrollInfo.ExtentWidth
        End Function
        ''' <summary>Called to execute the <see cref="ComponentCommands.ScrollPageRight"/> command</summary>
        ''' <param name="sender">Event source</param>
        ''' <param name="e">Event arguments</param>
        Private Sub OnScrollRight(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            OnScrollRight(e)
        End Sub
        ''' <summary>Performs scrolling right</summary>
        Protected Overridable Sub OnScrollRight(e As ExecutedRoutedEventArgs)
            If ScrollInfo IsNot Nothing Then ScrollInfo.MouseWheelRight()
        End Sub
#End Region

#Region "ScrollLeft"
        ''' <summary>Called to detect if the <see cref="ComponentCommands.ScrollPageLeft"/> command can be executed</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        Private Sub CanScrollLeft(ByVal sender As Object, ByVal e As CanExecuteRoutedEventArgs)
            e.CanExecute = CanScrollLeft()
        End Sub
        ''' <summary>Indicates if this <see cref="ScrollViewer"/> can scroll left</summary>
        ''' <returns></returns>
        Protected Overridable Function CanScrollLeft() As Boolean
            Return ScrollInfo IsNot Nothing AndAlso ScrollInfo.CanHorizontallyScroll AndAlso ScrollInfo.HorizontalOffset > 0
        End Function
        ''' <summary>Called to execute the <see cref="ComponentCommands.ScrollPageLeft"/> command</summary>
        ''' <param name="sender">Event source</param>
        ''' <param name="e">Event arguments</param>
        Private Sub OnScrollLeft(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            OnScrollLeft(e)
        End Sub
        ''' <summary>Performs scrolling left</summary>
        Protected Overridable Sub OnScrollLeft(e As ExecutedRoutedEventArgs)
            If ScrollInfo IsNot Nothing Then ScrollInfo.MouseWheelLeft()
        End Sub
#End Region

    End Class
End Namespace
