Namespace Drawing.Metadata 'ASAP:Wiki
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Represents Exif tag, contains all recognized Exif tags</summary>
    ''' <remarks><see>http://www.exif.org/Exif2-2.PDF</see></remarks>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ExifReader), LastChange:="4/25/2007")> _
    Public NotInheritable Class ExifTag
        ''' <summary>Tag number</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Tag As Tags
        ''' <summary>Possible datatypes of tag. Always have at least 1 item</summary>
        <CLSCompliant(False)> _
        Public ReadOnly DataType() As ExifIFDReader.DirectoryEntry.ExifDataTypes
        ''' <summary>Number of components of <see cref="DataType"/></summary>
        ''' <remarks>Can be 0 if number of components is not constant</remarks>
       Public ReadOnly Components As Integer

        <CLSCompliant(False)> _
        Public Enum Tags As UShort
            '^{[A-Za-z:b]+}:b{:i}:b:z:b{[0-9a-fA-F]+}$
            '''' <summary>\1</summary>\n\2 = &h\3
#Region "Image data structure"
            ''' <summary>Image width</summary>
            ImageWidth = &H100
            ''' <summary>Image height</summary>
            ImageLength = &H101
            ''' <summary>Number of bits per component</summary>
            BitsPerSample = &H102
            ''' <summary>Compression scheme</summary>
            Compression = &H103
            ''' <summary>Pixel composition</summary>
            PhotometricInterpretation = &H106
            ''' <summary>Orientation of image</summary>
            Orientation = &H112
            ''' <summary>Number of components</summary>
            SamplesPerPixel = &H115
            ''' <summary>Image data arrangement</summary>
            PlanarConfiguration = &H11C
            ''' <summary>Subsampling ratio of Y to C</summary>
            YCbCrSubSampling = &H212
            ''' <summary>Y and C positioning</summary>
            YCbCrPositioning = &H213
            ''' <summary>Image resolution in width direction</summary>
            XResolution = &H11A
            ''' <summary>Image resolution in height direction</summary>
            YResolution = &H11B
            ''' <summary>Unit of X and Y resolution</summary>
            ResolutionUnit = &H128
#End Region
        End Enum

    End Class
#End If
End Namespace