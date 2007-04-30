' GENERATED FILE -- DO NOT EDIT
' 
' Generator: TransformCodeGenerator, Version=1.0.2672.3054, Culture=neutral, PublicKeyToken=null
' Version: 1.0.2672.3054'
'
' Generated code from "ExifTags.xml"
'
' Created: 30. dubna 2007' By:noutbuk\Honza'Namespace Drawing.MetadataPartial Public Class Exif
        Partial Public Class IFDMain
            ''' <summary>Tag numbers used in IFD0 and IFD1</summary>
            <CLSCompliant(False)> Public Enum Tags As UShort
            
#Region "Sub IFD pointers"
        
            ''' <summary>Exif IFD Pointer</summary>
            <Category("PointersMain")> ExifIFD = &h8769
            ''' <summary>GPS Info IFD Pointer</summary>
            <Category("PointersMain")> GPSIFD = &h8825
#End Region
    
#Region "Tags relating to image data structure"
        
            ''' <summary>Image width</summary>
            <Category("ImageDataStructure")> ImageWidth = &h100
            ''' <summary>Image height</summary>
            <Category("ImageDataStructure")> ImageLength = &h101
            ''' <summary>Number of bits per component</summary>
            <Category("ImageDataStructure")> BitsPerSample = &h102
            ''' <summary>Compression scheme</summary>
            <Category("ImageDataStructure")> Compression = &h103
            ''' <summary>Pixel composition</summary>
            <Category("ImageDataStructure")> PhotometricInterpretation = &h106
            ''' <summary>Orientation of image</summary>
            <Category("ImageDataStructure")> Orientation = &h112
            ''' <summary>Number of components</summary>
            <Category("ImageDataStructure")> SamplesPerPixel = &h115
            ''' <summary>Image data arrangement</summary>
            <Category("ImageDataStructure")> PlanarConfiguration = &h11C
            ''' <summary>Subsampling ratio of Y to C</summary>
            <Category("ImageDataStructure")> YCbCrSubSampling = &h212
            ''' <summary>Y and C positioning</summary>
            <Category("ImageDataStructure")> YCbCrPositioning = &h213
            ''' <summary>Image resolution in width direction</summary>
            <Category("ImageDataStructure")> XResolution = &h11A
            ''' <summary>Image resolution in height direction</summary>
            <Category("ImageDataStructure")> YResolution = &h11B
            ''' <summary>Unit of X and Y resolution</summary>
            <Category("ImageDataStructure")> ResolutionUnit = &h128
#End Region
    
#Region "Tags relating to recording offset"
        
            ''' <summary>Image data location</summary>
            <Category("RecordingOffset")> StripOffsets = &h111
            ''' <summary>Number of rows per strip</summary>
            <Category("RecordingOffset")> RowsPerStrip = &h116
            ''' <summary>Bytes per compressed strip</summary>
            <Category("RecordingOffset")> StripByteCounts = &h117
            ''' <summary>Offset to JPEG SOI</summary>
            <Category("RecordingOffset")> JPEGInterchangeFormat = &h201
            ''' <summary>Bytes of JPEG data</summary>
            <Category("RecordingOffset")> JPEGInterchangeFormatLength = &h202
#End Region
    
#Region "Tags relating to image data characteristics"
        
            ''' <summary>Transfer function</summary>
            <Category("ImageDataCharacteristicsMain")> TransferFunction = &h12D
            ''' <summary>White point chromaticity</summary>
            <Category("ImageDataCharacteristicsMain")> WhitePoint = &h13E
            ''' <summary>Chromaticities of primaries</summary>
            <Category("ImageDataCharacteristicsMain")> PrimaryChromaticities = &h13F
            ''' <summary>Color space transformation matrix coefficients</summary>
            <Category("ImageDataCharacteristicsMain")> YCbCrCoefficients = &h211
            ''' <summary>Pair of black and white reference values</summary>
            <Category("ImageDataCharacteristicsMain")> ReferenceBlackWhite = &h214
#End Region
    
#Region "Other tags"
        
            ''' <summary>File change date and time</summary>
            <Category("IFDOther")> DateTime = &h132
            ''' <summary>Image title</summary>
            <Category("IFDOther")> ImageDescription = &h10E
            ''' <summary>Image input equipment manufacturer</summary>
            <Category("IFDOther")> Make = &h10F
            ''' <summary>Image input equipment model</summary>
            <Category("IFDOther")> Model = &h110
            ''' <summary>Software used</summary>
            <Category("IFDOther")> Software = &h131
            ''' <summary>Person who created the image</summary>
            <Category("IFDOther")> Artist = &h13B
            ''' <summary>Copyright holder</summary>
            <Category("IFDOther")> Copyright = &h8298
#End Region
    
            End Enum
            ''' <summary>Gets format for tag specified</summary>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Tag"/> contains unknown value</exception>
            <CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
            Get
            Const any As ushort=0
            Select Case Tag
            Case Tags.ExifIFD : Return New ExifTagFormat(1,"ExifIFD",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
        Case Tags.GPSIFD : Return New ExifTagFormat(1,"GPSIFD",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
        Case Tags.ImageWidth : Return New ExifTagFormat(1,"ImageWidth",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32,ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.ImageLength : Return New ExifTagFormat(1,"ImageLength",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32,ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.BitsPerSample : Return New ExifTagFormat(3,"BitsPerSample",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.Compression : Return New ExifTagFormat(1,"Compression",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.PhotometricInterpretation : Return New ExifTagFormat(1,"PhotometricInterpretation",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.Orientation : Return New ExifTagFormat(1,"Orientation",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.SamplesPerPixel : Return New ExifTagFormat(1,"SamplesPerPixel",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.PlanarConfiguration : Return New ExifTagFormat(1,"PlanarConfiguration",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.YCbCrSubSampling : Return New ExifTagFormat(2,"YCbCrSubSampling",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.YCbCrPositioning : Return New ExifTagFormat(1,"YCbCrPositioning",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.XResolution : Return New ExifTagFormat(1,"XResolution",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.YResolution : Return New ExifTagFormat(1,"YResolution",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.ResolutionUnit : Return New ExifTagFormat(1,"ResolutionUnit",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.StripOffsets : Return New ExifTagFormat(any,"StripOffsets",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32,ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.RowsPerStrip : Return New ExifTagFormat(1,"RowsPerStrip",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32,ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.StripByteCounts : Return New ExifTagFormat(any,"StripByteCounts",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32,ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.JPEGInterchangeFormat : Return New ExifTagFormat(1,"JPEGInterchangeFormat",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
        Case Tags.JPEGInterchangeFormatLength : Return New ExifTagFormat(1,"JPEGInterchangeFormatLength",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
        Case Tags.TransferFunction : Return New ExifTagFormat(768,"TransferFunction",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.WhitePoint : Return New ExifTagFormat(2,"WhitePoint",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.PrimaryChromaticities : Return New ExifTagFormat(6,"PrimaryChromaticities",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.YCbCrCoefficients : Return New ExifTagFormat(3,"YCbCrCoefficients",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.ReferenceBlackWhite : Return New ExifTagFormat(6,"ReferenceBlackWhite",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.DateTime : Return New ExifTagFormat(20,"DateTime",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.ImageDescription : Return New ExifTagFormat(any,"ImageDescription",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.Make : Return New ExifTagFormat(any,"Make",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.Model : Return New ExifTagFormat(any,"Model",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.Software : Return New ExifTagFormat(any,"Software",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.Artist : Return New ExifTagFormat(any,"Artist",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.Copyright : Return New ExifTagFormat(any,"Copyright",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        
                        Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
                    End Select
                End Get
           End Property
        End Class
        
        Partial Public Class IFDExif
            ''' <summary>Tag numbers used in Exif Sub IFD</summary>
            <CLSCompliant(False)> Public Enum Tags As UShort
            
#Region "Sub IFD pointers"
        
            ''' <summary>Interoperability IFD Pointer</summary>
            <Category("PointersExif")> InteroperabilityIFD = &hA005
#End Region
    
#Region "Tags Relating to Version"
        
            ''' <summary>Exif version</summary>
            <Category("Version")> ExifVersion = &h9000
            ''' <summary>Supported Flashpix version</summary>
            <Category("Version")> FlashpixVersion = &hA000
#End Region
    
#Region "Tag Relating to Image Data Characteristics"
        
            ''' <summary>Color space information</summary>
            <Category("ImageDataCharacteristicsExif")> ColorSpace = &hA001
#End Region
    
#Region "Tags Relating to Image Configuration"
        
            ''' <summary>Meaning of each component</summary>
            <Category("ImageConfiguration")> ComponentsConfiguration = &h9101
            ''' <summary>Image compression mode</summary>
            <Category("ImageConfiguration")> CompressedBitsPerPixel = &h9102
            ''' <summary>Valid image width</summary>
            <Category("ImageConfiguration")> PixelXDimension = &hA002
            ''' <summary>Valid image height</summary>
            <Category("ImageConfiguration")> PixelYDimension = &hA003
#End Region
    
#Region "Tags Relating to User Information"
        
            ''' <summary>Manufacturer notes</summary>
            <Category("UserInformation")> MakerNote = &h927C
            ''' <summary>User comments</summary>
            <Category("UserInformation")> UserComment = &h9286
#End Region
    
#Region "Tag Relating to Related File Information"
        
            ''' <summary>Related audio file</summary>
            <Category("FileInformation")> RelatedSoundFile = &hA004
#End Region
    
#Region "Tags Relating to Date and Time"
        
            ''' <summary>Date and time of original data generation</summary>
            <Category("DateAndTime")> DateTimeOriginal = &h9003
            ''' <summary>Date and time of digital data generation</summary>
            <Category("DateAndTime")> DateTimeDigitized = &h9004
            ''' <summary>DateTime subseconds</summary>
            <Category("DateAndTime")> SubSecTime = &h9290
            ''' <summary>DateTimeOriginal subseconds</summary>
            <Category("DateAndTime")> SubSecTimeOriginal = &h9291
            ''' <summary>DateTimeDigitized subseconds</summary>
            <Category("DateAndTime")> SubSecTimeDigitized = &h9292
#End Region
    
#Region "Tags Relating to Picture-Taking Conditions"
        
            ''' <summary>Exposure time</summary>
            <Category("PictureTakingConditions")> ExposureTime = &h829A
            ''' <summary>F number</summary>
            <Category("PictureTakingConditions")> FNumber = &h829D
            ''' <summary>Exposure program</summary>
            <Category("PictureTakingConditions")> ExposureProgram = &h8822
            ''' <summary>Spectral sensitivity</summary>
            <Category("PictureTakingConditions")> SpectralSensitivity = &h8824
            ''' <summary>ISO speed rating</summary>
            <Category("PictureTakingConditions")> ISOSpeedRatings = &h8827
            ''' <summary>Optoelectric conversion factor</summary>
            <Category("PictureTakingConditions")> OECF = &h8828
            ''' <summary>Shutter speed</summary>
            <Category("PictureTakingConditions")> ShutterSpeedValue = &h9201
            ''' <summary>Aperture</summary>
            <Category("PictureTakingConditions")> ApertureValue = &h9202
            ''' <summary>Brightness</summary>
            <Category("PictureTakingConditions")> BrightnessValue = &h9203
            ''' <summary>Exposure bias</summary>
            <Category("PictureTakingConditions")> ExposureBiasValue = &h9204
            ''' <summary>Maximum lens aperture</summary>
            <Category("PictureTakingConditions")> MaxApertureValue = &h9205
            ''' <summary>Subject distance</summary>
            <Category("PictureTakingConditions")> SubjectDistance = &h9206
            ''' <summary>Metering mode</summary>
            <Category("PictureTakingConditions")> MeteringMode = &h9207
            ''' <summary>Light source</summary>
            <Category("PictureTakingConditions")> LightSource = &h9208
            ''' <summary>Flash</summary>
            <Category("PictureTakingConditions")> Flash = &h9209
            ''' <summary>Lens focal length</summary>
            <Category("PictureTakingConditions")> FocalLength = &h920A
            ''' <summary>Subject area</summary>
            <Category("PictureTakingConditions")> SubjectArea = &h9214
            ''' <summary>Flash energy</summary>
            <Category("PictureTakingConditions")> FlashEnergy = &hA20B
            ''' <summary>Spatial frequency response</summary>
            <Category("PictureTakingConditions")> SpatialFrequencyResponse = &hA20C
            ''' <summary>Focal plane X resolution</summary>
            <Category("PictureTakingConditions")> FocalPlaneXResolution = &hA20E
            ''' <summary>Focal plane Y resolution</summary>
            <Category("PictureTakingConditions")> FocalPlaneYResolution = &hA20F
            ''' <summary>Focal plane resolution unit</summary>
            <Category("PictureTakingConditions")> FocalPlaneResolutionUnit = &hA210
            ''' <summary>Subject location</summary>
            <Category("PictureTakingConditions")> SubjectLocation = &hA214
            ''' <summary>Exposure index</summary>
            <Category("PictureTakingConditions")> ExposureIndex = &hA215
            ''' <summary>Sensing method</summary>
            <Category("PictureTakingConditions")> SensingMethod = &hA217
            ''' <summary>File source</summary>
            <Category("PictureTakingConditions")> FileSource = &hA300
            ''' <summary>Scene type</summary>
            <Category("PictureTakingConditions")> SceneType = &hA301
            ''' <summary>CFA pattern</summary>
            <Category("PictureTakingConditions")> CFAPattern = &hA302
            ''' <summary>Custom image processing</summary>
            <Category("PictureTakingConditions")> CustomRendered = &hA401
            ''' <summary>Exposure mode</summary>
            <Category("PictureTakingConditions")> ExposureMode = &hA402
            ''' <summary>White balance</summary>
            <Category("PictureTakingConditions")> WhiteBalance = &hA403
            ''' <summary>Digital zoom ratio</summary>
            <Category("PictureTakingConditions")> DigitalZoomRatio = &hA404
            ''' <summary>Focal length in 35 mm film</summary>
            <Category("PictureTakingConditions")> FocalLengthIn35mmFilm = &hA405
            ''' <summary>Scene capture type</summary>
            <Category("PictureTakingConditions")> SceneCaptureType = &hA406
            ''' <summary>Gain control</summary>
            <Category("PictureTakingConditions")> GainControl = &hA407
            ''' <summary>Contrast</summary>
            <Category("PictureTakingConditions")> Contrast = &hA408
            ''' <summary>Saturation</summary>
            <Category("PictureTakingConditions")> Saturation = &hA409
            ''' <summary>Sharpness</summary>
            <Category("PictureTakingConditions")> Sharpness = &hA40A
            ''' <summary>Device settings description</summary>
            <Category("PictureTakingConditions")> DeviceSettingDescription = &hA40B
            ''' <summary>Subject distance range</summary>
            <Category("PictureTakingConditions")> SubjectDistanceRange = &hA40C
#End Region
    
#Region "Other tags"
        
            ''' <summary>Unique image ID</summary>
            <Category("OtherExif")> ImageUniqueID = &hA420
#End Region
    
            End Enum
            ''' <summary>Gets format for tag specified</summary>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Tag"/> contains unknown value</exception>
            <CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
            Get
            Const any As ushort=0
            Select Case Tag
            Case Tags.InteroperabilityIFD : Return New ExifTagFormat(1,"InteroperabilityIFD",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32)
        Case Tags.ExifVersion : Return New ExifTagFormat(4,"ExifVersion",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.FlashpixVersion : Return New ExifTagFormat(4,"FlashpixVersion",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.ColorSpace : Return New ExifTagFormat(1,"ColorSpace",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.ComponentsConfiguration : Return New ExifTagFormat(4,"ComponentsConfiguration",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.CompressedBitsPerPixel : Return New ExifTagFormat(1,"CompressedBitsPerPixel",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.PixelXDimension : Return New ExifTagFormat(1,"PixelXDimension",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32,ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.PixelYDimension : Return New ExifTagFormat(1,"PixelYDimension",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32,ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.MakerNote : Return New ExifTagFormat(any,"MakerNote",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.UserComment : Return New ExifTagFormat(any,"UserComment",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.RelatedSoundFile : Return New ExifTagFormat(13,"RelatedSoundFile",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.DateTimeOriginal : Return New ExifTagFormat(20,"DateTimeOriginal",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.DateTimeDigitized : Return New ExifTagFormat(20,"DateTimeDigitized",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.SubSecTime : Return New ExifTagFormat(any,"SubSecTime",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.SubSecTimeOriginal : Return New ExifTagFormat(any,"SubSecTimeOriginal",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.SubSecTimeDigitized : Return New ExifTagFormat(any,"SubSecTimeDigitized",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.ExposureTime : Return New ExifTagFormat(1,"ExposureTime",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.FNumber : Return New ExifTagFormat(1,"FNumber",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.ExposureProgram : Return New ExifTagFormat(1,"ExposureProgram",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.SpectralSensitivity : Return New ExifTagFormat(any,"SpectralSensitivity",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.ISOSpeedRatings : Return New ExifTagFormat(any,"ISOSpeedRatings",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.OECF : Return New ExifTagFormat(any,"OECF",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.ShutterSpeedValue : Return New ExifTagFormat(1,"ShutterSpeedValue",ExifIFDReader.DirectoryEntry.ExifDataTypes.SRational)
        Case Tags.ApertureValue : Return New ExifTagFormat(1,"ApertureValue",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.BrightnessValue : Return New ExifTagFormat(1,"BrightnessValue",ExifIFDReader.DirectoryEntry.ExifDataTypes.SRational)
        Case Tags.ExposureBiasValue : Return New ExifTagFormat(1,"ExposureBiasValue",ExifIFDReader.DirectoryEntry.ExifDataTypes.SRational)
        Case Tags.MaxApertureValue : Return New ExifTagFormat(1,"MaxApertureValue",ExifIFDReader.DirectoryEntry.ExifDataTypes.SRational)
        Case Tags.SubjectDistance : Return New ExifTagFormat(1,"SubjectDistance",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.MeteringMode : Return New ExifTagFormat(1,"MeteringMode",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.LightSource : Return New ExifTagFormat(1,"LightSource",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.Flash : Return New ExifTagFormat(1,"Flash",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.FocalLength : Return New ExifTagFormat(1,"FocalLength",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.SubjectArea : Return New ExifTagFormat(any,"SubjectArea",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.FlashEnergy : Return New ExifTagFormat(1,"FlashEnergy",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.SpatialFrequencyResponse : Return New ExifTagFormat(any,"SpatialFrequencyResponse",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.FocalPlaneXResolution : Return New ExifTagFormat(1,"FocalPlaneXResolution",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.FocalPlaneYResolution : Return New ExifTagFormat(1,"FocalPlaneYResolution",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.FocalPlaneResolutionUnit : Return New ExifTagFormat(1,"FocalPlaneResolutionUnit",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.SubjectLocation : Return New ExifTagFormat(2,"SubjectLocation",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.ExposureIndex : Return New ExifTagFormat(1,"ExposureIndex",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.SensingMethod : Return New ExifTagFormat(1,"SensingMethod",ExifIFDReader.DirectoryEntry.ExifDataTypes.Int16)
        Case Tags.FileSource : Return New ExifTagFormat(1,"FileSource",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.SceneType : Return New ExifTagFormat(1,"SceneType",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.CFAPattern : Return New ExifTagFormat(any,"CFAPattern",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.CustomRendered : Return New ExifTagFormat(1,"CustomRendered",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.ExposureMode : Return New ExifTagFormat(1,"ExposureMode",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.WhiteBalance : Return New ExifTagFormat(1,"WhiteBalance",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.DigitalZoomRatio : Return New ExifTagFormat(1,"DigitalZoomRatio",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.FocalLengthIn35mmFilm : Return New ExifTagFormat(1,"FocalLengthIn35mmFilm",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.SceneCaptureType : Return New ExifTagFormat(1,"SceneCaptureType",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.GainControl : Return New ExifTagFormat(1,"GainControl",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.Contrast : Return New ExifTagFormat(1,"Contrast",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.Saturation : Return New ExifTagFormat(1,"Saturation",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.Sharpness : Return New ExifTagFormat(1,"Sharpness",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.DeviceSettingDescription : Return New ExifTagFormat(any,"DeviceSettingDescription",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.SubjectDistanceRange : Return New ExifTagFormat(1,"SubjectDistanceRange",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        Case Tags.ImageUniqueID : Return New ExifTagFormat(33,"ImageUniqueID",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        
                        Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
                    End Select
                End Get
           End Property
        End Class
        
        Partial Public Class IFDGPS
            ''' <summary>Tag numbers used in GPS Sub IFD</summary>
            <CLSCompliant(False)> Public Enum Tags As UShort
            
#Region "Tags Relating to GPS"
        
            ''' <summary>GPS tag version</summary>
            <Category("GPS")> GPSVersionID = &h0
            ''' <summary>North or South Latitude</summary>
            <Category("GPS")> GPSLatitudeRef = &h1
            ''' <summary>Latitude</summary>
            <Category("GPS")> GPSLatitude = &h2
            ''' <summary>East or West Longitude</summary>
            <Category("GPS")> GPSLongitudeRef = &h3
            ''' <summary>Longitude</summary>
            <Category("GPS")> GPSLongitude = &h4
            ''' <summary>Altitude reference</summary>
            <Category("GPS")> GPSAltitudeRef = &h5
            ''' <summary>Altitude</summary>
            <Category("GPS")> GPSAltitude = &h6
            ''' <summary>GPS time (atomic clock)</summary>
            <Category("GPS")> GPSTimeStamp = &h7
            ''' <summary>GPS satellites used for measurement</summary>
            <Category("GPS")> GPSSatellites = &h8
            ''' <summary>GPS receiver status</summary>
            <Category("GPS")> GPSStatus = &h9
            ''' <summary>GPS measurement mode</summary>
            <Category("GPS")> GPSMeasureMode = &hA
            ''' <summary>Measurement precision</summary>
            <Category("GPS")> GPSDOP = &hB
            ''' <summary>Speed unit</summary>
            <Category("GPS")> GPSSpeedRef = &hC
            ''' <summary>Speed of GPS receiver</summary>
            <Category("GPS")> GPSSpeed = &hD
            ''' <summary>Reference for direction of movement</summary>
            <Category("GPS")> GPSTrackRef = &hE
            ''' <summary>Direction of movement</summary>
            <Category("GPS")> GPSTrack = &hF
            ''' <summary>Reference for direction of image</summary>
            <Category("GPS")> GPSImgDirectionRef = &h10
            ''' <summary>Direction of image</summary>
            <Category("GPS")> GPSImgDirection = &h11
            ''' <summary>Geodetic survey data used</summary>
            <Category("GPS")> GPSMapDatum = &h12
            ''' <summary>Reference for latitude of destination</summary>
            <Category("GPS")> GPSDestLatitudeRef = &h13
            ''' <summary>Latitude of destination</summary>
            <Category("GPS")> GPSDestLatitude = &h14
            ''' <summary>Reference for longitude of destination</summary>
            <Category("GPS")> GPSDestLongitudeRef = &h15
            ''' <summary>Longitude of destination</summary>
            <Category("GPS")> GPSDestLongitude = &h16
            ''' <summary>Reference for bearing of destination</summary>
            <Category("GPS")> GPSDestBearingRef = &h17
            ''' <summary>Bearing of destination</summary>
            <Category("GPS")> GPSDestBearing = &h18
            ''' <summary>Reference for distance to destination</summary>
            <Category("GPS")> GPSDestDistanceRef = &h19
            ''' <summary>Distance to destination</summary>
            <Category("GPS")> GPSDestDistance = &h1A
            ''' <summary>Name of GPS processing method</summary>
            <Category("GPS")> GPSProcessingMethod = &h1B
            ''' <summary>Name of GPS area</summary>
            <Category("GPS")> GPSAreaInformation = &h1C
            ''' <summary>GPS date</summary>
            <Category("GPS")> GPSDateStamp = &h1D
            ''' <summary>GPS differential correction</summary>
            <Category("GPS")> GPSDifferential = &h1E
#End Region
    
            End Enum
            ''' <summary>Gets format for tag specified</summary>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Tag"/> contains unknown value</exception>
            <CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
            Get
            Const any As ushort=0
            Select Case Tag
            Case Tags.GPSVersionID : Return New ExifTagFormat(4,"GPSVersionID",ExifIFDReader.DirectoryEntry.ExifDataTypes.Byte)
        Case Tags.GPSLatitudeRef : Return New ExifTagFormat(2,"GPSLatitudeRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSLatitude : Return New ExifTagFormat(3,"GPSLatitude",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSLongitudeRef : Return New ExifTagFormat(2,"GPSLongitudeRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSLongitude : Return New ExifTagFormat(3,"GPSLongitude",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSAltitudeRef : Return New ExifTagFormat(1,"GPSAltitudeRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.Byte)
        Case Tags.GPSAltitude : Return New ExifTagFormat(1,"GPSAltitude",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSTimeStamp : Return New ExifTagFormat(3,"GPSTimeStamp",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSSatellites : Return New ExifTagFormat(any,"GPSSatellites",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSStatus : Return New ExifTagFormat(2,"GPSStatus",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSMeasureMode : Return New ExifTagFormat(2,"GPSMeasureMode",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSDOP : Return New ExifTagFormat(1,"GPSDOP",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSSpeedRef : Return New ExifTagFormat(2,"GPSSpeedRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSSpeed : Return New ExifTagFormat(1,"GPSSpeed",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSTrackRef : Return New ExifTagFormat(2,"GPSTrackRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSTrack : Return New ExifTagFormat(1,"GPSTrack",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSImgDirectionRef : Return New ExifTagFormat(2,"GPSImgDirectionRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSImgDirection : Return New ExifTagFormat(1,"GPSImgDirection",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSMapDatum : Return New ExifTagFormat(any,"GPSMapDatum",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSDestLatitudeRef : Return New ExifTagFormat(2,"GPSDestLatitudeRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSDestLatitude : Return New ExifTagFormat(3,"GPSDestLatitude",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSDestLongitudeRef : Return New ExifTagFormat(2,"GPSDestLongitudeRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSDestLongitude : Return New ExifTagFormat(3,"GPSDestLongitude",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSDestBearingRef : Return New ExifTagFormat(2,"GPSDestBearingRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSDestBearing : Return New ExifTagFormat(1,"GPSDestBearing",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSDestDistanceRef : Return New ExifTagFormat(2,"GPSDestDistanceRef",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSDestDistance : Return New ExifTagFormat(1,"GPSDestDistance",ExifIFDReader.DirectoryEntry.ExifDataTypes.URational)
        Case Tags.GPSProcessingMethod : Return New ExifTagFormat(any,"GPSProcessingMethod",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.GPSAreaInformation : Return New ExifTagFormat(any,"GPSAreaInformation",ExifIFDReader.DirectoryEntry.ExifDataTypes.NA)
        Case Tags.GPSDateStamp : Return New ExifTagFormat(11,"GPSDateStamp",ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII)
        Case Tags.GPSDifferential : Return New ExifTagFormat(1,"GPSDifferential",ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
        
                        Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
                    End Select
                End Get
           End Property
        End Class
        
        
        Partial Public Class IFDInterop
        
        End Class
    End ClassEnd Namespace