' GENERATED FILE -- DO NOT EDIT
'
' Generator: TransformCodeGenerator, Version=1.5.3.1, Culture=neutral, PublicKeyToken=373c02ac923768e6
' Version: 1.5.3.1
'
'
' Generated code from "IPTCTags.xml"
'
' Created: 27. července 2011
' By:BiggerBook\Honza
'
'Localize: IPTC needs localization of Decriptions, DisplayNames and error messages
'Localize: This auto-generated file was skipped during localization!
Imports System.ComponentModel
Imports Tools.ComponentModelT
Imports System.XML.Serialization
Imports Tools.DataStructuresT.GenericT
Imports Tools.DrawingT.DesignT
Imports Tools.MetadataT.IptcT.IptcDataTypes
Imports Tools.MetadataT.IptcT.Iptc
Namespace MetadataT.IptcT
#If Congig <= Nightly 'Stage: Nightly
#Region "Tag Enums"
		''' <summary>Numbers of IPTC records (groups of tags)</summary>
		Public Enum RecordNumbers As Byte
			''' <summary>Contains internal IPTC data used formerly in telecommunications. Now it is considered being deprecated and is not widely in use.</summary>
			<FieldDisplayName("")> Envelope = 1
			''' <summary>This record contain informative data about content. Whole record is optional, but when any tag is used then mandatory tags are mandatory.</summary>
			<FieldDisplayName("")> Application = 2
			''' <summary>This record provides image parameters. The record is optional however when used some datasets are mandatory.</summary>
			<FieldDisplayName("")> DigitalNewsphotoParameter = 3
			''' <summary>Unallocated IPTC Record. <see cref="MetadataT.IptcT"/> uses it for custom proprietary properties.</summary>
			<FieldDisplayName("")> NotAllocated5 = 5
			''' <summary>Information about ObjectData (before object has been sent)</summary>
			<FieldDisplayName("")> PreObjectDataDescriptorRecord = 7
			''' <summary>Contains embeded object</summary>
			<FieldDisplayName("")> ObjectDataRecord = 8
			''' <summary>Confirmation of size of ObjectData</summary>
			<FieldDisplayName("")> PostObjectDataDescriptorRecord = 9
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.Envelope"/> (1)</summary>
		Public Enum EnvelopeTags As Byte
			''' <summary>A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.ModelVersion"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Internal")> ModelVersion = 0
			''' <summary>This DataSet is to accommodate some providers who require routing information above the appropriate OSI layers.</summary>
			''' <remarks>See <seealso cref="IPTC.Destination"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Old IPTC")> Destination = 5
			''' <summary>A number representing the file format.</summary>
			''' <remarks>See <seealso cref="IPTC.FileFormat"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Embeded object")> FileFormat = 20
			''' <summary>A binary number representing the particular version of the <see cref="FileFormat"/></summary>
			''' <remarks>See <seealso cref="IPTC.FileFormatVersion"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Embeded object")> FileFormatVersion = 22
			''' <summary>Identifies the provider and product.</summary>
			''' <remarks>See <seealso cref="IPTC.ServiceIdentifier"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Old IPTC")> ServiceIdentifier = 30
			''' <summary>The characters form a number that will be unique for the date specified in <see cref="DateSent"/> and for the Service Identifier specified in <see cref="ServiceIdentifier"/>.</summary>
			''' <remarks>See <seealso cref="IPTC.EnvelopeNumber"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Old IPTC")> EnvelopeNumber = 40
			''' <summary>Allows a provider to identify subsets of its overall service.</summary>
			''' <remarks>See <seealso cref="IPTC.ProductID"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Old IPTC")> ProductID = 50
			''' <summary>Specifies the envelope handling priority and not the editorial urgency (see 2:10, <see cref="Urgency"/>).</summary>
			''' <remarks>See <seealso cref="IPTC.EnvelopePriority"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Status")> EnvelopePriority = 60
			''' <summary>Indicates year, month and day the service sent the material.</summary>
			''' <remarks>See <seealso cref="IPTC.DateSent"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> DateSent = 70
			''' <summary>This is the time the service sent the material.</summary>
			''' <remarks>See <seealso cref="IPTC.TimeSent"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> TimeSent = 80
			''' <summary>Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.</summary>
			''' <remarks>See <seealso cref="IPTC.CodedCharacterSet"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Old IPTC")> CodedCharacterSet = 90
			''' <summary>UNO Unique Name of Object, providing eternal, globally unique identification for objects as specified in the IIM, independent of provider and for any media form.</summary>
			''' <remarks>See <seealso cref="IPTC.UNO"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Old IPTC")> UNO = 100
			''' <summary>The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.</summary>
			''' <remarks>See <seealso cref="IPTC.ARMIdentifier"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Old IPTC")> ARMIdentifier = 120
			''' <summary>A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentifier'/>.</summary>
			''' <remarks>See <seealso cref="IPTC.ARMVersion"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Old IPTC")> ARMVersion = 122
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.Application"/> (2)</summary>
		Public Enum ApplicationTags As Byte
			''' <summary>A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.RecordVersion"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Internal")> RecordVersion = 0
			''' <summary>The Object Type is used to distinguish between different types of objects within the IIM.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectTypeReference"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Category")> ObjectTypeReference = 3
			''' <summary>The Object Attribute defines the nature of the object independent of the Subject.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectAttributeReference"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Category")> ObjectAttributeReference = 4
			''' <summary>Used as a shorthand reference for the object. Changes to existing data, such as updated stories or new crops on photos, should be identified in Edit Status.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectName"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Title")> ObjectName = 5
			''' <summary>Status of the objectdata, according to the practice of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.EditStatus"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Status")> EditStatus = 7
			''' <summary>Indicates the type of update that this object provides to a previous object. The link to the previous object is made using the ARM (DataSets 1:120 and 1:122 (<see cref='ARM'/>)), according to the practices of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.EditorialUpdate"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Status")> EditorialUpdate = 8
			''' <summary>Specifies the editorial urgency of content and not necessarily the envelope handling priority (see 1:60, <see cref="EnvelopePriority"/>).</summary>
			''' <remarks>See <seealso cref="IPTC.Urgency"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Status")> Urgency = 10
			''' <summary>The Subject Reference is a structured definition of the subject matter.</summary>
			''' <remarks>See <seealso cref="IPTC.SubjectReference"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Old IPTC")> SubjectReference = 12
			''' <summary>Identifies the subject of the objectdata in the opinion of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.Category"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Category")> Category = 15
			''' <summary>Supplemental categories further refine the subject of an objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.SupplementalCategory"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Category")> SupplementalCategory = 20
			''' <summary>Identifies objectdata that recurs often and predictably.</summary>
			''' <remarks>See <seealso cref="IPTC.FixtureIdentifier"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Category")> FixtureIdentifier = 22
			''' <summary>Used to indicate specific information retrieval words.</summary>
			''' <remarks>See <seealso cref="IPTC.Keywords"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Category")> Keywords = 25
			''' <summary>Indicates the code of a country/geographical location referenced by the content of the object.</summary>
			''' <remarks>See <seealso cref="IPTC.ContentLocationCode"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Location")> ContentLocationCode = 26
			''' <summary>Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.ContentLocationName"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Location")> ContentLocationName = 27
			''' <summary>The earliest date the provider intends the object to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ReleaseDate"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> ReleaseDate = 30
			''' <summary>The earliest time the provider intends the object to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ReleaseTime"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> ReleaseTime = 32
			''' <summary>The latest date the provider or owner intends the objectdata to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ExpirationDate"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> ExpirationDate = 37
			''' <summary>The latest time the provider or owner intends the objectdata to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ExpirationTime"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> ExpirationTime = 38
			''' <summary>Other editorial instructions concerning the use of the objectdata, such as embargoes and warnings.</summary>
			''' <remarks>See <seealso cref="IPTC.SpecialInstructions"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Other")> SpecialInstructions = 40
			''' <summary>Indicates the type of action that this object provides to a previous object.</summary>
			''' <remarks>See <seealso cref="IPTC.ActionAdvised"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Other")> ActionAdvised = 42
			''' <summary>Identifies the Service Identifier of a prior envelope to which the current object refers.</summary>
			''' <remarks>See <seealso cref="IPTC.ReferenceService"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Old IPTC")> ReferenceService = 45
			''' <summary>Identifies the date of a prior envelope to which the current object refers.</summary>
			''' <remarks>See <seealso cref="IPTC.ReferenceDate"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Old IPTC")> ReferenceDate = 47
			''' <summary>Identifies the Envelope Number of a prior envelope to which the current object refers.</summary>
			''' <remarks>See <seealso cref="IPTC.ReferenceNumber"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Old IPTC")> ReferenceNumber = 50
			''' <summary>The date the intellectual content of the objectdata was created rather than the date of the creation of the physical representation.</summary>
			''' <remarks>See <seealso cref="IPTC.DateCreated"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> DateCreated = 55
			''' <summary>The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.</summary>
			''' <remarks>See <seealso cref="IPTC.TimeCreated"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> TimeCreated = 60
			''' <summary>The date the digital representation of the objectdata was created.</summary>
			''' <remarks>See <seealso cref="IPTC.DigitalCreationDate"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> DigitalCreationDate = 62
			''' <summary>The time the digital representation of the objectdata was created.</summary>
			''' <remarks>See <seealso cref="IPTC.DigitalCreationTime"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Date")> DigitalCreationTime = 63
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.OriginatingProgram"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Other")> OriginatingProgram = 65
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.ProgramVersion"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Other")> ProgramVersion = 70
			''' <summary>Virtually only used in North America.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectCycle"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Status")> ObjectCycle = 75
			''' <summary>Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.</summary>
			''' <remarks>See <seealso cref="IPTC.ByLine"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Author")> ByLine = 80
			''' <summary>A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.</summary>
			''' <remarks>See <seealso cref="IPTC.ByLineTitle"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Author")> ByLineTitle = 85
			''' <summary>Identifies city of objectdata origin according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.City"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Location")> City = 90
			''' <summary>Identifies the location within a city from which the objectdata originates, according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.SubLocation"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Location")> SubLocation = 92
			''' <summary>Identifies Province/State of origin according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.ProvinceState"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Location")> ProvinceState = 95
			''' <summary>Indicates the code of the country/primary location where the intellectual property of the objectdata was created, e.g. a photo was taken, an event occurred.</summary>
			''' <remarks>See <seealso cref="IPTC.CountryPrimaryLocationCode"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Location")> CountryPrimaryLocationCode = 100
			''' <summary>Provides full, publishable, name of the country/primary location where the intellectual property of the objectdata was created, according to guidelines of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.CountryPrimaryLocationName"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Location")> CountryPrimaryLocationName = 101
			''' <summary>A code representing the location of original transmission according to practices of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.OriginalTransmissionReference"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Location")> OriginalTransmissionReference = 103
			''' <summary>A publishable entry providing a synopsis of the contents of the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.Headline"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Title")> Headline = 105
			''' <summary>Identifies the provider of the objectdata, not necessarily the owner/creator.</summary>
			''' <remarks>See <seealso cref="IPTC.Credit"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Author")> Credit = 110
			''' <summary>Identifies the original owner of the intellectual content of the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.Source"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Author")> Source = 115
			''' <summary>Contains any necessary copyright notice.</summary>
			''' <remarks>See <seealso cref="IPTC.CopyrightNotice"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Author")> CopyrightNotice = 116
			''' <summary>Identifies the person or organisation which can provide further background information on the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.Contact"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Author")> Contact = 118
			''' <summary>A textual description of the objectdata, particularly used where the object is not text.</summary>
			''' <remarks>See <seealso cref="IPTC.CaptionAbstract"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Title")> CaptionAbstract = 120
			''' <summary>Identification of the name of the person involved in the writing, editing or correcting the objectdata or caption/abstract.</summary>
			''' <remarks>See <seealso cref="IPTC.WriterEditor"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Author")> WriterEditor = 122
			''' <summary>Image width 460 pixels and image height 128 pixels. Scanning direction bottom to top, left to right.</summary>
			''' <remarks>See <seealso cref="IPTC.RasterizedeCaption"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Title")> RasterizedeCaption = 125
			''' <summary>Image Type</summary>
			''' <remarks>See <seealso cref="IPTC.ImageType"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image")> ImageType = 130
			''' <summary>Indicates the layout of the image area.</summary>
			''' <remarks>See <seealso cref="IPTC.ImageOrientation"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image")> ImageOrientation = 131
			''' <summary>Describes the major national language of the object, according to the 2-letter codes of ISO 639:1988.</summary>
			''' <remarks>See <seealso cref="IPTC.LanguageIdentifier"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Other")> LanguageIdentifier = 135
			''' <summary>Type of audio in objectdata</summary>
			''' <remarks>See <seealso cref="IPTC.AudioType"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Audio")> AudioType = 150
			''' <summary>Sampling rate, representing the sampling rate in hertz (Hz).</summary>
			''' <remarks>See <seealso cref="IPTC.AudioSamplingRate"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Audio")> AudioSamplingRate = 151
			''' <summary>The number of bits in each audio sample.</summary>
			''' <remarks>See <seealso cref="IPTC.AudioSamplingResolution"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Audio")> AudioSamplingResolution = 152
			''' <summary>The running time of an audio objectdata when played back at the speed at which it was recorded.</summary>
			''' <remarks>See <seealso cref="IPTC.AudioDuration"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Audio")> AudioDuration = 153
			''' <summary>Identifies the content of the end of an audio objectdata, according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.AudioOutcue"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Audio")> AudioOutcue = 154
			''' <summary>The file format of the ObjectData Preview.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataPreviewFileFormat"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Embeded object")> ObjectDataPreviewFileFormat = 200
			''' <summary>The particular version of the ObjectData Preview File Format specified in <see cref="ObjectDataPreviewFileFormat"/></summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataPreviewFileFormatVersion"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Embeded object")> ObjectDataPreviewFileFormatVersion = 201
			''' <summary>Maximum size of 256000 octets consisting of binary data.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataPreviewData"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> <Category("Embeded object")> ObjectDataPreviewData = 202
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.DigitalNewsphotoParameter"/> (3)</summary>
		Public Enum DigitalNewsphotoParameterTags As Byte
			''' <summary>A binary number representing the version of the Digital Newsphoto Parameter Record utilised by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.RecordVersion3"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Internal")> RecordVersion3 = 0
			''' <summary>The picture number provides a universally unique reference to an image.</summary>
			''' <remarks>See <seealso cref="IPTC.PictureNumber"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Old IPTC")> PictureNumber = 10
			''' <summary>A number representing the number of pixels in a scan line for the component with the highest resolution.</summary>
			''' <remarks>See <seealso cref="IPTC.PixelsPerLine"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> PixelsPerLine = 20
			''' <summary>A number representing the number of scan lines comprising the image for the component with the highest resolution.</summary>
			''' <remarks>See <seealso cref="IPTC.NumberOfLines"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> NumberOfLines = 30
			''' <summary>A number indicating the number of pixels per unit length in the scanning direction.</summary>
			''' <remarks>See <seealso cref="IPTC.PixelSizeInScanningDirection"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> PixelSizeInScanningDirection = 40
			''' <summary>A number indicating the number of pixels per unit length perpendicular to the scanning direction.</summary>
			''' <remarks>See <seealso cref="IPTC.PixelSizePerpendicularToScanningDirection"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> PixelSizePerpendicularToScanningDirection = 50
			''' <summary>A number indicating the image content.</summary>
			''' <remarks>See <seealso cref="IPTC.SupplementType"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> SupplementType = 55
			''' <summary>Indicates colour representation</summary>
			''' <remarks>See <seealso cref="IPTC.ColourRepresentation"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> ColourRepresentation = 60
			''' <summary>A value indicating the colour space in which the pixel values are expressed for each component in the image.</summary>
			''' <remarks>See <seealso cref="IPTC.InterchangeColourSpace"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> InterchangeColourSpace = 64
			''' <summary>Each of 1 to four octets contains a binary number that relates to the colour component using the identification number assigned to it in the appendix for each colour space.</summary>
			''' <remarks>See <seealso cref="IPTC.ColourSequence"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> ColourSequence = 65
			''' <summary>Specifies the International Color Consortium profile for the scanning/source device used to generate the digital image files.</summary>
			''' <remarks>See <seealso cref="IPTC.IccInputColourProfile"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> IccInputColourProfile = 66
			''' <summary>This DataSet is no longer required as its contents have been rendered obsolete by the introduction of DataSet P3:66 (ICC Input Colour Profile).</summary>
			''' <remarks>See <seealso cref="IPTC.ColourCalibrationMatrixTable"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> ColourCalibrationMatrixTable = 70
			''' <summary>Consists of one, three or four one-dimensional lookup tables (LUT).</summary>
			''' <remarks>See <seealso cref="IPTC.LookupTable"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> LookupTable = 80
			''' <summary>A binary number representing the number of index entries in the DataSet 3:85 (<see cref="ColourPalette"/>).</summary>
			''' <remarks>See <seealso cref="IPTC.NumberOfIndexEntries"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> NumberOfIndexEntries = 84
			''' <summary>In a single-frame colour image, a colour is described with a single sample per pixel.</summary>
			''' <remarks>See <seealso cref="IPTC.ColourPalette"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> ColourPalette = 85
			''' <summary>A number between 1 and 16 that indicates the number of bits per pixel value used as entries in the Colour Palette. These values are found in the objectdata itself.</summary>
			''' <remarks>See <seealso cref="IPTC.NumberOfBitsPerSample"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> NumberOfBitsPerSample = 86
			''' <summary>A number defining the spatial and temporal relationship between pixels.</summary>
			''' <remarks>See <seealso cref="IPTC.SamplingStructure"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> SamplingStructure = 90
			''' <summary>A number indicating the correct relative two dimensional order of the pixels in the objectdata. Eight possibilities exist.</summary>
			''' <remarks>See <seealso cref="IPTC.ScanningDirection"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> ScanningDirection = 100
			''' <summary>A number indicating the clockwise rotation applied to the image for presentation.</summary>
			''' <remarks>See <seealso cref="IPTC.ImageRotation"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> ImageRotation = 102
			''' <summary>Specifies data compression method</summary>
			''' <remarks>See <seealso cref="IPTC.DataCompressionMethod"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> DataCompressionMethod = 110
			''' <summary>Contains a binary number identifying the quantisation law.</summary>
			''' <remarks>See <seealso cref="IPTC.QuantisationMethod"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> QuantisationMethod = 120
			''' <summary>These end points apply to the coding process.</summary>
			''' <remarks>See <seealso cref="IPTC.EndPoints"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> EndPoints = 125
			''' <summary>Indicates if values outside the range defined by the end points in DataSet 3:125 may occur.</summary>
			''' <remarks>See <seealso cref="IPTC.ExcursionTolerance"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> ExcursionTolerance = 130
			''' <summary>Contains a sequence of one or more octets describing the number of bits used to encode each component. The sequence is specified by the order of components in DataSet 3:65.</summary>
			''' <remarks>See <seealso cref="IPTC.BitsPerComponent"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> BitsPerComponent = 135
			''' <summary>A binary value which specifies the maximum density range multiplied by 100.</summary>
			''' <remarks>See <seealso cref="IPTC.MaximumDensityRange"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> MaximumDensityRange = 140
			''' <summary>A binary value which specifies the value of gamma for the device multiplied by 100.</summary>
			''' <remarks>See <seealso cref="IPTC.GammaCompensatedValue"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Image 3")> GammaCompensatedValue = 145
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.NotAllocated5"/> (5)</summary>
		Public Enum NotAllocated5Tags As Byte
			''' <summary>Overll rating of the subject</summary>
			''' <remarks>See <seealso cref="IPTC.OverlallRating"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Status")> OverlallRating = 101
			''' <summary>Rates technical quality of subject data (e.g. Is the photo sharp?)</summary>
			''' <remarks>See <seealso cref="IPTC.TechnicalQuality"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Status")> TechnicalQuality = 102
			''' <summary>Rates artistic quality of subject data (i.e. How nice it is?)</summary>
			''' <remarks>See <seealso cref="IPTC.ArtQuality"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Status")> ArtQuality = 103
			''' <summary>Rates information value of subject data (i.e. Does it provide any valuable information?)</summary>
			''' <remarks>See <seealso cref="IPTC.InformationValue"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Status")> InformationValue = 104
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.PreObjectDataDescriptorRecord"/> (7)</summary>
		Public Enum PreObjectDataDescriptorRecordTags As Byte
			''' <summary>The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.</summary>
			''' <remarks>See <seealso cref="IPTC.SizeMode"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Embeded object")> SizeMode = 10
			''' <summary>The maximum size for the following Subfile DataSet(s).</summary>
			''' <remarks>See <seealso cref="IPTC.MaxSubfileSize"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Embeded object")> MaxSubfileSize = 20
			''' <summary>A binary number representing the overall size of the objectdata, expressed in octets, not including tags, if that size is known when transfer commences.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataSizeAnnounced"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Embeded object")> ObjectDataSizeAnnounced = 90
			''' <summary>Used when objectdata size is not known, indicating the largest size, expressed in octets, that the objectdata can possibly have, not including tags.</summary>
			''' <remarks>See <seealso cref="IPTC.MaximumObjectDataSize"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Embeded object")> MaximumObjectDataSize = 95
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.ObjectDataRecord"/> (8)</summary>
		Public Enum ObjectDataRecordTags As Byte
			''' <summary>Subfile DataSet containing the objectdata itself.</summary>
			''' <remarks>See <seealso cref="IPTC.Subfile"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Embeded object")> Subfile = 10
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.PostObjectDataDescriptorRecord"/> (9)</summary>
		Public Enum PostObjectDataDescriptorRecordTags As Byte
			''' <summary>Total size of the objectdata, in octets, without tags.</summary>
			''' <remarks>See <seealso cref="IPTC.ConfirmedObjectDataSize"/> for more info.</remarks>
			<FieldDisplayName("")> <Category("Embeded object")> ConfirmedObjectDataSize = 10
		End Enum
	Partial Class Iptc
		''' <summary>Gets Enum that contains list of tags for specific record (group of tags)</summary>
		''' <param name="Record">Number of record to get enum for</param>
		''' <exception cref="InvalidEnumArgumentException">Value of <paramref name="Record"/> is not member of <see cref="RecordNumbers"/></exception>
		Public Shared Function GetEnum(ByVal Record As RecordNumbers) As Type
			Select Case Record
				Case RecordNumbers.Envelope : Return GetType(EnvelopeTags)
				Case RecordNumbers.Application : Return GetType(ApplicationTags)
				Case RecordNumbers.DigitalNewsphotoParameter : Return GetType(DigitalNewsphotoParameterTags)
				Case RecordNumbers.NotAllocated5 : Return GetType(NotAllocated5Tags)
				Case RecordNumbers.PreObjectDataDescriptorRecord : Return GetType(PreObjectDataDescriptorRecordTags)
				Case RecordNumbers.ObjectDataRecord : Return GetType(ObjectDataRecordTags)
				Case RecordNumbers.PostObjectDataDescriptorRecord : Return GetType(PostObjectDataDescriptorRecordTags)
				Case Else : Throw New InvalidEnumArgumentException("Record", Record, GetType(RecordNumbers))
			End Select
		End Function
	End Class
		Partial Public Structure DataSetIdentification
			''' <summary>A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.</summary>
			''' <remarks>Version numbers are assigned by IPTC and NAA. The version number of this record is four (4).</remarks>
			Public Shared ReadOnly Property ModelVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ModelVersion, "ModelVersion", "Model Version")
				End Get
			End Property
			''' <summary>This DataSet is to accommodate some providers who require routing information above the appropriate OSI layers.</summary>
			Public Shared ReadOnly Property Destination As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.Destination, "Destination", "Destination")
				End Get
			End Property
			''' <summary>A number representing the file format.</summary>
			''' <remarks>The file format must be registered with IPTC or NAA with a unique number assigned to it (see Appendix A). The information is used to route the data to the appropriate system and to allow the receiving system to perform the appropriate actions thereto.</remarks>
			Public Shared ReadOnly Property FileFormat As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.FileFormat, "FileFormat", "File Format")
				End Get
			End Property
			''' <summary>A binary number representing the particular version of the <see cref="FileFormat"/></summary>
			Public Shared ReadOnly Property FileFormatVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.FileFormatVersion, "FileFormatVersion", "File Format Version")
				End Get
			End Property
			''' <summary>Identifies the provider and product.</summary>
			Public Shared ReadOnly Property ServiceIdentifier As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ServiceIdentifier, "ServiceIdentifier", "Service Identifier")
				End Get
			End Property
			''' <summary>The characters form a number that will be unique for the date specified in <see cref="DateSent"/> and for the Service Identifier specified in <see cref="ServiceIdentifier"/>.</summary>
			''' <remarks>If identical envelope numbers appear with the same date and with the same Service Identifier, records 2-9 must be unchanged from the original. This is not intended to be a sequential serial number reception check.</remarks>
			Public Shared ReadOnly Property EnvelopeNumber As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.EnvelopeNumber, "EnvelopeNumber", "Envelope Number")
				End Get
			End Property
			''' <summary>Allows a provider to identify subsets of its overall service.</summary>
			''' <remarks>Used to provide receiving organisation data on which to select, route, or otherwise handle data.</remarks>
			Public Shared ReadOnly Property ProductID As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ProductID, "ProductID", "Product I.D.")
				End Get
			End Property
			''' <summary>Specifies the envelope handling priority and not the editorial urgency (see 2:10, <see cref="Urgency"/>).</summary>
			''' <remarks>'1' indicates the most urgent, '5' the normal urgency, and '8' the least urgent copy. The numeral '9' indicates a User Defined Priority. The numeral '0' is reserved for future use.</remarks>
			Public Shared ReadOnly Property EnvelopePriority As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.EnvelopePriority, "EnvelopePriority", "Envelope Priority")
				End Get
			End Property
			''' <summary>Indicates year, month and day the service sent the material.</summary>
			Public Shared ReadOnly Property DateSent As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.DateSent, "DateSent", "Date Sent")
				End Get
			End Property
			''' <summary>This is the time the service sent the material.</summary>
			''' <remarks>This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
			''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
			Public Shared ReadOnly Property TimeSent As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.TimeSent, "TimeSent", "Time Sent")
				End Get
			End Property
			''' <summary>Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.</summary>
			''' <remarks>The control functions apply to character oriented DataSets in records 2-6. They also apply to record 8, unless the objectdata explicitly, or the File Format implicitly, defines character sets otherwise. If this DataSet contains the designation function for Unicode in UTF-8 then no other announcement, designation or invocation functions are permitted in this DataSet or in records 2-6. For all other character sets, one or more escape sequences are used: for the announcement of the code extension facilities used in the data which follows, for the initial designation of the G0, G1, G2 and G3 graphic character sets and for the initial invocation of the graphic set (7 bits) or the lefthand and the right-hand graphic set (8 bits) and for the initial invocation of the C0 (7 bits) or of the C0 and the C1 control character sets (8 bits). The announcement of the code extension facilities, if transmitted, must appear in this data set. Designation and invocation of graphic and control function sets (shifting) may be transmitted anywhere where the escape and the other necessary control characters are permitted. However, it is recommended to transmit in this DataSet an initial designation and invocation, i.e. to define all designations and the shift status currently in use by transmitting the appropriate escape sequences and locking-shift functions. If is omitted, the default for records 2-6 and 8 is ISO 646 IRV (7 bits) or ISO 4873 DV (8 bits). Record 1 shall always use ISO 646 IRV or ISO 4873 DV respectively. ECMA as the ISO Registration Authority for escape sequences maintains the International Register of Coded Character Sets to be used with escape sequences, a register of Codes and allocated standardised escape sequences, which are recognised by IPTC-NAA without further approval procedure. The registration procedure is defined in ISO 2375. IPTC-NAA maintain a Register of Codes and allocated private escape sequences, which are shown in paragraph 1.2. IPTC may, as Sponsoring Authority, submit such private sequence Codes for approval as standardised sequence Codes. The registers consist of a Graphic repertoire, a Control function repertoire and a Repertoire of other coding systems (e.g. complete Codes). Together they represent the IPTC-NAA Code Library. Graphic Repertoire94-character sets (intermediate character 2/8 to 2/11)002ISO 646 IRV 4/0004ISO 646 British Version 4/1006ISO 646 USA Version (ASCII) 4/2008-1NATS Primary Set for Finland and Sweden 4/3008-2NATS Secondary Set for Finland and Sweden 4/4009-1NATS Primary Set for Denmark and Norway 4/5009-2NATS Secondary Set for Denmark and Norway 4/6010ISO 646 Swedish Version (SEN 850200) 4/7015ISO 646 Italian Version (ECMA) 5/9016ISO 646 Portuguese Version (ECMA Olivetti) 4/12017ISO 646 Spanish Version (ECMA Olivetti) 5/10018ISO 646 Greek Version (ECMA) 5/11021ISO 646 German Version (DIN 66003) 4/11037Basic Cyrillic Character Set (ISO 5427) 4/14060ISO 646 Norwegian Version (NS 4551) 6/0069ISO 646 French Version (NF Z 62010-1982) 6/6084ISO 646 Portuguese Version (ECMA IBM) 6/7085ISO 646 Spanish Version (ECMA IBM) 6/8086ISO 646 Hungarian Version (HS 7795/3) 6/9121Alternate Primary Graphic Set No. 1 (Canada CSA Z 243.4-1985) 7/7122Alternate Primary Graphic Set No. 2 (Canada CSA Z 243.4-1985) 7/896-character sets (intermediate character 2/12 to 2/15):100Right-hand Part of Latin Alphabet No. 1 (ISO 8859-1) 4/1101Right-hand Part of Latin Alphabet No. 2 (ISO 8859-2) 4/2109Right-hand Part of Latin Alphabet No. 3 (ISO 8859-3) 4/3110Right-hand Part of Latin Alphabet No. 4 (ISO 8859-4) 4/4111Right-hand Part of Latin/Cyrillic Alphabet (ISO 8859-5) 4/0125Right-hand Part of Latin/Greek Alphabet (ISO 8859-7) 4/6127Right-hand Part of Latin/Arabic Alphabet (ISO 8859-6) 4/7138Right-hand Part of Latin/Hebrew Alphabet (ISO 8859-8) 4/8139Right-hand Part of Czechoslovak Standard (ČSN 369103) 4/9Multiple-Byte Graphic Character Sets (1st intermediate character 2/4, 2nd intermediate character 2/8 to 2/11)87Japanese characters (JIS X 0208-1983) 4/2Control Function RepertoireC0 Control Function Sets (intermediate character 2/1)001C0 Set of ISO 646 4/0026IPTC C0 Set for newspaper text transmission 4/3036C0 Set of ISO 646 with SS2 instead of IS4 4/4104Minimum C0 Set for ISO 4873 4/7 C1 Control Function Sets (intermediate character 2/2)077C1 Control Set of ISO 6429 4/3105Minimum C1 Set for ISO 4873 4/7 Single Additional Control Functions062Locking-Shift Two (LS2), ISO 2022 6/14063Locking-Shift Three (LS3), ISO 2022 6/15064Locking-Shift Three Right (LS3R), ISO 2022 7/12065Locking-Shift Two Right (LS2R), ISO 2022 7/13066Locking-Shift One Right (LS1R), ISO 2022 7/14Repertoire of Other Coding Systems (e.g. complete Codes, intermediate character 2/5 )196UCS Transformation Format (UTF-8) 4/7 What's mentioned above is from definition of IPTC standard. This class currently does not support all encodings that can be advertised here neither it supports changes of encodings using ISO 2022 mechanism anywhere in text. Only certain encodings are supported - see the property for details.</remarks>
			''' <version version="1.5.3">Added limited support for UTF-8 based encoding autodetection based on value of this dataset.</version>
			Public Shared ReadOnly Property CodedCharacterSet As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.CodedCharacterSet, "CodedCharacterSet", "CodedCharacterSet")
				End Get
			End Property
			''' <summary>UNO Unique Name of Object, providing eternal, globally unique identification for objects as specified in the IIM, independent of provider and for any media form.</summary>
			''' <remarks>The provider must ensure the UNO is unique. Objects with the same UNO are identical.</remarks>
			Public Shared ReadOnly Property UNO As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.UNO, "UNO", "UNO")
				End Get
			End Property
			''' <summary>The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ARMIdentifier As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ARMIdentifier, "ARMIdentifier", "ARM Identifier")
				End Get
			End Property
			''' <summary>A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentifier'/>.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ARMVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ARMVersion, "ARMVersion", "ARM Version")
				End Get
			End Property
			''' <summary>A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.</summary>
			''' <remarks>Version numbers are assigned by IPTC and NAA. The version number of this record is four (4). Same tag called Record Version" also exists in record no. 3.</remarks>
			Public Shared ReadOnly Property RecordVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.RecordVersion, "RecordVersion", "Record Version")
				End Get
			End Property
			''' <summary>The Object Type is used to distinguish between different types of objects within the IIM.</summary>
			''' <remarks>The first part is a number representing a language independent international reference to an Object Type followed by a colon separator. The second part, if used, is a text representation of the Object Type Number (maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix G, or in the language of the service as indicated in DataSet 2:135 (<see cref='LanguageIdentifier'/>)</remarks>
			Public Shared ReadOnly Property ObjectTypeReference As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectTypeReference, "ObjectTypeReference", "Object Type Reference")
				End Get
			End Property
			''' <summary>The Object Attribute defines the nature of the object independent of the Subject.</summary>
			''' <remarks>The first part is a number representing a language independent international reference to an Object Attribute followed by a colon separator. The second part, if used, is a text representation of the Object Attribute Number ( maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix G, or in the language of the service as indicated in DataSet 2:135 (<see cref='LanguageIdentifier'/>)</remarks>
			Public Shared ReadOnly Property ObjectAttributeReference As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectAttributeReference, "ObjectAttributeReference", "Object Attribute Reference")
				End Get
			End Property
			''' <summary>Used as a shorthand reference for the object. Changes to existing data, such as updated stories or new crops on photos, should be identified in Edit Status.</summary>
			Public Shared ReadOnly Property ObjectName As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectName, "ObjectName", "Object Name")
				End Get
			End Property
			''' <summary>Status of the objectdata, according to the practice of the provider.</summary>
			Public Shared ReadOnly Property EditStatus As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.EditStatus, "EditStatus", "Edit Status")
				End Get
			End Property
			''' <summary>Indicates the type of update that this object provides to a previous object. The link to the previous object is made using the ARM (DataSets 1:120 and 1:122 (<see cref='ARM'/>)), according to the practices of the provider.</summary>
			Public Shared ReadOnly Property EditorialUpdate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.EditorialUpdate, "EditorialUpdate", "Editorial Update")
				End Get
			End Property
			''' <summary>Specifies the editorial urgency of content and not necessarily the envelope handling priority (see 1:60, <see cref="EnvelopePriority"/>).</summary>
			''' <remarks>The '1' is most urgent, '5' normal and '8' denotes the least-urgent copy. The numerals '9' and '0' are reserved for future use.</remarks>
			Public Shared ReadOnly Property Urgency As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Urgency, "Urgency", "Urgency")
				End Get
			End Property
			''' <summary>The Subject Reference is a structured definition of the subject matter.</summary>
			''' <remarks>It must contain an IPR (default value is "IPTC"), an 8 digit Subject Reference Number and an optional Subject Name, Subject Matter Name and Subject Detail Name. Each part of the Subject reference is separated by a colon (:). The Subject Reference Number contains three parts, a 2 digit Subject Number, a 3 digit Subject Matter Number and a 3 digit Subject Detail Number thus providing unique identification of the object's subject.</remarks>
			Public Shared ReadOnly Property SubjectReference As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.SubjectReference, "SubjectReference", "Subject Reference")
				End Get
			End Property
			''' <summary>Identifies the subject of the objectdata in the opinion of the provider.</summary>
			''' <remarks>A list of categories will be maintained by a regional registry, where available, otherwise by the provider.</remarks>
			Public Shared ReadOnly Property Category As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Category, "Category", "Category")
				End Get
			End Property
			''' <summary>Supplemental categories further refine the subject of an objectdata.</summary>
			''' <remarks>Only a single supplemental category may be contained in each DataSet. A supplemental category may include any of the recognised categories as used in . Otherwise, selection of supplemental categories are left to the provider.</remarks>
			Public Shared ReadOnly Property SupplementalCategory As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.SupplementalCategory, "SupplementalCategory", "Supplemental Category")
				End Get
			End Property
			''' <summary>Identifies objectdata that recurs often and predictably.</summary>
			''' <remarks>Enables users to immediately find or recall such an object.</remarks>
			Public Shared ReadOnly Property FixtureIdentifier As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.FixtureIdentifier, "FixtureIdentifier", "Fixture Identifier")
				End Get
			End Property
			''' <summary>Used to indicate specific information retrieval words.</summary>
			''' <remarks>Each keyword uses a single Keywords DataSet. Multiple keywords use multiple Keywords DataSets. It is expected that a provider of various types of data that are related in subject matter uses the same keyword, enabling the receiving system or subsystems to search across all types of data for related material.</remarks>
			Public Shared ReadOnly Property Keywords As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Keywords, "Keywords", "Keywords")
				End Get
			End Property
			''' <summary>Indicates the code of a country/geographical location referenced by the content of the object.</summary>
			''' <remarks>Where ISO has established an appropriate country code under ISO 3166, that code will be used. When ISO3166 does not adequately provide for identification of a location or a country, e.g. ships at sea, space, IPTC will assign an appropriate threecharacter code under the provisions of ISO3166 to avoid conflicts. (see Appendix D) .</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ContentLocationCode As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ContentLocationCode, "ContentLocationCode", "Content Location Code")
				End Get
			End Property
			''' <summary>Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.</summary>
			''' <remarks>If used in the same object with DataSet , must immediately follow and correspond to it.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ContentLocationName As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ContentLocationName, "ContentLocationName", "Content Location Name")
				End Get
			End Property
			''' <summary>The earliest date the provider intends the object to be used.</summary>
			Public Shared ReadOnly Property ReleaseDate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReleaseDate, "ReleaseDate", "Release Date")
				End Get
			End Property
			''' <summary>The earliest time the provider intends the object to be used.</summary>
			''' <remarks>This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
			''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
			Public Shared ReadOnly Property ReleaseTime As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReleaseTime, "ReleaseTime", "Release Time")
				End Get
			End Property
			''' <summary>The latest date the provider or owner intends the objectdata to be used.</summary>
			Public Shared ReadOnly Property ExpirationDate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ExpirationDate, "ExpirationDate", "Expiration Date")
				End Get
			End Property
			''' <summary>The latest time the provider or owner intends the objectdata to be used.</summary>
			''' <remarks>This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
			''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
			Public Shared ReadOnly Property ExpirationTime As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ExpirationTime, "ExpirationTime", "Expiration Time")
				End Get
			End Property
			''' <summary>Other editorial instructions concerning the use of the objectdata, such as embargoes and warnings.</summary>
			Public Shared ReadOnly Property SpecialInstructions As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.SpecialInstructions, "SpecialInstructions", "Special Instructions")
				End Get
			End Property
			''' <summary>Indicates the type of action that this object provides to a previous object.</summary>
			''' <remarks>The link to the previous object is made using the (DataSets 1:120 () and 1:122 ()), according to the practices of the provider.</remarks>
			Public Shared ReadOnly Property ActionAdvised As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ActionAdvised, "ActionAdvised", "Action Advised")
				End Get
			End Property
			''' <summary>Identifies the Service Identifier of a prior envelope to which the current object refers.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ReferenceService As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReferenceService, "ReferenceService", "Reference Service")
				End Get
			End Property
			''' <summary>Identifies the date of a prior envelope to which the current object refers.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ReferenceDate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReferenceDate, "ReferenceDate", "Reference Date")
				End Get
			End Property
			''' <summary>Identifies the Envelope Number of a prior envelope to which the current object refers.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ReferenceNumber As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReferenceNumber, "ReferenceNumber", "Reference Number")
				End Get
			End Property
			''' <summary>The date the intellectual content of the objectdata was created rather than the date of the creation of the physical representation.</summary>
			''' <remarks>Thus a photo taken during the American Civil War would carry a creation date during that epoch (1861-1865) rather than the date the photo was digitised for archiving.</remarks>
			Public Shared ReadOnly Property DateCreated As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.DateCreated, "DateCreated", "Date Created")
				End Get
			End Property
			''' <summary>The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.</summary>
			''' <remarks>Where the time cannot be precisely determined, the closest approximation should be used. This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
			''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
			Public Shared ReadOnly Property TimeCreated As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.TimeCreated, "TimeCreated", "Time Created")
				End Get
			End Property
			''' <summary>The date the digital representation of the objectdata was created.</summary>
			''' <remarks>Thus a photo taken during the American Civil War would carry a Digital Creation Date within the past several years rather than the date where the image was captured on film, glass plate or other substrate during that epoch (1861-1865).</remarks>
			Public Shared ReadOnly Property DigitalCreationDate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.DigitalCreationDate, "DigitalCreationDate", "Digital Creation Date")
				End Get
			End Property
			''' <summary>The time the digital representation of the objectdata was created.</summary>
			''' <remarks>This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
			''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
			Public Shared ReadOnly Property DigitalCreationTime As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.DigitalCreationTime, "DigitalCreationTime", "Digital Creation Time")
				End Get
			End Property
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>Note: This DataSet to form an advisory to the user and are not "computer" fields. Programmers should not expect to find computer-readable information in this DataSet.</remarks>
			Public Shared ReadOnly Property OriginatingProgram As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.OriginatingProgram, "OriginatingProgram", "Originating Program")
				End Get
			End Property
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>Note: This DataSet to form an advisory to the user and are not "computer" fields. Programmers should not expect to find computer-readable information in this DataSet.</remarks>
			Public Shared ReadOnly Property ProgramVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ProgramVersion, "ProgramVersion", "Program Version")
				End Get
			End Property
			''' <summary>Virtually only used in North America.</summary>
			Public Shared ReadOnly Property ObjectCycle As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectCycle, "ObjectCycle", "Object Cycle")
				End Get
			End Property
			''' <summary>Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ByLine As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ByLine, "ByLine", "By-line")
				End Get
			End Property
			''' <summary>A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.</summary>
			''' <remarks>Examples: "Staff Photographer", "Corresponsal", "Envoyé Spécial"</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ByLineTitle As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ByLineTitle, "ByLineTitle", "By-line Title")
				End Get
			End Property
			''' <summary>Identifies city of objectdata origin according to guidelines established by the provider.</summary>
			Public Shared ReadOnly Property City As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.City, "City", "City")
				End Get
			End Property
			''' <summary>Identifies the location within a city from which the objectdata originates, according to guidelines established by the provider.</summary>
			Public Shared ReadOnly Property SubLocation As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.SubLocation, "SubLocation", "Sublocation")
				End Get
			End Property
			''' <summary>Identifies Province/State of origin according to guidelines established by the provider.</summary>
			Public Shared ReadOnly Property ProvinceState As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ProvinceState, "ProvinceState", "Province/State")
				End Get
			End Property
			''' <summary>Indicates the code of the country/primary location where the intellectual property of the objectdata was created, e.g. a photo was taken, an event occurred.</summary>
			''' <remarks>Where ISO has established an appropriate country code under ISO 3166, that code will be used. When ISO3166 does not adequately provide for identification of a location or a new country, e.g. ships at sea, space, IPTC will assign an appropriate three-character code under the provisions of ISO3166 to avoid conflicts. (see Appendix D)</remarks>
			Public Shared ReadOnly Property CountryPrimaryLocationCode As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.CountryPrimaryLocationCode, "CountryPrimaryLocationCode", "Country/Primary Location Code")
				End Get
			End Property
			''' <summary>Provides full, publishable, name of the country/primary location where the intellectual property of the objectdata was created, according to guidelines of the provider.</summary>
			Public Shared ReadOnly Property CountryPrimaryLocationName As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.CountryPrimaryLocationName, "CountryPrimaryLocationName", "Country/Primary Location Name")
				End Get
			End Property
			''' <summary>A code representing the location of original transmission according to practices of the provider.</summary>
			''' <remarks>Examples: BER-5, PAR-12-11-01</remarks>
			Public Shared ReadOnly Property OriginalTransmissionReference As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.OriginalTransmissionReference, "OriginalTransmissionReference", "Original Transmission Refrence")
				End Get
			End Property
			''' <summary>A publishable entry providing a synopsis of the contents of the objectdata.</summary>
			Public Shared ReadOnly Property Headline As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Headline, "Headline", "Headline")
				End Get
			End Property
			''' <summary>Identifies the provider of the objectdata, not necessarily the owner/creator.</summary>
			Public Shared ReadOnly Property Credit As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Credit, "Credit", "Credit")
				End Get
			End Property
			''' <summary>Identifies the original owner of the intellectual content of the objectdata.</summary>
			''' <remarks>This could be an agency, a member of an agency or an individual.</remarks>
			Public Shared ReadOnly Property Source As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Source, "Source", "Source")
				End Get
			End Property
			''' <summary>Contains any necessary copyright notice.</summary>
			Public Shared ReadOnly Property CopyrightNotice As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.CopyrightNotice, "CopyrightNotice", "Copyright Notice")
				End Get
			End Property
			''' <summary>Identifies the person or organisation which can provide further background information on the objectdata.</summary>
			Public Shared ReadOnly Property Contact As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Contact, "Contact", "Contact")
				End Get
			End Property
			''' <summary>A textual description of the objectdata, particularly used where the object is not text.</summary>
			Public Shared ReadOnly Property CaptionAbstract As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.CaptionAbstract, "CaptionAbstract", "Caption/Abstract")
				End Get
			End Property
			''' <summary>Identification of the name of the person involved in the writing, editing or correcting the objectdata or caption/abstract.</summary>
			Public Shared ReadOnly Property WriterEditor As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.WriterEditor, "WriterEditor", "Writer/Editor")
				End Get
			End Property
			''' <summary>Image width 460 pixels and image height 128 pixels. Scanning direction bottom to top, left to right.</summary>
			''' <remarks>Contains the rasterized objectdata description and is used where characters that have not been coded are required for the caption.</remarks>
			Public Shared ReadOnly Property RasterizedeCaption As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.RasterizedeCaption, "RasterizedeCaption", "Rasterized Caption")
				End Get
			End Property
			''' <summary>Image Type</summary>
			Public Shared ReadOnly Property ImageType As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ImageType, "ImageType", "Image Type")
				End Get
			End Property
			''' <summary>Indicates the layout of the image area.</summary>
			Public Shared ReadOnly Property ImageOrientation As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ImageOrientation, "ImageOrientation", "Image Orientation")
				End Get
			End Property
			''' <summary>Describes the major national language of the object, according to the 2-letter codes of ISO 639:1988.</summary>
			''' <remarks>Does not define or imply any coded character set, but is used for internal routing, e.g. to various editorial desks. Implementation note: Programmers should provide for three octets for Language Identifier because the ISO is expected to provide for 3-letter codes in the future.</remarks>
			Public Shared ReadOnly Property LanguageIdentifier As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.LanguageIdentifier, "LanguageIdentifier", "Language Identifier")
				End Get
			End Property
			''' <summary>Type of audio in objectdata</summary>
			''' <remarks>Note: When '0' or 'T' is used, the only authorised combination is "0T". This is the mechanism for sending a caption either to supplement an audio cut sent previously without a caption or to correct a previously sent caption.</remarks>
			Public Shared ReadOnly Property AudioType As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioType, "AudioType", "Audio Type")
				End Get
			End Property
			''' <summary>Sampling rate, representing the sampling rate in hertz (Hz).</summary>
			Public Shared ReadOnly Property AudioSamplingRate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioSamplingRate, "AudioSamplingRate", "Audio Sampling Rate")
				End Get
			End Property
			''' <summary>The number of bits in each audio sample.</summary>
			Public Shared ReadOnly Property AudioSamplingResolution As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioSamplingResolution, "AudioSamplingResolution", "Audio Sampling Resolution")
				End Get
			End Property
			''' <summary>The running time of an audio objectdata when played back at the speed at which it was recorded.</summary>
			Public Shared ReadOnly Property AudioDuration As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioDuration, "AudioDuration", "Audio Duration")
				End Get
			End Property
			''' <summary>Identifies the content of the end of an audio objectdata, according to guidelines established by the provider.</summary>
			''' <remarks>The outcue generally consists of the final words spoken within an audio objectdata or the final sounds heard.</remarks>
			Public Shared ReadOnly Property AudioOutcue As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioOutcue, "AudioOutcue", "Audio Outcue")
				End Get
			End Property
			''' <summary>The file format of the ObjectData Preview.</summary>
			''' <remarks>The file format must be registered with IPTC or NAA with a unique number assigned to it.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ObjectDataPreviewFileFormat As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectDataPreviewFileFormat, "ObjectDataPreviewFileFormat", "ObjectData Preview File Format")
				End Get
			End Property
			''' <summary>The particular version of the ObjectData Preview File Format specified in <see cref="ObjectDataPreviewFileFormat"/></summary>
			''' <remarks>The File Format Version is taken from the list included in Appendix A</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ObjectDataPreviewFileFormatVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectDataPreviewFileFormatVersion, "ObjectDataPreviewFileFormatVersion", "ObjectData Preview File Format Version")
				End Get
			End Property
			''' <summary>Maximum size of 256000 octets consisting of binary data.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ObjectDataPreviewData As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectDataPreviewData, "ObjectDataPreviewData", "ObjectData Preview Data")
				End Get
			End Property
			''' <summary>A binary number representing the version of the Digital Newsphoto Parameter Record utilised by the provider.</summary>
			''' <remarks>Version numbers are assigned by IPTC and NAA. The version of this record is four (4). Same tag called "Record Version" also exists in record no. 2.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property RecordVersion3 As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.RecordVersion3, "RecordVersion3", "Record Version (3)")
				End Get
			End Property
			''' <summary>The picture number provides a universally unique reference to an image.</summary>
			''' <remarks>For example, colour images, when split with colour components into multiple objects, i.e. envelopes, would carry the same Picture Number.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property PictureNumber As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.PictureNumber, "PictureNumber", "Picture Number")
				End Get
			End Property
			''' <summary>A number representing the number of pixels in a scan line for the component with the highest resolution.</summary>
			''' <remarks>Not valid when DataSet 3:60 () octet zero (0) is 0 (zero).NCPS values are 1024 or 2048.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property PixelsPerLine As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.PixelsPerLine, "PixelsPerLine", "Pixels Per Line")
				End Get
			End Property
			''' <summary>A number representing the number of scan lines comprising the image for the component with the highest resolution.</summary>
			''' <remarks>Not valid when DataSet 3:60 () octet zero (0) is 0 (zero).NCPS range is 1 to 2048.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property NumberOfLines As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.NumberOfLines, "NumberOfLines", "Number of Lines")
				End Get
			End Property
			''' <summary>A number indicating the number of pixels per unit length in the scanning direction.</summary>
			''' <remarks>Not valid when DataSet 3:60 () octet zero (0) is 0 (zero). NCPS value is 1.Pixel Size is a relative size expressed as the ratio of 3:40 to 3:50 (3:40/3:50).</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property PixelSizeInScanningDirection As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.PixelSizeInScanningDirection, "PixelSizeInScanningDirection", "Pixel Size In Scanning Direction")
				End Get
			End Property
			''' <summary>A number indicating the number of pixels per unit length perpendicular to the scanning direction.</summary>
			''' <remarks>Not valid when DataSet 3:60 () octet zero (0) is 0 (zero). NCPS value is 1.Pixel Size is a relative size expressed as the ratio of 3:40 to 3:50 (3:40/3:50).</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property PixelSizePerpendicularToScanningDirection As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.PixelSizePerpendicularToScanningDirection, "PixelSizePerpendicularToScanningDirection", "Pixel Size Perpendicular To Scanning Direction")
				End Get
			End Property
			''' <summary>A number indicating the image content.</summary>
			''' <remarks>Mandatory when the numeric character in DataSet 2:130 () is ‘9’ ()</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property SupplementType As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.SupplementType, "SupplementType", "Supplement Type")
				End Get
			End Property
			''' <summary>Indicates colour representation</summary>
			''' <remarks>The special interleaving structures refer to defined methods given in Appendix G according to the value in the sampling structure DataSet 3:90. Allowed combinations are: 0,01,03,0 - 4,0 (In a single-frame colour image, a colour is described with a single sample per pixel.)3,1 - 3,2 - 3,3 - 3,4 - 3,54,1 - 4,2 - 4,3 - 4,4 - 4,5NCPS values are 1,0 , 3,1 or 3,5.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property ColourRepresentation As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.ColourRepresentation, "ColourRepresentation", "Colour Representation")
				End Get
			End Property
			''' <summary>A value indicating the colour space in which the pixel values are expressed for each component in the image.</summary>
			''' <remarks>Mandatory if DataSet 3:60 octet zero (0) has a value greater than one, i.e. a multi-component image. Not valid when DataSet 3:60 octet zero (0) is 0 (zero). NCPS values are 0, 4 or 7 (, or ).</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property InterchangeColourSpace As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.InterchangeColourSpace, "InterchangeColourSpace", "Interchange Colour Space")
				End Get
			End Property
			''' <summary>Each of 1 to four octets contains a binary number that relates to the colour component using the identification number assigned to it in the appendix for each colour space.</summary>
			''' <remarks>The sequence specifies the sequence of the components as they appear in the objectdataFor frame sequential components, only one octet is set to identify the current colour component in the objectdata.Mandatory if DataSet 3:60 octet zero (0) has a value greater than one, i.e. a multi-component image AND the value of octet one (1) is 0, 1, 2, 3, or 4, i.e. single frame, frame sequential, line sequential or pixel sequential. Not valid when DataSet 3:60 octet zero (0) is 0 (zero).Allowed values are: 0 - 1 - 2 - 3 - 4; 0 - may be used for “monochrome” or “no image” representations. Other values are reserved.NCPS values are 0,1,2,3 or 123.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property ColourSequence As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.ColourSequence, "ColourSequence", "Colour Sequence")
				End Get
			End Property
			''' <summary>Specifies the International Color Consortium profile for the scanning/source device used to generate the digital image files.</summary>
			''' <remarks>Valid when DataSet 3:64 has values 1,2,4,5 or 8.This profile can be used to translate the image colour information from the input device colour space into another device's native colour space. The ICC profile is specified in ISO/TC 130/WG2N562.Maximul alloved lenght of this field according to IPCT IIM standard is 512K. However current implementation does not allow such big fields.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property IccInputColourProfile As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.IccInputColourProfile, "IccInputColourProfile", "ICC Input Colour Profile")
				End Get
			End Property
			''' <summary>This DataSet is no longer required as its contents have been rendered obsolete by the introduction of DataSet P3:66 (ICC Input Colour Profile).</summary>
			''' <remarks>This DataSet will be removed from the next Version of this Standard.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property ColourCalibrationMatrixTable As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.ColourCalibrationMatrixTable, "ColourCalibrationMatrixTable", "Colour Calibration Matrix Table")
				End Get
			End Property
			''' <summary>Consists of one, three or four one-dimensional lookup tables (LUT).</summary>
			''' <remarks>The LUT relates to the image data in the colour space defined in DataSet 3:64 and specifies the correction to apply to the pixel values before display or printing of the image. Not applicable if the colour space requires converting before display or printing. Maximul alloved lenght of this field according to IPCT IIM standard is 131072. However current implementation does not allow such big fields.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property LookupTable As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.LookupTable, "LookupTable", "Lookup Table")
				End Get
			End Property
			''' <summary>A binary number representing the number of index entries in the DataSet 3:85 (<see cref="ColourPalette"/>).</summary>
			''' <remarks>Mandatory where DataSet 3:60 octet zero (0) has a value greater than one, i.e. a multi-component image AND the value of octet one (1) is 0, i.e. single-frame. Not relevant for other image types.0 - No Colour Palette contained in DataSet 3:85. A default palette should be used. 1 - 65535 - valid numbers.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property NumberOfIndexEntries As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.NumberOfIndexEntries, "NumberOfIndexEntries", "Number of Index Entries")
				End Get
			End Property
			''' <summary>In a single-frame colour image, a colour is described with a single sample per pixel.</summary>
			''' <remarks>Mandatory if 3:84 exists and is non zero.The pixel value is used as an index into the Colour Palette.The purpose of the Colour Palette is to act as a lookup table mapping the pixel values into the Colour Space defined in 3:64.The number of index entries is defined in DataSet 3:84.The number of output values is defined in octet zero of DataSet 3:60.The number of octets used for each output value is deducted from 3:135.The colour sequence of the output values is defined in 3:65. A default palette may be referenced if this DataSet is omitted. A number of default palettes may be held to be selected according to the device identifier component of the Picture Number 3:10.Maximul alloved lenght of this field according to IPCT IIM standard is 524288. However current implementation does not allow such big fields.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property ColourPalette As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.ColourPalette, "ColourPalette", "Colour Pallette")
				End Get
			End Property
			''' <summary>A number between 1 and 16 that indicates the number of bits per pixel value used as entries in the Colour Palette. These values are found in the objectdata itself.</summary>
			''' <remarks>Mandatory if DataSet 3:60 octet zero (0) has a value greater than one, i.e. a multi-component image AND the value of octet one (1) is 0, i.e. single frame. Not relevant for other image types.Each entry should be in one octet if number of bits is less than or equal to 8 and in two octets if number of bits is between 9 and 16, the least significant bit should always be aligned on the least significant bit of the least significant octet.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property NumberOfBitsPerSample As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.NumberOfBitsPerSample, "NumberOfBitsPerSample", "Number of Bits per Sample")
				End Get
			End Property
			''' <summary>A number defining the spatial and temporal relationship between pixels.</summary>
			''' <remarks>NCPS values are 0 or 2.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property SamplingStructure As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.SamplingStructure, "SamplingStructure", "Sampling Structure")
				End Get
			End Property
			''' <summary>A number indicating the correct relative two dimensional order of the pixels in the objectdata. Eight possibilities exist.</summary>
			''' <remarks>Not valid when DataSet 3:60 octet zero (0) is 0 (zero).NCPS value is 0.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property ScanningDirection As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.ScanningDirection, "ScanningDirection", "Scanning Direction")
				End Get
			End Property
			''' <summary>A number indicating the clockwise rotation applied to the image for presentation.</summary>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property ImageRotation As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.ImageRotation, "ImageRotation", "Image Rotation")
				End Get
			End Property
			''' <summary>Specifies data compression method</summary>
			''' <remarks>Not valid when DataSet 3:60 octet zero (0) is 0 (zero).Octets 0-1 contain a binary number identifying the providerowner of the algorithm.Octet 2 contains a binary number identifying the type of compression algorithm.Octet 3 contains a binary number identifying the revision number of the algorithm.An identification number is issued by IPTC-NAA to providersowners of compression algorithms upon request (see Appendix A). The numbers identifying type and revision of algorithms are managed by the providers-owners.A zero (0) value of all octets in this DataSet identifies an uncompressed image. In this case the component values should be in one octet if number of bits is less than or equal to 8 and in two octets if number of bits is between 9 and 16, the least significant bit always being aligned on the least significant bit of the least significant octet.NCPS values are 0000 or 0121.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property DataCompressionMethod As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.DataCompressionMethod, "DataCompressionMethod", "Data Compression Method")
				End Get
			End Property
			''' <summary>Contains a binary number identifying the quantisation law.</summary>
			''' <remarks>The relations between different quantisation methods are described in DNPR Guideline 1. Not valid when DataSet 3:60 octet zero (0) is 0 (zero).NCPS values are 0 or 5.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property QuantisationMethod As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.QuantisationMethod, "QuantisationMethod", "Quantisation Method")
				End Get
			End Property
			''' <summary>These end points apply to the coding process.</summary>
			''' <remarks>2n octets. Valid only when DataSet 3:64 has a value of 0,1,2,4 or 5. Not relevant for other colour spaces. n = the number of octets per component as derived from 3:135 multiplied by the number of components. The number of components is 1 when octet one (1) of DataSet 3:60 has a value of one (1), in all other cases the number is defined in octet zero (0) of DataSet 3:60.The first n octets contain the values representing the minimum density that is encoded for each component in the order specified in DataSet 3:65.The second n octets contain the values representing the maximum density that is encoded for each component in the order specified in DataSet 3:65.The difference between the maximum and minimum density for every component is the same and given by the Maximum Density Range value in DataSet 3:140.NCPS end point values are 255 and 0.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property EndPoints As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.EndPoints, "EndPoints", "End Points")
				End Get
			End Property
			''' <summary>Indicates if values outside the range defined by the end points in DataSet 3:125 may occur.</summary>
			''' <remarks>NCPS value is false</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property ExcursionTolerance As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.ExcursionTolerance, "ExcursionTolerance", "Excursion Tolerance")
				End Get
			End Property
			''' <summary>Contains a sequence of one or more octets describing the number of bits used to encode each component. The sequence is specified by the order of components in DataSet 3:65.</summary>
			''' <remarks>The number of octets is 1 when octet one (1) of DataSet 3:60 has a value of one (1), in all other cases the number of octets is equivalent to the number of components specified in octet zero (0) of DataSet 3:60.Each octet contains a binary value between one and 16.NCPS values are 8 or 888.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property BitsPerComponent As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.BitsPerComponent, "BitsPerComponent", "Bits Per Component")
				End Get
			End Property
			''' <summary>A binary value which specifies the maximum density range multiplied by 100.</summary>
			''' <remarks>Not valid when DataSet 3:60 octet zero (0) is 0 (zero).The value represents the difference between the lowest density and the highest density points that can be encoded by the originating system.NCPS value is 160.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property MaximumDensityRange As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.MaximumDensityRange, "MaximumDensityRange", "Maximum Density Range")
				End Get
			End Property
			''' <summary>A binary value which specifies the value of gamma for the device multiplied by 100.</summary>
			''' <remarks>Valid only when DataSet 3:120 has a value of 5 or 7.If this DataSet is omitted receiving equipment should assume that a gamma value of 2.22 appliesNCPS value is 222</remarks>
			Public Shared ReadOnly Property GammaCompensatedValue As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.DigitalNewsphotoParameter, DigitalNewsphotoParametertags.GammaCompensatedValue, "GammaCompensatedValue", "Gamma Compensated Value")
				End Get
			End Property
			''' <summary>Overll rating of the subject</summary>
			''' <remarks>This property is not part of IPTC IIM standard and other software may ignore or remove it.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property OverlallRating As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.NotAllocated5, NotAllocated5tags.OverlallRating, "OverlallRating", "Overall Rating")
				End Get
			End Property
			''' <summary>Rates technical quality of subject data (e.g. Is the photo sharp?)</summary>
			''' <remarks>This property is not part of IPTC IIM standard and other software may ignore or remove it.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property TechnicalQuality As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.NotAllocated5, NotAllocated5tags.TechnicalQuality, "TechnicalQuality", "Technical Quality")
				End Get
			End Property
			''' <summary>Rates artistic quality of subject data (i.e. How nice it is?)</summary>
			''' <remarks>This property is not part of IPTC IIM standard and other software may ignore or remove it.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property ArtQuality As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.NotAllocated5, NotAllocated5tags.ArtQuality, "ArtQuality", "Art Quality")
				End Get
			End Property
			''' <summary>Rates information value of subject data (i.e. Does it provide any valuable information?)</summary>
			''' <remarks>This property is not part of IPTC IIM standard and other software may ignore or remove it.</remarks>
			''' <version version="1.5.4">This property is new in version 1.5.4</version>
			Public Shared ReadOnly Property InformationValue As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.NotAllocated5, NotAllocated5tags.InformationValue, "InformationValue", "Information Value")
				End Get
			End Property
			''' <summary>The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.</summary>
			Public Shared ReadOnly Property SizeMode As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PreObjectDataDescriptorRecord, PreObjectDataDescriptorRecordtags.SizeMode, "SizeMode", "Size Mode")
				End Get
			End Property
			''' <summary>The maximum size for the following Subfile DataSet(s).</summary>
			''' <remarks>The largest number is not defined, but programmers should provide at least for the largest binary number contained in four octets taken together. If the entire object is to be transferred together within a single DataSet 8:10, the number equals the size of the object.</remarks>
			Public Shared ReadOnly Property MaxSubfileSize As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PreObjectDataDescriptorRecord, PreObjectDataDescriptorRecordtags.MaxSubfileSize, "MaxSubfileSize", "Max Subfile Size")
				End Get
			End Property
			''' <summary>A binary number representing the overall size of the objectdata, expressed in octets, not including tags, if that size is known when transfer commences.</summary>
			''' <remarks>Mandatory if DataSet has value '1' and not allowed if DataSet has value '0'.</remarks>
			Public Shared ReadOnly Property ObjectDataSizeAnnounced As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PreObjectDataDescriptorRecord, PreObjectDataDescriptorRecordtags.ObjectDataSizeAnnounced, "ObjectDataSizeAnnounced", "ObjectData Size Announced")
				End Get
			End Property
			''' <summary>Used when objectdata size is not known, indicating the largest size, expressed in octets, that the objectdata can possibly have, not including tags.</summary>
			Public Shared ReadOnly Property MaximumObjectDataSize As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PreObjectDataDescriptorRecord, PreObjectDataDescriptorRecordtags.MaximumObjectDataSize, "MaximumObjectDataSize", "Maximum ObjectData Size")
				End Get
			End Property
			''' <summary>Subfile DataSet containing the objectdata itself.</summary>
			''' <remarks>Subfiles must be sequential so that the subfiles may be reassembled.</remarks>
			Public Shared ReadOnly Property Subfile As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.ObjectDataRecord, ObjectDataRecordtags.Subfile, "Subfile", "Subfile")
				End Get
			End Property
			''' <summary>Total size of the objectdata, in octets, without tags.</summary>
			''' <remarks>This number should equal the number in DataSet if the size of the objectdata is known and has been provided.</remarks>
			Public Shared ReadOnly Property ConfirmedObjectDataSize As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PostObjectDataDescriptorRecord, PostObjectDataDescriptorRecordtags.ConfirmedObjectDataSize, "ConfirmedObjectDataSize", "Confirmed ObjectData Size")
				End Get
			End Property
			''' <summary>Returns all known data sets</summary>
			''' <param name="Hidden">Returns also datasets that are within groups</param>
			Public Shared Function KnownDataSets(Optional ByVal Hidden As Boolean = False) As DataSetIdentification()
				If Hidden Then
					Return New DataSetIdentification(){ModelVersion, Destination, FileFormat, FileFormatVersion, ServiceIdentifier, EnvelopeNumber, ProductID, EnvelopePriority, DateSent, TimeSent, CodedCharacterSet, UNO, ARMIdentifier, ARMVersion, RecordVersion, ObjectTypeReference, ObjectAttributeReference, ObjectName, EditStatus, EditorialUpdate, Urgency, SubjectReference, Category, SupplementalCategory, FixtureIdentifier, Keywords, ContentLocationCode, ContentLocationName, ReleaseDate, ReleaseTime, ExpirationDate, ExpirationTime, SpecialInstructions, ActionAdvised, ReferenceService, ReferenceDate, ReferenceNumber, DateCreated, TimeCreated, DigitalCreationDate, DigitalCreationTime, OriginatingProgram, ProgramVersion, ObjectCycle, ByLine, ByLineTitle, City, SubLocation, ProvinceState, CountryPrimaryLocationCode, CountryPrimaryLocationName, OriginalTransmissionReference, Headline, Credit, Source, CopyrightNotice, Contact, CaptionAbstract, WriterEditor, RasterizedeCaption, ImageType, ImageOrientation, LanguageIdentifier, AudioType, AudioSamplingRate, AudioSamplingResolution, AudioDuration, AudioOutcue, ObjectDataPreviewFileFormat, ObjectDataPreviewFileFormatVersion, ObjectDataPreviewData, RecordVersion3, PictureNumber, PixelsPerLine, NumberOfLines, PixelSizeInScanningDirection, PixelSizePerpendicularToScanningDirection, SupplementType, ColourRepresentation, InterchangeColourSpace, ColourSequence, IccInputColourProfile, ColourCalibrationMatrixTable, LookupTable, NumberOfIndexEntries, ColourPalette, NumberOfBitsPerSample, SamplingStructure, ScanningDirection, ImageRotation, DataCompressionMethod, QuantisationMethod, EndPoints, ExcursionTolerance, BitsPerComponent, MaximumDensityRange, GammaCompensatedValue, OverlallRating, TechnicalQuality, ArtQuality, InformationValue, SizeMode, MaxSubfileSize, ObjectDataSizeAnnounced, MaximumObjectDataSize, Subfile, ConfirmedObjectDataSize}
				Else
					Return New DataSetIdentification(){ModelVersion, Destination, FileFormat, FileFormatVersion, ServiceIdentifier, EnvelopeNumber, ProductID, EnvelopePriority, DateSent, TimeSent, CodedCharacterSet, UNO, RecordVersion, ObjectTypeReference, ObjectAttributeReference, ObjectName, EditStatus, EditorialUpdate, Urgency, SubjectReference, Category, SupplementalCategory, FixtureIdentifier, Keywords, ReleaseDate, ReleaseTime, ExpirationDate, ExpirationTime, SpecialInstructions, ActionAdvised, DateCreated, TimeCreated, DigitalCreationDate, DigitalCreationTime, OriginatingProgram, ProgramVersion, ObjectCycle, City, SubLocation, ProvinceState, CountryPrimaryLocationCode, CountryPrimaryLocationName, OriginalTransmissionReference, Headline, Credit, Source, CopyrightNotice, Contact, CaptionAbstract, WriterEditor, RasterizedeCaption, ImageType, ImageOrientation, LanguageIdentifier, AudioType, AudioSamplingRate, AudioSamplingResolution, AudioDuration, AudioOutcue, RecordVersion3, PictureNumber, PixelsPerLine, NumberOfLines, PixelSizeInScanningDirection, PixelSizePerpendicularToScanningDirection, SupplementType, ColourRepresentation, InterchangeColourSpace, ColourSequence, IccInputColourProfile, ColourCalibrationMatrixTable, LookupTable, NumberOfIndexEntries, ColourPalette, NumberOfBitsPerSample, SamplingStructure, ScanningDirection, ImageRotation, DataCompressionMethod, QuantisationMethod, EndPoints, ExcursionTolerance, BitsPerComponent, MaximumDensityRange, GammaCompensatedValue, OverlallRating, TechnicalQuality, ArtQuality, InformationValue, SizeMode, MaxSubfileSize, ObjectDataSizeAnnounced, MaximumObjectDataSize, Subfile, ConfirmedObjectDataSize}
				End If
			End Function
		End Structure
#End Region
#Region "Enums"
	Partial Class Iptc
		''' <summary>Possible values of <see cref="ActionAdvised"/></summary>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of AdvisedActions)))> Public Enum AdvisedActions As Byte
			''' <summary>Object Kill. Signifies that the provider wishes the holder of a copy of the referenced object make no further use of that information and take steps to prevent further distribution thereof.</summary>
			''' <remarks>Implies that any use of the object might result in embarrassment or other exposure of the provider and/or recipient.</remarks>
			<FieldDisplayName("")> ObjectKill = 1
			''' <summary>Object Replace. Signifies that the provider wants to replace the referenced object with the object provided under the current envelope.</summary>
			<FieldDisplayName("")> ObjectReplace = 2
			''' <summary>Object Append. Signifies that the provider wants to append to the referenced object information provided in the objectdata of the current envelope.</summary>
			<FieldDisplayName("")> ObjectAppend = 3
			''' <summary>Object Reference. Signifies that the provider wants to make reference to objectdata in a different envelope.</summary>
			<FieldDisplayName("")> ObjectReference = 4
		End Enum
		''' <summary>Abstract Relation Methods Identifiers</summary>
		<Restrict(True)> <CLSCompliant(False)> <TypeConverter(GetType(EnumConverterWithAttributes(Of ARMMethods)))> Public Enum ARMMethods As UShort
			''' <summary>Using DataSets 2:45, 2:47 and 2:50 (<see cref='ReferenceService'/>, <see cref='ReferenceDate'/> and <see cref='ReferenceNumber'/>)</summary>
			<FieldDisplayName("")> IPTCMethod1 = 1
			''' <summary>Using DataSet 1:100 (<see cref='UNO'/>)</summary>
			<FieldDisplayName("")> IPTCMethod2 = 2
		End Enum
		''' <summary>Abstract Relation Method Versions</summary>
		<Restrict(True)> <CLSCompliant(False)> <TypeConverter(GetType(EnumConverterWithAttributes(Of ARMVersions)))> Public Enum ARMVersions As UShort
			''' <summary>The only ARM version</summary>
			<FieldDisplayName("")> ARM1 = 1
		End Enum
		''' <summary>Indicates colour representations</summary>
		''' <remarks>The value is composed from two parts - number of components and buid-up structure of image. Use masks to get these componets.</remarks>
		''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
		<Flags()> <Restrict(False)> <TypeConverter(GetType(EnumConverterWithAttributes(Of ColourRepresentations)))> Public Enum ColourRepresentations As Short
			''' <summary>Mask for getting number of components</summary>
			''' <remarks>To get number of components AND enumeration value with this mask. To get numeric value right-shift AND-ed value by 8 bits.</remarks>
			<Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> NumberOfComponentsMask = &hFF00S
			''' <summary>Mask for getting buid-up structure of image</summary>
			''' <remarks>To get image build-up structure AND enumeration value with this mask.</remarks>
			<Browsable(false),EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("")> ImageBuildUpStructureMask = &h00FFS
			''' <summary>Monochrome</summary>
			<FieldDisplayName("")> Monochrome = &h100
			''' <summary>Three componnets</summary>
			<FieldDisplayName("")> ThreeComponents = &h300
			''' <summary>Four components</summary>
			<FieldDisplayName("")> FourComponents = &h400
			''' <summary>No Image</summary>
			<FieldDisplayName("")> NoImage = 0
			''' <summary>Single frame.</summary>
			<FieldDisplayName("")> SingleFrame = 0
			''' <summary>Frame sequential in multiple objects (one component per object)</summary>
			''' <remarks>Only one component of a colour image in one envelope</remarks>
			<FieldDisplayName("")> SequentialFrameMultipleObjects = 1
			''' <summary>Frame sequential in one object</summary>
			''' <remarks>All components of a colour imagein one envelope.</remarks>
			<FieldDisplayName("")> SequentialFrameOneObject = 2
			''' <summary>Line sequential</summary>
			<FieldDisplayName("")> LineSequential = 3
			''' <summary>Pixel sequential</summary>
			<FieldDisplayName("")> PixelSequential = 4
			''' <summary>Special interleaving structure</summary>
			''' <remarks>The special interleaving structures refer to defined methods given in Appendix G according to the value in the sampling structure DataSet 3:90</remarks>
			<FieldDisplayName("")> SpecialStructure = 5
		End Enum
		''' <summary>Allowed Interchange Colour Space values</summary>
		''' <remarks>Other values are reserved for future use.</remarks>
		''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of ColourSpaceValue)))> Public Enum ColourSpaceValue As Byte
			''' <summary>No colour space set</summary>
			<FieldDisplayName("")> none = 0
			''' <summary>X,Y,Z CIE colour space (default illuminant D50)</summary>
			<FieldDisplayName("")> XyzCie = 1
			''' <summary>RGB SMPTE</summary>
			<FieldDisplayName("")> RgbSmpte = 2
			''' <summary>Y,U,V (K) (default illuminant D65)</summary>
			<FieldDisplayName("")> YuvK = 3
			''' <summary>RGB device dependent</summary>
			<FieldDisplayName("")> Rgb = 4
			''' <summary>CMY(K) device dependent</summary>
			<FieldDisplayName("")> Cmyk = 5
			''' <summary>L*a*b* (K) CIE colour space (default illuminant D50)</summary>
			<FieldDisplayName("")> LabKCie = 6
			''' <summary>YCbCr</summary>
			<FieldDisplayName("")> YCbCr = 7
			''' <summary>sRGB</summary>
			<FieldDisplayName("")> sRgb = 8
		End Enum
		''' <summary>Custom rating values</summary>
		''' <version version="1.5.4">This enum is new in version 1.5.4</version>
		<Restrict(True)> <CLSCompliant(False)> <TypeConverter(GetType(EnumConverterWithAttributes(Of CustomRating)))> Public Enum CustomRating As SByte
			''' <summary>User rated the subject as rejected</summary>
			<FieldDisplayName("")> Rejected = -1
			''' <summary>Rating was not set yet</summary>
			<FieldDisplayName("")> NotRated = 0
			''' <summary>User rated the subject with 1 star</summary>
			<FieldDisplayName("")> Star1 = 1
			''' <summary>User rated the subject with 2 stars</summary>
			<FieldDisplayName("")> Star2 = 2
			''' <summary>User rated the subject with 3 stars</summary>
			<FieldDisplayName("")> Star3 = 3
			''' <summary>User rated the subject with 4 stars</summary>
			<FieldDisplayName("")> Star4 = 4
			''' <summary>User rated the subject with 5 stars</summary>
			<FieldDisplayName("")> Star5 = 5
		End Enum
		''' <summary>Subject Detail Name and Subject Refrence Number relationship (Economy, Business &amp; Finnance)</summary>
		<Restrict(False)> <TypeConverter(GetType(EnumConverterWithAttributes(Of EconomySubjectDetail)))> Public Enum EconomySubjectDetail As Integer
			''' <summary>Arable Farming</summary>
			<FieldDisplayName("")> ArableFarming = 04001001
			''' <summary>Fishing Industry</summary>
			<FieldDisplayName("")> FishingIndustry = 04001002
			''' <summary>Forestry &amp; Timber</summary>
			<FieldDisplayName("")> ForestryAndTimber = 04001003
			''' <summary>Livestock Farming</summary>
			<FieldDisplayName("")> LivestockFarming = 04001004
			''' <summary>Biotechnology</summary>
			<FieldDisplayName("")> Biotechnology = 04002001
			''' <summary>Fertilisers</summary>
			<FieldDisplayName("")> Fertilisers = 04002002
			''' <summary>Health &amp; Beauty products</summary>
			<FieldDisplayName("")> HealthAndBeautyProducts = 04002003
			''' <summary>Inorganic chemicals</summary>
			<FieldDisplayName("")> InorganicChemicals = 04002004
			''' <summary>Organic chemicals</summary>
			<FieldDisplayName("")> OrganicChemicals = 04002005
			''' <summary>Pharmaceuticals</summary>
			<FieldDisplayName("")> Pharmaceuticals = 04002006
			''' <summary>Synthetics &amp; Plastics</summary>
			<FieldDisplayName("")> SyntheticsAndPlastics = 04002007
			''' <summary>Hardware</summary>
			<FieldDisplayName("")> Hardware = 04003001
			''' <summary>Networking</summary>
			<FieldDisplayName("")> Networking = 04003002
			''' <summary>Satellite technology</summary>
			<FieldDisplayName("")> SatelliteTechnology = 04003003
			''' <summary>Semiconductors &amp; active components</summary>
			<FieldDisplayName("")> SemiconductorsAndActiveComponents = 04003004
			''' <summary>Software</summary>
			<FieldDisplayName("")> Software = 04003005
			''' <summary>Telecommunications Equipment</summary>
			<FieldDisplayName("")> TelecommunicationsEquipment = 04003006
			''' <summary>Telecommunications Services</summary>
			<FieldDisplayName("")> TelecommunicationsServices = 04003007
			''' <summary>Heavy construction</summary>
			<FieldDisplayName("")> HeavyConstruction = 04004001
			''' <summary>House building</summary>
			<FieldDisplayName("")> HouseBuilding = 04004002
			''' <summary>Real Estate</summary>
			<FieldDisplayName("")> RealEstate = 04004003
			''' <summary>Alternative energy</summary>
			<FieldDisplayName("")> AlternativeEnergy = 04005001
			''' <summary>Coal</summary>
			<FieldDisplayName("")> Coal = 04005002
			''' <summary>Oil &amp; Gas - Downstream activities</summary>
			<FieldDisplayName("")> OilAndGasDownstreamActivities = 04005003
			''' <summary>Oil &amp; Gas - Upstream activities</summary>
			<FieldDisplayName("")> OilAndGasUpstreamActivities = 04005004
			''' <summary>Nuclear power</summary>
			<FieldDisplayName("")> NuclearPower = 04005005
			''' <summary>Electricity Production &amp; Distribution</summary>
			<FieldDisplayName("")> ElectricityProductionAndDistribution = 04005006
			''' <summary>Waste Management &amp; Pollution Control</summary>
			<FieldDisplayName("")> WasteManagementAndPollutionControl = 04005007
			''' <summary>Water Supply</summary>
			<FieldDisplayName("")> WaterSupply = 04005008
			''' <summary>Accountancy &amp; Auditing</summary>
			<FieldDisplayName("")> AccountancyAndAuditing = 04006001
			''' <summary>Banking</summary>
			<FieldDisplayName("")> Banking = 04006002
			''' <summary>Consultancy Services</summary>
			<FieldDisplayName("")> ConsultancyServices = 04006003
			''' <summary>Employment Agencies</summary>
			<FieldDisplayName("")> EmploymentAgencies = 04006004
			''' <summary>Healthcare Providers</summary>
			<FieldDisplayName("")> HealthcareProviders = 04006005
			''' <summary>Insurance</summary>
			<FieldDisplayName("")> Insurance = 04006006
			''' <summary>Legal services</summary>
			<FieldDisplayName("")> LegalServices = 04006007
			''' <summary>Market research</summary>
			<FieldDisplayName("")> MarketResearch = 04006008
			''' <summary>Stock broking</summary>
			<FieldDisplayName("")> StockBroking = 04006009
			''' <summary>Clothing</summary>
			<FieldDisplayName("")> Clothing = 04007001
			''' <summary>Department stores</summary>
			<FieldDisplayName("")> DepartmentStores = 04007002
			''' <summary>Food</summary>
			<FieldDisplayName("")> FoodDistribution = 04007003
			''' <summary>Mail Order</summary>
			<FieldDisplayName("")> MailOrder = 04007004
			''' <summary>Retail</summary>
			<FieldDisplayName("")> Retail = 04007005
			''' <summary>Speciality stores</summary>
			<FieldDisplayName("")> SpecialityAtores = 04007006
			''' <summary>Wholesale</summary>
			<FieldDisplayName("")> Wholesale = 04007007
			''' <summary>Central Banks</summary>
			<FieldDisplayName("")> CentralBanks = 04008001
			''' <summary>Consumer Issues</summary>
			<FieldDisplayName("")> ConsumerIssues = 04008002
			''' <summary>Debt Markets</summary>
			<FieldDisplayName("")> DebtMarkets = 04008003
			''' <summary>Economic Indicators</summary>
			<FieldDisplayName("")> EconomicIndicators = 04008004
			''' <summary>Emerging Markets Debt</summary>
			<FieldDisplayName("")> EmergingMarketsDebt = 04008005
			''' <summary>Foreign Exchange Markets</summary>
			<FieldDisplayName("")> ForeignExchangeMarkets = 04008006
			''' <summary>Government Aid</summary>
			<FieldDisplayName("")> GovernmentAid = 04008007
			''' <summary>Government Debt</summary>
			<FieldDisplayName("")> GovernmentDebt = 04008008
			''' <summary>Interest Rates</summary>
			<FieldDisplayName("")> InterestRates = 04008009
			''' <summary>International Economic Institutions</summary>
			<FieldDisplayName("")> InternationalEconomicInstitutions = 04008010
			''' <summary>International Trade Issues</summary>
			<FieldDisplayName("")> InternationalTradeIssues = 04008011
			''' <summary>Loan Markets</summary>
			<FieldDisplayName("")> LoanMarkets = 04008012
			''' <summary>Energy</summary>
			<FieldDisplayName("")> Energy = 04009001
			''' <summary>Metals</summary>
			<FieldDisplayName("")> Metals = 04009002
			''' <summary>Securities</summary>
			<FieldDisplayName("")> Securities = 04009003
			''' <summary>Soft Commodities</summary>
			<FieldDisplayName("")> SoftCommodities = 04009004
			''' <summary>Advertising</summary>
			<FieldDisplayName("")> Advertising = 04010001
			''' <summary>Books</summary>
			<FieldDisplayName("")> Books = 04010002
			''' <summary>Cinema</summary>
			<FieldDisplayName("")> Cinema = 04010003
			''' <summary>News Agencies</summary>
			<FieldDisplayName("")> NewsAgencies = 04010004
			''' <summary>Newspaper &amp; Magazines</summary>
			<FieldDisplayName("")> NewspaperAndMagazines = 04010005
			''' <summary>Online</summary>
			<FieldDisplayName("")> Online = 04010006
			''' <summary>Public Relations</summary>
			<FieldDisplayName("")> PublicRelations = 04010007
			''' <summary>Radio</summary>
			<FieldDisplayName("")> Radio = 04010008
			''' <summary>Satellite &amp; Cable Services</summary>
			<FieldDisplayName("")> SatelliteAndCableServices = 04010009
			''' <summary>Television</summary>
			<FieldDisplayName("")> Television = 04010010
			''' <summary>Aerospace</summary>
			<FieldDisplayName("")> Aerospace = 04011001
			''' <summary>Automotive Equipment</summary>
			<FieldDisplayName("")> AutomotiveEquipment = 04011002
			''' <summary>Defence Equipment</summary>
			<FieldDisplayName("")> DefenceEquipment = 04011003
			''' <summary>Electrical Appliances</summary>
			<FieldDisplayName("")> ElectricalAppliances = 04011004
			''' <summary>Heavy engineering</summary>
			<FieldDisplayName("")> HeavyEngineering = 04011005
			''' <summary>Industrial components</summary>
			<FieldDisplayName("")> IndustrialComponents = 04011006
			''' <summary>Instrument engineering</summary>
			<FieldDisplayName("")> InstrumentEngineering = 04011007
			''' <summary>Shipbuilding</summary>
			<FieldDisplayName("")> Shipbuilding = 04011008
			''' <summary>Building materials</summary>
			<FieldDisplayName("")> BuildingMaterials = 04012001
			''' <summary>Gold &amp; Precious Materials</summary>
			<FieldDisplayName("")> GoldAndPreciousMaterials = 04012002
			''' <summary>Iron &amp; Steel</summary>
			<FieldDisplayName("")> IronAndSteel = 04012003
			''' <summary>Non ferrous metals</summary>
			<FieldDisplayName("")> NonFerrousMetals = 04012004
			''' <summary>Alcoholic Drinks</summary>
			<FieldDisplayName("")> AlcoholicDrinks = 04013001
			''' <summary>Food</summary>
			<FieldDisplayName("")> FoodIndustry = 04013002
			''' <summary>Furnishings &amp; Furniture</summary>
			<FieldDisplayName("")> FurnishingsAndFurniture = 04013003
			''' <summary>Paper &amp; packaging products</summary>
			<FieldDisplayName("")> PaperAndPackagingProducts = 04013004
			''' <summary>Rubber products</summary>
			<FieldDisplayName("")> Rubberproducts = 04013005
			''' <summary>Soft Drinks</summary>
			<FieldDisplayName("")> SoftDrinks = 04013006
			''' <summary>Textiles &amp; Clothing</summary>
			<FieldDisplayName("")> TextilesAndClothing = 04013007
			''' <summary>Tobacco</summary>
			<FieldDisplayName("")> Tobacco = 04013008
			''' <summary>Casinos &amp; Gambling</summary>
			<FieldDisplayName("")> CasinosAndGambling = 04014001
			''' <summary>Hotels &amp; accommodation</summary>
			<FieldDisplayName("")> HotelsAndAccommodation = 04014002
			''' <summary>Recreational &amp; Sports goods</summary>
			<FieldDisplayName("")> RecreationalAndSportsGoods = 04014003
			''' <summary>Restaurants &amp; catering</summary>
			<FieldDisplayName("")> RestaurantsAndCatering = 04014004
			''' <summary>Tour operators</summary>
			<FieldDisplayName("")> TourOperators = 04014005
			''' <summary>Air Transport</summary>
			<FieldDisplayName("")> AirTransport = 04015001
			''' <summary>Railway</summary>
			<FieldDisplayName("")> Railway = 04015002
			''' <summary>Road Transport</summary>
			<FieldDisplayName("")> RoadTransport = 04015003
			''' <summary>Waterway &amp; Maritime Transport</summary>
			<FieldDisplayName("")> WaterwayAndMaritimeTransport = 04015004
		End Enum
		''' <summary>Values for <see cref="EditorialUpdate"/></summary>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of EditorialUpdateValues)))> Public Enum EditorialUpdateValues As Byte
			''' <summary>Additional language. Signifies that the accompanying Record 2 DataSets repeat information from another object in a different natural language (as indicated by DataSet 2:135 - <see cref='LanguageIdentifier'/>).</summary>
			<FieldDisplayName("")> AdditionalLanguage = 1
		End Enum
		''' <summary>Registered file formats by IPTC and NAA</summary>
		<Restrict(True)> <CLSCompliant(False)> <TypeConverter(GetType(EnumConverterWithAttributes(Of FileFormats)))> Public Enum FileFormats As UShort
			''' <summary>No Object Data</summary>
			<FieldDisplayName("")> NoObjectData = 0
			''' <summary>IPTC-NAA Digital Newsphoto Parameter Record</summary>
			<FieldDisplayName("")> NewsphotoParameterRecord = 01
			''' <summary>IPTC7901 Recommended Message Format</summary>
			<FieldDisplayName("")> RecommendedMessageFormat = 02
			''' <summary>Tagged Image File Format (Adobe/Aldus Image data) (Recommended for image ObjectData Preview)</summary>
			<FieldDisplayName("")> TIFF = 03
			''' <summary>Illustrator (Adobe Graphics data)</summary>
			<FieldDisplayName("")> AdobeIllustrator = 04
			''' <summary>AppleSingle (Apple Computer Inc)</summary>
			<FieldDisplayName("")> AppleSingle = 05
			''' <summary>NAA 89-3 (ANPA 1312)</summary>
			<FieldDisplayName("")> NAA89_3 = 06
			''' <summary>MacBinary II</summary>
			<FieldDisplayName("")> MacBinary = 07
			''' <summary>IPTC Unstructured Character Oriented File Format (UCOFF)</summary>
			<FieldDisplayName("")> UCOFF = 08
			''' <summary>United Press International ANPA 1312 variant</summary>
			<FieldDisplayName("")> UnitedPressInternationalANPA1312 = 09
			''' <summary>United Press International Down-Load Message</summary>
			<FieldDisplayName("")> UnitedPressInternationalDownLoadMessage = 10
			''' <summary>¤ JPEG File Interchange (JFIF) (Recommended for image ObjectData Preview)</summary>
			<FieldDisplayName("")> JPEG = 11
			''' <summary>Photo-CD Image-Pac (Eastman Kodak)</summary>
			<FieldDisplayName("")> PhotoCDImagePac = 12
			''' <summary>¤ Microsoft Bit Mapped Graphics File [*.BMP] (Recommended for image ObjectData Preview)</summary>
			<FieldDisplayName("")> BMP = 13
			''' <summary>Digital Audio File [*.WAV] (Microsoft &amp; Creative Labs)</summary>
			<FieldDisplayName("")> WAV = 14
			''' <summary>Audio plus Moving Video [*.AVI] (Microsoft)</summary>
			<FieldDisplayName("")> AVI = 15
			''' <summary>PC DOS/Windows Executable Files [*.COM][*.EXE]</summary>
			<FieldDisplayName("")> EXE = 16
			''' <summary>Compressed Binary File [*.ZIP] (PKWare Inc)</summary>
			<FieldDisplayName("")> ZIP = 17
			''' <summary>Audio Interchange File Format AIFF (Apple Computer Inc)</summary>
			<FieldDisplayName("")> AIFF = 18
			''' <summary>RIFF Wave (Microsoft Corporation)</summary>
			<FieldDisplayName("")> RIFFWave = 19
			''' <summary>Freehand (Macromedia/Aldus)</summary>
			<FieldDisplayName("")> Freehand = 20
			''' <summary>Hypertext Markup Language "HTML" (The Internet Society)</summary>
			<FieldDisplayName("")> HTML = 21
			''' <summary>MPEG 2 Audio Layer 2 (Musicom), ISO/IEC</summary>
			<FieldDisplayName("")> MP2 = 22
			''' <summary>MPEG 2 Audio Layer 3, ISO/IEC</summary>
			<FieldDisplayName("")> MP3 = 23
			''' <summary>Portable Document File (*.PDF) Adobe</summary>
			<FieldDisplayName("")> PDF = 24
			''' <summary>News Industry Text Format (NITF)</summary>
			<FieldDisplayName("")> NITF = 25
			''' <summary>Tape Archive (*.TAR)</summary>
			<FieldDisplayName("")> TAR = 26
			''' <summary>Tidningarnas Telegrambyrå NITF version (TTNITF DTD)</summary>
			<FieldDisplayName("")> TTNITF_DTD = 27
			''' <summary>Ritzaus Bureau NITF version (RBNITF DTD)</summary>
			<FieldDisplayName("")> RBNITF_DTD = 28
			''' <summary>Corel Draw [*.CDR]</summary>
			<FieldDisplayName("")> CorelDraw = 29
		End Enum
		''' <summary>File format version registered for NAA and IPTC</summary>
		<Restrict(True)> <CLSCompliant(False)> <TypeConverter(GetType(EnumConverterWithAttributes(Of FileFormatVersions)))> Public Enum FileFormatVersions As UShort
			''' <summary>Version 1 for FileFormat <see cref="FileFormats.NoObjectData"/></summary>
			<FieldDisplayName("")> V0 = 0
			''' <summary>Version 1 for file fromat <see cref="FileFormats.NewsphotoParameterRecord"/> and <see cref="FileFormats.NAA89_3"/>, 5.0 for <see cref="FileFormats.TIFF"/>, 1.40 for <see cref="FileFormats.AdobeIllustrator"/>, 2 for <see cref="FileFormats.AppleSingle"/>, 1.02 for <see cref="FileFormats.JPEG"/> and 3.1 for <see cref="FileFormats.Freehand"/></summary>
			<FieldDisplayName("")> V1 = 1
			''' <summary>Version 2 for file format <see cref="FileFormats.NewsphotoParameterRecord"/>, 6.0 for <see cref="FileFormats.TIFF"/>, 4.0 for <see cref="FileFormats.Freehand"/> and 2.0 for <see cref="FileFormats.HTML"/></summary>
			<FieldDisplayName("")> V2 = 2
			''' <summary>Version 3 for file format <see cref="FileFormats.NewsphotoparameterRecord"/> and 5.0 for <see cref="FileFormats.Freehand"/></summary>
			<FieldDisplayName("")> V3 = 3
			''' <summary>Version 4 for file format <see cref="FileFormats.NewsphotoparameterRecord"/> and <see cref="FileFormats.RecommendedMessageFormat"/> and 5.5 for <see cref="FileFormats.Freehand"/></summary>
			<FieldDisplayName("")> V4 = 4
		End Enum
		''' <summary>Possible inage rotations in clockwise direction</summary>
		''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of ImageRotationValue)))> Public Enum ImageRotationValue As Byte
			''' <summary>no rotation</summary>
			<FieldDisplayName("")> None = 0
			''' <summary>90 degrees rotation</summary>
			<FieldDisplayName("")> Rot90 = 1
			''' <summary>180 degrees rotation</summary>
			<FieldDisplayName("")> Rot180 = 2
			''' <summary>270 degrees rotation</summary>
			<FieldDisplayName("")> Rot270 = 3
		End Enum
		''' <summary>Number of components in image and special meanings of some numbers</summary>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of ImageTypeComponents)))> Public Enum ImageTypeComponents As Byte
			''' <summary>Record 2 caption for specific image</summary>
			<FieldDisplayName("")> NoObjectData = 0
			''' <summary>Image has 1 component</summary>
			<FieldDisplayName("")> BW = 1
			''' <summary>Image has 2 components</summary>
			<FieldDisplayName("")> Component2 = 2
			''' <summary>Image has 3 components</summary>
			<FieldDisplayName("")> Component3 = 3
			''' <summary>Image has 4 components</summary>
			<FieldDisplayName("")> Component4 = 4
			''' <summary>the objectdata contains supplementary data to an image</summary>
			<FieldDisplayName("")> SuplementaryData = 9
		End Enum
		''' <summary>Object Attribute Number abd Object Name relationship</summary>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of ObjectAttributes)))> Public Enum ObjectAttributes As Byte
			''' <summary>Current</summary>
			<FieldDisplayName("")> Current = 01
			''' <summary>Analysis</summary>
			<FieldDisplayName("")> Analysis = 02
			''' <summary>Archive material</summary>
			<FieldDisplayName("")> ArchiveMaterial = 03
			''' <summary>Background</summary>
			<FieldDisplayName("")> Background = 04
			''' <summary>Feature</summary>
			<FieldDisplayName("")> Feature = 05
			''' <summary>Forecast</summary>
			<FieldDisplayName("")> Forecast = 06
			''' <summary>History</summary>
			<FieldDisplayName("")> History = 07
			''' <summary>Obituary</summary>
			<FieldDisplayName("")> Obituary = 08
			''' <summary>Opinion</summary>
			<FieldDisplayName("")> Opinion = 09
			''' <summary>Polls &amp; Surveys</summary>
			<FieldDisplayName("")> PollsAndSurveys = 10
			''' <summary>Profile</summary>
			<FieldDisplayName("")> Profile = 11
			''' <summary>Results Listings &amp; Tables</summary>
			<FieldDisplayName("")> ResultsListingsAndTables = 12
			''' <summary>Side bar &amp; Supporting information</summary>
			<FieldDisplayName("")> SideBarAndSupportingInformation = 13
			''' <summary>Summary</summary>
			<FieldDisplayName("")> Summary = 14
			''' <summary>Transcript &amp; Verbatim</summary>
			<FieldDisplayName("")> TranscriptAndVerbatim = 15
			''' <summary>Interview</summary>
			<FieldDisplayName("")> Interview = 16
			''' <summary>From the Scene</summary>
			<FieldDisplayName("")> FromTheScene = 17
			''' <summary>Retrospective</summary>
			<FieldDisplayName("")> Retrospective = 18
			''' <summary>Statistics</summary>
			<FieldDisplayName("")> Statistics = 19
			''' <summary>Update</summary>
			<FieldDisplayName("")> Update = 20
			''' <summary>Wrap-up</summary>
			<FieldDisplayName("")> WrapUp = 21
			''' <summary>Press Release</summary>
			<FieldDisplayName("")> PressRelease = 22
		End Enum
		''' <summary>Object Type Number and Object Type Name relationship</summary>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of ObjectTypes)))> Public Enum ObjectTypes As Byte
			''' <summary>News</summary>
			<FieldDisplayName("")> News = 1
			''' <summary>Data. Data in this context implies typically non narrative information, usually not eligible for journalistic intervention or modification. It also applies to information routed by the provider from a third party to the user. Examples are sports results, stock prices and agate.</summary>
			<FieldDisplayName("")> Data = 2
			''' <summary>Advisory</summary>
			<FieldDisplayName("")> Advisory = 3
		End Enum
		''' <summary>Possible qantisation methods</summary>
		''' <remarks>Other values are reserved for future use. For quantisation methods 0 and 1, ascending values correspond to increasing reflectance/transmittance.</remarks>
		''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of QuantisationMethodValue)))> Public Enum QuantisationMethodValue As Byte
			''' <summary>Linear reflectance/transmittance (The domain in which the relative reflectance/transmittance of an image is mapped linearly onto a finite scale of integers (CCITT T1).)</summary>
			<FieldDisplayName("")> LinearReflectanceTransmittance = 0
			''' <summary>Linear density (The domain in which the relative density of an image is mapped linearly onto a finite scale of integers.)</summary>
			<FieldDisplayName("")> LinearDensity = 1
			''' <summary>IPTC ref “B” (Defined by IPTC in 1985 and amended in January 1990 to suppress reference to “Pixel Density.”)</summary>
			''' <remarks>see Appendix H</remarks>
			<FieldDisplayName("")> IPTCRefB = 2
			''' <summary>Linear Dot Percent (The domain in which the relative dot percent of an image is mapped linearly onto a finite scale of integers.)</summary>
			<FieldDisplayName("")> LinearDotPercent = 3
			''' <summary>AP Domestic Analogue (This may be described mathematically as either [√(linear density)] or [√(log (reflectance/transmittance))].)</summary>
			<FieldDisplayName("")> ApDomesticAnalogue = 4
			''' <summary>Compression method specific (Defined within the compression method.)</summary>
			''' <remarks>Refer to Appendix A</remarks>
			<FieldDisplayName("")> CompressionMethodSpecific = 5
			''' <summary>Colour Space Specific (quantisation method is contained within the Colour Space definition for DataSet 3:64 values of 1,2,3,6 or 7.)</summary>
			<FieldDisplayName("")> ColourSpaceSpecific = 6
			''' <summary>Gamma Compensated (the domain in which the relative reflectance/transmittance is raised to the power of the inverse of gamma.)</summary>
			<FieldDisplayName("")> GammaCompensated = 7
		End Enum
		''' <summary>Enumerates possible spatial and temporal relations between pixels</summary>
		''' <remarks>Other values are reserved for future use.</remarks>
		''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of SamplingStructureType)))> Public Enum SamplingStructureType As Byte
			''' <summary>Orthogonal with the same relative sampling frequencies on each component.</summary>
			<FieldDisplayName("")> OrthogonalSame = 0
			''' <summary>Orthogonal with the sampling frequencies in the ratio of 4:2:2:(4) as described in Appendix G. This structure can only be used with the YUV(K) or LAB(K) colour spaces.</summary>
			<FieldDisplayName("")> OrthogonalRatio = 1
			''' <summary>defined within the compression process.</summary>
			<FieldDisplayName("")> Custom = 2
		End Enum
		''' <summary>Possible scanning directions</summary>
		''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of ScanningDirectionValue)))> Public Enum ScanningDirectionValue As Byte
			''' <summary>left to right, top to bottom.</summary>
			<FieldDisplayName("")> LeftRightTopBottom = 0
			''' <summary>right to left, top to bottom.</summary>
			<FieldDisplayName("")> RightLeftTopBottom = 1
			''' <summary>left to right, bottom to top.</summary>
			<FieldDisplayName("")> LeftRightBottomTop = 2
			''' <summary>right to left, bottom to top.</summary>
			<FieldDisplayName("")> RightLeftBottomTop = 3
			''' <summary>top to bottom, left to right.</summary>
			<FieldDisplayName("")> TopBottomLeftRight = 4
			''' <summary>bottom to top, left to right.</summary>
			<FieldDisplayName("")> BottomTopLeftRight = 5
			''' <summary>top to bottom, right to left.</summary>
			<FieldDisplayName("")> TopBottomRightLeft = 6
			''' <summary>bottom to top, right to left.</summary>
			<FieldDisplayName("")> BottomTopRightLeft = 7
		End Enum
		''' <summary>Subject Matter Name and Subject Reference Number relationship</summary>
		<Restrict(False)> <TypeConverter(GetType(EnumConverterWithAttributes(Of SubjectMatterNumbers)))> Public Enum SubjectMatterNumbers As Integer
			''' <summary>Archaeology</summary>
			<FieldDisplayName("")> Archaeology = 01001000
			''' <summary>Architecture</summary>
			<FieldDisplayName("")> Architecture = 01002000
			''' <summary>Bullfighting</summary>
			<FieldDisplayName("")> Bullfighting = 01003000
			''' <summary>Carnival</summary>
			<FieldDisplayName("")> Carnival = 01004000
			''' <summary>Cinema</summary>
			<FieldDisplayName("")> Cinema = 01005000
			''' <summary>Dance</summary>
			<FieldDisplayName("")> Dance = 01006000
			''' <summary>Fashion</summary>
			<FieldDisplayName("")> Fashion = 01007000
			''' <summary>Language</summary>
			<FieldDisplayName("")> Language = 01008000
			''' <summary>Libraries &amp; Museums</summary>
			<FieldDisplayName("")> LibrariesMuseums = 01009000
			''' <summary>Literature</summary>
			<FieldDisplayName("")> Literature = 01010000
			''' <summary>Music</summary>
			<FieldDisplayName("")> Music = 01011000
			''' <summary>Painting</summary>
			<FieldDisplayName("")> Painting = 01012000
			''' <summary>Photography</summary>
			<FieldDisplayName("")> Photography = 01013000
			''' <summary>Radio</summary>
			<FieldDisplayName("")> Radio = 01014000
			''' <summary>Sculpture</summary>
			<FieldDisplayName("")> Sculpture = 01015000
			''' <summary>Television</summary>
			<FieldDisplayName("")> Television = 01016000
			''' <summary>Theatre</summary>
			<FieldDisplayName("")> Theatre = 01017000
			''' <summary>Crime</summary>
			<FieldDisplayName("")> Crime = 02001000
			''' <summary>Judiciary</summary>
			<FieldDisplayName("")> Judiciary = 02002000
			''' <summary>Police</summary>
			<FieldDisplayName("")> Police = 02003000
			''' <summary>Punishment</summary>
			<FieldDisplayName("")> Punishment = 02004000
			''' <summary>Prison</summary>
			<FieldDisplayName("")> Prison = 02005000
			''' <summary>Drought</summary>
			<FieldDisplayName("")> Drought = 03001000
			''' <summary>Earthquake</summary>
			<FieldDisplayName("")> Earthquake = 03002000
			''' <summary>Famine</summary>
			<FieldDisplayName("")> Famine = 03003000
			''' <summary>Fire</summary>
			<FieldDisplayName("")> Fire = 03004000
			''' <summary>Flood</summary>
			<FieldDisplayName("")> Flood = 03005000
			''' <summary>Industrial accident</summary>
			<FieldDisplayName("")> IndustrialAccident = 03006000
			''' <summary>Meteorological disaster</summary>
			<FieldDisplayName("")> MeteorologicalDisaster = 03007000
			''' <summary>Nuclear accident</summary>
			<FieldDisplayName("")> NuclearAccident = 03008000
			''' <summary>Pollution</summary>
			<FieldDisplayName("")> Pollution = 03009000
			''' <summary>Transport accident</summary>
			<FieldDisplayName("")> TransportAccident = 03010000
			''' <summary>Volcanic eruption</summary>
			<FieldDisplayName("")> VolcanicEruption = 03011000
			''' <summary>Agriculture</summary>
			<FieldDisplayName("")> Agriculture = 04001000
			''' <summary>Chemicals</summary>
			<FieldDisplayName("")> Chemicals = 04002000
			''' <summary>Computing &amp; Information Technology</summary>
			<FieldDisplayName("")> ComputingAndInformationTechnology = 04003000
			''' <summary>Construction &amp; Property</summary>
			<FieldDisplayName("")> ConstructionAndProperty = 04004000
			''' <summary>Energy &amp; Resources</summary>
			<FieldDisplayName("")> EnergyAndResources = 04005000
			''' <summary>Financial &amp; Business Services</summary>
			<FieldDisplayName("")> FinancialAndBusinessServices = 04006000
			''' <summary>Goods Distribution</summary>
			<FieldDisplayName("")> GoodsDistribution = 04007000
			''' <summary>Macro Economics</summary>
			<FieldDisplayName("")> MacroEconomics = 04008000
			''' <summary>Markets</summary>
			<FieldDisplayName("")> Markets = 04009000
			''' <summary>Media</summary>
			<FieldDisplayName("")> Media = 04010000
			''' <summary>Metal Goods &amp; Engineering</summary>
			<FieldDisplayName("")> MetalGoodsAndEngineering = 04011000
			''' <summary>Metals &amp; Minerals</summary>
			<FieldDisplayName("")> MetalsAndMinerals = 04012000
			''' <summary>Process Industries</summary>
			<FieldDisplayName("")> ProcessIndustries = 04013000
			''' <summary>Tourism &amp; Leisure</summary>
			<FieldDisplayName("")> TourismAndLeisure = 04014000
			''' <summary>Transport</summary>
			<FieldDisplayName("")> Transport = 04015000
			''' <summary>Adult Education</summary>
			<FieldDisplayName("")> AdultEducation = 05001000
			''' <summary>Further Education</summary>
			<FieldDisplayName("")> FurtherEducation = 05002000
			''' <summary>Parent Organisations</summary>
			<FieldDisplayName("")> ParentOrganisations = 05003000
			''' <summary>Preschooling</summary>
			<FieldDisplayName("")> Preschooling = 05004000
			''' <summary>Schools</summary>
			<FieldDisplayName("")> Schools = 05005000
			''' <summary>Teachers Unions</summary>
			<FieldDisplayName("")> TeachersUnions = 05006000
			''' <summary>University</summary>
			<FieldDisplayName("")> University = 05007000
			''' <summary>Alternative Energy</summary>
			<FieldDisplayName("")> AlternativeEnergy = 06001000
			''' <summary>Conservation</summary>
			<FieldDisplayName("")> Conservation = 06002000
			''' <summary>Energy Savings</summary>
			<FieldDisplayName("")> EnergySavings = 06003000
			''' <summary>Environmental Politics</summary>
			<FieldDisplayName("")> EnvironmentalPolitics = 06004000
			''' <summary>Environmental pollution</summary>
			<FieldDisplayName("")> EnvironmentalPollution = 06005000
			''' <summary>Natural resources</summary>
			<FieldDisplayName("")> NaturalResources = 06006000
			''' <summary>Nature</summary>
			<FieldDisplayName("")> Nature = 06007000
			''' <summary>Population</summary>
			<FieldDisplayName("")> Population = 06008000
			''' <summary>Waste</summary>
			<FieldDisplayName("")> Waste = 06009000
			''' <summary>Water Supplies</summary>
			<FieldDisplayName("")> WaterSupplies = 06010000
			''' <summary>Diseases</summary>
			<FieldDisplayName("")> Diseases = 07001000
			''' <summary>Epidemic &amp; Plague</summary>
			<FieldDisplayName("")> EpidemicAndPlague = 07002000
			''' <summary>Health treatment</summary>
			<FieldDisplayName("")> HealthTreatment = 07003000
			''' <summary>Health organisations</summary>
			<FieldDisplayName("")> HealthOrganisations = 07004000
			''' <summary>Medical research</summary>
			<FieldDisplayName("")> MedicalResearch = 07005000
			''' <summary>Medical staff</summary>
			<FieldDisplayName("")> MedicalStaff = 07006000
			''' <summary>Medicines</summary>
			<FieldDisplayName("")> Medicines = 07007000
			''' <summary>Preventative medicine</summary>
			<FieldDisplayName("")> PreventativeMedicine = 07008000
			''' <summary>Animals</summary>
			<FieldDisplayName("")> Animals = 08001000
			''' <summary>Curiosities</summary>
			<FieldDisplayName("")> Curiosities = 08002000
			''' <summary>People</summary>
			<FieldDisplayName("")> People = 08003000
			''' <summary>Apprentices</summary>
			<FieldDisplayName("")> Apprentices = 09001000
			''' <summary>Collective contracts</summary>
			<FieldDisplayName("")> CollectiveContracts = 09002000
			''' <summary>Employment</summary>
			<FieldDisplayName("")> Employment = 09003000
			''' <summary>Labour dispute</summary>
			<FieldDisplayName("")> LabourDispute = 09004000
			''' <summary>Labour legislation</summary>
			<FieldDisplayName("")> LabourLegislation = 09005000
			''' <summary>Retirement</summary>
			<FieldDisplayName("")> Retirement = 09006000
			''' <summary>Retraining</summary>
			<FieldDisplayName("")> Retraining = 09007000
			''' <summary>Strike</summary>
			<FieldDisplayName("")> Strike = 09008000
			''' <summary>Unemployment</summary>
			<FieldDisplayName("")> Unemployment = 09009000
			''' <summary>Unions</summary>
			<FieldDisplayName("")> Unions = 09010000
			''' <summary>Wages &amp; Pensions</summary>
			<FieldDisplayName("")> WagesAndPensions = 09011000
			''' <summary>Work Relations</summary>
			<FieldDisplayName("")> WorkRelations = 09012000
			''' <summary>Games</summary>
			<FieldDisplayName("")> Games = 10001000
			''' <summary>Gaming &amp; Lotteries</summary>
			<FieldDisplayName("")> GamingAndLotteries = 10002000
			''' <summary>Gastronomy</summary>
			<FieldDisplayName("")> Gastronomy = 10003000
			''' <summary>Hobbies</summary>
			<FieldDisplayName("")> Hobbies = 10004000
			''' <summary>Holidays or vacations</summary>
			<FieldDisplayName("")> HolidaysOrVacations = 10005000
			''' <summary>Tourism</summary>
			<FieldDisplayName("")> Tourism = 10006000
			''' <summary>Defence</summary>
			<FieldDisplayName("")> Defence = 11001000
			''' <summary>Diplomacy</summary>
			<FieldDisplayName("")> Diplomacy = 11002000
			''' <summary>Elections</summary>
			<FieldDisplayName("")> Elections = 11003000
			''' <summary>Espionage &amp; Intelligence</summary>
			<FieldDisplayName("")> EspionageAndIntelligence = 11004000
			''' <summary>Foreign Aid</summary>
			<FieldDisplayName("")> ForeignAid = 11005000
			''' <summary>Government</summary>
			<FieldDisplayName("")> Government = 11006000
			''' <summary>Human Rights</summary>
			<FieldDisplayName("")> HumanRights = 11007000
			''' <summary>Local authorities</summary>
			<FieldDisplayName("")> LocalAuthorities = 11008000
			''' <summary>Parliament</summary>
			<FieldDisplayName("")> Parliament = 11009000
			''' <summary>Parties</summary>
			<FieldDisplayName("")> Parties = 11010000
			''' <summary>Refugees</summary>
			<FieldDisplayName("")> Refugees = 11011000
			''' <summary>Regional authorities</summary>
			<FieldDisplayName("")> RegionalAuthorities = 11012000
			''' <summary>State Budget</summary>
			<FieldDisplayName("")> StateBudget = 11013000
			''' <summary>Treaties &amp; Organisations</summary>
			<FieldDisplayName("")> TreatiesAndOrganisations = 11014000
			''' <summary>Cults &amp; sects</summary>
			<FieldDisplayName("")> CultsAndSects = 12001000
			''' <summary>Faith</summary>
			<FieldDisplayName("")> Faith = 12002000
			''' <summary>Free masonry</summary>
			<FieldDisplayName("")> FreeMasonry = 12003000
			''' <summary>Religious institutions</summary>
			<FieldDisplayName("")> ReligiousInstitutions = 12004000
			''' <summary>Applied Sciences</summary>
			<FieldDisplayName("")> AppliedSciences = 13001000
			''' <summary>Engineering</summary>
			<FieldDisplayName("")> Engineering = 13002000
			''' <summary>Human Sciences</summary>
			<FieldDisplayName("")> HumanSciences = 13003000
			''' <summary>Natural Sciences</summary>
			<FieldDisplayName("")> NaturalSciences = 13004000
			''' <summary>Philosophical Sciences</summary>
			<FieldDisplayName("")> PhilosophicalSciences = 13005000
			''' <summary>Research</summary>
			<FieldDisplayName("")> Research = 13006000
			''' <summary>Scientific exploration</summary>
			<FieldDisplayName("")> ScientificExploration = 13007000
			''' <summary>Space programmes</summary>
			<FieldDisplayName("")> SpaceProgrammes = 13008000
			''' <summary>Addiction</summary>
			<FieldDisplayName("")> Addiction = 14001000
			''' <summary>Charity</summary>
			<FieldDisplayName("")> Charity = 14002000
			''' <summary>Demographics</summary>
			<FieldDisplayName("")> Demographics = 14003000
			''' <summary>Disabled</summary>
			<FieldDisplayName("")> Disabled = 14004000
			''' <summary>Euthanasia</summary>
			<FieldDisplayName("")> Euthanasia = 14005000
			''' <summary>Family</summary>
			<FieldDisplayName("")> Family = 14006000
			''' <summary>Family planning</summary>
			<FieldDisplayName("")> FamilyPlanning = 14007000
			''' <summary>Health insurance</summary>
			<FieldDisplayName("")> HealthInsurance = 14008000
			''' <summary>Homelessness</summary>
			<FieldDisplayName("")> Homelessness = 14009000
			''' <summary>Minority groups</summary>
			<FieldDisplayName("")> MinorityGroups = 14010000
			''' <summary>Pornography</summary>
			<FieldDisplayName("")> Pornography = 14011000
			''' <summary>Poverty</summary>
			<FieldDisplayName("")> Poverty = 14012000
			''' <summary>Prostitution</summary>
			<FieldDisplayName("")> Prostitution = 14013000
			''' <summary>Racism</summary>
			<FieldDisplayName("")> Racism = 14014000
			''' <summary>Welfare</summary>
			<FieldDisplayName("")> Welfare = 14015000
			''' <summary>Aero and Aviation Sports</summary>
			<FieldDisplayName("")> Aero = 15001000
			''' <summary>Alpine Skiing</summary>
			<FieldDisplayName("")> AlpineSkiing = 15002000
			''' <summary>American Football</summary>
			<FieldDisplayName("")> AmericanFootball = 15003000
			''' <summary>Archery</summary>
			<FieldDisplayName("")> Archery = 15004000
			''' <summary>Athletics, Track &amp; Field</summary>
			<FieldDisplayName("")> AthleticsTrackAndField = 15005000
			''' <summary>Badminton</summary>
			<FieldDisplayName("")> Badminton = 15006000
			''' <summary>Baseball</summary>
			<FieldDisplayName("")> Baseball = 15007000
			''' <summary>Basketball</summary>
			<FieldDisplayName("")> Basketball = 15008000
			''' <summary>Biathlon</summary>
			<FieldDisplayName("")> Biathlon = 15009000
			''' <summary>Billiards, Snooker and Pool</summary>
			<FieldDisplayName("")> BilliardsSnookerPool = 15010000
			''' <summary>Bobsleigh</summary>
			<FieldDisplayName("")> Bobsleigh = 15011000
			''' <summary>Bowling</summary>
			<FieldDisplayName("")> Bowling = 15012000
			''' <summary>Bowls &amp; Petanque</summary>
			<FieldDisplayName("")> BowlsAndPetanque = 15013000
			''' <summary>Boxing</summary>
			<FieldDisplayName("")> Boxing = 15014000
			''' <summary>Canoeing &amp; Kayaking</summary>
			<FieldDisplayName("")> CanoeingAndKayaking = 15015000
			''' <summary>Climbing</summary>
			<FieldDisplayName("")> Climbing = 15016000
			''' <summary>Cricket</summary>
			<FieldDisplayName("")> Cricket = 15017000
			''' <summary>Curling</summary>
			<FieldDisplayName("")> Curling = 15018000
			''' <summary>Cycling</summary>
			<FieldDisplayName("")> Cycling = 15019000
			''' <summary>Dancing</summary>
			<FieldDisplayName("")> Dancing = 15020000
			''' <summary>Diving</summary>
			<FieldDisplayName("")> Diving = 15021000
			''' <summary>Equestrian</summary>
			<FieldDisplayName("")> Equestrian = 15022000
			''' <summary>Fencing</summary>
			<FieldDisplayName("")> Fencing = 15023000
			''' <summary>Field Hockey</summary>
			<FieldDisplayName("")> FieldHockey = 15024000
			''' <summary>Figure Skating</summary>
			<FieldDisplayName("")> FigureSkating = 15025000
			''' <summary>Freestyle Skiing</summary>
			<FieldDisplayName("")> FreestyleSkiing = 15026000
			''' <summary>Golf</summary>
			<FieldDisplayName("")> Golf = 15027000
			''' <summary>Gymnastics</summary>
			<FieldDisplayName("")> Gymnastics = 15028000
			''' <summary>Handball (Team)</summary>
			<FieldDisplayName("")> Handball = 15029000
			''' <summary>Horse Racing, Harness Racing</summary>
			<FieldDisplayName("")> Horse = 15030000
			''' <summary>Ice Hockey</summary>
			<FieldDisplayName("")> IceHockey = 15031000
			''' <summary>Jai Alai (Pelota)</summary>
			<FieldDisplayName("")> JaiAlai = 15032000
			''' <summary>Judo</summary>
			<FieldDisplayName("")> Judo = 15033000
			''' <summary>Karate</summary>
			<FieldDisplayName("")> Karate = 15034000
			''' <summary>Lacrosse</summary>
			<FieldDisplayName("")> Lacrosse = 15035000
			''' <summary>Luge</summary>
			<FieldDisplayName("")> Luge = 15036000
			''' <summary>Marathon</summary>
			<FieldDisplayName("")> Marathon = 15037000
			''' <summary>Modern Pentathlon</summary>
			<FieldDisplayName("")> ModernPentathlon = 15038000
			''' <summary>Motor Racing</summary>
			<FieldDisplayName("")> MotorRacing = 15039000
			''' <summary>Motor Rallying</summary>
			<FieldDisplayName("")> MotorRallying = 15040000
			''' <summary>Motorcycling</summary>
			<FieldDisplayName("")> Motorcycling = 15041000
			''' <summary>Netball</summary>
			<FieldDisplayName("")> Netball = 15042000
			''' <summary>Nordic Skiing</summary>
			<FieldDisplayName("")> NordicSkiing = 15043000
			''' <summary>Orienteering</summary>
			<FieldDisplayName("")> Orienteering = 15044000
			''' <summary>Polo</summary>
			<FieldDisplayName("")> Polo = 15045000
			''' <summary>Power Boating</summary>
			<FieldDisplayName("")> PowerBoating = 15046000
			''' <summary>Rowing</summary>
			<FieldDisplayName("")> Rowing = 15047000
			''' <summary>Rugby League</summary>
			<FieldDisplayName("")> RugbyLeague = 15048000
			''' <summary>Rugby Union</summary>
			<FieldDisplayName("")> RugbyUnion = 15049000
			''' <summary>Sailing</summary>
			<FieldDisplayName("")> Sailing = 15050000
			''' <summary>Shooting</summary>
			<FieldDisplayName("")> Shooting = 15051000
			''' <summary>Ski Jumping</summary>
			<FieldDisplayName("")> SkiJumping = 15052000
			''' <summary>Snow Boarding</summary>
			<FieldDisplayName("")> SnowBoarding = 15053000
			''' <summary>Soccer</summary>
			<FieldDisplayName("")> Soccer = 15054000
			''' <summary>Softball</summary>
			<FieldDisplayName("")> Softball = 15055000
			''' <summary>Speed Skating</summary>
			<FieldDisplayName("")> SpeedSkating = 15056000
			''' <summary>Speedway</summary>
			<FieldDisplayName("")> Speedway = 15057000
			''' <summary>Sports Organisations</summary>
			<FieldDisplayName("")> SportsOrganisations = 15058000
			''' <summary>Squash</summary>
			<FieldDisplayName("")> Squash = 15059000
			''' <summary>Sumo Wrestling</summary>
			<FieldDisplayName("")> SumoWrestling = 15060000
			''' <summary>Surfing</summary>
			<FieldDisplayName("")> Surfing = 15061000
			''' <summary>Swimming</summary>
			<FieldDisplayName("")> Swimming = 15062000
			''' <summary>Table Tennis</summary>
			<FieldDisplayName("")> TableTennis = 15063000
			''' <summary>Taekwon-Do</summary>
			<FieldDisplayName("")> TaekwonDo = 15064000
			''' <summary>Tennis</summary>
			<FieldDisplayName("")> Tennis = 15065000
			''' <summary>Triathlon</summary>
			<FieldDisplayName("")> Triathlon = 15066000
			''' <summary>Volleyball</summary>
			<FieldDisplayName("")> Volleyball = 15067000
			''' <summary>Water Polo</summary>
			<FieldDisplayName("")> WaterPolo = 15068000
			''' <summary>Water Skiing</summary>
			<FieldDisplayName("")> WaterSkiing = 15069000
			''' <summary>Weightlifting</summary>
			<FieldDisplayName("")> Weightlifting = 15070000
			''' <summary>Windsurfing</summary>
			<FieldDisplayName("")> Windsurfing = 15071000
			''' <summary>Wrestling</summary>
			<FieldDisplayName("")> Wrestling = 15072000
			''' <summary>Acts of terror</summary>
			<FieldDisplayName("")> ActsOfTerror = 16001000
			''' <summary>Armed conflict</summary>
			<FieldDisplayName("")> ArmedConflict = 16002000
			''' <summary>Civil unrest</summary>
			<FieldDisplayName("")> CivilUnrest = 16003000
			''' <summary>Coup d'Etat</summary>
			<FieldDisplayName("")> CoupDEtat = 16004000
			''' <summary>Guerrilla activities</summary>
			<FieldDisplayName("")> GuerrillaActivities = 16005000
			''' <summary>Massacre</summary>
			<FieldDisplayName("")> Massacre = 16006000
			''' <summary>Riots</summary>
			<FieldDisplayName("")> Riots = 16007000
			''' <summary>Violent demonstrations</summary>
			<FieldDisplayName("")> ViolentDemonstrations = 16008000
			''' <summary>War</summary>
			<FieldDisplayName("")> War = 16009000
			''' <summary>Forecasts</summary>
			<FieldDisplayName("")> Forecasts = 17001000
			''' <summary>Global change</summary>
			<FieldDisplayName("")> GlobalChange = 17002000
			''' <summary>Reports</summary>
			<FieldDisplayName("")> Reports = 17003000
			''' <summary>Statistics</summary>
			<FieldDisplayName("")> Statistics = 17004000
			''' <summary>Warnings</summary>
			<FieldDisplayName("")> Warnings = 17005000
		End Enum
		''' <summary>Subject Reference Number and Subject Name relationship (version IPTC/1)</summary>
		<Restrict(False)> <TypeConverter(GetType(EnumConverterWithAttributes(Of SubjectReferenceNumbers)))> Public Enum SubjectReferenceNumbers As Integer
			''' <summary>Matters pertaining to the advancement and refinement of the human mind, of interests, skills, tastes and emotions</summary>
			<FieldDisplayName("")> ArtsCultureEntertainment = 01000000
			''' <summary>Establishment and/or statement of the rules of behaviour in society, the enforcement of these rules, breaches of the rules and the punishment of offenders. Organisations and bodies involved in these activities.</summary>
			<FieldDisplayName("")> CrimeLawJustice = 02000000
			''' <summary>Man made and natural events resulting in loss of life or injury to living creatures and/or damage to inanimate objects or property.</summary>
			<FieldDisplayName("")> DisastersAccidents = 03000000
			''' <summary>All matters concerning the planning, production and exchange of wealth.</summary>
			<FieldDisplayName("")> EconomyBusinessFinance = 04000000
			''' <summary>All aspects of furthering knowledge of human individuals from birth to death.</summary>
			<FieldDisplayName("")> Education = 05000000
			''' <summary>All aspects of protection, damage, and condition of the ecosystem of the planet earth and its surroundings.</summary>
			<FieldDisplayName("")> EnvironmentalIssues = 06000000
			''' <summary>All aspects pertaining to the physical and mental welfare of human beings.</summary>
			<FieldDisplayName("")> Health = 07000000
			''' <summary>Lighter items about individuals, groups, animals or objects.</summary>
			<FieldDisplayName("")> HumanInterest = 08000000
			''' <summary>Social aspects, organisations, rules and conditions affecting the employment of human effort for the generation of wealth or provision of services and the economic support of the unemployed.</summary>
			<FieldDisplayName("")> Labour = 09000000
			''' <summary>Activities undertaken for pleasure, relaxation or recreation outside paid employment, including eating and travel.</summary>
			<FieldDisplayName("")> LifestyleAndLeisure = 10000000
			''' <summary>Local, regional, national and international exercise of power, or struggle for power, and the relationships between governing bodies and states.</summary>
			<FieldDisplayName("")> Politics = 11000000
			''' <summary>All aspects of human existence involving theology, philosophy, ethics and spirituality.</summary>
			<FieldDisplayName("")> ReligionBelief = 12000000
			''' <summary>All aspects pertaining to human understanding of nature and the physical world and the development and application of this knowledge</summary>
			<FieldDisplayName("")> ScienceTechnology = 13000000
			''' <summary>Aspects of the behaviour of humans affecting the quality of life.</summary>
			<FieldDisplayName("")> SocialIssues = 14000000
			''' <summary>Competitive exercise involving physical effort. Organisations and bodies involved in these activities.</summary>
			<FieldDisplayName("")> Sport = 15000000
			''' <summary>Acts of socially or politically motivated protest and/or violence.</summary>
			<FieldDisplayName("")> UnrestConflictsWar = 16000000
			''' <summary>The study, reporting and predic meteorological phenomena.</summary>
			<FieldDisplayName("")> Weather = 17000000
		End Enum
		''' <summary>Numbers indicating image content</summary>
		''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
		<Restrict(True)> <TypeConverter(GetType(EnumConverterWithAttributes(Of SupplementTypeValue)))> Public Enum SupplementTypeValue As Byte
			''' <summary>Value is used if the first octet of DataSet 2:130 (<see cref='ImageType'/>) &amp;lt;> 9 (<see cref='ImageTypeComponents.SuplementaryData'/>)</summary>
			<FieldDisplayName("")> Unknown = 0
			''' <summary>Reduced resolution image.</summary>
			<FieldDisplayName("")> ReducedResolutionImage = 1
			''' <summary>Logo</summary>
			<FieldDisplayName("")> Logo = 2
			''' <summary>Rasterized caption.</summary>
			<FieldDisplayName("")> RasterizedCaption = 3
		End Enum
	End Class
#End Region
#Region "String enums"
	Partial Class Iptc
		''' <summary>The exact type of audio contained in the current objectdata.</summary>
		<Restrict(True)> Public Enum AudioDataType
			''' <summary>Actuality</summary>
			<FieldDisplayName("")> <XmlEnum("A")> Actuality
			''' <summary>Question and answer session</summary>
			<FieldDisplayName("")> <XmlEnum("C")> QuestionAndAnswer
			''' <summary>Music, transmitted by itself</summary>
			<FieldDisplayName("")> <XmlEnum("M")> Music
			''' <summary>Response to a question</summary>
			<FieldDisplayName("")> <XmlEnum("Q")> Response
			''' <summary>Raw sound</summary>
			<FieldDisplayName("")> <XmlEnum("R")> RawSound
			''' <summary>Scener</summary>
			<FieldDisplayName("")> <XmlEnum("S")> Scener
			''' <summary>Text only</summary>
			<FieldDisplayName("")> <XmlEnum("T")> TextOnly
			''' <summary>Voicer</summary>
			<FieldDisplayName("")> <XmlEnum("V")> Voicer
			''' <summary>Wrap</summary>
			<FieldDisplayName("")> <XmlEnum("W")> Wrap
		End Enum
		''' <summary>Exact content of the current objectdata in terms of colour composition.</summary>
		<Restrict(True)> Public Enum ImageTypeContents
			''' <summary>Monochrome</summary>
			<FieldDisplayName("")> <XmlEnum("W")> Monochrome
			''' <summary>Yellow component</summary>
			<FieldDisplayName("")> <XmlEnum("Y")> Yellow
			''' <summary>Magenta component</summary>
			<FieldDisplayName("")> <XmlEnum("M")> Magenta
			''' <summary>Cyan component</summary>
			<FieldDisplayName("")> <XmlEnum("C")> Cyan
			''' <summary>Black component</summary>
			<FieldDisplayName("")> <XmlEnum("K")> Black
			''' <summary>Red component</summary>
			<FieldDisplayName("")> <XmlEnum("R")> Red
			''' <summary>Green component</summary>
			<FieldDisplayName("")> <XmlEnum("G")> Green
			''' <summary>Blue component</summary>
			<FieldDisplayName("")> <XmlEnum("B")> Blue
			''' <summary>Text only</summary>
			<FieldDisplayName("")> <XmlEnum("T")> Text
			''' <summary>Full colour composite, frame sequential</summary>
			<FieldDisplayName("")> <XmlEnum("F")> FrameSequential
			''' <summary>Full colour composite, line sequential</summary>
			<FieldDisplayName("")> <XmlEnum("L")> LineSequential
			''' <summary>Full colour composite, pixel sequential</summary>
			<FieldDisplayName("")> <XmlEnum("P")> PixesSequential
			''' <summary>Full colour composite, special sequential</summary>
			<FieldDisplayName("")> <XmlEnum("S")> SpecialSequential
		End Enum
		''' <summary>Information Providers Reference</summary>
		<Restrict(False)> Public Enum InformationProviders
			''' <summary>Agence France Presse</summary>
			<FieldDisplayName("")> <XmlEnum("AFP")> AFP
			''' <summary>Associated Press</summary>
			<FieldDisplayName("")> <XmlEnum("AP")> AP
			''' <summary>Associated Press</summary>
			<FieldDisplayName("")> <XmlEnum("APD")> APD
			''' <summary>Associated Press</summary>
			<FieldDisplayName("")> <XmlEnum("APE")> APE
			''' <summary>Associated Press</summary>
			<FieldDisplayName("")> <XmlEnum("APF")> APF
			''' <summary>Associated Press</summary>
			<FieldDisplayName("")> <XmlEnum("APS")> APS
			''' <summary>Canadian Press</summary>
			<FieldDisplayName("")> <XmlEnum("BN")> BN
			''' <summary>Canadian Press</summary>
			<FieldDisplayName("")> <XmlEnum("CP")> CP
			''' <summary>Czech News Agency</summary>
			<FieldDisplayName("")> <XmlEnum("CTK")> CTK
			''' <summary>Deutsche Presse-Agentur GmbH</summary>
			<FieldDisplayName("")> <XmlEnum("dpa")> dpa
			''' <summary>Croatian News Agency</summary>
			<FieldDisplayName("")> <XmlEnum("HNA")> HNA
			''' <summary>International Press Telecommunications Council</summary>
			<FieldDisplayName("")> <XmlEnum("IPTC")> IPTC
			''' <summary>Magyar Távirati Iroda / Hungarian News Agency</summary>
			<FieldDisplayName("")> <XmlEnum("MTI")> MTI
			''' <summary>Canadian Press</summary>
			<FieldDisplayName("")> <XmlEnum("PC")> PC
			''' <summary>Canadian Press</summary>
			<FieldDisplayName("")> <XmlEnum("PN")> PN
			''' <summary>Reuters</summary>
			<FieldDisplayName("")> <XmlEnum("REUTERS")> REUTERS
			''' <summary>Slovenska Tiskovna Agencija</summary>
			<FieldDisplayName("")> <XmlEnum("STA")> STA
			''' <summary>Tidningarnas Telegrambyrå</summary>
			<FieldDisplayName("")> <XmlEnum("TT")> TT
			''' <summary>United Press International</summary>
			<FieldDisplayName("")> <XmlEnum("UP")> UP
			''' <summary>United Press International</summary>
			<FieldDisplayName("")> <XmlEnum("UPI")> UPI
		End Enum
		''' <summary>ISO 3166-1 alpha-3 codes used by <see cref="ContentLocationCode"/> with addition of some spacial codes used there.</summary>
		''' <remarks>Reserved code elements are codes which, while not ISO 3166-1 codes, are in use for some applications in conjunction with the ISO 3166 codes. The ISO 3166/MA therefore reserves them, so that they are not used for new official ISO 3166 codes, thereby creating conflicts between the standard and those applications.</remarks>
		<Restrict(False)> Public Enum ISO3166
			''' <summary>Aruba</summary>
			<FieldDisplayName("")> <XmlEnum("ABW")> Aruba
			''' <summary>Afghanistan</summary>
			<FieldDisplayName("")> <XmlEnum("AFG")> Afghanistan
			''' <summary>Angola</summary>
			<FieldDisplayName("")> <XmlEnum("AGO")> Angola
			''' <summary>Anguilla</summary>
			<FieldDisplayName("")> <XmlEnum("AIA")> Anguilla
			''' <summary>Åland Islands</summary>
			<FieldDisplayName("")> <XmlEnum("ALA")> Åland
			''' <summary>Albania</summary>
			<FieldDisplayName("")> <XmlEnum("ALB")> Albania
			''' <summary>Andorra</summary>
			<FieldDisplayName("")> <XmlEnum("AND")> Andorra
			''' <summary>Netherlands Antilles</summary>
			<FieldDisplayName("")> <XmlEnum("ANT")> NetherlandsAntilles
			''' <summary>United Arab Emirates</summary>
			<FieldDisplayName("")> <XmlEnum("ARE")> UnitedArabEmirates
			''' <summary>Argentina</summary>
			<FieldDisplayName("")> <XmlEnum("ARG")> Argentina
			''' <summary>Armenia</summary>
			<FieldDisplayName("")> <XmlEnum("ARM")> Armenia
			''' <summary>American Samoa</summary>
			<FieldDisplayName("")> <XmlEnum("ASM")> AmericanSamoa
			''' <summary>Antarctica</summary>
			<FieldDisplayName("")> <XmlEnum("ATA")> Antarctica
			''' <summary>French Southern Territories</summary>
			<FieldDisplayName("")> <XmlEnum("ATF")> FrenchSouthernTerritories
			''' <summary>Antigua and Barbuda</summary>
			<FieldDisplayName("")> <XmlEnum("ATG")> AntiguaAndBarbuda
			''' <summary>Australia</summary>
			<FieldDisplayName("")> <XmlEnum("AUS")> Australia
			''' <summary>Austria</summary>
			<FieldDisplayName("")> <XmlEnum("AUT")> Austria
			''' <summary>Azerbaijan</summary>
			<FieldDisplayName("")> <XmlEnum("AZE")> Azerbaijan
			''' <summary>Burundi</summary>
			<FieldDisplayName("")> <XmlEnum("BDI")> Burundi
			''' <summary>Belgium</summary>
			<FieldDisplayName("")> <XmlEnum("BEL")> Belgium
			''' <summary>Benin</summary>
			<FieldDisplayName("")> <XmlEnum("BEN")> Benin
			''' <summary>Burkina Faso</summary>
			<FieldDisplayName("")> <XmlEnum("BFA")> BurkinaFaso
			''' <summary>Bangladesh</summary>
			<FieldDisplayName("")> <XmlEnum("BGD")> Bangladesh
			''' <summary>Bulgaria</summary>
			<FieldDisplayName("")> <XmlEnum("BGR")> Bulgaria
			''' <summary>Bahrain</summary>
			<FieldDisplayName("")> <XmlEnum("BHR")> Bahrain
			''' <summary>Bahamas</summary>
			<FieldDisplayName("")> <XmlEnum("BHS")> Bahamas
			''' <summary>Bosnia and Herzegovina</summary>
			<FieldDisplayName("")> <XmlEnum("BIH")> BosniaAndHerzegovina
			''' <summary>Belarus</summary>
			<FieldDisplayName("")> <XmlEnum("BLR")> Belarus
			''' <summary>Belize</summary>
			<FieldDisplayName("")> <XmlEnum("BLZ")> Belize
			''' <summary>Bermuda</summary>
			<FieldDisplayName("")> <XmlEnum("BMU")> Bermuda
			''' <summary>Bolivia</summary>
			<FieldDisplayName("")> <XmlEnum("BOL")> Bolivia
			''' <summary>Brazil</summary>
			<FieldDisplayName("")> <XmlEnum("BRA")> Brazil
			''' <summary>Barbados</summary>
			<FieldDisplayName("")> <XmlEnum("BRB")> Barbados
			''' <summary>Brunei Darussalam</summary>
			<FieldDisplayName("")> <XmlEnum("BRN")> Brunei
			''' <summary>Bhutan</summary>
			<FieldDisplayName("")> <XmlEnum("BTN")> Bhutan
			''' <summary>Bouvet Island</summary>
			<FieldDisplayName("")> <XmlEnum("BVT")> BouvetIsland
			''' <summary>Botswana</summary>
			<FieldDisplayName("")> <XmlEnum("BWA")> Botswana
			''' <summary>Central African Republic</summary>
			<FieldDisplayName("")> <XmlEnum("CAF")> CentralAfricanRepublic
			''' <summary>Canada</summary>
			<FieldDisplayName("")> <XmlEnum("CAN")> Canada
			''' <summary>Cocos (Keeling) Islands</summary>
			<FieldDisplayName("")> <XmlEnum("CCK")> CocosIslands
			''' <summary>Switzerland</summary>
			<FieldDisplayName("")> <XmlEnum("CHE")> Switzerland
			''' <summary>Chile</summary>
			<FieldDisplayName("")> <XmlEnum("CHL")> Chile
			''' <summary>China</summary>
			<FieldDisplayName("")> <XmlEnum("CHN")> China
			''' <summary>Côte d'Ivoire</summary>
			<FieldDisplayName("")> <XmlEnum("CIV")> CôteDIvoire
			''' <summary>Cameroon</summary>
			<FieldDisplayName("")> <XmlEnum("CMR")> Cameroon
			''' <summary>Congo, the Democratic Republic of the[1]</summary>
			<FieldDisplayName("")> <XmlEnum("COD")> Zaire
			''' <summary>Congo</summary>
			<FieldDisplayName("")> <XmlEnum("COG")> Congo
			''' <summary>Cook Islands</summary>
			<FieldDisplayName("")> <XmlEnum("COK")> CookIslands
			''' <summary>Colombia</summary>
			<FieldDisplayName("")> <XmlEnum("COL")> Colombia
			''' <summary>Comoros</summary>
			<FieldDisplayName("")> <XmlEnum("COM")> Comoros
			''' <summary>Cape Verde</summary>
			<FieldDisplayName("")> <XmlEnum("CPV")> CapeVerde
			''' <summary>Costa Rica</summary>
			<FieldDisplayName("")> <XmlEnum("CRI")> CostaRica
			''' <summary>Cuba</summary>
			<FieldDisplayName("")> <XmlEnum("CUB")> Cuba
			''' <summary>Christmas Island</summary>
			<FieldDisplayName("")> <XmlEnum("CXR")> ChristmasIsland
			''' <summary>Cayman Islands</summary>
			<FieldDisplayName("")> <XmlEnum("CYM")> CaymanIslands
			''' <summary>Cyprus</summary>
			<FieldDisplayName("")> <XmlEnum("CYP")> Cyprus
			''' <summary>Czech Republic</summary>
			<FieldDisplayName("")> <XmlEnum("CZE")> CzechRepublic
			''' <summary>Germany</summary>
			<FieldDisplayName("")> <XmlEnum("DEU")> Germany
			''' <summary>Djibouti</summary>
			<FieldDisplayName("")> <XmlEnum("DJI")> Djibouti
			''' <summary>Dominica</summary>
			<FieldDisplayName("")> <XmlEnum("DMA")> Dominica
			''' <summary>Denmark</summary>
			<FieldDisplayName("")> <XmlEnum("DNK")> Denmark
			''' <summary>Dominican Republic</summary>
			<FieldDisplayName("")> <XmlEnum("DOM")> DominicanRepublic
			''' <summary>Algeria</summary>
			<FieldDisplayName("")> <XmlEnum("DZA")> Algeria
			''' <summary>Ecuador</summary>
			<FieldDisplayName("")> <XmlEnum("ECU")> Ecuador
			''' <summary>Egypt</summary>
			<FieldDisplayName("")> <XmlEnum("EGY")> Egypt
			''' <summary>Eritrea</summary>
			<FieldDisplayName("")> <XmlEnum("ERI")> Eritrea
			''' <summary>Western Sahara</summary>
			<FieldDisplayName("")> <XmlEnum("ESH")> WesternSahara
			''' <summary>Spain</summary>
			<FieldDisplayName("")> <XmlEnum("ESP")> Spain
			''' <summary>Estonia</summary>
			<FieldDisplayName("")> <XmlEnum("EST")> Estonia
			''' <summary>Ethiopia</summary>
			<FieldDisplayName("")> <XmlEnum("ETH")> Ethiopia
			''' <summary>Finland</summary>
			<FieldDisplayName("")> <XmlEnum("FIN")> Finland
			''' <summary>Fiji</summary>
			<FieldDisplayName("")> <XmlEnum("FJI")> Fiji
			''' <summary>Falkland Islands (Malvinas)</summary>
			<FieldDisplayName("")> <XmlEnum("FLK")> FalklandIslands
			''' <summary>France</summary>
			<FieldDisplayName("")> <XmlEnum("FRA")> France
			''' <summary>Faroe Islands</summary>
			<FieldDisplayName("")> <XmlEnum("FRO")> FaroeIslands
			''' <summary>Micronesia, Federated States of</summary>
			<FieldDisplayName("")> <XmlEnum("FSM")> Micronesia
			''' <summary>Gabon</summary>
			<FieldDisplayName("")> <XmlEnum("GAB")> Gabon
			''' <summary>United Kingdom</summary>
			<FieldDisplayName("")> <XmlEnum("GBR")> UnitedKingdom
			''' <summary>Georgia</summary>
			<FieldDisplayName("")> <XmlEnum("GEO")> Georgia
			''' <summary>Guernsey</summary>
			<FieldDisplayName("")> <XmlEnum("GGY")> Guernsey
			''' <summary>Ghana</summary>
			<FieldDisplayName("")> <XmlEnum("GHA")> Ghana
			''' <summary>Gibraltar</summary>
			<FieldDisplayName("")> <XmlEnum("GIB")> Gibraltar
			''' <summary>Guinea</summary>
			<FieldDisplayName("")> <XmlEnum("GIN")> Guinea
			''' <summary>Guadeloupe</summary>
			<FieldDisplayName("")> <XmlEnum("GLP")> Guadeloupe
			''' <summary>Gambia</summary>
			<FieldDisplayName("")> <XmlEnum("GMB")> Gambia
			''' <summary>Guinea-Bissau</summary>
			<FieldDisplayName("")> <XmlEnum("GNB")> GuineaBissau
			''' <summary>Equatorial Guinea</summary>
			<FieldDisplayName("")> <XmlEnum("GNQ")> EquatorialGuinea
			''' <summary>Greece</summary>
			<FieldDisplayName("")> <XmlEnum("GRC")> Greece
			''' <summary>Grenada</summary>
			<FieldDisplayName("")> <XmlEnum("GRD")> Grenada
			''' <summary>Greenland</summary>
			<FieldDisplayName("")> <XmlEnum("GRL")> Greenland
			''' <summary>Guatemala</summary>
			<FieldDisplayName("")> <XmlEnum("GTM")> Guatemala
			''' <summary>French Guiana</summary>
			<FieldDisplayName("")> <XmlEnum("GUF")> FrenchGuiana
			''' <summary>Guam</summary>
			<FieldDisplayName("")> <XmlEnum("GUM")> Guam
			''' <summary>Guyana</summary>
			<FieldDisplayName("")> <XmlEnum("GUY")> Guyana
			''' <summary>Hong Kong</summary>
			<FieldDisplayName("")> <XmlEnum("HKG")> HongKong
			''' <summary>Heard Island and McDonald Islands</summary>
			<FieldDisplayName("")> <XmlEnum("HMD")> HeardIslandAndMcDonaldIslands
			''' <summary>Honduras</summary>
			<FieldDisplayName("")> <XmlEnum("HND")> Honduras
			''' <summary>Croatia</summary>
			<FieldDisplayName("")> <XmlEnum("HRV")> Croatia
			''' <summary>Haiti</summary>
			<FieldDisplayName("")> <XmlEnum("HTI")> Haiti
			''' <summary>Hungary</summary>
			<FieldDisplayName("")> <XmlEnum("HUN")> Hungary
			''' <summary>Indonesia</summary>
			<FieldDisplayName("")> <XmlEnum("IDN")> Indonesia
			''' <summary>Isle of Man</summary>
			<FieldDisplayName("")> <XmlEnum("IMN")> Man
			''' <summary>India</summary>
			<FieldDisplayName("")> <XmlEnum("IND")> India
			''' <summary>British Indian Ocean Territory</summary>
			<FieldDisplayName("")> <XmlEnum("IOT")> BritishIndianOceanTerritory
			''' <summary>Ireland</summary>
			<FieldDisplayName("")> <XmlEnum("IRL")> Ireland
			''' <summary>Iran, Islamic Republic of</summary>
			<FieldDisplayName("")> <XmlEnum("IRN")> Iran
			''' <summary>Iraq</summary>
			<FieldDisplayName("")> <XmlEnum("IRQ")> Iraq
			''' <summary>Iceland</summary>
			<FieldDisplayName("")> <XmlEnum("ISL")> Iceland
			''' <summary>Israel</summary>
			<FieldDisplayName("")> <XmlEnum("ISR")> Israel
			''' <summary>Italy</summary>
			<FieldDisplayName("")> <XmlEnum("ITA")> Italy
			''' <summary>Jamaica</summary>
			<FieldDisplayName("")> <XmlEnum("JAM")> Jamaica
			''' <summary>Jersey</summary>
			<FieldDisplayName("")> <XmlEnum("JEY")> Jersey
			''' <summary>Jordan</summary>
			<FieldDisplayName("")> <XmlEnum("JOR")> Jordan
			''' <summary>Japan</summary>
			<FieldDisplayName("")> <XmlEnum("JPN")> Japan
			''' <summary>Kazakhstan</summary>
			<FieldDisplayName("")> <XmlEnum("KAZ")> Kazakhstan
			''' <summary>Kenya</summary>
			<FieldDisplayName("")> <XmlEnum("KEN")> Kenya
			''' <summary>Kyrgyzstan</summary>
			<FieldDisplayName("")> <XmlEnum("KGZ")> Kyrgyzstan
			''' <summary>Cambodia</summary>
			<FieldDisplayName("")> <XmlEnum("KHM")> Cambodia
			''' <summary>Kiribati</summary>
			<FieldDisplayName("")> <XmlEnum("KIR")> Kiribati
			''' <summary>Saint Kitts and Nevis</summary>
			<FieldDisplayName("")> <XmlEnum("KNA")> SaintKittsAndNevis
			''' <summary>Korea, Republic of</summary>
			<FieldDisplayName("")> <XmlEnum("KOR")> Korea
			''' <summary>Kuwait</summary>
			<FieldDisplayName("")> <XmlEnum("KWT")> Kuwait
			''' <summary>Lao People's Democratic Republic</summary>
			<FieldDisplayName("")> <XmlEnum("LAO")> Laos
			''' <summary>Lebanon</summary>
			<FieldDisplayName("")> <XmlEnum("LBN")> Lebanon
			''' <summary>Liberia</summary>
			<FieldDisplayName("")> <XmlEnum("LBR")> Liberia
			''' <summary>Libyan Arab Jamahiriya</summary>
			<FieldDisplayName("")> <XmlEnum("LBY")> Libya
			''' <summary>Saint Lucia</summary>
			<FieldDisplayName("")> <XmlEnum("LCA")> SaintLucia
			''' <summary>Liechtenstein</summary>
			<FieldDisplayName("")> <XmlEnum("LIE")> Liechtenstein
			''' <summary>Sri Lanka</summary>
			<FieldDisplayName("")> <XmlEnum("LKA")> SriLanka
			''' <summary>Lesotho</summary>
			<FieldDisplayName("")> <XmlEnum("LSO")> Lesotho
			''' <summary>Lithuania</summary>
			<FieldDisplayName("")> <XmlEnum("LTU")> Lithuania
			''' <summary>Luxembourg</summary>
			<FieldDisplayName("")> <XmlEnum("LUX")> Luxembourg
			''' <summary>Latvia</summary>
			<FieldDisplayName("")> <XmlEnum("LVA")> Latvia
			''' <summary>Macao</summary>
			<FieldDisplayName("")> <XmlEnum("MAC")> Macao
			''' <summary>Morocco</summary>
			<FieldDisplayName("")> <XmlEnum("MAR")> Morocco
			''' <summary>Monaco</summary>
			<FieldDisplayName("")> <XmlEnum("MCO")> Monaco
			''' <summary>Moldova, Republic of</summary>
			<FieldDisplayName("")> <XmlEnum("MDA")> Moldova
			''' <summary>Madagascar</summary>
			<FieldDisplayName("")> <XmlEnum("MDG")> Madagascar
			''' <summary>Maldives</summary>
			<FieldDisplayName("")> <XmlEnum("MDV")> Maldives
			''' <summary>Mexico</summary>
			<FieldDisplayName("")> <XmlEnum("MEX")> Mexico
			''' <summary>Marshall Islands</summary>
			<FieldDisplayName("")> <XmlEnum("MHL")> MarshallIslands
			''' <summary>Macedonia</summary>
			<FieldDisplayName("")> <XmlEnum("MKD")> Macedonia
			''' <summary>Mali</summary>
			<FieldDisplayName("")> <XmlEnum("MLI")> Mali
			''' <summary>Malta</summary>
			<FieldDisplayName("")> <XmlEnum("MLT")> Malta
			''' <summary>Myanmar</summary>
			<FieldDisplayName("")> <XmlEnum("MMR")> Myanmar
			''' <summary>Montenegro</summary>
			<FieldDisplayName("")> <XmlEnum("MNE")> Montenegro
			''' <summary>Mongolia</summary>
			<FieldDisplayName("")> <XmlEnum("MNG")> Mongolia
			''' <summary>Northern Mariana Islands</summary>
			<FieldDisplayName("")> <XmlEnum("MNP")> NorthernMarianaIslands
			''' <summary>Mozambique</summary>
			<FieldDisplayName("")> <XmlEnum("MOZ")> Mozambique
			''' <summary>Mauritania</summary>
			<FieldDisplayName("")> <XmlEnum("MRT")> Mauritania
			''' <summary>Montserrat</summary>
			<FieldDisplayName("")> <XmlEnum("MSR")> Montserrat
			''' <summary>Martinique</summary>
			<FieldDisplayName("")> <XmlEnum("MTQ")> Martinique
			''' <summary>Mauritius</summary>
			<FieldDisplayName("")> <XmlEnum("MUS")> Mauritius
			''' <summary>Malawi</summary>
			<FieldDisplayName("")> <XmlEnum("MWI")> Malawi
			''' <summary>Malaysia</summary>
			<FieldDisplayName("")> <XmlEnum("MYS")> Malaysia
			''' <summary>Mayotte</summary>
			<FieldDisplayName("")> <XmlEnum("MYT")> Mayotte
			''' <summary>Namibia</summary>
			<FieldDisplayName("")> <XmlEnum("NAM")> Namibia
			''' <summary>New Caledonia</summary>
			<FieldDisplayName("")> <XmlEnum("NCL")> NewCaledonia
			''' <summary>Niger</summary>
			<FieldDisplayName("")> <XmlEnum("NER")> Niger
			''' <summary>Norfolk Island</summary>
			<FieldDisplayName("")> <XmlEnum("NFK")> Norfolk
			''' <summary>Nigeria</summary>
			<FieldDisplayName("")> <XmlEnum("NGA")> Nigeria
			''' <summary>Nicaragua</summary>
			<FieldDisplayName("")> <XmlEnum("NIC")> Nicaragua
			''' <summary>Niue</summary>
			<FieldDisplayName("")> <XmlEnum("NIU")> Niue
			''' <summary>Netherlands</summary>
			<FieldDisplayName("")> <XmlEnum("NLD")> Netherlands
			''' <summary>Norway</summary>
			<FieldDisplayName("")> <XmlEnum("NOR")> Norway
			''' <summary>Nepal</summary>
			<FieldDisplayName("")> <XmlEnum("NPL")> Nepal
			''' <summary>Nauru</summary>
			<FieldDisplayName("")> <XmlEnum("NRU")> Nauru
			''' <summary>New Zealand</summary>
			<FieldDisplayName("")> <XmlEnum("NZL")> NewZealand
			''' <summary>Oman</summary>
			<FieldDisplayName("")> <XmlEnum("OMN")> Oman
			''' <summary>Pakistan</summary>
			<FieldDisplayName("")> <XmlEnum("PAK")> Pakistan
			''' <summary>Panama</summary>
			<FieldDisplayName("")> <XmlEnum("PAN")> Panama
			''' <summary>Pitcairn</summary>
			<FieldDisplayName("")> <XmlEnum("PCN")> Pitcairn
			''' <summary>Peru</summary>
			<FieldDisplayName("")> <XmlEnum("PER")> Peru
			''' <summary>Philippines</summary>
			<FieldDisplayName("")> <XmlEnum("PHL")> Philippines
			''' <summary>Palau</summary>
			<FieldDisplayName("")> <XmlEnum("PLW")> Palau
			''' <summary>Papua New Guinea</summary>
			<FieldDisplayName("")> <XmlEnum("PNG")> PapuaNewGuinea
			''' <summary>Poland</summary>
			<FieldDisplayName("")> <XmlEnum("POL")> Poland
			''' <summary>Puerto Rico</summary>
			<FieldDisplayName("")> <XmlEnum("PRI")> PuertoRico
			''' <summary>Korea, Democratic People's Republic of</summary>
			<FieldDisplayName("")> <XmlEnum("PRK")> NortKorea
			''' <summary>Portugal</summary>
			<FieldDisplayName("")> <XmlEnum("PRT")> Portugal
			''' <summary>Paraguay</summary>
			<FieldDisplayName("")> <XmlEnum("PRY")> Paraguay
			''' <summary>Palestinian Territory, Occupied</summary>
			<FieldDisplayName("")> <XmlEnum("PSE")> Palestina
			''' <summary>French Polynesia</summary>
			<FieldDisplayName("")> <XmlEnum("PYF")> FrenchPolynesia
			''' <summary>Qatar</summary>
			<FieldDisplayName("")> <XmlEnum("QAT")> Qatar
			''' <summary>Réunion</summary>
			<FieldDisplayName("")> <XmlEnum("REU")> Réunion
			''' <summary>Romania</summary>
			<FieldDisplayName("")> <XmlEnum("ROU")> Romania
			''' <summary>Russian Federation</summary>
			<FieldDisplayName("")> <XmlEnum("RUS")> Russiaa
			''' <summary>Rwanda</summary>
			<FieldDisplayName("")> <XmlEnum("RWA")> Rwanda
			''' <summary>Saudi Arabia</summary>
			<FieldDisplayName("")> <XmlEnum("SAU")> SaudiArabia
			''' <summary>Sudan</summary>
			<FieldDisplayName("")> <XmlEnum("SDN")> Sudan
			''' <summary>Senegal</summary>
			<FieldDisplayName("")> <XmlEnum("SEN")> Senegal
			''' <summary>Singapore</summary>
			<FieldDisplayName("")> <XmlEnum("SGP")> Singapore
			''' <summary>South Georgia and the South Sandwich Islands</summary>
			<FieldDisplayName("")> <XmlEnum("SGS")> SouthGeorgiaAndTheSouthSandwichIslands
			''' <summary>Saint Helena</summary>
			<FieldDisplayName("")> <XmlEnum("SHN")> SaintHelena
			''' <summary>Svalbard and Jan Mayen</summary>
			<FieldDisplayName("")> <XmlEnum("SJM")> SvalbardAndJanMayen
			''' <summary>Solomon Islands</summary>
			<FieldDisplayName("")> <XmlEnum("SLB")> SolomonIslands
			''' <summary>Sierra Leone</summary>
			<FieldDisplayName("")> <XmlEnum("SLE")> SierraLeone
			''' <summary>El Salvador</summary>
			<FieldDisplayName("")> <XmlEnum("SLV")> Salvador
			''' <summary>San Marino</summary>
			<FieldDisplayName("")> <XmlEnum("SMR")> SanMarino
			''' <summary>Somalia</summary>
			<FieldDisplayName("")> <XmlEnum("SOM")> Somalia
			''' <summary>Saint Pierre and Miquelon</summary>
			<FieldDisplayName("")> <XmlEnum("SPM")> SaintPierreAndMiquelon
			''' <summary>Serbia</summary>
			<FieldDisplayName("")> <XmlEnum("SRB")> Serbia
			''' <summary>Sao Tome and Principe</summary>
			<FieldDisplayName("")> <XmlEnum("STP")> SaoTomeAndPrincipe
			''' <summary>Suriname</summary>
			<FieldDisplayName("")> <XmlEnum("SUR")> Suriname
			''' <summary>Slovakia</summary>
			<FieldDisplayName("")> <XmlEnum("SVK")> Slovakia
			''' <summary>Slovenia</summary>
			<FieldDisplayName("")> <XmlEnum("SVN")> Slovenia
			''' <summary>Sweden</summary>
			<FieldDisplayName("")> <XmlEnum("SWE")> Sweden
			''' <summary>Swaziland</summary>
			<FieldDisplayName("")> <XmlEnum("SWZ")> Swaziland
			''' <summary>Seychelles</summary>
			<FieldDisplayName("")> <XmlEnum("SYC")> Seychelles
			''' <summary>Syrian Arab Republic</summary>
			<FieldDisplayName("")> <XmlEnum("SYR")> Syria
			''' <summary>Turks and Caicos Islands</summary>
			<FieldDisplayName("")> <XmlEnum("TCA")> TurksAndCaicos
			''' <summary>Chad</summary>
			<FieldDisplayName("")> <XmlEnum("TCD")> Chad
			''' <summary>Togo</summary>
			<FieldDisplayName("")> <XmlEnum("TGO")> Togo
			''' <summary>Thailand</summary>
			<FieldDisplayName("")> <XmlEnum("THA")> Thailand
			''' <summary>Tajikistan</summary>
			<FieldDisplayName("")> <XmlEnum("TJK")> Tajikistan
			''' <summary>Tokelau</summary>
			<FieldDisplayName("")> <XmlEnum("TKL")> Tokelau
			''' <summary>Turkmenistan</summary>
			<FieldDisplayName("")> <XmlEnum("TKM")> Turkmenistan
			''' <summary>Timor-Leste</summary>
			<FieldDisplayName("")> <XmlEnum("TLS")> TimorLeste
			''' <summary>Tonga</summary>
			<FieldDisplayName("")> <XmlEnum("TON")> Tonga
			''' <summary>Trinidad and Tobago</summary>
			<FieldDisplayName("")> <XmlEnum("TTO")> TrinidadAndTobago
			''' <summary>Tunisia</summary>
			<FieldDisplayName("")> <XmlEnum("TUN")> Tunisia
			''' <summary>Turkey</summary>
			<FieldDisplayName("")> <XmlEnum("TUR")> Turkey
			''' <summary>Tuvalu</summary>
			<FieldDisplayName("")> <XmlEnum("TUV")> Tuvalu
			''' <summary>Taiwan (ROC)</summary>
			<FieldDisplayName("")> <XmlEnum("TWN")> Taiwan
			''' <summary>Tanzania, United Republic of</summary>
			<FieldDisplayName("")> <XmlEnum("TZA")> Tanzania
			''' <summary>Uganda</summary>
			<FieldDisplayName("")> <XmlEnum("UGA")> Uganda
			''' <summary>Ukraine</summary>
			<FieldDisplayName("")> <XmlEnum("UKR")> Ukraine
			''' <summary>United States Minor Outlying Islands</summary>
			<FieldDisplayName("")> <XmlEnum("UMI")> UnitedStatesMinorOutlyingIslands
			''' <summary>Uruguay</summary>
			<FieldDisplayName("")> <XmlEnum("URY")> Uruguay
			''' <summary>United States</summary>
			<FieldDisplayName("")> <XmlEnum("USA")> USA
			''' <summary>Uzbekistan</summary>
			<FieldDisplayName("")> <XmlEnum("UZB")> Uzbekistan
			''' <summary>Vatican City State (Holy See)</summary>
			<FieldDisplayName("")> <XmlEnum("VAT")> Vatican
			''' <summary>Saint Vincent and the Grenadines</summary>
			<FieldDisplayName("")> <XmlEnum("VCT")> SaintVincentAndTheGrenadines
			''' <summary>Venezuela</summary>
			<FieldDisplayName("")> <XmlEnum("VEN")> Venezuela
			''' <summary>Virgin Islands, British</summary>
			<FieldDisplayName("")> <XmlEnum("VGB")> BritishVirginIslands
			''' <summary>Virgin Islands, U.S.</summary>
			<FieldDisplayName("")> <XmlEnum("VIR")> AmericanVirginIslands
			''' <summary>Viet Nam</summary>
			<FieldDisplayName("")> <XmlEnum("VNM")> VietNam
			''' <summary>Vanuatu</summary>
			<FieldDisplayName("")> <XmlEnum("VUT")> Vanuatu
			''' <summary>Wallis and Futuna</summary>
			<FieldDisplayName("")> <XmlEnum("WLF")> WallisAndFutuna
			''' <summary>Samoa</summary>
			<FieldDisplayName("")> <XmlEnum("WSM")> Samoa
			''' <summary>Yemen</summary>
			<FieldDisplayName("")> <XmlEnum("YEM")> Yemen
			''' <summary>South Africa</summary>
			<FieldDisplayName("")> <XmlEnum("ZAF")> SouthAfrica
			''' <summary>Zambia</summary>
			<FieldDisplayName("")> <XmlEnum("ZMB")> Zambia
			''' <summary>Zimbabwe</summary>
			<FieldDisplayName("")> <XmlEnum("ZWE")> Zimbabwe
			''' <summary>Ascension Island — Reserved on request of UPU, also used by ITU</summary>
			<FieldDisplayName("")> <XmlEnum("ASC")> Ascension
			''' <summary>Clipperton Island — Reserved on request of ITU</summary>
			<FieldDisplayName("")> <XmlEnum("CPT")> Clipperton
			''' <summary>Diego Garcia — Reserved on request of ITU</summary>
			<FieldDisplayName("")> <XmlEnum("DGA")> DiegoGarcia
			''' <summary>France, Metropolitan — Reserved on request of France</summary>
			<FieldDisplayName("")> <XmlEnum("FXX")> FranceMetropolitan
			''' <summary>Tristan da Cunha — Reserved on request of UPU</summary>
			<FieldDisplayName("")> <XmlEnum("TAA")> TristanDaCunha
			''' <summary>United Nations</summary>
			<FieldDisplayName("")> <XmlEnum("XUN")> UnitedNations
			''' <summary>European Union (formerly known as the EC and before that the EEC)</summary>
			<FieldDisplayName("")> <XmlEnum("XEU")> EuropeanUnion
			''' <summary>Space</summary>
			<FieldDisplayName("")> <XmlEnum("XSP")> Space
			''' <summary>at Sea</summary>
			<FieldDisplayName("")> <XmlEnum("XSE")> AtSea
			''' <summary>In Flight</summary>
			<FieldDisplayName("")> <XmlEnum("XIF")> InFlight
			''' <summary>England (where greater granularity than Great Britain is desired)</summary>
			<FieldDisplayName("")> <XmlEnum("XEN")> England
			''' <summary>- Scotland</summary>
			<FieldDisplayName("")> <XmlEnum("XSC")> Scotland
			''' <summary>Northern Ireland</summary>
			<FieldDisplayName("")> <XmlEnum("XNI")> NorthernIreland
			''' <summary>Wales</summary>
			<FieldDisplayName("")> <XmlEnum("XWA")> Wales
		End Enum
		''' <summary>Values of <see cref="ObjectCycle"/></summary>
		<Restrict(True)> Public Enum ObjectCycleValues
			''' <summary>Morning</summary>
			<FieldDisplayName("")> <XmlEnum("a")> Morning
			''' <summary>Evening</summary>
			<FieldDisplayName("")> <XmlEnum("p")> Evening
			''' <summary>Both</summary>
			<FieldDisplayName("")> <XmlEnum("b")> Both
		End Enum
		''' <summary>Possible orientations of image</summary>
		<Restrict(True)> Public Enum Orientations
			''' <summary>Portrait</summary>
			<FieldDisplayName("")> <XmlEnum("P")> Portrait
			''' <summary>Landscape</summary>
			<FieldDisplayName("")> <XmlEnum("L")> Landscape
			''' <summary>Square</summary>
			<FieldDisplayName("")> <XmlEnum("S")> Square
		End Enum
	End Class
#End Region
#Region "Tag types"
	Partial Class Iptc
		''' <summary>Gets details about tag format by tag record and number</summary>
		''' <param name="Record">Recor number</param>
		''' <param name="TagNumber">Number of tag within <paramref name="Record"/></param>
		''' <param name="UseThisGroup">If not null given instance of <see cref="GroupInfo"/> is used instead of obtaining new instance using shared property of the <see cref="GroupInfo"/> class. (Relevant only for tags grouped into groups.)</param>		''' <exception cref="InvalidEnumArgumentException"><paramref name="Record"/> is not member of <see cref="RecordNumbers"/> -or- <paramref name="TagNumber"/> is not tag within <paramref name="record"/></exception>
		Friend Shared Function GetTag(ByVal Record As RecordNumbers, TagNumber As Byte, ByVal UseThisGroup As GroupInfo) As IPTCTag
			Select Case Record
				Case RecordNumbers.Envelope
					Select Case TagNumber
						Case EnvelopeTags.ModelVersion : Return New IPTCTag(Number:=EnvelopeTags.ModelVersion, Record:=RecordNumbers.Envelope, Name:="ModelVersion", HumanName:="Model Version", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Internal", Description:="A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.", Lock:=True)
						Case EnvelopeTags.Destination : Return New IPTCTag(Number:=EnvelopeTags.Destination, Record:=RecordNumbers.Envelope, Name:="Destination", HumanName:="Destination", Type:=IPTCTypes.GraphicCharacters, Mandatory:=false, Repeatable:=true, Length:=1024, Fixed:=false, Category:="Old IPTC", Description:="This DataSet is to accommodate some providers who require routing information above the appropriate OSI layers.", Lock:=True)
						Case EnvelopeTags.FileFormat : Return New IPTCTag(Number:=EnvelopeTags.FileFormat, Record:=RecordNumbers.Envelope, Name:="FileFormat", HumanName:="File Format", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Embeded object", Description:="A number representing the file format.", [Enum]:=GetType(FileFormats), Lock:=True)
						Case EnvelopeTags.FileFormatVersion : Return New IPTCTag(Number:=EnvelopeTags.FileFormatVersion, Record:=RecordNumbers.Envelope, Name:="FileFormatVersion", HumanName:="File Format Version", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Embeded object", Description:="A binary number representing the particular version of the FileFormat", [Enum]:=GetType(FileFormatVersions), Lock:=True)
						Case EnvelopeTags.ServiceIdentifier : Return New IPTCTag(Number:=EnvelopeTags.ServiceIdentifier, Record:=RecordNumbers.Envelope, Name:="ServiceIdentifier", HumanName:="Service Identifier", Type:=IPTCTypes.GraphicCharacters, Mandatory:=true, Repeatable:=false, Length:=10, Fixed:=false, Category:="Old IPTC", Description:="Identifies the provider and product.", Lock:=True)
						Case EnvelopeTags.EnvelopeNumber : Return New IPTCTag(Number:=EnvelopeTags.EnvelopeNumber, Record:=RecordNumbers.Envelope, Name:="EnvelopeNumber", HumanName:="Envelope Number", Type:=IPTCTypes.NumericChar, Mandatory:=true, Repeatable:=false, Length:=8, Fixed:=true, Category:="Old IPTC", Description:="The characters form a number that will be unique for the date specified in DateSent and for the Service Identifier specified in ServiceIdentifier.", Lock:=True)
						Case EnvelopeTags.ProductID : Return New IPTCTag(Number:=EnvelopeTags.ProductID, Record:=RecordNumbers.Envelope, Name:="ProductID", HumanName:="Product I.D.", Type:=IPTCTypes.GraphicCharacters, Mandatory:=false, Repeatable:=true, Length:=32, Fixed:=false, Category:="Old IPTC", Description:="Allows a provider to identify subsets of its overall service.", Lock:=True)
						Case EnvelopeTags.EnvelopePriority : Return New IPTCTag(Number:=EnvelopeTags.EnvelopePriority, Record:=RecordNumbers.Envelope, Name:="EnvelopePriority", HumanName:="Envelope Priority", Type:=IPTCTypes.NumericChar, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Specifies the envelope handling priority and not the editorial urgency (see 2:10, Urgency).", Lock:=True)
						Case EnvelopeTags.DateSent : Return New IPTCTag(Number:=EnvelopeTags.DateSent, Record:=RecordNumbers.Envelope, Name:="DateSent", HumanName:="Date Sent", Type:=IPTCTypes.CCYYMMDD, Mandatory:=true, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="Indicates year, month and day the service sent the material.", Lock:=True)
						Case EnvelopeTags.TimeSent : Return New IPTCTag(Number:=EnvelopeTags.TimeSent, Record:=RecordNumbers.Envelope, Name:="TimeSent", HumanName:="Time Sent", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="This is the time the service sent the material.", Lock:=True)
						Case EnvelopeTags.CodedCharacterSet : Return New IPTCTag(Number:=EnvelopeTags.CodedCharacterSet, Record:=RecordNumbers.Envelope, Name:="CodedCharacterSet", HumanName:="CodedCharacterSet", Type:=IPTCTypes.ByteArray, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Old IPTC", Description:="Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.", Lock:=True)
						Case EnvelopeTags.UNO : Return New IPTCTag(Number:=EnvelopeTags.UNO, Record:=RecordNumbers.Envelope, Name:="UNO", HumanName:="UNO", Type:=IPTCTypes.UNO, Mandatory:=false, Repeatable:=false, Length:=80, Fixed:=false, Category:="Old IPTC", Description:="UNO Unique Name of Object, providing eternal, globally unique identification for objects as specified in the IIM, independent of provider and for any media form.", Lock:=True)
						Case EnvelopeTags.ARMIdentifier : Return New IPTCTag(Number:=EnvelopeTags.ARMIdentifier, Record:=RecordNumbers.Envelope, Name:="ARMIdentifier", HumanName:="ARM Identifier", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Old IPTC", Description:="The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.", [Enum]:=GetType(ARMMethods), Group:=If(UseThisGroup,GroupInfo.ARM), Lock:=True)
						Case EnvelopeTags.ARMVersion : Return New IPTCTag(Number:=EnvelopeTags.ARMVersion, Record:=RecordNumbers.Envelope, Name:="ARMVersion", HumanName:="ARM Version", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Old IPTC", Description:="A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentifier'/>.", [Enum]:=GetType(ARMVersions), Group:=If(UseThisGroup,GroupInfo.ARM), Lock:=True)
						Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(EnvelopeTags))
					End Select
				Case RecordNumbers.Application
					Select Case TagNumber
						Case ApplicationTags.RecordVersion : Return New IPTCTag(Number:=ApplicationTags.RecordVersion, Record:=RecordNumbers.Application, Name:="RecordVersion", HumanName:="Record Version", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Internal", Description:="A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.", Lock:=True)
						Case ApplicationTags.ObjectTypeReference : Return New IPTCTag(Number:=ApplicationTags.ObjectTypeReference, Record:=RecordNumbers.Application, Name:="ObjectTypeReference", HumanName:="Object Type Reference", Type:=IPTCTypes.Num2_Str, Mandatory:=false, Repeatable:=false, Length:=67, Fixed:=false, Category:="Category", Description:="The Object Type is used to distinguish between different types of objects within the IIM.", [Enum]:=GetType(ObjectTypes), Lock:=True)
						Case ApplicationTags.ObjectAttributeReference : Return New IPTCTag(Number:=ApplicationTags.ObjectAttributeReference, Record:=RecordNumbers.Application, Name:="ObjectAttributeReference", HumanName:="Object Attribute Reference", Type:=IPTCTypes.Num3_Str, Mandatory:=false, Repeatable:=true, Length:=68, Fixed:=false, Category:="Category", Description:="The Object Attribute defines the nature of the object independent of the Subject.", [Enum]:=GetType(ObjectAttributes), Lock:=True)
						Case ApplicationTags.ObjectName : Return New IPTCTag(Number:=ApplicationTags.ObjectName, Record:=RecordNumbers.Application, Name:="ObjectName", HumanName:="Object Name", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=64, Fixed:=false, Category:="Title", Description:="Used as a shorthand reference for the object. Changes to existing data, such as updated stories or new crops on photos, should be identified in Edit Status.", Lock:=True)
						Case ApplicationTags.EditStatus : Return New IPTCTag(Number:=ApplicationTags.EditStatus, Record:=RecordNumbers.Application, Name:="EditStatus", HumanName:="Edit Status", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=64, Fixed:=false, Category:="Status", Description:="Status of the objectdata, according to the practice of the provider.", Lock:=True)
						Case ApplicationTags.EditorialUpdate : Return New IPTCTag(Number:=ApplicationTags.EditorialUpdate, Record:=RecordNumbers.Application, Name:="EditorialUpdate", HumanName:="Editorial Update", Type:=IPTCTypes.Enum_NumChar, Mandatory:=false, Repeatable:=false, Length:=2, Fixed:=true, Category:="Status", Description:="Indicates the type of update that this object provides to a previous object. The link to the previous object is made using the ARM (DataSets 1:120 and 1:122 (<see cref='ARM'/>)), according to the practices of the provider.", [Enum]:=GetType(EditorialUpdateValues), Lock:=True)
						Case ApplicationTags.Urgency : Return New IPTCTag(Number:=ApplicationTags.Urgency, Record:=RecordNumbers.Application, Name:="Urgency", HumanName:="Urgency", Type:=IPTCTypes.NumericChar, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Specifies the editorial urgency of content and not necessarily the envelope handling priority (see 1:60, EnvelopePriority).", Lock:=True)
						Case ApplicationTags.SubjectReference : Return New IPTCTag(Number:=ApplicationTags.SubjectReference, Record:=RecordNumbers.Application, Name:="SubjectReference", HumanName:="Subject Reference", Type:=IPTCTypes.SubjectReference, Mandatory:=false, Repeatable:=true, Length:=236, Fixed:=false, Category:="Old IPTC", Description:="The Subject Reference is a structured definition of the subject matter.", Lock:=True)
						Case ApplicationTags.Category : Return New IPTCTag(Number:=ApplicationTags.Category, Record:=RecordNumbers.Application, Name:="Category", HumanName:="Category", Type:=IPTCTypes.Alpha, Mandatory:=false, Repeatable:=false, Length:=3, Fixed:=false, Category:="Category", Description:="Identifies the subject of the objectdata in the opinion of the provider.", Lock:=True)
						Case ApplicationTags.SupplementalCategory : Return New IPTCTag(Number:=ApplicationTags.SupplementalCategory, Record:=RecordNumbers.Application, Name:="SupplementalCategory", HumanName:="Supplemental Category", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=true, Length:=32, Fixed:=false, Category:="Category", Description:="Supplemental categories further refine the subject of an objectdata.", Lock:=True)
						Case ApplicationTags.FixtureIdentifier : Return New IPTCTag(Number:=ApplicationTags.FixtureIdentifier, Record:=RecordNumbers.Application, Name:="FixtureIdentifier", HumanName:="Fixture Identifier", Type:=IPTCTypes.GraphicCharacters, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Category", Description:="Identifies objectdata that recurs often and predictably.", Lock:=True)
						Case ApplicationTags.Keywords : Return New IPTCTag(Number:=ApplicationTags.Keywords, Record:=RecordNumbers.Application, Name:="Keywords", HumanName:="Keywords", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=true, Length:=64, Fixed:=false, Category:="Category", Description:="Used to indicate specific information retrieval words.", Lock:=True)
						Case ApplicationTags.ContentLocationCode : Return New IPTCTag(Number:=ApplicationTags.ContentLocationCode, Record:=RecordNumbers.Application, Name:="ContentLocationCode", HumanName:="Content Location Code", Type:=IPTCTypes.StringEnum, Mandatory:=true, Repeatable:=false, Length:=3, Fixed:=true, Category:="Location", Description:="Indicates the code of a country/geographical location referenced by the content of the object.", [Enum]:=GetType(ISO3166), Group:=If(UseThisGroup,GroupInfo.ContentLocation), Lock:=True)
						Case ApplicationTags.ContentLocationName : Return New IPTCTag(Number:=ApplicationTags.ContentLocationName, Record:=RecordNumbers.Application, Name:="ContentLocationName", HumanName:="Content Location Name", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=64, Fixed:=false, Category:="Location", Description:="Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.", Group:=If(UseThisGroup,GroupInfo.ContentLocation), Lock:=True)
						Case ApplicationTags.ReleaseDate : Return New IPTCTag(Number:=ApplicationTags.ReleaseDate, Record:=RecordNumbers.Application, Name:="ReleaseDate", HumanName:="Release Date", Type:=IPTCTypes.CCYYMMDD, Mandatory:=false, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="The earliest date the provider intends the object to be used.", Lock:=True)
						Case ApplicationTags.ReleaseTime : Return New IPTCTag(Number:=ApplicationTags.ReleaseTime, Record:=RecordNumbers.Application, Name:="ReleaseTime", HumanName:="Release Time", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="The earliest time the provider intends the object to be used.", Lock:=True)
						Case ApplicationTags.ExpirationDate : Return New IPTCTag(Number:=ApplicationTags.ExpirationDate, Record:=RecordNumbers.Application, Name:="ExpirationDate", HumanName:="Expiration Date", Type:=IPTCTypes.CCYYMMDD, Mandatory:=false, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="The latest date the provider or owner intends the objectdata to be used.", Lock:=True)
						Case ApplicationTags.ExpirationTime : Return New IPTCTag(Number:=ApplicationTags.ExpirationTime, Record:=RecordNumbers.Application, Name:="ExpirationTime", HumanName:="Expiration Time", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="The latest time the provider or owner intends the objectdata to be used.", Lock:=True)
						Case ApplicationTags.SpecialInstructions : Return New IPTCTag(Number:=ApplicationTags.SpecialInstructions, Record:=RecordNumbers.Application, Name:="SpecialInstructions", HumanName:="Special Instructions", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=256, Fixed:=false, Category:="Other", Description:="Other editorial instructions concerning the use of the objectdata, such as embargoes and warnings.", Lock:=True)
						Case ApplicationTags.ActionAdvised : Return New IPTCTag(Number:=ApplicationTags.ActionAdvised, Record:=RecordNumbers.Application, Name:="ActionAdvised", HumanName:="Action Advised", Type:=IPTCTypes.Enum_NumChar, Mandatory:=false, Repeatable:=false, Length:=2, Fixed:=true, Category:="Other", Description:="Indicates the type of action that this object provides to a previous object.", [Enum]:=GetType(AdvisedActions), Lock:=True)
						Case ApplicationTags.ReferenceService : Return New IPTCTag(Number:=ApplicationTags.ReferenceService, Record:=RecordNumbers.Application, Name:="ReferenceService", HumanName:="Reference Service", Type:=IPTCTypes.GraphicCharacters, Mandatory:=true, Repeatable:=false, Length:=10, Fixed:=false, Category:="Old IPTC", Description:="Identifies the Service Identifier of a prior envelope to which the current object refers.", Group:=If(UseThisGroup,GroupInfo.Reference), Lock:=True)
						Case ApplicationTags.ReferenceDate : Return New IPTCTag(Number:=ApplicationTags.ReferenceDate, Record:=RecordNumbers.Application, Name:="ReferenceDate", HumanName:="Reference Date", Type:=IPTCTypes.CCYYMMDD, Mandatory:=true, Repeatable:=false, Length:=8, Fixed:=true, Category:="Old IPTC", Description:="Identifies the date of a prior envelope to which the current object refers.", Group:=If(UseThisGroup,GroupInfo.Reference), Lock:=True)
						Case ApplicationTags.ReferenceNumber : Return New IPTCTag(Number:=ApplicationTags.ReferenceNumber, Record:=RecordNumbers.Application, Name:="ReferenceNumber", HumanName:="Reference Number", Type:=IPTCTypes.NumericChar, Mandatory:=true, Repeatable:=false, Length:=8, Fixed:=true, Category:="Old IPTC", Description:="Identifies the Envelope Number of a prior envelope to which the current object refers.", Group:=If(UseThisGroup,GroupInfo.Reference), Lock:=True)
						Case ApplicationTags.DateCreated : Return New IPTCTag(Number:=ApplicationTags.DateCreated, Record:=RecordNumbers.Application, Name:="DateCreated", HumanName:="Date Created", Type:=IPTCTypes.CCYYMMDDommitable, Mandatory:=false, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="The date the intellectual content of the objectdata was created rather than the date of the creation of the physical representation.", Lock:=True)
						Case ApplicationTags.TimeCreated : Return New IPTCTag(Number:=ApplicationTags.TimeCreated, Record:=RecordNumbers.Application, Name:="TimeCreated", HumanName:="Time Created", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.", Lock:=True)
						Case ApplicationTags.DigitalCreationDate : Return New IPTCTag(Number:=ApplicationTags.DigitalCreationDate, Record:=RecordNumbers.Application, Name:="DigitalCreationDate", HumanName:="Digital Creation Date", Type:=IPTCTypes.CCYYMMDD, Mandatory:=false, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="The date the digital representation of the objectdata was created.", Lock:=True)
						Case ApplicationTags.DigitalCreationTime : Return New IPTCTag(Number:=ApplicationTags.DigitalCreationTime, Record:=RecordNumbers.Application, Name:="DigitalCreationTime", HumanName:="Digital Creation Time", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="The time the digital representation of the objectdata was created.", Lock:=True)
						Case ApplicationTags.OriginatingProgram : Return New IPTCTag(Number:=ApplicationTags.OriginatingProgram, Record:=RecordNumbers.Application, Name:="OriginatingProgram", HumanName:="Originating Program", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Other", Description:="Identifies the type of program used to originate the objectdata.", Lock:=True)
						Case ApplicationTags.ProgramVersion : Return New IPTCTag(Number:=ApplicationTags.ProgramVersion, Record:=RecordNumbers.Application, Name:="ProgramVersion", HumanName:="Program Version", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=10, Fixed:=false, Category:="Other", Description:="Identifies the type of program used to originate the objectdata.", Lock:=True)
						Case ApplicationTags.ObjectCycle : Return New IPTCTag(Number:=ApplicationTags.ObjectCycle, Record:=RecordNumbers.Application, Name:="ObjectCycle", HumanName:="Object Cycle", Type:=IPTCTypes.StringEnum, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Virtually only used in North America.", [Enum]:=GetType(ObjectCycleValues), Lock:=True)
						Case ApplicationTags.ByLine : Return New IPTCTag(Number:=ApplicationTags.ByLine, Record:=RecordNumbers.Application, Name:="ByLine", HumanName:="By-line", Type:=IPTCTypes.TextWithSpaces, Mandatory:=true, Repeatable:=false, Length:=32, Fixed:=false, Category:="Author", Description:="Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.", Group:=If(UseThisGroup,GroupInfo.ByLineInfo), Lock:=True)
						Case ApplicationTags.ByLineTitle : Return New IPTCTag(Number:=ApplicationTags.ByLineTitle, Record:=RecordNumbers.Application, Name:="ByLineTitle", HumanName:="By-line Title", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Author", Description:="A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.", Group:=If(UseThisGroup,GroupInfo.ByLineInfo), Lock:=True)
						Case ApplicationTags.City : Return New IPTCTag(Number:=ApplicationTags.City, Record:=RecordNumbers.Application, Name:="City", HumanName:="City", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Location", Description:="Identifies city of objectdata origin according to guidelines established by the provider.", Lock:=True)
						Case ApplicationTags.SubLocation : Return New IPTCTag(Number:=ApplicationTags.SubLocation, Record:=RecordNumbers.Application, Name:="SubLocation", HumanName:="Sublocation", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Location", Description:="Identifies the location within a city from which the objectdata originates, according to guidelines established by the provider.", Lock:=True)
						Case ApplicationTags.ProvinceState : Return New IPTCTag(Number:=ApplicationTags.ProvinceState, Record:=RecordNumbers.Application, Name:="ProvinceState", HumanName:="Province/State", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Location", Description:="Identifies Province/State of origin according to guidelines established by the provider.", Lock:=True)
						Case ApplicationTags.CountryPrimaryLocationCode : Return New IPTCTag(Number:=ApplicationTags.CountryPrimaryLocationCode, Record:=RecordNumbers.Application, Name:="CountryPrimaryLocationCode", HumanName:="Country/Primary Location Code", Type:=IPTCTypes.StringEnum, Mandatory:=false, Repeatable:=false, Length:=3, Fixed:=true, Category:="Location", Description:="Indicates the code of the country/primary location where the intellectual property of the objectdata was created, e.g. a photo was taken, an event occurred.", [Enum]:=GetType(ISO3166), Lock:=True)
						Case ApplicationTags.CountryPrimaryLocationName : Return New IPTCTag(Number:=ApplicationTags.CountryPrimaryLocationName, Record:=RecordNumbers.Application, Name:="CountryPrimaryLocationName", HumanName:="Country/Primary Location Name", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=64, Fixed:=false, Category:="Location", Description:="Provides full, publishable, name of the country/primary location where the intellectual property of the objectdata was created, according to guidelines of the provider.", Lock:=True)
						Case ApplicationTags.OriginalTransmissionReference : Return New IPTCTag(Number:=ApplicationTags.OriginalTransmissionReference, Record:=RecordNumbers.Application, Name:="OriginalTransmissionReference", HumanName:="Original Transmission Refrence", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Location", Description:="A code representing the location of original transmission according to practices of the provider.", Lock:=True)
						Case ApplicationTags.Headline : Return New IPTCTag(Number:=ApplicationTags.Headline, Record:=RecordNumbers.Application, Name:="Headline", HumanName:="Headline", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=256, Fixed:=false, Category:="Title", Description:="A publishable entry providing a synopsis of the contents of the objectdata.", Lock:=True)
						Case ApplicationTags.Credit : Return New IPTCTag(Number:=ApplicationTags.Credit, Record:=RecordNumbers.Application, Name:="Credit", HumanName:="Credit", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Author", Description:="Identifies the provider of the objectdata, not necessarily the owner/creator.", Lock:=True)
						Case ApplicationTags.Source : Return New IPTCTag(Number:=ApplicationTags.Source, Record:=RecordNumbers.Application, Name:="Source", HumanName:="Source", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Author", Description:="Identifies the original owner of the intellectual content of the objectdata.", Lock:=True)
						Case ApplicationTags.CopyrightNotice : Return New IPTCTag(Number:=ApplicationTags.CopyrightNotice, Record:=RecordNumbers.Application, Name:="CopyrightNotice", HumanName:="Copyright Notice", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=128, Fixed:=false, Category:="Author", Description:="Contains any necessary copyright notice.", Lock:=True)
						Case ApplicationTags.Contact : Return New IPTCTag(Number:=ApplicationTags.Contact, Record:=RecordNumbers.Application, Name:="Contact", HumanName:="Contact", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=true, Length:=128, Fixed:=false, Category:="Author", Description:="Identifies the person or organisation which can provide further background information on the objectdata.", Lock:=True)
						Case ApplicationTags.CaptionAbstract : Return New IPTCTag(Number:=ApplicationTags.CaptionAbstract, Record:=RecordNumbers.Application, Name:="CaptionAbstract", HumanName:="Caption/Abstract", Type:=IPTCTypes.Text, Mandatory:=false, Repeatable:=false, Length:=2000, Fixed:=false, Category:="Title", Description:="A textual description of the objectdata, particularly used where the object is not text.", Lock:=True)
						Case ApplicationTags.WriterEditor : Return New IPTCTag(Number:=ApplicationTags.WriterEditor, Record:=RecordNumbers.Application, Name:="WriterEditor", HumanName:="Writer/Editor", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=true, Length:=32, Fixed:=false, Category:="Author", Description:="Identification of the name of the person involved in the writing, editing or correcting the objectdata or caption/abstract.", Lock:=True)
						Case ApplicationTags.RasterizedeCaption : Return New IPTCTag(Number:=ApplicationTags.RasterizedeCaption, Record:=RecordNumbers.Application, Name:="RasterizedeCaption", HumanName:="Rasterized Caption", Type:=IPTCTypes.BW460, Mandatory:=true, Repeatable:=false, Length:=7360, Fixed:=true, Category:="Title", Description:="Image width 460 pixels and image height 128 pixels. Scanning direction bottom to top, left to right.", Lock:=True)
						Case ApplicationTags.ImageType : Return New IPTCTag(Number:=ApplicationTags.ImageType, Record:=RecordNumbers.Application, Name:="ImageType", HumanName:="Image Type", Type:=IPTCTypes.ImageType, Mandatory:=false, Repeatable:=false, Length:=2, Fixed:=true, Category:="Image", Description:="Image Type", Lock:=True)
						Case ApplicationTags.ImageOrientation : Return New IPTCTag(Number:=ApplicationTags.ImageOrientation, Record:=RecordNumbers.Application, Name:="ImageOrientation", HumanName:="Image Orientation", Type:=IPTCTypes.StringEnum, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Image", Description:="Indicates the layout of the image area.", [Enum]:=GetType(Orientations), Lock:=True)
						Case ApplicationTags.LanguageIdentifier : Return New IPTCTag(Number:=ApplicationTags.LanguageIdentifier, Record:=RecordNumbers.Application, Name:="LanguageIdentifier", HumanName:="Language Identifier", Type:=IPTCTypes.Alpha, Mandatory:=true, Repeatable:=false, Length:=135, Fixed:=false, Category:="Other", Description:="Describes the major national language of the object, according to the 2-letter codes of ISO 639:1988.", Lock:=True)
						Case ApplicationTags.AudioType : Return New IPTCTag(Number:=ApplicationTags.AudioType, Record:=RecordNumbers.Application, Name:="AudioType", HumanName:="Audio Type", Type:=IPTCTypes.AudioType, Mandatory:=false, Repeatable:=false, Length:=2, Fixed:=true, Category:="Audio", Description:="Type of audio in objectdata", Lock:=True)
						Case ApplicationTags.AudioSamplingRate : Return New IPTCTag(Number:=ApplicationTags.AudioSamplingRate, Record:=RecordNumbers.Application, Name:="AudioSamplingRate", HumanName:="Audio Sampling Rate", Type:=IPTCTypes.NumericChar, Mandatory:=false, Repeatable:=false, Length:=6, Fixed:=true, Category:="Audio", Description:="Sampling rate, representing the sampling rate in hertz (Hz).", Lock:=True)
						Case ApplicationTags.AudioSamplingResolution : Return New IPTCTag(Number:=ApplicationTags.AudioSamplingResolution, Record:=RecordNumbers.Application, Name:="AudioSamplingResolution", HumanName:="Audio Sampling Resolution", Type:=IPTCTypes.NumericChar, Mandatory:=false, Repeatable:=false, Length:=2, Fixed:=true, Category:="Audio", Description:="The number of bits in each audio sample.", Lock:=True)
						Case ApplicationTags.AudioDuration : Return New IPTCTag(Number:=ApplicationTags.AudioDuration, Record:=RecordNumbers.Application, Name:="AudioDuration", HumanName:="Audio Duration", Type:=IPTCTypes.HHMMSS, Mandatory:=false, Repeatable:=false, Length:=6, Fixed:=true, Category:="Audio", Description:="The running time of an audio objectdata when played back at the speed at which it was recorded.", Lock:=True)
						Case ApplicationTags.AudioOutcue : Return New IPTCTag(Number:=ApplicationTags.AudioOutcue, Record:=RecordNumbers.Application, Name:="AudioOutcue", HumanName:="Audio Outcue", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=64, Fixed:=false, Category:="Audio", Description:="Identifies the content of the end of an audio objectdata, according to guidelines established by the provider.", Lock:=True)
						Case ApplicationTags.ObjectDataPreviewFileFormat : Return New IPTCTag(Number:=ApplicationTags.ObjectDataPreviewFileFormat, Record:=RecordNumbers.Application, Name:="ObjectDataPreviewFileFormat", HumanName:="ObjectData Preview File Format", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Embeded object", Description:="The file format of the ObjectData Preview.", [Enum]:=GetType(FileFormats), Group:=If(UseThisGroup,GroupInfo.ObjectDataPreview), Lock:=True)
						Case ApplicationTags.ObjectDataPreviewFileFormatVersion : Return New IPTCTag(Number:=ApplicationTags.ObjectDataPreviewFileFormatVersion, Record:=RecordNumbers.Application, Name:="ObjectDataPreviewFileFormatVersion", HumanName:="ObjectData Preview File Format Version", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Embeded object", Description:="The particular version of the ObjectData Preview File Format specified in ObjectDataPreviewFileFormat", [Enum]:=GetType(FileFormatVersions), Group:=If(UseThisGroup,GroupInfo.ObjectDataPreview), Lock:=True)
						Case ApplicationTags.ObjectDataPreviewData : Return New IPTCTag(Number:=ApplicationTags.ObjectDataPreviewData, Record:=RecordNumbers.Application, Name:="ObjectDataPreviewData", HumanName:="ObjectData Preview Data", Type:=IPTCTypes.ByteArray, Mandatory:=true, Repeatable:=false, Length:=0, Fixed:=false, Category:="Embeded object", Description:="Maximum size of 256000 octets consisting of binary data.", Group:=If(UseThisGroup,GroupInfo.ObjectDataPreview), Lock:=True)
						Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(ApplicationTags))
					End Select
				Case RecordNumbers.DigitalNewsphotoParameter
					Select Case TagNumber
						Case DigitalNewsphotoParameterTags.RecordVersion3 : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.RecordVersion3, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="RecordVersion3", HumanName:="Record Version (3)", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Internal", Description:="A binary number representing the version of the Digital Newsphoto Parameter Record utilised by the provider.", Lock:=True)
						Case DigitalNewsphotoParameterTags.PictureNumber : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.PictureNumber, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="PictureNumber", HumanName:="Picture Number", Type:=IPTCTypes.PictureNumber, Mandatory:=true, Repeatable:=false, Length:=16, Fixed:=true, Category:="Old IPTC", Description:="The picture number provides a universally unique reference to an image.", Lock:=True)
						Case DigitalNewsphotoParameterTags.PixelsPerLine : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.PixelsPerLine, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="PixelsPerLine", HumanName:="Pixels Per Line", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Image 3", Description:="A number representing the number of pixels in a scan line for the component with the highest resolution.", Lock:=True)
						Case DigitalNewsphotoParameterTags.NumberOfLines : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.NumberOfLines, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="NumberOfLines", HumanName:="Number of Lines", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Image 3", Description:="A number representing the number of scan lines comprising the image for the component with the highest resolution.", Lock:=True)
						Case DigitalNewsphotoParameterTags.PixelSizeInScanningDirection : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.PixelSizeInScanningDirection, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="PixelSizeInScanningDirection", HumanName:="Pixel Size In Scanning Direction", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Image 3", Description:="A number indicating the number of pixels per unit length in the scanning direction.", Lock:=True)
						Case DigitalNewsphotoParameterTags.PixelSizePerpendicularToScanningDirection : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.PixelSizePerpendicularToScanningDirection, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="PixelSizePerpendicularToScanningDirection", HumanName:="Pixel Size Perpendicular To Scanning Direction", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Image 3", Description:="A number indicating the number of pixels per unit length perpendicular to the scanning direction.", Lock:=True)
						Case DigitalNewsphotoParameterTags.SupplementType : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.SupplementType, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="SupplementType", HumanName:="Supplement Type", Type:=IPTCTypes.Enum_binary, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Image 3", Description:="A number indicating the image content.", [Enum]:=GetType(SupplementTypeValue), Lock:=True)
						Case DigitalNewsphotoParameterTags.ColourRepresentation : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.ColourRepresentation, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="ColourRepresentation", HumanName:="Colour Representation", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Image 3", Description:="Indicates colour representation", [Enum]:=GetType(ColourRepresentations), Lock:=True)
						Case DigitalNewsphotoParameterTags.InterchangeColourSpace : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.InterchangeColourSpace, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="InterchangeColourSpace", HumanName:="Interchange Colour Space", Type:=IPTCTypes.Enum_binary, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Image 3", Description:="A value indicating the colour space in which the pixel values are expressed for each component in the image.", [Enum]:=GetType(ColourSpaceValue), Lock:=True)
						Case DigitalNewsphotoParameterTags.ColourSequence : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.ColourSequence, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="ColourSequence", HumanName:="Colour Sequence", Type:=IPTCTypes.UnsignedBinaryNumber, Mandatory:=false, Repeatable:=false, Length:=4, Fixed:=false, Category:="Image 3", Description:="Each of 1 to four octets contains a binary number that relates to the colour component using the identification number assigned to it in the appendix for each colour space.", Lock:=True)
						Case DigitalNewsphotoParameterTags.IccInputColourProfile : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.IccInputColourProfile, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="IccInputColourProfile", HumanName:="ICC Input Colour Profile", Type:=IPTCTypes.ByteArray, Mandatory:=false, Repeatable:=false, Length:=0, Fixed:=false, Category:="Image 3", Description:="Specifies the International Color Consortium profile for the scanning/source device used to generate the digital image files.", Lock:=True)
						Case DigitalNewsphotoParameterTags.ColourCalibrationMatrixTable : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.ColourCalibrationMatrixTable, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="ColourCalibrationMatrixTable", HumanName:="Colour Calibration Matrix Table", Type:=IPTCTypes.ByteArray, Mandatory:=false, Repeatable:=false, Length:=0, Fixed:=false, Category:="Image 3", Description:="This DataSet is no longer required as its contents have been rendered obsolete by the introduction of DataSet P3:66 (ICC Input Colour Profile).", Lock:=True)
						Case DigitalNewsphotoParameterTags.LookupTable : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.LookupTable, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="LookupTable", HumanName:="Lookup Table", Type:=IPTCTypes.ByteArray, Mandatory:=false, Repeatable:=false, Length:=0, Fixed:=false, Category:="Image 3", Description:="Consists of one, three or four one-dimensional lookup tables (LUT).", Lock:=True)
						Case DigitalNewsphotoParameterTags.NumberOfIndexEntries : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.NumberOfIndexEntries, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="NumberOfIndexEntries", HumanName:="Number of Index Entries", Type:=IPTCTypes.UShort_binary, Mandatory:=false, Repeatable:=false, Length:=2, Fixed:=true, Category:="Image 3", Description:="A binary number representing the number of index entries in the DataSet 3:85 (ColourPalette).", Lock:=True)
						Case DigitalNewsphotoParameterTags.ColourPalette : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.ColourPalette, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="ColourPalette", HumanName:="Colour Pallette", Type:=IPTCTypes.ByteArray, Mandatory:=false, Repeatable:=false, Length:=0, Fixed:=false, Category:="Image 3", Description:="In a single-frame colour image, a colour is described with a single sample per pixel.", Lock:=True)
						Case DigitalNewsphotoParameterTags.NumberOfBitsPerSample : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.NumberOfBitsPerSample, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="NumberOfBitsPerSample", HumanName:="Number of Bits per Sample", Type:=IPTCTypes.Byte_binary, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Image 3", Description:="A number between 1 and 16 that indicates the number of bits per pixel value used as entries in the Colour Palette. These values are found in the objectdata itself.", Lock:=True)
						Case DigitalNewsphotoParameterTags.SamplingStructure : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.SamplingStructure, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="SamplingStructure", HumanName:="Sampling Structure", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=1, Fixed:=true, Category:="Image 3", Description:="A number defining the spatial and temporal relationship between pixels.", [Enum]:=GetType(SamplingStructureType), Lock:=True)
						Case DigitalNewsphotoParameterTags.ScanningDirection : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.ScanningDirection, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="ScanningDirection", HumanName:="Scanning Direction", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=1, Fixed:=true, Category:="Image 3", Description:="A number indicating the correct relative two dimensional order of the pixels in the objectdata. Eight possibilities exist.", [Enum]:=GetType(ScanningDirectionValue), Lock:=True)
						Case DigitalNewsphotoParameterTags.ImageRotation : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.ImageRotation, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="ImageRotation", HumanName:="Image Rotation", Type:=IPTCTypes.Enum_binary, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Image 3", Description:="A number indicating the clockwise rotation applied to the image for presentation.", [Enum]:=GetType(ImageRotationValue), Lock:=True)
						Case DigitalNewsphotoParameterTags.DataCompressionMethod : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.DataCompressionMethod, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="DataCompressionMethod", HumanName:="Data Compression Method", Type:=IPTCTypes.UInt_binary, Mandatory:=true, Repeatable:=false, Length:=4, Fixed:=true, Category:="Image 3", Description:="Specifies data compression method", Lock:=True)
						Case DigitalNewsphotoParameterTags.QuantisationMethod : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.QuantisationMethod, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="QuantisationMethod", HumanName:="Quantisation Method", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=1, Fixed:=true, Category:="Image 3", Description:="Contains a binary number identifying the quantisation law.", [Enum]:=GetType(QuantisationMethodValue), Lock:=True)
						Case DigitalNewsphotoParameterTags.EndPoints : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.EndPoints, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="EndPoints", HumanName:="End Points", Type:=IPTCTypes.ByteArray, Mandatory:=true, Repeatable:=false, Length:=0, Fixed:=false, Category:="Image 3", Description:="These end points apply to the coding process.", Lock:=True)
						Case DigitalNewsphotoParameterTags.ExcursionTolerance : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.ExcursionTolerance, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="ExcursionTolerance", HumanName:="Excursion Tolerance", Type:=IPTCTypes.Boolean_binary, Mandatory:=true, Repeatable:=false, Length:=1, Fixed:=true, Category:="Image 3", Description:="Indicates if values outside the range defined by the end points in DataSet 3:125 may occur.", Lock:=True)
						Case DigitalNewsphotoParameterTags.BitsPerComponent : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.BitsPerComponent, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="BitsPerComponent", HumanName:="Bits Per Component", Type:=IPTCTypes.ByteArray, Mandatory:=true, Repeatable:=false, Length:=0, Fixed:=0, Category:="Image 3", Description:="Contains a sequence of one or more octets describing the number of bits used to encode each component. The sequence is specified by the order of components in DataSet 3:65.", Lock:=True)
						Case DigitalNewsphotoParameterTags.MaximumDensityRange : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.MaximumDensityRange, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="MaximumDensityRange", HumanName:="Maximum Density Range", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Image 3", Description:="A binary value which specifies the maximum density range multiplied by 100.", Lock:=True)
						Case DigitalNewsphotoParameterTags.GammaCompensatedValue : Return New IPTCTag(Number:=DigitalNewsphotoParameterTags.GammaCompensatedValue, Record:=RecordNumbers.DigitalNewsphotoParameter, Name:="GammaCompensatedValue", HumanName:="Gamma Compensated Value", Type:=IPTCTypes.UShort_binary, Mandatory:=false, Repeatable:=false, Length:=2, Fixed:=true, Category:="Image 3", Description:="A binary value which specifies the value of gamma for the device multiplied by 100.", Lock:=True)
						Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(DigitalNewsphotoParameterTags))
					End Select
				Case RecordNumbers.NotAllocated5
					Select Case TagNumber
						Case NotAllocated5Tags.OverlallRating : Return New IPTCTag(Number:=NotAllocated5Tags.OverlallRating, Record:=RecordNumbers.NotAllocated5, Name:="OverlallRating", HumanName:="Overall Rating", Type:=IPTCTypes.Enum_binary, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Overll rating of the subject", [Enum]:=GetType(CustomRating), Lock:=True)
						Case NotAllocated5Tags.TechnicalQuality : Return New IPTCTag(Number:=NotAllocated5Tags.TechnicalQuality, Record:=RecordNumbers.NotAllocated5, Name:="TechnicalQuality", HumanName:="Technical Quality", Type:=IPTCTypes.Enum_binary, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Rates technical quality of subject data (e.g. Is the photo sharp?)", [Enum]:=GetType(CustomRating), Lock:=True)
						Case NotAllocated5Tags.ArtQuality : Return New IPTCTag(Number:=NotAllocated5Tags.ArtQuality, Record:=RecordNumbers.NotAllocated5, Name:="ArtQuality", HumanName:="Art Quality", Type:=IPTCTypes.Enum_binary, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Rates artistic quality of subject data (i.e. How nice it is?)", [Enum]:=GetType(CustomRating), Lock:=True)
						Case NotAllocated5Tags.InformationValue : Return New IPTCTag(Number:=NotAllocated5Tags.InformationValue, Record:=RecordNumbers.NotAllocated5, Name:="InformationValue", HumanName:="Information Value", Type:=IPTCTypes.Enum_binary, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Rates information value of subject data (i.e. Does it provide any valuable information?)", [Enum]:=GetType(CustomRating), Lock:=True)
						Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(NotAllocated5Tags))
					End Select
				Case RecordNumbers.PreObjectDataDescriptorRecord
					Select Case TagNumber
						Case PreObjectDataDescriptorRecordTags.SizeMode : Return New IPTCTag(Number:=PreObjectDataDescriptorRecordTags.SizeMode, Record:=RecordNumbers.PreObjectDataDescriptorRecord, Name:="SizeMode", HumanName:="Size Mode", Type:=IPTCTypes.Boolean_binary, Mandatory:=true, Repeatable:=false, Length:=1, Fixed:=true, Category:="Embeded object", Description:="The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.", Lock:=True)
						Case PreObjectDataDescriptorRecordTags.MaxSubfileSize : Return New IPTCTag(Number:=PreObjectDataDescriptorRecordTags.MaxSubfileSize, Record:=RecordNumbers.PreObjectDataDescriptorRecord, Name:="MaxSubfileSize", HumanName:="Max Subfile Size", Type:=IPTCTypes.UnsignedBinaryNumber, Mandatory:=true, Repeatable:=false, Length:=0, Fixed:=false, Category:="Embeded object", Description:="The maximum size for the following Subfile DataSet(s).", Lock:=True)
						Case PreObjectDataDescriptorRecordTags.ObjectDataSizeAnnounced : Return New IPTCTag(Number:=PreObjectDataDescriptorRecordTags.ObjectDataSizeAnnounced, Record:=RecordNumbers.PreObjectDataDescriptorRecord, Name:="ObjectDataSizeAnnounced", HumanName:="ObjectData Size Announced", Type:=IPTCTypes.UnsignedBinaryNumber, Mandatory:=false, Repeatable:=false, Length:=0, Fixed:=false, Category:="Embeded object", Description:="A binary number representing the overall size of the objectdata, expressed in octets, not including tags, if that size is known when transfer commences.", Lock:=True)
						Case PreObjectDataDescriptorRecordTags.MaximumObjectDataSize : Return New IPTCTag(Number:=PreObjectDataDescriptorRecordTags.MaximumObjectDataSize, Record:=RecordNumbers.PreObjectDataDescriptorRecord, Name:="MaximumObjectDataSize", HumanName:="Maximum ObjectData Size", Type:=IPTCTypes.UnsignedBinaryNumber, Mandatory:=false, Repeatable:=false, Length:=0, Fixed:=false, Category:="Embeded object", Description:="Used when objectdata size is not known, indicating the largest size, expressed in octets, that the objectdata can possibly have, not including tags.", Lock:=True)
						Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(PreObjectDataDescriptorRecordTags))
					End Select
				Case RecordNumbers.ObjectDataRecord
					Select Case TagNumber
						Case ObjectDataRecordTags.Subfile : Return New IPTCTag(Number:=ObjectDataRecordTags.Subfile, Record:=RecordNumbers.ObjectDataRecord, Name:="Subfile", HumanName:="Subfile", Type:=IPTCTypes.ByteArray, Mandatory:=true, Repeatable:=false, Length:=0, Fixed:=false, Category:="Embeded object", Description:="Subfile DataSet containing the objectdata itself.", Lock:=True)
						Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(ObjectDataRecordTags))
					End Select
				Case RecordNumbers.PostObjectDataDescriptorRecord
					Select Case TagNumber
						Case PostObjectDataDescriptorRecordTags.ConfirmedObjectDataSize : Return New IPTCTag(Number:=PostObjectDataDescriptorRecordTags.ConfirmedObjectDataSize, Record:=RecordNumbers.PostObjectDataDescriptorRecord, Name:="ConfirmedObjectDataSize", HumanName:="Confirmed ObjectData Size", Type:=IPTCTypes.UnsignedBinaryNumber, Mandatory:=true, Repeatable:=false, Length:=0, Fixed:=false, Category:="Embeded object", Description:="Total size of the objectdata, in octets, without tags.", Lock:=True)
						Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(PostObjectDataDescriptorRecordTags))
					End Select
				Case Else : Throw New InvalidEnumArgumentException("Record",Record,GetType(RecordNumbers))
			End Select
		End Function
		''' <summary>Get details about tag format for tag from record <see cref="RecordNumbers.Envelope"/></summary>
		''' <param name="Number">Number of tag within record</param>		''' <exception cref="InvalidEnumargumentException"><paramref name="Number"/> is not member of <see cref="EnvelopeTags"/></exception>
		Public Shared Function GetTag(ByVal Number As EnvelopeTags) As IPTCTag
			Return GetTag(RecordNumbers.Envelope, Number)
		End Function
		''' <summary>Get details about tag format for tag from record <see cref="RecordNumbers.Application"/></summary>
		''' <param name="Number">Number of tag within record</param>		''' <exception cref="InvalidEnumargumentException"><paramref name="Number"/> is not member of <see cref="ApplicationTags"/></exception>
		Public Shared Function GetTag(ByVal Number As ApplicationTags) As IPTCTag
			Return GetTag(RecordNumbers.Application, Number)
		End Function
		''' <summary>Get details about tag format for tag from record <see cref="RecordNumbers.DigitalNewsphotoParameter"/></summary>
		''' <param name="Number">Number of tag within record</param>		''' <exception cref="InvalidEnumargumentException"><paramref name="Number"/> is not member of <see cref="DigitalNewsphotoParameterTags"/></exception>
		Public Shared Function GetTag(ByVal Number As DigitalNewsphotoParameterTags) As IPTCTag
			Return GetTag(RecordNumbers.DigitalNewsphotoParameter, Number)
		End Function
		''' <summary>Get details about tag format for tag from record <see cref="RecordNumbers.NotAllocated5"/></summary>
		''' <param name="Number">Number of tag within record</param>		''' <exception cref="InvalidEnumargumentException"><paramref name="Number"/> is not member of <see cref="NotAllocated5Tags"/></exception>
		Public Shared Function GetTag(ByVal Number As NotAllocated5Tags) As IPTCTag
			Return GetTag(RecordNumbers.NotAllocated5, Number)
		End Function
		''' <summary>Get details about tag format for tag from record <see cref="RecordNumbers.PreObjectDataDescriptorRecord"/></summary>
		''' <param name="Number">Number of tag within record</param>		''' <exception cref="InvalidEnumargumentException"><paramref name="Number"/> is not member of <see cref="PreObjectDataDescriptorRecordTags"/></exception>
		Public Shared Function GetTag(ByVal Number As PreObjectDataDescriptorRecordTags) As IPTCTag
			Return GetTag(RecordNumbers.PreObjectDataDescriptorRecord, Number)
		End Function
		''' <summary>Get details about tag format for tag from record <see cref="RecordNumbers.ObjectDataRecord"/></summary>
		''' <param name="Number">Number of tag within record</param>		''' <exception cref="InvalidEnumargumentException"><paramref name="Number"/> is not member of <see cref="ObjectDataRecordTags"/></exception>
		Public Shared Function GetTag(ByVal Number As ObjectDataRecordTags) As IPTCTag
			Return GetTag(RecordNumbers.ObjectDataRecord, Number)
		End Function
		''' <summary>Get details about tag format for tag from record <see cref="RecordNumbers.PostObjectDataDescriptorRecord"/></summary>
		''' <param name="Number">Number of tag within record</param>		''' <exception cref="InvalidEnumargumentException"><paramref name="Number"/> is not member of <see cref="PostObjectDataDescriptorRecordTags"/></exception>
		Public Shared Function GetTag(ByVal Number As PostObjectDataDescriptorRecordTags) As IPTCTag
			Return GetTag(RecordNumbers.PostObjectDataDescriptorRecord, Number)
		End Function
	End Class
#End Region
#Region "Groups"
		''' <summary>Groups of tags</summary>
		Public Enum Groups
			''' <summary>Abstract Relation Method</summary>
			<Category("Old IPTC")> <FieldDisplayName("")> ARM
			''' <summary>Country/geographical location referenced by the content of the object</summary>
			<Category("Location")> <FieldDisplayName("")> ContentLocation
			''' <summary>Identifies a prior envelope to which the current object refers.</summary>
			<Category("Old IPTC")> <FieldDisplayName("")> Reference
			''' <summary>Creator of the object data</summary>
			<Category("Author")> <FieldDisplayName("")> ByLineInfo
			''' <summary>Preview of embeded object</summary>
			<Category("Embeded object")> <FieldDisplayName("")> ObjectDataPreview
		End Enum
		Partial Class GroupInfo
			''' <summary>Abstract Relation Method</summary>
			''' <returns>Information about known group <see cref="Groups.ARM"/></returns>
			Public Shared ReadOnly Property ARM As GroupInfo
				Get
					Dim g As New GroupInfo("ARM", "ARM", Groups.ARM, GetType(ARMGroup), "Old IPTC", "Abstract Relation Method", false, false)
					g.SetTags(GetTag(RecordNumbers.Envelope, EnvelopeTags.ARMIdentifier, g), GetTag(RecordNumbers.Envelope, EnvelopeTags.ARMVersion, g))
					Return g
				End Get
			End Property
			''' <summary>Country/geographical location referenced by the content of the object</summary>
			''' <returns>Information about known group <see cref="Groups.ContentLocation"/></returns>
			Public Shared ReadOnly Property ContentLocation As GroupInfo
				Get
					Dim g As New GroupInfo("ContentLocation", "ContentLocation", Groups.ContentLocation, GetType(ContentLocationGroup), "Location", "Country/geographical location referenced by the content of the object", false, true)
					g.SetTags(GetTag(RecordNumbers.Application, ApplicationTags.ContentLocationCode, g), GetTag(RecordNumbers.Application, ApplicationTags.ContentLocationName, g))
					Return g
				End Get
			End Property
			''' <summary>Identifies a prior envelope to which the current object refers.</summary>
			''' <returns>Information about known group <see cref="Groups.Reference"/></returns>
			Public Shared ReadOnly Property Reference As GroupInfo
				Get
					Dim g As New GroupInfo("Reference", "Reference", Groups.Reference, GetType(ReferenceGroup), "Old IPTC", "Identifies a prior envelope to which the current object refers.", false, true)
					g.SetTags(GetTag(RecordNumbers.Application, ApplicationTags.ReferenceService, g), GetTag(RecordNumbers.Application, ApplicationTags.ReferenceDate, g), GetTag(RecordNumbers.Application, ApplicationTags.ReferenceNumber, g))
					Return g
				End Get
			End Property
			''' <summary>Creator of the object data</summary>
			''' <returns>Information about known group <see cref="Groups.ByLineInfo"/></returns>
			Public Shared ReadOnly Property ByLineInfo As GroupInfo
				Get
					Dim g As New GroupInfo("ByLineInfo", "ByLineInfo", Groups.ByLineInfo, GetType(ByLineInfoGroup), "Author", "Creator of the object data", false, true)
					g.SetTags(GetTag(RecordNumbers.Application, ApplicationTags.ByLine, g), GetTag(RecordNumbers.Application, ApplicationTags.ByLineTitle, g))
					Return g
				End Get
			End Property
			''' <summary>Preview of embeded object</summary>
			''' <returns>Information about known group <see cref="Groups.ObjectDataPreview"/></returns>
			Public Shared ReadOnly Property ObjectDataPreview As GroupInfo
				Get
					Dim g As New GroupInfo("ObjectDataPreview", "ObjectDataPreview", Groups.ObjectDataPreview, GetType(ObjectDataPreviewGroup), "Embeded object", "Preview of embeded object", false, false)
					g.SetTags(GetTag(RecordNumbers.Application, ApplicationTags.ObjectDataPreviewFileFormat, g), GetTag(RecordNumbers.Application, ApplicationTags.ObjectDataPreviewFileFormatVersion, g), GetTag(RecordNumbers.Application, ApplicationTags.ObjectDataPreviewData, g))
					Return g
				End Get
			End Property
			''' <summary>Gets all known groups of IPTC tags</summary>
			''' <returns>All known groups of IPTC tags</returns>
			Public Shared Function GetAllGroups() As GroupInfo()
				Return New GroupInfo(){GroupInfo.ARM, GroupInfo.ContentLocation, GroupInfo.Reference, GroupInfo.ByLineInfo, GroupInfo.ObjectDataPreview}
			End Function
		End Class
	Partial Class Iptc
		''' <summary>Gets information about known group of IPTC tags</summary>
		''' <param name="Group">Code of group to get information about</param>
		Public Shared Function GetGroup(ByVal Group As Groups) As GroupInfo
			Select Case Group
				Case Groups.ARM : Return GroupInfo.ARM
				Case Groups.ContentLocation : Return GroupInfo.ContentLocation
				Case Groups.Reference : Return GroupInfo.Reference
				Case Groups.ByLineInfo : Return GroupInfo.ByLineInfo
				Case Groups.ObjectDataPreview : Return GroupInfo.ObjectDataPreview
				Case Else : Throw New InvalidEnumArgumentException("Group", Group, GetType(Groups))
			End Select
		End Function
#Region "Classes"
		''' <summary>Abstract Relation Method</summary>
		<FieldDisplayName("")> <Category("Old IPTC")> <TypeConverter(GetType(ARMGroup.Converter))> Partial Public NotInheritable Class ARMGroup : Inherits Group
				''' <summary>Loads groups from IPTC</summary>
				''' <param name="IPTC"><see cref="IPTC"/> to load groups from</param>
				''' <exception cref="ArgumentNullException"><paramref name="IPTC"/> is null</exception>
				Public Shared Function Load(ByVal IPTC As IPTC) As List(Of ARMGroup)
					If IPTC Is Nothing Then Throw New ArgumentNullException("IPTC")
					Dim Map as List(Of Integer()) = GetGroupMap(IPTC, GetTag(EnvelopeTags.ARMIdentifier), GetTag(EnvelopeTags.ARMVersion))
					If Map Is Nothing OrElse Map.Count = 0 Then Return Nothing
					Dim ret As New List(Of ARMGroup)
					Dim _all_ARMIdentifiers As List(Of ARMMethods) = ConvertEnumList(Of ARMMethods)(IPTC.Enum_Binary_Value(DataSetIdentification.ARMIdentifier, GetType(ARMMethods)))
					Dim _all_ARMVersions As List(Of ARMVersions) = ConvertEnumList(Of ARMVersions)(IPTC.Enum_Binary_Value(DataSetIdentification.ARMVersion, GetType(ARMVersions)))
					For Each item As Integer() In Map
						ret.add(New ARMGroup)
						If item(0) >= 0 Then ret(ret.Count - 1).ARMIdentifier = _all_ARMIdentifiers(item(0))
						If item(1) >= 0 Then ret(ret.Count - 1).ARMVersion = _all_ARMVersions(item(1))
					Next Item
					Return ret
				End Function
				''' <summary>Gets information about this group</summary>
				Public Shared ReadOnly Property GroupInfo As GroupInfo
					Get
						Return GetGroup(Groups.ARM)
					End Get
				End Property
				''' <summary>Contains value of the <see cref="ARMIdentifier"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ARMIdentifier As ARMMethods
				''' <summary>The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.</summary>
				<Category("Old IPTC")> <FieldDisplayName("")> <CLSCompliant(False)>Public Property ARMIdentifier As ARMMethods
					Get
						Return _ARMIdentifier
					End Get
					Set
						_ARMIdentifier = value
					End Set
				End Property

				''' <summary>Contains value of the <see cref="ARMVersion"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ARMVersion As ARMVersions
				''' <summary>A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentifier'/>.</summary>
				<Category("Old IPTC")> <FieldDisplayName("")> <CLSCompliant(False)>Public Property ARMVersion As ARMVersions
					Get
						Return _ARMVersion
					End Get
					Set
						_ARMVersion = value
					End Set
				End Property

		End Class
		''' <summary>Country/geographical location referenced by the content of the object</summary>
		<FieldDisplayName("")> <Category("Location")> <TypeConverter(GetType(ExpandableObjectConverter))> Partial Public NotInheritable Class ContentLocationGroup : Inherits Group
				''' <summary>Loads groups from IPTC</summary>
				''' <param name="IPTC"><see cref="IPTC"/> to load groups from</param>
				''' <exception cref="ArgumentNullException"><paramref name="IPTC"/> is null</exception>
				Public Shared Function Load(ByVal IPTC As IPTC) As List(Of ContentLocationGroup)
					If IPTC Is Nothing Then Throw New ArgumentNullException("IPTC")
					Dim Map as List(Of Integer()) = GetGroupMap(IPTC, GetTag(ApplicationTags.ContentLocationCode), GetTag(ApplicationTags.ContentLocationName))
					If Map Is Nothing OrElse Map.Count = 0 Then Return Nothing
					Dim ret As New List(Of ContentLocationGroup)
					Dim _all_ContentLocationCodes As List(Of StringEnum(Of ISO3166)) = ConvertEnumList(Of ISO3166)(IPTC.StringEnum_Value(DataSetIdentification.ContentLocationCode, GetType(ISO3166)))
					Dim _all_ContentLocationNames As List(Of String) = IPTC.TextWithSpaces_Value(DataSetIdentification.ContentLocationName)
					For Each item As Integer() In Map
						ret.add(New ContentLocationGroup)
						If item(0) >= 0 Then ret(ret.Count - 1).ContentLocationCode = _all_ContentLocationCodes(item(0))
						If item(1) >= 0 Then ret(ret.Count - 1).ContentLocationName = _all_ContentLocationNames(item(1))
					Next Item
					Return ret
				End Function
				''' <summary>Gets information about this group</summary>
				Public Shared ReadOnly Property GroupInfo As GroupInfo
					Get
						Return GetGroup(Groups.ContentLocation)
					End Get
				End Property
				''' <summary>Contains value of the <see cref="ContentLocationCode"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ContentLocationCode As StringEnum(Of ISO3166)
				''' <summary>Indicates the code of a country/geographical location referenced by the content of the object.</summary>
				''' <remarks>Where ISO has established an appropriate country code under ISO 3166, that code will be used. When ISO3166 does not adequately provide for identification of a location or a country, e.g. ships at sea, space, IPTC will assign an appropriate threecharacter code under the provisions of ISO3166 to avoid conflicts. (see Appendix D) .</remarks>
				<Category("Location")> <FieldDisplayName("")> <CLSCompliant(False)>Public Property ContentLocationCode As StringEnum(Of ISO3166)
					Get
						Return _ContentLocationCode
					End Get
					Set
						_ContentLocationCode = value
					End Set
				End Property

				''' <summary>Contains value of the <see cref="ContentLocationName"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ContentLocationName As String
				''' <summary>Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.</summary>
				''' <remarks>If used in the same object with DataSet , must immediately follow and correspond to it.</remarks>
				<Category("Location")> <FieldDisplayName("")> Public Property ContentLocationName As String
					Get
						Return _ContentLocationName
					End Get
					Set
						_ContentLocationName = value
					End Set
				End Property

		End Class
		''' <summary>Identifies a prior envelope to which the current object refers.</summary>
		''' <remarks>Indicate that the current object refers to the content of a prior envelope.</remarks>
		<FieldDisplayName("")> <Category("Old IPTC")> <TypeConverter(GetType(ExpandableObjectConverter))> Partial Public NotInheritable Class ReferenceGroup : Inherits Group
				''' <summary>Loads groups from IPTC</summary>
				''' <param name="IPTC"><see cref="IPTC"/> to load groups from</param>
				''' <exception cref="ArgumentNullException"><paramref name="IPTC"/> is null</exception>
				Public Shared Function Load(ByVal IPTC As IPTC) As List(Of ReferenceGroup)
					If IPTC Is Nothing Then Throw New ArgumentNullException("IPTC")
					Dim Map as List(Of Integer()) = GetGroupMap(IPTC, GetTag(ApplicationTags.ReferenceService), GetTag(ApplicationTags.ReferenceDate), GetTag(ApplicationTags.ReferenceNumber))
					If Map Is Nothing OrElse Map.Count = 0 Then Return Nothing
					Dim ret As New List(Of ReferenceGroup)
					Dim _all_ReferenceServices As List(Of String) = IPTC.GraphicCharacters_Value(DataSetIdentification.ReferenceService)
					Dim _all_ReferenceDates As List(Of Date) = IPTC.CCYYMMDD_Value(DataSetIdentification.ReferenceDate)
					Dim _all_ReferenceNumbers As List(Of Decimal) = IPTC.NumericChar_Value(DataSetIdentification.ReferenceNumber)
					For Each item As Integer() In Map
						ret.add(New ReferenceGroup)
						If item(0) >= 0 Then ret(ret.Count - 1).ReferenceService = _all_ReferenceServices(item(0))
						If item(1) >= 0 Then ret(ret.Count - 1).ReferenceDate = _all_ReferenceDates(item(1))
						If item(2) >= 0 Then ret(ret.Count - 1).ReferenceNumber = _all_ReferenceNumbers(item(2))
					Next Item
					Return ret
				End Function
				''' <summary>Gets information about this group</summary>
				Public Shared ReadOnly Property GroupInfo As GroupInfo
					Get
						Return GetGroup(Groups.Reference)
					End Get
				End Property
				''' <summary>Contains value of the <see cref="ReferenceService"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ReferenceService As String
				''' <summary>Identifies the Service Identifier of a prior envelope to which the current object refers.</summary>
				<Category("Old IPTC")> <FieldDisplayName("")> Public Property ReferenceService As String
					Get
						Return _ReferenceService
					End Get
					Set
						_ReferenceService = value
					End Set
				End Property

				''' <summary>Contains value of the <see cref="ReferenceDate"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ReferenceDate As Date
				''' <summary>Identifies the date of a prior envelope to which the current object refers.</summary>
				<Category("Old IPTC")> <FieldDisplayName("")> Public Property ReferenceDate As Date
					Get
						Return _ReferenceDate
					End Get
					Set
						_ReferenceDate = value
					End Set
				End Property

				''' <summary>Contains value of the <see cref="ReferenceNumber"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ReferenceNumber As Decimal
				''' <summary>Identifies the Envelope Number of a prior envelope to which the current object refers.</summary>
				<Category("Old IPTC")> <FieldDisplayName("")> Public Property ReferenceNumber As Decimal
					Get
						Return _ReferenceNumber
					End Get
					Set
						_ReferenceNumber = value
					End Set
				End Property

		End Class
		''' <summary>Creator of the object data</summary>
		<FieldDisplayName("")> <Category("Author")> <TypeConverter(GetType(ExpandableObjectConverter))> Partial Public NotInheritable Class ByLineInfoGroup : Inherits Group
				''' <summary>Loads groups from IPTC</summary>
				''' <param name="IPTC"><see cref="IPTC"/> to load groups from</param>
				''' <exception cref="ArgumentNullException"><paramref name="IPTC"/> is null</exception>
				Public Shared Function Load(ByVal IPTC As IPTC) As List(Of ByLineInfoGroup)
					If IPTC Is Nothing Then Throw New ArgumentNullException("IPTC")
					Dim Map as List(Of Integer()) = GetGroupMap(IPTC, GetTag(ApplicationTags.ByLine), GetTag(ApplicationTags.ByLineTitle))
					If Map Is Nothing OrElse Map.Count = 0 Then Return Nothing
					Dim ret As New List(Of ByLineInfoGroup)
					Dim _all_ByLines As List(Of String) = IPTC.TextWithSpaces_Value(DataSetIdentification.ByLine)
					Dim _all_ByLineTitles As List(Of String) = IPTC.TextWithSpaces_Value(DataSetIdentification.ByLineTitle)
					For Each item As Integer() In Map
						ret.add(New ByLineInfoGroup)
						If item(0) >= 0 Then ret(ret.Count - 1).ByLine = _all_ByLines(item(0))
						If item(1) >= 0 Then ret(ret.Count - 1).ByLineTitle = _all_ByLineTitles(item(1))
					Next Item
					Return ret
				End Function
				''' <summary>Gets information about this group</summary>
				Public Shared ReadOnly Property GroupInfo As GroupInfo
					Get
						Return GetGroup(Groups.ByLineInfo)
					End Get
				End Property
				''' <summary>Contains value of the <see cref="ByLine"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ByLine As String
				''' <summary>Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.</summary>
				<Category("Author")> <FieldDisplayName("")> Public Property ByLine As String
					Get
						Return _ByLine
					End Get
					Set
						_ByLine = value
					End Set
				End Property

				''' <summary>Contains value of the <see cref="ByLineTitle"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ByLineTitle As String
				''' <summary>A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.</summary>
				''' <remarks>Examples: "Staff Photographer", "Corresponsal", "Envoyé Spécial"</remarks>
				<Category("Author")> <FieldDisplayName("")> Public Property ByLineTitle As String
					Get
						Return _ByLineTitle
					End Get
					Set
						_ByLineTitle = value
					End Set
				End Property

		End Class
		''' <summary>Preview of embeded object</summary>
		<FieldDisplayName("")> <Category("Embeded object")> <TypeConverter(GetType(ObjectDataPreviewGroup.Converter))> Partial Public NotInheritable Class ObjectDataPreviewGroup : Inherits Group
				''' <summary>Loads groups from IPTC</summary>
				''' <param name="IPTC"><see cref="IPTC"/> to load groups from</param>
				''' <exception cref="ArgumentNullException"><paramref name="IPTC"/> is null</exception>
				Public Shared Function Load(ByVal IPTC As IPTC) As List(Of ObjectDataPreviewGroup)
					If IPTC Is Nothing Then Throw New ArgumentNullException("IPTC")
					Dim Map as List(Of Integer()) = GetGroupMap(IPTC, GetTag(ApplicationTags.ObjectDataPreviewFileFormat), GetTag(ApplicationTags.ObjectDataPreviewFileFormatVersion), GetTag(ApplicationTags.ObjectDataPreviewData))
					If Map Is Nothing OrElse Map.Count = 0 Then Return Nothing
					Dim ret As New List(Of ObjectDataPreviewGroup)
					Dim _all_ObjectDataPreviewFileFormats As List(Of FileFormats) = ConvertEnumList(Of FileFormats)(IPTC.Enum_Binary_Value(DataSetIdentification.ObjectDataPreviewFileFormat, GetType(FileFormats)))
					Dim _all_ObjectDataPreviewFileFormatVersions As List(Of FileFormatVersions) = ConvertEnumList(Of FileFormatVersions)(IPTC.Enum_Binary_Value(DataSetIdentification.ObjectDataPreviewFileFormatVersion, GetType(FileFormatVersions)))
					Dim _all_ObjectDataPreviewDatas As List(Of Byte()) = IPTC.ByteArray_Value(DataSetIdentification.ObjectDataPreviewData)
					For Each item As Integer() In Map
						ret.add(New ObjectDataPreviewGroup)
						If item(0) >= 0 Then ret(ret.Count - 1).ObjectDataPreviewFileFormat = _all_ObjectDataPreviewFileFormats(item(0))
						If item(1) >= 0 Then ret(ret.Count - 1).ObjectDataPreviewFileFormatVersion = _all_ObjectDataPreviewFileFormatVersions(item(1))
						If item(2) >= 0 Then ret(ret.Count - 1).ObjectDataPreviewData = _all_ObjectDataPreviewDatas(item(2))
					Next Item
					Return ret
				End Function
				''' <summary>Gets information about this group</summary>
				Public Shared ReadOnly Property GroupInfo As GroupInfo
					Get
						Return GetGroup(Groups.ObjectDataPreview)
					End Get
				End Property
				''' <summary>Contains value of the <see cref="ObjectDataPreviewFileFormat"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ObjectDataPreviewFileFormat As FileFormats
				''' <summary>The file format of the ObjectData Preview.</summary>
				''' <remarks>The file format must be registered with IPTC or NAA with a unique number assigned to it.</remarks>
				<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)>Public Property ObjectDataPreviewFileFormat As FileFormats
					Get
						Return _ObjectDataPreviewFileFormat
					End Get
					Set
						_ObjectDataPreviewFileFormat = value
					End Set
				End Property

				''' <summary>Contains value of the <see cref="ObjectDataPreviewFileFormatVersion"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ObjectDataPreviewFileFormatVersion As FileFormatVersions
				''' <summary>The particular version of the ObjectData Preview File Format specified in <see cref="ObjectDataPreviewFileFormat"/></summary>
				''' <remarks>The File Format Version is taken from the list included in Appendix A</remarks>
				<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)>Public Property ObjectDataPreviewFileFormatVersion As FileFormatVersions
					Get
						Return _ObjectDataPreviewFileFormatVersion
					End Get
					Set
						_ObjectDataPreviewFileFormatVersion = value
					End Set
				End Property

				''' <summary>Contains value of the <see cref="ObjectDataPreviewData"/> property</summary>
				<EditorBrowsable(EditorBrowsableState.Never)> Private Dim _ObjectDataPreviewData As Byte()
				''' <summary>Maximum size of 256000 octets consisting of binary data.</summary>
				<Editor(GetType(EmbededFileEditor), GetType(Drawing.Design.UITypeEditor))> <TypeConverter(GetType(FileByteConverter))> <Category("Embeded object")> <FieldDisplayName("")> Public Property ObjectDataPreviewData As Byte()
					Get
						Return _ObjectDataPreviewData
					End Get
					Set
						_ObjectDataPreviewData = value
					End Set
				End Property

		End Class
#End Region
#Region "Properties"
		''' <summary>Abstract Relation Method</summary>
		<FieldDisplayName("")> <Category("Old IPTC")> Public Property ARM As ARMGroup
			Get
				Dim v As List(Of ARMGroup)=ARMGroup.Load(Me)
				If v Is Nothing OrElse v.Count = 0 Then Return Nothing
				Return v(0)
			End Get
			Set
				ARMIdentifier = value.ARMIdentifier
				ARMVersion = value.ARMVersion
			End Set
		End Property
		''' <summary>Country/geographical location referenced by the content of the object</summary>
		<FieldDisplayName("")> <Category("Location")> <TypeConverter(GetType(TypeConverter))> Public Property ContentLocation As ContentLocationGroup()
			Get
				Dim v As List(Of ContentLocationGroup)=ContentLocationGroup.Load(Me)
				If v Is Nothing OrElse v.Count = 0 Then Return Nothing
				Return v.ToArray
			End Get
			Set
				Dim Items As ContentLocationGroup() = value
				Clear(dataSetIdentification.ContentLocationCode)
				Clear(dataSetIdentification.ContentLocationName)
				If Items IsNot Nothing Then
					For Each item As ContentLocationGroup In Items
						Dim ContentLocationCodeValues As StringEnum(Of ISO3166)() = ContentLocationCode
						If ContentLocationCodeValues Is Nothing Then ContentLocationCodeValues = New StringEnum(Of ISO3166)(){}
						ReDim Preserve ContentLocationCodeValues(ContentLocationCodeValues.Length)
						ContentLocationCodeValues(ContentLocationCodeValues.Length - 1) = item.ContentLocationCode
						ContentLocationCode = ContentLocationCodeValues

						Dim ContentLocationNameValues As String() = ContentLocationName
						If ContentLocationNameValues Is Nothing Then ContentLocationNameValues = New String(){}
						ReDim Preserve ContentLocationNameValues(ContentLocationNameValues.Length)
						ContentLocationNameValues(ContentLocationNameValues.Length - 1) = item.ContentLocationName
						ContentLocationName = ContentLocationNameValues
					Next item
				End If
			End Set
		End Property
		''' <summary>Identifies a prior envelope to which the current object refers.</summary>
		''' <remarks>Indicate that the current object refers to the content of a prior envelope.</remarks>
		<FieldDisplayName("")> <Category("Old IPTC")> <TypeConverter(GetType(TypeConverter))> Public Property Reference As ReferenceGroup()
			Get
				Dim v As List(Of ReferenceGroup)=ReferenceGroup.Load(Me)
				If v Is Nothing OrElse v.Count = 0 Then Return Nothing
				Return v.ToArray
			End Get
			Set
				Dim Items As ReferenceGroup() = value
				Clear(dataSetIdentification.ReferenceService)
				Clear(dataSetIdentification.ReferenceDate)
				Clear(dataSetIdentification.ReferenceNumber)
				If Items IsNot Nothing Then
					For Each item As ReferenceGroup In Items
						Dim ReferenceServiceValues As String() = ReferenceService
						If ReferenceServiceValues Is Nothing Then ReferenceServiceValues = New String(){}
						ReDim Preserve ReferenceServiceValues(ReferenceServiceValues.Length)
						ReferenceServiceValues(ReferenceServiceValues.Length - 1) = item.ReferenceService
						ReferenceService = ReferenceServiceValues

						Dim ReferenceDateValues As Date() = ReferenceDate
						If ReferenceDateValues Is Nothing Then ReferenceDateValues = New Date(){}
						ReDim Preserve ReferenceDateValues(ReferenceDateValues.Length)
						ReferenceDateValues(ReferenceDateValues.Length - 1) = item.ReferenceDate
						ReferenceDate = ReferenceDateValues

						Dim ReferenceNumberValues As Decimal() = ReferenceNumber
						If ReferenceNumberValues Is Nothing Then ReferenceNumberValues = New Decimal(){}
						ReDim Preserve ReferenceNumberValues(ReferenceNumberValues.Length)
						ReferenceNumberValues(ReferenceNumberValues.Length - 1) = item.ReferenceNumber
						ReferenceNumber = ReferenceNumberValues
					Next item
				End If
			End Set
		End Property
		''' <summary>Creator of the object data</summary>
		<FieldDisplayName("")> <Category("Author")> Public Property ByLineInfo As ByLineInfoGroup()
			Get
				Dim v As List(Of ByLineInfoGroup)=ByLineInfoGroup.Load(Me)
				If v Is Nothing OrElse v.Count = 0 Then Return Nothing
				Return v.ToArray
			End Get
			Set
				Dim Items As ByLineInfoGroup() = value
				Clear(dataSetIdentification.ByLine)
				Clear(dataSetIdentification.ByLineTitle)
				If Items IsNot Nothing Then
					For Each item As ByLineInfoGroup In Items
						Dim ByLineValues As String() = ByLine
						If ByLineValues Is Nothing Then ByLineValues = New String(){}
						ReDim Preserve ByLineValues(ByLineValues.Length)
						ByLineValues(ByLineValues.Length - 1) = item.ByLine
						ByLine = ByLineValues

						Dim ByLineTitleValues As String() = ByLineTitle
						If ByLineTitleValues Is Nothing Then ByLineTitleValues = New String(){}
						ReDim Preserve ByLineTitleValues(ByLineTitleValues.Length)
						ByLineTitleValues(ByLineTitleValues.Length - 1) = item.ByLineTitle
						ByLineTitle = ByLineTitleValues
					Next item
				End If
			End Set
		End Property
		''' <summary>Preview of embeded object</summary>
		<FieldDisplayName("")> <Category("Embeded object")> <Editor(GetType(NewEditor),GetType(System.Drawing.Design.UITypeEditor))> Public Property ObjectDataPreview As ObjectDataPreviewGroup
			Get
				Dim v As List(Of ObjectDataPreviewGroup)=ObjectDataPreviewGroup.Load(Me)
				If v Is Nothing OrElse v.Count = 0 Then Return Nothing
				Return v(0)
			End Get
			Set
				ObjectDataPreviewFileFormat = value.ObjectDataPreviewFileFormat
				ObjectDataPreviewFileFormatVersion = value.ObjectDataPreviewFileFormatVersion
				ObjectDataPreviewData = value.ObjectDataPreviewData
			End Set
		End Property
#End Region
	End Class
#End Region
#Region "Properties"
	Partial Class Iptc
		''' <summary>A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.</summary>
		''' <remarks>Version numbers are assigned by IPTC and NAA. The version number of this record is four (4).</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.")> _
		<Category("Internal")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property ModelVersion As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.ModelVersion)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.ModelVersion) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>This DataSet is to accommodate some providers who require routing information above the appropriate OSI layers.</summary>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("This DataSet is to accommodate some providers who require routing information above the appropriate OSI layers.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> Public Overridable Property Destination As String()
			Get
				Try
					Dim AllValues As List(Of String) = GraphicCharacters_Value(DataSetIdentification.Destination)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					GraphicCharacters_Value(DataSetIdentification.Destination, 1024, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number representing the file format.</summary>
		''' <remarks>The file format must be registered with IPTC or NAA with a unique number assigned to it (see Appendix A). The information is used to route the data to the appropriate system and to allow the receiving system to perform the appropriate actions thereto.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number representing the file format.")> _
		<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property FileFormat As FileFormats
			Get
				Try
					Dim AllValues As List(Of FileFormats) = ConvertEnumList(Of FileFormats)(Enum_Binary_Value(DataSetIdentification.FileFormat, GetType(FileFormats)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.FileFormat, GetType(FileFormats)) = ConvertEnumList(New List(Of FileFormats)(New FileFormats(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A binary number representing the particular version of the <see cref="FileFormat"/></summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A binary number representing the particular version of the File Format")> _
		<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property FileFormatVersion As FileFormatVersions
			Get
				Try
					Dim AllValues As List(Of FileFormatVersions) = ConvertEnumList(Of FileFormatVersions)(Enum_Binary_Value(DataSetIdentification.FileFormatVersion, GetType(FileFormatVersions)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.FileFormatVersion, GetType(FileFormatVersions)) = ConvertEnumList(New List(Of FileFormatVersions)(New FileFormatVersions(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the provider and product.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the provider and product.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> Public Overridable Property ServiceIdentifier As String
			Get
				Try
					Dim AllValues As List(Of String) = GraphicCharacters_Value(DataSetIdentification.ServiceIdentifier)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					GraphicCharacters_Value(DataSetIdentification.ServiceIdentifier, 10, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The characters form a number that will be unique for the date specified in <see cref="DateSent"/> and for the Service Identifier specified in <see cref="ServiceIdentifier"/>.</summary>
		''' <remarks>If identical envelope numbers appear with the same date and with the same Service Identifier, records 2-9 must be unchanged from the original. This is not intended to be a sequential serial number reception check.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The characters form a number that will be unique for the date specified in Date Sentand for the Service Identifier specified in Service Identifier.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> Public Overridable Property EnvelopeNumber As Decimal
			Get
				Try
					Dim AllValues As List(Of Decimal) = NumericChar_Value(DataSetIdentification.EnvelopeNumber)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					NumericChar_Value(DataSetIdentification.EnvelopeNumber, 8, true) = New List(Of Decimal)(New Decimal(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Allows a provider to identify subsets of its overall service.</summary>
		''' <remarks>Used to provide receiving organisation data on which to select, route, or otherwise handle data.</remarks>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Allows a provider to identify subsets of its overall service.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> Public Overridable Property ProductID As String()
			Get
				Try
					Dim AllValues As List(Of String) = GraphicCharacters_Value(DataSetIdentification.ProductID)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					GraphicCharacters_Value(DataSetIdentification.ProductID, 32, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Specifies the envelope handling priority and not the editorial urgency (see 2:10, <see cref="Urgency"/>).</summary>
		''' <remarks>'1' indicates the most urgent, '5' the normal urgency, and '8' the least urgent copy. The numeral '9' indicates a User Defined Priority. The numeral '0' is reserved for future use.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Specifies the envelope handling priority and not the editorial urgency (see 2:10, Urgency).")> _
		<Category("Status")> <FieldDisplayName("")> Public Overridable Property EnvelopePriority As Decimal
			Get
				Try
					Dim AllValues As List(Of Decimal) = NumericChar_Value(DataSetIdentification.EnvelopePriority)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					NumericChar_Value(DataSetIdentification.EnvelopePriority, 1, true) = New List(Of Decimal)(New Decimal(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Indicates year, month and day the service sent the material.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Indicates year, month and day the service sent the material.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property DateSent As Date
			Get
				Try
					Dim AllValues As List(Of Date) = CCYYMMDD_Value(DataSetIdentification.DateSent)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					CCYYMMDD_Value(DataSetIdentification.DateSent) = New List(Of Date)(New Date(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>This is the time the service sent the material.</summary>
		''' <remarks>This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
		''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("This is the time the service sent the material.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property TimeSent As Time
			Get
				Try
					Dim AllValues As List(Of Time) = HHMMSS_HHMM_Value(DataSetIdentification.TimeSent)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					HHMMSS_HHMM_Value(DataSetIdentification.TimeSent) = New List(Of Time)(New Time(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.</summary>
		''' <remarks>The control functions apply to character oriented DataSets in records 2-6. They also apply to record 8, unless the objectdata explicitly, or the File Format implicitly, defines character sets otherwise. If this DataSet contains the designation function for Unicode in UTF-8 then no other announcement, designation or invocation functions are permitted in this DataSet or in records 2-6. For all other character sets, one or more escape sequences are used: for the announcement of the code extension facilities used in the data which follows, for the initial designation of the G0, G1, G2 and G3 graphic character sets and for the initial invocation of the graphic set (7 bits) or the lefthand and the right-hand graphic set (8 bits) and for the initial invocation of the C0 (7 bits) or of the C0 and the C1 control character sets (8 bits). The announcement of the code extension facilities, if transmitted, must appear in this data set. Designation and invocation of graphic and control function sets (shifting) may be transmitted anywhere where the escape and the other necessary control characters are permitted. However, it is recommended to transmit in this DataSet an initial designation and invocation, i.e. to define all designations and the shift status currently in use by transmitting the appropriate escape sequences and locking-shift functions. If is omitted, the default for records 2-6 and 8 is ISO 646 IRV (7 bits) or ISO 4873 DV (8 bits). Record 1 shall always use ISO 646 IRV or ISO 4873 DV respectively. ECMA as the ISO Registration Authority for escape sequences maintains the International Register of Coded Character Sets to be used with escape sequences, a register of Codes and allocated standardised escape sequences, which are recognised by IPTC-NAA without further approval procedure. The registration procedure is defined in ISO 2375. IPTC-NAA maintain a Register of Codes and allocated private escape sequences, which are shown in paragraph 1.2. IPTC may, as Sponsoring Authority, submit such private sequence Codes for approval as standardised sequence Codes. The registers consist of a Graphic repertoire, a Control function repertoire and a Repertoire of other coding systems (e.g. complete Codes). Together they represent the IPTC-NAA Code Library. Graphic Repertoire94-character sets (intermediate character 2/8 to 2/11)002ISO 646 IRV 4/0004ISO 646 British Version 4/1006ISO 646 USA Version (ASCII) 4/2008-1NATS Primary Set for Finland and Sweden 4/3008-2NATS Secondary Set for Finland and Sweden 4/4009-1NATS Primary Set for Denmark and Norway 4/5009-2NATS Secondary Set for Denmark and Norway 4/6010ISO 646 Swedish Version (SEN 850200) 4/7015ISO 646 Italian Version (ECMA) 5/9016ISO 646 Portuguese Version (ECMA Olivetti) 4/12017ISO 646 Spanish Version (ECMA Olivetti) 5/10018ISO 646 Greek Version (ECMA) 5/11021ISO 646 German Version (DIN 66003) 4/11037Basic Cyrillic Character Set (ISO 5427) 4/14060ISO 646 Norwegian Version (NS 4551) 6/0069ISO 646 French Version (NF Z 62010-1982) 6/6084ISO 646 Portuguese Version (ECMA IBM) 6/7085ISO 646 Spanish Version (ECMA IBM) 6/8086ISO 646 Hungarian Version (HS 7795/3) 6/9121Alternate Primary Graphic Set No. 1 (Canada CSA Z 243.4-1985) 7/7122Alternate Primary Graphic Set No. 2 (Canada CSA Z 243.4-1985) 7/896-character sets (intermediate character 2/12 to 2/15):100Right-hand Part of Latin Alphabet No. 1 (ISO 8859-1) 4/1101Right-hand Part of Latin Alphabet No. 2 (ISO 8859-2) 4/2109Right-hand Part of Latin Alphabet No. 3 (ISO 8859-3) 4/3110Right-hand Part of Latin Alphabet No. 4 (ISO 8859-4) 4/4111Right-hand Part of Latin/Cyrillic Alphabet (ISO 8859-5) 4/0125Right-hand Part of Latin/Greek Alphabet (ISO 8859-7) 4/6127Right-hand Part of Latin/Arabic Alphabet (ISO 8859-6) 4/7138Right-hand Part of Latin/Hebrew Alphabet (ISO 8859-8) 4/8139Right-hand Part of Czechoslovak Standard (ČSN 369103) 4/9Multiple-Byte Graphic Character Sets (1st intermediate character 2/4, 2nd intermediate character 2/8 to 2/11)87Japanese characters (JIS X 0208-1983) 4/2Control Function RepertoireC0 Control Function Sets (intermediate character 2/1)001C0 Set of ISO 646 4/0026IPTC C0 Set for newspaper text transmission 4/3036C0 Set of ISO 646 with SS2 instead of IS4 4/4104Minimum C0 Set for ISO 4873 4/7 C1 Control Function Sets (intermediate character 2/2)077C1 Control Set of ISO 6429 4/3105Minimum C1 Set for ISO 4873 4/7 Single Additional Control Functions062Locking-Shift Two (LS2), ISO 2022 6/14063Locking-Shift Three (LS3), ISO 2022 6/15064Locking-Shift Three Right (LS3R), ISO 2022 7/12065Locking-Shift Two Right (LS2R), ISO 2022 7/13066Locking-Shift One Right (LS1R), ISO 2022 7/14Repertoire of Other Coding Systems (e.g. complete Codes, intermediate character 2/5 )196UCS Transformation Format (UTF-8) 4/7 What's mentioned above is from definition of IPTC standard. This class currently does not support all encodings that can be advertised here neither it supports changes of encodings using ISO 2022 mechanism anywhere in text. Only certain encodings are supported - see the property for details.</remarks>
		''' <version version="1.5.3">Added limited support for UTF-8 based encoding autodetection based on value of this dataset.</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> <TypeConverter(GetType(HexaConverter))> <Editor(GetType(Drawing.Design.UITypeEditor), GetType(Drawing.Design.UITypeEditor))> Public Overridable Property CodedCharacterSet As Byte()
			Get
				Try
					Dim AllValues As List(Of Byte()) = ByteArray_Value(DataSetIdentification.CodedCharacterSet)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Bytearray_Value(DataSetIdentification.CodedCharacterSet, 32, false) = New List(Of Byte())(New Byte()(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>UNO Unique Name of Object, providing eternal, globally unique identification for objects as specified in the IIM, independent of provider and for any media form.</summary>
		''' <remarks>The provider must ensure the UNO is unique. Objects with the same UNO are identical.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("UNO Unique Name of Object, providing eternal, globally unique identification for objects as specified in the IIM, independent of provider and for any media form.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> Public Overridable Property UNO As iptcUNO
			Get
				Try
					Dim AllValues As List(Of iptcUNO) = UNO_Value(DataSetIdentification.UNO)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UNO_Value(DataSetIdentification.UNO) = New List(Of iptcUNO)(New iptcUNO(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.</summary>
		''' <remarks>Version numbers are assigned by IPTC and NAA. The version number of this record is four (4). Same tag called Record Version" also exists in record no. 3.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.")> _
		<Category("Internal")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property RecordVersion As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.RecordVersion)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.RecordVersion) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The Object Type is used to distinguish between different types of objects within the IIM.</summary>
		''' <remarks>The first part is a number representing a language independent international reference to an Object Type followed by a colon separator. The second part, if used, is a text representation of the Object Type Number (maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix G, or in the language of the service as indicated in DataSet 2:135 (<see cref='LanguageIdentifier'/>)</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The Object Type is used to distinguish between different types of objects within the IIM.")> _
		<Category("Category")> <FieldDisplayName("")> <CLSCompliant(False)><DefaultValue(GetType(NumStr2(Of ObjectTypes)), "Tools.MetadataT.IptcT.IptcDataTypes.NumStr2`1;Tools.MetadataT.IptcT.Iptc+ObjectTypes;02;")> Public Overridable Property ObjectTypeReference As NumStr2(Of ObjectTypes)
			Get
				Try
					Dim AllValues As List(Of NumStr2(Of ObjectTypes)) = ConvertNumStrList(Of NumStr2, NumStr2(Of ObjectTypes))(Num2_Str_Value(DataSetIdentification.ObjectTypeReference))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Num2_Str_Value(DataSetIdentification.ObjectTypeReference, 67) = ConvertNumStrList(Of NumStr2, NumStr2(Of ObjectTypes))(New List(Of NumStr2(Of ObjectTypes))(New NumStr2(Of ObjectTypes)(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The Object Attribute defines the nature of the object independent of the Subject.</summary>
		''' <remarks>The first part is a number representing a language independent international reference to an Object Attribute followed by a colon separator. The second part, if used, is a text representation of the Object Attribute Number ( maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix G, or in the language of the service as indicated in DataSet 2:135 (<see cref='LanguageIdentifier'/>)</remarks>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The Object Attribute defines the nature of the object independent of the Subject.")> _
		<Category("Category")> <FieldDisplayName("")> <CLSCompliant(False)><DefaultValue(GetType(NumStr2(Of ObjectAttributes)), "Tools.MetadataT.IptcT.IptcDataTypes.NumStr3`1;Tools.MetadataT.IptcT.Iptc+ObjectAttributes;001;")> Public Overridable Property ObjectAttributeReference As NumStr3(Of ObjectAttributes)()
			Get
				Try
					Dim AllValues As List(Of NumStr3(Of ObjectAttributes)) = ConvertNumStrList(Of NumStr3, NumStr3(Of ObjectAttributes))(Num3_Str_Value(DataSetIdentification.ObjectAttributeReference))
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Num3_Str_Value(DataSetIdentification.ObjectAttributeReference, 68) = ConvertNumStrList(Of NumStr3, NumStr3(Of ObjectAttributes))(New List(Of NumStr3(Of ObjectAttributes))(value))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Used as a shorthand reference for the object. Changes to existing data, such as updated stories or new crops on photos, should be identified in Edit Status.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Used as a shorthand reference for the object. Changes to existing data, such as updated stories or new crops on photos, should be identified in Edit Status.")> _
		<Category("Title")> <FieldDisplayName("")> Public Overridable Property ObjectName As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.ObjectName)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.ObjectName, 64, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Status of the objectdata, according to the practice of the provider.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Status of the objectdata, according to the practice of the provider.")> _
		<Category("Status")> <FieldDisplayName("")> Public Overridable Property EditStatus As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.EditStatus)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.EditStatus, 64, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Indicates the type of update that this object provides to a previous object. The link to the previous object is made using the ARM (DataSets 1:120 and 1:122 (<see cref='ARM'/>)), according to the practices of the provider.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Indicates the type of update that this object provides to a previous object. The link to the previous object is made using the ARM (DataSets 1:120 and 1:122 (<see cref='ARM'/>)), according to the practices of the provider.")> _
		<Category("Status")> <FieldDisplayName("")> Public Overridable Property EditorialUpdate As EditorialUpdateValues
			Get
				Try
					Dim AllValues As List(Of EditorialUpdateValues) = ConvertEnumList(Of EditorialUpdateValues)(Enum_NumChar_Value(DataSetIdentification.EditorialUpdate, GetType(EditorialUpdateValues)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_NumChar_Value(DataSetIdentification.EditorialUpdate, GetType(EditorialUpdateValues), 2, true) = ConvertEnumList(New List(Of EditorialUpdateValues)(New EditorialUpdateValues(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Specifies the editorial urgency of content and not necessarily the envelope handling priority (see 1:60, <see cref="EnvelopePriority"/>).</summary>
		''' <remarks>The '1' is most urgent, '5' normal and '8' denotes the least-urgent copy. The numerals '9' and '0' are reserved for future use.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Specifies the editorial urgency of content and not necessarily the envelope handling priority (see 1:60, Envelope Priority).")> _
		<Category("Status")> <FieldDisplayName("")> Public Overridable Property Urgency As Decimal
			Get
				Try
					Dim AllValues As List(Of Decimal) = NumericChar_Value(DataSetIdentification.Urgency)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					NumericChar_Value(DataSetIdentification.Urgency, 1, true) = New List(Of Decimal)(New Decimal(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The Subject Reference is a structured definition of the subject matter.</summary>
		''' <remarks>It must contain an IPR (default value is "IPTC"), an 8 digit Subject Reference Number and an optional Subject Name, Subject Matter Name and Subject Detail Name. Each part of the Subject reference is separated by a colon (:). The Subject Reference Number contains three parts, a 2 digit Subject Number, a 3 digit Subject Matter Number and a 3 digit Subject Detail Number thus providing unique identification of the object's subject.</remarks>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The Subject Reference is a structured definition of the subject matter.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> Public Overridable Property SubjectReference As iptcSubjectReference()
			Get
				Try
					Dim AllValues As List(Of iptcSubjectReference) = SubjectReference_Value(DataSetIdentification.SubjectReference)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					SubjectReference_Value(DataSetIdentification.SubjectReference) = New List(Of iptcSubjectReference)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the subject of the objectdata in the opinion of the provider.</summary>
		''' <remarks>A list of categories will be maintained by a regional registry, where available, otherwise by the provider.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the subject of the objectdata in the opinion of the provider.")> _
		<Category("Category")> <FieldDisplayName("")> <Obsolete("Use of this DataSet is Deprecated. It is likely that this DataSet will not be included in further versions of the IIM.")> Public Overridable Property Category As String
			Get
				Try
					Dim AllValues As List(Of String) = Alpha_Value(DataSetIdentification.Category)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Alpha_Value(DataSetIdentification.Category, 3, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Supplemental categories further refine the subject of an objectdata.</summary>
		''' <remarks>Only a single supplemental category may be contained in each DataSet. A supplemental category may include any of the recognised categories as used in . Otherwise, selection of supplemental categories are left to the provider.</remarks>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Supplemental categories further refine the subject of an objectdata.")> _
		<Category("Category")> <FieldDisplayName("")> <Obsolete("Use of this DataSet is Deprecated. It is likely that this DataSet will not be included in further versions of the IIM.")> Public Overridable Property SupplementalCategory As String()
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.SupplementalCategory)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.SupplementalCategory, 32, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies objectdata that recurs often and predictably.</summary>
		''' <remarks>Enables users to immediately find or recall such an object.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies objectdata that recurs often and predictably.")> _
		<Category("Category")> <FieldDisplayName("")> Public Overridable Property FixtureIdentifier As String
			Get
				Try
					Dim AllValues As List(Of String) = GraphicCharacters_Value(DataSetIdentification.FixtureIdentifier)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					GraphicCharacters_Value(DataSetIdentification.FixtureIdentifier, 32, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Used to indicate specific information retrieval words.</summary>
		''' <remarks>Each keyword uses a single Keywords DataSet. Multiple keywords use multiple Keywords DataSets. It is expected that a provider of various types of data that are related in subject matter uses the same keyword, enabling the receiving system or subsystems to search across all types of data for related material.</remarks>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Used to indicate specific information retrieval words.")> _
		<Category("Category")> <FieldDisplayName("")> Public Overridable Property Keywords As String()
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.Keywords)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.Keywords, 64, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The earliest date the provider intends the object to be used.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The earliest date the provider intends the object to be used.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property ReleaseDate As Date
			Get
				Try
					Dim AllValues As List(Of Date) = CCYYMMDD_Value(DataSetIdentification.ReleaseDate)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					CCYYMMDD_Value(DataSetIdentification.ReleaseDate) = New List(Of Date)(New Date(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The earliest time the provider intends the object to be used.</summary>
		''' <remarks>This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
		''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The earliest time the provider intends the object to be used.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property ReleaseTime As Time
			Get
				Try
					Dim AllValues As List(Of Time) = HHMMSS_HHMM_Value(DataSetIdentification.ReleaseTime)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					HHMMSS_HHMM_Value(DataSetIdentification.ReleaseTime) = New List(Of Time)(New Time(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The latest date the provider or owner intends the objectdata to be used.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The latest date the provider or owner intends the objectdata to be used.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property ExpirationDate As Date
			Get
				Try
					Dim AllValues As List(Of Date) = CCYYMMDD_Value(DataSetIdentification.ExpirationDate)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					CCYYMMDD_Value(DataSetIdentification.ExpirationDate) = New List(Of Date)(New Date(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The latest time the provider or owner intends the objectdata to be used.</summary>
		''' <remarks>This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
		''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The latest time the provider or owner intends the objectdata to be used.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property ExpirationTime As Time
			Get
				Try
					Dim AllValues As List(Of Time) = HHMMSS_HHMM_Value(DataSetIdentification.ExpirationTime)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					HHMMSS_HHMM_Value(DataSetIdentification.ExpirationTime) = New List(Of Time)(New Time(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Other editorial instructions concerning the use of the objectdata, such as embargoes and warnings.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Other editorial instructions concerning the use of the objectdata, such as embargoes and warnings.")> _
		<Category("Other")> <FieldDisplayName("")> Public Overridable Property SpecialInstructions As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.SpecialInstructions)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.SpecialInstructions, 256, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Indicates the type of action that this object provides to a previous object.</summary>
		''' <remarks>The link to the previous object is made using the (DataSets 1:120 () and 1:122 ()), according to the practices of the provider.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Indicates the type of action that this object provides to a previous object.")> _
		<Category("Other")> <FieldDisplayName("")> Public Overridable Property ActionAdvised As AdvisedActions
			Get
				Try
					Dim AllValues As List(Of AdvisedActions) = ConvertEnumList(Of AdvisedActions)(Enum_NumChar_Value(DataSetIdentification.ActionAdvised, GetType(AdvisedActions)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_NumChar_Value(DataSetIdentification.ActionAdvised, GetType(AdvisedActions), 2, true) = ConvertEnumList(New List(Of AdvisedActions)(New AdvisedActions(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The date the intellectual content of the objectdata was created rather than the date of the creation of the physical representation.</summary>
		''' <remarks>Thus a photo taken during the American Civil War would carry a creation date during that epoch (1861-1865) rather than the date the photo was digitised for archiving.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The date the intellectual content of the objectdata was created rather than the date of the creation of the physical representation.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property DateCreated As OmmitableDate
			Get
				Try
					Dim AllValues As List(Of OmmitableDate) = CCYYMMDDOmmitable_Value(DataSetIdentification.DateCreated)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					CCYYMMDDOmmitable_Value(DataSetIdentification.DateCreated) = New List(Of OmmitableDate)(New OmmitableDate(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.</summary>
		''' <remarks>Where the time cannot be precisely determined, the closest approximation should be used. This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
		''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property TimeCreated As Time
			Get
				Try
					Dim AllValues As List(Of Time) = HHMMSS_HHMM_Value(DataSetIdentification.TimeCreated)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					HHMMSS_HHMM_Value(DataSetIdentification.TimeCreated) = New List(Of Time)(New Time(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The date the digital representation of the objectdata was created.</summary>
		''' <remarks>Thus a photo taken during the American Civil War would carry a Digital Creation Date within the past several years rather than the date where the image was captured on film, glass plate or other substrate during that epoch (1861-1865).</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The date the digital representation of the objectdata was created.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property DigitalCreationDate As Date
			Get
				Try
					Dim AllValues As List(Of Date) = CCYYMMDD_Value(DataSetIdentification.DigitalCreationDate)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					CCYYMMDD_Value(DataSetIdentification.DigitalCreationDate) = New List(Of Date)(New Date(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The time the digital representation of the objectdata was created.</summary>
		''' <remarks>This property should be stored in HHMMSS+HHMM format. When IPTC value was originally stored in HHMMSS format it can be red as well (but it's IPTC IIM standard violation).</remarks>
		''' <version version="1.5.3">Values in HHMMSS format can be read.</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The time the digital representation of the objectdata was created.")> _
		<Category("Date")> <FieldDisplayName("")> Public Overridable Property DigitalCreationTime As Time
			Get
				Try
					Dim AllValues As List(Of Time) = HHMMSS_HHMM_Value(DataSetIdentification.DigitalCreationTime)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					HHMMSS_HHMM_Value(DataSetIdentification.DigitalCreationTime) = New List(Of Time)(New Time(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the type of program used to originate the objectdata.</summary>
		''' <remarks>Note: This DataSet to form an advisory to the user and are not "computer" fields. Programmers should not expect to find computer-readable information in this DataSet.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the type of program used to originate the objectdata.")> _
		<Category("Other")> <FieldDisplayName("")> Public Overridable Property OriginatingProgram As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.OriginatingProgram)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.OriginatingProgram, 32, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the type of program used to originate the objectdata.</summary>
		''' <remarks>Note: This DataSet to form an advisory to the user and are not "computer" fields. Programmers should not expect to find computer-readable information in this DataSet.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the type of program used to originate the objectdata.")> _
		<Category("Other")> <FieldDisplayName("")> Public Overridable Property ProgramVersion As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.ProgramVersion)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.ProgramVersion, 10, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Virtually only used in North America.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Virtually only used in North America.")> _
		<Category("Status")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property ObjectCycle As StringEnum(Of ObjectCycleValues)
			Get
				Try
					Dim AllValues As List(Of StringEnum(Of ObjectCycleValues)) = ConvertEnumList(Of ObjectCycleValues)(StringEnum_Value(DataSetIdentification.ObjectCycle, GetType(ObjectCycleValues)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					StringEnum_Value(DataSetIdentification.ObjectCycle, GetType(ObjectCycleValues), 1, true) = ConvertEnumList(Of ObjectCycleValues)(New List(Of StringEnum(Of ObjectCycleValues))(New StringEnum(Of ObjectCycleValues)(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies city of objectdata origin according to guidelines established by the provider.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies city of objectdata origin according to guidelines established by the provider.")> _
		<Category("Location")> <FieldDisplayName("")> Public Overridable Property City As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.City)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.City, 32, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the location within a city from which the objectdata originates, according to guidelines established by the provider.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the location within a city from which the objectdata originates, according to guidelines established by the provider.")> _
		<Category("Location")> <FieldDisplayName("")> Public Overridable Property SubLocation As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.SubLocation)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.SubLocation, 32, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies Province/State of origin according to guidelines established by the provider.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies Province/State of origin according to guidelines established by the provider.")> _
		<Category("Location")> <FieldDisplayName("")> Public Overridable Property ProvinceState As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.ProvinceState)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.ProvinceState, 32, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Indicates the code of the country/primary location where the intellectual property of the objectdata was created, e.g. a photo was taken, an event occurred.</summary>
		''' <remarks>Where ISO has established an appropriate country code under ISO 3166, that code will be used. When ISO3166 does not adequately provide for identification of a location or a new country, e.g. ships at sea, space, IPTC will assign an appropriate three-character code under the provisions of ISO3166 to avoid conflicts. (see Appendix D)</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Indicates the code of the country/primary location where the intellectual property of the objectdata was created, e.g. a photo was taken, an event occurred.")> _
		<Category("Location")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property CountryPrimaryLocationCode As StringEnum(Of ISO3166)
			Get
				Try
					Dim AllValues As List(Of StringEnum(Of ISO3166)) = ConvertEnumList(Of ISO3166)(StringEnum_Value(DataSetIdentification.CountryPrimaryLocationCode, GetType(ISO3166)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					StringEnum_Value(DataSetIdentification.CountryPrimaryLocationCode, GetType(ISO3166), 3, true) = ConvertEnumList(Of ISO3166)(New List(Of StringEnum(Of ISO3166))(New StringEnum(Of ISO3166)(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Provides full, publishable, name of the country/primary location where the intellectual property of the objectdata was created, according to guidelines of the provider.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Provides full, publishable, name of the country/primary location where the intellectual property of the objectdata was created, according to guidelines of the provider.")> _
		<Category("Location")> <FieldDisplayName("")> Public Overridable Property CountryPrimaryLocationName As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.CountryPrimaryLocationName)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.CountryPrimaryLocationName, 64, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A code representing the location of original transmission according to practices of the provider.</summary>
		''' <remarks>Examples: BER-5, PAR-12-11-01</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A code representing the location of original transmission according to practices of the provider.")> _
		<Category("Location")> <FieldDisplayName("")> Public Overridable Property OriginalTransmissionReference As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.OriginalTransmissionReference)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.OriginalTransmissionReference, 32, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A publishable entry providing a synopsis of the contents of the objectdata.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A publishable entry providing a synopsis of the contents of the objectdata.")> _
		<Category("Title")> <FieldDisplayName("")> Public Overridable Property Headline As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.Headline)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.Headline, 256, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the provider of the objectdata, not necessarily the owner/creator.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the provider of the objectdata, not necessarily the owner/creator.")> _
		<Category("Author")> <FieldDisplayName("")> Public Overridable Property Credit As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.Credit)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.Credit, 32, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the original owner of the intellectual content of the objectdata.</summary>
		''' <remarks>This could be an agency, a member of an agency or an individual.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the original owner of the intellectual content of the objectdata.")> _
		<Category("Author")> <FieldDisplayName("")> Public Overridable Property Source As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.Source)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.Source, 32, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Contains any necessary copyright notice.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Contains any necessary copyright notice.")> _
		<Category("Author")> <FieldDisplayName("")> Public Overridable Property CopyrightNotice As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.CopyrightNotice)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.CopyrightNotice, 128, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the person or organisation which can provide further background information on the objectdata.</summary>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the person or organisation which can provide further background information on the objectdata.")> _
		<Category("Author")> <FieldDisplayName("")> Public Overridable Property Contact As String()
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.Contact)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.Contact, 128, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A textual description of the objectdata, particularly used where the object is not text.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A textual description of the objectdata, particularly used where the object is not text.")> _
		<Category("Title")> <FieldDisplayName("")> <Editor(gettype(System.ComponentModel.Design.MultilineStringEditor),gettype(System.Drawing.Design.UITypeEditor))> <TypeConverter(gettype(MultilineStringConverter ))> Public Overridable Property CaptionAbstract As String
			Get
				Try
					Dim AllValues As List(Of String) = Text_Value(DataSetIdentification.CaptionAbstract)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Text_Value(DataSetIdentification.CaptionAbstract, 2000, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identification of the name of the person involved in the writing, editing or correcting the objectdata or caption/abstract.</summary>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identification of the name of the person involved in the writing, editing or correcting the objectdata or caption/abstract.")> _
		<Category("Author")> <FieldDisplayName("")> Public Overridable Property WriterEditor As String()
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.WriterEditor)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.WriterEditor, 32, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Image width 460 pixels and image height 128 pixels. Scanning direction bottom to top, left to right.</summary>
		''' <remarks>Contains the rasterized objectdata description and is used where characters that have not been coded are required for the caption.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Image width 460 pixels and image height 128 pixels. Scanning direction bottom to top, left to right.")> _
		<Category("Title")> <FieldDisplayName("")> <Editor(GetType(EmbededImageEditor), GetType(System.Drawing.Design.UITypeEditor))> Public Overridable Property RasterizedeCaption As Drawing.Bitmap
			Get
				Try
					Dim AllValues As List(Of Drawing.Bitmap) = BW460_Value(DataSetIdentification.RasterizedeCaption)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					BW460_Value(DataSetIdentification.RasterizedeCaption, 7360, true) = New List(Of Drawing.Bitmap)(New Drawing.Bitmap(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Image Type</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Image Type")> _
		<Category("Image")> <FieldDisplayName("")> Public Overridable Property ImageType As iptcImageType
			Get
				Try
					Dim AllValues As List(Of iptcImageType) = ImageType_Value(DataSetIdentification.ImageType)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					ImageType_Value(DataSetIdentification.ImageType) = New List(Of iptcImageType)(New iptcImageType(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Indicates the layout of the image area.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Indicates the layout of the image area.")> _
		<Category("Image")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property ImageOrientation As StringEnum(Of Orientations)
			Get
				Try
					Dim AllValues As List(Of StringEnum(Of Orientations)) = ConvertEnumList(Of Orientations)(StringEnum_Value(DataSetIdentification.ImageOrientation, GetType(Orientations)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					StringEnum_Value(DataSetIdentification.ImageOrientation, GetType(Orientations), 1, true) = ConvertEnumList(Of Orientations)(New List(Of StringEnum(Of Orientations))(New StringEnum(Of Orientations)(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Describes the major national language of the object, according to the 2-letter codes of ISO 639:1988.</summary>
		''' <remarks>Does not define or imply any coded character set, but is used for internal routing, e.g. to various editorial desks. Implementation note: Programmers should provide for three octets for Language Identifier because the ISO is expected to provide for 3-letter codes in the future.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Describes the major national language of the object, according to the 2-letter codes of ISO 639:1988.")> _
		<Category("Other")> <FieldDisplayName("")> Public Overridable Property LanguageIdentifier As String
			Get
				Try
					Dim AllValues As List(Of String) = Alpha_Value(DataSetIdentification.LanguageIdentifier)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Alpha_Value(DataSetIdentification.LanguageIdentifier, 135, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Type of audio in objectdata</summary>
		''' <remarks>Note: When '0' or 'T' is used, the only authorised combination is "0T". This is the mechanism for sending a caption either to supplement an audio cut sent previously without a caption or to correct a previously sent caption.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Type of audio in objectdata")> _
		<Category("Audio")> <FieldDisplayName("")> Public Overridable Property AudioType As iptcAudioType
			Get
				Try
					Dim AllValues As List(Of iptcAudioType) = Audiotype_Value(DataSetIdentification.AudioType)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Audiotype_Value(DataSetIdentification.AudioType) = New List(Of iptcAudioType)(New iptcAudioType(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Sampling rate, representing the sampling rate in hertz (Hz).</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Sampling rate, representing the sampling rate in hertz (Hz).")> _
		<Category("Audio")> <FieldDisplayName("")> Public Overridable Property AudioSamplingRate As Decimal
			Get
				Try
					Dim AllValues As List(Of Decimal) = NumericChar_Value(DataSetIdentification.AudioSamplingRate)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					NumericChar_Value(DataSetIdentification.AudioSamplingRate, 6, true) = New List(Of Decimal)(New Decimal(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The number of bits in each audio sample.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The number of bits in each audio sample.")> _
		<Category("Audio")> <FieldDisplayName("")> Public Overridable Property AudioSamplingResolution As Decimal
			Get
				Try
					Dim AllValues As List(Of Decimal) = NumericChar_Value(DataSetIdentification.AudioSamplingResolution)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					NumericChar_Value(DataSetIdentification.AudioSamplingResolution, 2, true) = New List(Of Decimal)(New Decimal(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The running time of an audio objectdata when played back at the speed at which it was recorded.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The running time of an audio objectdata when played back at the speed at which it was recorded.")> _
		<Category("Audio")> <FieldDisplayName("")> Public Overridable Property AudioDuration As TimeSpan
			Get
				Try
					Dim AllValues As List(Of TimeSpan) = HHMMSS_Value(DataSetIdentification.AudioDuration)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					HHMMSS_Value(DataSetIdentification.AudioDuration) = New List(Of TimeSpan)(New TimeSpan(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the content of the end of an audio objectdata, according to guidelines established by the provider.</summary>
		''' <remarks>The outcue generally consists of the final words spoken within an audio objectdata or the final sounds heard.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the content of the end of an audio objectdata, according to guidelines established by the provider.")> _
		<Category("Audio")> <FieldDisplayName("")> Public Overridable Property AudioOutcue As String
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.AudioOutcue)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.AudioOutcue, 64, false) = New List(Of String)(New String(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A binary number representing the version of the Digital Newsphoto Parameter Record utilised by the provider.</summary>
		''' <remarks>Version numbers are assigned by IPTC and NAA. The version of this record is four (4). Same tag called "Record Version" also exists in record no. 2.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A binary number representing the version of the Digital Newsphoto Parameter Record utilised by the provider.")> _
		<Category("Internal")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property RecordVersion3 As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.RecordVersion3)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.RecordVersion3) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The picture number provides a universally unique reference to an image.</summary>
		''' <remarks>For example, colour images, when split with colour components into multiple objects, i.e. envelopes, would carry the same Picture Number.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The picture number provides a universally unique reference to an image.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> Public Overridable Property PictureNumber As iptcPictureNumber
			Get
				Try
					Dim AllValues As List(Of iptcPictureNumber) = PictureNumber_Value(DataSetIdentification.PictureNumber)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					PictureNumber_Value(DataSetIdentification.PictureNumber) = New List(Of iptcPictureNumber)(New iptcPictureNumber(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number representing the number of pixels in a scan line for the component with the highest resolution.</summary>
		''' <remarks>Not valid when DataSet 3:60 () octet zero (0) is 0 (zero).NCPS values are 1024 or 2048.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number representing the number of pixels in a scan line for the component with the highest resolution.")> _
		<Category("Image 3")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property PixelsPerLine As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.PixelsPerLine)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.PixelsPerLine) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number representing the number of scan lines comprising the image for the component with the highest resolution.</summary>
		''' <remarks>Not valid when DataSet 3:60 () octet zero (0) is 0 (zero).NCPS range is 1 to 2048.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number representing the number of scan lines comprising the image for the component with the highest resolution.")> _
		<Category("Image 3")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property NumberOfLines As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.NumberOfLines)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.NumberOfLines) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number indicating the number of pixels per unit length in the scanning direction.</summary>
		''' <remarks>Not valid when DataSet 3:60 () octet zero (0) is 0 (zero). NCPS value is 1.Pixel Size is a relative size expressed as the ratio of 3:40 to 3:50 (3:40/3:50).</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number indicating the number of pixels per unit length in the scanning direction.")> _
		<Category("Image 3")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property PixelSizeInScanningDirection As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.PixelSizeInScanningDirection)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.PixelSizeInScanningDirection) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number indicating the number of pixels per unit length perpendicular to the scanning direction.</summary>
		''' <remarks>Not valid when DataSet 3:60 () octet zero (0) is 0 (zero). NCPS value is 1.Pixel Size is a relative size expressed as the ratio of 3:40 to 3:50 (3:40/3:50).</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number indicating the number of pixels per unit length perpendicular to the scanning direction.")> _
		<Category("Image 3")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property PixelSizePerpendicularToScanningDirection As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.PixelSizePerpendicularToScanningDirection)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.PixelSizePerpendicularToScanningDirection) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number indicating the image content.</summary>
		''' <remarks>Mandatory when the numeric character in DataSet 2:130 () is ‘9’ ()</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number indicating the image content.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property SupplementType As SupplementTypeValue
			Get
				Try
					Dim AllValues As List(Of SupplementTypeValue) = ConvertEnumList(Of SupplementTypeValue)(Enum_Binary_Value(DataSetIdentification.SupplementType, GetType(SupplementTypeValue)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.SupplementType, GetType(SupplementTypeValue)) = ConvertEnumList(New List(Of SupplementTypeValue)(New SupplementTypeValue(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Indicates colour representation</summary>
		''' <remarks>The special interleaving structures refer to defined methods given in Appendix G according to the value in the sampling structure DataSet 3:90. Allowed combinations are: 0,01,03,0 - 4,0 (In a single-frame colour image, a colour is described with a single sample per pixel.)3,1 - 3,2 - 3,3 - 3,4 - 3,54,1 - 4,2 - 4,3 - 4,4 - 4,5NCPS values are 1,0 , 3,1 or 3,5.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Indicates colour representation")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property ColourRepresentation As ColourRepresentations
			Get
				Try
					Dim AllValues As List(Of ColourRepresentations) = ConvertEnumList(Of ColourRepresentations)(Enum_Binary_Value(DataSetIdentification.ColourRepresentation, GetType(ColourRepresentations)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.ColourRepresentation, GetType(ColourRepresentations)) = ConvertEnumList(New List(Of ColourRepresentations)(New ColourRepresentations(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A value indicating the colour space in which the pixel values are expressed for each component in the image.</summary>
		''' <remarks>Mandatory if DataSet 3:60 octet zero (0) has a value greater than one, i.e. a multi-component image. Not valid when DataSet 3:60 octet zero (0) is 0 (zero). NCPS values are 0, 4 or 7 (, or ).</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A value indicating the colour space in which the pixel values are expressed for each component in the image.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property InterchangeColourSpace As ColourSpaceValue
			Get
				Try
					Dim AllValues As List(Of ColourSpaceValue) = ConvertEnumList(Of ColourSpaceValue)(Enum_Binary_Value(DataSetIdentification.InterchangeColourSpace, GetType(ColourSpaceValue)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.InterchangeColourSpace, GetType(ColourSpaceValue)) = ConvertEnumList(New List(Of ColourSpaceValue)(New ColourSpaceValue(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Each of 1 to four octets contains a binary number that relates to the colour component using the identification number assigned to it in the appendix for each colour space.</summary>
		''' <remarks>The sequence specifies the sequence of the components as they appear in the objectdataFor frame sequential components, only one octet is set to identify the current colour component in the objectdata.Mandatory if DataSet 3:60 octet zero (0) has a value greater than one, i.e. a multi-component image AND the value of octet one (1) is 0, 1, 2, 3, or 4, i.e. single frame, frame sequential, line sequential or pixel sequential. Not valid when DataSet 3:60 octet zero (0) is 0 (zero).Allowed values are: 0 - 1 - 2 - 3 - 4; 0 - may be used for “monochrome” or “no image” representations. Other values are reserved.NCPS values are 0,1,2,3 or 123.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Each of 1 to four octets contains a binary number that relates to the colour component using the identification number assigned to it in the appendix for each colour space.")> _
		<Category("Image 3")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property ColourSequence As ULong
			Get
				Try
					Dim AllValues As List(Of ULong) = UnsignedBinaryNumber_Value(DataSetIdentification.ColourSequence)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UnsignedBinaryNumber_Value(DataSetIdentification.ColourSequence) = New List(Of ULong)(New ULong(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Specifies the International Color Consortium profile for the scanning/source device used to generate the digital image files.</summary>
		''' <remarks>Valid when DataSet 3:64 has values 1,2,4,5 or 8.This profile can be used to translate the image colour information from the input device colour space into another device's native colour space. The ICC profile is specified in ISO/TC 130/WG2N562.Maximul alloved lenght of this field according to IPCT IIM standard is 512K. However current implementation does not allow such big fields.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Specifies the International Color Consortium profile for the scanning/source device used to generate the digital image files.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property IccInputColourProfile As Byte()
			Get
				Try
					Dim AllValues As List(Of Byte()) = ByteArray_Value(DataSetIdentification.IccInputColourProfile)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Bytearray_Value(DataSetIdentification.IccInputColourProfile, 0, false) = New List(Of Byte())(New Byte()(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>This DataSet is no longer required as its contents have been rendered obsolete by the introduction of DataSet P3:66 (ICC Input Colour Profile).</summary>
		''' <remarks>This DataSet will be removed from the next Version of this Standard.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("This DataSet is no longer required as its contents have been rendered obsolete by the introduction of DataSet P3:66 (ICC Input Colour Profile).")> _
		<Category("Image 3")> <FieldDisplayName("")> <Obsolete("This DataSet is no longer required as its contents have been rendered obsolete by the introduction of DataSet P3:66 (ICC Input Colour Profile).")> Public Overridable Property ColourCalibrationMatrixTable As Byte()
			Get
				Try
					Dim AllValues As List(Of Byte()) = ByteArray_Value(DataSetIdentification.ColourCalibrationMatrixTable)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Bytearray_Value(DataSetIdentification.ColourCalibrationMatrixTable, 0, false) = New List(Of Byte())(New Byte()(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Consists of one, three or four one-dimensional lookup tables (LUT).</summary>
		''' <remarks>The LUT relates to the image data in the colour space defined in DataSet 3:64 and specifies the correction to apply to the pixel values before display or printing of the image. Not applicable if the colour space requires converting before display or printing. Maximul alloved lenght of this field according to IPCT IIM standard is 131072. However current implementation does not allow such big fields.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Consists of one, three or four one-dimensional lookup tables (LUT).")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property LookupTable As Byte()
			Get
				Try
					Dim AllValues As List(Of Byte()) = ByteArray_Value(DataSetIdentification.LookupTable)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Bytearray_Value(DataSetIdentification.LookupTable, 0, false) = New List(Of Byte())(New Byte()(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A binary number representing the number of index entries in the DataSet 3:85 (<see cref="ColourPalette"/>).</summary>
		''' <remarks>Mandatory where DataSet 3:60 octet zero (0) has a value greater than one, i.e. a multi-component image AND the value of octet one (1) is 0, i.e. single-frame. Not relevant for other image types.0 - No Colour Palette contained in DataSet 3:85. A default palette should be used. 1 - 65535 - valid numbers.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A binary number representing the number of index entries in the DataSet 3:85 ( Colour Pallette).")> _
		<Category("Image 3")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property NumberOfIndexEntries As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.NumberOfIndexEntries)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.NumberOfIndexEntries) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>In a single-frame colour image, a colour is described with a single sample per pixel.</summary>
		''' <remarks>Mandatory if 3:84 exists and is non zero.The pixel value is used as an index into the Colour Palette.The purpose of the Colour Palette is to act as a lookup table mapping the pixel values into the Colour Space defined in 3:64.The number of index entries is defined in DataSet 3:84.The number of output values is defined in octet zero of DataSet 3:60.The number of octets used for each output value is deducted from 3:135.The colour sequence of the output values is defined in 3:65. A default palette may be referenced if this DataSet is omitted. A number of default palettes may be held to be selected according to the device identifier component of the Picture Number 3:10.Maximul alloved lenght of this field according to IPCT IIM standard is 524288. However current implementation does not allow such big fields.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("In a single-frame colour image, a colour is described with a single sample per pixel.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property ColourPalette As Byte()
			Get
				Try
					Dim AllValues As List(Of Byte()) = ByteArray_Value(DataSetIdentification.ColourPalette)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Bytearray_Value(DataSetIdentification.ColourPalette, 0, false) = New List(Of Byte())(New Byte()(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number between 1 and 16 that indicates the number of bits per pixel value used as entries in the Colour Palette. These values are found in the objectdata itself.</summary>
		''' <remarks>Mandatory if DataSet 3:60 octet zero (0) has a value greater than one, i.e. a multi-component image AND the value of octet one (1) is 0, i.e. single frame. Not relevant for other image types.Each entry should be in one octet if number of bits is less than or equal to 8 and in two octets if number of bits is between 9 and 16, the least significant bit should always be aligned on the least significant bit of the least significant octet.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number between 1 and 16 that indicates the number of bits per pixel value used as entries in the Colour Palette. These values are found in the objectdata itself.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property NumberOfBitsPerSample As Byte
			Get
				Try
					Dim AllValues As List(Of Byte) = Byte_Binary_Value(DataSetIdentification.NumberOfBitsPerSample)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Byte_Binary_Value(DataSetIdentification.NumberOfBitsPerSample) = New List(Of Byte)(New Byte(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number defining the spatial and temporal relationship between pixels.</summary>
		''' <remarks>NCPS values are 0 or 2.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number defining the spatial and temporal relationship between pixels.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property SamplingStructure As SamplingStructureType
			Get
				Try
					Dim AllValues As List(Of SamplingStructureType) = ConvertEnumList(Of SamplingStructureType)(Enum_Binary_Value(DataSetIdentification.SamplingStructure, GetType(SamplingStructureType)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.SamplingStructure, GetType(SamplingStructureType)) = ConvertEnumList(New List(Of SamplingStructureType)(New SamplingStructureType(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number indicating the correct relative two dimensional order of the pixels in the objectdata. Eight possibilities exist.</summary>
		''' <remarks>Not valid when DataSet 3:60 octet zero (0) is 0 (zero).NCPS value is 0.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number indicating the correct relative two dimensional order of the pixels in the objectdata. Eight possibilities exist.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property ScanningDirection As ScanningDirectionValue
			Get
				Try
					Dim AllValues As List(Of ScanningDirectionValue) = ConvertEnumList(Of ScanningDirectionValue)(Enum_Binary_Value(DataSetIdentification.ScanningDirection, GetType(ScanningDirectionValue)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.ScanningDirection, GetType(ScanningDirectionValue)) = ConvertEnumList(New List(Of ScanningDirectionValue)(New ScanningDirectionValue(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number indicating the clockwise rotation applied to the image for presentation.</summary>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number indicating the clockwise rotation applied to the image for presentation.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property ImageRotation As ImageRotationValue
			Get
				Try
					Dim AllValues As List(Of ImageRotationValue) = ConvertEnumList(Of ImageRotationValue)(Enum_Binary_Value(DataSetIdentification.ImageRotation, GetType(ImageRotationValue)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.ImageRotation, GetType(ImageRotationValue)) = ConvertEnumList(New List(Of ImageRotationValue)(New ImageRotationValue(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Specifies data compression method</summary>
		''' <remarks>Not valid when DataSet 3:60 octet zero (0) is 0 (zero).Octets 0-1 contain a binary number identifying the providerowner of the algorithm.Octet 2 contains a binary number identifying the type of compression algorithm.Octet 3 contains a binary number identifying the revision number of the algorithm.An identification number is issued by IPTC-NAA to providersowners of compression algorithms upon request (see Appendix A). The numbers identifying type and revision of algorithms are managed by the providers-owners.A zero (0) value of all octets in this DataSet identifies an uncompressed image. In this case the component values should be in one octet if number of bits is less than or equal to 8 and in two octets if number of bits is between 9 and 16, the least significant bit always being aligned on the least significant bit of the least significant octet.NCPS values are 0000 or 0121.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Specifies data compression method")> _
		<Category("Image 3")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property DataCompressionMethod As UInteger
			Get
				Try
					Dim AllValues As List(Of UInteger) = UInt_Binary_Value(DataSetIdentification.DataCompressionMethod)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UInt_Binary_Value(DataSetIdentification.DataCompressionMethod) = New List(Of UInteger)(New UInteger(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Contains a binary number identifying the quantisation law.</summary>
		''' <remarks>The relations between different quantisation methods are described in DNPR Guideline 1. Not valid when DataSet 3:60 octet zero (0) is 0 (zero).NCPS values are 0 or 5.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Contains a binary number identifying the quantisation law.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property QuantisationMethod As QuantisationMethodValue
			Get
				Try
					Dim AllValues As List(Of QuantisationMethodValue) = ConvertEnumList(Of QuantisationMethodValue)(Enum_Binary_Value(DataSetIdentification.QuantisationMethod, GetType(QuantisationMethodValue)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.QuantisationMethod, GetType(QuantisationMethodValue)) = ConvertEnumList(New List(Of QuantisationMethodValue)(New QuantisationMethodValue(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>These end points apply to the coding process.</summary>
		''' <remarks>2n octets. Valid only when DataSet 3:64 has a value of 0,1,2,4 or 5. Not relevant for other colour spaces. n = the number of octets per component as derived from 3:135 multiplied by the number of components. The number of components is 1 when octet one (1) of DataSet 3:60 has a value of one (1), in all other cases the number is defined in octet zero (0) of DataSet 3:60.The first n octets contain the values representing the minimum density that is encoded for each component in the order specified in DataSet 3:65.The second n octets contain the values representing the maximum density that is encoded for each component in the order specified in DataSet 3:65.The difference between the maximum and minimum density for every component is the same and given by the Maximum Density Range value in DataSet 3:140.NCPS end point values are 255 and 0.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("These end points apply to the coding process.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property EndPoints As Byte()
			Get
				Try
					Dim AllValues As List(Of Byte()) = ByteArray_Value(DataSetIdentification.EndPoints)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Bytearray_Value(DataSetIdentification.EndPoints, 0, false) = New List(Of Byte())(New Byte()(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Indicates if values outside the range defined by the end points in DataSet 3:125 may occur.</summary>
		''' <remarks>NCPS value is false</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Indicates if values outside the range defined by the end points in DataSet 3:125 may occur.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property ExcursionTolerance As Boolean
			Get
				Try
					Dim AllValues As List(Of Boolean) = Boolean_Binary_Value(DataSetIdentification.ExcursionTolerance)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Boolean_Binary_Value(DataSetIdentification.ExcursionTolerance, 1) = New List(Of Boolean)(New Boolean(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Contains a sequence of one or more octets describing the number of bits used to encode each component. The sequence is specified by the order of components in DataSet 3:65.</summary>
		''' <remarks>The number of octets is 1 when octet one (1) of DataSet 3:60 has a value of one (1), in all other cases the number of octets is equivalent to the number of components specified in octet zero (0) of DataSet 3:60.Each octet contains a binary value between one and 16.NCPS values are 8 or 888.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Contains a sequence of one or more octets describing the number of bits used to encode each component. The sequence is specified by the order of components in DataSet 3:65.")> _
		<Category("Image 3")> <FieldDisplayName("")> Public Overridable Property BitsPerComponent As Byte()
			Get
				Try
					Dim AllValues As List(Of Byte()) = ByteArray_Value(DataSetIdentification.BitsPerComponent)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Bytearray_Value(DataSetIdentification.BitsPerComponent, 0, 0) = New List(Of Byte())(New Byte()(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A binary value which specifies the maximum density range multiplied by 100.</summary>
		''' <remarks>Not valid when DataSet 3:60 octet zero (0) is 0 (zero).The value represents the difference between the lowest density and the highest density points that can be encoded by the originating system.NCPS value is 160.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A binary value which specifies the maximum density range multiplied by 100.")> _
		<Category("Image 3")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property MaximumDensityRange As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.MaximumDensityRange)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.MaximumDensityRange) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A binary value which specifies the value of gamma for the device multiplied by 100.</summary>
		''' <remarks>Valid only when DataSet 3:120 has a value of 5 or 7.If this DataSet is omitted receiving equipment should assume that a gamma value of 2.22 appliesNCPS value is 222</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A binary value which specifies the value of gamma for the device multiplied by 100.")> _
		<Category("Image 3")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property GammaCompensatedValue As UShort
			Get
				Try
					Dim AllValues As List(Of UShort) = UShort_Binary_Value(DataSetIdentification.GammaCompensatedValue)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UShort_Binary_Value(DataSetIdentification.GammaCompensatedValue) = New List(Of UShort)(New UShort(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Overll rating of the subject</summary>
		''' <remarks>This property is not part of IPTC IIM standard and other software may ignore or remove it.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Overll rating of the subject")> _
		<Category("Status")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property OverlallRating As CustomRating
			Get
				Try
					Dim AllValues As List(Of CustomRating) = ConvertEnumList(Of CustomRating)(Enum_Binary_Value(DataSetIdentification.OverlallRating, GetType(CustomRating)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.OverlallRating, GetType(CustomRating)) = ConvertEnumList(New List(Of CustomRating)(New CustomRating(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Rates technical quality of subject data (e.g. Is the photo sharp?)</summary>
		''' <remarks>This property is not part of IPTC IIM standard and other software may ignore or remove it.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Rates technical quality of subject data (e.g. Is the photo sharp?)")> _
		<Category("Status")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property TechnicalQuality As CustomRating
			Get
				Try
					Dim AllValues As List(Of CustomRating) = ConvertEnumList(Of CustomRating)(Enum_Binary_Value(DataSetIdentification.TechnicalQuality, GetType(CustomRating)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.TechnicalQuality, GetType(CustomRating)) = ConvertEnumList(New List(Of CustomRating)(New CustomRating(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Rates artistic quality of subject data (i.e. How nice it is?)</summary>
		''' <remarks>This property is not part of IPTC IIM standard and other software may ignore or remove it.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Rates artistic quality of subject data (i.e. How nice it is?)")> _
		<Category("Status")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property ArtQuality As CustomRating
			Get
				Try
					Dim AllValues As List(Of CustomRating) = ConvertEnumList(Of CustomRating)(Enum_Binary_Value(DataSetIdentification.ArtQuality, GetType(CustomRating)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.ArtQuality, GetType(CustomRating)) = ConvertEnumList(New List(Of CustomRating)(New CustomRating(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Rates information value of subject data (i.e. Does it provide any valuable information?)</summary>
		''' <remarks>This property is not part of IPTC IIM standard and other software may ignore or remove it.</remarks>
		''' <version version="1.5.4">This property is new in version 1.5.4</version>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Rates information value of subject data (i.e. Does it provide any valuable information?)")> _
		<Category("Status")> <FieldDisplayName("")> <CLSCompliant(False)>Public Overridable Property InformationValue As CustomRating
			Get
				Try
					Dim AllValues As List(Of CustomRating) = ConvertEnumList(Of CustomRating)(Enum_Binary_Value(DataSetIdentification.InformationValue, GetType(CustomRating)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.InformationValue, GetType(CustomRating)) = ConvertEnumList(New List(Of CustomRating)(New CustomRating(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.")> _
		<Category("Embeded object")> <FieldDisplayName("")> <[ReadOnly](True)> Public Overridable Property SizeMode As Boolean
			Get
				Try
					Dim AllValues As List(Of Boolean) = Boolean_Binary_Value(DataSetIdentification.SizeMode)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Boolean_Binary_Value(DataSetIdentification.SizeMode, 1) = New List(Of Boolean)(New Boolean(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The maximum size for the following Subfile DataSet(s).</summary>
		''' <remarks>The largest number is not defined, but programmers should provide at least for the largest binary number contained in four octets taken together. If the entire object is to be transferred together within a single DataSet 8:10, the number equals the size of the object.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The maximum size for the following Subfile DataSet(s).")> _
		<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)><[ReadOnly](True)> Public Overridable Property MaxSubfileSize As ULong
			Get
				Try
					Dim AllValues As List(Of ULong) = UnsignedBinaryNumber_Value(DataSetIdentification.MaxSubfileSize)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UnsignedBinaryNumber_Value(DataSetIdentification.MaxSubfileSize) = New List(Of ULong)(New ULong(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A binary number representing the overall size of the objectdata, expressed in octets, not including tags, if that size is known when transfer commences.</summary>
		''' <remarks>Mandatory if DataSet has value '1' and not allowed if DataSet has value '0'.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A binary number representing the overall size of the objectdata, expressed in octets, not including tags, if that size is known when transfer commences.")> _
		<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)><[ReadOnly](True)> Public Overridable Property ObjectDataSizeAnnounced As ULong
			Get
				Try
					Dim AllValues As List(Of ULong) = UnsignedBinaryNumber_Value(DataSetIdentification.ObjectDataSizeAnnounced)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UnsignedBinaryNumber_Value(DataSetIdentification.ObjectDataSizeAnnounced) = New List(Of ULong)(New ULong(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Used when objectdata size is not known, indicating the largest size, expressed in octets, that the objectdata can possibly have, not including tags.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Used when objectdata size is not known, indicating the largest size, expressed in octets, that the objectdata can possibly have, not including tags.")> _
		<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)><[ReadOnly](True)> Public Overridable Property MaximumObjectDataSize As ULong
			Get
				Try
					Dim AllValues As List(Of ULong) = UnsignedBinaryNumber_Value(DataSetIdentification.MaximumObjectDataSize)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UnsignedBinaryNumber_Value(DataSetIdentification.MaximumObjectDataSize) = New List(Of ULong)(New ULong(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Subfile DataSet containing the objectdata itself.</summary>
		''' <remarks>Subfiles must be sequential so that the subfiles may be reassembled.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Subfile DataSet containing the objectdata itself.")> _
		<Category("Embeded object")> <FieldDisplayName("")> <Editor(GetType(EmbededFileEditor), GetType(Drawing.Design.UITypeEditor))> <TypeConverter(GetType(FileByteConverter))> Public Overridable Property Subfile As Byte()
			Get
				Try
					Dim AllValues As List(Of Byte()) = ByteArray_Value(DataSetIdentification.Subfile)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Bytearray_Value(DataSetIdentification.Subfile, 0, false) = New List(Of Byte())(New Byte()(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Total size of the objectdata, in octets, without tags.</summary>
		''' <remarks>This number should equal the number in DataSet if the size of the objectdata is known and has been provided.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Total size of the objectdata, in octets, without tags.")> _
		<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)><[ReadOnly](True)> Public Overridable Property ConfirmedObjectDataSize As ULong
			Get
				Try
					Dim AllValues As List(Of ULong) = UnsignedBinaryNumber_Value(DataSetIdentification.ConfirmedObjectDataSize)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					UnsignedBinaryNumber_Value(DataSetIdentification.ConfirmedObjectDataSize) = New List(Of ULong)(New ULong(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
#Region "Grouped" 'Those propertiers can be accessed via groups, do not use them directly!
		''' <summary>The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ARMIdentifier As ARMMethods
			Get
				Try
					Dim AllValues As List(Of ARMMethods) = ConvertEnumList(Of ARMMethods)(Enum_Binary_Value(DataSetIdentification.ARMIdentifier, GetType(ARMMethods)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.ARMIdentifier, GetType(ARMMethods)) = ConvertEnumList(New List(Of ARMMethods)(New ARMMethods(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentifier'/>.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentifier'/>.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ARMVersion As ARMVersions
			Get
				Try
					Dim AllValues As List(Of ARMVersions) = ConvertEnumList(Of ARMVersions)(Enum_Binary_Value(DataSetIdentification.ARMVersion, GetType(ARMVersions)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.ARMVersion, GetType(ARMVersions)) = ConvertEnumList(New List(Of ARMVersions)(New ARMVersions(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Indicates the code of a country/geographical location referenced by the content of the object.</summary>
		''' <remarks>Where ISO has established an appropriate country code under ISO 3166, that code will be used. When ISO3166 does not adequately provide for identification of a location or a country, e.g. ships at sea, space, IPTC will assign an appropriate threecharacter code under the provisions of ISO3166 to avoid conflicts. (see Appendix D) .</remarks>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Indicates the code of a country/geographical location referenced by the content of the object.")> _
		<Category("Location")> <FieldDisplayName("")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ContentLocationCode As StringEnum(Of ISO3166)()
			Get
				Try
					Dim AllValues As List(Of StringEnum(Of ISO3166)) = ConvertEnumList(Of ISO3166)(StringEnum_Value(DataSetIdentification.ContentLocationCode, GetType(ISO3166)))
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					StringEnum_Value(DataSetIdentification.ContentLocationCode, GetType(ISO3166), 3, true) = ConvertEnumList(Of ISO3166)(New List(Of StringEnum(Of ISO3166))(value))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.</summary>
		''' <remarks>If used in the same object with DataSet , must immediately follow and correspond to it.</remarks>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.")> _
		<Category("Location")> <FieldDisplayName("")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ContentLocationName As String()
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.ContentLocationName)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.ContentLocationName, 64, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the Service Identifier of a prior envelope to which the current object refers.</summary>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the Service Identifier of a prior envelope to which the current object refers.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ReferenceService As String()
			Get
				Try
					Dim AllValues As List(Of String) = GraphicCharacters_Value(DataSetIdentification.ReferenceService)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					GraphicCharacters_Value(DataSetIdentification.ReferenceService, 10, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the date of a prior envelope to which the current object refers.</summary>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the date of a prior envelope to which the current object refers.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ReferenceDate As Date()
			Get
				Try
					Dim AllValues As List(Of Date) = CCYYMMDD_Value(DataSetIdentification.ReferenceDate)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					CCYYMMDD_Value(DataSetIdentification.ReferenceDate) = New List(Of Date)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Identifies the Envelope Number of a prior envelope to which the current object refers.</summary>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Identifies the Envelope Number of a prior envelope to which the current object refers.")> _
		<Category("Old IPTC")> <FieldDisplayName("")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ReferenceNumber As Decimal()
			Get
				Try
					Dim AllValues As List(Of Decimal) = NumericChar_Value(DataSetIdentification.ReferenceNumber)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					NumericChar_Value(DataSetIdentification.ReferenceNumber, 8, true) = New List(Of Decimal)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.</summary>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.")> _
		<Category("Author")> <FieldDisplayName("")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ByLine As String()
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.ByLine)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.ByLine, 32, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.</summary>
		''' <remarks>Examples: "Staff Photographer", "Corresponsal", "Envoyé Spécial"</remarks>
		''' <returns>If this instance contains this tag(s) retuns them. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.")> _
		<Category("Author")> <FieldDisplayName("")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ByLineTitle As String()
			Get
				Try
					Dim AllValues As List(Of String) = TextWithSpaces_Value(DataSetIdentification.ByLineTitle)
					If AllValues Is Nothing OrElse AllValues.Count = 0 Then Return Nothing
					Return AllValues.ToArray
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					TextWithSpaces_Value(DataSetIdentification.ByLineTitle, 32, false) = New List(Of String)(value)
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The file format of the ObjectData Preview.</summary>
		''' <remarks>The file format must be registered with IPTC or NAA with a unique number assigned to it.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The file format of the ObjectData Preview.")> _
		<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ObjectDataPreviewFileFormat As FileFormats
			Get
				Try
					Dim AllValues As List(Of FileFormats) = ConvertEnumList(Of FileFormats)(Enum_Binary_Value(DataSetIdentification.ObjectDataPreviewFileFormat, GetType(FileFormats)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.ObjectDataPreviewFileFormat, GetType(FileFormats)) = ConvertEnumList(New List(Of FileFormats)(New FileFormats(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>The particular version of the ObjectData Preview File Format specified in <see cref="ObjectDataPreviewFileFormat"/></summary>
		''' <remarks>The File Format Version is taken from the list included in Appendix A</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The particular version of the ObjectData Preview File Format specified in ObjectData Preview File Format")> _
		<Category("Embeded object")> <FieldDisplayName("")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ObjectDataPreviewFileFormatVersion As FileFormatVersions
			Get
				Try
					Dim AllValues As List(Of FileFormatVersions) = ConvertEnumList(Of FileFormatVersions)(Enum_Binary_Value(DataSetIdentification.ObjectDataPreviewFileFormatVersion, GetType(FileFormatVersions)))
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Enum_Binary_Value(DataSetIdentification.ObjectDataPreviewFileFormatVersion, GetType(FileFormatVersions)) = ConvertEnumList(New List(Of FileFormatVersions)(New FileFormatVersions(){value}))
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
		''' <summary>Maximum size of 256000 octets consisting of binary data.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Maximum size of 256000 octets consisting of binary data.")> _
		<Category("Embeded object")> <FieldDisplayName("")> <Editor(GetType(EmbededFileEditor), GetType(Drawing.Design.UITypeEditor))> <TypeConverter(GetType(FileByteConverter))> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ObjectDataPreviewData As Byte()
			Get
				Try
					Dim AllValues As List(Of Byte()) = ByteArray_Value(DataSetIdentification.ObjectDataPreviewData)
					If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
				Catch ex As Exception
					Throw New IPTCGetException(ex)
				End Try
			End Get
			Set
				Try
					Bytearray_Value(DataSetIdentification.ObjectDataPreviewData, 0, false) = New List(Of Byte())(New Byte()(){value})
				Catch ex As Exception
					Throw New IPTCSetException(ex)
				End Try
			End Set
		End Property
#End Region
	End Class
#End Region
#Region "GetTypedValue"
	Partial Class Iptc
		''' <summary>Gets value of given dataset in expected type for that dataset</summary>
		''' <param name="Item">Identifies data set by its record number and data set number</param>
		''' <returns>Value of property identified by <paramref name="Item"/>. If there is not property appropriate for <paramref name="item"/> value is returned as string. If there is no such value, null is returned.</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid according to its type (if it is know tag) or as string (if it is unknown tag).</exception>
		''' <version version="1.5.2">Function introduced</version>
		Public Function GetTypedValue(Item as DataSetIdentification) As Object
			If Me.Contains(Item) = 0 Then Return Nothing
			Select Case Item
				Case DataSetIdentification.ModelVersion : Return Me.ModelVersion
				Case DataSetIdentification.Destination : Return Me.Destination
				Case DataSetIdentification.FileFormat : Return Me.FileFormat
				Case DataSetIdentification.FileFormatVersion : Return Me.FileFormatVersion
				Case DataSetIdentification.ServiceIdentifier : Return Me.ServiceIdentifier
				Case DataSetIdentification.EnvelopeNumber : Return Me.EnvelopeNumber
				Case DataSetIdentification.ProductID : Return Me.ProductID
				Case DataSetIdentification.EnvelopePriority : Return Me.EnvelopePriority
				Case DataSetIdentification.DateSent : Return Me.DateSent
				Case DataSetIdentification.TimeSent : Return Me.TimeSent
				Case DataSetIdentification.CodedCharacterSet : Return Me.CodedCharacterSet
				Case DataSetIdentification.UNO : Return Me.UNO
				Case DataSetIdentification.ARMIdentifier : Return Me.ARMIdentifier
				Case DataSetIdentification.ARMVersion : Return Me.ARMVersion
				Case DataSetIdentification.RecordVersion : Return Me.RecordVersion
				Case DataSetIdentification.ObjectTypeReference : Return Me.ObjectTypeReference
				Case DataSetIdentification.ObjectAttributeReference : Return Me.ObjectAttributeReference
				Case DataSetIdentification.ObjectName : Return Me.ObjectName
				Case DataSetIdentification.EditStatus : Return Me.EditStatus
				Case DataSetIdentification.EditorialUpdate : Return Me.EditorialUpdate
				Case DataSetIdentification.Urgency : Return Me.Urgency
				Case DataSetIdentification.SubjectReference : Return Me.SubjectReference
				Case DataSetIdentification.Category : Return Me.Category
				Case DataSetIdentification.SupplementalCategory : Return Me.SupplementalCategory
				Case DataSetIdentification.FixtureIdentifier : Return Me.FixtureIdentifier
				Case DataSetIdentification.Keywords : Return Me.Keywords
				Case DataSetIdentification.ContentLocationCode : Return Me.ContentLocationCode
				Case DataSetIdentification.ContentLocationName : Return Me.ContentLocationName
				Case DataSetIdentification.ReleaseDate : Return Me.ReleaseDate
				Case DataSetIdentification.ReleaseTime : Return Me.ReleaseTime
				Case DataSetIdentification.ExpirationDate : Return Me.ExpirationDate
				Case DataSetIdentification.ExpirationTime : Return Me.ExpirationTime
				Case DataSetIdentification.SpecialInstructions : Return Me.SpecialInstructions
				Case DataSetIdentification.ActionAdvised : Return Me.ActionAdvised
				Case DataSetIdentification.ReferenceService : Return Me.ReferenceService
				Case DataSetIdentification.ReferenceDate : Return Me.ReferenceDate
				Case DataSetIdentification.ReferenceNumber : Return Me.ReferenceNumber
				Case DataSetIdentification.DateCreated : Return Me.DateCreated
				Case DataSetIdentification.TimeCreated : Return Me.TimeCreated
				Case DataSetIdentification.DigitalCreationDate : Return Me.DigitalCreationDate
				Case DataSetIdentification.DigitalCreationTime : Return Me.DigitalCreationTime
				Case DataSetIdentification.OriginatingProgram : Return Me.OriginatingProgram
				Case DataSetIdentification.ProgramVersion : Return Me.ProgramVersion
				Case DataSetIdentification.ObjectCycle : Return Me.ObjectCycle
				Case DataSetIdentification.ByLine : Return Me.ByLine
				Case DataSetIdentification.ByLineTitle : Return Me.ByLineTitle
				Case DataSetIdentification.City : Return Me.City
				Case DataSetIdentification.SubLocation : Return Me.SubLocation
				Case DataSetIdentification.ProvinceState : Return Me.ProvinceState
				Case DataSetIdentification.CountryPrimaryLocationCode : Return Me.CountryPrimaryLocationCode
				Case DataSetIdentification.CountryPrimaryLocationName : Return Me.CountryPrimaryLocationName
				Case DataSetIdentification.OriginalTransmissionReference : Return Me.OriginalTransmissionReference
				Case DataSetIdentification.Headline : Return Me.Headline
				Case DataSetIdentification.Credit : Return Me.Credit
				Case DataSetIdentification.Source : Return Me.Source
				Case DataSetIdentification.CopyrightNotice : Return Me.CopyrightNotice
				Case DataSetIdentification.Contact : Return Me.Contact
				Case DataSetIdentification.CaptionAbstract : Return Me.CaptionAbstract
				Case DataSetIdentification.WriterEditor : Return Me.WriterEditor
				Case DataSetIdentification.RasterizedeCaption : Return Me.RasterizedeCaption
				Case DataSetIdentification.ImageType : Return Me.ImageType
				Case DataSetIdentification.ImageOrientation : Return Me.ImageOrientation
				Case DataSetIdentification.LanguageIdentifier : Return Me.LanguageIdentifier
				Case DataSetIdentification.AudioType : Return Me.AudioType
				Case DataSetIdentification.AudioSamplingRate : Return Me.AudioSamplingRate
				Case DataSetIdentification.AudioSamplingResolution : Return Me.AudioSamplingResolution
				Case DataSetIdentification.AudioDuration : Return Me.AudioDuration
				Case DataSetIdentification.AudioOutcue : Return Me.AudioOutcue
				Case DataSetIdentification.ObjectDataPreviewFileFormat : Return Me.ObjectDataPreviewFileFormat
				Case DataSetIdentification.ObjectDataPreviewFileFormatVersion : Return Me.ObjectDataPreviewFileFormatVersion
				Case DataSetIdentification.ObjectDataPreviewData : Return Me.ObjectDataPreviewData
				Case DataSetIdentification.RecordVersion3 : Return Me.RecordVersion3
				Case DataSetIdentification.PictureNumber : Return Me.PictureNumber
				Case DataSetIdentification.PixelsPerLine : Return Me.PixelsPerLine
				Case DataSetIdentification.NumberOfLines : Return Me.NumberOfLines
				Case DataSetIdentification.PixelSizeInScanningDirection : Return Me.PixelSizeInScanningDirection
				Case DataSetIdentification.PixelSizePerpendicularToScanningDirection : Return Me.PixelSizePerpendicularToScanningDirection
				Case DataSetIdentification.SupplementType : Return Me.SupplementType
				Case DataSetIdentification.ColourRepresentation : Return Me.ColourRepresentation
				Case DataSetIdentification.InterchangeColourSpace : Return Me.InterchangeColourSpace
				Case DataSetIdentification.ColourSequence : Return Me.ColourSequence
				Case DataSetIdentification.IccInputColourProfile : Return Me.IccInputColourProfile
				Case DataSetIdentification.ColourCalibrationMatrixTable : Return Me.ColourCalibrationMatrixTable
				Case DataSetIdentification.LookupTable : Return Me.LookupTable
				Case DataSetIdentification.NumberOfIndexEntries : Return Me.NumberOfIndexEntries
				Case DataSetIdentification.ColourPalette : Return Me.ColourPalette
				Case DataSetIdentification.NumberOfBitsPerSample : Return Me.NumberOfBitsPerSample
				Case DataSetIdentification.SamplingStructure : Return Me.SamplingStructure
				Case DataSetIdentification.ScanningDirection : Return Me.ScanningDirection
				Case DataSetIdentification.ImageRotation : Return Me.ImageRotation
				Case DataSetIdentification.DataCompressionMethod : Return Me.DataCompressionMethod
				Case DataSetIdentification.QuantisationMethod : Return Me.QuantisationMethod
				Case DataSetIdentification.EndPoints : Return Me.EndPoints
				Case DataSetIdentification.ExcursionTolerance : Return Me.ExcursionTolerance
				Case DataSetIdentification.BitsPerComponent : Return Me.BitsPerComponent
				Case DataSetIdentification.MaximumDensityRange : Return Me.MaximumDensityRange
				Case DataSetIdentification.GammaCompensatedValue : Return Me.GammaCompensatedValue
				Case DataSetIdentification.OverlallRating : Return Me.OverlallRating
				Case DataSetIdentification.TechnicalQuality : Return Me.TechnicalQuality
				Case DataSetIdentification.ArtQuality : Return Me.ArtQuality
				Case DataSetIdentification.InformationValue : Return Me.InformationValue
				Case DataSetIdentification.SizeMode : Return Me.SizeMode
				Case DataSetIdentification.MaxSubfileSize : Return Me.MaxSubfileSize
				Case DataSetIdentification.ObjectDataSizeAnnounced : Return Me.ObjectDataSizeAnnounced
				Case DataSetIdentification.MaximumObjectDataSize : Return Me.MaximumObjectDataSize
				Case DataSetIdentification.Subfile : Return Me.Subfile
				Case DataSetIdentification.ConfirmedObjectDataSize : Return Me.ConfirmedObjectDataSize
			End Select
			Try
                Dim AllValues As List(Of String) = Text_Value(Item)
                If AllValues IsNot Nothing AndAlso AllValues.Count <> 0 Then Return AllValues(0) Else Return Nothing
            Catch ex As Exception
                Throw New IPTCGetException(ex)
            End Try
		End Function
	End Class
#End Region
#End If
End Namespace
