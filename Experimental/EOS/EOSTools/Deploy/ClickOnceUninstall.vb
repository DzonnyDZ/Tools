Imports Microsoft.Win32, System.IO, Microsoft.VisualBasic.Logging
Namespace Deployment
    Public Module ClickOnceUninstall
        Dim Log As New Log
        ''' <summary>Odinstaluje aplikaci nainstalovanou pomocí click-once</summary>
        ''' <param name="AppName">Název aplikace</param>
        Sub DoUninstall(ByVal AppName As String)
            Dim Uninstall As RegistryKey = Nothing
            Dim OdinstStr As String = "Odinstalace alikace " & AppName
            'Odebrání informací z registru
            Dim Registry As Boolean = False
            Try
                Try
                    Uninstall = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall", True)
                Catch ex As Exception
                    Write(OdinstStr & ": Klíče v registru nebudou odebrány, protože nelze získat přístup ke klíči HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall z důvodu " & ex.Message, TraceEventType.Information)
                End Try
                If Uninstall IsNot Nothing Then
                    For Each KeyName As String In Uninstall.GetSubKeyNames
                        Dim Current As RegistryKey = Nothing
                        Try
                            Try
                                Current = Uninstall.OpenSubKey(KeyName)
                            Catch ex As Exception
                                WriteException(ex, TraceEventType.Warning, OdinstStr & ": Nelze získat přístup ke klíči registru HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\" & KeyName)
                                Continue For
                            End Try
                            Dim DisplayName As Object = Current.GetValue("DisplayName", Nothing)
                            If DisplayName IsNot Nothing AndAlso TypeOf DisplayName Is String AndAlso CStr(DisplayName).ToLower = AppName.ToLower Then
                                Current.Close()
                                Try
                                    Registry = True
                                    Uninstall.DeleteSubKeyTree(KeyName)
                                    Write(OdinstStr & ": Klíč v registru smazán HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\" & KeyName, TraceEventType.Information)
                                Catch ex As Exception
                                    WriteException(ex, TraceEventType.Error, OdinstStr & ": Nelze smazat klíč v registru HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\" & KeyName)
                                    Continue For
                                End Try
                            End If
                        Finally
                            If Current IsNot Nothing Then Try : Current.Close() : Catch : End Try
                        End Try
                    Next KeyName
                End If
            Finally
                If Uninstall IsNot Nothing Then Try : Uninstall.Close() : Catch : End Try
            End Try
            'Odebrání souborů
            Dim File As Boolean = False
            Try
                Dim App20$ = Path.Combine(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)), "Local Settings\Apps\2.0")
                If IO.Directory.Exists(App20) Then
                    Try
                        For Each SubDir1 As String In Directory.GetDirectories(App20)
                            Try
                                For Each SubDir2 As String In Directory.GetDirectories(SubDir1)
                                    Try
                                        For Each SubDir3 As String In Directory.GetDirectories(SubDir2)
                                            Try
                                                If Directory.GetFiles(SubDir3, AppName & ".*").Length > 0 Then
                                                    Try
                                                        File = True
                                                        IO.Directory.Delete(SubDir3, True)
                                                        Write(OdinstStr & ": Složka " & SubDir3 & " smazána", TraceEventType.Information)
                                                    Catch ex As Exception
                                                        WriteException(ex, TraceEventType.Warning, OdinstStr & ": Nelze smazat složku" & SubDir3)
                                                    End Try
                                                End If
                                            Catch ex As Exception
                                                WriteException(ex, TraceEventType.Warning, OdinstStr & ": Nelze získat seznam souborů složky" & SubDir3)
                                            End Try
                                        Next SubDir3
                                    Catch ex As Exception
                                        WriteException(ex, TraceEventType.Warning, OdinstStr & ": Nelze získat seznam adresářů složky" & SubDir2)
                                    End Try
                                Next SubDir2
                            Catch ex As Exception
                                WriteException(ex, TraceEventType.Warning, OdinstStr & ": Nelze získat seznam adresářů složky" & SubDir1)
                            End Try
                        Next SubDir1
                    Catch ex As Exception
                        WriteException(ex, TraceEventType.Warning, OdinstStr & ": Nelze získat seznam adresářů složky" & Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Apps\2.0"))
                    End Try
                End If
            Catch ex As Exception
                WriteException(ex, TraceEventType.Warning, OdinstStr & ": Nelze získat přístup ke složce " & Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Apps\2.0"))
            End Try
            If File OrElse Registry Then
                'Odebrání zástupce z plochy
                Try
                    For Each DesktopFile As String In Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory))
                        If Path.GetFileNameWithoutExtension(DesktopFile).ToLower = AppName.ToLower Then
                            Try
                                IO.File.Delete(DesktopFile)
                            Catch : End Try
                        End If
                    Next DesktopFile
                Catch : End Try
                'Odebrání zástupců z nabídky start
                DeleteRecursive(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), AppName)
            End If
        End Sub
        ''' <summary>Rekurzivně smaže zástupce dané aplikace z dané složky. Odstraní prázdné složky</summary>
        ''' <param name="Folder">Složka</param>
        ''' <param name="AppName">Aplikace</param>
        Private Sub DeleteRecursive(ByVal Folder$, ByVal AppName$)
            Try
                For Each file As String In Directory.GetFiles(Folder)
                    If Path.GetFileNameWithoutExtension(file).ToLower = AppName.ToLower Then
                        Try
                            IO.File.Delete(file)
                        Catch : End Try
                    End If
                Next file
                For Each dir As String In Directory.GetDirectories(Folder)
                    DeleteRecursive(dir, AppName)
                Next dir
                If Directory.GetFiles(Folder).Length = 0 Then
                    Try
                        Directory.Delete(Folder, True)
                    Catch : End Try
                End If
            Catch : End Try
        End Sub

        Private Sub WriteException(ByVal ex As Exception, ByVal Level As TraceEventType, ByVal Additional As String)
            Log.WriteException(ex, Level, Additional)
            Console.WriteLine(Additional & "Tex chyby: " & ex.Message)
        End Sub
        Private Sub Write(ByVal Text As String, ByVal Level As TraceEventType)
            Log.WriteEntry(Text, Level)
            Console.WriteLine(Text)
        End Sub

    End Module
End Namespace