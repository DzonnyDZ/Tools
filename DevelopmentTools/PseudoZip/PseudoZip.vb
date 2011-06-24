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
        Dim archiveFile = My.Application.CommandLineArgs(1)
        Dim folder = My.Application.CommandLineArgs(2)
        If Not Folder.EndsWith("\") AndAlso Not Folder.EndsWith("/") Then Folder &= "\"
        Try
            Select Case command
                Case "a" 'Pack
                    If My.Application.CommandLineArgs.Count >= 4 AndAlso (My.Application.CommandLineArgs(3) = "-sd" OrElse My.Application.CommandLineArgs(3) = "-sg") Then
                        Using targetStream = IO.File.Open(archiveFile, IO.FileMode.Create, IO.FileAccess.Write),
                              compressed As IO.Stream = If(My.Application.CommandLineArgs(3) = "-sd",
                                                           CType(New IO.Compression.DeflateStream(targetStream, IO.Compression.CompressionMode.Compress, True), IO.Stream),
                                                           CType(New IO.Compression.GZipStream(targetStream, IO.Compression.CompressionMode.Compress, True), IO.Stream)
                                                          ),
                              sourceStream = IO.File.Open(IO.Path.Combine(folder, My.Application.CommandLineArgs(4)), IO.FileMode.Open, IO.FileAccess.Read)
                            Dim buffer(1024) As Byte
                            Dim nread%
                            Do
                                nread = sourceStream.Read(buffer, 0, buffer.Length)
                                compressed.Write(buffer, 0, nread)
                            Loop Until nread = 0
                            compressed.Flush()
                            targetStream.Flush()
                        End Using
                    Else
                        Using archive = ZipPackage.Open(archiveFile, IO.FileMode.Create, IO.FileAccess.ReadWrite)
                            Dim masks As New List(Of String)
                            Dim recursive As Boolean = My.Application.CommandLineArgs.Count >= 4 AndAlso My.Application.CommandLineArgs(3) = "-r"
                            For i = If(recursive, 4, 3) To My.Application.CommandLineArgs.Count - 1
                                masks.Add(My.Application.CommandLineArgs(i))
                            Next
                            If masks.Count = 0 Then masks.Add("*")
                            Dim PackN% = 0
                            For Each file In IO.Directory.GetFiles(folder, "*", If(recursive, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))
                                Dim Match = False
                                For Each Mask In masks
                                    If IO.Path.GetFileName(file) Like Mask Then Match = True : Exit For
                                Next
                                If Not Match Then Continue For
                                Dim partUri = New Uri("/" & file.Substring(folder.Length).Replace("\", "/"), UriKind.RelativeOrAbsolute)
                                Dim part = archive.CreatePart(partUri, "application/octet-stream")
                                Using partStream = part.GetStream(IO.FileMode.Create, IO.FileAccess.Write),
                                      fileStream = IO.File.Open(file, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                                    Dim buffer(1024) As Byte
                                    Dim nread%
                                    Do
                                        nread = fileStream.Read(buffer, 0, buffer.Length)
                                        partStream.Write(buffer, 0, nread)
                                    Loop Until nread = 0
                                End Using
                                PackN += 1
                            Next
                            Console.WriteLine(My.Resources.FilesPacked, PackN)
                        End Using
                    End If
                Case "e" 'Extract
                    If My.Application.CommandLineArgs.Count >= 4 AndAlso (My.Application.CommandLineArgs(3) = "-sd" OrElse My.Application.CommandLineArgs(3) = "-sg") Then
                        Using targetStream = IO.File.Open(folder.TrimEnd("\"), IO.FileMode.Create, IO.FileAccess.Write),
                              compressed = IO.File.Open(archiveFile, IO.FileMode.Open, IO.FileAccess.Read),
                              uncompressed As IO.Stream = If(My.Application.CommandLineArgs(3) = "-sd",
                                                           CType(New IO.Compression.DeflateStream(compressed, IO.Compression.CompressionMode.Decompress, True), IO.Stream),
                                                           CType(New IO.Compression.GZipStream(compressed, IO.Compression.CompressionMode.Decompress, True), IO.Stream)
                                                          )
                            Dim buffer(1024) As Byte
                            Dim nread%
                            Do
                                nread = uncompressed.Read(buffer, 0, buffer.Length)
                                targetStream.Write(buffer, 0, nread)
                            Loop Until nread = 0
                            targetStream.Flush()
                        End Using
                    Else
                        Using Archive = ZipPackage.Open(archiveFile, IO.FileMode.Open, IO.FileAccess.Read)
                            Dim PackN% = 0
                            For Each Part In Archive.GetParts
                                Dim PartPath = IO.Path.Combine(folder, Part.Uri.OriginalString.Substring(1))
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
                    End If
                Case Else
                    Console.WriteLine(My.Resources.UnknownCommand0, command)
                    Environment.Exit(-1)
                    End
            End Select
        Catch ex As Exception
            Console.WriteLine(My.Resources.Error0, ex.Message)
            Environment.Exit(1)
        End Try
        Environment.Exit(0)
    End Sub

End Module
