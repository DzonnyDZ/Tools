' GENERATED FILE -- DO NOT EDIT
'
' Generator: TransformCodeGenerator, Version=1.5.3.1, Culture=neutral, PublicKeyToken=373c02ac923768e6
' Version: 1.5.3.1
'
'
' Generated code from "ExifTags.xml"
'
' Created: 15. dubna 2012
' By:BiggerBook\Honza
'
'Localize: This auto-generated file was skipped during localization
#If Config <= Nightly 'Stage: Nightly
#Region "Generated code"
Imports Tools.ComponentModelT
Imports Tools.NumericsT
Namespace MetadataT.ExifT
		Partial Public Class IfdMain
			''' <summary>Tag numbers used in IFD0 and IFD1</summary>
			''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added for enum items.</version>
			<CLSCompliant(False)> Public Enum Tags As UShort
#Region "Sub IFD pointers"
				''' <summary>Exif IFD Pointer</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Exif IFD"), Category("PointersMain")>ExifIFD = &h8769
				''' <summary>GPS Info IFD Pointer</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS IFD"), Category("PointersMain")>GPSIFD = &h8825
#End Region
#Region "Tags relating to image data structure"
				''' <summary>Image width</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Image width"), Category("ImageDataStructure")>ImageWidth = &h100
				''' <summary>Image height</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Image lenght"), Category("ImageDataStructure")>ImageLength = &h101
				''' <summary>Number of bits per component</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Bits per sample"), Category("ImageDataStructure")>BitsPerSample = &h102
				''' <summary>Compression scheme</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Compression"), Category("ImageDataStructure")>Compression = &h103
				''' <summary>Pixel composition</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Photometric interpretation"), Category("ImageDataStructure")>PhotometricInterpretation = &h106
				''' <summary>Orientation of image</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Orientation"), Category("ImageDataStructure")>Orientation = &h112
				''' <summary>Number of components</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Samples per pixel"), Category("ImageDataStructure")>SamplesPerPixel = &h115
				''' <summary>Image data arrangement</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Planar configuration"), Category("ImageDataStructure")>PlanarConfiguration = &h11C
				''' <summary>Subsampling ratio of Y to C</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("YCbCr sub-sampling"), Category("ImageDataStructure")>YCbCrSubSampling = &h212
				''' <summary>Y and C positioning</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("YCbCr positioning"), Category("ImageDataStructure")>YCbCrPositioning = &h213
				''' <summary>Image resolution in width direction</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("X-resolution"), Category("ImageDataStructure")>XResolution = &h11A
				''' <summary>Image resolution in height direction</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Y-resolution"), Category("ImageDataStructure")>YResolution = &h11B
				''' <summary>Unit of X and Y resolution</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Resolution unit"), Category("ImageDataStructure")>ResolutionUnit = &h128
#End Region
#Region "Tags relating to recording offset"
				''' <summary>Image data location</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Strip offsets"), Category("RecordingOffset")>StripOffsets = &h111
				''' <summary>Number of rows per strip</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Rows per strip"), Category("RecordingOffset")>RowsPerStrip = &h116
				''' <summary>Bytes per compressed strip</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Strip byte counts"), Category("RecordingOffset")>StripByteCounts = &h117
				''' <summary>Offset to JPEG SOI</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("JPEG interchange format"), Category("RecordingOffset")>JPEGInterchangeFormat = &h201
				''' <summary>Bytes of JPEG data</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("JPEG interchange format length"), Category("RecordingOffset")>JPEGInterchangeFormatLength = &h202
#End Region
#Region "Tags relating to image data characteristics"
				''' <summary>Transfer function</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Transfer function"), Category("ImageDataCharacteristicsMain")>TransferFunction = &h12D
				''' <summary>White point chromaticity</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("White point"), Category("ImageDataCharacteristicsMain")>WhitePoint = &h13E
				''' <summary>Chromaticities of primaries</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Primnary chromacities"), Category("ImageDataCharacteristicsMain")>PrimaryChromaticities = &h13F
				''' <summary>Color space transformation matrix coefficients</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("YCbCr coeficients"), Category("ImageDataCharacteristicsMain")>YCbCrCoefficients = &h211
				''' <summary>Pair of black and white reference values</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Reference black/white"), Category("ImageDataCharacteristicsMain")>ReferenceBlackWhite = &h214
#End Region
#Region "Other tags"
				''' <summary>File change date and time</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Date-time"), Category("IFDOther")>DateTime = &h132
				''' <summary>Image title</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Image description"), Category("IFDOther")>ImageDescription = &h10E
				''' <summary>Image input equipment manufacturer</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Make"), Category("IFDOther")>Make = &h10F
				''' <summary>Image input equipment model</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Model"), Category("IFDOther")>Model = &h110
				''' <summary>Software used</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Software"), Category("IFDOther")>Software = &h131
				''' <summary>Person who created the image</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Artist"), Category("IFDOther")>Artist = &h13B
				''' <summary>Copyright holder</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Copyright"), Category("IFDOther")>Copyright = &h8298
#End Region
			End Enum
#Region "Properties"
#Region "PointersMain"
			''' <summary>Exif IFD Pointer</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Exif IFD"), Category("PointersMain"), Description("Exif IFD Pointer")> _
			<CLSCompliant(False)> _
			Public Property ExifIFD As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.ExifIFD)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.ExifIFD) = New ExifRecord(TagFormat(Tags.ExifIFD), value.Value, True)
					Else : Records(Tags.ExifIFD) = Nothing : End If
				End Set
			End Property
			''' <summary>GPS Info IFD Pointer</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS IFD"), Category("PointersMain"), Description("GPS Info IFD Pointer")> _
			<CLSCompliant(False)> _
			Public Property GPSIFD As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.GPSIFD)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.GPSIFD) = New ExifRecord(TagFormat(Tags.GPSIFD), value.Value, True)
					Else : Records(Tags.GPSIFD) = Nothing : End If
				End Set
			End Property
#End Region
#Region "ImageDataStructure"
			''' <summary>Image width</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Image width"), Category("ImageDataStructure"), Description("Image width")> _
			<CLSCompliant(False)> _
			Public Property ImageWidth As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.ImageWidth)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.ImageWidth) = New ExifRecord(TagFormat(Tags.ImageWidth), value.Value, True)
					Else : Records(Tags.ImageWidth) = Nothing : End If
				End Set
			End Property
			''' <summary>Image height</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Image lenght"), Category("ImageDataStructure"), Description("Image height")> _
			<CLSCompliant(False)> _
			Public Property ImageLength As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.ImageLength)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.ImageLength) = New ExifRecord(TagFormat(Tags.ImageLength), value.Value, True)
					Else : Records(Tags.ImageLength) = Nothing : End If
				End Set
			End Property
			''' <summary>Number of bits per component</summary>
			''' <returns>Typical number of items in an array returned by this property is 3.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 3 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("Bits per sample"), Category("ImageDataStructure"), Description("Number of bits per component")> _
			<CLSCompliant(False)> _
			Public Property BitsPerSample As UInt16()
				Get
					Dim value As ExifRecord = Record(Tags.BitsPerSample)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is UInt16() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As UInt16
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), UInt16)
						Next
						Return ret
					Else
						Return New UInt16() {CType(value.Data, UInt16)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.BitsPerSample) = New ExifRecord(TagFormat(Tags.BitsPerSample), value, False)
					Else : Records(Tags.BitsPerSample) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="Compression"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum CompressionValues As UInt16
				''' <summary>uncompressed</summary>
				uncompressed = 1
				''' <summary>JPEG compression (thumbnails only)</summary>
				JPEG = 6
			End Enum
			''' <summary>Compression scheme</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="CompressionValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Compression"), Category("ImageDataStructure"), Description("Compression scheme")> _
			<CLSCompliant(False)> _
			Public Property Compression As Nullable(Of CompressionValues)
				Get
					Dim value As ExifRecord = Record(Tags.Compression)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(CompressionValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(CompressionValues))
					If value.HasValue Then
						Records(Tags.Compression) = New ExifRecord(TagFormat(Tags.Compression), value.Value, True)
					Else : Records(Tags.Compression) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="PhotometricInterpretation"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum PhotometricInterpretationValues As UInt16
				''' <summary>RGB</summary>
				RGB = 2
				''' <summary>YCbCr</summary>
				YCbCr = 6
			End Enum
			''' <summary>Pixel composition</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="PhotometricInterpretationValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Photometric interpretation"), Category("ImageDataStructure"), Description("Pixel composition")> _
			<CLSCompliant(False)> _
			Public Property PhotometricInterpretation As Nullable(Of PhotometricInterpretationValues)
				Get
					Dim value As ExifRecord = Record(Tags.PhotometricInterpretation)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(PhotometricInterpretationValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(PhotometricInterpretationValues))
					If value.HasValue Then
						Records(Tags.PhotometricInterpretation) = New ExifRecord(TagFormat(Tags.PhotometricInterpretation), value.Value, True)
					Else : Records(Tags.PhotometricInterpretation) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="Orientation"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum OrientationValues As UInt16
				''' <summary>The 0th row is at the visual top of the image, and the 0th column is the visual left-hand side.</summary>
				TopLeft = 1
				''' <summary>The 0th row is at the visual top of the image, and the 0th column is the visual right-hand side.</summary>
				TopRight = 2
				''' <summary>The 0th row is at the visual bottom of the image, and the 0th column is the visual right-hand side.</summary>
				BottomRight = 3
				''' <summary>The 0th row is at the visual bottom of the image, and the 0th column is the visual left-hand side.</summary>
				BottomLeft = 4
				''' <summary>The 0th row is the visual left-hand side of the image, and the 0th column is the visual top.</summary>
				LeftTop = 5
				''' <summary>The 0th row is the visual right-hand side of the image, and the 0th column is the visual top.</summary>
				RightTop = 6
				''' <summary>The 0th row is the visual right-hand side of the image, and the 0th column is the visual bottom.</summary>
				RightBottom = 7
				''' <summary>The 0th row is the visual left-hand side of the image, and the 0th column is the visual bottom.</summary>
				LeftBottom = 8
			End Enum
			''' <summary>Orientation of image</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="OrientationValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Orientation"), Category("ImageDataStructure"), Description("Orientation of image")> _
			<CLSCompliant(False)> _
			Public Property Orientation As Nullable(Of OrientationValues)
				Get
					Dim value As ExifRecord = Record(Tags.Orientation)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(OrientationValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(OrientationValues))
					If value.HasValue Then
						Records(Tags.Orientation) = New ExifRecord(TagFormat(Tags.Orientation), value.Value, True)
					Else : Records(Tags.Orientation) = Nothing : End If
				End Set
			End Property
			''' <summary>Number of components</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Samples per pixel"), Category("ImageDataStructure"), Description("Number of components")> _
			<CLSCompliant(False)> _
			Public Property SamplesPerPixel As Nullable(Of UInt16)
				Get
					Dim value As ExifRecord = Record(Tags.SamplesPerPixel)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.SamplesPerPixel) = New ExifRecord(TagFormat(Tags.SamplesPerPixel), value.Value, True)
					Else : Records(Tags.SamplesPerPixel) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="PlanarConfiguration"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum PlanarConfigurationValues As UInt16
				''' <summary>chunky format</summary>
				Chunky = 1
				''' <summary>planar format</summary>
				Planar = 2
			End Enum
			''' <summary>Image data arrangement</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="PlanarConfigurationValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Planar configuration"), Category("ImageDataStructure"), Description("Image data arrangement")> _
			<CLSCompliant(False)> _
			Public Property PlanarConfiguration As Nullable(Of PlanarConfigurationValues)
				Get
					Dim value As ExifRecord = Record(Tags.PlanarConfiguration)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(PlanarConfigurationValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(PlanarConfigurationValues))
					If value.HasValue Then
						Records(Tags.PlanarConfiguration) = New ExifRecord(TagFormat(Tags.PlanarConfiguration), value.Value, True)
					Else : Records(Tags.PlanarConfiguration) = Nothing : End If
				End Set
			End Property
			''' <summary>Subsampling ratio of Y to C</summary>
			''' <returns>Typical number of items in an array returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 2 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("YCbCr sub-sampling"), Category("ImageDataStructure"), Description("Subsampling ratio of Y to C")> _
			<CLSCompliant(False)> _
			Public Property YCbCrSubSampling As UInt16()
				Get
					Dim value As ExifRecord = Record(Tags.YCbCrSubSampling)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is UInt16() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As UInt16
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), UInt16)
						Next
						Return ret
					Else
						Return New UInt16() {CType(value.Data, UInt16)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.YCbCrSubSampling) = New ExifRecord(TagFormat(Tags.YCbCrSubSampling), value, False)
					Else : Records(Tags.YCbCrSubSampling) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="YCbCrPositioning"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum YCbCrPositioningValues As UInt16
				''' <summary>centered</summary>
				Centered = 1
				''' <summary>co-sited</summary>
				Cosited = 2
			End Enum
			''' <summary>Y and C positioning</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="YCbCrPositioningValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("YCbCr positioning"), Category("ImageDataStructure"), Description("Y and C positioning")> _
			<CLSCompliant(False)> _
			Public Property YCbCrPositioning As Nullable(Of YCbCrPositioningValues)
				Get
					Dim value As ExifRecord = Record(Tags.YCbCrPositioning)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(YCbCrPositioningValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(YCbCrPositioningValues))
					If value.HasValue Then
						Records(Tags.YCbCrPositioning) = New ExifRecord(TagFormat(Tags.YCbCrPositioning), value.Value, True)
					Else : Records(Tags.YCbCrPositioning) = Nothing : End If
				End Set
			End Property
			''' <summary>Image resolution in width direction</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("X-resolution"), Category("ImageDataStructure"), Description("Image resolution in width direction")> _
			<CLSCompliant(False)> _
			Public Property XResolution As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.XResolution)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.XResolution) = New ExifRecord(TagFormat(Tags.XResolution), value.Value, True)
					Else : Records(Tags.XResolution) = Nothing : End If
				End Set
			End Property
			''' <summary>Image resolution in height direction</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Y-resolution"), Category("ImageDataStructure"), Description("Image resolution in height direction")> _
			<CLSCompliant(False)> _
			Public Property YResolution As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.YResolution)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.YResolution) = New ExifRecord(TagFormat(Tags.YResolution), value.Value, True)
					Else : Records(Tags.YResolution) = Nothing : End If
				End Set
			End Property
			''' <summary>Unit of X and Y resolution</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Resolution unit"), Category("ImageDataStructure"), Description("Unit of X and Y resolution")> _
			<CLSCompliant(False)> _
			Public Property ResolutionUnit As Nullable(Of UInt16)
				Get
					Dim value As ExifRecord = Record(Tags.ResolutionUnit)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.ResolutionUnit) = New ExifRecord(TagFormat(Tags.ResolutionUnit), value.Value, True)
					Else : Records(Tags.ResolutionUnit) = Nothing : End If
				End Set
			End Property
#End Region
#Region "RecordingOffset"
			''' <summary>Image data location</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Strip offsets"), Category("RecordingOffset"), Description("Image data location")> _
			<CLSCompliant(False)> _
			Public Property StripOffsets As UInt32()
				Get
					Dim value As ExifRecord = Record(Tags.StripOffsets)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is UInt32() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As UInt32
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), UInt32)
						Next
						Return ret
					Else
						Return New UInt32() {CType(value.Data, UInt32)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.StripOffsets) = New ExifRecord(TagFormat(Tags.StripOffsets), value, False)
					Else : Records(Tags.StripOffsets) = Nothing : End If
				End Set
			End Property
			''' <summary>Number of rows per strip</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Rows per strip"), Category("RecordingOffset"), Description("Number of rows per strip")> _
			<CLSCompliant(False)> _
			Public Property RowsPerStrip As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.RowsPerStrip)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.RowsPerStrip) = New ExifRecord(TagFormat(Tags.RowsPerStrip), value.Value, True)
					Else : Records(Tags.RowsPerStrip) = Nothing : End If
				End Set
			End Property
			''' <summary>Bytes per compressed strip</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Strip byte counts"), Category("RecordingOffset"), Description("Bytes per compressed strip")> _
			<CLSCompliant(False)> _
			Public Property StripByteCounts As UInt32()
				Get
					Dim value As ExifRecord = Record(Tags.StripByteCounts)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is UInt32() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As UInt32
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), UInt32)
						Next
						Return ret
					Else
						Return New UInt32() {CType(value.Data, UInt32)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.StripByteCounts) = New ExifRecord(TagFormat(Tags.StripByteCounts), value, False)
					Else : Records(Tags.StripByteCounts) = Nothing : End If
				End Set
			End Property
			''' <summary>Offset to JPEG SOI</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("JPEG interchange format"), Category("RecordingOffset"), Description("Offset to JPEG SOI")> _
			<CLSCompliant(False)> _
			Public Property JPEGInterchangeFormat As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.JPEGInterchangeFormat)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.JPEGInterchangeFormat) = New ExifRecord(TagFormat(Tags.JPEGInterchangeFormat), value.Value, True)
					Else : Records(Tags.JPEGInterchangeFormat) = Nothing : End If
				End Set
			End Property
			''' <summary>Bytes of JPEG data</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("JPEG interchange format length"), Category("RecordingOffset"), Description("Bytes of JPEG data")> _
			<CLSCompliant(False)> _
			Public Property JPEGInterchangeFormatLength As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.JPEGInterchangeFormatLength)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.JPEGInterchangeFormatLength) = New ExifRecord(TagFormat(Tags.JPEGInterchangeFormatLength), value.Value, True)
					Else : Records(Tags.JPEGInterchangeFormatLength) = Nothing : End If
				End Set
			End Property
#End Region
#Region "ImageDataCharacteristicsMain"
			''' <summary>Transfer function</summary>
			''' <returns>Typical number of items in an array returned by this property is 768.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 768 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("Transfer function"), Category("ImageDataCharacteristicsMain"), Description("Transfer function")> _
			<CLSCompliant(False)> _
			Public Property TransferFunction As UInt16()
				Get
					Dim value As ExifRecord = Record(Tags.TransferFunction)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is UInt16() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As UInt16
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), UInt16)
						Next
						Return ret
					Else
						Return New UInt16() {CType(value.Data, UInt16)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.TransferFunction) = New ExifRecord(TagFormat(Tags.TransferFunction), value, False)
					Else : Records(Tags.TransferFunction) = Nothing : End If
				End Set
			End Property
			''' <summary>White point chromaticity</summary>
			''' <returns>Typical number of items in an array returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 2 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("White point"), Category("ImageDataCharacteristicsMain"), Description("White point chromaticity")> _
			<CLSCompliant(False)> _
			Public Property WhitePoint As URational()
				Get
					Dim value As ExifRecord = Record(Tags.WhitePoint)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.WhitePoint) = New ExifRecord(TagFormat(Tags.WhitePoint), value, False)
					Else : Records(Tags.WhitePoint) = Nothing : End If
				End Set
			End Property
			''' <summary>Chromaticities of primaries</summary>
			''' <returns>Typical number of items in an array returned by this property is 6.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 6 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("Primnary chromacities"), Category("ImageDataCharacteristicsMain"), Description("Chromaticities of primaries")> _
			<CLSCompliant(False)> _
			Public Property PrimaryChromaticities As URational()
				Get
					Dim value As ExifRecord = Record(Tags.PrimaryChromaticities)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.PrimaryChromaticities) = New ExifRecord(TagFormat(Tags.PrimaryChromaticities), value, False)
					Else : Records(Tags.PrimaryChromaticities) = Nothing : End If
				End Set
			End Property
			''' <summary>Color space transformation matrix coefficients</summary>
			''' <returns>Typical number of items in an array returned by this property is 3.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 3 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("YCbCr coeficients"), Category("ImageDataCharacteristicsMain"), Description("Color space transformation matrix coefficients")> _
			<CLSCompliant(False)> _
			Public Property YCbCrCoefficients As URational()
				Get
					Dim value As ExifRecord = Record(Tags.YCbCrCoefficients)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.YCbCrCoefficients) = New ExifRecord(TagFormat(Tags.YCbCrCoefficients), value, False)
					Else : Records(Tags.YCbCrCoefficients) = Nothing : End If
				End Set
			End Property
			''' <summary>Pair of black and white reference values</summary>
			''' <returns>Typical number of items in an array returned by this property is 6.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 6 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("Reference black/white"), Category("ImageDataCharacteristicsMain"), Description("Pair of black and white reference values")> _
			<CLSCompliant(False)> _
			Public Property ReferenceBlackWhite As URational()
				Get
					Dim value As ExifRecord = Record(Tags.ReferenceBlackWhite)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.ReferenceBlackWhite) = New ExifRecord(TagFormat(Tags.ReferenceBlackWhite), value, False)
					Else : Records(Tags.ReferenceBlackWhite) = Nothing : End If
				End Set
			End Property
#End Region
#Region "IFDOther"
			''' <summary>File change date and time</summary>
			''' <returns>Typical number of characters in a string returned by this property is 20.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 19 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			<DisplayName("Date-time"), Category("IFDOther"), Description("File change date and time")> _
			Public Property DateTime As String
				Get
					Dim value As ExifRecord = Record(Tags.DateTime)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.DateTime) = New ExifRecord(TagFormat(Tags.DateTime), value, False)
					Else : Records(Tags.DateTime) = Nothing : End If
				End Set
			End Property
			''' <summary>Image title</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Image description"), Category("IFDOther"), Description("Image title")> _
			Public Property ImageDescription As String
				Get
					Dim value As ExifRecord = Record(Tags.ImageDescription)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.ImageDescription) = New ExifRecord(TagFormat(Tags.ImageDescription), value, False)
					Else : Records(Tags.ImageDescription) = Nothing : End If
				End Set
			End Property
			''' <summary>Image input equipment manufacturer</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Make"), Category("IFDOther"), Description("Image input equipment manufacturer")> _
			Public Property Make As String
				Get
					Dim value As ExifRecord = Record(Tags.Make)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.Make) = New ExifRecord(TagFormat(Tags.Make), value, False)
					Else : Records(Tags.Make) = Nothing : End If
				End Set
			End Property
			''' <summary>Image input equipment model</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Model"), Category("IFDOther"), Description("Image input equipment model")> _
			Public Property Model As String
				Get
					Dim value As ExifRecord = Record(Tags.Model)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.Model) = New ExifRecord(TagFormat(Tags.Model), value, False)
					Else : Records(Tags.Model) = Nothing : End If
				End Set
			End Property
			''' <summary>Software used</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Software"), Category("IFDOther"), Description("Software used")> _
			Public Property Software As String
				Get
					Dim value As ExifRecord = Record(Tags.Software)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.Software) = New ExifRecord(TagFormat(Tags.Software), value, False)
					Else : Records(Tags.Software) = Nothing : End If
				End Set
			End Property
			''' <summary>Person who created the image</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Artist"), Category("IFDOther"), Description("Person who created the image")> _
			Public Property Artist As String
				Get
					Dim value As ExifRecord = Record(Tags.Artist)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.Artist) = New ExifRecord(TagFormat(Tags.Artist), value, False)
					Else : Records(Tags.Artist) = Nothing : End If
				End Set
			End Property
			''' <summary>Copyright holder</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Copyright"), Category("IFDOther"), Description("Copyright holder")> _
			Public Property Copyright As String
				Get
					Dim value As ExifRecord = Record(Tags.Copyright)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.Copyright) = New ExifRecord(TagFormat(Tags.Copyright), value, False)
					Else : Records(Tags.Copyright) = Nothing : End If
				End Set
			End Property
#End Region
#End Region
			''' <summary>Gets format for tag specified</summary>
			''' <exception cref="InvalidEnumArgumentException"><paramref name="Tag"/> contains unknown value</exception>
			<CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
				Get
					Const any As ushort=0
					Select Case Tag
						Case Tags.ExifIFD : Return New ExifTagFormat(1, &h8769, "ExifIFD", ExifDataTypes.UInt32)
						Case Tags.GPSIFD : Return New ExifTagFormat(1, &h8825, "GPSIFD", ExifDataTypes.UInt32)
						Case Tags.ImageWidth : Return New ExifTagFormat(1, &h100, "ImageWidth", ExifDataTypes.UInt32, ExifDataTypes.UInt16)
						Case Tags.ImageLength : Return New ExifTagFormat(1, &h101, "ImageLength", ExifDataTypes.UInt32, ExifDataTypes.UInt16)
						Case Tags.BitsPerSample : Return New ExifTagFormat(3, &h102, "BitsPerSample", ExifDataTypes.UInt16)
						Case Tags.Compression : Return New ExifTagFormat(1, &h103, "Compression", ExifDataTypes.UInt16)
						Case Tags.PhotometricInterpretation : Return New ExifTagFormat(1, &h106, "PhotometricInterpretation", ExifDataTypes.UInt16)
						Case Tags.Orientation : Return New ExifTagFormat(1, &h112, "Orientation", ExifDataTypes.UInt16)
						Case Tags.SamplesPerPixel : Return New ExifTagFormat(1, &h115, "SamplesPerPixel", ExifDataTypes.UInt16)
						Case Tags.PlanarConfiguration : Return New ExifTagFormat(1, &h11C, "PlanarConfiguration", ExifDataTypes.UInt16)
						Case Tags.YCbCrSubSampling : Return New ExifTagFormat(2, &h212, "YCbCrSubSampling", ExifDataTypes.UInt16)
						Case Tags.YCbCrPositioning : Return New ExifTagFormat(1, &h213, "YCbCrPositioning", ExifDataTypes.UInt16)
						Case Tags.XResolution : Return New ExifTagFormat(1, &h11A, "XResolution", ExifDataTypes.URational)
						Case Tags.YResolution : Return New ExifTagFormat(1, &h11B, "YResolution", ExifDataTypes.URational)
						Case Tags.ResolutionUnit : Return New ExifTagFormat(1, &h128, "ResolutionUnit", ExifDataTypes.UInt16)
						Case Tags.StripOffsets : Return New ExifTagFormat(any, &h111, "StripOffsets", ExifDataTypes.UInt32, ExifDataTypes.UInt16)
						Case Tags.RowsPerStrip : Return New ExifTagFormat(1, &h116, "RowsPerStrip", ExifDataTypes.UInt32, ExifDataTypes.UInt16)
						Case Tags.StripByteCounts : Return New ExifTagFormat(any, &h117, "StripByteCounts", ExifDataTypes.UInt32, ExifDataTypes.UInt16)
						Case Tags.JPEGInterchangeFormat : Return New ExifTagFormat(1, &h201, "JPEGInterchangeFormat", ExifDataTypes.UInt32)
						Case Tags.JPEGInterchangeFormatLength : Return New ExifTagFormat(1, &h202, "JPEGInterchangeFormatLength", ExifDataTypes.UInt32)
						Case Tags.TransferFunction : Return New ExifTagFormat(768, &h12D, "TransferFunction", ExifDataTypes.UInt16)
						Case Tags.WhitePoint : Return New ExifTagFormat(2, &h13E, "WhitePoint", ExifDataTypes.URational)
						Case Tags.PrimaryChromaticities : Return New ExifTagFormat(6, &h13F, "PrimaryChromaticities", ExifDataTypes.URational)
						Case Tags.YCbCrCoefficients : Return New ExifTagFormat(3, &h211, "YCbCrCoefficients", ExifDataTypes.URational)
						Case Tags.ReferenceBlackWhite : Return New ExifTagFormat(6, &h214, "ReferenceBlackWhite", ExifDataTypes.URational)
						Case Tags.DateTime : Return New ExifTagFormat(20, &h132, "DateTime", ExifDataTypes.ASCII)
						Case Tags.ImageDescription : Return New ExifTagFormat(any, &h10E, "ImageDescription", ExifDataTypes.ASCII)
						Case Tags.Make : Return New ExifTagFormat(any, &h10F, "Make", ExifDataTypes.ASCII)
						Case Tags.Model : Return New ExifTagFormat(any, &h110, "Model", ExifDataTypes.ASCII)
						Case Tags.Software : Return New ExifTagFormat(any, &h131, "Software", ExifDataTypes.ASCII)
						Case Tags.Artist : Return New ExifTagFormat(any, &h13B, "Artist", ExifDataTypes.ASCII)
						Case Tags.Copyright : Return New ExifTagFormat(any, &h8298, "Copyright", ExifDataTypes.ASCII)
						Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
					End Select
				End Get
			End Property
		End Class
		Partial Public Class IfdExif
			''' <summary>Tag numbers used in Exif Sub IFD</summary>
			''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added for enum items.</version>
			<CLSCompliant(False)> Public Enum Tags As UShort
#Region "Sub IFD pointers"
				''' <summary>Interoperability IFD Pointer</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Interoperability IFD"), Category("PointersExif")>InteroperabilityIFD = &hA005
#End Region
#Region "Tags Relating to Version"
				''' <summary>Exif version</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Exif version"), Category("Version")>ExifVersion = &h9000
				''' <summary>Supported Flashpix version</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Flashpix version"), Category("Version")>FlashpixVersion = &hA000
#End Region
#Region "Tag Relating to Image Data Characteristics"
				''' <summary>Color space information</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Color space"), Category("ImageDataCharacteristicsExif")>ColorSpace = &hA001
#End Region
#Region "Tags Relating to Image Configuration"
				''' <summary>Meaning of each component</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Components configuration"), Category("ImageConfiguration")>ComponentsConfiguration = &h9101
				''' <summary>Image compression mode</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Compressed bits per pixel"), Category("ImageConfiguration")>CompressedBitsPerPixel = &h9102
				''' <summary>Valid image width</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Pixel X-dimension"), Category("ImageConfiguration")>PixelXDimension = &hA002
				''' <summary>Valid image height</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Pixel Y-dimension"), Category("ImageConfiguration")>PixelYDimension = &hA003
#End Region
#Region "Tags Relating to User Information"
				''' <summary>Manufacturer notes</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Maker note"), Category("UserInformation")>MakerNote = &h927C
				''' <summary>User comments</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("User comment"), Category("UserInformation")>UserComment = &h9286
#End Region
#Region "Tag Relating to Related File Information"
				''' <summary>Related audio file</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Relate sound file"), Category("FileInformation")>RelatedSoundFile = &hA004
#End Region
#Region "Tags Relating to Date and Time"
				''' <summary>Date and time of original data generation</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				''' <version version="1.5.3">Expected number of components changed from 20 to 19. This is ad-hoc fix date format compatibility issues.</version>
				<FieldDisplayName("Date-time original"), Category("DateAndTime")>DateTimeOriginal = &h9003
				''' <summary>Date and time of digital data generation</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				''' <version version="1.5.3">Expected number of components changed from 20 to 19. This is ad-hoc fix date format compatibility issues.</version>
				<FieldDisplayName("Date-time digitized"), Category("DateAndTime")>DateTimeDigitized = &h9004
				''' <summary>DateTime subseconds</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Sub-sec time"), Category("DateAndTime")>SubSecTime = &h9290
				''' <summary>DateTimeOriginal subseconds</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Sub-sec time oroginal"), Category("DateAndTime")>SubSecTimeOriginal = &h9291
				''' <summary>DateTimeDigitized subseconds</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Sub-sec time digitized"), Category("DateAndTime")>SubSecTimeDigitized = &h9292
#End Region
#Region "Tags Relating to Picture-Taking Conditions"
				''' <summary>Exposure time</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Exposure time"), Category("PictureTakingConditions")>ExposureTime = &h829A
				''' <summary>F number</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("F number"), Category("PictureTakingConditions")>FNumber = &h829D
				''' <summary>Exposure program</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Exposure program"), Category("PictureTakingConditions")>ExposureProgram = &h8822
				''' <summary>Spectral sensitivity</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Spectral sensitivity"), Category("PictureTakingConditions")>SpectralSensitivity = &h8824
				''' <summary>ISO speed rating</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("ISO speed ratings"), Category("PictureTakingConditions")>ISOSpeedRatings = &h8827
				''' <summary>Optoelectric conversion factor</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("OECF"), Category("PictureTakingConditions")>OECF = &h8828
				''' <summary>Shutter speed</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Shutter speed value"), Category("PictureTakingConditions")>ShutterSpeedValue = &h9201
				''' <summary>Aperture</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Aperture value"), Category("PictureTakingConditions")>ApertureValue = &h9202
				''' <summary>Brightness</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Brightness value"), Category("PictureTakingConditions")>BrightnessValue = &h9203
				''' <summary>Exposure bias</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Exposure bias value"), Category("PictureTakingConditions")>ExposureBiasValue = &h9204
				''' <summary>Maximum lens aperture</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Max. aperture value"), Category("PictureTakingConditions")>MaxApertureValue = &h9205
				''' <summary>Subject distance</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Subject distance"), Category("PictureTakingConditions")>SubjectDistance = &h9206
				''' <summary>Metering mode</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Metering mode"), Category("PictureTakingConditions")>MeteringMode = &h9207
				''' <summary>Light source</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Light source"), Category("PictureTakingConditions")>LightSource = &h9208
				''' <summary>Flash</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Flash"), Category("PictureTakingConditions")>Flash = &h9209
				''' <summary>Lens focal length</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Focla length"), Category("PictureTakingConditions")>FocalLength = &h920A
				''' <summary>Subject area</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Subject area"), Category("PictureTakingConditions")>SubjectArea = &h9214
				''' <summary>Flash energy</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Flash energy"), Category("PictureTakingConditions")>FlashEnergy = &hA20B
				''' <summary>Spatial frequency response</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Spatial frequency response"), Category("PictureTakingConditions")>SpatialFrequencyResponse = &hA20C
				''' <summary>Focal plane X resolution</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Focal plane X-resolution"), Category("PictureTakingConditions")>FocalPlaneXResolution = &hA20E
				''' <summary>Focal plane Y resolution</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Focal plane Y-resolution"), Category("PictureTakingConditions")>FocalPlaneYResolution = &hA20F
				''' <summary>Focal plane resolution unit</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Focal plane resolution unit"), Category("PictureTakingConditions")>FocalPlaneResolutionUnit = &hA210
				''' <summary>Subject location</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Subject location"), Category("PictureTakingConditions")>SubjectLocation = &hA214
				''' <summary>Exposure index</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Exposure index"), Category("PictureTakingConditions")>ExposureIndex = &hA215
				''' <summary>Sensing method</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Sensing method"), Category("PictureTakingConditions")>SensingMethod = &hA217
				''' <summary>File source</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("File Source"), Category("PictureTakingConditions")>FileSource = &hA300
				''' <summary>Scene type</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Scene type"), Category("PictureTakingConditions")>SceneType = &hA301
				''' <summary>CFA pattern</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("CFA pattern"), Category("PictureTakingConditions")>CFAPattern = &hA302
				''' <summary>Custom image processing</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Custom rendered"), Category("PictureTakingConditions")>CustomRendered = &hA401
				''' <summary>Exposure mode</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Exposure mode"), Category("PictureTakingConditions")>ExposureMode = &hA402
				''' <summary>White balance</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("White balance"), Category("PictureTakingConditions")>WhiteBalance = &hA403
				''' <summary>Digital zoom ratio</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Digital zoom ratio"), Category("PictureTakingConditions")>DigitalZoomRatio = &hA404
				''' <summary>Focal length in 35 mm film</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Focal length in 35mm film"), Category("PictureTakingConditions")>FocalLengthIn35mmFilm = &hA405
				''' <summary>Scene capture type</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Scene capture type"), Category("PictureTakingConditions")>SceneCaptureType = &hA406
				''' <summary>Gain control</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Gain control"), Category("PictureTakingConditions")>GainControl = &hA407
				''' <summary>Contrast</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Contrast"), Category("PictureTakingConditions")>Contrast = &hA408
				''' <summary>Saturation</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Saturation"), Category("PictureTakingConditions")>Saturation = &hA409
				''' <summary>Sharpness</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Sharpness"), Category("PictureTakingConditions")>Sharpness = &hA40A
				''' <summary>Device settings description</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Device setting description"), Category("PictureTakingConditions")>DeviceSettingDescription = &hA40B
				''' <summary>Subject distance range</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Subject distance range"), Category("PictureTakingConditions")>SubjectDistanceRange = &hA40C
#End Region
#Region "Other tags"
				''' <summary>Unique image ID</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Image unique ID"), Category("OtherExif")>ImageUniqueID = &hA420
#End Region
			End Enum
#Region "Properties"
#Region "PointersExif"
			''' <summary>Interoperability IFD Pointer</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Interoperability IFD"), Category("PointersExif"), Description("Interoperability IFD Pointer")> _
			<CLSCompliant(False)> _
			Public Property InteroperabilityIFD As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.InteroperabilityIFD)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.InteroperabilityIFD) = New ExifRecord(TagFormat(Tags.InteroperabilityIFD), value.Value, True)
					Else : Records(Tags.InteroperabilityIFD) = Nothing : End If
				End Set
			End Property
#End Region
#Region "Version"
			''' <summary>Exif version</summary>
			''' <returns>Typical number of items in an array returned by this property is 4.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 4 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("Exif version"), Category("Version"), Description("Exif version")> _
			Public Property ExifVersion As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.ExifVersion)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.ExifVersion) = New ExifRecord(TagFormat(Tags.ExifVersion), value, False)
					Else : Records(Tags.ExifVersion) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="FlashpixVersion"/> property</summary>
			Public Enum FlashpixVersionValues As Byte
				''' <summary>Flashpix Format Version 1.0</summary>
				Flashpix10 = 0100
			End Enum
			''' <summary>Supported Flashpix version</summary>
			''' <returns>Typical number of items in an array returned by this property is 4.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 4 items</exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="FlashpixVersionValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Flashpix version"), Category("Version"), Description("Supported Flashpix version")> _
			Public Property FlashpixVersion As FlashpixVersionValues()
				Get
					Dim value As ExifRecord = Record(Tags.FlashpixVersion)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is FlashpixVersionValues() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As FlashpixVersionValues
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New FlashpixVersionValues() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As FlashpixVersionValues In value
							If Array.IndexOf([Enum].GetValues(GetType(FlashpixVersionValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(FlashpixVersionValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Records(Tags.FlashpixVersion) = New ExifRecord(TagFormat(Tags.FlashpixVersion), value, False)
					Else : Records(Tags.FlashpixVersion) = Nothing : End If
				End Set
			End Property
#End Region
#Region "ImageDataCharacteristicsExif"
			''' <summary>Possible values of the <see cref="ColorSpace"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum ColorSpaceValues As UInt16
				''' <summary>sRGB</summary>
				sRGB = 1
				''' <summary>Uncalibrated</summary>
				Uncalibrated = &hFFFF
			End Enum
			''' <summary>Color space information</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="ColorSpaceValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Color space"), Category("ImageDataCharacteristicsExif"), Description("Color space information")> _
			<CLSCompliant(False)> _
			Public Property ColorSpace As Nullable(Of ColorSpaceValues)
				Get
					Dim value As ExifRecord = Record(Tags.ColorSpace)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(ColorSpaceValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(ColorSpaceValues))
					If value.HasValue Then
						Records(Tags.ColorSpace) = New ExifRecord(TagFormat(Tags.ColorSpace), value.Value, True)
					Else : Records(Tags.ColorSpace) = Nothing : End If
				End Set
			End Property
#End Region
#Region "ImageConfiguration"
			''' <summary>Possible values of the <see cref="ComponentsConfiguration"/> property</summary>
			Public Enum ComponentsConfigurationValues As Byte
				''' <summary>does not exist</summary>
				DoesNotExist = 0
				''' <summary>Y</summary>
				Y = 1
				''' <summary>Cb</summary>
				Cb = 2
				''' <summary>Cr</summary>
				Cr = 3
				''' <summary>R</summary>
				R = 4
				''' <summary>G</summary>
				G = 5
				''' <summary>B</summary>
				B = 6
			End Enum
			''' <summary>Meaning of each component</summary>
			''' <returns>Typical number of items in an array returned by this property is 4.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 4 items</exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="ComponentsConfigurationValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Components configuration"), Category("ImageConfiguration"), Description("Meaning of each component")> _
			Public Property ComponentsConfiguration As ComponentsConfigurationValues()
				Get
					Dim value As ExifRecord = Record(Tags.ComponentsConfiguration)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is ComponentsConfigurationValues() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As ComponentsConfigurationValues
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New ComponentsConfigurationValues() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As ComponentsConfigurationValues In value
							If Array.IndexOf([Enum].GetValues(GetType(ComponentsConfigurationValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(ComponentsConfigurationValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Records(Tags.ComponentsConfiguration) = New ExifRecord(TagFormat(Tags.ComponentsConfiguration), value, False)
					Else : Records(Tags.ComponentsConfiguration) = Nothing : End If
				End Set
			End Property
			''' <summary>Image compression mode</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Compressed bits per pixel"), Category("ImageConfiguration"), Description("Image compression mode")> _
			<CLSCompliant(False)> _
			Public Property CompressedBitsPerPixel As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.CompressedBitsPerPixel)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.CompressedBitsPerPixel) = New ExifRecord(TagFormat(Tags.CompressedBitsPerPixel), value.Value, True)
					Else : Records(Tags.CompressedBitsPerPixel) = Nothing : End If
				End Set
			End Property
			''' <summary>Valid image width</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Pixel X-dimension"), Category("ImageConfiguration"), Description("Valid image width")> _
			<CLSCompliant(False)> _
			Public Property PixelXDimension As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.PixelXDimension)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.PixelXDimension) = New ExifRecord(TagFormat(Tags.PixelXDimension), value.Value, True)
					Else : Records(Tags.PixelXDimension) = Nothing : End If
				End Set
			End Property
			''' <summary>Valid image height</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Pixel Y-dimension"), Category("ImageConfiguration"), Description("Valid image height")> _
			<CLSCompliant(False)> _
			Public Property PixelYDimension As Nullable(Of UInt32)
				Get
					Dim value As ExifRecord = Record(Tags.PixelYDimension)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt32)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.PixelYDimension) = New ExifRecord(TagFormat(Tags.PixelYDimension), value.Value, True)
					Else : Records(Tags.PixelYDimension) = Nothing : End If
				End Set
			End Property
#End Region
#Region "UserInformation"
			''' <summary>Manufacturer notes</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Maker note"), Category("UserInformation"), Description("Manufacturer notes")> _
			Public Property MakerNote As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.MakerNote)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.MakerNote) = New ExifRecord(TagFormat(Tags.MakerNote), value, False)
					Else : Records(Tags.MakerNote) = Nothing : End If
				End Set
			End Property
			''' <summary>User comments</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("User comment"), Category("UserInformation"), Description("User comments")> _
			Public Property UserComment As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.UserComment)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.UserComment) = New ExifRecord(TagFormat(Tags.UserComment), value, False)
					Else : Records(Tags.UserComment) = Nothing : End If
				End Set
			End Property
#End Region
#Region "FileInformation"
			''' <summary>Related audio file</summary>
			''' <returns>Typical number of characters in a string returned by this property is 13.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 12 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			<DisplayName("Relate sound file"), Category("FileInformation"), Description("Related audio file")> _
			Public Property RelatedSoundFile As String
				Get
					Dim value As ExifRecord = Record(Tags.RelatedSoundFile)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.RelatedSoundFile) = New ExifRecord(TagFormat(Tags.RelatedSoundFile), value, False)
					Else : Records(Tags.RelatedSoundFile) = Nothing : End If
				End Set
			End Property
#End Region
#Region "DateAndTime"
			''' <summary>Date and time of original data generation</summary>
			''' <returns>Typical number of characters in a string returned by this property is 19.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 18 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			<DisplayName("Date-time original"), Category("DateAndTime"), Description("Date and time of original data generation")> _
			Public Property DateTimeOriginal As String
				Get
					Dim value As ExifRecord = Record(Tags.DateTimeOriginal)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.DateTimeOriginal) = New ExifRecord(TagFormat(Tags.DateTimeOriginal), value, False)
					Else : Records(Tags.DateTimeOriginal) = Nothing : End If
				End Set
			End Property
			''' <summary>Date and time of digital data generation</summary>
			''' <returns>Typical number of characters in a string returned by this property is 19.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 18 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			<DisplayName("Date-time digitized"), Category("DateAndTime"), Description("Date and time of digital data generation")> _
			Public Property DateTimeDigitized As String
				Get
					Dim value As ExifRecord = Record(Tags.DateTimeDigitized)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.DateTimeDigitized) = New ExifRecord(TagFormat(Tags.DateTimeDigitized), value, False)
					Else : Records(Tags.DateTimeDigitized) = Nothing : End If
				End Set
			End Property
			''' <summary>DateTime subseconds</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Sub-sec time"), Category("DateAndTime"), Description("DateTime subseconds")> _
			Public Property SubSecTime As String
				Get
					Dim value As ExifRecord = Record(Tags.SubSecTime)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.SubSecTime) = New ExifRecord(TagFormat(Tags.SubSecTime), value, False)
					Else : Records(Tags.SubSecTime) = Nothing : End If
				End Set
			End Property
			''' <summary>DateTimeOriginal subseconds</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Sub-sec time oroginal"), Category("DateAndTime"), Description("DateTimeOriginal subseconds")> _
			Public Property SubSecTimeOriginal As String
				Get
					Dim value As ExifRecord = Record(Tags.SubSecTimeOriginal)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.SubSecTimeOriginal) = New ExifRecord(TagFormat(Tags.SubSecTimeOriginal), value, False)
					Else : Records(Tags.SubSecTimeOriginal) = Nothing : End If
				End Set
			End Property
			''' <summary>DateTimeDigitized subseconds</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Sub-sec time digitized"), Category("DateAndTime"), Description("DateTimeDigitized subseconds")> _
			Public Property SubSecTimeDigitized As String
				Get
					Dim value As ExifRecord = Record(Tags.SubSecTimeDigitized)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.SubSecTimeDigitized) = New ExifRecord(TagFormat(Tags.SubSecTimeDigitized), value, False)
					Else : Records(Tags.SubSecTimeDigitized) = Nothing : End If
				End Set
			End Property
#End Region
#Region "PictureTakingConditions"
			''' <summary>Exposure time</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Exposure time"), Category("PictureTakingConditions"), Description("Exposure time")> _
			<CLSCompliant(False)> _
			Public Property ExposureTime As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.ExposureTime)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.ExposureTime) = New ExifRecord(TagFormat(Tags.ExposureTime), value.Value, True)
					Else : Records(Tags.ExposureTime) = Nothing : End If
				End Set
			End Property
			''' <summary>F number</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("F number"), Category("PictureTakingConditions"), Description("F number")> _
			<CLSCompliant(False)> _
			Public Property FNumber As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.FNumber)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.FNumber) = New ExifRecord(TagFormat(Tags.FNumber), value.Value, True)
					Else : Records(Tags.FNumber) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="ExposureProgram"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum ExposureProgramValues As UInt16
				''' <summary>Not defined</summary>
				NotDefined = 0
				''' <summary>Manual</summary>
				Manual = 1
				''' <summary>Normal program</summary>
				NormalProgram = 2
				''' <summary>Aperture priority</summary>
				AperturePriority = 3
				''' <summary>Shutter priority</summary>
				ShutterPriority = 4
				''' <summary>Creative program (biased toward depth of field)</summary>
				CreativeProgram = 5
				''' <summary>Action program (biased toward fast shutter speed)</summary>
				ActionProgram = 6
				''' <summary>Portrait mode (for closeup photos with the background out of focus)</summary>
				PortraitMode = 7
				''' <summary>Landscape mode (for landscape photos with the background in focus)</summary>
				LandscapeMode = 8
			End Enum
			''' <summary>Exposure program</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="ExposureProgramValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Exposure program"), Category("PictureTakingConditions"), Description("Exposure program")> _
			<CLSCompliant(False)> _
			Public Property ExposureProgram As Nullable(Of ExposureProgramValues)
				Get
					Dim value As ExifRecord = Record(Tags.ExposureProgram)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(ExposureProgramValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(ExposureProgramValues))
					If value.HasValue Then
						Records(Tags.ExposureProgram) = New ExifRecord(TagFormat(Tags.ExposureProgram), value.Value, True)
					Else : Records(Tags.ExposureProgram) = Nothing : End If
				End Set
			End Property
			''' <summary>Spectral sensitivity</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Spectral sensitivity"), Category("PictureTakingConditions"), Description("Spectral sensitivity")> _
			<CLSCompliant(False)> _
			Public Property SpectralSensitivity As URational()
				Get
					Dim value As ExifRecord = Record(Tags.SpectralSensitivity)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.SpectralSensitivity) = New ExifRecord(TagFormat(Tags.SpectralSensitivity), value, False)
					Else : Records(Tags.SpectralSensitivity) = Nothing : End If
				End Set
			End Property
			''' <summary>ISO speed rating</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("ISO speed ratings"), Category("PictureTakingConditions"), Description("ISO speed rating")> _
			<CLSCompliant(False)> _
			Public Property ISOSpeedRatings As UInt16()
				Get
					Dim value As ExifRecord = Record(Tags.ISOSpeedRatings)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is UInt16() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As UInt16
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), UInt16)
						Next
						Return ret
					Else
						Return New UInt16() {CType(value.Data, UInt16)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.ISOSpeedRatings) = New ExifRecord(TagFormat(Tags.ISOSpeedRatings), value, False)
					Else : Records(Tags.ISOSpeedRatings) = Nothing : End If
				End Set
			End Property
			''' <summary>Optoelectric conversion factor</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("OECF"), Category("PictureTakingConditions"), Description("Optoelectric conversion factor")> _
			Public Property OECF As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.OECF)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.OECF) = New ExifRecord(TagFormat(Tags.OECF), value, False)
					Else : Records(Tags.OECF) = Nothing : End If
				End Set
			End Property
			''' <summary>Shutter speed</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Shutter speed value"), Category("PictureTakingConditions"), Description("Shutter speed")> _
			Public Property ShutterSpeedValue As Nullable(Of SRational)
				Get
					Dim value As ExifRecord = Record(Tags.ShutterSpeedValue)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.ShutterSpeedValue) = New ExifRecord(TagFormat(Tags.ShutterSpeedValue), value.Value, True)
					Else : Records(Tags.ShutterSpeedValue) = Nothing : End If
				End Set
			End Property
			''' <summary>Aperture</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Aperture value"), Category("PictureTakingConditions"), Description("Aperture")> _
			<CLSCompliant(False)> _
			Public Property ApertureValue As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.ApertureValue)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.ApertureValue) = New ExifRecord(TagFormat(Tags.ApertureValue), value.Value, True)
					Else : Records(Tags.ApertureValue) = Nothing : End If
				End Set
			End Property
			''' <summary>Brightness</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Brightness value"), Category("PictureTakingConditions"), Description("Brightness")> _
			Public Property BrightnessValue As Nullable(Of SRational)
				Get
					Dim value As ExifRecord = Record(Tags.BrightnessValue)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.BrightnessValue) = New ExifRecord(TagFormat(Tags.BrightnessValue), value.Value, True)
					Else : Records(Tags.BrightnessValue) = Nothing : End If
				End Set
			End Property
			''' <summary>Exposure bias</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Exposure bias value"), Category("PictureTakingConditions"), Description("Exposure bias")> _
			Public Property ExposureBiasValue As Nullable(Of SRational)
				Get
					Dim value As ExifRecord = Record(Tags.ExposureBiasValue)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.ExposureBiasValue) = New ExifRecord(TagFormat(Tags.ExposureBiasValue), value.Value, True)
					Else : Records(Tags.ExposureBiasValue) = Nothing : End If
				End Set
			End Property
			''' <summary>Maximum lens aperture</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Max. aperture value"), Category("PictureTakingConditions"), Description("Maximum lens aperture")> _
			Public Property MaxApertureValue As Nullable(Of SRational)
				Get
					Dim value As ExifRecord = Record(Tags.MaxApertureValue)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.MaxApertureValue) = New ExifRecord(TagFormat(Tags.MaxApertureValue), value.Value, True)
					Else : Records(Tags.MaxApertureValue) = Nothing : End If
				End Set
			End Property
			''' <summary>Subject distance</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Subject distance"), Category("PictureTakingConditions"), Description("Subject distance")> _
			<CLSCompliant(False)> _
			Public Property SubjectDistance As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.SubjectDistance)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.SubjectDistance) = New ExifRecord(TagFormat(Tags.SubjectDistance), value.Value, True)
					Else : Records(Tags.SubjectDistance) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="MeteringMode"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum MeteringModeValues As UInt16
				''' <summary>unknown</summary>
				Unknown = 0
				''' <summary>Average</summary>
				Average = 1
				''' <summary>Center weighted average</summary>
				CenterWeightedAverage = 2
				''' <summary>Spot</summary>
				Spot = 3
				''' <summary>MultiSpot</summary>
				MultiSpot = 4
				''' <summary>Pattern</summary>
				Pattern = 5
				''' <summary>Partial</summary>
				PartialMode = 6
				''' <summary>Other</summary>
				Other = 255
			End Enum
			''' <summary>Metering mode</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="MeteringModeValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Metering mode"), Category("PictureTakingConditions"), Description("Metering mode")> _
			<CLSCompliant(False)> _
			Public Property MeteringMode As Nullable(Of MeteringModeValues)
				Get
					Dim value As ExifRecord = Record(Tags.MeteringMode)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(MeteringModeValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(MeteringModeValues))
					If value.HasValue Then
						Records(Tags.MeteringMode) = New ExifRecord(TagFormat(Tags.MeteringMode), value.Value, True)
					Else : Records(Tags.MeteringMode) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="LightSource"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum LightSourceValues As UInt16
				''' <summary>unknown</summary>
				unknown = 0
				''' <summary>Daylight</summary>
				Daylight = 1
				''' <summary>Fluorescent</summary>
				Fluorescent = 2
				''' <summary>Tungsten (incandescent light)</summary>
				Tungsten = 3
				''' <summary>Flash</summary>
				Flash = 4
				''' <summary>Fine weather</summary>
				FineWeather = 9
				''' <summary>Cloudy weather</summary>
				CloudyWeather = 10
				''' <summary>Shade</summary>
				Shade = 11
				''' <summary>Daylight fluorescent (D 5700 – 7100K)</summary>
				DaylightFluorescent = 12
				''' <summary>Day white fluorescent (N 4600 – 5400K)</summary>
				DayWhiteFluorescent = 13
				''' <summary>Cool white fluorescent (W 3900 – 4500K)</summary>
				CoolWhiteFluorescent = 14
				''' <summary>White fluorescent (WW 3200 – 3700K)</summary>
				WhiteFluorescent = 15
				''' <summary>Standard light A</summary>
				StandardLightA = 17
				''' <summary>Standard light B</summary>
				StandardLightB = 18
				''' <summary>Standard light C</summary>
				StandardLightC = 19
				''' <summary>D55</summary>
				D55 = 20
				''' <summary>D65</summary>
				D65 = 21
				''' <summary>D75</summary>
				D75 = 22
				''' <summary>D50</summary>
				D50 = 23
				''' <summary>ISO studio tungsten</summary>
				ISOStudioTungsten = 24
				''' <summary>other light source</summary>
				OtherLightSource = 255
			End Enum
			''' <summary>Light source</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="LightSourceValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Light source"), Category("PictureTakingConditions"), Description("Light source")> _
			<CLSCompliant(False)> _
			Public Property LightSource As Nullable(Of LightSourceValues)
				Get
					Dim value As ExifRecord = Record(Tags.LightSource)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(LightSourceValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(LightSourceValues))
					If value.HasValue Then
						Records(Tags.LightSource) = New ExifRecord(TagFormat(Tags.LightSource), value.Value, True)
					Else : Records(Tags.LightSource) = Nothing : End If
				End Set
			End Property
			''' <summary>Flash</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Flash"), Category("PictureTakingConditions"), Description("Flash")> _
			<CLSCompliant(False)> _
			Public Property Flash As Nullable(Of UInt16)
				Get
					Dim value As ExifRecord = Record(Tags.Flash)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.Flash) = New ExifRecord(TagFormat(Tags.Flash), value.Value, True)
					Else : Records(Tags.Flash) = Nothing : End If
				End Set
			End Property
			''' <summary>Lens focal length</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Focla length"), Category("PictureTakingConditions"), Description("Lens focal length")> _
			<CLSCompliant(False)> _
			Public Property FocalLength As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.FocalLength)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.FocalLength) = New ExifRecord(TagFormat(Tags.FocalLength), value.Value, True)
					Else : Records(Tags.FocalLength) = Nothing : End If
				End Set
			End Property
			''' <summary>Subject area</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Subject area"), Category("PictureTakingConditions"), Description("Subject area")> _
			<CLSCompliant(False)> _
			Public Property SubjectArea As UInt16()
				Get
					Dim value As ExifRecord = Record(Tags.SubjectArea)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is UInt16() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As UInt16
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), UInt16)
						Next
						Return ret
					Else
						Return New UInt16() {CType(value.Data, UInt16)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.SubjectArea) = New ExifRecord(TagFormat(Tags.SubjectArea), value, False)
					Else : Records(Tags.SubjectArea) = Nothing : End If
				End Set
			End Property
			''' <summary>Flash energy</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Flash energy"), Category("PictureTakingConditions"), Description("Flash energy")> _
			<CLSCompliant(False)> _
			Public Property FlashEnergy As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.FlashEnergy)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.FlashEnergy) = New ExifRecord(TagFormat(Tags.FlashEnergy), value.Value, True)
					Else : Records(Tags.FlashEnergy) = Nothing : End If
				End Set
			End Property
			''' <summary>Spatial frequency response</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Spatial frequency response"), Category("PictureTakingConditions"), Description("Spatial frequency response")> _
			Public Property SpatialFrequencyResponse As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.SpatialFrequencyResponse)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.SpatialFrequencyResponse) = New ExifRecord(TagFormat(Tags.SpatialFrequencyResponse), value, False)
					Else : Records(Tags.SpatialFrequencyResponse) = Nothing : End If
				End Set
			End Property
			''' <summary>Focal plane X resolution</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Focal plane X-resolution"), Category("PictureTakingConditions"), Description("Focal plane X resolution")> _
			<CLSCompliant(False)> _
			Public Property FocalPlaneXResolution As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.FocalPlaneXResolution)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.FocalPlaneXResolution) = New ExifRecord(TagFormat(Tags.FocalPlaneXResolution), value.Value, True)
					Else : Records(Tags.FocalPlaneXResolution) = Nothing : End If
				End Set
			End Property
			''' <summary>Focal plane Y resolution</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Focal plane Y-resolution"), Category("PictureTakingConditions"), Description("Focal plane Y resolution")> _
			<CLSCompliant(False)> _
			Public Property FocalPlaneYResolution As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.FocalPlaneYResolution)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.FocalPlaneYResolution) = New ExifRecord(TagFormat(Tags.FocalPlaneYResolution), value.Value, True)
					Else : Records(Tags.FocalPlaneYResolution) = Nothing : End If
				End Set
			End Property
			''' <summary>Focal plane resolution unit</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Focal plane resolution unit"), Category("PictureTakingConditions"), Description("Focal plane resolution unit")> _
			<CLSCompliant(False)> _
			Public Property FocalPlaneResolutionUnit As Nullable(Of UInt16)
				Get
					Dim value As ExifRecord = Record(Tags.FocalPlaneResolutionUnit)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.FocalPlaneResolutionUnit) = New ExifRecord(TagFormat(Tags.FocalPlaneResolutionUnit), value.Value, True)
					Else : Records(Tags.FocalPlaneResolutionUnit) = Nothing : End If
				End Set
			End Property
			''' <summary>Subject location</summary>
			''' <returns>Typical number of items in an array returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 2 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("Subject location"), Category("PictureTakingConditions"), Description("Subject location")> _
			<CLSCompliant(False)> _
			Public Property SubjectLocation As UInt16()
				Get
					Dim value As ExifRecord = Record(Tags.SubjectLocation)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is UInt16() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As UInt16
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), UInt16)
						Next
						Return ret
					Else
						Return New UInt16() {CType(value.Data, UInt16)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.SubjectLocation) = New ExifRecord(TagFormat(Tags.SubjectLocation), value, False)
					Else : Records(Tags.SubjectLocation) = Nothing : End If
				End Set
			End Property
			''' <summary>Exposure index</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Exposure index"), Category("PictureTakingConditions"), Description("Exposure index")> _
			<CLSCompliant(False)> _
			Public Property ExposureIndex As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.ExposureIndex)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.ExposureIndex) = New ExifRecord(TagFormat(Tags.ExposureIndex), value.Value, True)
					Else : Records(Tags.ExposureIndex) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="SensingMethod"/> property</summary>
			Public Enum SensingMethodValues As Int16
				''' <summary>Not defined</summary>
				NotDefined = 1
				''' <summary>One-chip color area sensor</summary>
				OneChipColorAreaSensor = 2
				''' <summary>Two-chip color area sensor</summary>
				TwoChipColorAreaSensor = 3
				''' <summary>Three-chip color area sensor</summary>
				ThreeChipColorAreaSensor = 4
				''' <summary>Color sequential area sensor</summary>
				ColorSequentialAreaSensor = 5
				''' <summary>Trilinear sensor</summary>
				TrilinearSensor = 7
				''' <summary>Color sequential linear sensor</summary>
				ColorSequentialLinearSensor = 8
			End Enum
			''' <summary>Sensing method</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="SensingMethodValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Sensing method"), Category("PictureTakingConditions"), Description("Sensing method")> _
			Public Property SensingMethod As Nullable(Of SensingMethodValues)
				Get
					Dim value As ExifRecord = Record(Tags.SensingMethod)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, Int16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(SensingMethodValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(SensingMethodValues))
					If value.HasValue Then
						Records(Tags.SensingMethod) = New ExifRecord(TagFormat(Tags.SensingMethod), value.Value, True)
					Else : Records(Tags.SensingMethod) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="FileSource"/> property</summary>
			Public Enum FileSourceValues As Byte
				''' <summary>DSC</summary>
				DSC = 3
			End Enum
			''' <summary>File source</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="FileSourceValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("File Source"), Category("PictureTakingConditions"), Description("File source")> _
			Public Property FileSource As Nullable(Of FileSourceValues)
				Get
					Dim value As ExifRecord = Record(Tags.FileSource)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(FileSourceValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(FileSourceValues))
					If value.HasValue Then
						Records(Tags.FileSource) = New ExifRecord(TagFormat(Tags.FileSource), value.Value, True)
					Else : Records(Tags.FileSource) = Nothing : End If
				End Set
			End Property
			''' <summary>Scene type</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Scene type"), Category("PictureTakingConditions"), Description("Scene type")> _
			Public Property SceneType As Nullable(Of Byte)
				Get
					Dim value As ExifRecord = Record(Tags.SceneType)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.SceneType) = New ExifRecord(TagFormat(Tags.SceneType), value.Value, True)
					Else : Records(Tags.SceneType) = Nothing : End If
				End Set
			End Property
			''' <summary>CFA pattern</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("CFA pattern"), Category("PictureTakingConditions"), Description("CFA pattern")> _
			Public Property CFAPattern As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.CFAPattern)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.CFAPattern) = New ExifRecord(TagFormat(Tags.CFAPattern), value, False)
					Else : Records(Tags.CFAPattern) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="CustomRendered"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum CustomRenderedValues As UInt16
				''' <summary>Normal process</summary>
				NormalProcess = 0
				''' <summary>Custom process</summary>
				Customrocess = 1
			End Enum
			''' <summary>Custom image processing</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="CustomRenderedValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Custom rendered"), Category("PictureTakingConditions"), Description("Custom image processing")> _
			<CLSCompliant(False)> _
			Public Property CustomRendered As Nullable(Of CustomRenderedValues)
				Get
					Dim value As ExifRecord = Record(Tags.CustomRendered)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(CustomRenderedValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(CustomRenderedValues))
					If value.HasValue Then
						Records(Tags.CustomRendered) = New ExifRecord(TagFormat(Tags.CustomRendered), value.Value, True)
					Else : Records(Tags.CustomRendered) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="ExposureMode"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum ExposureModeValues As UInt16
				''' <summary>Auto exposure</summary>
				AutoExposure = 0
				''' <summary>Manual exposure</summary>
				ManualExposure = 1
				''' <summary>Auto bracket</summary>
				AutoBracket = 2
			End Enum
			''' <summary>Exposure mode</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="ExposureModeValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Exposure mode"), Category("PictureTakingConditions"), Description("Exposure mode")> _
			<CLSCompliant(False)> _
			Public Property ExposureMode As Nullable(Of ExposureModeValues)
				Get
					Dim value As ExifRecord = Record(Tags.ExposureMode)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(ExposureModeValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(ExposureModeValues))
					If value.HasValue Then
						Records(Tags.ExposureMode) = New ExifRecord(TagFormat(Tags.ExposureMode), value.Value, True)
					Else : Records(Tags.ExposureMode) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="WhiteBalance"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum WhiteBalanceValues As UInt16
				''' <summary>Auto white balance</summary>
				Auto = 0
				''' <summary>Manual white balance</summary>
				Manual = 1
			End Enum
			''' <summary>White balance</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="WhiteBalanceValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("White balance"), Category("PictureTakingConditions"), Description("White balance")> _
			<CLSCompliant(False)> _
			Public Property WhiteBalance As Nullable(Of WhiteBalanceValues)
				Get
					Dim value As ExifRecord = Record(Tags.WhiteBalance)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(WhiteBalanceValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(WhiteBalanceValues))
					If value.HasValue Then
						Records(Tags.WhiteBalance) = New ExifRecord(TagFormat(Tags.WhiteBalance), value.Value, True)
					Else : Records(Tags.WhiteBalance) = Nothing : End If
				End Set
			End Property
			''' <summary>Digital zoom ratio</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Digital zoom ratio"), Category("PictureTakingConditions"), Description("Digital zoom ratio")> _
			<CLSCompliant(False)> _
			Public Property DigitalZoomRatio As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.DigitalZoomRatio)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.DigitalZoomRatio) = New ExifRecord(TagFormat(Tags.DigitalZoomRatio), value.Value, True)
					Else : Records(Tags.DigitalZoomRatio) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="FocalLengthIn35mmFilm"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum FocalLengthIn35mmFilmValues As UInt16
				''' <summary>Standard</summary>
				Standard = 0
				''' <summary>Landscape</summary>
				Landscape = 1
				''' <summary>Portrait</summary>
				Portrait = 2
				''' <summary>Night scene</summary>
				NightScene = 3
			End Enum
			''' <summary>Focal length in 35 mm film</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="FocalLengthIn35mmFilmValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Focal length in 35mm film"), Category("PictureTakingConditions"), Description("Focal length in 35 mm film")> _
			<CLSCompliant(False)> _
			Public Property FocalLengthIn35mmFilm As Nullable(Of FocalLengthIn35mmFilmValues)
				Get
					Dim value As ExifRecord = Record(Tags.FocalLengthIn35mmFilm)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(FocalLengthIn35mmFilmValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(FocalLengthIn35mmFilmValues))
					If value.HasValue Then
						Records(Tags.FocalLengthIn35mmFilm) = New ExifRecord(TagFormat(Tags.FocalLengthIn35mmFilm), value.Value, True)
					Else : Records(Tags.FocalLengthIn35mmFilm) = Nothing : End If
				End Set
			End Property
			''' <summary>Scene capture type</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Scene capture type"), Category("PictureTakingConditions"), Description("Scene capture type")> _
			<CLSCompliant(False)> _
			Public Property SceneCaptureType As Nullable(Of UInt16)
				Get
					Dim value As ExifRecord = Record(Tags.SceneCaptureType)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.SceneCaptureType) = New ExifRecord(TagFormat(Tags.SceneCaptureType), value.Value, True)
					Else : Records(Tags.SceneCaptureType) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="GainControl"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum GainControlValues As UInt16
				''' <summary>None</summary>
				None = 0
				''' <summary>Low gain up</summary>
				LowGainUp = 1
				''' <summary>High gain up</summary>
				HighGainUp = 2
				''' <summary>Low gain down</summary>
				LowGainDown = 3
				''' <summary>High gain down</summary>
				HighGainDown = 4
			End Enum
			''' <summary>Gain control</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GainControlValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Gain control"), Category("PictureTakingConditions"), Description("Gain control")> _
			<CLSCompliant(False)> _
			Public Property GainControl As Nullable(Of GainControlValues)
				Get
					Dim value As ExifRecord = Record(Tags.GainControl)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(GainControlValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(GainControlValues))
					If value.HasValue Then
						Records(Tags.GainControl) = New ExifRecord(TagFormat(Tags.GainControl), value.Value, True)
					Else : Records(Tags.GainControl) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="Contrast"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum ContrastValues As UInt16
				''' <summary>Normal</summary>
				Normal = 0
				''' <summary>Soft</summary>
				Soft = 1
				''' <summary>Hard</summary>
				Hard = 2
			End Enum
			''' <summary>Contrast</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="ContrastValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Contrast"), Category("PictureTakingConditions"), Description("Contrast")> _
			<CLSCompliant(False)> _
			Public Property Contrast As Nullable(Of ContrastValues)
				Get
					Dim value As ExifRecord = Record(Tags.Contrast)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(ContrastValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(ContrastValues))
					If value.HasValue Then
						Records(Tags.Contrast) = New ExifRecord(TagFormat(Tags.Contrast), value.Value, True)
					Else : Records(Tags.Contrast) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="Saturation"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum SaturationValues As UInt16
				''' <summary>Normal</summary>
				Normal = 0
				''' <summary>Low saturation</summary>
				Low = 1
				''' <summary>High saturation</summary>
				High = 2
			End Enum
			''' <summary>Saturation</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="SaturationValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Saturation"), Category("PictureTakingConditions"), Description("Saturation")> _
			<CLSCompliant(False)> _
			Public Property Saturation As Nullable(Of SaturationValues)
				Get
					Dim value As ExifRecord = Record(Tags.Saturation)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(SaturationValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(SaturationValues))
					If value.HasValue Then
						Records(Tags.Saturation) = New ExifRecord(TagFormat(Tags.Saturation), value.Value, True)
					Else : Records(Tags.Saturation) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="Sharpness"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum SharpnessValues As UInt16
				''' <summary>Normal</summary>
				Normal = 0
				''' <summary>Soft</summary>
				Soft = 1
				''' <summary>Hard</summary>
				Hard = 2
			End Enum
			''' <summary>Sharpness</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="SharpnessValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Sharpness"), Category("PictureTakingConditions"), Description("Sharpness")> _
			<CLSCompliant(False)> _
			Public Property Sharpness As Nullable(Of SharpnessValues)
				Get
					Dim value As ExifRecord = Record(Tags.Sharpness)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(SharpnessValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(SharpnessValues))
					If value.HasValue Then
						Records(Tags.Sharpness) = New ExifRecord(TagFormat(Tags.Sharpness), value.Value, True)
					Else : Records(Tags.Sharpness) = Nothing : End If
				End Set
			End Property
			''' <summary>Device settings description</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Device setting description"), Category("PictureTakingConditions"), Description("Device settings description")> _
			Public Property DeviceSettingDescription As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.DeviceSettingDescription)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.DeviceSettingDescription) = New ExifRecord(TagFormat(Tags.DeviceSettingDescription), value, False)
					Else : Records(Tags.DeviceSettingDescription) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="SubjectDistanceRange"/> property</summary>
			<CLSCompliant(False)> _
			Public Enum SubjectDistanceRangeValues As UInt16
				''' <summary>unknown</summary>
				unknown = 0
				''' <summary>Macro</summary>
				Macro = 1
				''' <summary>Close view</summary>
				CloseView = 2
				''' <summary>Distant view</summary>
				DistantView = 3
			End Enum
			''' <summary>Subject distance range</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="SubjectDistanceRangeValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("Subject distance range"), Category("PictureTakingConditions"), Description("Subject distance range")> _
			<CLSCompliant(False)> _
			Public Property SubjectDistanceRange As Nullable(Of SubjectDistanceRangeValues)
				Get
					Dim value As ExifRecord = Record(Tags.SubjectDistanceRange)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(SubjectDistanceRangeValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(SubjectDistanceRangeValues))
					If value.HasValue Then
						Records(Tags.SubjectDistanceRange) = New ExifRecord(TagFormat(Tags.SubjectDistanceRange), value.Value, True)
					Else : Records(Tags.SubjectDistanceRange) = Nothing : End If
				End Set
			End Property
#End Region
#Region "OtherExif"
			''' <summary>Unique image ID</summary>
			''' <returns>Typical number of characters in a string returned by this property is 33.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 32 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			<DisplayName("Image unique ID"), Category("OtherExif"), Description("Unique image ID")> _
			Public Property ImageUniqueID As String
				Get
					Dim value As ExifRecord = Record(Tags.ImageUniqueID)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.ImageUniqueID) = New ExifRecord(TagFormat(Tags.ImageUniqueID), value, False)
					Else : Records(Tags.ImageUniqueID) = Nothing : End If
				End Set
			End Property
#End Region
#End Region
			''' <summary>Gets format for tag specified</summary>
			''' <exception cref="InvalidEnumArgumentException"><paramref name="Tag"/> contains unknown value</exception>
			<CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
				Get
					Const any As ushort=0
					Select Case Tag
						Case Tags.InteroperabilityIFD : Return New ExifTagFormat(1, &hA005, "InteroperabilityIFD", ExifDataTypes.UInt32)
						Case Tags.ExifVersion : Return New ExifTagFormat(4, &h9000, "ExifVersion", ExifDataTypes.NA)
						Case Tags.FlashpixVersion : Return New ExifTagFormat(4, &hA000, "FlashpixVersion", ExifDataTypes.NA)
						Case Tags.ColorSpace : Return New ExifTagFormat(1, &hA001, "ColorSpace", ExifDataTypes.UInt16)
						Case Tags.ComponentsConfiguration : Return New ExifTagFormat(4, &h9101, "ComponentsConfiguration", ExifDataTypes.NA)
						Case Tags.CompressedBitsPerPixel : Return New ExifTagFormat(1, &h9102, "CompressedBitsPerPixel", ExifDataTypes.URational)
						Case Tags.PixelXDimension : Return New ExifTagFormat(1, &hA002, "PixelXDimension", ExifDataTypes.UInt32, ExifDataTypes.UInt16)
						Case Tags.PixelYDimension : Return New ExifTagFormat(1, &hA003, "PixelYDimension", ExifDataTypes.UInt32, ExifDataTypes.UInt16)
						Case Tags.MakerNote : Return New ExifTagFormat(any, &h927C, "MakerNote", ExifDataTypes.NA)
						Case Tags.UserComment : Return New ExifTagFormat(any, &h9286, "UserComment", ExifDataTypes.NA)
						Case Tags.RelatedSoundFile : Return New ExifTagFormat(13, &hA004, "RelatedSoundFile", ExifDataTypes.ASCII)
						Case Tags.DateTimeOriginal : Return New ExifTagFormat(19, &h9003, "DateTimeOriginal", ExifDataTypes.ASCII)
						Case Tags.DateTimeDigitized : Return New ExifTagFormat(19, &h9004, "DateTimeDigitized", ExifDataTypes.ASCII)
						Case Tags.SubSecTime : Return New ExifTagFormat(any, &h9290, "SubSecTime", ExifDataTypes.ASCII)
						Case Tags.SubSecTimeOriginal : Return New ExifTagFormat(any, &h9291, "SubSecTimeOriginal", ExifDataTypes.ASCII)
						Case Tags.SubSecTimeDigitized : Return New ExifTagFormat(any, &h9292, "SubSecTimeDigitized", ExifDataTypes.ASCII)
						Case Tags.ExposureTime : Return New ExifTagFormat(1, &h829A, "ExposureTime", ExifDataTypes.URational)
						Case Tags.FNumber : Return New ExifTagFormat(1, &h829D, "FNumber", ExifDataTypes.URational)
						Case Tags.ExposureProgram : Return New ExifTagFormat(1, &h8822, "ExposureProgram", ExifDataTypes.UInt16)
						Case Tags.SpectralSensitivity : Return New ExifTagFormat(any, &h8824, "SpectralSensitivity", ExifDataTypes.URational)
						Case Tags.ISOSpeedRatings : Return New ExifTagFormat(any, &h8827, "ISOSpeedRatings", ExifDataTypes.UInt16)
						Case Tags.OECF : Return New ExifTagFormat(any, &h8828, "OECF", ExifDataTypes.NA)
						Case Tags.ShutterSpeedValue : Return New ExifTagFormat(1, &h9201, "ShutterSpeedValue", ExifDataTypes.SRational)
						Case Tags.ApertureValue : Return New ExifTagFormat(1, &h9202, "ApertureValue", ExifDataTypes.URational)
						Case Tags.BrightnessValue : Return New ExifTagFormat(1, &h9203, "BrightnessValue", ExifDataTypes.SRational)
						Case Tags.ExposureBiasValue : Return New ExifTagFormat(1, &h9204, "ExposureBiasValue", ExifDataTypes.SRational)
						Case Tags.MaxApertureValue : Return New ExifTagFormat(1, &h9205, "MaxApertureValue", ExifDataTypes.SRational)
						Case Tags.SubjectDistance : Return New ExifTagFormat(1, &h9206, "SubjectDistance", ExifDataTypes.URational)
						Case Tags.MeteringMode : Return New ExifTagFormat(1, &h9207, "MeteringMode", ExifDataTypes.UInt16)
						Case Tags.LightSource : Return New ExifTagFormat(1, &h9208, "LightSource", ExifDataTypes.UInt16)
						Case Tags.Flash : Return New ExifTagFormat(1, &h9209, "Flash", ExifDataTypes.UInt16)
						Case Tags.FocalLength : Return New ExifTagFormat(1, &h920A, "FocalLength", ExifDataTypes.URational)
						Case Tags.SubjectArea : Return New ExifTagFormat(any, &h9214, "SubjectArea", ExifDataTypes.UInt16)
						Case Tags.FlashEnergy : Return New ExifTagFormat(1, &hA20B, "FlashEnergy", ExifDataTypes.URational)
						Case Tags.SpatialFrequencyResponse : Return New ExifTagFormat(any, &hA20C, "SpatialFrequencyResponse", ExifDataTypes.NA)
						Case Tags.FocalPlaneXResolution : Return New ExifTagFormat(1, &hA20E, "FocalPlaneXResolution", ExifDataTypes.URational)
						Case Tags.FocalPlaneYResolution : Return New ExifTagFormat(1, &hA20F, "FocalPlaneYResolution", ExifDataTypes.URational)
						Case Tags.FocalPlaneResolutionUnit : Return New ExifTagFormat(1, &hA210, "FocalPlaneResolutionUnit", ExifDataTypes.UInt16)
						Case Tags.SubjectLocation : Return New ExifTagFormat(2, &hA214, "SubjectLocation", ExifDataTypes.UInt16)
						Case Tags.ExposureIndex : Return New ExifTagFormat(1, &hA215, "ExposureIndex", ExifDataTypes.URational)
						Case Tags.SensingMethod : Return New ExifTagFormat(1, &hA217, "SensingMethod", ExifDataTypes.Int16)
						Case Tags.FileSource : Return New ExifTagFormat(1, &hA300, "FileSource", ExifDataTypes.NA)
						Case Tags.SceneType : Return New ExifTagFormat(1, &hA301, "SceneType", ExifDataTypes.NA)
						Case Tags.CFAPattern : Return New ExifTagFormat(any, &hA302, "CFAPattern", ExifDataTypes.NA)
						Case Tags.CustomRendered : Return New ExifTagFormat(1, &hA401, "CustomRendered", ExifDataTypes.UInt16)
						Case Tags.ExposureMode : Return New ExifTagFormat(1, &hA402, "ExposureMode", ExifDataTypes.UInt16)
						Case Tags.WhiteBalance : Return New ExifTagFormat(1, &hA403, "WhiteBalance", ExifDataTypes.UInt16)
						Case Tags.DigitalZoomRatio : Return New ExifTagFormat(1, &hA404, "DigitalZoomRatio", ExifDataTypes.URational)
						Case Tags.FocalLengthIn35mmFilm : Return New ExifTagFormat(1, &hA405, "FocalLengthIn35mmFilm", ExifDataTypes.UInt16)
						Case Tags.SceneCaptureType : Return New ExifTagFormat(1, &hA406, "SceneCaptureType", ExifDataTypes.UInt16)
						Case Tags.GainControl : Return New ExifTagFormat(1, &hA407, "GainControl", ExifDataTypes.UInt16)
						Case Tags.Contrast : Return New ExifTagFormat(1, &hA408, "Contrast", ExifDataTypes.UInt16)
						Case Tags.Saturation : Return New ExifTagFormat(1, &hA409, "Saturation", ExifDataTypes.UInt16)
						Case Tags.Sharpness : Return New ExifTagFormat(1, &hA40A, "Sharpness", ExifDataTypes.UInt16)
						Case Tags.DeviceSettingDescription : Return New ExifTagFormat(any, &hA40B, "DeviceSettingDescription", ExifDataTypes.NA)
						Case Tags.SubjectDistanceRange : Return New ExifTagFormat(1, &hA40C, "SubjectDistanceRange", ExifDataTypes.UInt16)
						Case Tags.ImageUniqueID : Return New ExifTagFormat(33, &hA420, "ImageUniqueID", ExifDataTypes.ASCII)
						Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
					End Select
				End Get
			End Property
		End Class
		Partial Public Class IfdGps
			''' <summary>Tag numbers used in GPS Sub IFD</summary>
			''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added for enum items.</version>
			<CLSCompliant(False)> Public Enum Tags As UShort
#Region "Tags Relating to GPS"
				''' <summary>GPS tag version</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS version ID"), Category("GPS")>GPSVersionID = &h0
				''' <summary>North or South Latitude</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS Latitude ref"), Category("GPS")>GPSLatitudeRef = &h1
				''' <summary>Latitude</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS latitude"), Category("GPS")>GPSLatitude = &h2
				''' <summary>East or West Longitude</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS longitude ref"), Category("GPS")>GPSLongitudeRef = &h3
				''' <summary>Longitude</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS longitude"), Category("GPS")>GPSLongitude = &h4
				''' <summary>Altitude reference</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS altitude ref"), Category("GPS")>GPSAltitudeRef = &h5
				''' <summary>Altitude</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS altitude"), Category("GPS")>GPSAltitude = &h6
				''' <summary>GPS time (atomic clock)</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS time-stamp"), Category("GPS")>GPSTimeStamp = &h7
				''' <summary>GPS satellites used for measurement</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS satellites"), Category("GPS")>GPSSatellites = &h8
				''' <summary>GPS receiver status</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS status"), Category("GPS")>GPSStatus = &h9
				''' <summary>GPS measurement mode</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS measure mode"), Category("GPS")>GPSMeasureMode = &hA
				''' <summary>Measurement precision</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS DOP"), Category("GPS")>GPSDOP = &hB
				''' <summary>Speed unit</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS speed ref"), Category("GPS")>GPSSpeedRef = &hC
				''' <summary>Speed of GPS receiver</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS speed"), Category("GPS")>GPSSpeed = &hD
				''' <summary>Reference for direction of movement</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS track ref"), Category("GPS")>GPSTrackRef = &hE
				''' <summary>Direction of movement</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS track"), Category("GPS")>GPSTrack = &hF
				''' <summary>Reference for direction of image</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS img. derection ref"), Category("GPS")>GPSImgDirectionRef = &h10
				''' <summary>Direction of image</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS img. direction"), Category("GPS")>GPSImgDirection = &h11
				''' <summary>Geodetic survey data used</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS map datum"), Category("GPS")>GPSMapDatum = &h12
				''' <summary>Reference for latitude of destination</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS dest. latitude ref"), Category("GPS")>GPSDestLatitudeRef = &h13
				''' <summary>Latitude of destination</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS dest. latitude"), Category("GPS")>GPSDestLatitude = &h14
				''' <summary>Reference for longitude of destination</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS dest. longitude ref"), Category("GPS")>GPSDestLongitudeRef = &h15
				''' <summary>Longitude of destination</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS dest. longitude"), Category("GPS")>GPSDestLongitude = &h16
				''' <summary>Reference for bearing of destination</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS dest. bearing ref"), Category("GPS")>GPSDestBearingRef = &h17
				''' <summary>Bearing of destination</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS dest. bearing"), Category("GPS")>GPSDestBearing = &h18
				''' <summary>Reference for distance to destination</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS dest. distance ref"), Category("GPS")>GPSDestDistanceRef = &h19
				''' <summary>Distance to destination</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS dest. distance"), Category("GPS")>GPSDestDistance = &h1A
				''' <summary>Name of GPS processing method</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS processing method"), Category("GPS")>GPSProcessingMethod = &h1B
				''' <summary>Name of GPS area</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS area information"), Category("GPS")>GPSAreaInformation = &h1C
				''' <summary>GPS date</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS date stamp"), Category("GPS")>GPSDateStamp = &h1D
				''' <summary>GPS differential correction</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("GPS differential"), Category("GPS")>GPSDifferential = &h1E
#End Region
			End Enum
#Region "Properties"
#Region "GPS"
			''' <summary>GPS tag version</summary>
			''' <returns>Typical number of items in an array returned by this property is 4.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 4 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("GPS version ID"), Category("GPS"), Description("GPS tag version")> _
			Public Property GPSVersionID As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.GPSVersionID)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSVersionID) = New ExifRecord(TagFormat(Tags.GPSVersionID), value, False)
					Else : Records(Tags.GPSVersionID) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="GPSLatitudeRef"/> property</summary>
			Public Enum GPSLatitudeRefValues As Integer
				''' <summary>Nort latitude</summary>
				North = AscW("N"c)
				''' <summary>South latitude</summary>
				South = AscW("S"c)
			End Enum
			''' <summary>North or South Latitude</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSLatitudeRefValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS Latitude ref"), Category("GPS"), Description("North or South Latitude")> _
			Public Property GPSLatitudeRef As GPSLatitudeRefValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSLatitudeRef)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSLatitudeRefValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSLatitudeRefValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSLatitudeRefValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSLatitudeRefValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSLatitudeRefValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSLatitudeRef) = New ExifRecord(TagFormat(Tags.GPSLatitudeRef), Str, False)
					Else : Records(Tags.GPSLatitudeRef) = Nothing : End If
				End Set
			End Property
			''' <summary>Latitude</summary>
			''' <returns>Typical number of items in an array returned by this property is 3.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 3 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("GPS latitude"), Category("GPS"), Description("Latitude")> _
			<CLSCompliant(False)> _
			Public Property GPSLatitude As URational()
				Get
					Dim value As ExifRecord = Record(Tags.GPSLatitude)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSLatitude) = New ExifRecord(TagFormat(Tags.GPSLatitude), value, False)
					Else : Records(Tags.GPSLatitude) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="GPSLongitudeRef"/> property</summary>
			Public Enum GPSLongitudeRefValues As Integer
				''' <summary>East longitude</summary>
				East = AscW("E"c)
				''' <summary>West longitude</summary>
				West = AscW("W"c)
			End Enum
			''' <summary>East or West Longitude</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSLongitudeRefValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS longitude ref"), Category("GPS"), Description("East or West Longitude")> _
			Public Property GPSLongitudeRef As GPSLongitudeRefValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSLongitudeRef)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSLongitudeRefValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSLongitudeRefValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSLongitudeRefValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSLongitudeRefValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSLongitudeRefValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSLongitudeRef) = New ExifRecord(TagFormat(Tags.GPSLongitudeRef), Str, False)
					Else : Records(Tags.GPSLongitudeRef) = Nothing : End If
				End Set
			End Property
			''' <summary>Longitude</summary>
			''' <returns>Typical number of items in an array returned by this property is 3.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 3 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("GPS longitude"), Category("GPS"), Description("Longitude")> _
			<CLSCompliant(False)> _
			Public Property GPSLongitude As URational()
				Get
					Dim value As ExifRecord = Record(Tags.GPSLongitude)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSLongitude) = New ExifRecord(TagFormat(Tags.GPSLongitude), value, False)
					Else : Records(Tags.GPSLongitude) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="GPSAltitudeRef"/> property</summary>
			Public Enum GPSAltitudeRefValues As Byte
				''' <summary>Sea level</summary>
				SeaLevel = 0
				''' <summary>Sea level reference (negative value)</summary>
				SeaLevelReference = 1
			End Enum
			''' <summary>Altitude reference</summary>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSAltitudeRefValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS altitude ref"), Category("GPS"), Description("Altitude reference")> _
			Public Property GPSAltitudeRef As Nullable(Of GPSAltitudeRefValues)
				Get
					Dim value As ExifRecord = Record(Tags.GPSAltitudeRef)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, Byte)
					End If
				End Get
				Set
					If Value.HasValue AndAlso Array.IndexOf([Enum].GetValues(GetType(GPSAltitudeRefValues)), Value) = -1 Then Throw New InvalidEnumArgumentException("value", Value, GetType(GPSAltitudeRefValues))
					If value.HasValue Then
						Records(Tags.GPSAltitudeRef) = New ExifRecord(TagFormat(Tags.GPSAltitudeRef), value.Value, True)
					Else : Records(Tags.GPSAltitudeRef) = Nothing : End If
				End Set
			End Property
			''' <summary>Altitude</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS altitude"), Category("GPS"), Description("Altitude")> _
			<CLSCompliant(False)> _
			Public Property GPSAltitude As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.GPSAltitude)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.GPSAltitude) = New ExifRecord(TagFormat(Tags.GPSAltitude), value.Value, True)
					Else : Records(Tags.GPSAltitude) = Nothing : End If
				End Set
			End Property
			''' <summary>GPS time (atomic clock)</summary>
			''' <returns>Typical number of items in an array returned by this property is 3.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 3 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("GPS time-stamp"), Category("GPS"), Description("GPS time (atomic clock)")> _
			<CLSCompliant(False)> _
			Public Property GPSTimeStamp As URational()
				Get
					Dim value As ExifRecord = Record(Tags.GPSTimeStamp)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSTimeStamp) = New ExifRecord(TagFormat(Tags.GPSTimeStamp), value, False)
					Else : Records(Tags.GPSTimeStamp) = Nothing : End If
				End Set
			End Property
			''' <summary>GPS satellites used for measurement</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("GPS satellites"), Category("GPS"), Description("GPS satellites used for measurement")> _
			Public Property GPSSatellites As String
				Get
					Dim value As ExifRecord = Record(Tags.GPSSatellites)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSSatellites) = New ExifRecord(TagFormat(Tags.GPSSatellites), value, False)
					Else : Records(Tags.GPSSatellites) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="GPSStatus"/> property</summary>
			Public Enum GPSStatusValues As Integer
				''' <summary>Measurement in progress</summary>
				InProgress = AscW("A"c)
				''' <summary>Measurement Interoperability</summary>
				Interoperability = AscW("V"c)
			End Enum
			''' <summary>GPS receiver status</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSStatusValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS status"), Category("GPS"), Description("GPS receiver status")> _
			Public Property GPSStatus As GPSStatusValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSStatus)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSStatusValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSStatusValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSStatusValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSStatusValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSStatusValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSStatus) = New ExifRecord(TagFormat(Tags.GPSStatus), Str, False)
					Else : Records(Tags.GPSStatus) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="GPSMeasureMode"/> property</summary>
			Public Enum GPSMeasureModeValues As Integer
				''' <summary>2-dimensional measurement</summary>
				Measurement2D = AscW("2"c)
				''' <summary>3-dimensional measurement</summary>
				Measurement3D = AscW("3"c)
			End Enum
			''' <summary>GPS measurement mode</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSMeasureModeValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS measure mode"), Category("GPS"), Description("GPS measurement mode")> _
			Public Property GPSMeasureMode As GPSMeasureModeValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSMeasureMode)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSMeasureModeValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSMeasureModeValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSMeasureModeValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSMeasureModeValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSMeasureModeValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSMeasureMode) = New ExifRecord(TagFormat(Tags.GPSMeasureMode), Str, False)
					Else : Records(Tags.GPSMeasureMode) = Nothing : End If
				End Set
			End Property
			''' <summary>Measurement precision</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS DOP"), Category("GPS"), Description("Measurement precision")> _
			<CLSCompliant(False)> _
			Public Property GPSDOP As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.GPSDOP)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.GPSDOP) = New ExifRecord(TagFormat(Tags.GPSDOP), value.Value, True)
					Else : Records(Tags.GPSDOP) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="GPSSpeedRef"/> property</summary>
			Public Enum GPSSpeedRefValues As Integer
				''' <summary>Kilometers per hour</summary>
				KilometersPeHour = AscW("K"c)
				''' <summary>Miles per hour</summary>
				MilesPerHour = AscW("M"c)
				''' <summary>Knots</summary>
				Knots = AscW("N"c)
			End Enum
			''' <summary>Speed unit</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSSpeedRefValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS speed ref"), Category("GPS"), Description("Speed unit")> _
			Public Property GPSSpeedRef As GPSSpeedRefValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSSpeedRef)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSSpeedRefValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSSpeedRefValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSSpeedRefValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSSpeedRefValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSSpeedRefValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSSpeedRef) = New ExifRecord(TagFormat(Tags.GPSSpeedRef), Str, False)
					Else : Records(Tags.GPSSpeedRef) = Nothing : End If
				End Set
			End Property
			''' <summary>Speed of GPS receiver</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS speed"), Category("GPS"), Description("Speed of GPS receiver")> _
			<CLSCompliant(False)> _
			Public Property GPSSpeed As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.GPSSpeed)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.GPSSpeed) = New ExifRecord(TagFormat(Tags.GPSSpeed), value.Value, True)
					Else : Records(Tags.GPSSpeed) = Nothing : End If
				End Set
			End Property
			''' <summary>Possible values of the <see cref="GPSTrackRef"/> property</summary>
			Public Enum GPSTrackRefValues As Integer
				''' <summary>True direction</summary>
				TrueDirection = AscW("T"c)
				''' <summary>Magnetic direction</summary>
				MagneticDirection = AscW("M"c)
			End Enum
			''' <summary>Reference for direction of movement</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSTrackRefValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS track ref"), Category("GPS"), Description("Reference for direction of movement")> _
			Public Property GPSTrackRef As GPSTrackRefValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSTrackRef)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSTrackRefValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSTrackRefValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSTrackRefValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSTrackRefValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSTrackRefValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSTrackRef) = New ExifRecord(TagFormat(Tags.GPSTrackRef), Str, False)
					Else : Records(Tags.GPSTrackRef) = Nothing : End If
				End Set
			End Property
			''' <summary>Direction of movement</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS track"), Category("GPS"), Description("Direction of movement")> _
			<CLSCompliant(False)> _
			Public Property GPSTrack As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.GPSTrack)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.GPSTrack) = New ExifRecord(TagFormat(Tags.GPSTrack), value.Value, True)
					Else : Records(Tags.GPSTrack) = Nothing : End If
				End Set
			End Property
			''' <summary>Reference for direction of image</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSTrackRefValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS img. derection ref"), Category("GPS"), Description("Reference for direction of image")> _
			Public Property GPSImgDirectionRef As GPSTrackRefValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSImgDirectionRef)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSTrackRefValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSTrackRefValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSTrackRefValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSTrackRefValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSTrackRefValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSImgDirectionRef) = New ExifRecord(TagFormat(Tags.GPSImgDirectionRef), Str, False)
					Else : Records(Tags.GPSImgDirectionRef) = Nothing : End If
				End Set
			End Property
			''' <summary>Direction of image</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS img. direction"), Category("GPS"), Description("Direction of image")> _
			<CLSCompliant(False)> _
			Public Property GPSImgDirection As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.GPSImgDirection)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.GPSImgDirection) = New ExifRecord(TagFormat(Tags.GPSImgDirection), value.Value, True)
					Else : Records(Tags.GPSImgDirection) = Nothing : End If
				End Set
			End Property
			''' <summary>Geodetic survey data used</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("GPS map datum"), Category("GPS"), Description("Geodetic survey data used")> _
			Public Property GPSMapDatum As String
				Get
					Dim value As ExifRecord = Record(Tags.GPSMapDatum)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSMapDatum) = New ExifRecord(TagFormat(Tags.GPSMapDatum), value, False)
					Else : Records(Tags.GPSMapDatum) = Nothing : End If
				End Set
			End Property
			''' <summary>Reference for latitude of destination</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSLatitudeRefValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS dest. latitude ref"), Category("GPS"), Description("Reference for latitude of destination")> _
			Public Property GPSDestLatitudeRef As GPSLatitudeRefValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSDestLatitudeRef)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSLatitudeRefValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSLatitudeRefValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSLatitudeRefValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSLatitudeRefValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSLatitudeRefValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSDestLatitudeRef) = New ExifRecord(TagFormat(Tags.GPSDestLatitudeRef), Str, False)
					Else : Records(Tags.GPSDestLatitudeRef) = Nothing : End If
				End Set
			End Property
			''' <summary>Latitude of destination</summary>
			''' <returns>Typical number of items in an array returned by this property is 3.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 3 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("GPS dest. latitude"), Category("GPS"), Description("Latitude of destination")> _
			<CLSCompliant(False)> _
			Public Property GPSDestLatitude As URational()
				Get
					Dim value As ExifRecord = Record(Tags.GPSDestLatitude)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSDestLatitude) = New ExifRecord(TagFormat(Tags.GPSDestLatitude), value, False)
					Else : Records(Tags.GPSDestLatitude) = Nothing : End If
				End Set
			End Property
			''' <summary>Reference for longitude of destination</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSLongitudeRefValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS dest. longitude ref"), Category("GPS"), Description("Reference for longitude of destination")> _
			Public Property GPSDestLongitudeRef As GPSLongitudeRefValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSDestLongitudeRef)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSLongitudeRefValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSLongitudeRefValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSLongitudeRefValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSLongitudeRefValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSLongitudeRefValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSDestLongitudeRef) = New ExifRecord(TagFormat(Tags.GPSDestLongitudeRef), Str, False)
					Else : Records(Tags.GPSDestLongitudeRef) = Nothing : End If
				End Set
			End Property
			''' <summary>Longitude of destination</summary>
			''' <returns>Typical number of items in an array returned by this property is 3.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is array of exactly 3 items</exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			<DisplayName("GPS dest. longitude"), Category("GPS"), Description("Longitude of destination")> _
			<CLSCompliant(False)> _
			Public Property GPSDestLongitude As URational()
				Get
					Dim value As ExifRecord = Record(Tags.GPSDestLongitude)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is URational() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As URational
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), URational)
						Next
						Return ret
					Else
						Return New URational() {CType(value.Data, URational)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSDestLongitude) = New ExifRecord(TagFormat(Tags.GPSDestLongitude), value, False)
					Else : Records(Tags.GPSDestLongitude) = Nothing : End If
				End Set
			End Property
			''' <summary>Reference for bearing of destination</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <exception cref="InvalidEnumArgumentException">Value of item of <paramref name="value"/> is not member of <see cref="GPSTrackRefValues"/></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS dest. bearing ref"), Category("GPS"), Description("Reference for bearing of destination")> _
			Public Property GPSDestBearingRef As GPSTrackRefValues()
				Get
					Dim value As ExifRecord = Record(Tags.GPSDestBearingRef)
					If value Is Nothing Then
						Return Nothing
					ElseIf value.DataType.NumberOfElements = 1 Then
						Return New GPSTrackRefValues() {AscW(CStr(value.Data))}
					Else
						Dim ret(DirectCast(value.Data, String).Length - 1) As GPSTrackRefValues
						For i As Integer = 0 To DirectCast(value.Data, String).Length - 1
							ret(i) = AscW(DirectCast(value.Data, String)(i))
						Next i
						Return ret
					End If
				End Get
				Set
					If value IsNot Nothing Then
						For Each itm As GPSTrackRefValues In value
							If Array.IndexOf([Enum].GetValues(GetType(GPSTrackRefValues)), itm) = -1 Then Throw New InvalidEnumArgumentException("value", itm, GetType(GPSTrackRefValues))
						Next itm
					End If
					If value IsNot Nothing Then
						Dim str As String = ""
						For Each itm As GPSLatitudeRefValues In value
							str &= ChrW(itm)
						Next itm
						Records(Tags.GPSDestBearingRef) = New ExifRecord(TagFormat(Tags.GPSDestBearingRef), Str, False)
					Else : Records(Tags.GPSDestBearingRef) = Nothing : End If
				End Set
			End Property
			''' <summary>Bearing of destination</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS dest. bearing"), Category("GPS"), Description("Bearing of destination")> _
			<CLSCompliant(False)> _
			Public Property GPSDestBearing As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.GPSDestBearing)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.GPSDestBearing) = New ExifRecord(TagFormat(Tags.GPSDestBearing), value.Value, True)
					Else : Records(Tags.GPSDestBearing) = Nothing : End If
				End Set
			End Property
			''' <summary>Reference for distance to destination</summary>
			''' <returns>Typical number of characters in a string returned by this property is 2.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 1 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
        <DisplayName("GPS dest. distance ref"), Category("GPS"), Description("Reference for distance to destination")> _
        Public Property GPSDestDistanceRef As Char?
            Get
                Dim value As ExifRecord = Record(Tags.GPSDestDistanceRef)
                If value Is Nothing Then Return Nothing Else Return value.Data
            End Get
            Set(value As Char?)
                If Value IsNot Nothing Then
                    Records(Tags.GPSDestDistanceRef) = New ExifRecord(TagFormat(Tags.GPSDestDistanceRef), Value, False)
                Else : Records(Tags.GPSDestDistanceRef) = Nothing : End If
            End Set
        End Property
			''' <summary>Distance to destination</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS dest. distance"), Category("GPS"), Description("Distance to destination")> _
			<CLSCompliant(False)> _
			Public Property GPSDestDistance As Nullable(Of URational)
				Get
					Dim value As ExifRecord = Record(Tags.GPSDestDistance)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value.HasValue Then
						Records(Tags.GPSDestDistance) = New ExifRecord(TagFormat(Tags.GPSDestDistance), value.Value, True)
					Else : Records(Tags.GPSDestDistance) = Nothing : End If
				End Set
			End Property
			''' <summary>Name of GPS processing method</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS processing method"), Category("GPS"), Description("Name of GPS processing method")> _
			Public Property GPSProcessingMethod As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.GPSProcessingMethod)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSProcessingMethod) = New ExifRecord(TagFormat(Tags.GPSProcessingMethod), value, False)
					Else : Records(Tags.GPSProcessingMethod) = Nothing : End If
				End Set
			End Property
			''' <summary>Name of GPS area</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS area information"), Category("GPS"), Description("Name of GPS area")> _
			Public Property GPSAreaInformation As Byte()
				Get
					Dim value As ExifRecord = Record(Tags.GPSAreaInformation)
					If value Is Nothing Then
						Return Nothing
					ElseIf TypeOf value.Data Is Byte() Then
						Return value.Data
					ElseIf IsArray(value.Data) Then
						Dim ret(DirectCast(value.Data, Array).Length) As Byte
						For i As Integer = 0 To ret.length - 1
							ret(i) = CType(DirectCast(value.Data, Array).GetValue(i), Byte)
						Next
						Return ret
					Else
						Return New Byte() {CType(value.Data, Byte)}
					End If
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSAreaInformation) = New ExifRecord(TagFormat(Tags.GPSAreaInformation), value, False)
					Else : Records(Tags.GPSAreaInformation) = Nothing : End If
				End Set
			End Property
			''' <summary>GPS date</summary>
			''' <returns>Typical number of characters in a string returned by this property is 11.</returns>
			''' <exception cref='ArgumentException'>Value being set is neither null nor it is string of exactly 10 characters.<note><Terminating nulchar is counted to total lenght of string wheather it is specififed in value being set or not. So, if you don't have a nullchar at the end of your string it must be one character shorther!/note></exception>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4"><see cref="ArgumentException"/> documentation added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			<DisplayName("GPS date stamp"), Category("GPS"), Description("GPS date")> _
			Public Property GPSDateStamp As String
				Get
					Dim value As ExifRecord = Record(Tags.GPSDateStamp)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.GPSDateStamp) = New ExifRecord(TagFormat(Tags.GPSDateStamp), value, False)
					Else : Records(Tags.GPSDateStamp) = Nothing : End If
				End Set
			End Property
			''' <summary>GPS differential correction</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			<DisplayName("GPS differential"), Category("GPS"), Description("GPS differential correction")> _
			<CLSCompliant(False)> _
			Public Property GPSDifferential As Nullable(Of UInt16)
				Get
					Dim value As ExifRecord = Record(Tags.GPSDifferential)
					If value Is Nothing Then
						Return Nothing
					Else
						Return CType(value.Data, UInt16)
					End If
				End Get
				Set
					If value.HasValue Then
						Records(Tags.GPSDifferential) = New ExifRecord(TagFormat(Tags.GPSDifferential), value.Value, True)
					Else : Records(Tags.GPSDifferential) = Nothing : End If
				End Set
			End Property
#End Region
#End Region
			''' <summary>Gets format for tag specified</summary>
			''' <exception cref="InvalidEnumArgumentException"><paramref name="Tag"/> contains unknown value</exception>
			<CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
				Get
					Const any As ushort=0
					Select Case Tag
						Case Tags.GPSVersionID : Return New ExifTagFormat(4, &h0, "GPSVersionID", ExifDataTypes.Byte)
						Case Tags.GPSLatitudeRef : Return New ExifTagFormat(2, &h1, "GPSLatitudeRef", ExifDataTypes.ASCII)
						Case Tags.GPSLatitude : Return New ExifTagFormat(3, &h2, "GPSLatitude", ExifDataTypes.URational)
						Case Tags.GPSLongitudeRef : Return New ExifTagFormat(2, &h3, "GPSLongitudeRef", ExifDataTypes.ASCII)
						Case Tags.GPSLongitude : Return New ExifTagFormat(3, &h4, "GPSLongitude", ExifDataTypes.URational)
						Case Tags.GPSAltitudeRef : Return New ExifTagFormat(1, &h5, "GPSAltitudeRef", ExifDataTypes.Byte)
						Case Tags.GPSAltitude : Return New ExifTagFormat(1, &h6, "GPSAltitude", ExifDataTypes.URational)
						Case Tags.GPSTimeStamp : Return New ExifTagFormat(3, &h7, "GPSTimeStamp", ExifDataTypes.URational)
						Case Tags.GPSSatellites : Return New ExifTagFormat(any, &h8, "GPSSatellites", ExifDataTypes.ASCII)
						Case Tags.GPSStatus : Return New ExifTagFormat(2, &h9, "GPSStatus", ExifDataTypes.ASCII)
						Case Tags.GPSMeasureMode : Return New ExifTagFormat(2, &hA, "GPSMeasureMode", ExifDataTypes.ASCII)
						Case Tags.GPSDOP : Return New ExifTagFormat(1, &hB, "GPSDOP", ExifDataTypes.URational)
						Case Tags.GPSSpeedRef : Return New ExifTagFormat(2, &hC, "GPSSpeedRef", ExifDataTypes.ASCII)
						Case Tags.GPSSpeed : Return New ExifTagFormat(1, &hD, "GPSSpeed", ExifDataTypes.URational)
						Case Tags.GPSTrackRef : Return New ExifTagFormat(2, &hE, "GPSTrackRef", ExifDataTypes.ASCII)
						Case Tags.GPSTrack : Return New ExifTagFormat(1, &hF, "GPSTrack", ExifDataTypes.URational)
						Case Tags.GPSImgDirectionRef : Return New ExifTagFormat(2, &h10, "GPSImgDirectionRef", ExifDataTypes.ASCII)
						Case Tags.GPSImgDirection : Return New ExifTagFormat(1, &h11, "GPSImgDirection", ExifDataTypes.URational)
						Case Tags.GPSMapDatum : Return New ExifTagFormat(any, &h12, "GPSMapDatum", ExifDataTypes.ASCII)
						Case Tags.GPSDestLatitudeRef : Return New ExifTagFormat(2, &h13, "GPSDestLatitudeRef", ExifDataTypes.ASCII)
						Case Tags.GPSDestLatitude : Return New ExifTagFormat(3, &h14, "GPSDestLatitude", ExifDataTypes.URational)
						Case Tags.GPSDestLongitudeRef : Return New ExifTagFormat(2, &h15, "GPSDestLongitudeRef", ExifDataTypes.ASCII)
						Case Tags.GPSDestLongitude : Return New ExifTagFormat(3, &h16, "GPSDestLongitude", ExifDataTypes.URational)
						Case Tags.GPSDestBearingRef : Return New ExifTagFormat(2, &h17, "GPSDestBearingRef", ExifDataTypes.ASCII)
						Case Tags.GPSDestBearing : Return New ExifTagFormat(1, &h18, "GPSDestBearing", ExifDataTypes.URational)
						Case Tags.GPSDestDistanceRef : Return New ExifTagFormat(2, &h19, "GPSDestDistanceRef", ExifDataTypes.ASCII)
						Case Tags.GPSDestDistance : Return New ExifTagFormat(1, &h1A, "GPSDestDistance", ExifDataTypes.URational)
						Case Tags.GPSProcessingMethod : Return New ExifTagFormat(any, &h1B, "GPSProcessingMethod", ExifDataTypes.NA)
						Case Tags.GPSAreaInformation : Return New ExifTagFormat(any, &h1C, "GPSAreaInformation", ExifDataTypes.NA)
						Case Tags.GPSDateStamp : Return New ExifTagFormat(11, &h1D, "GPSDateStamp", ExifDataTypes.ASCII)
						Case Tags.GPSDifferential : Return New ExifTagFormat(1, &h1E, "GPSDifferential", ExifDataTypes.UInt16)
						Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
					End Select
				End Get
			End Property
		End Class
		Partial Public Class IfdInterop
			''' <summary>Tag numbers used in Exif Interoperability IFD</summary>
			''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added for enum items.</version>
			<CLSCompliant(False)> Public Enum Tags As UShort
#Region "Ineroperability Tags"
				''' <summary>Indicates the identification of the Interoperability rule.</summary>
				''' <version version="1.5.2"><see cref="FieldDisplayNameAttribute"/> added</version>
				<FieldDisplayName("Interoperability index"), Category("Interop")>InteroperabilityIndex = &h1
#End Region
			End Enum
#Region "Properties"
#Region "Interop"
			''' <summary>Indicates the identification of the Interoperability rule.</summary>
			''' <version version="1.5.2"><see cref="DisplayNameAttribute"/> added</version>
			''' <version version="1.5.4">Fix: Returned string no longer contains terminating nullchars.</version>
			''' <version version="1.5.4">Type changed from <see cref="String"/> to <see cref="Char"/>.</version>
			<DisplayName("Interoperability index"), Category("Interop"), Description("Indicates the identification of the Interoperability rule.")> _
			Public Property InteroperabilityIndex As String
				Get
					Dim value As ExifRecord = Record(Tags.InteroperabilityIndex)
					If value Is Nothing Then Return Nothing Else Return value.Data
				End Get
				Set
					If value IsNot Nothing Then
						Records(Tags.InteroperabilityIndex) = New ExifRecord(TagFormat(Tags.InteroperabilityIndex), value, False)
					Else : Records(Tags.InteroperabilityIndex) = Nothing : End If
				End Set
			End Property
#End Region
#End Region
			''' <summary>Gets format for tag specified</summary>
			''' <exception cref="InvalidEnumArgumentException"><paramref name="Tag"/> contains unknown value</exception>
			<CLSCompliant(False)> Public ReadOnly Property TagFormat(ByVal Tag As Tags) As ExifTagFormat
				Get
					Const any As ushort=0
					Select Case Tag
						Case Tags.InteroperabilityIndex : Return New ExifTagFormat(any, &h1, "InteroperabilityIndex", ExifDataTypes.ASCII)
						Case Else : Throw New InvalidEnumArgumentException("Tag", Tag, GetType(Tags))
					End Select
				End Get
			End Property
		End Class
End Namespace
#End Region
#End If