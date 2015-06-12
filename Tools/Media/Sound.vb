Imports Tools.ExtensionsT, System.Media, Tools.ComponentModelT, Tools.DataStructuresT.GenericT
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Drawing, System.CodeDom
Imports System.ComponentModel.Design.Serialization
Imports Tools.DrawingT.DesignT
Imports Tools.MediaT.Sound

'TODO: Test how it behaves in PropertyGrid for MessageBox
'TODO: Verify codedom serialization behaviour
#If Config <= Nightly Then 'Stage: Nightly
Namespace MediaT
    ''' <summary>Abstract class that represents short sound to be played</summary>
    ''' <remarks>This class does not provide seeking and stoping capabilities. It is intended to be used with short sounds.
    ''' <para>Provided type editor can convert names of system sounds and any URI to instance of <see cref="Sound"/>.</para>
    ''' </remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    <TypeConverter(GetType(Sound.SoundTypeConverter))>
    <Editor(GetType(Tools.DrawingT.DesignT.DropDownControlEditor(Of Sound, SoundDropDownList)), GetType(UITypeEditor))>
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
            PlayOnBackground(TryCast(Nothing, Action(Of Sound)), times)
        End Sub
        ''' <summary>Plays sound on background</summary>
        ''' <param name="times">Defines how many times shound will be played</param>
        ''' <param name="Callback">Callback method that will be called after playing is finished. Can be null.</param>
        Public Sub PlayOnBackground(ByVal Callback As Action(Of Sound), Optional ByVal times As Integer = 1)
            PlayOnBackground(Of Object)(If(Callback IsNot Nothing, Callback.AddArgument(Of Object), Nothing), Nothing, times)
        End Sub
        ''' <summary>Plays sound on background</summary>
        ''' <param name="times">Defines how many times shound will be played</param>
        ''' <param name="Callback">Callback method that will be called after playing is finished. Can be nothing.</param>
        ''' <param name="CustomObject">Custom object passed to <paramref name="Callback"/> method</param>
        ''' <typeparam name="T">Type of custom object passed to <paramref name="Callback"/> method</typeparam>
        Public Overridable Sub PlayOnBackground(Of T)(ByVal Callback As Action(Of Sound, T), ByVal CustomObject As T, Optional ByVal times As Integer = 1)
            Dim bgw As New BackgroundWorker
            AddHandler bgw.DoWork, AddressOf Me.PlayOnBackground
            If Callback IsNot Nothing Then AddHandler bgw.RunWorkerCompleted, AddressOf OnPlayed
            Dim CallbackUnsafe As Action(Of Sound, Object) = Nothing
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
            Dim Callback As Action(Of Sound, Object) = arg(0)
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
            Implements ITypeConverter(Of InstanceDescriptor)
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

            ''' <summary>Performs conversion from type <see cref="InstanceDescriptor"/> to type <see cref="Sound"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to type <see cref="Sound"/></param>
            ''' <returns>Value of type <see cref="Sound"/> initialized by <paramref name="value"/></returns>
            Public Overloads Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As System.ComponentModel.Design.Serialization.InstanceDescriptor) As Sound Implements ITypeConverterFrom(Of System.ComponentModel.Design.Serialization.InstanceDescriptor).ConvertFrom
                Return value.Invoke
            End Function

            ''' <summary>Performs conversion from type <see cref="Sound"/> to type <see cref="InstanceDescriptor"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in type <see cref="InstanceDescriptor"/></returns>
            Public Overloads Function ConvertToInstanceDescriptor(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Sound) As System.ComponentModel.Design.Serialization.InstanceDescriptor Implements ITypeConverterTo(Of System.ComponentModel.Design.Serialization.InstanceDescriptor).ConvertTo
                If value Is Nothing Then Return Nothing
                If TypeOf value Is SoundPlayerWrapper Then
                    With DirectCast(value, SoundPlayerWrapper)
                        If .Player.SoundLocation.IsNullOrEmpty Then Throw New ArgumentException(ResourcesT.Exceptions.SystemSoundPlayerWrapperCanBeConvertedOnlyWhenItProvidesSoundLocation)
                        Return New InstanceDescriptor(GetType(SoundPlayerWrapper).GetConstructor(New Type() {GetType(String)}), New Object() { .Player.SoundLocation}, True)
                    End With
                ElseIf TypeOf value Is SystemSoundPlayer Then
                    Dim SSvalue As SystemSoundPlayer.KnownSystemSounds = DirectCast(value, SystemSoundPlayer)
                    Return New InstanceDescriptor(GetType(SystemSoundPlayer).GetConstructor(New Type() {GetType(SystemSoundPlayer.KnownSystemSounds)}), New Object() {SSvalue}, True)
                End If
                Throw New TypeMismatchException(ResourcesT.Exceptions.OnlySoundsOfTypeSystemSoundPlayerAndSoundPlayerWrapperAreSupported, value)
            End Function
        End Class
        ''' <summary>Implements editing control for editing <see cref="Sound"/> in <see cref="PropertyGrid"/></summary>
        Friend Class SoundDropDownList
            'TODO: Test behavior when Enter/Escape is pressed
            Inherits DropDownUITypeEditorControlBase(Of Sound)
            ''' <summary><see cref="ListBox"/> which contains values to chose between</summary>
            Private WithEvents lstList As New ListBox With {.TabIndex = 0}
            ''' <summary><see cref="Label"/> to browse for file</summary>
            Private WithEvents cmdBrowse As New Button With {.AutoSize = True, .AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink, .TabIndex = 0}
            ''' <summary>Button which plays the sound</summary>
            Private WithEvents cmdPlay As New Button With {.Image = My.Resources.play, .AutoSize = True, .AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink, .TabIndex = 1}
            ''' <summary>Layout panel for <see cref="cmdBrowse"/> and <see cref="cmdPlay"/></summary>
            Private tlbBottom As New TableLayoutPanel With {.ColumnCount = 2, .RowCount = 1, .TabIndex = 1}
            ''' <summary>Tooltip for <see cref="cmdPlay"/></summary>
            Private totToolTip As New ToolTip
            ''' <summary>Contains value of the <see cref="Value"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Value As Sound
            ''' <summary>CTor</summary>
            Public Sub New()
                lstList.DisplayMember = "Value2"
                lstList.Items.AddRange(New Object() {
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Asterisk, ResourcesT.Components.Asterisk),
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Beep, ResourcesT.Components.Beep),
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Exclamation, ResourcesT.Components.Exclamation),
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Hand, ResourcesT.Components.Hand),
                    New Pair(Of SystemSoundPlayer, String)(SystemSounds.Question, ResourcesT.Components.Question)
                })
                cmdBrowse.Text = ResourcesT.Components.Browse
                lstList.Dock = DockStyle.Fill
                lstList.ClientSize = New Size(lstList.ClientSize.Width, lstList.ItemHeight * lstList.Items.Count)
                Me.AutoSize = True
                Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
                cmdBrowse.Anchor = AnchorStyles.Left
                tlbBottom.Dock = DockStyle.Bottom
                tlbBottom.AutoSize = True
                tlbBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
                If tlbBottom.RowStyles.Count < 1 Then tlbBottom.RowStyles.Add(New RowStyle)
                tlbBottom.RowStyles(0).SizeType = SizeType.AutoSize
                If tlbBottom.ColumnStyles.Count < 1 Then tlbBottom.ColumnStyles.Add(New ColumnStyle)
                tlbBottom.ColumnStyles(0).SizeType = SizeType.AutoSize
                If tlbBottom.ColumnStyles.Count < 2 Then tlbBottom.ColumnStyles.Add(New ColumnStyle)
                tlbBottom.ColumnStyles(1).SizeType = SizeType.Percent
                tlbBottom.ColumnStyles(1).Width = 100
                cmdPlay.Anchor = AnchorStyles.Right
                tlbBottom.Controls.Add(cmdBrowse, 0, 0)
                tlbBottom.Controls.Add(cmdPlay, 1, 0)
                Me.BackColor = SystemColors.Window
                Me.Controls.Add(lstList)
                Me.Controls.Add(tlbBottom)
                totToolTip.SetToolTip(cmdPlay, ResourcesT.Components.Play)
            End Sub
            ''' <summary>Owner of control informs control that it is about to be shown by calling this methos. It is called just befiore the control is shown.</summary>
            Protected Overrides Sub OnBeforeShow()
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
            ''' <summary>If there is any selected system sound passest it to <see cref="Value"/>.</summary>
            Protected Overrides Sub UpdateValue()
                If lstList.SelectedIndex <> -1 Then Value = DirectCast(lstList.SelectedItem, Pair(Of SystemSoundPlayer, String)).Value1
            End Sub

            Private Sub lblBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
                Dim ofd As New OpenFileDialog With {.Title = ResourcesT.Components.SelectWavFile, .Filter = ResourcesT.Components.WavFilter}
                If ofd.ShowDialog = DialogResult.OK Then
                    Dim val As SoundPlayerWrapper
                    Try
                        val = New SoundPlayer(ofd.FileName)
                    Catch ex As Exception
                        'WindowsT.IndependentT.MessageBox.[Error_X](ex)
                        MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType)
                        Result = False
                        Exit Sub
                    End Try
                    lstList.SelectedIndex = -1
                    Value = val
                    Result = True
                    Service.CloseDropDown()
                Else
                    Result = False
                    Service.CloseDropDown()
                End If
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

            ''' <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
            ''' <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
            Protected Overrides Sub OnGotFocus(ByVal e As System.EventArgs)
                lstList.Select()
            End Sub
        End Class
    End Class
    ''' <summary>Implements <see cref="Sound"/> which plays given <see cref="SystemSound"/></summary>
    ''' <completionlist cref="System.Media.SystemSounds"/>
    <DebuggerDisplay("{ToString}")>
    Public NotInheritable Class SystemSoundPlayer : Inherits Sound
        ''' <summary><see cref="SystemSound"/> to be played</summary>
        Friend ReadOnly SystemSound As SystemSound
        ''' <summary>CTor</summary>
        ''' <param name="SystemSound">System sound to be played</param>
        ''' <exception cref="ArgumentNullException"><paramref name="SystemSound"/> is null</exception>
        Public Sub New(ByVal SystemSound As SystemSound)
            If SystemSound Is Nothing Then [DoThrow](New ArgumentNullException("SystemSound"))
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
        ''' <summary>Represents known system sounds as defined by the <see cref="SystemSounds"/> class</summary>
        Public Enum KnownSystemSounds
            ''' <summary>Sound associated with the Asterisk program event in the current Windows sound scheme</summary>
            Asterisk = &H40
            ''' <summary>Sound associated with the Beep program event in the current Windows sound scheme.</summary>
            Beep = 0
            ''' <summary>Sound associated with the Exclamation program event in the current Windows sound scheme.</summary>
            Exclamation = &H30
            ''' <summary>Sound associated with the Hand program event in the current Windows sound scheme.</summary>
            Hand = &H10
            ''' <summary>Sound associated with the Question program event in the current Windows sound scheme.</summary>
            Question = &H20
        End Enum
        ''' <summary>CTor from <see cref="KnownSystemSounds"/></summary>
        ''' <param name="known">One of <see cref="KnownSystemSounds"/> values</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="known"/> is not one of <see cref="KnownSystemSounds"/> values</exception>
        Public Sub New(ByVal known As KnownSystemSounds)
            Select Case known
                Case KnownSystemSounds.Asterisk : Me.SystemSound = SystemSounds.Asterisk
                Case KnownSystemSounds.Beep : Me.SystemSound = SystemSounds.Beep
                Case KnownSystemSounds.Exclamation : Me.SystemSound = SystemSounds.Exclamation
                Case KnownSystemSounds.Hand : Me.SystemSound = SystemSounds.Hand
                Case KnownSystemSounds.Question : Me.SystemSound = SystemSounds.Question
                Case Else : Throw New InvalidEnumArgumentException("known", known, known.GetType)
            End Select
        End Sub
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <exception cref="T:System.NullReferenceException">The 
        ''' <paramref name="obj" /> parameter is null.</exception>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim SS As SystemSound = Nothing
            If obj IsNot Nothing AndAlso TypeOf obj Is SystemSoundPlayer Then
                SS = DirectCast(obj, SystemSoundPlayer).SystemSound
            ElseIf obj IsNot Nothing AndAlso TypeOf obj Is SystemSound Then
                SS = obj
            End If
            If SS IsNot Nothing Then
                Return SS Is Me.SystemSound
            End If
            Return MyBase.Equals(obj)
        End Function
        ''' <summary>Converts <see cref="SystemSoundPlayer"/> to <see cref="KnownSystemSounds"/></summary>
        ''' <param name="a">A <see cref="SystemSoundPlayer"/></param>
        ''' <remarks>A <see cref="KnownSystemSounds"/> value that represents <paramref name="a"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
        ''' <exception cref="InvalidCastException"><paramref name="a"/> doesn't represent one of <see cref="SystemSounds"/> values. This situation may hardly ocure.</exception>
        Public Overloads Shared Widening Operator CType(ByVal a As SystemSoundPlayer) As KnownSystemSounds
            If a Is Nothing Then Throw New ArgumentNullException("a")
            If a.SystemSound Is SystemSounds.Asterisk Then : Return KnownSystemSounds.Asterisk
            ElseIf a.SystemSound Is SystemSounds.Beep Then : Return KnownSystemSounds.Beep
            ElseIf a.SystemSound Is SystemSounds.Exclamation Then : Return KnownSystemSounds.Exclamation
            ElseIf a.SystemSound Is SystemSounds.Hand Then : Return KnownSystemSounds.Hand
            ElseIf a.SystemSound Is SystemSounds.Question Then : Return KnownSystemSounds.Question
            Else : Throw New InvalidCastException(String.Format(ResourcesT.Exceptions.CannotConvertGiven0To1BecauseItDoesNotRepresentKnown2, "SystemSoundPlayer", "KnownSystemSounds", "SystemSound"))
            End If
        End Operator
        ''' <summary>Converts <see cref="KnownSystemSounds"/> value to <see cref="SystemSoundPlayer"/></summary>
        ''' <param name="a">A <see cref="KnownSystemSounds"/> value</param>
        ''' <returns><see cref="SystemSoundPlayer"/> initialized with <paramref name="a"/></returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="a"/> is not member of <see cref="KnownSystemSounds"/></exception>
        Public Overloads Shared Narrowing Operator CType(ByVal a As KnownSystemSounds) As SystemSoundPlayer
            Return New SystemSoundPlayer(a)
        End Operator
    End Class
    ''' <summary>Wraps <see cref="SoundPlayer"/> as <see cref="Sound"/></summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
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
            Me.New(New SoundPlayer(ThrowOnNull(Stream)))
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