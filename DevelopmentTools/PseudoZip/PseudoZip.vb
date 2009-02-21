Imports System.IO.Packaging
Module PseudoZip

    Sub Main()
        Console.WriteLine("{0} {1} {2}{3}{4}", My.Application.Info.ProductName, My.Application.Info.Title, My.Application.Info.Version, vbCrLf, My.Application.Info.Copyright)
        If My.Application.CommandLineArgs.Count < 3 Then
            Console.WriteLine(My.Resources.Usage, My.Application.Info.AssemblyName)
            Environment.Exit(-1)
            End
        End If

        Dim command = My.Application.CommandLineArgs(0)
        Dim ArchiveFile = My.Application.CommandLineArgs(1)
        Dim Folder = My.Application.CommandLineArgs(2)
        If Not Folder.EndsWith("\") AndAlso Not Folder.EndsWith("/") Then Folder &= "\"
        Select Case command
            Case "a" 'Pack
                Using Archive = ZipPackage.Open(ArchiveFile, IO.FileMode.Create, IO.FileAccess.ReadWrite)
                    Dim masks As New List(Of String)
                    Dim recursive As Boolean = My.Application.CommandLineArgs.Count >= 4 AndAlso My.Application.CommandLineArgs(3) = "-r"
                    For i = If(recursive, 4, 3) To My.Application.CommandLineArgs.Count - 1
                        masks.Add(My.Application.CommandLineArgs(i))
                    Next
                    If masks.Count = 0 Then masks.Add("*")
                    Dim PackN% = 0
                    For Each file In IO.Directory.GetFiles(Folder, "*", If(recursive, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))
                        Dim Match = False
                        For Each Mask In masks
                            If IO.Path.GetFileName(file) Like Mask Then Match = True : Exit For
                        Next
                        If Not Match Then Continue For
                        Dim PartUri = New Uri("/" & file.Substring(Folder.Length).Replace("\", "/"), UriKind.RelativeOrAbsolute)
                        Dim Part = Archive.CreatePart(PartUri, "application/octet-stream")
                        Using PartStream = Part.GetStream(IO.FileMode.Create, IO.FileAccess.Write), FileStream = IO.File.Open(file, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                            Dim Buffer(1024) As Byte
                            Dim nread%
                            Do
                                nread = FileStream.Read(Buffer, 0, Buffer.Length)
                                PartStream.Write(Buffer, 0, nread)
                            Loop Until nread = 0
                        End Using
                        PackN += 1
                    Next
                    Console.WriteLine(My.Resources.FilesPacked, PackN)
                End Using
            Case "e" 'Extract
                Using Archive = ZipPackage.Open(ArchiveFile, IO.FileMode.Open, IO.FileAccess.Read)
                    Dim PackN% = 0
                    For Each Part In Archive.GetParts
                        Dim PartPath = IO.Path.Combine(Folder, Part.Uri.OriginalString.Substring(1))
                        If Not IO.Directory.Exists(IO.Path.GetDirectoryName(PartPath)) Then IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(PartPath))
                        Using PartStream = Part.GetStream(IO.FileMode.Open, IO.FileAccess.Read), FileStream = IO.File.Open(PartPath, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)
                            Dim Buffer(1024) As Byte
                            Dim nread%
                            Do
                                nread = PartStream.Read(Buffer, 0, Buffer.Length)
                                FileStream.Write(Buffer, 0, nread)
                            Loop Until nread = 0
                        End Using
                        PackN += 1
                    Next
                    Console.WriteLine(My.Resources.FilesExtracted, PackN)
                End Using
            Case Else
                Console.WriteLine(My.Resources.UnknownCommand0, command)
                Environment.Exit(-1)
                End
        End Select
    End Sub

End Module
