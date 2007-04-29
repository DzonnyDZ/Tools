
' GENERATED FILE -- DO NOT EDIT
' 
' Generator: TransformCodeGenerator, Version=1.0.2672.41774, Culture=neutral, PublicKeyToken=null
' Version: 1.0.2672.41774
'
'
' Generated code from "ExifTags.xml"
'
' Created: 30. dubna 2007
' By: DZONNY\Honza
'

    
Namespace Drawing.Metadata
    Partial Public Class Exif
        
        Partial Public Class IFDMain
            <CLSCompliant(False)> Public Enum Tags As UShort
            
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
        End Class
        
        Partial Public Class IFDExif
            <CLSCompliant(False)> Public Enum Tags As UShort
            
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
        End Class
        
        Partial Public Class IFDGPS
            <CLSCompliant(False)> Public Enum Tags As UShort
            
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
        End Class
        
    End Class
    
End Namespace
        