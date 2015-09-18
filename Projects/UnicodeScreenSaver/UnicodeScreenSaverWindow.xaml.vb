Imports System.Timers
Imports System.Windows.Forms
Imports Tools.TextT.UnicodeT

''' <summary>Main screensaver UI</summary>
Friend Class UnicodeScreenSaverWindow

    ''' <summary>Timer ticks to change the character displayed</summary>
    Private WithEvents timer As Timers.Timer

    Private Sub timer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles timer.Elapsed
        OnTimer()
    End Sub

    Private Sub UnicodeScreenSaverWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        OnTimer()
        timer = New Timers.Timer()
        timer.Interval = My.Settings.Interval.TotalMilliseconds
        timer.Start()
    End Sub

    ''' <summary>Called when time relapses</summary>
    Private Sub OnTimer()
        Me.Dispatcher.Invoke(Sub() DataContext = GetNextCharacter())
    End Sub

#Region "GetNextCharacter"
    ''' <summary>Random number generator for generating Unicode code points</summary>
    Private ReadOnly rnd As New Random

    ''' <summary>Retrieves next character to be displayed</summary>
    ''' <returns>Next character to be displayed</returns>
    Private Function GetNextCharacter() As CharacterInfo
        Dim unicode As UInt32
        Select Case My.Settings.SortingAlghoritm
            Case SortingAlghoritm.RandomCharacter : unicode = GetNextCharacterRandomCharacter()
            Case SortingAlghoritm.RandomCharacterPenaltyBigRanges : Throw New NotImplementedException  'TODO:
            Case SortingAlghoritm.RandomRangeRandomCharacter : unicode = GetNextCharacterRandomRangeRandomCharacter()
            Case SortingAlghoritm.RandomRangeRandomCharacterPenaltyBigRanges : Throw New NotImplementedException 'TODO:
            Case SortingAlghoritm.RandomRangeSequential : unicode = GetNextCharacterRandomRangeSequential()
            Case SortingAlghoritm.RandomRangeSequentialCharacterPenaltyBigRanges : Throw New NotImplementedException 'TODO:
            Case SortingAlghoritm.RandomRangeSequentialCharacterSplitBigRanges : unicode = GetNextCharacterRandomRangeSequentialCharacterSplitBigRanges()
            Case SortingAlghoritm.Sequantial : unicode = GetNextCharacterSequential()
            Case SortingAlghoritm.RandomRangeRandomCharacterSplitBigRanges :unicode = GetNextCharacterRandomRangeRandomCharacterSplitBigRanges
            Case Else : unicode = GetNextCharacterRandomCharacter() 'Random
        End Select

        Dim codePoint = UnicodeCharacterDatabase.Default.FindCodePoint(unicode)
        Return New CharacterInfo(unicode, codePoint?.Name, Config.GetRange(unicode)?.Item3, codePoint?.Script, codePoint?.Block.Name)
    End Function

    ''' <summary>Gets new character to be displayed when using <see cref="SortingAlghoritm.RandomCharacter"/></summary>
    ''' <returns>Unicode code point of next character to be displaced</returns>
    Private Function GetNextCharacterRandomCharacter() As UInteger
        Return Config.GetCharacter(rnd.Next(Config.TotalCharacters - 1))
    End Function

    Private function GetNextCharacterRandomRangeRandomCharacterSplitBigRanges As UInteger 
        Dim range = Config.UnicodeRangesSplit(rnd.Next(Config.UnicodeRanges.Length - 1))
        Return  range.GetCharacter(range.CharacterCount - 1)
    End function

    ''' <summary>Gets new character to be displayed when using <see cref="SortingAlghoritm.RandomRangeRandomCharacter"/></summary>
    ''' <returns>Unicode code point of next character to be displaced</returns>
    Private Function GetNextCharacterRandomRangeRandomCharacter() As UInteger
        Dim range = Config.UnicodeRanges(rnd.Next(Config.UnicodeRanges.Length - 1))
        Return  range.GetCharacter(range.CharacterCount - 1)
    End Function

    ''' <summary>Gets new character to be displayed when using <see cref="SortingAlghoritm.RandomRangeSequential"/></summary>
    ''' <returns>Unicode code point of next character to be displaced</returns>
    Private Function GetNextCharacterRandomRangeSequential() As UInteger
        Dim unicode As UInteger

        Dim myScreen = Array.IndexOf(Screen.AllScreens, Screen.FromRectangle(New System.Drawing.Rectangle(Me.Left, Me.Top, Me.Width, Me.Height)))
        Dim previous = If(My.Settings.LastCodePoint Is Nothing OrElse My.Settings.LastCodePoint.Length < myScreen, -1, My.Settings.LastCodePoint(myScreen))
        If previous >= 0 Then unicode = GetNextCharacterSequential(myScreen)
        Dim prevBlock = If(previous < 0, Nothing, UnicodeCharacterDatabase.Default.FindCodePoint(previous).Block)
        Dim currBlock = UnicodeCharacterDatabase.Default.FindCodePoint(unicode).Block
        If prevBlock Is Nothing OrElse prevBlock.FirstCodePoint <> currBlock.FirstCodePoint Then
            Dim range = Config.UnicodeRanges(rnd.Next(Config.UnicodeRanges.Length - 1))
            unicode = range.GetCharacter(0)
        End If
        My.Settings.LastCodePoint(myScreen) = unicode
        My.Settings.Save 
        Return unicode
    End Function

    ''' <summary>Gets new character to be displayed when using <see cref="SortingAlghoritm.RandomRangeSequentialCharacterSplitBigRanges"/></summary>
    ''' <returns>Unicode code point of next character to be displaced</returns>
    Private Function GetNextCharacterRandomRangeSequentialCharacterSplitBigRanges() As UInteger
        Dim unicode As UInteger

        Dim myScreen = Array.IndexOf(Screen.AllScreens, Screen.FromRectangle(New System.Drawing.Rectangle(Me.Left, Me.Top, Me.Width, Me.Height)))
        Dim previous = If(My.Settings.LastCodePoint Is Nothing OrElse My.Settings.LastCodePoint.Length < myScreen, -1, My.Settings.LastCodePoint(myScreen))
        If previous >= 0 Then unicode = GetNextCharacterSequential(myScreen)
        Dim prevBlock = If(previous < 0, Nothing, UnicodeCharacterDatabase.Default.FindCodePoint(previous).Block)
        Dim prevRange = (From r In Config.UnicodeRanges Where r.First.First  = prevBlock?.FirstCodePoint?.CodePoint).SingleOrDefault
        Dim currBlock = UnicodeCharacterDatabase.Default.FindCodePoint(unicode).Block
        Dim currRange = (From r In Config.UnicodeRanges Where r.First.First  = currBlock?.FirstCodePoint?.CodePoint).SingleOrDefault
        Dim prevIdx = prevRange?.GetCharIndex(previous)
        Dim currIdx = currRange?.GetCharIndex(unicode)
        If prevBlock Is Nothing OrElse prevBlock.FirstCodePoint <> currBlock.FirstCodePoint OrElse (prevBlock.FirstCodePoint = currBlock.FirstCodePoint AndAlso currRange.CharacterCount >= My.Settings.BigRangeTreshold AndAlso prevIdx \ My.Settings.RangeChunkSize <> currIdx \ My.Settings.RangeChunkSize) Then
            Dim range = Config.UnicodeRanges(rnd.Next(Config.UnicodeRanges.Length - 1))
            Dim idx = 0
            If range.CharacterCount >= My.Settings.RangeChunkSize Then
                idx = My.Settings.RangeChunkSize * rnd.Next(CInt(Math.Ceiling(range.CharacterCount / My.Settings.RangeChunkSize)))
            End If
            unicode = range.GetCharacter(idx)
        End If
        EnsureScreenConfig(myScreen)
        My.Settings.LastCodePoint(myScreen) = unicode
        My.Settings.Save 
        Return unicode
    End Function

    ''' <summary>Gets new character to be displayed when using <see cref="SortingAlghoritm.Sequential"/></summary>
    ''' <returns>Unicode code point of next character to be displaced</returns>
    Private Function GetNextCharacterSequential() As UInteger
        Dim unicode As UInteger

        Dim myScreen = Array.IndexOf(Screen.AllScreens, Screen.FromRectangle(New System.Drawing.Rectangle(Me.Left, Me.Top, Me.Width, Me.Height)))
        unicode = GetNextCharacterSequential(myScreen)
        My.Settings.LastCodePoint(myScreen) = unicode
        My.Settings.Save 
        Return unicode
    End Function

    ''' <summary>Gets sequentially next character for current screen</summary>
    ''' <param name="myScreen">0-base screen index</param>
    ''' <returns>Unicode code point of next character to be displaced</returns>
    Private Function GetNextCharacterSequential(myScreen As Integer) As UInteger
        Dim unicode As UInteger
        EnsureScreenConfig(myScreen)

        Dim lastIndex = Config.GetCharIndex(My.Settings.LastCodePoint(myScreen))
        If lastIndex = -1 OrElse lastIndex >= Config.TotalCharacters Then
            unicode = Config.GetCharacter(0)
        Else
            unicode = Config.GetCharacter(lastIndex + 1)
        End If
        Return unicode
    End Function

    ''' <summary>Makes sure <see cref="MySettings.LastCodePoint"/> contains index of given screen</summary>
    ''' <param name="myScreen">0-base screen index</param>
    Private Sub EnsureScreenConfig(myScreen As Integer)

        If My.Settings.LastCodePoint Is Nothing OrElse My.Settings.LastCodePoint.Length < myScreen Then
            Dim li = If(My.Settings.LastCodePoint?.Length, 0)
            ReDim Preserve My.Settings.LastCodePoint(0 To Screen.AllScreens.Length - 1)
            For i = li To My.Settings.LastCodePoint.Length - 1
                My.Settings.LastCodePoint(i) = -1
            Next
        End If
    End Sub
#End Region

    Private Sub UnicodeScreenSaverWindow_KeyDown(sender As Object, e As Input.KeyEventArgs) Handles Me.KeyDown
        If e.Key = Key.Right Then DataContext = GetNextCharacter() _
        Else End
    End Sub

    Private Sub UnicodeScreenSaverWindow_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseDown
        End
    End Sub
End Class
