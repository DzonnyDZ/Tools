#If Config <= Nightly Then 'Stage:Nightly
'TODO: Propper implementation
Namespace DrawingT.MetadataT.ExifT
    ''' <summary>This class will once become full-featured Exif metadata writer</summary>
    Friend Class ExifWriter
        Private Writer As IOt.BinaryWriter
        Public Sub New(ByVal Stream As IO.Stream, Optional ByVal ByteAlign As IOt.BinaryReader.ByteAlign = IOt.BinaryReader.ByteAlign.LittleEndian)
            'TODO:Exceptions
            Me.Writer = New IOt.BinaryWriter(Stream, System.Text.Encoding.ASCII, ByteAlign)
        End Sub
        Public Sub WriteHeader()
            If Writer.ByteOrder = IOt.BinaryReader.ByteAlign.BigEndian Then
                Writer.Write("MM")
            Else
                Writer.Write("II")
            End If
            Writer.Write(&H2AUS)
            'Offset of IFD0
            Writer.Write(8UI)
        End Sub
        Private FreeSpace As UInteger
        Public Sub WriteIfd(ByVal Ifd As Ifd)
            Dim Mapping As New Dictionary(Of UShort, Integer)
            Writer.Write(CUShort(Ifd.Records.Count))
            FreeSpace = Writer.BaseStream.Position + 12 * Ifd.Records.Count + 4 + 1
            For Each Record In Ifd.Records
                Writer.Write(CUShort(Record.Key))
                Writer.Write(CUShort(Record.Value.DataType.DataType))
                Writer.Write(CUInt(Record.Value.DataType.NumberOfElements))
                Dim ComponentSize As Integer
                Select Case Record.Value.DataType.DataType
                    Case ExifDataTypes.ASCII : ComponentSize = 1
                    Case ExifDataTypes.Byte : ComponentSize = 1
                    Case ExifDataTypes.Double : ComponentSize = 8
                    Case ExifDataTypes.Int16 : ComponentSize = 2
                    Case ExifDataTypes.Int32 : ComponentSize = 4
                    Case ExifDataTypes.NA : ComponentSize = 1
                    Case ExifDataTypes.SByte : ComponentSize = 1
                    Case ExifDataTypes.Single : ComponentSize = 4
                    Case ExifDataTypes.SRational : ComponentSize = 8
                    Case ExifDataTypes.UInt16 : ComponentSize = 2
                    Case ExifDataTypes.UInt32 : ComponentSize = 4
                    Case ExifDataTypes.URational : ComponentSize = 8
                End Select
                Dim DataSize = ComponentSize * Record.Value.DataType.NumberOfElements
                If DataSize <= 4 Then
                    Mapping.Add(Record.Key, Writer.BaseStream.Position)
                    WriteData(Record.Value)
                Else
                    Writer.Write(FreeSpace)
                    Dim OldPos = Writer.BaseStream.Position
                    Writer.BaseStream.Seek(FreeSpace, IO.SeekOrigin.Begin)
                    Mapping.Add(Record.Key, Writer.BaseStream.Position)
                    WriteData(Record.Value)
                    FreeSpace = Writer.BaseStream.Position
                    Writer.BaseStream.Seek(OldPos, IO.SeekOrigin.Begin)
                End If
            Next
            Writer.Write(0UI)
            LastIfdKeys = Mapping
        End Sub
        Public Sub WritePointedBlob(ByVal PointerKey As UShort, ByVal PointerType As ExifDataTypes, ByVal Blob As Byte(), Optional ByVal BlobLenKey As UShort = 0US, Optional ByVal LenType As ExifDataTypes = ExifDataTypes.UInt32)
            Dim OldFree = FreeSpace
            Writer.Seek(FreeSpace, IO.SeekOrigin.Begin)
            Writer.Write(Blob)
            FreeSpace = Writer.BaseStream.Position
            Dim PointerAddress = LastIfdKeys(PointerKey)
            Writer.Seek(PointerAddress, IO.SeekOrigin.Begin)
            Select Case PointerType
                Case ExifDataTypes.Int32 : Writer.Write(CInt(OldFree))
                Case ExifDataTypes.Int16 : Writer.Write(CShort(OldFree))
                Case ExifDataTypes.UInt16 : Writer.Write(CUShort(OldFree))
                Case ExifDataTypes.UInt32 : Writer.Write(CUInt(OldFree))
            End Select
            If BlobLenKey <> 0 Then
                Dim LenAddress = LastIfdKeys(BlobLenKey)
                Writer.BaseStream.Seek(LenAddress, IO.SeekOrigin.Begin)
                Select Case LenType
                    Case ExifDataTypes.Int32 : Writer.Write(CInt(Blob.Length))
                    Case ExifDataTypes.Int16 : Writer.Write(CShort(Blob.Length))
                    Case ExifDataTypes.UInt16 : Writer.Write(CUShort(Blob.Length))
                    Case ExifDataTypes.UInt32 : Writer.Write(CUInt(Blob.Length))
                End Select
            End If
        End Sub
        Private LastIfdKeys As Dictionary(Of UShort, Integer)
        Private Sub WriteData(ByVal Data As ExifRecord)
            If Data.DataType.NumberOfElements = 0 Then Exit Sub
            If Data.DataType.NumberOfElements = 1 Then
                Select Case Data.DataType.DataType
                    Case ExifDataTypes.ASCII : Writer.Write(CChar(Data.Data))
                    Case ExifDataTypes.Byte : Writer.Write(CByte(Data.Data))
                    Case ExifDataTypes.Double : Writer.Write(CDbl(Data.Data))
                    Case ExifDataTypes.Int16 : Writer.Write(CShort(Data.Data))
                    Case ExifDataTypes.Int32 : Writer.Write(CInt(Data.Data))
                    Case ExifDataTypes.NA : Writer.Write(CByte(Data.Data))
                    Case ExifDataTypes.SByte : Writer.Write(CSByte(Data.Data))
                    Case ExifDataTypes.Single : Writer.Write(CSng(Data.Data))
                    Case ExifDataTypes.SRational : Writer.Write(CType(Data.Data, SRational).Numerator) : Writer.Write(CType(Data.Data, SRational).Denominator)
                    Case ExifDataTypes.UInt16 : Writer.Write(CUShort(Data.Data))
                    Case ExifDataTypes.UInt32 : Writer.Write(CUInt(Data.Data))
                    Case ExifDataTypes.URational : Writer.Write(CType(Data.Data, URational).Numerator) : Writer.Write(CType(Data.Data, URational).Denominator)
                End Select
            Else
                If Data.DataType.DataType = ExifDataTypes.ASCII Then
                    Writer.Write(CStr(Data.Data))
                Else
                    Dim dArray As Array = Data.Data
                    For Each item As Object In dArray
                        Select Case Data.DataType.DataType
                            Case ExifDataTypes.Byte : Writer.Write(CByte(item))
                            Case ExifDataTypes.Double : Writer.Write(CDbl(item))
                            Case ExifDataTypes.Int16 : Writer.Write(CShort(item))
                            Case ExifDataTypes.Int32 : Writer.Write(CInt(item))
                            Case ExifDataTypes.NA : Writer.Write(CByte(item))
                            Case ExifDataTypes.SByte : Writer.Write(CSByte(item))
                            Case ExifDataTypes.Single : Writer.Write(CSng(item))
                            Case ExifDataTypes.SRational : Writer.Write(CType(item, SRational).Numerator) : Writer.Write(CType(item, SRational).Denominator)
                            Case ExifDataTypes.UInt16 : Writer.Write(CUShort(item))
                            Case ExifDataTypes.UInt32 : Writer.Write(CUInt(item))
                            Case ExifDataTypes.URational : Writer.Write(CType(item, URational).Numerator) : Writer.Write(CType(item, URational).Denominator)
                        End Select
                    Next
                End If
            End If
        End Sub
    End Class
End Namespace

#End If
