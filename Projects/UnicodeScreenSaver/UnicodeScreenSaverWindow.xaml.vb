
Imports System.Timers

Class UnicodeScreenSaverWindow

    Private WithEvents timer As Timer

    Private Sub timer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles timer.Elapsed
        OnTimer
    End Sub

    Private Sub UnicodeScreenSaverWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        OnTimer
        timer = New Timer()
        timer.Interval = 10000
        timer.Start()
    End Sub

    Private Sub OnTimer()
        Me.Dispatcher.Invoke(Sub() DataContext = GetNextCharacter())
    End Sub

    Private ReadOnly rnd As New Random
    Private Function GetNextCharacter() As CharacterInfo
        Dim unicode = Config.GetCharacter(rnd.Next(Config.TotalCharacters - 1))
        Dim codePoint = Tools.TextT.UnicodeT.UnicodeCharacterDatabase.Default.FindCodePoint(unicode)
        Return New CharacterInfo(unicode, codePoint?.Name, Config.GetRange(unicode)?.Item3, codePoint?.Script, codePoint?.Block.Name)
    End Function

    Private Sub UnicodeScreenSaverWindow_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Key = Key.Right Then DataContext = GetNextCharacter() _
        Else End
    End Sub

    Private Sub UnicodeScreenSaverWindow_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDown
        End
    End Sub

    'Private Sub UnicodeScreenSaverWindow_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
    '    End
    'End Sub
End Class
