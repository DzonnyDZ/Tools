Imports System.Linq
''' <summary>Main module of version correcor</summary>
Module VersionCorrector
    ''' <summary>Performs the correction</summary>
    Public Sub Main()
        If My.Application.CommandLineArgs.Count < 1 Then
            Console.WriteLine("Finds misversioned references to given assembly in the same asembly and corrects them.")
            Console.WriteLine("Usage: {0} <assembly> [<snk file>]", IO.Path.GetFileNameWithoutExtension(GetType(VersionCorrector).Assembly.Location))
            Environment.Exit(1)
            Return
        End If
        Dim path = My.Application.CommandLineArgs(0)
        Dim Name = Reflection.AssemblyName.GetAssemblyName(path)
        Dim invc = Globalization.CultureInfo.InvariantCulture
        'clr-namespace:Tools.Tests.WindowsT.WPF;assembly=Tools.Tests,Version=1.5.
        Dim LookFor As String
        LookFor = String.Format(invc, "{0}, Version={1}.{2}.", Name.Name, Name.Version.Major, Name.Version.Minor)
        Dim sn As Boolean = False
        Using str = IO.File.Open(path, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.Read)
            Dim b = str.ReadByte
            Dim index = 0I
            Dim ToWrite = String.Format(invc, "{0}.{1}", Name.Version.Build, Name.Version.Revision)
            Dim WritePositions As New List(Of Integer)
            'Searching
            While b >= 0
                If index < LookFor.Length AndAlso LookFor(index) = ChrW(b) Then 'Continue in search
                    index += 1
                ElseIf index = LookFor.Length Then  'Prepare replace
                    Dim n1 = ""
                    While b >= 0 AndAlso ChrW(b) >= "0"c AndAlso ChrW(b) <= "9"c
                        n1 &= ChrW(b)
                        b = str.ReadByte
                    End While
                    If n1 <> "" AndAlso b >= 0 AndAlso ChrW(b) = "."c Then
                        b = str.ReadByte
                        Dim n2 = ""
                        While b >= 0 AndAlso ChrW(b) >= "0" AndAlso ChrW(b) <= "9"c
                            n2 &= ChrW(b)
                            b = str.ReadByte
                        End While
                        Dim OldPart = n1 & "." & n2
                        If n2 <> "" AndAlso OldPart <> ToWrite Then
                            If OldPart.Length <> ToWrite.Length Then
                                'Cannot risk shifting data as it may break offsets but *-generated versions usually have same lengths
                                Console.WriteLine("Canot update assembly. Versions have different lenght. Try rebuilding. Old {2}.{3}.{0}, new {2}.{3}.{1}", ToWrite, OldPart, Name.Version.Major, Name.Version.Minor)
                                Environment.Exit(2)
                                Return
                            End If
                            Dim wPosition As Long = str.Position - OldPart.Length - 1
                            Console.WriteLine("Position {0}, will change {1}.{2}.{3} to {1}.{2}.{4}", wPosition, Name.Version.Major, Name.Version.Minor, OldPart, ToWrite)
                            WritePositions.Add(wPosition) 'Remember the position
                        End If
                    End If
                    index = 0
                Else
                    index = 0
                End If
                b = str.ReadByte
            End While
            If WritePositions.Count <> 0 Then
                'Do the write
                Console.WriteLine("Performing {0} writes", WritePositions.Count)
                For Each pos In WritePositions
                    str.Position = pos
                    For Each ch In ToWrite
                        str.WriteByte(AscW(ch))
                    Next
                Next
                sn = True
            Else
                Console.WriteLine("No changes needed")
            End If
        End Using
        If sn AndAlso My.Application.CommandLineArgs.Count >= 2 Then
            'Re-sign the asembly
            'Warning: If you change something in signed assembly it may behave unpredictably (i.e. doesn't load)
            Dim p As New Process
            p.StartInfo.FileName = My.Settings.sn
            p.StartInfo.Arguments = String.Format("-Ra ""{0}"" ""{1}""", path, My.Application.CommandLineArgs(1))
            p.StartInfo.UseShellExecute = False
            Console.WriteLine("{0} {1}", p.StartInfo.FileName, p.StartInfo.Arguments)
            Try
                p.Start()
                p.WaitForExit()
                If p.ExitCode <> 0 Then
                    Console.WriteLine("Error in sn.exe")
                    Environment.Exit(p.ExitCode)
                    Return
                End If
            Catch ex As Exception
                Console.WriteLine("Canot start sn.exe. You can change path to sn.exe in configuration file.")
                Environment.Exit(3)
                Return
            End Try
        End If
    End Sub

End Module
