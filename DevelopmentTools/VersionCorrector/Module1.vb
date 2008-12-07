Imports System.Linq
''' <summary>Main module of version correcor</summary>
Module VersionCorrector
    ''' <summary>Performs the correction</summary>
    Public Sub Main()
        If My.Application.CommandLineArgs.Count < 1 Then
            Console.WriteLine("Finds misversioned references to given assembly in the same asembly and corrects them.")
            Console.WriteLine("Usage: {0} <assembly> [<snk file>]", IO.Path.GetFileNameWithoutExtension(GetType(VersionCorrector).Assembly.Location))
            Console.WriteLine("Assembly name can contain wildchars (*)")
            Console.WriteLine("String "".dllexe"" at end is treated as both, "".dll"" and "".exe""")
            Console.WriteLine("Assembiles that are not strongly named are not re-signed event when snk file is specified.")
            Console.WriteLine("Files ""*.vshost.exe"" are always skipped")
            Environment.Exit(1)
            Return
        End If
        Dim path = My.Application.CommandLineArgs(0)
        Dim Directory = IO.Path.GetDirectoryName(path)
        Dim mask = IO.Path.GetFileName(path)
        Console.WriteLine("• {0} {1}", My.Application.Info.Title, My.Application.Info.Version)
        If mask.EndsWith(".dllexe") Then
            For Each file In IO.Directory.GetFiles(Directory, mask.Substring(0, mask.Length - 6) & "dll")
                CorrectAssembly(file)
            Next
            For Each file In IO.Directory.GetFiles(Directory, mask.Substring(0, mask.Length - 6) & "exe")
                If Not IO.Path.GetFileName(file) Like "*.vshost.exe" Then CorrectAssembly(file)
            Next
        Else
            For Each file In IO.Directory.GetFiles(Directory, mask)
                If Not IO.Path.GetFileName(file) Like "*.vshost.exe" Then CorrectAssembly(file)
            Next
        End If
    End Sub
    ''' <summary>Performs the corection for single assembly</summary>
    ''' <param name="path">Assembly path</param>
    Private Sub CorrectAssembly(ByVal path As String)
        Dim Name = Reflection.AssemblyName.GetAssemblyName(path)
        Dim invc = Globalization.CultureInfo.InvariantCulture
        'clr-namespace:Tools.Tests.WindowsT.WPF;assembly=Tools.Tests,Version=1.5.
        Dim LookFor As String
        LookFor = String.Format(invc, "{0}, Version={1}.{2}.", Name.Name, Name.Version.Major, Name.Version.Minor)
        Console.WriteLine("  Looking for {0}, replace with {1}", LookFor, Name.Version)
        Dim sn As Boolean = False
        Using str = IO.File.Open(path, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.Read)
            Dim b = str.ReadByte
            Dim index = 0I
            Dim ToWrite = String.Format(invc, "{0}.{1}", Name.Version.Build, Name.Version.Revision)
            Dim WritePositions As New List(Of Integer)
            'Searching
            While b >= 0
                '#If DEBUG Then
                '                Dim Surrounding As String = "", Found$
                '                'If index > 0 Then
                '                Dim oldP As Integer = str.Position
                '                Const MINUS = 30
                '                str.Position = Math.Max(0, str.Position - MINUS)
                '                Dim bytes(59) As Byte
                '                Dim BytesRead = str.Read(bytes, 0, bytes.Length)
                '                ReDim Preserve bytes(BytesRead - 1)
                '                Dim xi% = Math.Max(0, oldP - MINUS) + 1
                '                For Each mib As Byte In bytes
                '                    If xi = oldP Then Surrounding &= "-->"
                '                    Surrounding &= ChrW(mib)
                '                    If xi = oldP Then Surrounding &= "<--"
                '                    xi += 1
                '                Next
                '                Surrounding = Surrounding.Replace(vbNullChar, "•")
                '                str.Position = oldP
                '                'End If
                '                If index < LookFor.Length Then Found = LookFor.Substring(0, index + 1)
                '#End If
                ' If str.Position > &H113D30 Then Stop '1129782
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
                                Console.WriteLine("    Canot update assembly. Versions have different lenght. Try rebuilding. Old {2}.{3}.{0}, new {2}.{3}.{1}", ToWrite, OldPart, Name.Version.Major, Name.Version.Minor)
                                Environment.Exit(2)
                                Return
                            End If
                            Dim wPosition As Long = str.Position - OldPart.Length - 1
                            Console.WriteLine("    Position {0} (&h{0:X}), will change {1}.{2}.{3} to {1}.{2}.{4}", wPosition, Name.Version.Major, Name.Version.Minor, OldPart, ToWrite)
                            WritePositions.Add(wPosition) 'Remember the position
                        Else
                            Console.WriteLine("    Position {0} (&h{0:X}) no change needed from {1}.{2}.{3} to {1}.{2}.{4}", str.Position - OldPart.Length - 1, Name.Version.Major, Name.Version.Minor, OldPart, ToWrite)
                        End If
                    End If
                    index = 0
                ElseIf ChrW(b) = LookFor(0) Then
                    index = 1
                Else
                    index = 0
                End If
                b = str.ReadByte
            End While
            If WritePositions.Count <> 0 Then
                'Do the write
                Console.WriteLine("    > Performing {0} writes", WritePositions.Count)
                For Each pos In WritePositions
                    str.Position = pos
                    For Each ch In ToWrite
                        str.WriteByte(AscW(ch))
                    Next
                Next
                sn = True
            Else
                Console.WriteLine("    > No changes needed")
            End If
        End Using
        If sn AndAlso My.Application.CommandLineArgs.Count >= 2 AndAlso Name.GetPublicKeyToken IsNot Nothing Then
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
