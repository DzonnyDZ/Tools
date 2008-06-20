Imports System.IO
Imports System.Runtime.CompilerServices

#If Config <= Nightly Then
Namespace IOt
    ''' <summary>Tools related to IO <see cref="System.IO.Stream"/>s</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <FirstVersion("07/22/2007")> _
    <Version(1, 0, GetType(StreamTools), LastChange:="10/30/2007")> _
    Public Module StreamTools
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
#If Framework >= 3.5 Then
        <Extension()> _
        Public Sub InsertInto(ByVal Stream As IO.Stream, ByVal Position As Integer, ByVal BytesToReplace As Integer, ByVal Data As Byte(), Optional ByVal Chunk As Integer = 1024)
#Else
        Public Sub InsertInto(ByVal Stream As IO.Stream, ByVal Position As Integer, ByVal BytesToReplace As Integer, ByVal Data As Byte(), Optional ByVal Chunk As Integer = 1024)
#End If
            If Stream Is Nothing Then Throw New ArgumentNullException("Stream")
            If Data Is Nothing Then Throw New ArgumentNullException("Data")
            If Position < 0 OrElse Position >= Stream.Length Then Throw New ArgumentOutOfRangeException(String.Format(ResourcesT.Exceptions.MustBeWithinRange12, "Position", 0, "Stream.Length"), "Position")
            If BytesToReplace < 0 OrElse BytesToReplace + Position >= Stream.Length + 1 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustBeWithinRange12, "BytesToReplace", 0, "Stream.Lenght - Position"), "BytesToReplace")
            If Chunk <= 0 Then Throw New ArgumentOutOfRangeException("Chunk", String.Format(ResourcesT.Exceptions.MustBePositive, "Chunk"))
            If BytesToReplace >= Data.Length Then
                Stream.Seek(Position, SeekOrigin.Begin)
                Stream.Write(Data, 0, Data.Length)
                Dim i As Integer
                Dim BArr(Chunk - 1) As Byte
                For i = Position + BytesToReplace To Stream.Length - Chunk Step Chunk
                    Stream.Seek(i, SeekOrigin.Begin)
                    Stream.Read(BArr, 0, Chunk)
                    Stream.Seek(i - (BytesToReplace - Data.Length), SeekOrigin.Begin)
                    Stream.Write(BArr, 0, Chunk)
                Next i
                If Stream.Length - i <> 0 Then
                    ReDim BArr(Stream.Length - i - 1)
                    Stream.Seek(i, SeekOrigin.Begin)
                    Stream.Read(BArr, 0, BArr.Length)
                    Stream.Seek(i - (BytesToReplace - Data.Length), SeekOrigin.Begin)
                    Stream.Write(BArr, 0, BArr.Length)
                End If
                'For i = i To Stream.Length - 1
                '    Stream.Seek(i, SeekOrigin.Begin)
                '    Dim B As Byte = Stream.ReadByte
                '    Stream.Seek(i - (BytesToReplace - Data.Length), SeekOrigin.Begin)
                '    Stream.WriteByte(B)
                'Next i
                Stream.SetLength(Stream.Length - (BytesToReplace - Data.Length))
            Else
                Dim OldLen As Integer = Stream.Length
                Stream.SetLength(Stream.Length + (Data.Length - BytesToReplace))
                Dim i As Integer
                Dim BArr(Chunk - 1) As Byte
                For i = OldLen - Chunk To Position + BytesToReplace Step -Chunk
                    Stream.Seek(i, SeekOrigin.Begin)
                    Stream.Read(BArr, 0, Chunk)
                    Stream.Seek(i + (Data.Length - BytesToReplace), SeekOrigin.Begin)
                    Stream.Write(BArr, 0, Chunk)
                Next i
                If i + Chunk <> Position + BytesToReplace Then
                    ReDim BArr(Chunk - (Position + BytesToReplace - i))
                    Stream.Seek(Position + BytesToReplace, SeekOrigin.Begin)
                    Stream.Read(BArr, 0, BArr.Length)
                    Stream.Seek(Position + Data.Length, SeekOrigin.Begin)
                    Stream.Write(BArr, 0, BArr.Length)
                End If
                'For i = i + Chunk - 1 To Position + BytesToReplace Step -1
                '    Stream.Seek(i, SeekOrigin.Begin)
                '    Dim B As Byte = Stream.ReadByte
                '    Stream.Seek(i + (Data.Length - BytesToReplace), SeekOrigin.Begin)
                '    Stream.WriteByte(B)
                'Next i
                Stream.Seek(Position, SeekOrigin.Begin)
                Stream.Write(Data, 0, Data.Length)
            End If
        End Sub
    End Module
End Namespace
#End If