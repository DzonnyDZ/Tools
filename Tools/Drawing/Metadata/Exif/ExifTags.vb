
' GENERATED FILE -- DO NOT EDIT
' 
' Generator: TransformCodeGenerator, Version=1.0.2672.41774, Culture=neutral, PublicKeyToken=null
' Version: 1.0.2672.41774
'
'
' Generated code from "ExifTags.xml"
'
' Created: 27. dubna 2007
' By: DZONNY\Honza
'

    
Namespace Drawing.Metadata
    Partial Public Class ExifTags
        <CLSCompliant(False)> Public Enum IFDTags As UShor
            
#Region "Sub IFD pointers"
        
            ''' <summary>Exif IFD Pointer</summary>
            ExifIFD = &h8769
            ''' <summary>GPS Info IFD Pointer</summary>
            GPSIFD = &h8825
#End Region
    
#Region "Tags relating to image data structure"
        
            ''' <summary>Image width</summary>
            ImageWidth = &h100
            ''' <summary>Image height</summary>
            ImageLength = &h101
            ''' <summary>Number of bits per component</summary>
            BitsPerSample = &h102
            ''' <summary>Compression scheme</summary>
            Compression = &h103
            ''' <summary>Pixel composition</summary>
            PhotometricInterpretation = &h106
            ''' <summary>Orientation of image</summary>
            Orientation = &h112
            ''' <summary>Number of components</summary>
            SamplesPerPixel = &h115
            ''' <summary>Image data arrangement</summary>
            PlanarConfiguration = &h11C
            ''' <summary>Subsampling ratio of Y to C</summary>
            YCbCrSubSampling = &h212
            ''' <summary>Y and C positioning</summary>
            YCbCrPositioning = &h213
            ''' <summary>Image resolution in width direction</summary>
            XResolution = &h11A
            ''' <summary>Image resolution in height direction</summary>
            YResolution = &h11B
            ''' <summary>Unit of X and Y resolution</summary>
            ResolutionUnit = &h128
#End Region
    
#Region "Tags relating to recording offset"
        
            ''' <summary>Image data location</summary>
            StripOffsets = &h111
            ''' <summary>Number of rows per strip</summary>
            RowsPerStrip = &h116
            ''' <summary>Bytes per compressed strip</summary>
            StripByteCounts = &h117
            ''' <summary>Offset to JPEG SOI</summary>
            JPEGInterchangeFormat = &h201
            ''' <summary>Bytes of JPEG data</summary>
            JPEGInterchangeFormatLength = &h202
#End Region
    
#Region "Tags relating to image data characteristics"
        
            ''' <summary>Transfer function</summary>
            TransferFunction = &h12D
            ''' <summary>White point chromaticity</summary>
            WhitePoint = &h13E
            ''' <summary>Chromaticities of primaries</summary>
            PrimaryChromaticities = &h13F
            ''' <summary>Color space transformation matrix coefficients</summary>
            YCbCrCoefficients = &h211
            ''' <summary>Pair of black and white reference values</summary>
            ReferenceBlackWhite = &h214
#End Region
    
#Region "Other tags"
        
            ''' <summary>File change date and time</summary>
            DateTime = &h132
            ''' <summary>Image title</summary>
            ImageDescription = &h10E
            ''' <summary>Image input equipment manufacturer</summary>
            Make = &h10F
            ''' <summary>Image input equipment model</summary>
            Model = &h110
            ''' <summary>Software used</summary>
            Software = &h131
            ''' <summary>Person who created the image</summary>
            Artist = &h13B
            ''' <summary>Copyright holder</summary>
            Copyright = &h8298
#End Region
    
        End Enum
        
        <CLSCompliant(False)> Public Enum ExifTags As UShort
            
#Region "Sub IFD pointers"
        
            ''' <summary>Interoperability IFD Pointer</summary>
            InteroperabilityIFD = &hA005
#End Region
    
#Region "Tags Relating to Version"
        
            ''' <summary>Exif version</summary>
            ExifVersion = &h9000
            ''' <summary>Supported Flashpix version</summary>
            FlashpixVersion = &hA000
#End Region
    
#Region "Tag Relating to Image Data Characteristics"
        
            ''' <summary>Color space information</summary>
            ColorSpace = &hA001
#End Region
    
#Region "Tags Relating to Image Configuration"
        
            ''' <summary>Meaning of each component</summary>
            ComponentsConfiguration = &h9101
            ''' <summary>Image compression mode</summary>
            CompressedBitsPerPixel = &h9102
            ''' <summary>Valid image width</summary>
            PixelXDimension = &hA002
            ''' <summary>Valid image height</summary>
            PixelYDimension = &hA003
#End Region
    
#Region "Tags Relating to User Information"
        
            ''' <summary>Manufacturer notes</summary>
            MakerNote = &h927C
            ''' <summary>User comments</summary>
            UserComment = &h9286
#End Region
    
#Region "Tag Relating to Related File Information"
        
            ''' <summary>Related audio file</summary>
            RelatedSoundFile = &hA004
#End Region
    
#Region "Tags Relating to Date and Time"
        
            ''' <summary>Date and time of original data generation</summary>
            DateTimeOriginal = &h9003
            ''' <summary>Date and time of digital data generation</summary>
            DateTimeDigitized = &h9004
            ''' <summary>DateTime subseconds</summary>
            SubSecTime = &h9290
            ''' <summary>DateTimeOriginal subseconds</summary>
            SubSecTimeOriginal = &h9291
            ''' <summary>DateTimeDigitized subseconds</summary>
            SubSecTimeDigitized = &h9292
#End Region
    
#Region "Tags Relating to Picture-Taking Conditions"
        
            ''' <summary>Exposure time</summary>
            ExposureTime = &h829A
            ''' <summary>F number</summary>
            FNumber = &h829D
            ''' <summary>Exposure program</summary>
            ExposureProgram = &h8822
            ''' <summary>Spectral sensitivity</summary>
            SpectralSensitivity = &h8824
            ''' <summary>ISO speed rating</summary>
            ISOSpeedRatings = &h8827
            ''' <summary>Optoelectric conversion factor</summary>
            OECF = &h8828
            ''' <summary>Shutter speed</summary>
            ShutterSpeedValue = &h9201
            ''' <summary>Aperture</summary>
            ApertureValue = &h9202
            ''' <summary>Brightness</summary>
            BrightnessValue = &h9203
            ''' <summary>Exposure bias</summary>
            ExposureBiasValue = &h9204
            ''' <summary>Maximum lens aperture</summary>
            MaxApertureValue = &h9205
            ''' <summary>Subject distance</summary>
            SubjectDistance = &h9206
            ''' <summary>Metering mode</summary>
            MeteringMode = &h9207
            ''' <summary>Light source</summary>
            LightSource = &h9208
            ''' <summary>Flash</summary>
            Flash = &h9209
            ''' <summary>Lens focal length</summary>
            FocalLength = &h920A
            ''' <summary>Subject area</summary>
            SubjectArea = &h9214
            ''' <summary>Flash energy</summary>
            FlashEnergy = &hA20B
            ''' <summary>Spatial frequency response</summary>
            SpatialFrequencyResponse = &hA20C
            ''' <summary>Focal plane X resolution</summary>
            FocalPlaneXResolution = &hA20E
            ''' <summary>Focal plane Y resolution</summary>
            FocalPlaneYResolution = &hA20F
            ''' <summary>Focal plane resolution unit</summary>
            FocalPlaneResolutionUnit = &hA210
            ''' <summary>Subject location</summary>
            SubjectLocation = &hA214
            ''' <summary>Exposure index</summary>
            ExposureIndex = &hA215
            ''' <summary>Sensing method</summary>
            SensingMethod = &hA217
            ''' <summary>File source</summary>
            FileSource = &hA300
            ''' <summary>Scene type</summary>
            SceneType = &hA301
            ''' <summary>CFA pattern</summary>
            CFAPattern = &hA302
            ''' <summary>Custom image processing</summary>
            CustomRendered = &hA401
            ''' <summary>Exposure mode</summary>
            ExposureMode = &hA402
            ''' <summary>White balance</summary>
            WhiteBalance = &hA403
            ''' <summary>Digital zoom ratio</summary>
            DigitalZoomRatio = &hA404
            ''' <summary>Focal length in 35 mm film</summary>
            FocalLengthIn35mmFilm = &hA405
            ''' <summary>Scene capture type</summary>
            SceneCaptureType = &hA406
            ''' <summary>Gain control</summary>
            GainControl = &hA407
            ''' <summary>Contrast</summary>
            Contrast = &hA408
            ''' <summary>Saturation</summary>
            Saturation = &hA409
            ''' <summary>Sharpness</summary>
            Sharpness = &hA40A
            ''' <summary>Device settings description</summary>
            DeviceSettingDescription = &hA40B
            ''' <summary>Subject distance range</summary>
            SubjectDistanceRange = &hA40C
#End Region
    
#Region "Other tags"
        
            ''' <summary>Unique image ID</summary>
            ImageUniqueID = &hA420
#End Region
    
        End Enum
        
        <CLSCompliant(False)> Public Enum GPSTags As UShort
            
#Region "Tags Relating to GPS"
        
            ''' <summary>GPS tag version</summary>
            GPSVersionID = &h0
            ''' <summary>North or South Latitude</summary>
            GPSLatitudeRef = &h1
            ''' <summary>Latitude</summary>
            GPSLatitude = &h2
            ''' <summary>East or West Longitude</summary>
            GPSLongitudeRef = &h3
            ''' <summary>Longitude</summary>
            GPSLongitude = &h4
            ''' <summary>Altitude reference</summary>
            GPSAltitudeRef = &h5
            ''' <summary>Altitude</summary>
            GPSAltitude = &h6
            ''' <summary>GPS time (atomic clock)</summary>
            GPSTimeStamp = &h7
            ''' <summary>GPS satellites used for measurement</summary>
            GPSSatellites = &h8
            ''' <summary>GPS receiver status</summary>
            GPSStatus = &h9
            ''' <summary>GPS measurement mode</summary>
            GPSMeasureMode = &hA
            ''' <summary>Measurement precision</summary>
            GPSDOP = &hB
            ''' <summary>Speed unit</summary>
            GPSSpeedRef = &hC
            ''' <summary>Speed of GPS receiver</summary>
            GPSSpeed = &hD
            ''' <summary>Reference for direction of movement</summary>
            GPSTrackRef = &hE
            ''' <summary>Direction of movement</summary>
            GPSTrack = &hF
            ''' <summary>Reference for direction of image</summary>
            GPSImgDirectionRef = &h10
            ''' <summary>Direction of image</summary>
            GPSImgDirection = &h11
            ''' <summary>Geodetic survey data used</summary>
            GPSMapDatum = &h12
            ''' <summary>Reference for latitude of destination</summary>
            GPSDestLatitudeRef = &h13
            ''' <summary>Latitude of destination</summary>
            GPSDestLatitude = &h14
            ''' <summary>Reference for longitude of destination</summary>
            GPSDestLongitudeRef = &h15
            ''' <summary>Longitude of destination</summary>
            GPSDestLongitude = &h16
            ''' <summary>Reference for bearing of destination</summary>
            GPSDestBearingRef = &h17
            ''' <summary>Bearing of destination</summary>
            GPSDestBearing = &h18
            ''' <summary>Reference for distance to destination</summary>
            GPSDestDistanceRef = &h19
            ''' <summary>Distance to destination</summary>
            GPSDestDistance = &h1A
            ''' <summary>Name of GPS processing method</summary>
            GPSProcessingMethod = &h1B
            ''' <summary>Name of GPS area</summary>
            GPSAreaInformation = &h1C
            ''' <summary>GPS date</summary>
            GPSDateStamp = &h1D
            ''' <summary>GPS differential correction</summary>
            GPSDifferential = &h1E
#End Region
    
        End Enum
        
        Partial Public Class ExifTag
            ''' <summary>Tag number of this tag</summary>
            <CLSCompliant(False)> Public ReadOnly Tag As UShort
            ''' <summary>Number of components of this tag</summary>
            ''' <remarks>Can be 0 if number of components is not constant</remarks>
            Public ReadOnly Components As Integer
            ''' <summary>Type od data stored in this tag</summary>
            ''' <remarks>The most preffered and the widest datatype is listed as first</remarks>
            <CLSCompliant(False)> Public ReadOnly DataTypes As Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes()
            ''' <sumary>CTor</summary>
            ''' <param name="Number">Tag number</param>
            ''' <param name="Componets">Number of components</param>
            ''' <param name="DataTypes">Possible datatypes of this tag. The most preffered and the widest data type must be at first index</param>
            <CLSCompliant(False)> Public Sub New(ByVal Number As UShort, ByVal Components As Integer, ParamArray ByVal DataTypes As Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes())
                Me.Tag = Number
                Me.Components = Components
                Me.DataTypes = DataTypes
            End Sub
        End Class
        
        Public NotInheritable Class MainIFD
            
#Region "Sub IFD pointers"
        
            ''' <summary>Type of  Exif IFD Pointer tag</summary>
            Public Shared ReadOnly Property TypeOfExifIFD
                Get
                    Return New ExifTag(&h8769, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
                End Get
            End Property

            ''' <summary>Exif IFD Pointer</summary>
            Public Property ExifIFD As 
                Get
                    Return Me(&h8769)
                End Get
                Set
                    Me(&h8769) = valu
                End Set
            End Property
            
            ''' <summary>Type of  GPS Info IFD Pointer tag</summary>
            Public Shared ReadOnly Property TypeOfGPSIFD
                Get
                    Return New ExifTag(&h8825, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
                End Get
            End Property

            ''' <summary>GPS Info IFD Pointer</summary>
            Public Property GPSIFD As 
                Get
                    Return Me(&h8825)
                End Get
                Set
                    Me(&h8825) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tags relating to image data structure"
        
            ''' <summary>Type of  Image width tag</summary>
            Public Shared ReadOnly Property TypeOfImageWidth
                Get
                    Return New ExifTag(&h100, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Image width</summary>
            Public Property ImageWidth As 
                Get
                    Return Me(&h100)
                End Get
                Set
                    Me(&h100) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Image height tag</summary>
            Public Shared ReadOnly Property TypeOfImageLength
                Get
                    Return New ExifTag(&h101, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Image height</summary>
            Public Property ImageLength As 
                Get
                    Return Me(&h101)
                End Get
                Set
                    Me(&h101) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Number of bits per component tag</summary>
            Public Shared ReadOnly Property TypeOfBitsPerSample
                Get
                    Return New ExifTag(&h102, 3, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Number of bits per component</summary>
            Public Property BitsPerSample As ()
                Get
                    Return Me(&h102)
                End Get
                Set
                    Me(&h102) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Compression scheme tag</summary>
            Public Shared ReadOnly Property TypeOfCompression
                Get
                    Return New ExifTag(&h103, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Compression scheme</summary>
            Public Property Compression As CompressionValues
                Get
                    Return Me(&h103)
                End Get
                Set
                    Me(&h103) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Pixel composition tag</summary>
            Public Shared ReadOnly Property TypeOfPhotometricInterpretation
                Get
                    Return New ExifTag(&h106, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Pixel composition</summary>
            Public Property PhotometricInterpretation As PhotometricInterpretationValues
                Get
                    Return Me(&h106)
                End Get
                Set
                    Me(&h106) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Orientation of image tag</summary>
            Public Shared ReadOnly Property TypeOfOrientation
                Get
                    Return New ExifTag(&h112, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Orientation of image</summary>
            Public Property Orientation As OrientationValues
                Get
                    Return Me(&h112)
                End Get
                Set
                    Me(&h112) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Number of components tag</summary>
            Public Shared ReadOnly Property TypeOfSamplesPerPixel
                Get
                    Return New ExifTag(&h115, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Number of components</summary>
            Public Property SamplesPerPixel As 
                Get
                    Return Me(&h115)
                End Get
                Set
                    Me(&h115) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Image data arrangement tag</summary>
            Public Shared ReadOnly Property TypeOfPlanarConfiguration
                Get
                    Return New ExifTag(&h11C, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Image data arrangement</summary>
            Public Property PlanarConfiguration As PlanarConfigurationValues
                Get
                    Return Me(&h11C)
                End Get
                Set
                    Me(&h11C) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Subsampling ratio of Y to C tag</summary>
            Public Shared ReadOnly Property TypeOfYCbCrSubSampling
                Get
                    Return New ExifTag(&h212, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Subsampling ratio of Y to C</summary>
            Public Property YCbCrSubSampling As ()
                Get
                    Return Me(&h212)
                End Get
                Set
                    Me(&h212) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Y and C positioning tag</summary>
            Public Shared ReadOnly Property TypeOfYCbCrPositioning
                Get
                    Return New ExifTag(&h213, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Y and C positioning</summary>
            Public Property YCbCrPositioning As YCbCrPositioningValues
                Get
                    Return Me(&h213)
                End Get
                Set
                    Me(&h213) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Image resolution in width direction tag</summary>
            Public Shared ReadOnly Property TypeOfXResolution
                Get
                    Return New ExifTag(&h11A, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Image resolution in width direction</summary>
            Public Property XResolution As 
                Get
                    Return Me(&h11A)
                End Get
                Set
                    Me(&h11A) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Image resolution in height direction tag</summary>
            Public Shared ReadOnly Property TypeOfYResolution
                Get
                    Return New ExifTag(&h11B, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Image resolution in height direction</summary>
            Public Property YResolution As 
                Get
                    Return Me(&h11B)
                End Get
                Set
                    Me(&h11B) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Unit of X and Y resolution tag</summary>
            Public Shared ReadOnly Property TypeOfResolutionUnit
                Get
                    Return New ExifTag(&h128, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Unit of X and Y resolution</summary>
            Public Property ResolutionUnit As 
                Get
                    Return Me(&h128)
                End Get
                Set
                    Me(&h128) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tags relating to recording offset"
        
            ''' <summary>Type of  Image data location tag</summary>
            Public Shared ReadOnly Property TypeOfStripOffsets
                Get
                    Return New ExifTag(&h111, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Image data location</summary>
            Public Property StripOffsets As ()
                Get
                    Return Me(&h111)
                End Get
                Set
                    Me(&h111) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Number of rows per strip tag</summary>
            Public Shared ReadOnly Property TypeOfRowsPerStrip
                Get
                    Return New ExifTag(&h116, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Number of rows per strip</summary>
            Public Property RowsPerStrip As 
                Get
                    Return Me(&h116)
                End Get
                Set
                    Me(&h116) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Bytes per compressed strip tag</summary>
            Public Shared ReadOnly Property TypeOfStripByteCounts
                Get
                    Return New ExifTag(&h117, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Bytes per compressed strip</summary>
            Public Property StripByteCounts As ()
                Get
                    Return Me(&h117)
                End Get
                Set
                    Me(&h117) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Offset to JPEG SOI tag</summary>
            Public Shared ReadOnly Property TypeOfJPEGInterchangeFormat
                Get
                    Return New ExifTag(&h201, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
                End Get
            End Property

            ''' <summary>Offset to JPEG SOI</summary>
            Public Property JPEGInterchangeFormat As 
                Get
                    Return Me(&h201)
                End Get
                Set
                    Me(&h201) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Bytes of JPEG data tag</summary>
            Public Shared ReadOnly Property TypeOfJPEGInterchangeFormatLength
                Get
                    Return New ExifTag(&h202, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
                End Get
            End Property

            ''' <summary>Bytes of JPEG data</summary>
            Public Property JPEGInterchangeFormatLength As 
                Get
                    Return Me(&h202)
                End Get
                Set
                    Me(&h202) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tags relating to image data characteristics"
        
            ''' <summary>Type of  Transfer function tag</summary>
            Public Shared ReadOnly Property TypeOfTransferFunction
                Get
                    Return New ExifTag(&h12D, 768, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Transfer function</summary>
            Public Property TransferFunction As ()
                Get
                    Return Me(&h12D)
                End Get
                Set
                    Me(&h12D) = valu
                End Set
            End Property
            
            ''' <summary>Type of  White point chromaticity tag</summary>
            Public Shared ReadOnly Property TypeOfWhitePoint
                Get
                    Return New ExifTag(&h13E, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>White point chromaticity</summary>
            Public Property WhitePoint As ()
                Get
                    Return Me(&h13E)
                End Get
                Set
                    Me(&h13E) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Chromaticities of primaries tag</summary>
            Public Shared ReadOnly Property TypeOfPrimaryChromaticities
                Get
                    Return New ExifTag(&h13F, 6, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Chromaticities of primaries</summary>
            Public Property PrimaryChromaticities As ()
                Get
                    Return Me(&h13F)
                End Get
                Set
                    Me(&h13F) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Color space transformation matrix coefficients tag</summary>
            Public Shared ReadOnly Property TypeOfYCbCrCoefficients
                Get
                    Return New ExifTag(&h211, 3, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Color space transformation matrix coefficients</summary>
            Public Property YCbCrCoefficients As ()
                Get
                    Return Me(&h211)
                End Get
                Set
                    Me(&h211) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Pair of black and white reference values tag</summary>
            Public Shared ReadOnly Property TypeOfReferenceBlackWhite
                Get
                    Return New ExifTag(&h214, 6, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Pair of black and white reference values</summary>
            Public Property ReferenceBlackWhite As ()
                Get
                    Return Me(&h214)
                End Get
                Set
                    Me(&h214) = valu
                End Set
            End Property
            
#End Region
    
#Region "Other tags"
        
            ''' <summary>Type of  File change date and time tag</summary>
            Public Shared ReadOnly Property TypeOfDateTime
                Get
                    Return New ExifTag(&h132, 20, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>File change date and time</summary>
            Public Property DateTime As ()
                Get
                    Return Me(&h132)
                End Get
                Set
                    Me(&h132) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Image title tag</summary>
            Public Shared ReadOnly Property TypeOfImageDescription
                Get
                    Return New ExifTag(&h10E, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Image title</summary>
            Public Property ImageDescription As ()
                Get
                    Return Me(&h10E)
                End Get
                Set
                    Me(&h10E) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Image input equipment manufacturer tag</summary>
            Public Shared ReadOnly Property TypeOfMake
                Get
                    Return New ExifTag(&h10F, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Image input equipment manufacturer</summary>
            Public Property Make As ()
                Get
                    Return Me(&h10F)
                End Get
                Set
                    Me(&h10F) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Image input equipment model tag</summary>
            Public Shared ReadOnly Property TypeOfModel
                Get
                    Return New ExifTag(&h110, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Image input equipment model</summary>
            Public Property Model As ()
                Get
                    Return Me(&h110)
                End Get
                Set
                    Me(&h110) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Software used tag</summary>
            Public Shared ReadOnly Property TypeOfSoftware
                Get
                    Return New ExifTag(&h131, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Software used</summary>
            Public Property Software As ()
                Get
                    Return Me(&h131)
                End Get
                Set
                    Me(&h131) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Person who created the image tag</summary>
            Public Shared ReadOnly Property TypeOfArtist
                Get
                    Return New ExifTag(&h13B, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Person who created the image</summary>
            Public Property Artist As ()
                Get
                    Return Me(&h13B)
                End Get
                Set
                    Me(&h13B) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Copyright holder tag</summary>
            Public Shared ReadOnly Property TypeOfCopyright
                Get
                    Return New ExifTag(&h8298, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Copyright holder</summary>
            Public Property Copyright As ()
                Get
                    Return Me(&h8298)
                End Get
                Set
                    Me(&h8298) = valu
                End Set
            End Property
            
#End Region
    
        End Class
        
        Public NotInheritable Class ExifIFD
            
#Region "Sub IFD pointers"
        
            ''' <summary>Type of  Interoperability IFD Pointer tag</summary>
            Public Shared ReadOnly Property TypeOfInteroperabilityIFD
                Get
                    Return New ExifTag(&hA005, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
                End Get
            End Property

            ''' <summary>Interoperability IFD Pointer</summary>
            Public Property InteroperabilityIFD As 
                Get
                    Return Me(&hA005)
                End Get
                Set
                    Me(&hA005) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tags Relating to Version"
        
            ''' <summary>Type of  Exif version tag</summary>
            Public Shared ReadOnly Property TypeOfExifVersion
                Get
                    Return New ExifTag(&h9000, 4, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Exif version</summary>
            Public Property ExifVersion As ()
                Get
                    Return Me(&h9000)
                End Get
                Set
                    Me(&h9000) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Supported Flashpix version tag</summary>
            Public Shared ReadOnly Property TypeOfFlashpixVersion
                Get
                    Return New ExifTag(&hA000, 4, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Supported Flashpix version</summary>
            Public Property FlashpixVersion As FlashpixVersionValues()
                Get
                    Return Me(&hA000)
                End Get
                Set
                    Me(&hA000) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tag Relating to Image Data Characteristics"
        
            ''' <summary>Type of  Color space information tag</summary>
            Public Shared ReadOnly Property TypeOfColorSpace
                Get
                    Return New ExifTag(&hA001, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Color space information</summary>
            Public Property ColorSpace As ColorSpaceValues
                Get
                    Return Me(&hA001)
                End Get
                Set
                    Me(&hA001) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tags Relating to Image Configuration"
        
            ''' <summary>Type of  Meaning of each component tag</summary>
            Public Shared ReadOnly Property TypeOfComponentsConfiguration
                Get
                    Return New ExifTag(&h9101, 4, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Meaning of each component</summary>
            Public Property ComponentsConfiguration As ComponentsConfigurationValues()
                Get
                    Return Me(&h9101)
                End Get
                Set
                    Me(&h9101) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Image compression mode tag</summary>
            Public Shared ReadOnly Property TypeOfCompressedBitsPerPixel
                Get
                    Return New ExifTag(&h9102, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Image compression mode</summary>
            Public Property CompressedBitsPerPixel As 
                Get
                    Return Me(&h9102)
                End Get
                Set
                    Me(&h9102) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Valid image width tag</summary>
            Public Shared ReadOnly Property TypeOfPixelXDimension
                Get
                    Return New ExifTag(&hA002, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Valid image width</summary>
            Public Property PixelXDimension As 
                Get
                    Return Me(&hA002)
                End Get
                Set
                    Me(&hA002) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Valid image height tag</summary>
            Public Shared ReadOnly Property TypeOfPixelYDimension
                Get
                    Return New ExifTag(&hA003, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Valid image height</summary>
            Public Property PixelYDimension As 
                Get
                    Return Me(&hA003)
                End Get
                Set
                    Me(&hA003) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tags Relating to User Information"
        
            ''' <summary>Type of  Manufacturer notes tag</summary>
            Public Shared ReadOnly Property TypeOfMakerNote
                Get
                    Return New ExifTag(&h927C, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Manufacturer notes</summary>
            Public Property MakerNote As ()
                Get
                    Return Me(&h927C)
                End Get
                Set
                    Me(&h927C) = valu
                End Set
            End Property
            
            ''' <summary>Type of  User comments tag</summary>
            Public Shared ReadOnly Property TypeOfUserComment
                Get
                    Return New ExifTag(&h9286, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>User comments</summary>
            Public Property UserComment As ()
                Get
                    Return Me(&h9286)
                End Get
                Set
                    Me(&h9286) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tag Relating to Related File Information"
        
            ''' <summary>Type of  Related audio file tag</summary>
            Public Shared ReadOnly Property TypeOfRelatedSoundFile
                Get
                    Return New ExifTag(&hA004, 13, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Related audio file</summary>
            Public Property RelatedSoundFile As ()
                Get
                    Return Me(&hA004)
                End Get
                Set
                    Me(&hA004) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tags Relating to Date and Time"
        
            ''' <summary>Type of  Date and time of original data generation tag</summary>
            Public Shared ReadOnly Property TypeOfDateTimeOriginal
                Get
                    Return New ExifTag(&h9003, 20, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Date and time of original data generation</summary>
            Public Property DateTimeOriginal As ()
                Get
                    Return Me(&h9003)
                End Get
                Set
                    Me(&h9003) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Date and time of digital data generation tag</summary>
            Public Shared ReadOnly Property TypeOfDateTimeDigitized
                Get
                    Return New ExifTag(&h9004, 20, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Date and time of digital data generation</summary>
            Public Property DateTimeDigitized As ()
                Get
                    Return Me(&h9004)
                End Get
                Set
                    Me(&h9004) = valu
                End Set
            End Property
            
            ''' <summary>Type of  DateTime subseconds tag</summary>
            Public Shared ReadOnly Property TypeOfSubSecTime
                Get
                    Return New ExifTag(&h9290, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>DateTime subseconds</summary>
            Public Property SubSecTime As ()
                Get
                    Return Me(&h9290)
                End Get
                Set
                    Me(&h9290) = valu
                End Set
            End Property
            
            ''' <summary>Type of  DateTimeOriginal subseconds tag</summary>
            Public Shared ReadOnly Property TypeOfSubSecTimeOriginal
                Get
                    Return New ExifTag(&h9291, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>DateTimeOriginal subseconds</summary>
            Public Property SubSecTimeOriginal As ()
                Get
                    Return Me(&h9291)
                End Get
                Set
                    Me(&h9291) = valu
                End Set
            End Property
            
            ''' <summary>Type of  DateTimeDigitized subseconds tag</summary>
            Public Shared ReadOnly Property TypeOfSubSecTimeDigitized
                Get
                    Return New ExifTag(&h9292, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>DateTimeDigitized subseconds</summary>
            Public Property SubSecTimeDigitized As ()
                Get
                    Return Me(&h9292)
                End Get
                Set
                    Me(&h9292) = valu
                End Set
            End Property
            
#End Region
    
#Region "Tags Relating to Picture-Taking Conditions"
        
            ''' <summary>Type of  Exposure time tag</summary>
            Public Shared ReadOnly Property TypeOfExposureTime
                Get
                    Return New ExifTag(&h829A, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Exposure time</summary>
            Public Property ExposureTime As 
                Get
                    Return Me(&h829A)
                End Get
                Set
                    Me(&h829A) = valu
                End Set
            End Property
            
            ''' <summary>Type of  F number tag</summary>
            Public Shared ReadOnly Property TypeOfFNumber
                Get
                    Return New ExifTag(&h829D, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>F number</summary>
            Public Property FNumber As 
                Get
                    Return Me(&h829D)
                End Get
                Set
                    Me(&h829D) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Exposure program tag</summary>
            Public Shared ReadOnly Property TypeOfExposureProgram
                Get
                    Return New ExifTag(&h8822, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Exposure program</summary>
            Public Property ExposureProgram As ExposureProgramValues
                Get
                    Return Me(&h8822)
                End Get
                Set
                    Me(&h8822) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Spectral sensitivity tag</summary>
            Public Shared ReadOnly Property TypeOfSpectralSensitivity
                Get
                    Return New ExifTag(&h8824, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Spectral sensitivity</summary>
            Public Property SpectralSensitivity As ()
                Get
                    Return Me(&h8824)
                End Get
                Set
                    Me(&h8824) = valu
                End Set
            End Property
            
            ''' <summary>Type of  ISO speed rating tag</summary>
            Public Shared ReadOnly Property TypeOfISOSpeedRatings
                Get
                    Return New ExifTag(&h8827, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>ISO speed rating</summary>
            Public Property ISOSpeedRatings As ()
                Get
                    Return Me(&h8827)
                End Get
                Set
                    Me(&h8827) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Optoelectric conversion factor tag</summary>
            Public Shared ReadOnly Property TypeOfOECF
                Get
                    Return New ExifTag(&h8828, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Optoelectric conversion factor</summary>
            Public Property OECF As ()
                Get
                    Return Me(&h8828)
                End Get
                Set
                    Me(&h8828) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Shutter speed tag</summary>
            Public Shared ReadOnly Property TypeOfShutterSpeedValue
                Get
                    Return New ExifTag(&h9201, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.SRational)
                End Get
            End Property

            ''' <summary>Shutter speed</summary>
            Public Property ShutterSpeedValue As 
                Get
                    Return Me(&h9201)
                End Get
                Set
                    Me(&h9201) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Aperture tag</summary>
            Public Shared ReadOnly Property TypeOfApertureValue
                Get
                    Return New ExifTag(&h9202, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Aperture</summary>
            Public Property ApertureValue As 
                Get
                    Return Me(&h9202)
                End Get
                Set
                    Me(&h9202) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Brightness tag</summary>
            Public Shared ReadOnly Property TypeOfBrightnessValue
                Get
                    Return New ExifTag(&h9203, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.SRational)
                End Get
            End Property

            ''' <summary>Brightness</summary>
            Public Property BrightnessValue As 
                Get
                    Return Me(&h9203)
                End Get
                Set
                    Me(&h9203) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Exposure bias tag</summary>
            Public Shared ReadOnly Property TypeOfExposureBiasValue
                Get
                    Return New ExifTag(&h9204, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.SRational)
                End Get
            End Property

            ''' <summary>Exposure bias</summary>
            Public Property ExposureBiasValue As 
                Get
                    Return Me(&h9204)
                End Get
                Set
                    Me(&h9204) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Maximum lens aperture tag</summary>
            Public Shared ReadOnly Property TypeOfMaxApertureValue
                Get
                    Return New ExifTag(&h9205, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.SRational)
                End Get
            End Property

            ''' <summary>Maximum lens aperture</summary>
            Public Property MaxApertureValue As 
                Get
                    Return Me(&h9205)
                End Get
                Set
                    Me(&h9205) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Subject distance tag</summary>
            Public Shared ReadOnly Property TypeOfSubjectDistance
                Get
                    Return New ExifTag(&h9206, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Subject distance</summary>
            Public Property SubjectDistance As 
                Get
                    Return Me(&h9206)
                End Get
                Set
                    Me(&h9206) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Metering mode tag</summary>
            Public Shared ReadOnly Property TypeOfMeteringMode
                Get
                    Return New ExifTag(&h9207, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Metering mode</summary>
            Public Property MeteringMode As MeteringModeValues
                Get
                    Return Me(&h9207)
                End Get
                Set
                    Me(&h9207) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Light source tag</summary>
            Public Shared ReadOnly Property TypeOfLightSource
                Get
                    Return New ExifTag(&h9208, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Light source</summary>
            Public Property LightSource As LightSourceValues
                Get
                    Return Me(&h9208)
                End Get
                Set
                    Me(&h9208) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Flash tag</summary>
            Public Shared ReadOnly Property TypeOfFlash
                Get
                    Return New ExifTag(&h9209, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Flash</summary>
            Public Property Flash As 
                Get
                    Return Me(&h9209)
                End Get
                Set
                    Me(&h9209) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Lens focal length tag</summary>
            Public Shared ReadOnly Property TypeOfFocalLength
                Get
                    Return New ExifTag(&h920A, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Lens focal length</summary>
            Public Property FocalLength As 
                Get
                    Return Me(&h920A)
                End Get
                Set
                    Me(&h920A) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Subject area tag</summary>
            Public Shared ReadOnly Property TypeOfSubjectArea
                Get
                    Return New ExifTag(&h9214, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Subject area</summary>
            Public Property SubjectArea As ()
                Get
                    Return Me(&h9214)
                End Get
                Set
                    Me(&h9214) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Flash energy tag</summary>
            Public Shared ReadOnly Property TypeOfFlashEnergy
                Get
                    Return New ExifTag(&hA20B, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Flash energy</summary>
            Public Property FlashEnergy As 
                Get
                    Return Me(&hA20B)
                End Get
                Set
                    Me(&hA20B) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Spatial frequency response tag</summary>
            Public Shared ReadOnly Property TypeOfSpatialFrequencyResponse
                Get
                    Return New ExifTag(&hA20C, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Spatial frequency response</summary>
            Public Property SpatialFrequencyResponse As ()
                Get
                    Return Me(&hA20C)
                End Get
                Set
                    Me(&hA20C) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Focal plane X resolution tag</summary>
            Public Shared ReadOnly Property TypeOfFocalPlaneXResolution
                Get
                    Return New ExifTag(&hA20E, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Focal plane X resolution</summary>
            Public Property FocalPlaneXResolution As 
                Get
                    Return Me(&hA20E)
                End Get
                Set
                    Me(&hA20E) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Focal plane Y resolution tag</summary>
            Public Shared ReadOnly Property TypeOfFocalPlaneYResolution
                Get
                    Return New ExifTag(&hA20F, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Focal plane Y resolution</summary>
            Public Property FocalPlaneYResolution As 
                Get
                    Return Me(&hA20F)
                End Get
                Set
                    Me(&hA20F) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Focal plane resolution unit tag</summary>
            Public Shared ReadOnly Property TypeOfFocalPlaneResolutionUnit
                Get
                    Return New ExifTag(&hA210, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Focal plane resolution unit</summary>
            Public Property FocalPlaneResolutionUnit As 
                Get
                    Return Me(&hA210)
                End Get
                Set
                    Me(&hA210) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Subject location tag</summary>
            Public Shared ReadOnly Property TypeOfSubjectLocation
                Get
                    Return New ExifTag(&hA214, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Subject location</summary>
            Public Property SubjectLocation As ()
                Get
                    Return Me(&hA214)
                End Get
                Set
                    Me(&hA214) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Exposure index tag</summary>
            Public Shared ReadOnly Property TypeOfExposureIndex
                Get
                    Return New ExifTag(&hA215, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Exposure index</summary>
            Public Property ExposureIndex As 
                Get
                    Return Me(&hA215)
                End Get
                Set
                    Me(&hA215) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Sensing method tag</summary>
            Public Shared ReadOnly Property TypeOfSensingMethod
                Get
                    Return New ExifTag(&hA217, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.Int16)
                End Get
            End Property

            ''' <summary>Sensing method</summary>
            Public Property SensingMethod As SensingMethodValues
                Get
                    Return Me(&hA217)
                End Get
                Set
                    Me(&hA217) = valu
                End Set
            End Property
            
            ''' <summary>Type of  File source tag</summary>
            Public Shared ReadOnly Property TypeOfFileSource
                Get
                    Return New ExifTag(&hA300, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>File source</summary>
            Public Property FileSource As FileSourceValues
                Get
                    Return Me(&hA300)
                End Get
                Set
                    Me(&hA300) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Scene type tag</summary>
            Public Shared ReadOnly Property TypeOfSceneType
                Get
                    Return New ExifTag(&hA301, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Scene type</summary>
            Public Property SceneType As 
                Get
                    Return Me(&hA301)
                End Get
                Set
                    Me(&hA301) = valu
                End Set
            End Property
            
            ''' <summary>Type of  CFA pattern tag</summary>
            Public Shared ReadOnly Property TypeOfCFAPattern
                Get
                    Return New ExifTag(&hA302, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>CFA pattern</summary>
            Public Property CFAPattern As ()
                Get
                    Return Me(&hA302)
                End Get
                Set
                    Me(&hA302) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Custom image processing tag</summary>
            Public Shared ReadOnly Property TypeOfCustomRendered
                Get
                    Return New ExifTag(&hA401, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Custom image processing</summary>
            Public Property CustomRendered As CustomRenderedValues
                Get
                    Return Me(&hA401)
                End Get
                Set
                    Me(&hA401) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Exposure mode tag</summary>
            Public Shared ReadOnly Property TypeOfExposureMode
                Get
                    Return New ExifTag(&hA402, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Exposure mode</summary>
            Public Property ExposureMode As ExposureModeValues
                Get
                    Return Me(&hA402)
                End Get
                Set
                    Me(&hA402) = valu
                End Set
            End Property
            
            ''' <summary>Type of  White balance tag</summary>
            Public Shared ReadOnly Property TypeOfWhiteBalance
                Get
                    Return New ExifTag(&hA403, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>White balance</summary>
            Public Property WhiteBalance As WhiteBalanceValues
                Get
                    Return Me(&hA403)
                End Get
                Set
                    Me(&hA403) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Digital zoom ratio tag</summary>
            Public Shared ReadOnly Property TypeOfDigitalZoomRatio
                Get
                    Return New ExifTag(&hA404, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Digital zoom ratio</summary>
            Public Property DigitalZoomRatio As 
                Get
                    Return Me(&hA404)
                End Get
                Set
                    Me(&hA404) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Focal length in 35 mm film tag</summary>
            Public Shared ReadOnly Property TypeOfFocalLengthIn35mmFilm
                Get
                    Return New ExifTag(&hA405, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Focal length in 35 mm film</summary>
            Public Property FocalLengthIn35mmFilm As FocalLengthIn35mmFilmValues
                Get
                    Return Me(&hA405)
                End Get
                Set
                    Me(&hA405) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Scene capture type tag</summary>
            Public Shared ReadOnly Property TypeOfSceneCaptureType
                Get
                    Return New ExifTag(&hA406, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Scene capture type</summary>
            Public Property SceneCaptureType As 
                Get
                    Return Me(&hA406)
                End Get
                Set
                    Me(&hA406) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Gain control tag</summary>
            Public Shared ReadOnly Property TypeOfGainControl
                Get
                    Return New ExifTag(&hA407, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Gain control</summary>
            Public Property GainControl As GainControlValues
                Get
                    Return Me(&hA407)
                End Get
                Set
                    Me(&hA407) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Contrast tag</summary>
            Public Shared ReadOnly Property TypeOfContrast
                Get
                    Return New ExifTag(&hA408, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Contrast</summary>
            Public Property Contrast As ContrastValues
                Get
                    Return Me(&hA408)
                End Get
                Set
                    Me(&hA408) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Saturation tag</summary>
            Public Shared ReadOnly Property TypeOfSaturation
                Get
                    Return New ExifTag(&hA409, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Saturation</summary>
            Public Property Saturation As SaturationValues
                Get
                    Return Me(&hA409)
                End Get
                Set
                    Me(&hA409) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Sharpness tag</summary>
            Public Shared ReadOnly Property TypeOfSharpness
                Get
                    Return New ExifTag(&hA40A, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Sharpness</summary>
            Public Property Sharpness As SharpnessValues
                Get
                    Return Me(&hA40A)
                End Get
                Set
                    Me(&hA40A) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Device settings description tag</summary>
            Public Shared ReadOnly Property TypeOfDeviceSettingDescription
                Get
                    Return New ExifTag(&hA40B, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Device settings description</summary>
            Public Property DeviceSettingDescription As ()
                Get
                    Return Me(&hA40B)
                End Get
                Set
                    Me(&hA40B) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Subject distance range tag</summary>
            Public Shared ReadOnly Property TypeOfSubjectDistanceRange
                Get
                    Return New ExifTag(&hA40C, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>Subject distance range</summary>
            Public Property SubjectDistanceRange As SubjectDistanceRangeValues
                Get
                    Return Me(&hA40C)
                End Get
                Set
                    Me(&hA40C) = valu
                End Set
            End Property
            
#End Region
    
#Region "Other tags"
        
            ''' <summary>Type of  Unique image ID tag</summary>
            Public Shared ReadOnly Property TypeOfImageUniqueID
                Get
                    Return New ExifTag(&hA420, 33, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Unique image ID</summary>
            Public Property ImageUniqueID As ()
                Get
                    Return Me(&hA420)
                End Get
                Set
                    Me(&hA420) = valu
                End Set
            End Property
            
#End Region
    
        End Class
        
        Public NotInheritable Class GPSIFD
            
#Region "Tags Relating to GPS"
        
            ''' <summary>Type of  GPS tag version tag</summary>
            Public Shared ReadOnly Property TypeOfGPSVersionID
                Get
                    Return New ExifTag(&h0, 4, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.Byte)
                End Get
            End Property

            ''' <summary>GPS tag version</summary>
            Public Property GPSVersionID As ()
                Get
                    Return Me(&h0)
                End Get
                Set
                    Me(&h0) = valu
                End Set
            End Property
            
            ''' <summary>Type of  North or South Latitude tag</summary>
            Public Shared ReadOnly Property TypeOfGPSLatitudeRef
                Get
                    Return New ExifTag(&h1, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>North or South Latitude</summary>
            Public Property GPSLatitudeRef As GPSLatitudeRefValues()
                Get
                    Return Me(&h1)
                End Get
                Set
                    Me(&h1) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Latitude tag</summary>
            Public Shared ReadOnly Property TypeOfGPSLatitude
                Get
                    Return New ExifTag(&h2, 3, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Latitude</summary>
            Public Property GPSLatitude As ()
                Get
                    Return Me(&h2)
                End Get
                Set
                    Me(&h2) = valu
                End Set
            End Property
            
            ''' <summary>Type of  East or West Longitude tag</summary>
            Public Shared ReadOnly Property TypeOfGPSLongitudeRef
                Get
                    Return New ExifTag(&h3, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>East or West Longitude</summary>
            Public Property GPSLongitudeRef As GPSLongitudeRefValues()
                Get
                    Return Me(&h3)
                End Get
                Set
                    Me(&h3) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Longitude tag</summary>
            Public Shared ReadOnly Property TypeOfGPSLongitude
                Get
                    Return New ExifTag(&h4, 3, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Longitude</summary>
            Public Property GPSLongitude As ()
                Get
                    Return Me(&h4)
                End Get
                Set
                    Me(&h4) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Altitude reference tag</summary>
            Public Shared ReadOnly Property TypeOfGPSAltitudeRef
                Get
                    Return New ExifTag(&h5, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.Byte)
                End Get
            End Property

            ''' <summary>Altitude reference</summary>
            Public Property GPSAltitudeRef As GPSAltitudeRefValues
                Get
                    Return Me(&h5)
                End Get
                Set
                    Me(&h5) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Altitude tag</summary>
            Public Shared ReadOnly Property TypeOfGPSAltitude
                Get
                    Return New ExifTag(&h6, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Altitude</summary>
            Public Property GPSAltitude As 
                Get
                    Return Me(&h6)
                End Get
                Set
                    Me(&h6) = valu
                End Set
            End Property
            
            ''' <summary>Type of  GPS time (atomic clock) tag</summary>
            Public Shared ReadOnly Property TypeOfGPSTimeStamp
                Get
                    Return New ExifTag(&h7, 3, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>GPS time (atomic clock)</summary>
            Public Property GPSTimeStamp As ()
                Get
                    Return Me(&h7)
                End Get
                Set
                    Me(&h7) = valu
                End Set
            End Property
            
            ''' <summary>Type of  GPS satellites used for measurement tag</summary>
            Public Shared ReadOnly Property TypeOfGPSSatellites
                Get
                    Return New ExifTag(&h8, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>GPS satellites used for measurement</summary>
            Public Property GPSSatellites As ()
                Get
                    Return Me(&h8)
                End Get
                Set
                    Me(&h8) = valu
                End Set
            End Property
            
            ''' <summary>Type of  GPS receiver status tag</summary>
            Public Shared ReadOnly Property TypeOfGPSStatus
                Get
                    Return New ExifTag(&h9, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>GPS receiver status</summary>
            Public Property GPSStatus As GPSStatusValues()
                Get
                    Return Me(&h9)
                End Get
                Set
                    Me(&h9) = valu
                End Set
            End Property
            
            ''' <summary>Type of  GPS measurement mode tag</summary>
            Public Shared ReadOnly Property TypeOfGPSMeasureMode
                Get
                    Return New ExifTag(&hA, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>GPS measurement mode</summary>
            Public Property GPSMeasureMode As GPSMeasureModeValues()
                Get
                    Return Me(&hA)
                End Get
                Set
                    Me(&hA) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Measurement precision tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDOP
                Get
                    Return New ExifTag(&hB, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Measurement precision</summary>
            Public Property GPSDOP As 
                Get
                    Return Me(&hB)
                End Get
                Set
                    Me(&hB) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Speed unit tag</summary>
            Public Shared ReadOnly Property TypeOfGPSSpeedRef
                Get
                    Return New ExifTag(&hC, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Speed unit</summary>
            Public Property GPSSpeedRef As GPSSpeedRefValues()
                Get
                    Return Me(&hC)
                End Get
                Set
                    Me(&hC) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Speed of GPS receiver tag</summary>
            Public Shared ReadOnly Property TypeOfGPSSpeed
                Get
                    Return New ExifTag(&hD, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Speed of GPS receiver</summary>
            Public Property GPSSpeed As 
                Get
                    Return Me(&hD)
                End Get
                Set
                    Me(&hD) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Reference for direction of movement tag</summary>
            Public Shared ReadOnly Property TypeOfGPSTrackRef
                Get
                    Return New ExifTag(&hE, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Reference for direction of movement</summary>
            Public Property GPSTrackRef As GPSTrackRefValues()
                Get
                    Return Me(&hE)
                End Get
                Set
                    Me(&hE) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Direction of movement tag</summary>
            Public Shared ReadOnly Property TypeOfGPSTrack
                Get
                    Return New ExifTag(&hF, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Direction of movement</summary>
            Public Property GPSTrack As 
                Get
                    Return Me(&hF)
                End Get
                Set
                    Me(&hF) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Reference for direction of image tag</summary>
            Public Shared ReadOnly Property TypeOfGPSImgDirectionRef
                Get
                    Return New ExifTag(&h10, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Reference for direction of image</summary>
            Public Property GPSImgDirectionRef As GPSImgDirectionRefValues()
                Get
                    Return Me(&h10)
                End Get
                Set
                    Me(&h10) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Direction of image tag</summary>
            Public Shared ReadOnly Property TypeOfGPSImgDirection
                Get
                    Return New ExifTag(&h11, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Direction of image</summary>
            Public Property GPSImgDirection As 
                Get
                    Return Me(&h11)
                End Get
                Set
                    Me(&h11) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Geodetic survey data used tag</summary>
            Public Shared ReadOnly Property TypeOfGPSMapDatum
                Get
                    Return New ExifTag(&h12, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Geodetic survey data used</summary>
            Public Property GPSMapDatum As ()
                Get
                    Return Me(&h12)
                End Get
                Set
                    Me(&h12) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Reference for latitude of destination tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDestLatitudeRef
                Get
                    Return New ExifTag(&h13, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Reference for latitude of destination</summary>
            Public Property GPSDestLatitudeRef As GPSDestLatitudeRefValues()
                Get
                    Return Me(&h13)
                End Get
                Set
                    Me(&h13) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Latitude of destination tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDestLatitude
                Get
                    Return New ExifTag(&h14, 3, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Latitude of destination</summary>
            Public Property GPSDestLatitude As ()
                Get
                    Return Me(&h14)
                End Get
                Set
                    Me(&h14) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Reference for longitude of destination tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDestLongitudeRef
                Get
                    Return New ExifTag(&h15, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Reference for longitude of destination</summary>
            Public Property GPSDestLongitudeRef As GPSDestLongitudeRefValues()
                Get
                    Return Me(&h15)
                End Get
                Set
                    Me(&h15) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Longitude of destination tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDestLongitude
                Get
                    Return New ExifTag(&h16, 3, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Longitude of destination</summary>
            Public Property GPSDestLongitude As ()
                Get
                    Return Me(&h16)
                End Get
                Set
                    Me(&h16) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Reference for bearing of destination tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDestBearingRef
                Get
                    Return New ExifTag(&h17, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Reference for bearing of destination</summary>
            Public Property GPSDestBearingRef As GPSDestBearingRefValues()
                Get
                    Return Me(&h17)
                End Get
                Set
                    Me(&h17) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Bearing of destination tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDestBearing
                Get
                    Return New ExifTag(&h18, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Bearing of destination</summary>
            Public Property GPSDestBearing As 
                Get
                    Return Me(&h18)
                End Get
                Set
                    Me(&h18) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Reference for distance to destination tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDestDistanceRef
                Get
                    Return New ExifTag(&h19, 2, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>Reference for distance to destination</summary>
            Public Property GPSDestDistanceRef As ()
                Get
                    Return Me(&h19)
                End Get
                Set
                    Me(&h19) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Distance to destination tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDestDistance
                Get
                    Return New ExifTag(&h1A, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
                End Get
            End Property

            ''' <summary>Distance to destination</summary>
            Public Property GPSDestDistance As 
                Get
                    Return Me(&h1A)
                End Get
                Set
                    Me(&h1A) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Name of GPS processing method tag</summary>
            Public Shared ReadOnly Property TypeOfGPSProcessingMethod
                Get
                    Return New ExifTag(&h1B, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Name of GPS processing method</summary>
            Public Property GPSProcessingMethod As ()
                Get
                    Return Me(&h1B)
                End Get
                Set
                    Me(&h1B) = valu
                End Set
            End Property
            
            ''' <summary>Type of  Name of GPS area tag</summary>
            Public Shared ReadOnly Property TypeOfGPSAreaInformation
                Get
                    Return New ExifTag(&h1C, any, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
                End Get
            End Property

            ''' <summary>Name of GPS area</summary>
            Public Property GPSAreaInformation As ()
                Get
                    Return Me(&h1C)
                End Get
                Set
                    Me(&h1C) = valu
                End Set
            End Property
            
            ''' <summary>Type of  GPS date tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDateStamp
                Get
                    Return New ExifTag(&h1D, 11, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
                End Get
            End Property

            ''' <summary>GPS date</summary>
            Public Property GPSDateStamp As ()
                Get
                    Return Me(&h1D)
                End Get
                Set
                    Me(&h1D) = valu
                End Set
            End Property
            
            ''' <summary>Type of  GPS differential correction tag</summary>
            Public Shared ReadOnly Property TypeOfGPSDifferential
                Get
                    Return New ExifTag(&h1E, 1, Tools.Drawing.Metadata.ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property

            ''' <summary>GPS differential correction</summary>
            Public Property GPSDifferential As 
                Get
                    Return Me(&h1E)
                End Get
                Set
                    Me(&h1E) = valu
                End Set
            End Property
            
#End Region
    
        End Class
        
    End Class
    
End Namespace
        