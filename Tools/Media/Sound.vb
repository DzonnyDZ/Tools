Imports Tools.ExtensionsT, System.Media, Tools.ComponentModelT, Tools.DataStructuresT.GenericT
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Drawing

'TODO: Test how it behaves in PropertyGrid for MessageBox
'TODO: CodeDom serializer
#If Config <= Nightly Then 'Stage: Nightly
Namespace MediaT
    ''' <summary>Abstract class that represents short sound to be played</summary>
    ''' <remarks>This class does not provide seeking and stoping capabilities. It is intended to be used with short sounds.
    ''' <para>Provided type editor can convert names of system sounds and any URI to instance of <see cref="Sound"/>.</para>
    ''' </remarks>
    <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(Sound)), FirstVersion(2008, 6, 21)> _
    <TypeConverter(GetType(Sound.SoundTypeConverter))> _
    <Editor(GetType(Tools.DrawingT.DesignT.DropDownControlEditor(Of Sound, SoundDropDownList)), GetType(UITypeEditor))> _
    Public MustInherit Class Sound
        ''' <summary>When overriden in derived class plays sound. Returns when sound is finished</summary>
        ''' <remarks>Note for inheritors: If you do not override <see cref="PlayOnBackground"/> function this function is called when <see cref="PlayOnBackground"/> is invoked. In such case this function is called in another thread that object was created in.</remarks>
        Public MustOverride Sub Play()
        ''' <summary>Plays shound given number of times. Returns after all plays are finished.</summary>
        ''' <param name="times">Defines how many times shound will be played</param>
        Public Overridable Sub Play(ByVal times As Integer)
            For i = 0 To times - 1
                Play()
            Next
        End Sub
        ''' <summary>Plays sound on background</summary>
        ''' <param name="times">Defines how many times shound will be played</param>
        Public Sub PlayOnBackground(Optional ByVal times As Integer = 1)
            PlayOnBackground(TryCast(Nothing, dSub(Of Sound)), times)
        End Sub
        ''' <summary>Plays sound on background</summary>
        ''' <param name="times">Defines how many times shound will be played</param>
        ''' <param name="Callback">Callback method that will be called after playing is finished. Can be null.</param>
        Public Sub PlayOnBackground(ByVal Callback As dSub(Of Sound), Optional ByVal times As Integer = 1)
            PlayOnBackground(Of Object)(If(Callback IsNot Nothing, Callback.AddArgument(Of Object), Nothing), Nothing, times)
        End Sub
        ''' <summary>Plays sound on background</summary>
        ''' <param name="times">Defines how many times shound will be played</param>
        ''' <param name="Callback">Callback method that will be called after playing is finished. Can be nothing.</param>
        ''' <param name="CustomObject">Custom object passed to <paramref name="Callback"/> method</param>
        ''' <typeparam name="T">Type of custom object passed to <paramref name="Callback"/> method</typeparam>
        Public Overridable Sub PlayOnBackground(Of T)(ByVal Callback As dSub(Of Sound, T), ByVal CustomObject As T, Optional ByVal times As Integer = 1)
            Dim bgw As New BackgroundWorker
            AddHandler bgw.DoWork, AddressOf Me.PlayOnBackground
            If Callback IsNot Nothing Then AddHandler bgw.RunWorkerCompleted, AddressOf OnPlayed
            Dim CallbackUnsafe As dSub(Of Sound, Object) = Nothing
            If Callback IsNot Nothing Then CallbackUnsafe = AddressOf Callback.Invoke
            bgw.RunWorkerAsync(New Object() {CallbackUnsafe, times, CustomObject})
        End Sub
        ''' <summary>Called asynchronously when sound is about to be played at background</summary>
        ''' <param name="sender"><see cref="BackgroundWorker"/> which realizes background thread</param>
        ''' <param name="e">Event arguments. <paramref name="e"/>.<see cref="DoWorkEventArgs.Argument">Argument</see> contains 1D array with callback method, number of times to repeat the sound and custom return object</param>
        Private Sub PlayOnBackground(ByVal sender As BackgroundWorker, ByVal e As DoWorkEventArgs)
            Dim arg As Object() = e.Argument
            Dim times As Integer = arg(1)
            Play(times)
            e.Result = e.Argument
        End Sub
        ''' <summary>Called when asynchronous sound playing is finished</summary>
        ''' <param name="sender"><see cref="BackgroundWorker"/> which realized background thread</param>
        ''' <param name="e">Event arguments. <paramref name="e"/>.<see cref="RunWorkerCompletedEventArgs.Result">Result</see> contains 1D array with callback method, number of times to repeat the sound and custom return object</param>
        Private Sub OnPlayed(ByVal sender As BackgroundWorker, ByVal e As RunWorkerCompletedEventArgs)
            Dim arg As Object() = e.Result
            Dim Callback As dSub(Of Sound, Object) = arg(0)
            Dim CustomObject As Object = arg(2)
            If Callback IsNot Nothing Then
                Callback.Invoke(Me, CustomObject)
            End If
            sender.Dispose()
        End Sub
        ''' <summary>Converts <see cref="SystemSound"/> to <see cref="Sound"/></summary>
        ''' <param name="a">A <see cref="SystemSound"/></param>
        ''' <returns>New instance of <see cref="SystemSoundPlayer"/> initialized with <paramref name="a"/>; returs null if <paramref name="a"/> is null</returns>
        Public Shared Widening Operator CType(ByVal a As SystemSound) As Sound
            If a Is Nothing Then Return Nothing
            Return New SystemSoundPlayer(a)
        End Operator
        ''' <summary>Implements type convertor for <see cref="Sound"/> and <see cref="String"/></summary>
        Friend Class SoundTypeConverter
            Inherits TypeConverter(Of Sound, String)

            ''' <summary>Performs conversion from type <see cref="String"/> to type <see cref="Sound"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to type <see cref="Sound"/></param>
            ''' <returns>Value of type <see cref="Sound"/> initialized by <paramref name="value"/>; null if <paramref name="value"/> is null or <see cref="System.String.Empty"/></returns>
            Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As Sound
                If value.IsNullOrEmpty Then Return Nothing
                Select Case value
                    Case "Asterisk" : Return SystemSounds.Asterisk
                    Case "Beep" : Return SystemSounds.Beep
                    Case "Exclamation" : Return SystemSounds.Exclamation
                    Case "Hand" : Return SystemSounds.Hand
                    Case "Question" : Return SystemSounds.Question
                    Case Else : Return New SoundPlayerWrapper(value)
                End Select
            End Function

            ''' <summary>Performs conversion from type <see cref="Sound"/> to type <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in type <see cref="String"/>; null if <paramref name="value"/> is null</returns>
            ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither <see cref="SystemSoundPlayer"/> nor <see cref="SoundPlayerWrapper"/></exception>
            ''' <exception cref="ArgumentException"><paramref name="value"/> is <see cref="SystemSoundPlayer"/> but it does represent any of <see cref="SystemSounds"/> =or= <paramref name="value"/> is <see cref="SoundPlayerWrapper"/> and <paramref name="value"/>.<see cref="SoundPlayerWrapper.Player">Player</see>.<see cref="SoundPlayer.SoundLocation">SoundLocation</see> is null or <see cref="System.String.Empty"/>.</exception>
            Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Sound) As String
                If value Is Nothing Then Return Nothing
                If TypeOf value Is SystemSoundPlayer Then
                    With DirectCast(value, SystemSoundPlayer)
                        If .SystemSound Is SystemSounds.Asterisk Then : Return "Asterisk"
                        ElseIf .SystemSound Is SystemSounds.Beep Then : Return "Beep"
                        ElseIf .SystemSound Is SystemSounds.Exclamation Then : Return "Exclamation"
                        ElseIf .SystemSound Is SystemSounds.Hand Then : Return "Hand"
                        ElseIf .SystemSound Is SystemSounds.Question Then : Return "Question"
                        Else : Throw New ArgumentException(ResourcesT.Exceptions.GivenValueIsNotKnownSystemSound, "value")
                        End If
                    End With
                ElseIf TypeOf value Is SoundPlayerWrapper Then
                    With DirectCast(value, SoundPlayerWrapper)
                        If Not .Player.SoundLocation.IsNullOrEmpty Then
                            Return .Player.SoundLocation
                        Else
                            Throw New ArgumentException(ResourcesT.Exceptions.OnlySoundsWithKnownLocationCanBeConverted, "value")
                        End If
                    End With
                End If
                Throw New TypeMismatchException(ResourcesT.Exceptions.OnlySoundsOfTypeSystemSoundPlayerAndSoundPlayerWrapperAreSupported, value)
            End Function
        End Class
        ''' <summary>Implements editing control for editing <see cref="Sound"/> in <see cref="PropertyGrid"/></summary>
        Friend Class SoundDropDownList
            Inherits System.Windows.Forms.UserControl
            Implements DrawingT.DesignT.IEditor(Of Sound)
            ''' <summary>Contains value of the <see cref="Context"/> property</summary>
            Private _Context As System.ComponentModel.ITypeDescriptorContext
            ''' <summary><see cref="ListBox"/> which contains values to chose between</summary>
            Private WithEvents lstList As New ListBox
            ''' <summary><see cref="Label"/> to browse for file</summary>
            Private WithEvents lblBrowse As New Label
            ''' <summary>Button which plays the sound</summary>
            Private WithEvents cmdPlay As New Button With {.Image = My.Resources.Play}
            ''' <summary>Layout panel for <see cref="lblBrowse"/> and <see cref="cmdPlay"/></summary>
            Private tlbBottom As New TableLayoutPanel With {.ColumnCount = 2, .RowCount = 1}
            ''' <summary>Contains value of the <see cref="Value"/> property</summary>
            Private _Value As Sound
            ''' <summary>CTor</summary>
            Public Sub New()
                lstList.DisplayMember = "Value2"
                lstList.Items.AddRange(New Object() { _
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Asterisk, ResourcesT.Components.Asterisk), _
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Beep, ResourcesT.Components.Beep), _
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Exclamation, ResourcesT.Components.Exclamation), _
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Hand, ResourcesT.Components.Hand), _
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Question, ResourcesT.Components.Question) _
                })
                lblBrowse.Text = ResourcesT.Components.Browse
                lstList.Dock = DockStyle.Fill
                lblBrowse.Anchor = AnchorStyles.Left
                tlbBottom.Dock = DockStyle.Bottom
                tlbBottom.AutoSize = True
                tlbBottom.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
                tlbBottom.RowStyles(0).SizeType = SizeType.AutoSize
                tlbBottom.ColumnStyles(0).SizeType = SizeType.AutoSize
                tlbBottom.ColumnStyles(1).SizeType = SizeType.Percent
                tlbBottom.ColumnStyles(1).Width = 100
                cmdPlay.Anchor = AnchorStyles.Right
                tlbBottom.Controls.Add(lblBrowse, 0, 0)
                tlbBottom.Controls.Add(lblBrowse, 1, 0)
                Me.BackColor = SystemColors.Window
                Me.Controls.Add(lstList)
                Me.Controls.Add(tlbBottom)
            End Sub
            ''' <summary>Stores context of current editing session</summary>
            ''' <remarks>This property is set by owner of the control and is valid between calls of <see cref="OnBeforeShow"/> and <see cref="OnClosed"/>.</remarks>
            Private Property Context() As System.ComponentModel.ITypeDescriptorContext Implements DrawingT.DesignT.IEditor(Of Sound).Context
                Get
                    Return _Context
                End Get
                Set(ByVal value As System.ComponentModel.ITypeDescriptorContext)
                    _Context = value
                End Set
            End Property

            ''' <summary>Owner of control informs control that it is about to be shown by calling this methos. It is called just befiore the control is shown.</summary>
            Private Sub OnBeforeShow() Implements DrawingT.DesignT.IEditor(Of Sound).OnBeforeShow
                lstList.SelectedIndex = -1
                If Value IsNot Nothing AndAlso TypeOf Value Is SystemSoundPlayer Then
                    Dim i As Integer = 0
                    For Each item As Pair(Of SystemSoundPlayer, String) In lstList.Items
                        If item.Value1.SystemSound Is Value Then
                            lstList.SelectedIndex = i
                            Exit For
                        End If
                        i += 1
                    Next item
                End If
            End Sub

            ''' <summary>Informs control that it was just hidden by calling this method.</summary>
            ''' <remarks>When implementing editor for reference type that is edited by changin its properties instead of changing its instance. Properties shouldbe changed in this method and onyl if <see cref="Result"/> is true.</remarks>
            Private Sub OnClosed() Implements DrawingT.DesignT.IEditor(Of Sound).OnClosed
                'Do nothing
            End Sub
            ''' <summary>Contains value of the <see cref="Result"/> property</summary>
            Private _Result As Boolean
            ''' <summary>Stores editing result</summary>
            ''' <returns>True if editing was terminated with success, false if it was canceled</returns>
            ''' <remarks>This property is set by owner of the control and is valid when and after <see cref="OnClosed"/> is called</remarks>
            Private Property Result() As Boolean Implements DrawingT.DesignT.IEditor(Of Sound).Result
                Get
                    Return _Result
                End Get
                Set(ByVal value As Boolean)
                    _Result = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Service"/> property</summary>
            Private _Service As System.Windows.Forms.Design.IWindowsFormsEditorService
            ''' <summary>Stores <see cref="System.Windows.Forms.Design.IWindowsFormsEditorService"/> valid for current editing session</summary>
            ''' <remarks>This property is set by owner of the control and is valid between calls of <see cref="OnBeforeShow"/> and <see cref="OnClosed"/>.</remarks>
            Private Property Service() As System.Windows.Forms.Design.IWindowsFormsEditorService Implements DrawingT.DesignT.IEditor(Of Sound).Service
                Get
                    Return _Service
                End Get
                Set(ByVal value As System.Windows.Forms.Design.IWindowsFormsEditorService)
                    _Service = value
                End Set
            End Property


            ''' <summary>Gets or sets edited value</summary>
            Public Property Value() As Sound Implements DrawingT.DesignT.IEditor(Of Sound).Value
                Get
                    Return _Value
                End Get
                Set(ByVal value As Sound)
                    _Value = value
                End Set
            End Property

            Private Sub lblBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblBrowse.Click
                Dim ofd As New OpenFileDialog With {.Title = ResourcesT.Components.SelectWavFile, .Filter = ResourcesT.Components.WavFilter}
                If ofd.ShowDialog Then
                    Dim val As SoundPlayerWrapper
                    Try
                        val = New SoundPlayer(ofd.FileName)
                    Catch ex As Exception
                        WindowsT.IndependentT.MessageBox.Error(ex)
                        Result = False
                        Exit Sub
                    End Try
                    Value = val
                    Result = True
                    Service.CloseDropDown()
                Else
                    Result = False
                    Service.CloseDropDown()
                End If
            End Sub

            Private Sub lstList_KeyDown(ByVal sender As ListBox, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstList.KeyDown
                Select Case e.KeyCode
                    Case Keys.Enter
                        If sender.SelectedItem Is Nothing Then
                            Value = Nothing
                        Else
                            Value = DirectCast(sender.SelectedItem, Pair(Of SystemSoundPlayer, String)).Value1
                        End If
                        Result = True
                        Service.CloseDropDown()
                    Case Keys.Escape
                        Result = False
                        Service.CloseDropDown()
                End Select
            End Sub

            Private Sub lstList_MouseClick(ByVal sender As ListBox, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstList.MouseClick
                If sender.SelectedItem Is Nothing Then
                    Value = Nothing
                Else
                    Value = DirectCast(sender.SelectedItem, Pair(Of SystemSoundPlayer, String)).Value1
                End If
                Result = True
                Service.CloseDropDown()
            End Sub

            Private Sub cmdPlay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPlay.Click
                If Value IsNot Nothing Then Value.PlayOnBackground()
            End Sub
        End Class
    End Class
    ''' <summary>Implements <see cref="Sound"/> which plays given <see cref="SystemSound"/></summary>
    ''' <completionlist cref="SystemSounds"/>
    ''' 
    <DebuggerDisplay("{ToString}")> _
    Public NotInheritable Class SystemSoundPlayer : Inherits Sound
        ''' <summary><see cref="SystemSound"/> to be played</summary>
        Friend ReadOnly SystemSound As SystemSound
        ''' <summary>CTor</summary>
        ''' <param name="SystemSound">System sound to be played</param>
        ''' <exception cref="ArgumentNullException"><paramref name="SystemSound"/> is null</exception>
        Public Sub New(ByVal SystemSound As SystemSound)
            If SystemSound Is Nothing Then [Throw](New ArgumentNullException("SystemSound"))
            Me.SystemSound = SystemSound
        End Sub
        ''' <summary>Plays system sound represented by this instance</summary>
        Public Overloads Overrides Sub Play()
            Me.SystemSound.Play()
        End Sub
        ''' <summary>Converts <see cref="System.Media.SystemSound"/> to <see cref="SystemSoundPlayer"/></summary>
        ''' <param name="a">A <see cref="System.Media.SystemSound"/></param>
        ''' <returns>New instance of <see cref="SystemSoundPlayer"/> initialized with <paramref name="a"/>; returs null if <paramref name="a"/> is null</returns>
        Public Overloads Shared Widening Operator CType(ByVal a As SystemSound) As SystemSoundPlayer
            If a Is Nothing Then Return Nothing
            Return New SystemSoundPlayer(a)
        End Operator
        ''' <summary>Converts <see cref="SystemSoundPlayer"/> to <see cref="System.Media.SystemSound"/></summary>
        ''' <param name="a">A <see cref="SystemSoundPlayer"/></param>
        ''' <returns><see cref="System.Media.SystemSound"/> represented by <paramref name="a"/>; returns null if <paramref name="a"/> is null</returns>
        Public Overloads Shared Widening Operator CType(ByVal a As SystemSoundPlayer) As SystemSound
            If a Is Nothing Then Return Nothing
            Return a.SystemSound
        End Operator
        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            'Do not localize!
            If SystemSound Is SystemSounds.Asterisk Then : Return "Asterisk"
            ElseIf SystemSound Is SystemSounds.Beep Then : Return "Beep"
            ElseIf SystemSound Is SystemSounds.Exclamation Then : Return "Exclamation"
            ElseIf SystemSound Is SystemSounds.Hand Then : Return "Hand"
            ElseIf SystemSound Is SystemSounds.Question Then : Return "Question"
            Else : Return MyBase.ToString()
            End If
        End Function
    End Class
    ''' <summary>Wraps <see cref="SoundPlayer"/> as <see cref="Sound"/></summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class SoundPlayerWrapper
        Inherits Sound
        ''' <summary>Countains value of the <see cref="Player"/> property</summary>
        Private ReadOnly _Player As SoundPlayer
        ''' <summary>CTor from <see cref="SoundPlayer"/></summary>
        ''' <param name="Player"><see cref="SoundPlayer"/> to be wrapped</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Player"/> is null</exception>
        Public Sub New(ByVal Player As SoundPlayer)
            If Player Is Nothing Then Throw New ArgumentNullException("Player")
            Me._Player = Player
        End Sub
        ''' <summary>CTor from URI of sound file</summary>
        ''' <param name="Path">The location of a .wav file to load.</param>
        ''' <exception cref="System.UriFormatException">The URL value specified by <paramref name="Path"/> cannot be resolved.</exception>
        ''' <exception cref="System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="Player">Player</see>.<see cref="SoundPlayer.LoadTimeout">LoadTimeout</see> default value (10s).</exception>
        ''' <exception cref="System.IO.FileNotFoundException">The file specified by <paramref name="Path"/> cannot be found.</exception>
        Public Sub New(ByVal Path As String)
            Me.New(New SoundPlayer(Path))
            Player.Load()
        End Sub
        ''' <summary>CTor from wav stream</summary>
        ''' <param name="Stream">A <see cref="System.IO.Stream"/> to a .wav file.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Stream"/> is null</exception>
        Public Sub New(ByVal Stream As IO.Stream)
            Me.new(New SoundPlayer(ThrowOnNull(Stream)))
        End Sub
        ''' <summary>Throws <see cref="ArgumentNullException"/> if given <see cref="IO.Stream"/> is null</summary>
        ''' <param name="stream"><see cref="IO.Stream"/> to check</param>
        ''' <exception cref="ArgumentNullException"><paramref name="stream"/> is null</exception>
        ''' <returns><paramref name="stream"/></returns>
        Private Shared Function ThrowOnNull(ByVal stream As IO.Stream) As IO.Stream
            If stream Is Nothing Then Throw New ArgumentException
            Return stream
        End Function
        ''' <summary>Gets <see cref="SoundPlayer"/> wrapped by this instance</summary>
        ''' <returns>Sound player this instance wraps.</returns>
        Public ReadOnly Property Player() As SoundPlayer
            Get
                Return _Player
            End Get
        End Property
        ''' <summary>Plays sound. Returns when sound is finished</summary>
        Public Overloads Overrides Sub Play()
            Player.PlaySync()
        End Sub
        ''' <summary>Converts <see cref="SoundPlayer"/> to <see cref="SoundPlayerWrapper"/></summary>
        ''' <param name="a">A <see cref="SoundPlayer"/></param> 
        ''' <remarks>New instance of <see cref="SoundPlayerWrapper"/> initialized with <paramref name="a"/>; null if <paramref name="a"/> is null</remarks>
        Public Overloads Shared Widening Operator CType(ByVal a As SoundPlayer) As SoundPlayerWrapper
            If a Is Nothing Then Return Nothing
            Return New SoundPlayerWrapper(a)
        End Operator
        ''' <summary>Converts <see cref="SoundPlayerWrapper"/> to <see cref="SoundPlayer"/></summary>
        ''' <param name="a">A <see cref="SoundPlayerWrapper"/></param>
        ''' <remarks><paramref name="a"/>.<see cref="SoundPlayerWrapper.Player">Player</see>; returns null if <paramref name="a"/> is null.</remarks>
        Public Overloads Shared Widening Operator CType(ByVal a As SoundPlayerWrapper) As SoundPlayer
            If a Is Nothing Then Return Nothing
            Return a.Player
        End Operator
    End Class
End Namespace
#End If