Imports System.IO
Imports System.Runtime.CompilerServices

#If Config <= Nightly Then
Namespace IOt
    ''' <summary>Tools related to IO <see cref="System.IO.Stream"/>s</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Module StreamTools
        ''' <summary>Replaces given ammount of bytes in <see cref="IO.Stream"/> with another amount of bytes</summary>
        ''' <param name="Stream">Stream to perform operation on. It must support seking, reading and writing</param>
        ''' <param name="Position">Position where bytes to be replaced starts</param>
        ''' <param name="BytesToReplace">Number of bytes currently in stream to be replaced (can be 0)</param>
        ''' <param name="Data">Bytes to replace old bytes with</param>
        ''' <param name="Chunk">when data are moved from one part of stream to another they are moved in chunks. This defines size of chunk in bytes. Befault is 1024.</param>
        ''' <param name="Offset">Offset in <paramref name="Data"/> to start inserting from</param>
        ''' <param name="Count">Number of bytes from <paramref name="Data"/> to use</param>
        ''' <remarks>If <paramref name="Data"/>'s lenght does not match <paramref name="BytesToReplace"/> the stream is shortened or enlarged and data after replaced block are moved as necessary</remarks>
        ''' <exception cref="IOException">An IO error occurs</exception>
        ''' <exception cref="ObjectDisposedException"><paramref name="Stream"/> is closed</exception>
        ''' <exception cref="NotSupportedException">
        ''' <paramref name="Stream"/> does not support seeking -or-
        ''' <paramref name="Stream"/> does not support writing -or-
        ''' <paramref name="Stream"/> does not suport reading
        ''' </exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Stream"/> is null -or- <paramref name="Data"/> is null</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Position"/> is not within range &lt;0; <paramref name="Stream"/>.<see cref="Stream.Length">Length</see>) -or- <paramref name="BytesToReplace"/> is not within range &lt;0; <paramref name="Stream"/>.<see cref="Stream.Length">Lenght</see> - <paramref name="Position"/> - or- <paramref name="Chunk"/> is not positive -or- <paramref name="Offset"/> or <paramref name="Count"/> is negative</exception>
        ''' <exception cref="ArgumentException">Sum of <paramref name="Offset"/> and <paramref name="Count"/> is greater than length of <paramref name="Data"/></exception>
        <Extension()> _
        Public Sub InsertInto(ByVal Stream As IO.Stream, ByVal Position As Integer, ByVal BytesToReplace As Integer, ByVal Data As Byte(), ByVal Offset As Long, ByVal Count As Long, Optional ByVal Chunk As Integer = 1024)
            If Stream Is Nothing Then Throw New ArgumentNullException("Stream")
            If Data Is Nothing Then Throw New ArgumentNullException("Data")
            If Position < 0 OrElse Position >= Stream.Length Then Throw New ArgumentOutOfRangeException(String.Format(ResourcesT.Exceptions.MustBeWithinRange12, "Position", 0, "Stream.Length"), "Position")
            If BytesToReplace < 0 OrElse BytesToReplace + Position >= Stream.Length + 1 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustBeWithinRange12, "BytesToReplace", 0, "Stream.Lenght - Position"), "BytesToReplace")
            If Chunk <= 0 Then Throw New ArgumentOutOfRangeException("Chunk", String.Format(ResourcesT.Exceptions.MustBePositive, "Chunk"))
            If Offset + Count > Data.Length Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.WasGreaterThanLegnthOf2, "Offset", "Count", "Data"))
            If Offset < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "Offset"))
            If Count < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "Count"))
            If BytesToReplace >= Count Then
                Stream.Seek(Position, SeekOrigin.Begin)
                Stream.Write(Data, Offset, Count)
                Dim i As Integer
                Dim BArr(Chunk - 1) As Byte
                For i = Position + BytesToReplace To Stream.Length - Chunk Step Chunk
                    Stream.Seek(i, SeekOrigin.Begin)
                    Dim pos As Long = 0
                    While pos < Chunk
                        pos += Stream.Read(BArr, pos, Chunk - pos)
                    End While
                    Stream.Seek(i - (BytesToReplace - Count), SeekOrigin.Begin)
                    Stream.Write(BArr, 0, Chunk)
                Next i
                If Stream.Length - i <> 0 Then
                    ReDim BArr(Stream.Length - i - 1)
                    Stream.Seek(i, SeekOrigin.Begin)
                    Dim pos As Long = 0
                    While pos < BArr.Length
                        pos += Stream.Read(BArr, pos, Chunk - BArr.Length)
                    End While
                    Stream.Seek(i - (BytesToReplace - Count), SeekOrigin.Begin)
                    Stream.Write(BArr, 0, BArr.Length)
                End If
                'For i = i To Stream.Length - 1
                '    Stream.Seek(i, SeekOrigin.Begin)
                '    Dim B As Byte = Stream.ReadByte
                '    Stream.Seek(i - (BytesToReplace - Data.Length), SeekOrigin.Begin)
                '    Stream.WriteByte(B)
                'Next i
                Stream.SetLength(Stream.Length - (BytesToReplace - Count))
            Else
                Dim OldLen As Integer = Stream.Length
                Stream.SetLength(Stream.Length + (Count - BytesToReplace))
                Dim i As Integer
                Dim BArr(Chunk - 1) As Byte
                For i = OldLen - Chunk To Position + BytesToReplace Step -Chunk
                    Stream.Seek(i, SeekOrigin.Begin)
                    Dim pos As Long = 0
                    While pos < Chunk
                        pos += Stream.Read(BArr, pos, Chunk - pos)
                    End While
                    Stream.Seek(i + (Count - BytesToReplace), SeekOrigin.Begin)
                    Stream.Write(BArr, 0, Chunk)
                Next i
                If i + Chunk <> Position + BytesToReplace Then
                    ReDim BArr(Chunk - (Position + BytesToReplace - i))
                    Stream.Seek(Position + BytesToReplace, SeekOrigin.Begin)
                    Dim pos As Long = 0
                    While pos < BArr.Length
                        pos += Stream.Read(BArr, pos, Chunk - BArr.Length)
                    End While
                    Stream.Seek(Position + Count, SeekOrigin.Begin)
                    Stream.Write(BArr, 0, BArr.Length)
                End If
                'For i = i + Chunk - 1 To Position + BytesToReplace Step -1
                '    Stream.Seek(i, SeekOrigin.Begin)
                '    Dim B As Byte = Stream.ReadByte
                '    Stream.Seek(i + (Data.Length - BytesToReplace), SeekOrigin.Begin)
                '    Stream.WriteByte(B)
                'Next i
                Stream.Seek(Position, SeekOrigin.Begin)
                Stream.Write(Data, Offset, Count)
            End If
        End Sub
        ''' <summary>Replaces given ammount of bytes in <see cref="IO.Stream"/> with another amount of bytes</summary>
        ''' <param name="Stream">Stream to perform operation on. It must support seking, reading and writing</param>
        ''' <param name="Position">Position where bytes to be replaced starts</param>
        ''' <param name="BytesToReplace">Number of bytes currently in stream to be replaced (can be 0)</param>
        ''' <param name="Data">Bytes to replace old bytes with</param>
        ''' <param name="Chunk">when data are moved from one part of stream to another they are moved in chunks. This defines size of chunk in bytes. Befault is 1024.</param>
        ''' <remarks>If <paramref name="Data"/>'s lenght does not match <paramref name="BytesToReplace"/> the stream is shortened or enlarged and data after replaced block are moved as necessary</remarks>
        ''' <exception cref="IOException">An IO error occurs</exception>
        ''' <exception cref="ObjectDisposedException"><paramref name="Stream"/> is closed</exception>
        ''' <exception cref="NotSupportedException">
        ''' <paramref name="Stream"/> does not support seeking -or-
        ''' <paramref name="Stream"/> does not support writing -or-
        ''' <paramref name="Stream"/> does not suport reading
        ''' </exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Stream"/> is null -or- <paramref name="Data"/> is null</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Position"/> is not within range &lt;0; <paramref name="Stream"/>.<see cref="Stream.Length">Length</see>) -or- <paramref name="BytesToReplace"/> is not within range &lt;0; <paramref name="Stream"/>.<see cref="Stream.Length">Lenght</see> - <paramref name="Position"/> - or- <paramref name="Chunk"/> is not positive</exception>
        <Extension()> _
        Public Sub InsertInto(ByVal Stream As IO.Stream, ByVal Position As Integer, ByVal BytesToReplace As Integer, ByVal Data As Byte(), Optional ByVal Chunk As Integer = 1024)
            Stream.InsertInto(Position, BytesToReplace, Data, 0, Data.Length, Chunk)
        End Sub

        ''' <summary>Writes content of <see cref="Stream"/> to <see cref="Stream"/></summary>
        ''' <param name="Target">Target to write content of <paramref name="Source"/> to</param>
        ''' <param name="Source">Contains data to write to <paramref name="Target"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="Source"/> or <paramref name="Target"/> is null</exception>
        ''' <exception cref="IOException">An IO error occurs</exception>
        ''' <exception cref="NotSupportedException"><paramref name="Source"/> does not support reading or <paramref name="Target"/> does not suport writing.</exception>
        ''' <exception cref="ObjectDisposedException"><paramref name="Source"/> or <paramref name="Target"/> was cloased</exception>
        <Extension()> _
        Public Sub Write(ByVal Target As IO.Stream, ByVal Source As IO.Stream)
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If Source Is Nothing Then Throw New ArgumentNullException("Source")
            Dim buff(1023) As Byte
            Dim noRead = Source.Read(buff, 0, buff.Length)
            While noRead > 0
                Target.Write(buff, 0, noRead)
                noRead = Source.Read(buff, 0, buff.Length)
            End While
        End Sub
        ''' <summary>Writes content of <see cref="Stream"/> to <see cref="Stream"/></summary>
        ''' <param name="Target">Target to write content of <paramref name="Source"/> to</param>
        ''' <param name="Source">Contains data to write to <paramref name="Target"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="Source"/> or <paramref name="Target"/> is null</exception>
        ''' <exception cref="IOException">An IO error occurs</exception>
        ''' <exception cref="NotSupportedException"><paramref name="Source"/> does not support reading or <paramref name="Target"/> does not suport writing.</exception>
        ''' <exception cref="ObjectDisposedException"><paramref name="Source"/> or <paramref name="Target"/> was cloased</exception>
        <Extension()> _
       Public Sub Write(ByVal Target As BinaryWriter, ByVal Source As IO.Stream)
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If Source Is Nothing Then Throw New ArgumentNullException("Source")
            Dim buff(1023) As Byte
            Dim noRead = Source.Read(buff, 0, buff.Length)
            While noRead > 0
                Target.Write(buff, 0, noRead)
                noRead = Source.Read(buff, 0, buff.Length)
            End While
        End Sub
        ''' <summary>Writes content of <see cref="Stream"/> to <see cref="Stream"/></summary>
        ''' <param name="Target">Target to write content of <paramref name="Source"/> to</param>
        ''' <param name="Source">Contains data to write to <paramref name="Target"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="Source"/> or <paramref name="Target"/> is null</exception>
        ''' <exception cref="IOException">An IO error occurs</exception>
        ''' <exception cref="NotSupportedException"><paramref name="Source"/> does not support reading or <paramref name="Target"/> does not suport writing.</exception>
        ''' <exception cref="ObjectDisposedException"><paramref name="Source"/> or <paramref name="Target"/> was cloased</exception>
        <Extension()> _
       Public Sub Write(ByVal Target As BinaryWriter, ByVal Source As BinaryReader)
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If Source Is Nothing Then Throw New ArgumentNullException("Source")
            Dim buff(1023) As Byte
            Dim noRead = Source.Read(buff, 0, buff.Length)
            While noRead > 0
                Target.Write(buff, 0, noRead)
                noRead = Source.Read(buff, 0, buff.Length)
            End While
        End Sub
        ''' <summary>Writes content of <see cref="Stream"/> to <see cref="Stream"/></summary>
        ''' <param name="Target">Target to write content of <paramref name="Source"/> to</param>
        ''' <param name="Source">Contains data to write to <paramref name="Target"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="Source"/> or <paramref name="Target"/> is null</exception>
        ''' <exception cref="IOException">An IO error occurs</exception>
        ''' <exception cref="NotSupportedException"><paramref name="Source"/> does not support reading or <paramref name="Target"/> does not suport writing.</exception>
        ''' <exception cref="ObjectDisposedException"><paramref name="Source"/> or <paramref name="Target"/> was cloased</exception>
        <Extension()> _
       Public Sub Write(ByVal Target As Stream, ByVal Source As BinaryReader)
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If Source Is Nothing Then Throw New ArgumentNullException("Source")
            Dim buff(1023) As Byte
            Dim noRead = Source.Read(buff, 0, buff.Length)
            While noRead > 0
                Target.Write(buff, 0, noRead)
                noRead = Source.Read(buff, 0, buff.Length)
            End While
        End Sub
    End Module
End Namespace
#End If