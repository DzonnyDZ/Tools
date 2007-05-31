' GENERATED FILE -- DO NOT EDIT
'
' Generator: TransformCodeGenerator, Version=1.0.2701.36373, Culture=neutral, PublicKeyToken=null
' Version: 1.0.2701.36373
'
'
' Generated code from "IPTCTags.xml"
'
' Created: 31. května 2007
' By:DZONNY\Honza
'
Imports System.ComponentModel
Imports Tools.ComponentModelT
Namespace DrawingT.MetadataT
#If Congig <= Nightly 'Stage: Nightly
	Partial Public Class IPTC
#Region "Enums"
		''' <summary>Numbers of IPTC records (groups of tags)</summary>
		Public Enum RecordNumbers As Byte
			''' <summary>Contains internal IPTC data used formerly in telecommunications. Now it is considered being deprecated and is not widely in use.</summary>
			<FieldDisplayName("Envelope record")> Envelope = 1
			''' <summary>This record contain informative data about content. Whole record is optional, but when any tag is used then mandatory tags are mandatory.</summary>
			<FieldDisplayName("Application Record No. 2")> Application = 2
			''' <summary>Information about ObjectData (before object has been sent)</summary>
			<FieldDisplayName("Pre-ObjectData descriptor record")> PreObjectDataDescriptorRecord = 7
			''' <summary>Contains embeded object</summary>
			<FieldDisplayName("ObjectData Record")> ObjectDataRecord = 8
			''' <summary>Confirmation of size of ObjectData</summary>
			<FieldDisplayName("Pos-ObjectData Descriptor Record")> PostObjectDataDescriptorRecord = 9
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.Envelope"/> (1)</summary>
		Public Enum EnvelopeTags As Byte
			''' <summary>A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.ModelVersion"/> for more info.</remarks>
			<FieldDisplayName("Model Version")> <Category("Model Version")> ModelVersion = 0
			''' <summary>This DataSet is to accommodate some providers who require routing information above the appropriate OSI layers.</summary>
			''' <remarks>See <seealso cref="IPTC.Destination"/> for more info.</remarks>
			<FieldDisplayName("Destination")> <Category("Destination")> Destination = 5
			''' <summary>A number representing the file format.</summary>
			''' <remarks>See <seealso cref="IPTC.FileFormat"/> for more info.</remarks>
			<FieldDisplayName("File Format")> <Category("File Format")> FileFormat = 20
			''' <summary>A binary number representing the particular version of the</summary>
			''' <remarks>See <seealso cref="IPTC.FileFormatVersion"/> for more info.</remarks>
			<FieldDisplayName("File Format Version")> <Category("File Format Version")> FileFormatVersion = 22
			''' <summary>Identifies the provider and product.</summary>
			''' <remarks>See <seealso cref="IPTC.ServiceIdentifier"/> for more info.</remarks>
			<FieldDisplayName("Service Identifier")> <Category("Service Identifier")> ServiceIdentifier = 30
			''' <summary>The characters form a number that will be unique for the date specified in and for the Service Identifier specified in .</summary>
			''' <remarks>See <seealso cref="IPTC.EnvelopeNumber"/> for more info.</remarks>
			<FieldDisplayName("Envelope Number")> <Category("Envelope Number")> EnvelopeNumber = 40
			''' <summary>Allows a provider to identify subsets of its overall service.</summary>
			''' <remarks>See <seealso cref="IPTC.ProductID"/> for more info.</remarks>
			<FieldDisplayName("Product I.D.")> <Category("Product I.D.")> ProductID = 50
			''' <summary>Specifies the envelope handling priority and not the editorial urgency (see 2:10, ).</summary>
			''' <remarks>See <seealso cref="IPTC.EnvelopePriority"/> for more info.</remarks>
			<FieldDisplayName("Envelope Priority")> <Category("Envelope Priority")> EnvelopePriority = 60
			''' <summary>Indicates year, month and day the service sent the material.</summary>
			''' <remarks>See <seealso cref="IPTC.DateSent"/> for more info.</remarks>
			<FieldDisplayName("Date Sent")> <Category("Date Sent")> DateSent = 70
			''' <summary>This is the time the service sent the material.</summary>
			''' <remarks>See <seealso cref="IPTC.TimeSent"/> for more info.</remarks>
			<FieldDisplayName("Time Sent")> <Category("Time Sent")> TimeSent = 80
			''' <summary>Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.</summary>
			''' <remarks>See <seealso cref="IPTC.CodedCharacterSet"/> for more info.</remarks>
			<FieldDisplayName("CodedCharacterSet")> <Category("CodedCharacterSet")> CodedCharacterSet = 90
			''' <summary>UNO Unique Name of Object, providing eternal, globally unique identification for objects as specified in the IIM, independent of provider and for any media form.</summary>
			''' <remarks>See <seealso cref="IPTC.UNO"/> for more info.</remarks>
			<FieldDisplayName("UNO")> <Category("UNO")> UNO = 100
			''' <summary>The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.</summary>
			''' <remarks>See <seealso cref="IPTC.ARMIdentifier"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("ARM Identifier")> <Category("ARM Identifier")> ARMIdentifier = 120
			''' <summary>A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentified'/>.</summary>
			''' <remarks>See <seealso cref="IPTC.ARMVersion"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("ARM Version")> <Category("ARM Version")> ARMVersion = 122
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.Application"/> (2)</summary>
		Public Enum ApplicationTags As Byte
			''' <summary>A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.RecordVersion"/> for more info.</remarks>
			<FieldDisplayName("Record Version")> <Category("Record Version")> RecordVersion = 0
			''' <summary>The Object Type is used to distinguish between different types of objects within the IIM.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectTypeReference"/> for more info.</remarks>
			<FieldDisplayName("Object Type Reference")> <Category("Object Type Reference")> ObjectTypeReference = 3
			''' <summary>The Object Attribute defines the nature of the object independent of the Subject.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectAttributeReference"/> for more info.</remarks>
			<FieldDisplayName("Object Attribute Reference")> <Category("Object Attribute Reference")> ObjectAttributeReference = 4
			''' <summary>Status of the objectdata, according to the practice of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.EditStatus"/> for more info.</remarks>
			<FieldDisplayName("Edit Status")> <Category("Edit Status")> EditStatus = 7
			''' <summary>Indicates the type of update that this object provides to a previous object. The link to the previous object is made using the ARM (DataSets 1:120 and 1:122 (<see cref='ARM'/>)), according to the practices of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.EditorialUpdate"/> for more info.</remarks>
			<FieldDisplayName("Editorial Update")> <Category("Editorial Update")> EditorialUpdate = 8
			''' <summary>Specifies the editorial urgency of content and not necessarily the envelope handling priority (see 1:60, ).</summary>
			''' <remarks>See <seealso cref="IPTC.Urgency"/> for more info.</remarks>
			<FieldDisplayName("Urgency")> <Category("Urgency")> Urgency = 10
			''' <summary>The Subject Reference is a structured definition of the subject matter.</summary>
			''' <remarks>See <seealso cref="IPTC.SubjectReference"/> for more info.</remarks>
			<FieldDisplayName("Subject Reference")> <Category("Subject Reference")> SubjectReference = 12
			''' <summary>Identifies the subject of the objectdata in the opinion of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.Category"/> for more info.</remarks>
			<FieldDisplayName("Category")> <Category("Category")> Category = 15
			''' <summary>Supplemental categories further refine the subject of an objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.SupplementalCategory"/> for more info.</remarks>
			<FieldDisplayName("Supplemental Category")> <Category("Supplemental Category")> SupplementalCategory = 20
			''' <summary>Identifies objectdata that recurs often and predictably.</summary>
			''' <remarks>See <seealso cref="IPTC.FixtureIdentifier"/> for more info.</remarks>
			<FieldDisplayName("Fixture Identifier")> <Category("Fixture Identifier")> FixtureIdentifier = 22
			''' <summary>Used to indicate specific information retrieval words.</summary>
			''' <remarks>See <seealso cref="IPTC.Keywords"/> for more info.</remarks>
			<FieldDisplayName("Keywords")> <Category("Keywords")> Keywords = 25
			''' <summary>Indicates the code of a country/geographical location referenced by the content of the object.</summary>
			''' <remarks>See <seealso cref="IPTC.ContentLocationCode"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("Content Location Code")> <Category("Content Location Code")> ContentLocationCode = 26
			''' <summary>Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.ContentLocationName"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("Content Location Name")> <Category("Content Location Name")> ContentLocationName = 27
			''' <summary>The earliest date the provider intends the object to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ReleaseDate"/> for more info.</remarks>
			<FieldDisplayName("Release Date")> <Category("Release Date")> ReleaseDate = 30
			''' <summary>The earliest time the provider intends the object to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ReleaseTime"/> for more info.</remarks>
			<FieldDisplayName("Release Time")> <Category("Release Time")> ReleaseTime = 32
			''' <summary>The latest date the provider or owner intends the objectdata to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ExpirationDate"/> for more info.</remarks>
			<FieldDisplayName("Expiration Date")> <Category("Expiration Date")> ExpirationDate = 37
			''' <summary>The latest time the provider or owner intends the objectdata to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ExpirationTime"/> for more info.</remarks>
			<FieldDisplayName("Expiration Time")> <Category("Expiration Time")> ExpirationTime = 38
			''' <summary>Other editorial instructions concerning the use of the objectdata, such as embargoes and warnings.</summary>
			''' <remarks>See <seealso cref="IPTC.SpecialInstructions"/> for more info.</remarks>
			<FieldDisplayName("Special Instructions")> <Category("Special Instructions")> SpecialInstructions = 40
			''' <summary>Indicates the type of action that this object provides to a previous object.</summary>
			''' <remarks>See <seealso cref="IPTC.ActionAdvised"/> for more info.</remarks>
			<FieldDisplayName("Action Advised")> <Category("Action Advised")> ActionAdvised = 42
			''' <summary>Identifies the Service Identifier of a prior envelope to which the current object refers.</summary>
			''' <remarks>See <seealso cref="IPTC.ReferenceService"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("Reference Service")> <Category("Reference Service")> ReferenceService = 45
			''' <summary>Identifies the date of a prior envelope to which the current object refers.</summary>
			''' <remarks>See <seealso cref="IPTC.ReferenceDate"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("Reference Date")> <Category("Reference Date")> ReferenceDate = 47
			''' <summary>Identifies the Envelope Number of a prior envelope to which the current object refers.</summary>
			''' <remarks>See <seealso cref="IPTC.ReferenceNumber"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("Reference Number")> <Category("Reference Number")> ReferenceNumber = 50
			''' <summary>The date the intellectual content of the objectdata was created rather than the date of the creation of the physical representation.</summary>
			''' <remarks>See <seealso cref="IPTC.DateCreated"/> for more info.</remarks>
			<FieldDisplayName("Date Created")> <Category("Date Created")> DateCreated = 55
			''' <summary>The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.</summary>
			''' <remarks>See <seealso cref="IPTC.TimeCreated"/> for more info.</remarks>
			<FieldDisplayName("Time Created")> <Category("Time Created")> TimeCreated = 60
			''' <summary>The date the digital representation of the objectdata was created.</summary>
			''' <remarks>See <seealso cref="IPTC.DigitalCreationDate"/> for more info.</remarks>
			<FieldDisplayName("Digital Creation Date")> <Category("Digital Creation Date")> DigitalCreationDate = 62
			''' <summary>The time the digital representation of the objectdata was created.</summary>
			''' <remarks>See <seealso cref="IPTC.DigitalCreationTime"/> for more info.</remarks>
			<FieldDisplayName("Digital Creation Time")> <Category("Digital Creation Time")> DigitalCreationTime = 63
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.OriginatingProgram"/> for more info.</remarks>
			<FieldDisplayName("Originating Program")> <Category("Originating Program")> OriginatingProgram = 65
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.ProgramVersion"/> for more info.</remarks>
			<FieldDisplayName("Program Version")> <Category("Program Version")> ProgramVersion = 70
			''' <summary>Virtually only used in North America.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectCycle"/> for more info.</remarks>
			<FieldDisplayName("Object Cycle")> <Category("Object Cycle")> ObjectCycle = 75
			''' <summary>Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.</summary>
			''' <remarks>See <seealso cref="IPTC.ByLine"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("By-line")> <Category("By-line")> ByLine = 80
			''' <summary>A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.</summary>
			''' <remarks>See <seealso cref="IPTC.ByLineTitle"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("By-line Title")> <Category("By-line Title")> ByLineTitle = 85
			''' <summary>Identifies city of objectdata origin according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.City"/> for more info.</remarks>
			<FieldDisplayName("City")> <Category("City")> City = 90
			''' <summary>Identifies the location within a city from which the objectdata originates, according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.SubLocation"/> for more info.</remarks>
			<FieldDisplayName("Sublocation")> <Category("Sublocation")> SubLocation = 92
			''' <summary>Identifies Province/State of origin according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.ProvinceState"/> for more info.</remarks>
			<FieldDisplayName("Province/State")> <Category("Province/State")> ProvinceState = 95
			''' <summary>Indicates the code of the country/primary location where the intellectual property of the objectdata was created, e.g. a photo was taken, an event occurred.</summary>
			''' <remarks>See <seealso cref="IPTC.CountryPrimaryLocationCode"/> for more info.</remarks>
			<FieldDisplayName("Country/Primary Location Code")> <Category("Country/Primary Location Code")> CountryPrimaryLocationCode = 100
			''' <summary>Provides full, publishable, name of the country/primary location where the intellectual property of the objectdata was created, according to guidelines of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.CountryPrimaryLocationName"/> for more info.</remarks>
			<FieldDisplayName("Country/Primary Location Name")> <Category("Country/Primary Location Name")> CountryPrimaryLocationName = 101
			''' <summary>A code representing the location of original transmission according to practices of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.OriginalTransmissionReference"/> for more info.</remarks>
			<FieldDisplayName("Original Transmission Refrence")> <Category("Original Transmission Refrence")> OriginalTransmissionReference = 103
			''' <summary>A publishable entry providing a synopsis of the contents of the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.Headline"/> for more info.</remarks>
			<FieldDisplayName("Headline")> <Category("Headline")> Headline = 105
			''' <summary>Identifies the provider of the objectdata, not necessarily the owner/creator.</summary>
			''' <remarks>See <seealso cref="IPTC.Credit"/> for more info.</remarks>
			<FieldDisplayName("Credit")> <Category("Credit")> Credit = 110
			''' <summary>Identifies the original owner of the intellectual content of the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.Source"/> for more info.</remarks>
			<FieldDisplayName("Source")> <Category("Source")> Source = 115
			''' <summary>Contains any necessary copyright notice.</summary>
			''' <remarks>See <seealso cref="IPTC.CopyrightNotice"/> for more info.</remarks>
			<FieldDisplayName("Copyright Notice")> <Category("Copyright Notice")> CopyrightNotice = 116
			''' <summary>Identifies the person or organisation which can provide further background information on the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.Contact"/> for more info.</remarks>
			<FieldDisplayName("Contact")> <Category("Contact")> Contact = 118
			''' <summary>A textual description of the objectdata, particularly used where the object is not text.</summary>
			''' <remarks>See <seealso cref="IPTC.CaptionAbstract"/> for more info.</remarks>
			<FieldDisplayName("Caption/Abstract")> <Category("Caption/Abstract")> CaptionAbstract = 120
			''' <summary>Identification of the name of the person involved in the writing, editing or correcting the objectdata or caption/abstract.</summary>
			''' <remarks>See <seealso cref="IPTC.WriterEditor"/> for more info.</remarks>
			<FieldDisplayName("Writer/Editor")> <Category("Writer/Editor")> WriterEditor = 122
			''' <summary>Image width 460 pixels and image height 128 pixels. Scanning direction bottom to top, left to right.</summary>
			''' <remarks>See <seealso cref="IPTC.RasterizedeCaption"/> for more info.</remarks>
			<FieldDisplayName("Rasterized Caption")> <Category("Rasterized Caption")> RasterizedeCaption = 125
			''' <summary>Image Type</summary>
			''' <remarks>See <seealso cref="IPTC.ImageType"/> for more info.</remarks>
			<FieldDisplayName("Image Type")> <Category("Image Type")> ImageType = 130
			''' <summary>Indicates the layout of the image area.</summary>
			''' <remarks>See <seealso cref="IPTC.ImageOrientation"/> for more info.</remarks>
			<FieldDisplayName("Image Orientation")> <Category("Image Orientation")> ImageOrientation = 131
			''' <summary>Describes the major national language of the object, according to the 2-letter codes of ISO 639:1988.</summary>
			''' <remarks>See <seealso cref="IPTC.LanguageIdentifier"/> for more info.</remarks>
			<FieldDisplayName("Language Identifier")> <Category("Language Identifier")> LanguageIdentifier = 135
			''' <summary>Type of audio in objectdata</summary>
			''' <remarks>See <seealso cref="IPTC.AudioType"/> for more info.</remarks>
			<FieldDisplayName("Audio Type")> <Category("Audio Type")> AudioType = 150
			''' <summary>Sampling rate, representing the sampling rate in hertz (Hz).</summary>
			''' <remarks>See <seealso cref="IPTC.AudioSamplingRate"/> for more info.</remarks>
			<FieldDisplayName("Audio Sampling Rate")> <Category("Audio Sampling Rate")> AudioSamplingRate = 151
			''' <summary>The number of bits in each audio sample.</summary>
			''' <remarks>See <seealso cref="IPTC.AudioSamplingResolution"/> for more info.</remarks>
			<FieldDisplayName("Audio Sampling Resolution")> <Category("Audio Sampling Resolution")> AudioSamplingResolution = 152
			''' <summary>The running time of an audio objectdata when played back at the speed at which it was recorded.</summary>
			''' <remarks>See <seealso cref="IPTC.AudioDuration"/> for more info.</remarks>
			<FieldDisplayName("Audio Duration")> <Category("Audio Duration")> AudioDuration = 153
			''' <summary>Identifies the content of the end of an audio objectdata, according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.AudioOutcue"/> for more info.</remarks>
			<FieldDisplayName("Audio Outcue")> <Category("Audio Outcue")> AudioOutcue = 154
			''' <summary>The file format of the ObjectData Preview.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataPreviewFileFormat"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("ObjectData Preview File Format")> <Category("ObjectData Preview File Format")> ObjectDataPreviewFileFormat = 200
			''' <summary>The particular version of the ObjectData Preview File Format specified in</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataPreviewFileFormatVersion"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("ObjectData Preview File Format Version")> <Category("ObjectData Preview File Format Version")> ObjectDataPreviewFileFormatVersion = 201
			''' <summary>Maximum size of 256000 octets consisting of binary data.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataPreviewData"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> <FieldDisplayName("ObjectData Preview Data")> <Category("ObjectData Preview Data")> ObjectDataPreviewData = 202
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.PreObjectDataDescriptorRecord"/> (7)</summary>
		Public Enum PreObjectDataDescriptorRecordTags As Byte
			''' <summary>The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.</summary>
			''' <remarks>See <seealso cref="IPTC.SizeMode"/> for more info.</remarks>
			<FieldDisplayName("Size Mode")> <Category("Size Mode")> SizeMode = 10
			''' <summary>The maximum size for the following Subfile DataSet(s).</summary>
			''' <remarks>See <seealso cref="IPTC.MaxSubfileSize"/> for more info.</remarks>
			<FieldDisplayName("Max Subfile Size")> <Category("Max Subfile Size")> MaxSubfileSize = 20
			''' <summary>A binary number representing the overall size of the objectdata, expressed in octets, not including tags, if that size is known when transfer commences.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataSizeAnnounced"/> for more info.</remarks>
			<FieldDisplayName("ObjectData Size Announced")> <Category("ObjectData Size Announced")> ObjectDataSizeAnnounced = 90
			''' <summary>Used when objectdata size is not known, indicating the largest size, expressed in octets, that the objectdata can possibly have, not including tags.</summary>
			''' <remarks>See <seealso cref="IPTC.MaximumObjectDataSize"/> for more info.</remarks>
			<FieldDisplayName("Maximum ObjectData Size")> <Category("Maximum ObjectData Size")> MaximumObjectDataSize = 95
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.ObjectDataRecord"/> (8)</summary>
		Public Enum ObjectDataRecordTags As Byte
			''' <summary>Subfile DataSet containing the objectdata itself.</summary>
			''' <remarks>See <seealso cref="IPTC.Subfile"/> for more info.</remarks>
			<FieldDisplayName("Subfile")> <Category("Subfile")> Subfile = 10
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.PostObjectDataDescriptorRecord"/> (9)</summary>
		Public Enum PostObjectDataDescriptorRecordTags As Byte
			''' <summary>Total size of the objectdata, in octets, without tags.</summary>
			''' <remarks>See <seealso cref="IPTC.ConfirmedObjectDataSize"/> for more info.</remarks>
			<FieldDisplayName("Confirmed ObjectData Size")> <Category("Confirmed ObjectData Size")> ConfirmedObjectDataSize = 10
		End Enum
		''' <summary>Gets Enum that contains list of tags for specific record (group of tags)</summary>
		''' <param name="Record">Number of record to get enum for</param>
		''' <exception name="InvalidEnumArgumentException">Value of <paramref name="Record"/> is not member of <see cref="RecordNumbers"/></exception>
		Public Shared Function GetEnum(ByVal Record As RecordNumbers) As Type
			Select Case Record
				Case RecordNumbers.Envelope : Return GetType(EnvelopeTags)
				Case RecordNumbers.Application : Return GetType(ApplicationTags)
				Case RecordNumbers.PreObjectDataDescriptorRecord : Return GetType(PreObjectDataDescriptorRecordTags)
				Case RecordNumbers.ObjectDataRecord : Return GetType(ObjectDataRecordTags)
				Case RecordNumbers.PostObjectDataDescriptorRecord : Return GetType(PostObjectDataDescriptorRecordTags)
				Case Else : Throw New InvalidEnumArgumentException("Record", Record, GetType(RecordNumbers))
			End Select
		End Function
#End Region
	End Class
#End If
End Namespace
