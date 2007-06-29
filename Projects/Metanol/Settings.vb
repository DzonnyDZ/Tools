Imports Tools.CollectionsT.GenericT

Namespace My
    
    'This class allows you to handle specific events on the settings class:
    ' The SettingChanging event is raised before a setting's value is changed.
    ' The PropertyChanged event is raised after a setting's value is changed.
    ' The SettingsLoaded event is raised after the setting values are loaded.
    ' The SettingsSaving event is raised before the setting values are saved.
    Partial Friend NotInheritable Class MySettings

        Private Sub MySettings_SettingsLoaded(ByVal sender As Object, ByVal e As System.Configuration.SettingsLoadedEventArgs) Handles Me.SettingsLoaded
            If My.Settings.LastVer = "" Then
                My.Settings.Upgrade()
                My.Settings.LastVer = My.Application.Info.Version.ToString
                My.Settings.Save()
            End If
            AutoComplete.Clear()
            If Me.KwAutoComplete <> "" Then
                Dim LastI As Integer = 0
                Dim Bs As Boolean = False
                Dim Current As New System.Text.StringBuilder
                For i As Integer = 0 To Me.KwAutoComplete.Length - 1
                    If Not Bs AndAlso Me.KwAutoComplete(i) = "\"c AndAlso i < Me.KwAutoComplete.Length - 1 Then
                        Bs = True
                    ElseIf Me.KwAutoComplete(i) <> ","c OrElse Bs Then
                        Current.Append(Me.KwAutoComplete(i))
                        Bs = False
                    ElseIf Me.KwAutoComplete(i) = ","c Then
                        If Current.ToString.Trim <> "" Then
                            AutoComplete.Add(Current.ToString.Trim)
                        End If
                        Current = New System.Text.StringBuilder
                    End If
                Next i
                If Current.ToString.Trim <> "" Then
                    AutoComplete.Add(Current.ToString.Trim)
                End If
            End If

            Synonyms.Clear()
            If Me.KwSynonyms <> "" Then
                Dim Groups As String() = Me.KwSynonyms.Split("|"c)
                If Groups IsNot Nothing Then
                    For Each Group As String In Groups
                        Dim GroupParts As String() = Group.Split("=")
                        If GroupParts IsNot Nothing AndAlso GroupParts.Length > 0 Then
                            Dim KeyList As New List(Of String)
                            Dim ValueList As New List(Of String)
                            Dim Keys As String() = GroupParts(0).Split(",")
                            If Keys IsNot Nothing Then
                                For Each Key As String In Keys
                                    If Key.Trim <> "" Then KeyList.Add(Key.Trim.Replace("\C", ",").Replace("\R", "=").Replace("\S", "|").Replace("\\", "\"))
                                Next Key
                            End If
                            If GroupParts.Length > 1 Then
                                Dim Values As String() = GroupParts(1).Split(",")
                                If Values IsNot Nothing Then
                                    For Each Value As String In Values
                                        If Value.Trim <> "" Then ValueList.Add(Value.Trim.Replace("\C", ",").Replace("\R", "=").Replace("\S", "|").Replace("\\", "\"))
                                    Next Value
                                End If
                            End If
                            Synonyms.Add(New KeyValuePair(Of String(), String())(KeyList.ToArray, ValueList.ToArray))
                        End If
                    Next Group
                End If
            End If
        End Sub
        Private _AutoComplete As New ListWithEvents(Of String)
        Public ReadOnly Property AutoComplete() As ListWithEvents(Of String)
            Get
                'Comma (,) separated, universal escape \
                Return _AutoComplete
            End Get
        End Property
        Private _Synonyms As New List(Of KeyValuePair(Of String(), String()))
        Public ReadOnly Property Synonyms() As List(Of KeyValuePair(Of String(), String()))
            Get
                'Key1,Key2,Key3=Value1,Value2,Value3|KeyA,KeyB=ValueC
                ', \C
                '= \R
                '| \S
                '\ \\
                Return _Synonyms
            End Get
        End Property

        Private Sub MySettings_SettingsSaving(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.SettingsSaving
            Dim sb As New System.Text.StringBuilder
            For Each item As String In AutoComplete
                If sb.Length <> 0 Then sb.Append(","c)
                sb.Append(item.Replace("\", "\\").Replace(",", "\,"))
            Next item
            Me.KwAutoComplete = sb.ToString

            sb = New System.Text.StringBuilder
            For Each item As KeyValuePair(Of String(), String()) In Synonyms
                If sb.Length <> 0 Then sb.Append("|"c)
                If item.Key IsNot Nothing AndAlso item.Key.Length > 0 Then
                    Dim i As Integer = 0
                    For Each key As String In item.Key
                        If i > 0 Then sb.Append(","c)
                        sb.Append(key.Replace("\", "\\").Replace(",", "\C").Replace("=", "\R").Replace("|", "\S"))
                        i += 1
                    Next key
                    sb.Append("="c)
                    If item.Value IsNot Nothing Then
                        i = 0
                        For Each value As String In item.Value
                            If i > 0 Then sb.Append(","c)
                            sb.Append(value.Replace("\", "\\").Replace(",", "\C").Replace("=", "\R").Replace("|", "\S"))
                            i += 1
                        Next value
                    End If
                End If
            Next item
            Me.KwSynonyms = sb.ToString
        End Sub
    End Class
End Namespace
