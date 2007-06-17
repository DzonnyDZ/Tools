' GENERATED FILE -- DO NOT EDIT
'
' Generator: TransformCodeGenerator, Version=1.0.2701.36373, Culture=neutral, PublicKeyToken=null
' Version: 1.0.2701.36373
'
'
' Generated code from "IPTCTags.xml"
'
' Created: 17. června 2007
' By:DZONNY\Honza
'
'Localize: IPTC needs localization of Decriptions, DisplayNames and error messages
Imports System.ComponentModel
Imports Tools.ComponentModelT
Imports System.XML.Serialization
Imports Tools.DataStructuresT.GenericT
Namespace DrawingT.MetadataT
#If Congig <= Nightly 'Stage: Nightly
	Partial Public Class IPTC
#Region "Tag Enums"
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
			<FieldDisplayName("Model Version")> <Category("Internal")> ModelVersion = 0
			''' <summary>This DataSet is to accommodate some providers who require routing information above the appropriate OSI layers.</summary>
			''' <remarks>See <seealso cref="IPTC.Destination"/> for more info.</remarks>
			<FieldDisplayName("Destination")> <Category("Old IPTC")> Destination = 5
			''' <summary>A number representing the file format.</summary>
			''' <remarks>See <seealso cref="IPTC.FileFormat"/> for more info.</remarks>
			<FieldDisplayName("File Format")> <Category("Old IPTC")> FileFormat = 20
			''' <summary>A binary number representing the particular version of the <see cref="FileFormat"/></summary>
			''' <remarks>See <seealso cref="IPTC.FileFormatVersion"/> for more info.</remarks>
			<FieldDisplayName("File Format Version")> <Category("Old IPTC")> FileFormatVersion = 22
			''' <summary>Identifies the provider and product.</summary>
			''' <remarks>See <seealso cref="IPTC.ServiceIdentifier"/> for more info.</remarks>
			<FieldDisplayName("Service Identifier")> <Category("Old IPTC")> ServiceIdentifier = 30
			''' <summary>The characters form a number that will be unique for the date specified in <see cref="DateSent"/> and for the Service Identifier specified in <see cref="ServiceIdentifier"/>.</summary>
			''' <remarks>See <seealso cref="IPTC.EnvelopeNumber"/> for more info.</remarks>
			<FieldDisplayName("Envelope Number")> <Category("Old IPTC")> EnvelopeNumber = 40
			''' <summary>Allows a provider to identify subsets of its overall service.</summary>
			''' <remarks>See <seealso cref="IPTC.ProductID"/> for more info.</remarks>
			<FieldDisplayName("Product I.D.")> <Category("Old IPTC")> ProductID = 50
			''' <summary>Specifies the envelope handling priority and not the editorial urgency (see 2:10, <see cref="Urgency"/>).</summary>
			''' <remarks>See <seealso cref="IPTC.EnvelopePriority"/> for more info.</remarks>
			<FieldDisplayName("Envelope Priority")> <Category("Status")> EnvelopePriority = 60
			''' <summary>Indicates year, month and day the service sent the material.</summary>
			''' <remarks>See <seealso cref="IPTC.DateSent"/> for more info.</remarks>
			<FieldDisplayName("Date Sent")> <Category("Date")> DateSent = 70
			''' <summary>This is the time the service sent the material.</summary>
			''' <remarks>See <seealso cref="IPTC.TimeSent"/> for more info.</remarks>
			<FieldDisplayName("Time Sent")> <Category("Date")> TimeSent = 80
			''' <summary>Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.</summary>
			''' <remarks>See <seealso cref="IPTC.CodedCharacterSet"/> for more info.</remarks>
			<FieldDisplayName("CodedCharacterSet")> <Category("Old IPTC")> CodedCharacterSet = 90
			''' <summary>UNO Unique Name of Object, providing eternal, globally unique identification for objects as specified in the IIM, independent of provider and for any media form.</summary>
			''' <remarks>See <seealso cref="IPTC.UNO"/> for more info.</remarks>
			<FieldDisplayName("UNO")> <Category("Old IPTC")> UNO = 100
			''' <summary>The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.</summary>
			''' <remarks>See <seealso cref="IPTC.ARMIdentifier"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("ARM Identifier")> <Category("Old IPTC")> ARMIdentifier = 120
			''' <summary>A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentifier'/>.</summary>
			''' <remarks>See <seealso cref="IPTC.ARMVersion"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("ARM Version")> <Category("Old IPTC")> ARMVersion = 122
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.Application"/> (2)</summary>
		Public Enum ApplicationTags As Byte
			''' <summary>A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.RecordVersion"/> for more info.</remarks>
			<FieldDisplayName("Record Version")> <Category("Internal")> RecordVersion = 0
			''' <summary>The Object Type is used to distinguish between different types of objects within the IIM.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectTypeReference"/> for more info.</remarks>
			<FieldDisplayName("Object Type Reference")> <Category("Category")> ObjectTypeReference = 3
			''' <summary>The Object Attribute defines the nature of the object independent of the Subject.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectAttributeReference"/> for more info.</remarks>
			<FieldDisplayName("Object Attribute Reference")> <Category("Category")> ObjectAttributeReference = 4
			''' <summary>Status of the objectdata, according to the practice of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.EditStatus"/> for more info.</remarks>
			<FieldDisplayName("Edit Status")> <Category("Status")> EditStatus = 7
			''' <summary>Indicates the type of update that this object provides to a previous object. The link to the previous object is made using the ARM (DataSets 1:120 and 1:122 (<see cref='ARM'/>)), according to the practices of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.EditorialUpdate"/> for more info.</remarks>
			<FieldDisplayName("Editorial Update")> <Category("Status")> EditorialUpdate = 8
			''' <summary>Specifies the editorial urgency of content and not necessarily the envelope handling priority (see 1:60, <see cref="EnvelopePriority"/>).</summary>
			''' <remarks>See <seealso cref="IPTC.Urgency"/> for more info.</remarks>
			<FieldDisplayName("Urgency")> <Category("Status")> Urgency = 10
			''' <summary>The Subject Reference is a structured definition of the subject matter.</summary>
			''' <remarks>See <seealso cref="IPTC.SubjectReference"/> for more info.</remarks>
			<FieldDisplayName("Subject Reference")> <Category("Old IPTC")> SubjectReference = 12
			''' <summary>Identifies the subject of the objectdata in the opinion of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.Category"/> for more info.</remarks>
			<FieldDisplayName("Category")> <Category("Category")> Category = 15
			''' <summary>Supplemental categories further refine the subject of an objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.SupplementalCategory"/> for more info.</remarks>
			<FieldDisplayName("Supplemental Category")> <Category("Category")> SupplementalCategory = 20
			''' <summary>Identifies objectdata that recurs often and predictably.</summary>
			''' <remarks>See <seealso cref="IPTC.FixtureIdentifier"/> for more info.</remarks>
			<FieldDisplayName("Fixture Identifier")> <Category("Category")> FixtureIdentifier = 22
			''' <summary>Used to indicate specific information retrieval words.</summary>
			''' <remarks>See <seealso cref="IPTC.Keywords"/> for more info.</remarks>
			<FieldDisplayName("Keywords")> <Category("Category")> Keywords = 25
			''' <summary>Indicates the code of a country/geographical location referenced by the content of the object.</summary>
			''' <remarks>See <seealso cref="IPTC.ContentLocationCode"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("Content Location Code")> <Category("Location")> ContentLocationCode = 26
			''' <summary>Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.ContentLocationName"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("Content Location Name")> <Category("Location")> ContentLocationName = 27
			''' <summary>The earliest date the provider intends the object to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ReleaseDate"/> for more info.</remarks>
			<FieldDisplayName("Release Date")> <Category("Date")> ReleaseDate = 30
			''' <summary>The earliest time the provider intends the object to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ReleaseTime"/> for more info.</remarks>
			<FieldDisplayName("Release Time")> <Category("Date")> ReleaseTime = 32
			''' <summary>The latest date the provider or owner intends the objectdata to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ExpirationDate"/> for more info.</remarks>
			<FieldDisplayName("Expiration Date")> <Category("Date")> ExpirationDate = 37
			''' <summary>The latest time the provider or owner intends the objectdata to be used.</summary>
			''' <remarks>See <seealso cref="IPTC.ExpirationTime"/> for more info.</remarks>
			<FieldDisplayName("Expiration Time")> <Category("Date")> ExpirationTime = 38
			''' <summary>Other editorial instructions concerning the use of the objectdata, such as embargoes and warnings.</summary>
			''' <remarks>See <seealso cref="IPTC.SpecialInstructions"/> for more info.</remarks>
			<FieldDisplayName("Special Instructions")> <Category("Other")> SpecialInstructions = 40
			''' <summary>Indicates the type of action that this object provides to a previous object.</summary>
			''' <remarks>See <seealso cref="IPTC.ActionAdvised"/> for more info.</remarks>
			<FieldDisplayName("Action Advised")> <Category("Other")> ActionAdvised = 42
			''' <summary>Identifies the Service Identifier of a prior envelope to which the current object refers.</summary>
			''' <remarks>See <seealso cref="IPTC.ReferenceService"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("Reference Service")> <Category("Old IPTC")> ReferenceService = 45
			''' <summary>Identifies the date of a prior envelope to which the current object refers.</summary>
			''' <remarks>See <seealso cref="IPTC.ReferenceDate"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("Reference Date")> <Category("Old IPTC")> ReferenceDate = 47
			''' <summary>Identifies the Envelope Number of a prior envelope to which the current object refers.</summary>
			''' <remarks>See <seealso cref="IPTC.ReferenceNumber"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("Reference Number")> <Category("Old IPTC")> ReferenceNumber = 50
			''' <summary>The date the intellectual content of the objectdata was created rather than the date of the creation of the physical representation.</summary>
			''' <remarks>See <seealso cref="IPTC.DateCreated"/> for more info.</remarks>
			<FieldDisplayName("Date Created")> <Category("Date")> DateCreated = 55
			''' <summary>The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.</summary>
			''' <remarks>See <seealso cref="IPTC.TimeCreated"/> for more info.</remarks>
			<FieldDisplayName("Time Created")> <Category("Date")> TimeCreated = 60
			''' <summary>The date the digital representation of the objectdata was created.</summary>
			''' <remarks>See <seealso cref="IPTC.DigitalCreationDate"/> for more info.</remarks>
			<FieldDisplayName("Digital Creation Date")> <Category("Date")> DigitalCreationDate = 62
			''' <summary>The time the digital representation of the objectdata was created.</summary>
			''' <remarks>See <seealso cref="IPTC.DigitalCreationTime"/> for more info.</remarks>
			<FieldDisplayName("Digital Creation Time")> <Category("Date")> DigitalCreationTime = 63
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.OriginatingProgram"/> for more info.</remarks>
			<FieldDisplayName("Originating Program")> <Category("Other")> OriginatingProgram = 65
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.ProgramVersion"/> for more info.</remarks>
			<FieldDisplayName("Program Version")> <Category("Other")> ProgramVersion = 70
			''' <summary>Virtually only used in North America.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectCycle"/> for more info.</remarks>
			<FieldDisplayName("Object Cycle")> <Category("Status")> ObjectCycle = 75
			''' <summary>Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.</summary>
			''' <remarks>See <seealso cref="IPTC.ByLine"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("By-line")> <Category("Author")> ByLine = 80
			''' <summary>A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.</summary>
			''' <remarks>See <seealso cref="IPTC.ByLineTitle"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("By-line Title")> <Category("Author")> ByLineTitle = 85
			''' <summary>Identifies city of objectdata origin according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.City"/> for more info.</remarks>
			<FieldDisplayName("City")> <Category("Location")> City = 90
			''' <summary>Identifies the location within a city from which the objectdata originates, according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.SubLocation"/> for more info.</remarks>
			<FieldDisplayName("Sublocation")> <Category("Location")> SubLocation = 92
			''' <summary>Identifies Province/State of origin according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.ProvinceState"/> for more info.</remarks>
			<FieldDisplayName("Province/State")> <Category("Location")> ProvinceState = 95
			''' <summary>Indicates the code of the country/primary location where the intellectual property of the objectdata was created, e.g. a photo was taken, an event occurred.</summary>
			''' <remarks>See <seealso cref="IPTC.CountryPrimaryLocationCode"/> for more info.</remarks>
			<FieldDisplayName("Country/Primary Location Code")> <Category("Location")> CountryPrimaryLocationCode = 100
			''' <summary>Provides full, publishable, name of the country/primary location where the intellectual property of the objectdata was created, according to guidelines of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.CountryPrimaryLocationName"/> for more info.</remarks>
			<FieldDisplayName("Country/Primary Location Name")> <Category("Location")> CountryPrimaryLocationName = 101
			''' <summary>A code representing the location of original transmission according to practices of the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.OriginalTransmissionReference"/> for more info.</remarks>
			<FieldDisplayName("Original Transmission Refrence")> <Category("Location")> OriginalTransmissionReference = 103
			''' <summary>A publishable entry providing a synopsis of the contents of the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.Headline"/> for more info.</remarks>
			<FieldDisplayName("Headline")> <Category("Title")> Headline = 105
			''' <summary>Identifies the provider of the objectdata, not necessarily the owner/creator.</summary>
			''' <remarks>See <seealso cref="IPTC.Credit"/> for more info.</remarks>
			<FieldDisplayName("Credit")> <Category("Author")> Credit = 110
			''' <summary>Identifies the original owner of the intellectual content of the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.Source"/> for more info.</remarks>
			<FieldDisplayName("Source")> <Category("Author")> Source = 115
			''' <summary>Contains any necessary copyright notice.</summary>
			''' <remarks>See <seealso cref="IPTC.CopyrightNotice"/> for more info.</remarks>
			<FieldDisplayName("Copyright Notice")> <Category("Author")> CopyrightNotice = 116
			''' <summary>Identifies the person or organisation which can provide further background information on the objectdata.</summary>
			''' <remarks>See <seealso cref="IPTC.Contact"/> for more info.</remarks>
			<FieldDisplayName("Contact")> <Category("Author")> Contact = 118
			''' <summary>A textual description of the objectdata, particularly used where the object is not text.</summary>
			''' <remarks>See <seealso cref="IPTC.CaptionAbstract"/> for more info.</remarks>
			<FieldDisplayName("Caption/Abstract")> <Category("Title")> CaptionAbstract = 120
			''' <summary>Identification of the name of the person involved in the writing, editing or correcting the objectdata or caption/abstract.</summary>
			''' <remarks>See <seealso cref="IPTC.WriterEditor"/> for more info.</remarks>
			<FieldDisplayName("Writer/Editor")> <Category("Author")> WriterEditor = 122
			''' <summary>Image width 460 pixels and image height 128 pixels. Scanning direction bottom to top, left to right.</summary>
			''' <remarks>See <seealso cref="IPTC.RasterizedeCaption"/> for more info.</remarks>
			<FieldDisplayName("Rasterized Caption")> <Category("Title")> RasterizedeCaption = 125
			''' <summary>Image Type</summary>
			''' <remarks>See <seealso cref="IPTC.ImageType"/> for more info.</remarks>
			<FieldDisplayName("Image Type")> <Category("Image")> ImageType = 130
			''' <summary>Indicates the layout of the image area.</summary>
			''' <remarks>See <seealso cref="IPTC.ImageOrientation"/> for more info.</remarks>
			<FieldDisplayName("Image Orientation")> <Category("Image")> ImageOrientation = 131
			''' <summary>Describes the major national language of the object, according to the 2-letter codes of ISO 639:1988.</summary>
			''' <remarks>See <seealso cref="IPTC.LanguageIdentifier"/> for more info.</remarks>
			<FieldDisplayName("Language Identifier")> <Category("Other")> LanguageIdentifier = 135
			''' <summary>Type of audio in objectdata</summary>
			''' <remarks>See <seealso cref="IPTC.AudioType"/> for more info.</remarks>
			<FieldDisplayName("Audio Type")> <Category("Audio")> AudioType = 150
			''' <summary>Sampling rate, representing the sampling rate in hertz (Hz).</summary>
			''' <remarks>See <seealso cref="IPTC.AudioSamplingRate"/> for more info.</remarks>
			<FieldDisplayName("Audio Sampling Rate")> <Category("Audio")> AudioSamplingRate = 151
			''' <summary>The number of bits in each audio sample.</summary>
			''' <remarks>See <seealso cref="IPTC.AudioSamplingResolution"/> for more info.</remarks>
			<FieldDisplayName("Audio Sampling Resolution")> <Category("Audio")> AudioSamplingResolution = 152
			''' <summary>The running time of an audio objectdata when played back at the speed at which it was recorded.</summary>
			''' <remarks>See <seealso cref="IPTC.AudioDuration"/> for more info.</remarks>
			<FieldDisplayName("Audio Duration")> <Category("Audio")> AudioDuration = 153
			''' <summary>Identifies the content of the end of an audio objectdata, according to guidelines established by the provider.</summary>
			''' <remarks>See <seealso cref="IPTC.AudioOutcue"/> for more info.</remarks>
			<FieldDisplayName("Audio Outcue")> <Category("Audio")> AudioOutcue = 154
			''' <summary>The file format of the ObjectData Preview.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataPreviewFileFormat"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("ObjectData Preview File Format")> <Category("Embeded object")> ObjectDataPreviewFileFormat = 200
			''' <summary>The particular version of the ObjectData Preview File Format specified in <see cref="ObjectDataPreviewFileFormat"/></summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataPreviewFileFormatVersion"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("ObjectData Preview File Format Version")> <Category("Embeded object")> ObjectDataPreviewFileFormatVersion = 201
			''' <summary>Maximum size of 256000 octets consisting of binary data.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataPreviewData"/> for more info.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> <FieldDisplayName("ObjectData Preview Data")> <Category("Embeded object")> ObjectDataPreviewData = 202
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.PreObjectDataDescriptorRecord"/> (7)</summary>
		Public Enum PreObjectDataDescriptorRecordTags As Byte
			''' <summary>The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.</summary>
			''' <remarks>See <seealso cref="IPTC.SizeMode"/> for more info.</remarks>
			<FieldDisplayName("Size Mode")> <Category("Embeded object")> SizeMode = 10
			''' <summary>The maximum size for the following Subfile DataSet(s).</summary>
			''' <remarks>See <seealso cref="IPTC.MaxSubfileSize"/> for more info.</remarks>
			<FieldDisplayName("Max Subfile Size")> <Category("Embeded object")> MaxSubfileSize = 20
			''' <summary>A binary number representing the overall size of the objectdata, expressed in octets, not including tags, if that size is known when transfer commences.</summary>
			''' <remarks>See <seealso cref="IPTC.ObjectDataSizeAnnounced"/> for more info.</remarks>
			<FieldDisplayName("ObjectData Size Announced")> <Category("Embeded object")> ObjectDataSizeAnnounced = 90
			''' <summary>Used when objectdata size is not known, indicating the largest size, expressed in octets, that the objectdata can possibly have, not including tags.</summary>
			''' <remarks>See <seealso cref="IPTC.MaximumObjectDataSize"/> for more info.</remarks>
			<FieldDisplayName("Maximum ObjectData Size")> <Category("Embeded object")> MaximumObjectDataSize = 95
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.ObjectDataRecord"/> (8)</summary>
		Public Enum ObjectDataRecordTags As Byte
			''' <summary>Subfile DataSet containing the objectdata itself.</summary>
			''' <remarks>See <seealso cref="IPTC.Subfile"/> for more info.</remarks>
			<FieldDisplayName("Subfile")> <Category("Embeded object")> Subfile = 10
		End Enum
		''' <summary>Numbers of data sets (tags) inside record <see cref="RecordNumbers.PostObjectDataDescriptorRecord"/> (9)</summary>
		Public Enum PostObjectDataDescriptorRecordTags As Byte
			''' <summary>Total size of the objectdata, in octets, without tags.</summary>
			''' <remarks>See <seealso cref="IPTC.ConfirmedObjectDataSize"/> for more info.</remarks>
			<FieldDisplayName("Confirmed ObjectData Size")> <Category("Embeded object")> ConfirmedObjectDataSize = 10
		End Enum
		''' <summary>Gets Enum that contains list of tags for specific record (group of tags)</summary>
		''' <param name="Record">Number of record to get enum for</param>
		''' <exception cref="InvalidEnumArgumentException">Value of <paramref name="Record"/> is not member of <see cref="RecordNumbers"/></exception>
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
		Partial Public Structure DataSetIdentification
			''' <summary>A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.</summary>
			''' <remarks>Version numbers are assigned by IPTC and NAA. The version number of this record is four (4).</remarks>
			Public Shared ReadOnly Property ModelVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ModelVersion)
				End Get
			End Property
			''' <summary>This DataSet is to accommodate some providers who require routing information above the appropriate OSI layers.</summary>
			Public Shared ReadOnly Property Destination As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.Destination)
				End Get
			End Property
			''' <summary>A number representing the file format.</summary>
			''' <remarks>The file format must be registered with IPTC or NAA with a unique number assigned to it (see Appendix A). The information is used to route the data to the appropriate system and to allow the receiving system to perform the appropriate actions thereto.</remarks>
			Public Shared ReadOnly Property FileFormat As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.FileFormat)
				End Get
			End Property
			''' <summary>A binary number representing the particular version of the <see cref="FileFormat"/></summary>
			Public Shared ReadOnly Property FileFormatVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.FileFormatVersion)
				End Get
			End Property
			''' <summary>Identifies the provider and product.</summary>
			Public Shared ReadOnly Property ServiceIdentifier As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ServiceIdentifier)
				End Get
			End Property
			''' <summary>The characters form a number that will be unique for the date specified in <see cref="DateSent"/> and for the Service Identifier specified in <see cref="ServiceIdentifier"/>.</summary>
			''' <remarks>If identical envelope numbers appear with the same date and with the same Service Identifier, records 2-9 must be unchanged from the original. This is not intended to be a sequential serial number reception check.</remarks>
			Public Shared ReadOnly Property EnvelopeNumber As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.EnvelopeNumber)
				End Get
			End Property
			''' <summary>Allows a provider to identify subsets of its overall service.</summary>
			''' <remarks>Used to provide receiving organisation data on which to select, route, or otherwise handle data.</remarks>
			Public Shared ReadOnly Property ProductID As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ProductID)
				End Get
			End Property
			''' <summary>Specifies the envelope handling priority and not the editorial urgency (see 2:10, <see cref="Urgency"/>).</summary>
			''' <remarks>'1' indicates the most urgent, '5' the normal urgency, and '8' the least urgent copy. The numeral '9' indicates a User Defined Priority. The numeral '0' is reserved for future use.</remarks>
			Public Shared ReadOnly Property EnvelopePriority As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.EnvelopePriority)
				End Get
			End Property
			''' <summary>Indicates year, month and day the service sent the material.</summary>
			Public Shared ReadOnly Property DateSent As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.DateSent)
				End Get
			End Property
			''' <summary>This is the time the service sent the material.</summary>
			Public Shared ReadOnly Property TimeSent As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.TimeSent)
				End Get
			End Property
			''' <summary>Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.</summary>
			''' <remarks>The control functions apply to character oriented DataSets in records 2-6. They also apply to record 8, unless the objectdata explicitly, or the File Format implicitly, defines character sets otherwise. If this DataSet contains the designation function for Unicode in UTF-8 then no other announcement, designation or invocation functions are permitted in this DataSet or in records 2-6. For all other character sets, one or more escape sequences are used: for the announcement of the code extension facilities used in the data which follows, for the initial designation of the G0, G1, G2 and G3 graphic character sets and for the initial invocation of the graphic set (7 bits) or the lefthand and the right-hand graphic set (8 bits) and for the initial invocation of the C0 (7 bits) or of the C0 and the C1 control character sets (8 bits). The announcement of the code extension facilities, if transmitted, must appear in this data set. Designation and invocation of graphic and control function sets (shifting) may be transmitted anywhere where the escape and the other necessary control characters are permitted. However, it is recommended to transmit in this DataSet an initial designation and invocation, i.e. to define all designations and the shift status currently in use by transmitting the appropriate escape sequences and locking-shift functions. If is omitted, the default for records 2-6 and 8 is ISO 646 IRV (7 bits) or ISO 4873 DV (8 bits). Record 1 shall always use ISO 646 IRV or ISO 4873 DV respectively. ECMA as the ISO Registration Authority for escape sequences maintains the International Register of Coded Character Sets to be used with escape sequences, a register of Codes and allocated standardised escape sequences, which are recognised by IPTC-NAA without further approval procedure. The registration procedure is defined in ISO 2375. IPTC-NAA maintain a Register of Codes and allocated private escape sequences, which are shown in paragraph 1.2. IPTC may, as Sponsoring Authority, submit such private sequence Codes for approval as standardised sequence Codes. The registers consist of a Graphic repertoire, a Control function repertoire and a Repertoire of other coding systems (e.g. complete Codes). Together they represent the IPTC-NAA Code Library. Graphic Repertoire94-character sets (intermediate character 2/8 to 2/11)002ISO 646 IRV 4/0004ISO 646 British Version 4/1006ISO 646 USA Version (ASCII) 4/2008-1NATS Primary Set for Finland and Sweden 4/3008-2NATS Secondary Set for Finland and Sweden 4/4009-1NATS Primary Set for Denmark and Norway 4/5009-2NATS Secondary Set for Denmark and Norway 4/6010ISO 646 Swedish Version (SEN 850200) 4/7015ISO 646 Italian Version (ECMA) 5/9016ISO 646 Portuguese Version (ECMA Olivetti) 4/12017ISO 646 Spanish Version (ECMA Olivetti) 5/10018ISO 646 Greek Version (ECMA) 5/11021ISO 646 German Version (DIN 66003) 4/11037Basic Cyrillic Character Set (ISO 5427) 4/14060ISO 646 Norwegian Version (NS 4551) 6/0069ISO 646 French Version (NF Z 62010-1982) 6/6084ISO 646 Portuguese Version (ECMA IBM) 6/7085ISO 646 Spanish Version (ECMA IBM) 6/8086ISO 646 Hungarian Version (HS 7795/3) 6/9121Alternate Primary Graphic Set No. 1 (Canada CSA Z 243.4-1985) 7/7122Alternate Primary Graphic Set No. 2 (Canada CSA Z 243.4-1985) 7/896-character sets (intermediate character 2/12 to 2/15):100Right-hand Part of Latin Alphabet No. 1 (ISO 8859-1) 4/1101Right-hand Part of Latin Alphabet No. 2 (ISO 8859-2) 4/2109Right-hand Part of Latin Alphabet No. 3 (ISO 8859-3) 4/3110Right-hand Part of Latin Alphabet No. 4 (ISO 8859-4) 4/4111Right-hand Part of Latin/Cyrillic Alphabet (ISO 8859-5) 4/0125Right-hand Part of Latin/Greek Alphabet (ISO 8859-7) 4/6127Right-hand Part of Latin/Arabic Alphabet (ISO 8859-6) 4/7138Right-hand Part of Latin/Hebrew Alphabet (ISO 8859-8) 4/8139Right-hand Part of Czechoslovak Standard (ČSN 369103) 4/9Multiple-Byte Graphic Character Sets (1st intermediate character 2/4, 2nd intermediate character 2/8 to 2/11)87Japanese characters (JIS X 0208-1983) 4/2Control Function RepertoireC0 Control Function Sets (intermediate character 2/1)001C0 Set of ISO 646 4/0026IPTC C0 Set for newspaper text transmission 4/3036C0 Set of ISO 646 with SS2 instead of IS4 4/4104Minimum C0 Set for ISO 4873 4/7 C1 Control Function Sets (intermediate character 2/2)077C1 Control Set of ISO 6429 4/3105Minimum C1 Set for ISO 4873 4/7 Single Additional Control Functions062Locking-Shift Two (LS2), ISO 2022 6/14063Locking-Shift Three (LS3), ISO 2022 6/15064Locking-Shift Three Right (LS3R), ISO 2022 7/12065Locking-Shift Two Right (LS2R), ISO 2022 7/13066Locking-Shift One Right (LS1R), ISO 2022 7/14Repertoire of Other Coding Systems (e.g. complete Codes, intermediate character 2/5 )196UCS Transformation Format (UTF-8) 4/7 --></remarks>
			Public Shared ReadOnly Property CodedCharacterSet As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.CodedCharacterSet)
				End Get
			End Property
			''' <summary>UNO Unique Name of Object, providing eternal, globally unique identification for objects as specified in the IIM, independent of provider and for any media form.</summary>
			''' <remarks>The provider must ensure the UNO is unique. Objects with the same UNO are identical.</remarks>
			Public Shared ReadOnly Property UNO As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.UNO)
				End Get
			End Property
			''' <summary>The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ARMIdentifier As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ARMIdentifier)
				End Get
			End Property
			''' <summary>A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentifier'/>.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ARMVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Envelope, Envelopetags.ARMVersion)
				End Get
			End Property
			''' <summary>A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.</summary>
			''' <remarks>Version numbers are assigned by IPTC and NAA. The version number of this record is four (4).</remarks>
			Public Shared ReadOnly Property RecordVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.RecordVersion)
				End Get
			End Property
			''' <summary>The Object Type is used to distinguish between different types of objects within the IIM.</summary>
			''' <remarks>The first part is a number representing a language independent international reference to an Object Type followed by a colon separator. The second part, if used, is a text representation of the Object Type Number (maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix G, or in the language of the service as indicated in DataSet 2:135 (<see cref='LanguageIdentifier'/>)</remarks>
			Public Shared ReadOnly Property ObjectTypeReference As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectTypeReference)
				End Get
			End Property
			''' <summary>The Object Attribute defines the nature of the object independent of the Subject.</summary>
			''' <remarks>The first part is a number representing a language independent international reference to an Object Attribute followed by a colon separator. The second part, if used, is a text representation of the Object Attribute Number ( maximum 64 octets) consisting of graphic characters plus spaces either in English, as defined in Appendix G, or in the language of the service as indicated in DataSet 2:135 (<see cref='LanguageIdentifier'/>)</remarks>
			Public Shared ReadOnly Property ObjectAttributeReference As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectAttributeReference)
				End Get
			End Property
			''' <summary>Status of the objectdata, according to the practice of the provider.</summary>
			Public Shared ReadOnly Property EditStatus As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.EditStatus)
				End Get
			End Property
			''' <summary>Indicates the type of update that this object provides to a previous object. The link to the previous object is made using the ARM (DataSets 1:120 and 1:122 (<see cref='ARM'/>)), according to the practices of the provider.</summary>
			Public Shared ReadOnly Property EditorialUpdate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.EditorialUpdate)
				End Get
			End Property
			''' <summary>Specifies the editorial urgency of content and not necessarily the envelope handling priority (see 1:60, <see cref="EnvelopePriority"/>).</summary>
			''' <remarks>The '1' is most urgent, '5' normal and '8' denotes the least-urgent copy. The numerals '9' and '0' are reserved for future use.</remarks>
			Public Shared ReadOnly Property Urgency As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Urgency)
				End Get
			End Property
			''' <summary>The Subject Reference is a structured definition of the subject matter.</summary>
			''' <remarks>It must contain an IPR (default value is "IPTC"), an 8 digit Subject Reference Number and an optional Subject Name, Subject Matter Name and Subject Detail Name. Each part of the Subject reference is separated by a colon (:). The Subject Reference Number contains three parts, a 2 digit Subject Number, a 3 digit Subject Matter Number and a 3 digit Subject Detail Number thus providing unique identification of the object's subject.</remarks>
			Public Shared ReadOnly Property SubjectReference As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.SubjectReference)
				End Get
			End Property
			''' <summary>Identifies the subject of the objectdata in the opinion of the provider.</summary>
			''' <remarks>A list of categories will be maintained by a regional registry, where available, otherwise by the provider.</remarks>
			Public Shared ReadOnly Property Category As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Category)
				End Get
			End Property
			''' <summary>Supplemental categories further refine the subject of an objectdata.</summary>
			''' <remarks>Only a single supplemental category may be contained in each DataSet. A supplemental category may include any of the recognised categories as used in . Otherwise, selection of supplemental categories are left to the provider.</remarks>
			Public Shared ReadOnly Property SupplementalCategory As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.SupplementalCategory)
				End Get
			End Property
			''' <summary>Identifies objectdata that recurs often and predictably.</summary>
			''' <remarks>Enables users to immediately find or recall such an object.</remarks>
			Public Shared ReadOnly Property FixtureIdentifier As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.FixtureIdentifier)
				End Get
			End Property
			''' <summary>Used to indicate specific information retrieval words.</summary>
			''' <remarks>Each keyword uses a single Keywords DataSet. Multiple keywords use multiple Keywords DataSets. It is expected that a provider of various types of data that are related in subject matter uses the same keyword, enabling the receiving system or subsystems to search across all types of data for related material.</remarks>
			Public Shared ReadOnly Property Keywords As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Keywords)
				End Get
			End Property
			''' <summary>Indicates the code of a country/geographical location referenced by the content of the object.</summary>
			''' <remarks>Where ISO has established an appropriate country code under ISO 3166, that code will be used. When ISO3166 does not adequately provide for identification of a location or a country, e.g. ships at sea, space, IPTC will assign an appropriate threecharacter code under the provisions of ISO3166 to avoid conflicts. (see Appendix D) .</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ContentLocationCode As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ContentLocationCode)
				End Get
			End Property
			''' <summary>Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.</summary>
			''' <remarks>If used in the same object with DataSet , must immediately follow and correspond to it.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ContentLocationName As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ContentLocationName)
				End Get
			End Property
			''' <summary>The earliest date the provider intends the object to be used.</summary>
			Public Shared ReadOnly Property ReleaseDate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReleaseDate)
				End Get
			End Property
			''' <summary>The earliest time the provider intends the object to be used.</summary>
			Public Shared ReadOnly Property ReleaseTime As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReleaseTime)
				End Get
			End Property
			''' <summary>The latest date the provider or owner intends the objectdata to be used.</summary>
			Public Shared ReadOnly Property ExpirationDate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ExpirationDate)
				End Get
			End Property
			''' <summary>The latest time the provider or owner intends the objectdata to be used.</summary>
			Public Shared ReadOnly Property ExpirationTime As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ExpirationTime)
				End Get
			End Property
			''' <summary>Other editorial instructions concerning the use of the objectdata, such as embargoes and warnings.</summary>
			Public Shared ReadOnly Property SpecialInstructions As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.SpecialInstructions)
				End Get
			End Property
			''' <summary>Indicates the type of action that this object provides to a previous object.</summary>
			''' <remarks>The link to the previous object is made using the (DataSets 1:120 () and 1:122 ()), according to the practices of the provider.</remarks>
			Public Shared ReadOnly Property ActionAdvised As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ActionAdvised)
				End Get
			End Property
			''' <summary>Identifies the Service Identifier of a prior envelope to which the current object refers.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ReferenceService As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReferenceService)
				End Get
			End Property
			''' <summary>Identifies the date of a prior envelope to which the current object refers.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ReferenceDate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReferenceDate)
				End Get
			End Property
			''' <summary>Identifies the Envelope Number of a prior envelope to which the current object refers.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ReferenceNumber As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ReferenceNumber)
				End Get
			End Property
			''' <summary>The date the intellectual content of the objectdata was created rather than the date of the creation of the physical representation.</summary>
			''' <remarks>Thus a photo taken during the American Civil War would carry a creation date during that epoch (1861-1865) rather than the date the photo was digitised for archiving.</remarks>
			Public Shared ReadOnly Property DateCreated As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.DateCreated)
				End Get
			End Property
			''' <summary>The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.</summary>
			''' <remarks>Where the time cannot be precisely determined, the closest approximation should be used.</remarks>
			Public Shared ReadOnly Property TimeCreated As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.TimeCreated)
				End Get
			End Property
			''' <summary>The date the digital representation of the objectdata was created.</summary>
			''' <remarks>Thus a photo taken during the American Civil War would carry a Digital Creation Date within the past several years rather than the date where the image was captured on film, glass plate or other substrate during that epoch (1861-1865).</remarks>
			Public Shared ReadOnly Property DigitalCreationDate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.DigitalCreationDate)
				End Get
			End Property
			''' <summary>The time the digital representation of the objectdata was created.</summary>
			Public Shared ReadOnly Property DigitalCreationTime As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.DigitalCreationTime)
				End Get
			End Property
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>Note: This DataSet to form an advisory to the user and are not "computer" fields. Programmers should not expect to find computer-readable information in this DataSet.</remarks>
			Public Shared ReadOnly Property OriginatingProgram As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.OriginatingProgram)
				End Get
			End Property
			''' <summary>Identifies the type of program used to originate the objectdata.</summary>
			''' <remarks>Note: This DataSet to form an advisory to the user and are not "computer" fields. Programmers should not expect to find computer-readable information in this DataSet.</remarks>
			Public Shared ReadOnly Property ProgramVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ProgramVersion)
				End Get
			End Property
			''' <summary>Virtually only used in North America.</summary>
			Public Shared ReadOnly Property ObjectCycle As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectCycle)
				End Get
			End Property
			''' <summary>Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ByLine As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ByLine)
				End Get
			End Property
			''' <summary>A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.</summary>
			''' <remarks>Examples: "Staff Photographer", "Corresponsal", "Envoyé Spécial"</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ByLineTitle As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ByLineTitle)
				End Get
			End Property
			''' <summary>Identifies city of objectdata origin according to guidelines established by the provider.</summary>
			Public Shared ReadOnly Property City As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.City)
				End Get
			End Property
			''' <summary>Identifies the location within a city from which the objectdata originates, according to guidelines established by the provider.</summary>
			Public Shared ReadOnly Property SubLocation As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.SubLocation)
				End Get
			End Property
			''' <summary>Identifies Province/State of origin according to guidelines established by the provider.</summary>
			Public Shared ReadOnly Property ProvinceState As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ProvinceState)
				End Get
			End Property
			''' <summary>Indicates the code of the country/primary location where the intellectual property of the objectdata was created, e.g. a photo was taken, an event occurred.</summary>
			''' <remarks>Where ISO has established an appropriate country code under ISO 3166, that code will be used. When ISO3166 does not adequately provide for identification of a location or a new country, e.g. ships at sea, space, IPTC will assign an appropriate three-character code under the provisions of ISO3166 to avoid conflicts. (see Appendix D)</remarks>
			Public Shared ReadOnly Property CountryPrimaryLocationCode As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.CountryPrimaryLocationCode)
				End Get
			End Property
			''' <summary>Provides full, publishable, name of the country/primary location where the intellectual property of the objectdata was created, according to guidelines of the provider.</summary>
			Public Shared ReadOnly Property CountryPrimaryLocationName As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.CountryPrimaryLocationName)
				End Get
			End Property
			''' <summary>A code representing the location of original transmission according to practices of the provider.</summary>
			''' <remarks>Examples: BER-5, PAR-12-11-01</remarks>
			Public Shared ReadOnly Property OriginalTransmissionReference As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.OriginalTransmissionReference)
				End Get
			End Property
			''' <summary>A publishable entry providing a synopsis of the contents of the objectdata.</summary>
			Public Shared ReadOnly Property Headline As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Headline)
				End Get
			End Property
			''' <summary>Identifies the provider of the objectdata, not necessarily the owner/creator.</summary>
			Public Shared ReadOnly Property Credit As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Credit)
				End Get
			End Property
			''' <summary>Identifies the original owner of the intellectual content of the objectdata.</summary>
			''' <remarks>This could be an agency, a member of an agency or an individual.</remarks>
			Public Shared ReadOnly Property Source As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Source)
				End Get
			End Property
			''' <summary>Contains any necessary copyright notice.</summary>
			Public Shared ReadOnly Property CopyrightNotice As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.CopyrightNotice)
				End Get
			End Property
			''' <summary>Identifies the person or organisation which can provide further background information on the objectdata.</summary>
			Public Shared ReadOnly Property Contact As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.Contact)
				End Get
			End Property
			''' <summary>A textual description of the objectdata, particularly used where the object is not text.</summary>
			Public Shared ReadOnly Property CaptionAbstract As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.CaptionAbstract)
				End Get
			End Property
			''' <summary>Identification of the name of the person involved in the writing, editing or correcting the objectdata or caption/abstract.</summary>
			Public Shared ReadOnly Property WriterEditor As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.WriterEditor)
				End Get
			End Property
			''' <summary>Image width 460 pixels and image height 128 pixels. Scanning direction bottom to top, left to right.</summary>
			''' <remarks>Contains the rasterized objectdata description and is used where characters that have not been coded are required for the caption.</remarks>
			Public Shared ReadOnly Property RasterizedeCaption As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.RasterizedeCaption)
				End Get
			End Property
			''' <summary>Image Type</summary>
			Public Shared ReadOnly Property ImageType As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ImageType)
				End Get
			End Property
			''' <summary>Indicates the layout of the image area.</summary>
			Public Shared ReadOnly Property ImageOrientation As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ImageOrientation)
				End Get
			End Property
			''' <summary>Describes the major national language of the object, according to the 2-letter codes of ISO 639:1988.</summary>
			''' <remarks>Does not define or imply any coded character set, but is used for internal routing, e.g. to various editorial desks. Implementation note: Programmers should provide for three octets for Language Identifier because the ISO is expected to provide for 3-letter codes in the future.</remarks>
			Public Shared ReadOnly Property LanguageIdentifier As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.LanguageIdentifier)
				End Get
			End Property
			''' <summary>Type of audio in objectdata</summary>
			''' <remarks>Note: When '0' or 'T' is used, the only authorised combination is "0T". This is the mechanism for sending a caption either to supplement an audio cut sent previously without a caption or to correct a previously sent caption.</remarks>
			Public Shared ReadOnly Property AudioType As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioType)
				End Get
			End Property
			''' <summary>Sampling rate, representing the sampling rate in hertz (Hz).</summary>
			Public Shared ReadOnly Property AudioSamplingRate As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioSamplingRate)
				End Get
			End Property
			''' <summary>The number of bits in each audio sample.</summary>
			Public Shared ReadOnly Property AudioSamplingResolution As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioSamplingResolution)
				End Get
			End Property
			''' <summary>The running time of an audio objectdata when played back at the speed at which it was recorded.</summary>
			Public Shared ReadOnly Property AudioDuration As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioDuration)
				End Get
			End Property
			''' <summary>Identifies the content of the end of an audio objectdata, according to guidelines established by the provider.</summary>
			''' <remarks>The outcue generally consists of the final words spoken within an audio objectdata or the final sounds heard.</remarks>
			Public Shared ReadOnly Property AudioOutcue As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.AudioOutcue)
				End Get
			End Property
			''' <summary>The file format of the ObjectData Preview.</summary>
			''' <remarks>The file format must be registered with IPTC or NAA with a unique number assigned to it.</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ObjectDataPreviewFileFormat As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectDataPreviewFileFormat)
				End Get
			End Property
			''' <summary>The particular version of the ObjectData Preview File Format specified in <see cref="ObjectDataPreviewFileFormat"/></summary>
			''' <remarks>The File Format Version is taken from the list included in Appendix A</remarks>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ObjectDataPreviewFileFormatVersion As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectDataPreviewFileFormatVersion)
				End Get
			End Property
			''' <summary>Maximum size of 256000 octets consisting of binary data.</summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)> _
			Public Shared ReadOnly Property ObjectDataPreviewData As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.Application, Applicationtags.ObjectDataPreviewData)
				End Get
			End Property
			''' <summary>The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.</summary>
			Public Shared ReadOnly Property SizeMode As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PreObjectDataDescriptorRecord, PreObjectDataDescriptorRecordtags.SizeMode)
				End Get
			End Property
			''' <summary>The maximum size for the following Subfile DataSet(s).</summary>
			''' <remarks>The largest number is not defined, but programmers should provide at least for the largest binary number contained in four octets taken together. If the entire object is to be transferred together within a single DataSet 8:10, the number equals the size of the object.</remarks>
			Public Shared ReadOnly Property MaxSubfileSize As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PreObjectDataDescriptorRecord, PreObjectDataDescriptorRecordtags.MaxSubfileSize)
				End Get
			End Property
			''' <summary>A binary number representing the overall size of the objectdata, expressed in octets, not including tags, if that size is known when transfer commences.</summary>
			''' <remarks>Mandatory if DataSet has value '1' and not allowed if DataSet has value '0'.</remarks>
			Public Shared ReadOnly Property ObjectDataSizeAnnounced As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PreObjectDataDescriptorRecord, PreObjectDataDescriptorRecordtags.ObjectDataSizeAnnounced)
				End Get
			End Property
			''' <summary>Used when objectdata size is not known, indicating the largest size, expressed in octets, that the objectdata can possibly have, not including tags.</summary>
			Public Shared ReadOnly Property MaximumObjectDataSize As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PreObjectDataDescriptorRecord, PreObjectDataDescriptorRecordtags.MaximumObjectDataSize)
				End Get
			End Property
			''' <summary>Subfile DataSet containing the objectdata itself.</summary>
			''' <remarks>Subfiles must be sequential so that the subfiles may be reassembled.</remarks>
			Public Shared ReadOnly Property Subfile As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.ObjectDataRecord, ObjectDataRecordtags.Subfile)
				End Get
			End Property
			''' <summary>Total size of the objectdata, in octets, without tags.</summary>
			''' <remarks>This number should equal the number in DataSet if the size of the objectdata is known and has been provided.</remarks>
			Public Shared ReadOnly Property ConfirmedObjectDataSize As DataSetIdentification
				<DebuggerStepThrough()> Get
					Return New DataSetIdentification(RecordNumbers.PostObjectDataDescriptorRecord, PostObjectDataDescriptorRecordtags.ConfirmedObjectDataSize)
				End Get
			End Property
			''' <summary>Returns all known data sets</summary>
			''' <param name="Hidden">Returns also datasets that are within groups</param>
			Public Shared Function KnownDataSets(Optional ByVal Hidden As Boolean = False) As DataSetIdentification()
				If Hidden Then
					Return New DataSetIdentification(){ModelVersion, Destination, FileFormat, FileFormatVersion, ServiceIdentifier, EnvelopeNumber, ProductID, EnvelopePriority, DateSent, TimeSent, CodedCharacterSet, UNO, ARMIdentifier, ARMVersion, RecordVersion, ObjectTypeReference, ObjectAttributeReference, EditStatus, EditorialUpdate, Urgency, SubjectReference, Category, SupplementalCategory, FixtureIdentifier, Keywords, ContentLocationCode, ContentLocationName, ReleaseDate, ReleaseTime, ExpirationDate, ExpirationTime, SpecialInstructions, ActionAdvised, ReferenceService, ReferenceDate, ReferenceNumber, DateCreated, TimeCreated, DigitalCreationDate, DigitalCreationTime, OriginatingProgram, ProgramVersion, ObjectCycle, ByLine, ByLineTitle, City, SubLocation, ProvinceState, CountryPrimaryLocationCode, CountryPrimaryLocationName, OriginalTransmissionReference, Headline, Credit, Source, CopyrightNotice, Contact, CaptionAbstract, WriterEditor, RasterizedeCaption, ImageType, ImageOrientation, LanguageIdentifier, AudioType, AudioSamplingRate, AudioSamplingResolution, AudioDuration, AudioOutcue, ObjectDataPreviewFileFormat, ObjectDataPreviewFileFormatVersion, ObjectDataPreviewData, SizeMode, MaxSubfileSize, ObjectDataSizeAnnounced, MaximumObjectDataSize, Subfile, ConfirmedObjectDataSize}
				Else
					Return New DataSetIdentification(){ModelVersion, Destination, FileFormat, FileFormatVersion, ServiceIdentifier, EnvelopeNumber, ProductID, EnvelopePriority, DateSent, TimeSent, CodedCharacterSet, UNO, RecordVersion, ObjectTypeReference, ObjectAttributeReference, EditStatus, EditorialUpdate, Urgency, SubjectReference, Category, SupplementalCategory, FixtureIdentifier, Keywords, ReleaseDate, ReleaseTime, ExpirationDate, ExpirationTime, SpecialInstructions, ActionAdvised, DateCreated, TimeCreated, DigitalCreationDate, DigitalCreationTime, OriginatingProgram, ProgramVersion, ObjectCycle, City, SubLocation, ProvinceState, CountryPrimaryLocationCode, CountryPrimaryLocationName, OriginalTransmissionReference, Headline, Credit, Source, CopyrightNotice, Contact, CaptionAbstract, WriterEditor, RasterizedeCaption, ImageType, ImageOrientation, LanguageIdentifier, AudioType, AudioSamplingRate, AudioSamplingResolution, AudioDuration, AudioOutcue, SizeMode, MaxSubfileSize, ObjectDataSizeAnnounced, MaximumObjectDataSize, Subfile, ConfirmedObjectDataSize}
				End If
			End Function
		End Structure
#End Region
#Region "Enums"
		''' <summary>Possible values of <see cref="ActionAdvised"/></summary>
		<Restrict(True)> Public Enum AdvisedActions As Byte
			''' <summary>Object Kill. Signifies that the provider wishes the holder of a copy of the referenced object make no further use of that information and take steps to prevent further distribution thereof.</summary>
			''' <remarks>Implies that any use of the object might result in embarrassment or other exposure of the provider and/or recipient.</remarks>
			<FieldDisplayName("Object Kill")> ObjectKill = 1
			''' <summary>Object Replace. Signifies that the provider wants to replace the referenced object with the object provided under the current envelope.</summary>
			<FieldDisplayName("Object Replace")> ObjectReplace = 2
			''' <summary>Object Append. Signifies that the provider wants to append to the referenced object information provided in the objectdata of the current envelope.</summary>
			<FieldDisplayName("Object Append")> ObjectAppend = 3
			''' <summary>Object Reference. Signifies that the provider wants to make reference to objectdata in a different envelope.</summary>
			<FieldDisplayName("Object Reference")> ObjectReference = 4
		End Enum
		''' <summary>Abstract Relation Methods Identifiers</summary>
		<Restrict(True)> <CLSCompliant(False)> Public Enum ARMMethods As UShort
			''' <summary>Using DataSets 2:45, 2:47 and 2:50 (<see cref='ReferenceService'/>, <see cref='ReferenceDate'/> and <see cref='ReferenceNumber'/>)</summary>
			<FieldDisplayName("Method 1 (Reference service, date, number)")> IPTCMethod1 = 1
			''' <summary>Using DataSet 1:100 (<see cref='UNO'/>)</summary>
			<FieldDisplayName("Method 2 (UNO)")> IPTCMethod2 = 2
		End Enum
		''' <summary>Abstract Relation Method Versions</summary>
		<Restrict(True)> <CLSCompliant(False)> Public Enum ARMVersions As UShort
			''' <summary>The only ARM version</summary>
			<FieldDisplayName("Version 1")> ARM1 = 1
		End Enum
		''' <summary>Subject Detail Name and Subject Refrence Number relationship (Economy, Business &amp; Finnance)</summary>
		<Restrict(True)> Public Enum EconomySubjectDetail As Integer
			''' <summary>Arable Farming</summary>
			<FieldDisplayName("Arable Farming")> ArableFarming = 04001001
			''' <summary>Fishing Industry</summary>
			<FieldDisplayName("Fishing Industry")> FishingIndustry = 04001002
			''' <summary>Forestry &amp; Timber</summary>
			<FieldDisplayName("Forestry & Timber")> ForestryAndTimber = 04001003
			''' <summary>Livestock Farming</summary>
			<FieldDisplayName("Livestock Farming")> LivestockFarming = 04001004
			''' <summary>Biotechnology</summary>
			<FieldDisplayName("Biotechnology")> Biotechnology = 04002001
			''' <summary>Fertilisers</summary>
			<FieldDisplayName("Fertilisers")> Fertilisers = 04002002
			''' <summary>Health &amp; Beauty products</summary>
			<FieldDisplayName("Health & Beauty products")> HealthAndBeautyProducts = 04002003
			''' <summary>Inorganic chemicals</summary>
			<FieldDisplayName("Inorganic chemicals")> InorganicChemicals = 04002004
			''' <summary>Organic chemicals</summary>
			<FieldDisplayName("Organic chemicals")> OrganicChemicals = 04002005
			''' <summary>Pharmaceuticals</summary>
			<FieldDisplayName("Pharmaceuticals")> Pharmaceuticals = 04002006
			''' <summary>Synthetics &amp; Plastics</summary>
			<FieldDisplayName("Synthetics & Plastics")> SyntheticsAndPlastics = 04002007
			''' <summary>Hardware</summary>
			<FieldDisplayName("Hardware")> Hardware = 04003001
			''' <summary>Networking</summary>
			<FieldDisplayName("Networking")> Networking = 04003002
			''' <summary>Satellite technology</summary>
			<FieldDisplayName("Satellite technology")> SatelliteTechnology = 04003003
			''' <summary>Semiconductors &amp; active components</summary>
			<FieldDisplayName("Semiconductors & active components")> SemiconductorsAndActiveComponents = 04003004
			''' <summary>Software</summary>
			<FieldDisplayName("Software")> Software = 04003005
			''' <summary>Telecommunications Equipment</summary>
			<FieldDisplayName("Telecommunications Equipment")> TelecommunicationsEquipment = 04003006
			''' <summary>Telecommunications Services</summary>
			<FieldDisplayName("Telecommunications Services")> TelecommunicationsServices = 04003007
			''' <summary>Heavy construction</summary>
			<FieldDisplayName("Heavy construction")> HeavyConstruction = 04004001
			''' <summary>House building</summary>
			<FieldDisplayName("House building")> HouseBuilding = 04004002
			''' <summary>Real Estate</summary>
			<FieldDisplayName("Real Estate")> RealEstate = 04004003
			''' <summary>Alternative energy</summary>
			<FieldDisplayName("Alternative energy")> AlternativeEnergy = 04005001
			''' <summary>Coal</summary>
			<FieldDisplayName("Coal")> Coal = 04005002
			''' <summary>Oil &amp; Gas - Downstream activities</summary>
			<FieldDisplayName("Oil & Gas - Downstream activities")> OilAndGasDownstreamActivities = 04005003
			''' <summary>Oil &amp; Gas - Upstream activities</summary>
			<FieldDisplayName("Oil & Gas - Upstream activities")> OilAndGasUpstreamActivities = 04005004
			''' <summary>Nuclear power</summary>
			<FieldDisplayName("Nuclear power")> NuclearPower = 04005005
			''' <summary>Electricity Production &amp; Distribution</summary>
			<FieldDisplayName("Electricity Production & Distribution")> ElectricityProductionAndDistribution = 04005006
			''' <summary>Waste Management &amp; Pollution Control</summary>
			<FieldDisplayName("Waste Management & Pollution Control")> WasteManagementAndPollutionControl = 04005007
			''' <summary>Water Supply</summary>
			<FieldDisplayName("Water Supply")> WaterSupply = 04005008
			''' <summary>Accountancy &amp; Auditing</summary>
			<FieldDisplayName("Accountancy & Auditing")> AccountancyAndAuditing = 04006001
			''' <summary>Banking</summary>
			<FieldDisplayName("Banking")> Banking = 04006002
			''' <summary>Consultancy Services</summary>
			<FieldDisplayName("Consultancy Services")> ConsultancyServices = 04006003
			''' <summary>Employment Agencies</summary>
			<FieldDisplayName("Employment Agencies")> EmploymentAgencies = 04006004
			''' <summary>Healthcare Providers</summary>
			<FieldDisplayName("Healthcare Providers")> HealthcareProviders = 04006005
			''' <summary>Insurance</summary>
			<FieldDisplayName("Insurance")> Insurance = 04006006
			''' <summary>Legal services</summary>
			<FieldDisplayName("Legal services")> LegalServices = 04006007
			''' <summary>Market research</summary>
			<FieldDisplayName("Market research")> MarketResearch = 04006008
			''' <summary>Stock broking</summary>
			<FieldDisplayName("Stock broking")> StockBroking = 04006009
			''' <summary>Clothing</summary>
			<FieldDisplayName("Clothing")> Clothing = 04007001
			''' <summary>Department stores</summary>
			<FieldDisplayName("Department stores")> DepartmentStores = 04007002
			''' <summary>Food</summary>
			<FieldDisplayName("Food (distribution)")> FoodDistribution = 04007003
			''' <summary>Mail Order</summary>
			<FieldDisplayName("Mail Order")> MailOrder = 04007004
			''' <summary>Retail</summary>
			<FieldDisplayName("Retail")> Retail = 04007005
			''' <summary>Speciality stores</summary>
			<FieldDisplayName("Speciality stores")> SpecialityAtores = 04007006
			''' <summary>Wholesale</summary>
			<FieldDisplayName("Wholesale")> Wholesale = 04007007
			''' <summary>Central Banks</summary>
			<FieldDisplayName("Central Banks")> CentralBanks = 04008001
			''' <summary>Consumer Issues</summary>
			<FieldDisplayName("Consumer Issues")> ConsumerIssues = 04008002
			''' <summary>Debt Markets</summary>
			<FieldDisplayName("Debt Markets")> DebtMarkets = 04008003
			''' <summary>Economic Indicators</summary>
			<FieldDisplayName("Economic Indicators")> EconomicIndicators = 04008004
			''' <summary>Emerging Markets Debt</summary>
			<FieldDisplayName("Emerging Markets Debt")> EmergingMarketsDebt = 04008005
			''' <summary>Foreign Exchange Markets</summary>
			<FieldDisplayName("Foreign Exchange Markets")> ForeignExchangeMarkets = 04008006
			''' <summary>Government Aid</summary>
			<FieldDisplayName("Government Aid")> GovernmentAid = 04008007
			''' <summary>Government Debt</summary>
			<FieldDisplayName("Government Debt")> GovernmentDebt = 04008008
			''' <summary>Interest Rates</summary>
			<FieldDisplayName("Interest Rates")> InterestRates = 04008009
			''' <summary>International Economic Institutions</summary>
			<FieldDisplayName("International Economic Institutions")> InternationalEconomicInstitutions = 04008010
			''' <summary>International Trade Issues</summary>
			<FieldDisplayName("International Trade Issues")> InternationalTradeIssues = 04008011
			''' <summary>Loan Markets</summary>
			<FieldDisplayName("Loan Markets")> LoanMarkets = 04008012
			''' <summary>Energy</summary>
			<FieldDisplayName("Energy")> Energy = 04009001
			''' <summary>Metals</summary>
			<FieldDisplayName("Metals")> Metals = 04009002
			''' <summary>Securities</summary>
			<FieldDisplayName("Securities")> Securities = 04009003
			''' <summary>Soft Commodities</summary>
			<FieldDisplayName("Soft Commodities")> SoftCommodities = 04009004
			''' <summary>Advertising</summary>
			<FieldDisplayName("Advertising")> Advertising = 04010001
			''' <summary>Books</summary>
			<FieldDisplayName("Books")> Books = 04010002
			''' <summary>Cinema</summary>
			<FieldDisplayName("Cinema")> Cinema = 04010003
			''' <summary>News Agencies</summary>
			<FieldDisplayName("News Agencies")> NewsAgencies = 04010004
			''' <summary>Newspaper &amp; Magazines</summary>
			<FieldDisplayName("Newspaper & Magazines")> NewspaperAndMagazines = 04010005
			''' <summary>Online</summary>
			<FieldDisplayName("Online")> Online = 04010006
			''' <summary>Public Relations</summary>
			<FieldDisplayName("Public Relations")> PublicRelations = 04010007
			''' <summary>Radio</summary>
			<FieldDisplayName("Radio")> Radio = 04010008
			''' <summary>Satellite &amp; Cable Services</summary>
			<FieldDisplayName("Satellite & Cable Services")> SatelliteAndCableServices = 04010009
			''' <summary>Television</summary>
			<FieldDisplayName("Television")> Television = 04010010
			''' <summary>Aerospace</summary>
			<FieldDisplayName("Aerospace")> Aerospace = 04011001
			''' <summary>Automotive Equipment</summary>
			<FieldDisplayName("Automotive Equipment")> AutomotiveEquipment = 04011002
			''' <summary>Defence Equipment</summary>
			<FieldDisplayName("Defence Equipment")> DefenceEquipment = 04011003
			''' <summary>Electrical Appliances</summary>
			<FieldDisplayName("Electrical Appliances")> ElectricalAppliances = 04011004
			''' <summary>Heavy engineering</summary>
			<FieldDisplayName("Heavy engineering")> HeavyEngineering = 04011005
			''' <summary>Industrial components</summary>
			<FieldDisplayName("Industrial components")> IndustrialComponents = 04011006
			''' <summary>Instrument engineering</summary>
			<FieldDisplayName("Instrument engineering")> InstrumentEngineering = 04011007
			''' <summary>Shipbuilding</summary>
			<FieldDisplayName("Shipbuilding")> Shipbuilding = 04011008
			''' <summary>Building materials</summary>
			<FieldDisplayName("Building materials")> BuildingMaterials = 04012001
			''' <summary>Gold &amp; Precious Materials</summary>
			<FieldDisplayName("Gold & Precious Materials")> GoldAndPreciousMaterials = 04012002
			''' <summary>Iron &amp; Steel</summary>
			<FieldDisplayName("Iron & Steel")> IronAndSteel = 04012003
			''' <summary>Non ferrous metals</summary>
			<FieldDisplayName("Non ferrous metals")> NonFerrousMetals = 04012004
			''' <summary>Alcoholic Drinks</summary>
			<FieldDisplayName("Alcoholic Drinks")> AlcoholicDrinks = 04013001
			''' <summary>Food</summary>
			<FieldDisplayName("Food (industry)")> FoodIndustry = 04013002
			''' <summary>Furnishings &amp; Furniture</summary>
			<FieldDisplayName("Furnishings & Furniture")> FurnishingsAndFurniture = 04013003
			''' <summary>Paper &amp; packaging products</summary>
			<FieldDisplayName("Paper & packaging products")> PaperAndPackagingProducts = 04013004
			''' <summary>Rubber products</summary>
			<FieldDisplayName("Rubber products")> Rubberproducts = 04013005
			''' <summary>Soft Drinks</summary>
			<FieldDisplayName("Soft Drinks")> SoftDrinks = 04013006
			''' <summary>Textiles &amp; Clothing</summary>
			<FieldDisplayName("Textiles & Clothing")> TextilesAndClothing = 04013007
			''' <summary>Tobacco</summary>
			<FieldDisplayName("Tobacco")> Tobacco = 04013008
			''' <summary>Casinos &amp; Gambling</summary>
			<FieldDisplayName("Casinos & Gambling")> CasinosAndGambling = 04014001
			''' <summary>Hotels &amp; accommodation</summary>
			<FieldDisplayName("Hotels & mp; accommodation")> HotelsAndAccommodation = 04014002
			''' <summary>Recreational &amp; Sports goods</summary>
			<FieldDisplayName("Recreational & Sports goods")> RecreationalAndSportsGoods = 04014003
			''' <summary>Restaurants &amp; catering</summary>
			<FieldDisplayName("Restaurants & catering")> RestaurantsAndCatering = 04014004
			''' <summary>Tour operators</summary>
			<FieldDisplayName("Tour operators")> TourOperators = 04014005
			''' <summary>Air Transport</summary>
			<FieldDisplayName("Air Transport")> AirTransport = 04015001
			''' <summary>Railway</summary>
			<FieldDisplayName("Railway")> Railway = 04015002
			''' <summary>Road Transport</summary>
			<FieldDisplayName("Road Transport")> RoadTransport = 04015003
			''' <summary>Waterway &amp; Maritime Transport</summary>
			<FieldDisplayName("Waterway & Maritime Transport")> WaterwayAndMaritimeTransport = 04015004
		End Enum
		''' <summary>Values for <see cref="EditorialUpdate"/></summary>
		<Restrict(True)> Public Enum EditorialUpdateValues As Byte
			''' <summary>Additional language. Signifies that the accompanying Record 2 DataSets repeat information from another object in a different natural language (as indicated by DataSet 2:135 - <see cref='LanguageIdentifier'/>).</summary>
			<FieldDisplayName("Additional language")> AdditionalLanguage = 1
		End Enum
		''' <summary>Registered file formats by IPTC and NAA</summary>
		<Restrict(True)> <CLSCompliant(False)> Public Enum FileFormats As UShort
			''' <summary>No Object Data</summary>
			<FieldDisplayName("No Object Data")> NoObjectData = 0
			''' <summary>IPTC-NAA Digital Newsphoto Parameter Record</summary>
			<FieldDisplayName("Digital Newsphoto Parameter Record")> NewsphotoParameterRecord = 01
			''' <summary>IPTC7901 Recommended Message Format</summary>
			<FieldDisplayName("Recommended Message Format")> RecommendedMessageFormat = 02
			''' <summary>Tagged Image File Format (Adobe/Aldus Image data) (Recommended for image ObjectData Preview)</summary>
			<FieldDisplayName("TIFF")> TIFF = 03
			''' <summary>Illustrator (Adobe Graphics data)</summary>
			<FieldDisplayName("Adobe Illustrator")> AdobeIllustrator = 04
			''' <summary>AppleSingle (Apple Computer Inc)</summary>
			<FieldDisplayName("Apple Single")> AppleSingle = 05
			''' <summary>NAA 89-3 (ANPA 1312)</summary>
			<FieldDisplayName("NAA 89-3")> NAA89_3 = 06
			''' <summary>MacBinary II</summary>
			<FieldDisplayName("MAcBinary II")> MacBinary = 07
			''' <summary>IPTC Unstructured Character Oriented File Format (UCOFF)</summary>
			<FieldDisplayName("IPTC Unstructured Character Oriented File Format")> UCOFF = 08
			''' <summary>United Press International ANPA 1312 variant</summary>
			<FieldDisplayName("United Press International ANPA 1312")> UnitedPressInternationalANPA1312 = 09
			''' <summary>United Press International Down-Load Message</summary>
			<FieldDisplayName("Down-Load message")> UnitedPressInternationalDownLoadMessage = 10
			''' <summary>¤ JPEG File Interchange (JFIF) (Recommended for image ObjectData Preview)</summary>
			<FieldDisplayName("JPEG")> JPEG = 11
			''' <summary>Photo-CD Image-Pac (Eastman Kodak)</summary>
			<FieldDisplayName("Koned Photo-CD Image-Pac")> PhotoCDImagePac = 12
			''' <summary>¤ Microsoft Bit Mapped Graphics File [*.BMP] (Recommended for image ObjectData Preview)</summary>
			<FieldDisplayName("BMP")> BMP = 13
			''' <summary>Digital Audio File [*.WAV] (Microsoft &amp; Creative Labs)</summary>
			<FieldDisplayName("WAV")> WAV = 14
			''' <summary>Audio plus Moving Video [*.AVI] (Microsoft)</summary>
			<FieldDisplayName("AVI")> AVI = 15
			''' <summary>PC DOS/Windows Executable Files [*.COM][*.EXE]</summary>
			<FieldDisplayName("EXE, COM")> EXE = 16
			''' <summary>Compressed Binary File [*.ZIP] (PKWare Inc)</summary>
			<FieldDisplayName("ZIP")> ZIP = 17
			''' <summary>Audio Interchange File Format AIFF (Apple Computer Inc)</summary>
			<FieldDisplayName("AIFF (Apple)")> AIFF = 18
			''' <summary>RIFF Wave (Microsoft Corporation)</summary>
			<FieldDisplayName("RIFF Wave")> RIFFWave = 19
			''' <summary>Freehand (Macromedia/Aldus)</summary>
			<FieldDisplayName("Freehand")> Freehand = 20
			''' <summary>Hypertext Markup Language "HTML" (The Internet Society)</summary>
			<FieldDisplayName("HTML")> HTML = 21
			''' <summary>MPEG 2 Audio Layer 2 (Musicom), ISO/IEC</summary>
			<FieldDisplayName("MPEG 2 Audio")> MP2 = 22
			''' <summary>MPEG 2 Audio Layer 3, ISO/IEC</summary>
			<FieldDisplayName("MP3")> MP3 = 23
			''' <summary>Portable Document File (*.PDF) Adobe</summary>
			<FieldDisplayName("PDF")> PDF = 24
			''' <summary>News Industry Text Format (NITF)</summary>
			<FieldDisplayName("News Industry Text Format")> NITF = 25
			''' <summary>Tape Archive (*.TAR)</summary>
			<FieldDisplayName("TAR")> TAR = 26
			''' <summary>Tidningarnas Telegrambyrå NITF version (TTNITF DTD)</summary>
			<FieldDisplayName("TINITF DTD")> TTNITF_DTD = 27
			''' <summary>Ritzaus Bureau NITF version (RBNITF DTD)</summary>
			<FieldDisplayName("RBNITF DTD")> RBNITF_DTD = 28
			''' <summary>Corel Draw [*.CDR]</summary>
			<FieldDisplayName("Corel Draw")> CorelDraw = 29
		End Enum
		''' <summary>File format version registered for NAA and IPTC</summary>
		<Restrict(True)> <CLSCompliant(False)> Public Enum FileFormatVersions As UShort
			''' <summary>Version 1 for FileFormat <see cref="FileFormats.NoObjectData"/></summary>
			<FieldDisplayName("1 (No Object Data)")> V0 = 0
			''' <summary>Version 1 for file fromat <see cref="FileFormats.NewsphotoParameterRecord"/> and <see cref="FileFormats.NAA89_3"/>, 5.0 for <see cref="FileFormats.TIFF"/>, 1.40 for <see cref="FileFormats.AdobeIllustrator"/>, 2 for <see cref="FileFormats.AppleSingle"/>, 1.02 for <see cref="FileFormats.JPEG"/> and 3.1 for <see cref="FileFormats.Freehand"/></summary>
			<FieldDisplayName("1 (IPTC-NAA Digital Newsphoto Parameter Record, NAA 89-3), 5.0 (TIFF), 1.40 (Adobe Illustrator), 2 (Apple Single), 1.02 (JPEG), 3.1 (Freehand)")> V1 = 1
			''' <summary>Version 2 for file format <see cref="FileFormats.NewsphotoParameterRecord"/>, 6.0 for <see cref="FileFormats.TIFF"/>, 4.0 for <see cref="FileFormats.Freehand"/> and 2.0 for <see cref="FileFormats.HTML"/></summary>
			<FieldDisplayName("2 (IPTC-NAA Digital Newsphoto Parameter Record), 6.0 (TIFF), 4.0 (Freehand), 2.0 (HTML)")> V2 = 2
			''' <summary>Version 3 for file format <see cref="FileFormats.NewsphotoparameterRecord"/> and 5.0 for <see cref="FileFormats.Freehand"/></summary>
			<FieldDisplayName("3 (IPTC-NAA Digital Newsphoto Parameter Record), 5.0 (Freehand)")> V3 = 3
			''' <summary>Version 4 for file format <see cref="FileFormats.NewsphotoparameterRecord"/> and <see cref="FileFormats.RecommendedMessageFormat"/> and 5.5 for <see cref="FileFormats.Freehand"/></summary>
			<FieldDisplayName("4 (IPTC-NAA Digital Newsphoto Parameter Record, Recommended Message Format), 5.5 (Freehand)")> V4 = 4
		End Enum
		''' <summary>Number of components in image and special meanings of some numbers</summary>
		<Restrict(True)> Public Enum ImageTypeComponents As Byte
			''' <summary>Record 2 caption for specific image</summary>
			<FieldDisplayName("NoObjectData")> NoObjectData = 0
			''' <summary>Image has 1 component</summary>
			<FieldDisplayName("Black and White")> BW = 1
			''' <summary>Image has 2 components</summary>
			<FieldDisplayName("2 components")> Component2 = 2
			''' <summary>Image has 3 components</summary>
			<FieldDisplayName("3 components")> Component3 = 3
			''' <summary>Image has 4 components</summary>
			<FieldDisplayName("4 components")> Component4 = 4
			''' <summary>the objectdata contains supplementary data to an image</summary>
			<FieldDisplayName("Suplementary data")> SuplementaryData = 9
		End Enum
		''' <summary>Object Attribute Number abd Object Name relationship</summary>
		<Restrict(True)> Public Enum ObjectAttributes As Byte
			''' <summary>Current</summary>
			<FieldDisplayName("Current")> Current = 01
			''' <summary>Analysis</summary>
			<FieldDisplayName("Analysis")> Analysis = 02
			''' <summary>Archive material</summary>
			<FieldDisplayName("Archive material")> ArchiveMaterial = 03
			''' <summary>Background</summary>
			<FieldDisplayName("Background")> Background = 04
			''' <summary>Feature</summary>
			<FieldDisplayName("Feature")> Feature = 05
			''' <summary>Forecast</summary>
			<FieldDisplayName("Forecast")> Forecast = 06
			''' <summary>History</summary>
			<FieldDisplayName("History")> History = 07
			''' <summary>Obituary</summary>
			<FieldDisplayName("Obituary")> Obituary = 08
			''' <summary>Opinion</summary>
			<FieldDisplayName("Opinion")> Opinion = 09
			''' <summary>Polls &amp; Surveys</summary>
			<FieldDisplayName("Polls & Survays")> PollsAndSurveys = 10
			''' <summary>Profile</summary>
			<FieldDisplayName("Profile")> Profile = 11
			''' <summary>Results Listings &amp; Tables</summary>
			<FieldDisplayName("Results Listings & Tables")> ResultsListingsAndTables = 12
			''' <summary>Side bar &amp; Supporting information</summary>
			<FieldDisplayName("Side bar & Supporting information")> SideBarAndSupportingInformation = 13
			''' <summary>Summary</summary>
			<FieldDisplayName("Summary")> Summary = 14
			''' <summary>Transcript &amp; Verbatim</summary>
			<FieldDisplayName("Transcript & Verbatim")> TranscriptAndVerbatim = 15
			''' <summary>Interview</summary>
			<FieldDisplayName("Interview")> Interview = 16
			''' <summary>From the Scene</summary>
			<FieldDisplayName("From the Scene")> FromTheScene = 17
			''' <summary>Retrospective</summary>
			<FieldDisplayName("Retrospective")> Retrospective = 18
			''' <summary>Statistics</summary>
			<FieldDisplayName("Statistics")> Statistics = 19
			''' <summary>Update</summary>
			<FieldDisplayName("Update")> Update = 20
			''' <summary>Wrap-up</summary>
			<FieldDisplayName("Wrap-up")> WrapUp = 21
			''' <summary>Press Release</summary>
			<FieldDisplayName("Press Release")> PressRelease = 22
		End Enum
		''' <summary>Object Type Number and Object Type Name relationship</summary>
		<Restrict(True)> Public Enum ObjectTypes As Byte
			''' <summary>News</summary>
			<FieldDisplayName("News")> News = 1
			''' <summary>Data. Data in this context implies typically non narrative information, usually not eligible for journalistic intervention or modification. It also applies to information routed by the provider from a third party to the user. Examples are sports results, stock prices and agate.</summary>
			<FieldDisplayName("Data")> Data = 2
			''' <summary>Advisory</summary>
			<FieldDisplayName("Advisory")> Advisory = 3
		End Enum
		''' <summary>Subject Matter Name and Subject Reference Number relationship</summary>
		<Restrict(True)> Public Enum SubjectMatterNumbers As Integer
			''' <summary>Archaeology</summary>
			<FieldDisplayName("Archaeology")> Archaeology = 01001000
			''' <summary>Architecture</summary>
			<FieldDisplayName("Architecture")> Architecture = 01002000
			''' <summary>Bullfighting</summary>
			<FieldDisplayName("Bullfighting")> Bullfighting = 01003000
			''' <summary>Carnival</summary>
			<FieldDisplayName("Carnival")> Carnival = 01004000
			''' <summary>Cinema</summary>
			<FieldDisplayName("Cinema")> Cinema = 01005000
			''' <summary>Dance</summary>
			<FieldDisplayName("Dance")> Dance = 01006000
			''' <summary>Fashion</summary>
			<FieldDisplayName("Fashion")> Fashion = 01007000
			''' <summary>Language</summary>
			<FieldDisplayName("Language")> Language = 01008000
			''' <summary>Libraries &amp; Museums</summary>
			<FieldDisplayName("Libraries & Museums")> LibrariesMuseums = 01009000
			''' <summary>Literature</summary>
			<FieldDisplayName("Literature")> Literature = 01010000
			''' <summary>Music</summary>
			<FieldDisplayName("Music")> Music = 01011000
			''' <summary>Painting</summary>
			<FieldDisplayName("Painting")> Painting = 01012000
			''' <summary>Photography</summary>
			<FieldDisplayName("Photography")> Photography = 01013000
			''' <summary>Radio</summary>
			<FieldDisplayName("Radio")> Radio = 01014000
			''' <summary>Sculpture</summary>
			<FieldDisplayName("Sculpture")> Sculpture = 01015000
			''' <summary>Television</summary>
			<FieldDisplayName("Television")> Television = 01016000
			''' <summary>Theatre</summary>
			<FieldDisplayName("Theatre")> Theatre = 01017000
			''' <summary>Crime</summary>
			<FieldDisplayName("Crime")> Crime = 02001000
			''' <summary>Judiciary</summary>
			<FieldDisplayName("Judiciary")> Judiciary = 02002000
			''' <summary>Police</summary>
			<FieldDisplayName("Police")> Police = 02003000
			''' <summary>Punishment</summary>
			<FieldDisplayName("Punishment")> Punishment = 02004000
			''' <summary>Prison</summary>
			<FieldDisplayName("Prison")> Prison = 02005000
			''' <summary>Drought</summary>
			<FieldDisplayName("Drought")> Drought = 03001000
			''' <summary>Earthquake</summary>
			<FieldDisplayName("Earthquake")> Earthquake = 03002000
			''' <summary>Famine</summary>
			<FieldDisplayName("Famine")> Famine = 03003000
			''' <summary>Fire</summary>
			<FieldDisplayName("Fire")> Fire = 03004000
			''' <summary>Flood</summary>
			<FieldDisplayName("Flood")> Flood = 03005000
			''' <summary>Industrial accident</summary>
			<FieldDisplayName("Industrial accident")> IndustrialAccident = 03006000
			''' <summary>Meteorological disaster</summary>
			<FieldDisplayName("Meteorological disaster")> MeteorologicalDisaster = 03007000
			''' <summary>Nuclear accident</summary>
			<FieldDisplayName("Nuclear accident")> NuclearAccident = 03008000
			''' <summary>Pollution</summary>
			<FieldDisplayName("Pollution")> Pollution = 03009000
			''' <summary>Transport accident</summary>
			<FieldDisplayName("Transport accident")> TransportAccident = 03010000
			''' <summary>Volcanic eruption</summary>
			<FieldDisplayName("Volcanic eruption")> VolcanicEruption = 03011000
			''' <summary>Agriculture</summary>
			<FieldDisplayName("Agriculture")> Agriculture = 04001000
			''' <summary>Chemicals</summary>
			<FieldDisplayName("Chemicals")> Chemicals = 04002000
			''' <summary>Computing &amp; Information Technology</summary>
			<FieldDisplayName("Computing & Information Technology")> ComputingAndInformationTechnology = 04003000
			''' <summary>Construction &amp; Property</summary>
			<FieldDisplayName("Construction & Property")> ConstructionAndProperty = 04004000
			''' <summary>Energy &amp; Resources</summary>
			<FieldDisplayName("Energy & Resources")> EnergyAndResources = 04005000
			''' <summary>Financial &amp; Business Services</summary>
			<FieldDisplayName("Financial & Business Services")> FinancialAndBusinessServices = 04006000
			''' <summary>Goods Distribution</summary>
			<FieldDisplayName("Goods Distribution")> GoodsDistribution = 04007000
			''' <summary>Macro Economics</summary>
			<FieldDisplayName("Macro Economics")> MacroEconomics = 04008000
			''' <summary>Markets</summary>
			<FieldDisplayName("Markets")> Markets = 04009000
			''' <summary>Media</summary>
			<FieldDisplayName("Media")> Media = 04010000
			''' <summary>Metal Goods &amp; Engineering</summary>
			<FieldDisplayName("Metal Goods & Engineering")> MetalGoodsAndEngineering = 04011000
			''' <summary>Metals &amp; Minerals</summary>
			<FieldDisplayName("Metals & Minerals")> MetalsAndMinerals = 04012000
			''' <summary>Process Industries</summary>
			<FieldDisplayName("Process Industries")> ProcessIndustries = 04013000
			''' <summary>Tourism &amp; Leisure</summary>
			<FieldDisplayName("Tourism & Leisure")> TourismAndLeisure = 04014000
			''' <summary>Transport</summary>
			<FieldDisplayName("Transport")> Transport = 04015000
			''' <summary>Adult Education</summary>
			<FieldDisplayName("Adult Education")> AdultEducation = 05001000
			''' <summary>Further Education</summary>
			<FieldDisplayName("Further Education")> FurtherEducation = 05002000
			''' <summary>Parent Organisations</summary>
			<FieldDisplayName("Parent Organisations")> ParentOrganisations = 05003000
			''' <summary>Preschooling</summary>
			<FieldDisplayName("Preschooling")> Preschooling = 05004000
			''' <summary>Schools</summary>
			<FieldDisplayName("Schools")> Schools = 05005000
			''' <summary>Teachers Unions</summary>
			<FieldDisplayName("Teachers Unions")> TeachersUnions = 05006000
			''' <summary>University</summary>
			<FieldDisplayName("University")> University = 05007000
			''' <summary>Alternative Energy</summary>
			<FieldDisplayName("Alternative Energy")> AlternativeEnergy = 06001000
			''' <summary>Conservation</summary>
			<FieldDisplayName("Conservation")> Conservation = 06002000
			''' <summary>Energy Savings</summary>
			<FieldDisplayName("Energy Savings")> EnergySavings = 06003000
			''' <summary>Environmental Politics</summary>
			<FieldDisplayName("Environmental Politics")> EnvironmentalPolitics = 06004000
			''' <summary>Environmental pollution</summary>
			<FieldDisplayName("Environmental pollution")> EnvironmentalPollution = 06005000
			''' <summary>Natural resources</summary>
			<FieldDisplayName("Natural resources")> NaturalResources = 06006000
			''' <summary>Nature</summary>
			<FieldDisplayName("Nature")> Nature = 06007000
			''' <summary>Population</summary>
			<FieldDisplayName("Population")> Population = 06008000
			''' <summary>Waste</summary>
			<FieldDisplayName("Waste")> Waste = 06009000
			''' <summary>Water Supplies</summary>
			<FieldDisplayName("Water Supplies")> WaterSupplies = 06010000
			''' <summary>Diseases</summary>
			<FieldDisplayName("Diseases")> Diseases = 07001000
			''' <summary>Epidemic &amp; Plague</summary>
			<FieldDisplayName("Epidemic & Plague")> EpidemicAndPlague = 07002000
			''' <summary>Health treatment</summary>
			<FieldDisplayName("Health treatment")> HealthTreatment = 07003000
			''' <summary>Health organisations</summary>
			<FieldDisplayName("Health organisations")> HealthOrganisations = 07004000
			''' <summary>Medical research</summary>
			<FieldDisplayName("Medical research")> MedicalResearch = 07005000
			''' <summary>Medical staff</summary>
			<FieldDisplayName("Medical staff")> MedicalStaff = 07006000
			''' <summary>Medicines</summary>
			<FieldDisplayName("Medicines")> Medicines = 07007000
			''' <summary>Preventative medicine</summary>
			<FieldDisplayName("Preventative medicine")> PreventativeMedicine = 07008000
			''' <summary>Animals</summary>
			<FieldDisplayName("Animals")> Animals = 08001000
			''' <summary>Curiosities</summary>
			<FieldDisplayName("Curiosities")> Curiosities = 08002000
			''' <summary>People</summary>
			<FieldDisplayName("People")> People = 08003000
			''' <summary>Apprentices</summary>
			<FieldDisplayName("Apprentices")> Apprentices = 09001000
			''' <summary>Collective contracts</summary>
			<FieldDisplayName("Collective contracts")> CollectiveContracts = 09002000
			''' <summary>Employment</summary>
			<FieldDisplayName("Employment")> Employment = 09003000
			''' <summary>Labour dispute</summary>
			<FieldDisplayName("Labour dispute")> LabourDispute = 09004000
			''' <summary>Labour legislation</summary>
			<FieldDisplayName("Labour legislation")> LabourLegislation = 09005000
			''' <summary>Retirement</summary>
			<FieldDisplayName("Retirement")> Retirement = 09006000
			''' <summary>Retraining</summary>
			<FieldDisplayName("Retraining")> Retraining = 09007000
			''' <summary>Strike</summary>
			<FieldDisplayName("Strike")> Strike = 09008000
			''' <summary>Unemployment</summary>
			<FieldDisplayName("Unemployment")> Unemployment = 09009000
			''' <summary>Unions</summary>
			<FieldDisplayName("Unions")> Unions = 09010000
			''' <summary>Wages &amp; Pensions</summary>
			<FieldDisplayName("Wages & Pensions")> WagesAndPensions = 09011000
			''' <summary>Work Relations</summary>
			<FieldDisplayName("Work Relations")> WorkRelations = 09012000
			''' <summary>Games</summary>
			<FieldDisplayName("Games")> Games = 10001000
			''' <summary>Gaming &amp; Lotteries</summary>
			<FieldDisplayName("Gaming & Lotteries")> GamingAndLotteries = 10002000
			''' <summary>Gastronomy</summary>
			<FieldDisplayName("Gastronomy")> Gastronomy = 10003000
			''' <summary>Hobbies</summary>
			<FieldDisplayName("Hobbies")> Hobbies = 10004000
			''' <summary>Holidays or vacations</summary>
			<FieldDisplayName("Holidays or vacations")> HolidaysOrVacations = 10005000
			''' <summary>Tourism</summary>
			<FieldDisplayName("Tourism")> Tourism = 10006000
			''' <summary>Defence</summary>
			<FieldDisplayName("Defence")> Defence = 11001000
			''' <summary>Diplomacy</summary>
			<FieldDisplayName("Diplomacy")> Diplomacy = 11002000
			''' <summary>Elections</summary>
			<FieldDisplayName("Elections")> Elections = 11003000
			''' <summary>Espionage &amp; Intelligence</summary>
			<FieldDisplayName("Espionage & Intelligence")> EspionageAndIntelligence = 11004000
			''' <summary>Foreign Aid</summary>
			<FieldDisplayName("Foreign Aid")> ForeignAid = 11005000
			''' <summary>Government</summary>
			<FieldDisplayName("Government")> Government = 11006000
			''' <summary>Human Rights</summary>
			<FieldDisplayName("Human Rights")> HumanRights = 11007000
			''' <summary>Local authorities</summary>
			<FieldDisplayName("Local authorities")> LocalAuthorities = 11008000
			''' <summary>Parliament</summary>
			<FieldDisplayName("Parliament")> Parliament = 11009000
			''' <summary>Parties</summary>
			<FieldDisplayName("Parties")> Parties = 11010000
			''' <summary>Refugees</summary>
			<FieldDisplayName("Refugees")> Refugees = 11011000
			''' <summary>Regional authorities</summary>
			<FieldDisplayName("Regional authorities")> RegionalAuthorities = 11012000
			''' <summary>State Budget</summary>
			<FieldDisplayName("State Budget")> StateBudget = 11013000
			''' <summary>Treaties &amp; Organisations</summary>
			<FieldDisplayName("Treaties & Organisations")> TreatiesAndOrganisations = 11014000
			''' <summary>Cults &amp; sects</summary>
			<FieldDisplayName("Cults & sects")> CultsAndSects = 12001000
			''' <summary>Faith</summary>
			<FieldDisplayName("Faith")> Faith = 12002000
			''' <summary>Free masonry</summary>
			<FieldDisplayName("Free masonry")> FreeMasonry = 12003000
			''' <summary>Religious institutions</summary>
			<FieldDisplayName("Religious institutions")> ReligiousInstitutions = 12004000
			''' <summary>Applied Sciences</summary>
			<FieldDisplayName("Applied Sciences")> AppliedSciences = 13001000
			''' <summary>Engineering</summary>
			<FieldDisplayName("Engineering")> Engineering = 13002000
			''' <summary>Human Sciences</summary>
			<FieldDisplayName("Human Sciences")> HumanSciences = 13003000
			''' <summary>Natural Sciences</summary>
			<FieldDisplayName("Natural Sciences")> NaturalSciences = 13004000
			''' <summary>Philosophical Sciences</summary>
			<FieldDisplayName("Philosophical Sciences")> PhilosophicalSciences = 13005000
			''' <summary>Research</summary>
			<FieldDisplayName("Research")> Research = 13006000
			''' <summary>Scientific exploration</summary>
			<FieldDisplayName("Scientific exploration")> ScientificExploration = 13007000
			''' <summary>Space programmes</summary>
			<FieldDisplayName("Space programmes")> SpaceProgrammes = 13008000
			''' <summary>Addiction</summary>
			<FieldDisplayName("Addiction")> Addiction = 14001000
			''' <summary>Charity</summary>
			<FieldDisplayName("Charity")> Charity = 14002000
			''' <summary>Demographics</summary>
			<FieldDisplayName("Demographics")> Demographics = 14003000
			''' <summary>Disabled</summary>
			<FieldDisplayName("Disabled")> Disabled = 14004000
			''' <summary>Euthanasia</summary>
			<FieldDisplayName("Euthanasia")> Euthanasia = 14005000
			''' <summary>Family</summary>
			<FieldDisplayName("Family")> Family = 14006000
			''' <summary>Family planning</summary>
			<FieldDisplayName("Family planning")> FamilyPlanning = 14007000
			''' <summary>Health insurance</summary>
			<FieldDisplayName("Health insurance")> HealthInsurance = 14008000
			''' <summary>Homelessness</summary>
			<FieldDisplayName("Homelessness")> Homelessness = 14009000
			''' <summary>Minority groups</summary>
			<FieldDisplayName("Minority groups")> MinorityGroups = 14010000
			''' <summary>Pornography</summary>
			<FieldDisplayName("Pornography")> Pornography = 14011000
			''' <summary>Poverty</summary>
			<FieldDisplayName("Poverty")> Poverty = 14012000
			''' <summary>Prostitution</summary>
			<FieldDisplayName("Prostitution")> Prostitution = 14013000
			''' <summary>Racism</summary>
			<FieldDisplayName("Racism")> Racism = 14014000
			''' <summary>Welfare</summary>
			<FieldDisplayName("Welfare")> Welfare = 14015000
			''' <summary>Aero and Aviation Sports</summary>
			<FieldDisplayName("Aero and Aviation Sports")> Aero = 15001000
			''' <summary>Alpine Skiing</summary>
			<FieldDisplayName("Alpine Skiing")> AlpineSkiing = 15002000
			''' <summary>American Football</summary>
			<FieldDisplayName("American Football")> AmericanFootball = 15003000
			''' <summary>Archery</summary>
			<FieldDisplayName("Archery")> Archery = 15004000
			''' <summary>Athletics, Track &amp; Field</summary>
			<FieldDisplayName("Athletics, Track & Field")> AthleticsTrackAndField = 15005000
			''' <summary>Badminton</summary>
			<FieldDisplayName("Badminton")> Badminton = 15006000
			''' <summary>Baseball</summary>
			<FieldDisplayName("Baseball")> Baseball = 15007000
			''' <summary>Basketball</summary>
			<FieldDisplayName("Basketball")> Basketball = 15008000
			''' <summary>Biathlon</summary>
			<FieldDisplayName("Biathlon")> Biathlon = 15009000
			''' <summary>Billiards, Snooker and Pool</summary>
			<FieldDisplayName("Billiards, Snooker and Pool")> BilliardsSnookerPool = 15010000
			''' <summary>Bobsleigh</summary>
			<FieldDisplayName("Bobsleigh")> Bobsleigh = 15011000
			''' <summary>Bowling</summary>
			<FieldDisplayName("Bowling")> Bowling = 15012000
			''' <summary>Bowls &amp; Petanque</summary>
			<FieldDisplayName("Bowls & Petanque")> BowlsAndPetanque = 15013000
			''' <summary>Boxing</summary>
			<FieldDisplayName("Boxing")> Boxing = 15014000
			''' <summary>Canoeing &amp; Kayaking</summary>
			<FieldDisplayName("Canoeing & Kayaking")> CanoeingAndKayaking = 15015000
			''' <summary>Climbing</summary>
			<FieldDisplayName("Climbing")> Climbing = 15016000
			''' <summary>Cricket</summary>
			<FieldDisplayName("Cricket")> Cricket = 15017000
			''' <summary>Curling</summary>
			<FieldDisplayName("Curling")> Curling = 15018000
			''' <summary>Cycling</summary>
			<FieldDisplayName("Cycling")> Cycling = 15019000
			''' <summary>Dancing</summary>
			<FieldDisplayName("Dancing")> Dancing = 15020000
			''' <summary>Diving</summary>
			<FieldDisplayName("Diving")> Diving = 15021000
			''' <summary>Equestrian</summary>
			<FieldDisplayName("Equestrian")> Equestrian = 15022000
			''' <summary>Fencing</summary>
			<FieldDisplayName("Fencing")> Fencing = 15023000
			''' <summary>Field Hockey</summary>
			<FieldDisplayName("Field Hockey")> FieldHockey = 15024000
			''' <summary>Figure Skating</summary>
			<FieldDisplayName("Figure Skating")> FigureSkating = 15025000
			''' <summary>Freestyle Skiing</summary>
			<FieldDisplayName("Freestyle Skiing")> FreestyleSkiing = 15026000
			''' <summary>Golf</summary>
			<FieldDisplayName("Golf")> Golf = 15027000
			''' <summary>Gymnastics</summary>
			<FieldDisplayName("Gymnastics")> Gymnastics = 15028000
			''' <summary>Handball (Team)</summary>
			<FieldDisplayName("Handball")> Handball = 15029000
			''' <summary>Horse Racing, Harness Racing</summary>
			<FieldDisplayName("Horse Racing, Harness Racing")> Horse = 15030000
			''' <summary>Ice Hockey</summary>
			<FieldDisplayName("Ice Hockey")> IceHockey = 15031000
			''' <summary>Jai Alai (Pelota)</summary>
			<FieldDisplayName("Jai Alai (Pelota)")> JaiAlai = 15032000
			''' <summary>Judo</summary>
			<FieldDisplayName("Judo")> Judo = 15033000
			''' <summary>Karate</summary>
			<FieldDisplayName("Karate")> Karate = 15034000
			''' <summary>Lacrosse</summary>
			<FieldDisplayName("Lacrosse")> Lacrosse = 15035000
			''' <summary>Luge</summary>
			<FieldDisplayName("Luge")> Luge = 15036000
			''' <summary>Marathon</summary>
			<FieldDisplayName("Marathon")> Marathon = 15037000
			''' <summary>Modern Pentathlon</summary>
			<FieldDisplayName("Modern Pentathlon")> ModernPentathlon = 15038000
			''' <summary>Motor Racing</summary>
			<FieldDisplayName("Motor Racing")> MotorRacing = 15039000
			''' <summary>Motor Rallying</summary>
			<FieldDisplayName("Motor Rallying")> MotorRallying = 15040000
			''' <summary>Motorcycling</summary>
			<FieldDisplayName("Motorcycling")> Motorcycling = 15041000
			''' <summary>Netball</summary>
			<FieldDisplayName("Netball")> Netball = 15042000
			''' <summary>Nordic Skiing</summary>
			<FieldDisplayName("Nordic Skiing")> NordicSkiing = 15043000
			''' <summary>Orienteering</summary>
			<FieldDisplayName("Orienteering")> Orienteering = 15044000
			''' <summary>Polo</summary>
			<FieldDisplayName("Polo")> Polo = 15045000
			''' <summary>Power Boating</summary>
			<FieldDisplayName("Power Boating")> PowerBoating = 15046000
			''' <summary>Rowing</summary>
			<FieldDisplayName("Rowing")> Rowing = 15047000
			''' <summary>Rugby League</summary>
			<FieldDisplayName("Rugby League")> RugbyLeague = 15048000
			''' <summary>Rugby Union</summary>
			<FieldDisplayName("Rugby Union")> RugbyUnion = 15049000
			''' <summary>Sailing</summary>
			<FieldDisplayName("Sailing")> Sailing = 15050000
			''' <summary>Shooting</summary>
			<FieldDisplayName("Shooting")> Shooting = 15051000
			''' <summary>Ski Jumping</summary>
			<FieldDisplayName("Ski Jumping")> SkiJumping = 15052000
			''' <summary>Snow Boarding</summary>
			<FieldDisplayName("Snow Boarding")> SnowBoarding = 15053000
			''' <summary>Soccer</summary>
			<FieldDisplayName("Soccer")> Soccer = 15054000
			''' <summary>Softball</summary>
			<FieldDisplayName("Softball")> Softball = 15055000
			''' <summary>Speed Skating</summary>
			<FieldDisplayName("Speed Skating")> SpeedSkating = 15056000
			''' <summary>Speedway</summary>
			<FieldDisplayName("Speedway")> Speedway = 15057000
			''' <summary>Sports Organisations</summary>
			<FieldDisplayName("Sports Organisations")> SportsOrganisations = 15058000
			''' <summary>Squash</summary>
			<FieldDisplayName("Squash")> Squash = 15059000
			''' <summary>Sumo Wrestling</summary>
			<FieldDisplayName("Sumo Wrestling")> SumoWrestling = 15060000
			''' <summary>Surfing</summary>
			<FieldDisplayName("Surfing")> Surfing = 15061000
			''' <summary>Swimming</summary>
			<FieldDisplayName("Swimming")> Swimming = 15062000
			''' <summary>Table Tennis</summary>
			<FieldDisplayName("Table Tennis")> TableTennis = 15063000
			''' <summary>Taekwon-Do</summary>
			<FieldDisplayName("Taekwon-Do")> TaekwonDo = 15064000
			''' <summary>Tennis</summary>
			<FieldDisplayName("Tennis")> Tennis = 15065000
			''' <summary>Triathlon</summary>
			<FieldDisplayName("Triathlon")> Triathlon = 15066000
			''' <summary>Volleyball</summary>
			<FieldDisplayName("Volleyball")> Volleyball = 15067000
			''' <summary>Water Polo</summary>
			<FieldDisplayName("Water Polo")> WaterPolo = 15068000
			''' <summary>Water Skiing</summary>
			<FieldDisplayName("Water Skiing")> WaterSkiing = 15069000
			''' <summary>Weightlifting</summary>
			<FieldDisplayName("Weightlifting")> Weightlifting = 15070000
			''' <summary>Windsurfing</summary>
			<FieldDisplayName("Windsurfing")> Windsurfing = 15071000
			''' <summary>Wrestling</summary>
			<FieldDisplayName("Wrestling")> Wrestling = 15072000
			''' <summary>Acts of terror</summary>
			<FieldDisplayName("Acts of terror")> ActsOfTerror = 16001000
			''' <summary>Armed conflict</summary>
			<FieldDisplayName("Armed conflict")> ArmedConflict = 16002000
			''' <summary>Civil unrest</summary>
			<FieldDisplayName("Civil unrest")> CivilUnrest = 16003000
			''' <summary>Coup d'Etat</summary>
			<FieldDisplayName("Coup d'Etat")> CoupDEtat = 16004000
			''' <summary>Guerrilla activities</summary>
			<FieldDisplayName("Guerrilla activities")> GuerrillaActivities = 16005000
			''' <summary>Massacre</summary>
			<FieldDisplayName("Massacre")> Massacre = 16006000
			''' <summary>Riots</summary>
			<FieldDisplayName("Riots")> Riots = 16007000
			''' <summary>Violent demonstrations</summary>
			<FieldDisplayName("Violent demonstrations")> ViolentDemonstrations = 16008000
			''' <summary>War</summary>
			<FieldDisplayName("War")> War = 16009000
			''' <summary>Forecasts</summary>
			<FieldDisplayName("Forecasts")> Forecasts = 17001000
			''' <summary>Global change</summary>
			<FieldDisplayName("Global change")> GlobalChange = 17002000
			''' <summary>Reports</summary>
			<FieldDisplayName("Reports")> Reports = 17003000
			''' <summary>Statistics</summary>
			<FieldDisplayName("Statistics")> Statistics = 17004000
			''' <summary>Warnings</summary>
			<FieldDisplayName("Warnings")> Warnings = 17005000
		End Enum
		''' <summary>Subject Reference Number and Subject Name relationship (version IPTC/1)</summary>
		<Restrict(True)> Public Enum SubjectReferenceNumbers As Integer
			''' <summary>Matters pertaining to the advancement and refinement of the human mind, of interests, skills, tastes and emotions</summary>
			<FieldDisplayName("Arts, Culture & Entertainment")> ArtsCultureEntertainment = 01000000
			''' <summary>Establishment and/or statement of the rules of behaviour in society, the enforcement of these rules, breaches of the rules and the punishment of offenders. Organisations and bodies involved in these activities.</summary>
			<FieldDisplayName("Crime, Law & Justice")> CrimeLawJustice = 02000000
			''' <summary>Man made and natural events resulting in loss of life or injury to living creatures and/or damage to inanimate objects or property.</summary>
			<FieldDisplayName("Disasters & Accidents")> DisastersAccidents = 03000000
			''' <summary>All matters concerning the planning, production and exchange of wealth.</summary>
			<FieldDisplayName("Economy, Business & Finance")> EconomyBusinessFinance = 04000000
			''' <summary>All aspects of furthering knowledge of human individuals from birth to death.</summary>
			<FieldDisplayName("Education")> Education = 05000000
			''' <summary>All aspects of protection, damage, and condition of the ecosystem of the planet earth and its surroundings.</summary>
			<FieldDisplayName("Environmental Issues")> EnvironmentalIssues = 06000000
			''' <summary>All aspects pertaining to the physical and mental welfare of human beings.</summary>
			<FieldDisplayName("Health")> Health = 07000000
			''' <summary>Lighter items about individuals, groups, animals or objects.</summary>
			<FieldDisplayName("Human Interest")> HumanInterest = 08000000
			''' <summary>Social aspects, organisations, rules and conditions affecting the employment of human effort for the generation of wealth or provision of services and the economic support of the unemployed.</summary>
			<FieldDisplayName("Labour")> Labour = 09000000
			''' <summary>Activities undertaken for pleasure, relaxation or recreation outside paid employment, including eating and travel.</summary>
			<FieldDisplayName("Lifestyle & Leisure")> LifestyleAndLeisure = 10000000
			''' <summary>Local, regional, national and international exercise of power, or struggle for power, and the relationships between governing bodies and states.</summary>
			<FieldDisplayName("Politics")> Politics = 11000000
			''' <summary>All aspects of human existence involving theology, philosophy, ethics and spirituality.</summary>
			<FieldDisplayName("Religion & Belief")> ReligionBelief = 12000000
			''' <summary>All aspects pertaining to human understanding of nature and the physical world and the development and application of this knowledge</summary>
			<FieldDisplayName("Science & Technology")> ScienceTechnology = 13000000
			''' <summary>Aspects of the behaviour of humans affecting the quality of life.</summary>
			<FieldDisplayName("Social Issues")> SocialIssues = 14000000
			''' <summary>Competitive exercise involving physical effort. Organisations and bodies involved in these activities.</summary>
			<FieldDisplayName("Sport")> Sport = 15000000
			''' <summary>Acts of socially or politically motivated protest and/or violence.</summary>
			<FieldDisplayName("Unrest, Conflicts & War")> UnrestConflictsWar = 16000000
			''' <summary>The study, reporting and predic meteorological phenomena.</summary>
			<FieldDisplayName("Weather")> Weather = 17000000
		End Enum
#End Region
#Region "String enums"
		''' <summary>The exact type of audio contained in the current objectdata.</summary>
		<Restrict(True)> Public Enum AudioDataType
			''' <summary>Actuality</summary>
			<FieldDisplayName("Actuality")> <XmlEnum("A")> Actuality
			''' <summary>Question and answer session</summary>
			<FieldDisplayName("Question and answer session")> <XmlEnum("C")> QuestionAndAnswer
			''' <summary>Music, transmitted by itself</summary>
			<FieldDisplayName("Music")> <XmlEnum("M")> Music
			''' <summary>Response to a question</summary>
			<FieldDisplayName("Response to a question")> <XmlEnum("Q")> Response
			''' <summary>Raw sound</summary>
			<FieldDisplayName("Raw sound")> <XmlEnum("R")> RawSound
			''' <summary>Scener</summary>
			<FieldDisplayName("Scener")> <XmlEnum("S")> Scener
			''' <summary>Text only</summary>
			<FieldDisplayName("Text only")> <XmlEnum("T")> TextOnly
			''' <summary>Voicer</summary>
			<FieldDisplayName("Voicer")> <XmlEnum("V")> Voicer
			''' <summary>Wrap</summary>
			<FieldDisplayName("Wrap")> <XmlEnum("W")> Wrap
		End Enum
		''' <summary>Exact content of the current objectdata in terms of colour composition.</summary>
		<Restrict(True)> Public Enum ImageTypeContents
			''' <summary>Monochrome</summary>
			<FieldDisplayName("Monochrome")> <XmlEnum("W")> Monochrome
			''' <summary>Yellow component</summary>
			<FieldDisplayName("Yellow")> <XmlEnum("Y")> Yellow
			''' <summary>Magenta component</summary>
			<FieldDisplayName("Magenta")> <XmlEnum("M")> Magenta
			''' <summary>Cyan component</summary>
			<FieldDisplayName("Cyan")> <XmlEnum("C")> Cyan
			''' <summary>Black component</summary>
			<FieldDisplayName("Black")> <XmlEnum("K")> Black
			''' <summary>Red component</summary>
			<FieldDisplayName("Red")> <XmlEnum("R")> Red
			''' <summary>Green component</summary>
			<FieldDisplayName("Green")> <XmlEnum("G")> Green
			''' <summary>Blue component</summary>
			<FieldDisplayName("Blue")> <XmlEnum("B")> Blue
			''' <summary>Text only</summary>
			<FieldDisplayName("text only")> <XmlEnum("T")> Text
			''' <summary>Full colour composite, frame sequential</summary>
			<FieldDisplayName("Frame Sequential")> <XmlEnum("F")> FrameSequential
			''' <summary>Full colour composite, line sequential</summary>
			<FieldDisplayName("Line Sequential")> <XmlEnum("L")> LineSequential
			''' <summary>Full colour composite, pixel sequential</summary>
			<FieldDisplayName("Pixel Sequential")> <XmlEnum("P")> PixesSequential
			''' <summary>Full colour composite, special sequential</summary>
			<FieldDisplayName("Special Sequential")> <XmlEnum("S")> SpecialSequential
		End Enum
		''' <summary>Information Providers Reference</summary>
		<Restrict(False)> Public Enum InformationProviders
			''' <summary>Agence France Presse</summary>
			<FieldDisplayName("AFP")> <XmlEnum("AFP")> AFP
			''' <summary>Associated Press</summary>
			<FieldDisplayName("AP")> <XmlEnum("AP")> AP
			''' <summary>Associated Press</summary>
			<FieldDisplayName("APD")> <XmlEnum("APD")> APD
			''' <summary>Associated Press</summary>
			<FieldDisplayName("APE")> <XmlEnum("APE")> APE
			''' <summary>Associated Press</summary>
			<FieldDisplayName("APF")> <XmlEnum("APF")> APF
			''' <summary>Associated Press</summary>
			<FieldDisplayName("APS")> <XmlEnum("APS")> APS
			''' <summary>Canadian Press</summary>
			<FieldDisplayName("BN")> <XmlEnum("BN")> BN
			''' <summary>Canadian Press</summary>
			<FieldDisplayName("CP")> <XmlEnum("CP")> CP
			''' <summary>Czech News Agency</summary>
			<FieldDisplayName("CTK")> <XmlEnum("CTK")> CTK
			''' <summary>Deutsche Presse-Agentur GmbH</summary>
			<FieldDisplayName("dpa")> <XmlEnum("dpa")> dpa
			''' <summary>Croatian News Agency</summary>
			<FieldDisplayName("HNA")> <XmlEnum("HNA")> HNA
			''' <summary>International Press Telecommunications Council</summary>
			<FieldDisplayName("IPTC")> <XmlEnum("IPTC")> IPTC
			''' <summary>Magyar Távirati Iroda / Hungarian News Agency</summary>
			<FieldDisplayName("MTI")> <XmlEnum("MTI")> MTI
			''' <summary>Canadian Press</summary>
			<FieldDisplayName("PC")> <XmlEnum("PC")> PC
			''' <summary>Canadian Press</summary>
			<FieldDisplayName("PN")> <XmlEnum("PN")> PN
			''' <summary>Reuters</summary>
			<FieldDisplayName("REUTERS")> <XmlEnum("REUTERS")> REUTERS
			''' <summary>Slovenska Tiskovna Agencija</summary>
			<FieldDisplayName("STA")> <XmlEnum("STA")> STA
			''' <summary>Tidningarnas Telegrambyrå</summary>
			<FieldDisplayName("TT")> <XmlEnum("TT")> TT
			''' <summary>United Press International</summary>
			<FieldDisplayName("UP")> <XmlEnum("UP")> UP
			''' <summary>United Press International</summary>
			<FieldDisplayName("UPI")> <XmlEnum("UPI")> UPI
		End Enum
		''' <summary>ISO 3166-1 alpha-3 codes used by <see cref="ContentLocationCode"/> with addition of some spacial codes used there.</summary>
		''' <remarks>Reserved code elements are codes which, while not ISO 3166-1 codes, are in use for some applications in conjunction with the ISO 3166 codes. The ISO 3166/MA therefore reserves them, so that they are not used for new official ISO 3166 codes, thereby creating conflicts between the standard and those applications.</remarks>
		<Restrict(False)> Public Enum ISO3166
			''' <summary>Aruba</summary>
			<FieldDisplayName("Aruba")> <XmlEnum("ABW")> Aruba
			''' <summary>Afghanistan</summary>
			<FieldDisplayName("Afghanistan")> <XmlEnum("AFG")> Afghanistan
			''' <summary>Angola</summary>
			<FieldDisplayName("Angola")> <XmlEnum("AGO")> Angola
			''' <summary>Anguilla</summary>
			<FieldDisplayName("Anguilla")> <XmlEnum("AIA")> Anguilla
			''' <summary>Åland Islands</summary>
			<FieldDisplayName("Åland")> <XmlEnum("ALA")> Åland
			''' <summary>Albania</summary>
			<FieldDisplayName("Albania")> <XmlEnum("ALB")> Albania
			''' <summary>Andorra</summary>
			<FieldDisplayName("Andorra")> <XmlEnum("AND")> Andorra
			''' <summary>Netherlands Antilles</summary>
			<FieldDisplayName("Netherlands Antilles")> <XmlEnum("ANT")> NetherlandsAntilles
			''' <summary>United Arab Emirates</summary>
			<FieldDisplayName("United Arab Emirates")> <XmlEnum("ARE")> UnitedArabEmirates
			''' <summary>Argentina</summary>
			<FieldDisplayName("Argentina")> <XmlEnum("ARG")> Argentina
			''' <summary>Armenia</summary>
			<FieldDisplayName("Armenia")> <XmlEnum("ARM")> Armenia
			''' <summary>American Samoa</summary>
			<FieldDisplayName("American Samoa")> <XmlEnum("ASM")> AmericanSamoa
			''' <summary>Antarctica</summary>
			<FieldDisplayName("Antarctica")> <XmlEnum("ATA")> Antarctica
			''' <summary>French Southern Territories</summary>
			<FieldDisplayName("French Southern Territories")> <XmlEnum("ATF")> FrenchSouthernTerritories
			''' <summary>Antigua and Barbuda</summary>
			<FieldDisplayName("Antigua and Barbuda")> <XmlEnum("ATG")> AntiguaAndBarbuda
			''' <summary>Australia</summary>
			<FieldDisplayName("Australia")> <XmlEnum("AUS")> Australia
			''' <summary>Austria</summary>
			<FieldDisplayName("Austria")> <XmlEnum("AUT")> Austria
			''' <summary>Azerbaijan</summary>
			<FieldDisplayName("Azerbaijan")> <XmlEnum("AZE")> Azerbaijan
			''' <summary>Burundi</summary>
			<FieldDisplayName("Burundi")> <XmlEnum("BDI")> Burundi
			''' <summary>Belgium</summary>
			<FieldDisplayName("Belgium")> <XmlEnum("BEL")> Belgium
			''' <summary>Benin</summary>
			<FieldDisplayName("Benin")> <XmlEnum("BEN")> Benin
			''' <summary>Burkina Faso</summary>
			<FieldDisplayName("Burkina Faso")> <XmlEnum("BFA")> BurkinaFaso
			''' <summary>Bangladesh</summary>
			<FieldDisplayName("Bangladesh")> <XmlEnum("BGD")> Bangladesh
			''' <summary>Bulgaria</summary>
			<FieldDisplayName("Bulgaria")> <XmlEnum("BGR")> Bulgaria
			''' <summary>Bahrain</summary>
			<FieldDisplayName("Bahrain")> <XmlEnum("BHR")> Bahrain
			''' <summary>Bahamas</summary>
			<FieldDisplayName("Bahamas")> <XmlEnum("BHS")> Bahamas
			''' <summary>Bosnia and Herzegovina</summary>
			<FieldDisplayName("Bosnia and Herzegovina")> <XmlEnum("BIH")> BosniaAndHerzegovina
			''' <summary>Belarus</summary>
			<FieldDisplayName("Belarus")> <XmlEnum("BLR")> Belarus
			''' <summary>Belize</summary>
			<FieldDisplayName("Belize")> <XmlEnum("BLZ")> Belize
			''' <summary>Bermuda</summary>
			<FieldDisplayName("Bermuda")> <XmlEnum("BMU")> Bermuda
			''' <summary>Bolivia</summary>
			<FieldDisplayName("Bolivia")> <XmlEnum("BOL")> Bolivia
			''' <summary>Brazil</summary>
			<FieldDisplayName("Brazil")> <XmlEnum("BRA")> Brazil
			''' <summary>Barbados</summary>
			<FieldDisplayName("Barbados")> <XmlEnum("BRB")> Barbados
			''' <summary>Brunei Darussalam</summary>
			<FieldDisplayName("Brunei")> <XmlEnum("BRN")> Brunei
			''' <summary>Bhutan</summary>
			<FieldDisplayName("Bhutan")> <XmlEnum("BTN")> Bhutan
			''' <summary>Bouvet Island</summary>
			<FieldDisplayName("Bouvet Island")> <XmlEnum("BVT")> BouvetIsland
			''' <summary>Botswana</summary>
			<FieldDisplayName("Botswana")> <XmlEnum("BWA")> Botswana
			''' <summary>Central African Republic</summary>
			<FieldDisplayName("Central African Republic")> <XmlEnum("CAF")> CentralAfricanRepublic
			''' <summary>Canada</summary>
			<FieldDisplayName("Canada")> <XmlEnum("CAN")> Canada
			''' <summary>Cocos (Keeling) Islands</summary>
			<FieldDisplayName("Cocos Islands")> <XmlEnum("CCK")> CocosIslands
			''' <summary>Switzerland</summary>
			<FieldDisplayName("Switzerland")> <XmlEnum("CHE")> Switzerland
			''' <summary>Chile</summary>
			<FieldDisplayName("Chile")> <XmlEnum("CHL")> Chile
			''' <summary>China</summary>
			<FieldDisplayName("China")> <XmlEnum("CHN")> China
			''' <summary>Côte d'Ivoire</summary>
			<FieldDisplayName("Côte d'Ivoire")> <XmlEnum("CIV")> CôteDIvoire
			''' <summary>Cameroon</summary>
			<FieldDisplayName("Cameroon")> <XmlEnum("CMR")> Cameroon
			''' <summary>Congo, the Democratic Republic of the[1]</summary>
			<FieldDisplayName("Congo-Zaire")> <XmlEnum("COD")> Zaire
			''' <summary>Congo</summary>
			<FieldDisplayName("Congo")> <XmlEnum("COG")> Congo
			''' <summary>Cook Islands</summary>
			<FieldDisplayName("Cook Islands")> <XmlEnum("COK")> CookIslands
			''' <summary>Colombia</summary>
			<FieldDisplayName("Colombia")> <XmlEnum("COL")> Colombia
			''' <summary>Comoros</summary>
			<FieldDisplayName("Comoros")> <XmlEnum("COM")> Comoros
			''' <summary>Cape Verde</summary>
			<FieldDisplayName("Cape Verde")> <XmlEnum("CPV")> CapeVerde
			''' <summary>Costa Rica</summary>
			<FieldDisplayName("Costa Rica")> <XmlEnum("CRI")> CostaRica
			''' <summary>Cuba</summary>
			<FieldDisplayName("Cuba")> <XmlEnum("CUB")> Cuba
			''' <summary>Christmas Island</summary>
			<FieldDisplayName("Christmas Island")> <XmlEnum("CXR")> ChristmasIsland
			''' <summary>Cayman Islands</summary>
			<FieldDisplayName("Cayman Islands")> <XmlEnum("CYM")> CaymanIslands
			''' <summary>Cyprus</summary>
			<FieldDisplayName("Cyprus")> <XmlEnum("CYP")> Cyprus
			''' <summary>Czech Republic</summary>
			<FieldDisplayName("Czech Republic")> <XmlEnum("CZE")> CzechRepublic
			''' <summary>Germany</summary>
			<FieldDisplayName("Germany")> <XmlEnum("DEU")> Germany
			''' <summary>Djibouti</summary>
			<FieldDisplayName("Djibouti")> <XmlEnum("DJI")> Djibouti
			''' <summary>Dominica</summary>
			<FieldDisplayName("Dominica")> <XmlEnum("DMA")> Dominica
			''' <summary>Denmark</summary>
			<FieldDisplayName("Denmark")> <XmlEnum("DNK")> Denmark
			''' <summary>Dominican Republic</summary>
			<FieldDisplayName("Dominican Republic")> <XmlEnum("DOM")> DominicanRepublic
			''' <summary>Algeria</summary>
			<FieldDisplayName("Algeria")> <XmlEnum("DZA")> Algeria
			''' <summary>Ecuador</summary>
			<FieldDisplayName("Ecuador")> <XmlEnum("ECU")> Ecuador
			''' <summary>Egypt</summary>
			<FieldDisplayName("Egypt")> <XmlEnum("EGY")> Egypt
			''' <summary>Eritrea</summary>
			<FieldDisplayName("Eritrea")> <XmlEnum("ERI")> Eritrea
			''' <summary>Western Sahara</summary>
			<FieldDisplayName("Western Sahara")> <XmlEnum("ESH")> WesternSahara
			''' <summary>Spain</summary>
			<FieldDisplayName("Spain")> <XmlEnum("ESP")> Spain
			''' <summary>Estonia</summary>
			<FieldDisplayName("Estonia")> <XmlEnum("EST")> Estonia
			''' <summary>Ethiopia</summary>
			<FieldDisplayName("Ethiopia")> <XmlEnum("ETH")> Ethiopia
			''' <summary>Finland</summary>
			<FieldDisplayName("Finland")> <XmlEnum("FIN")> Finland
			''' <summary>Fiji</summary>
			<FieldDisplayName("Fiji")> <XmlEnum("FJI")> Fiji
			''' <summary>Falkland Islands (Malvinas)</summary>
			<FieldDisplayName("Falkland Islands (Malvinas)")> <XmlEnum("FLK")> FalklandIslands
			''' <summary>France</summary>
			<FieldDisplayName("France")> <XmlEnum("FRA")> France
			''' <summary>Faroe Islands</summary>
			<FieldDisplayName("Faroe Islands")> <XmlEnum("FRO")> FaroeIslands
			''' <summary>Micronesia, Federated States of</summary>
			<FieldDisplayName("Micronesia")> <XmlEnum("FSM")> Micronesia
			''' <summary>Gabon</summary>
			<FieldDisplayName("Gabon")> <XmlEnum("GAB")> Gabon
			''' <summary>United Kingdom</summary>
			<FieldDisplayName("United Kingdom")> <XmlEnum("GBR")> UnitedKingdom
			''' <summary>Georgia</summary>
			<FieldDisplayName("Georgia")> <XmlEnum("GEO")> Georgia
			''' <summary>Guernsey</summary>
			<FieldDisplayName("Guernsey")> <XmlEnum("GGY")> Guernsey
			''' <summary>Ghana</summary>
			<FieldDisplayName("Ghana")> <XmlEnum("GHA")> Ghana
			''' <summary>Gibraltar</summary>
			<FieldDisplayName("Gibraltar")> <XmlEnum("GIB")> Gibraltar
			''' <summary>Guinea</summary>
			<FieldDisplayName("Guinea")> <XmlEnum("GIN")> Guinea
			''' <summary>Guadeloupe</summary>
			<FieldDisplayName("Guadeloupe")> <XmlEnum("GLP")> Guadeloupe
			''' <summary>Gambia</summary>
			<FieldDisplayName("Gambia")> <XmlEnum("GMB")> Gambia
			''' <summary>Guinea-Bissau</summary>
			<FieldDisplayName("Guinea-Bissau")> <XmlEnum("GNB")> GuineaBissau
			''' <summary>Equatorial Guinea</summary>
			<FieldDisplayName("Equatorial Guinea")> <XmlEnum("GNQ")> EquatorialGuinea
			''' <summary>Greece</summary>
			<FieldDisplayName("Greece")> <XmlEnum("GRC")> Greece
			''' <summary>Grenada</summary>
			<FieldDisplayName("Grenada")> <XmlEnum("GRD")> Grenada
			''' <summary>Greenland</summary>
			<FieldDisplayName("Greenland")> <XmlEnum("GRL")> Greenland
			''' <summary>Guatemala</summary>
			<FieldDisplayName("Guatemala")> <XmlEnum("GTM")> Guatemala
			''' <summary>French Guiana</summary>
			<FieldDisplayName("French Guiana")> <XmlEnum("GUF")> FrenchGuiana
			''' <summary>Guam</summary>
			<FieldDisplayName("Guam")> <XmlEnum("GUM")> Guam
			''' <summary>Guyana</summary>
			<FieldDisplayName("Guyana")> <XmlEnum("GUY")> Guyana
			''' <summary>Hong Kong</summary>
			<FieldDisplayName("Hong Kong")> <XmlEnum("HKG")> HongKong
			''' <summary>Heard Island and McDonald Islands</summary>
			<FieldDisplayName("Heard Island and McDonald Islands")> <XmlEnum("HMD")> HeardIslandAndMcDonaldIslands
			''' <summary>Honduras</summary>
			<FieldDisplayName("Honduras")> <XmlEnum("HND")> Honduras
			''' <summary>Croatia</summary>
			<FieldDisplayName("Croatia")> <XmlEnum("HRV")> Croatia
			''' <summary>Haiti</summary>
			<FieldDisplayName("Haiti")> <XmlEnum("HTI")> Haiti
			''' <summary>Hungary</summary>
			<FieldDisplayName("Hungary")> <XmlEnum("HUN")> Hungary
			''' <summary>Indonesia</summary>
			<FieldDisplayName("Indonesia")> <XmlEnum("IDN")> Indonesia
			''' <summary>Isle of Man</summary>
			<FieldDisplayName("Isle of Man")> <XmlEnum("IMN")> Man
			''' <summary>India</summary>
			<FieldDisplayName("India")> <XmlEnum("IND")> India
			''' <summary>British Indian Ocean Territory</summary>
			<FieldDisplayName("British Indian Ocean Territory")> <XmlEnum("IOT")> BritishIndianOceanTerritory
			''' <summary>Ireland</summary>
			<FieldDisplayName("Ireland")> <XmlEnum("IRL")> Ireland
			''' <summary>Iran, Islamic Republic of</summary>
			<FieldDisplayName("Iran")> <XmlEnum("IRN")> Iran
			''' <summary>Iraq</summary>
			<FieldDisplayName("Iraq")> <XmlEnum("IRQ")> Iraq
			''' <summary>Iceland</summary>
			<FieldDisplayName("Iceland")> <XmlEnum("ISL")> Iceland
			''' <summary>Israel</summary>
			<FieldDisplayName("Israel")> <XmlEnum("ISR")> Israel
			''' <summary>Italy</summary>
			<FieldDisplayName("Italy")> <XmlEnum("ITA")> Italy
			''' <summary>Jamaica</summary>
			<FieldDisplayName("Jamaica")> <XmlEnum("JAM")> Jamaica
			''' <summary>Jersey</summary>
			<FieldDisplayName("Jersey")> <XmlEnum("JEY")> Jersey
			''' <summary>Jordan</summary>
			<FieldDisplayName("Jordan")> <XmlEnum("JOR")> Jordan
			''' <summary>Japan</summary>
			<FieldDisplayName("Japan")> <XmlEnum("JPN")> Japan
			''' <summary>Kazakhstan</summary>
			<FieldDisplayName("Kazakhstan")> <XmlEnum("KAZ")> Kazakhstan
			''' <summary>Kenya</summary>
			<FieldDisplayName("Kenya")> <XmlEnum("KEN")> Kenya
			''' <summary>Kyrgyzstan</summary>
			<FieldDisplayName("Kyrgyzstan")> <XmlEnum("KGZ")> Kyrgyzstan
			''' <summary>Cambodia</summary>
			<FieldDisplayName("Cambodia")> <XmlEnum("KHM")> Cambodia
			''' <summary>Kiribati</summary>
			<FieldDisplayName("Kiribati")> <XmlEnum("KIR")> Kiribati
			''' <summary>Saint Kitts and Nevis</summary>
			<FieldDisplayName("Saint Kitts and Nevis")> <XmlEnum("KNA")> SaintKittsAndNevis
			''' <summary>Korea, Republic of</summary>
			<FieldDisplayName("Korea")> <XmlEnum("KOR")> Korea
			''' <summary>Kuwait</summary>
			<FieldDisplayName("Kuwait")> <XmlEnum("KWT")> Kuwait
			''' <summary>Lao People's Democratic Republic</summary>
			<FieldDisplayName("Laos")> <XmlEnum("LAO")> Laos
			''' <summary>Lebanon</summary>
			<FieldDisplayName("Lebanon")> <XmlEnum("LBN")> Lebanon
			''' <summary>Liberia</summary>
			<FieldDisplayName("Liberia")> <XmlEnum("LBR")> Liberia
			''' <summary>Libyan Arab Jamahiriya</summary>
			<FieldDisplayName("Libya")> <XmlEnum("LBY")> Libya
			''' <summary>Saint Lucia</summary>
			<FieldDisplayName("Saint Lucia")> <XmlEnum("LCA")> SaintLucia
			''' <summary>Liechtenstein</summary>
			<FieldDisplayName("Liechtenstein")> <XmlEnum("LIE")> Liechtenstein
			''' <summary>Sri Lanka</summary>
			<FieldDisplayName("Sri Lanka")> <XmlEnum("LKA")> SriLanka
			''' <summary>Lesotho</summary>
			<FieldDisplayName("Lesotho")> <XmlEnum("LSO")> Lesotho
			''' <summary>Lithuania</summary>
			<FieldDisplayName("Lithuania")> <XmlEnum("LTU")> Lithuania
			''' <summary>Luxembourg</summary>
			<FieldDisplayName("Luxembourg")> <XmlEnum("LUX")> Luxembourg
			''' <summary>Latvia</summary>
			<FieldDisplayName("Latvia")> <XmlEnum("LVA")> Latvia
			''' <summary>Macao</summary>
			<FieldDisplayName("Macao")> <XmlEnum("MAC")> Macao
			''' <summary>Morocco</summary>
			<FieldDisplayName("Morocco")> <XmlEnum("MAR")> Morocco
			''' <summary>Monaco</summary>
			<FieldDisplayName("Monaco")> <XmlEnum("MCO")> Monaco
			''' <summary>Moldova, Republic of</summary>
			<FieldDisplayName("Moldova")> <XmlEnum("MDA")> Moldova
			''' <summary>Madagascar</summary>
			<FieldDisplayName("Madagascar")> <XmlEnum("MDG")> Madagascar
			''' <summary>Maldives</summary>
			<FieldDisplayName("Maldives")> <XmlEnum("MDV")> Maldives
			''' <summary>Mexico</summary>
			<FieldDisplayName("Mexico")> <XmlEnum("MEX")> Mexico
			''' <summary>Marshall Islands</summary>
			<FieldDisplayName("Marshall Islands")> <XmlEnum("MHL")> MarshallIslands
			''' <summary>Macedonia</summary>
			<FieldDisplayName("Macedonia")> <XmlEnum("MKD")> Macedonia
			''' <summary>Mali</summary>
			<FieldDisplayName("Mali")> <XmlEnum("MLI")> Mali
			''' <summary>Malta</summary>
			<FieldDisplayName("Malta")> <XmlEnum("MLT")> Malta
			''' <summary>Myanmar</summary>
			<FieldDisplayName("Myanmar")> <XmlEnum("MMR")> Myanmar
			''' <summary>Montenegro</summary>
			<FieldDisplayName("Montenegro")> <XmlEnum("MNE")> Montenegro
			''' <summary>Mongolia</summary>
			<FieldDisplayName("Mongolia")> <XmlEnum("MNG")> Mongolia
			''' <summary>Northern Mariana Islands</summary>
			<FieldDisplayName("Northern Mariana Islands")> <XmlEnum("MNP")> NorthernMarianaIslands
			''' <summary>Mozambique</summary>
			<FieldDisplayName("Mozambique")> <XmlEnum("MOZ")> Mozambique
			''' <summary>Mauritania</summary>
			<FieldDisplayName("Mauritania")> <XmlEnum("MRT")> Mauritania
			''' <summary>Montserrat</summary>
			<FieldDisplayName("Montserrat")> <XmlEnum("MSR")> Montserrat
			''' <summary>Martinique</summary>
			<FieldDisplayName("Martinique")> <XmlEnum("MTQ")> Martinique
			''' <summary>Mauritius</summary>
			<FieldDisplayName("Mauritius")> <XmlEnum("MUS")> Mauritius
			''' <summary>Malawi</summary>
			<FieldDisplayName("Malawi")> <XmlEnum("MWI")> Malawi
			''' <summary>Malaysia</summary>
			<FieldDisplayName("Malaysia")> <XmlEnum("MYS")> Malaysia
			''' <summary>Mayotte</summary>
			<FieldDisplayName("Mayotte")> <XmlEnum("MYT")> Mayotte
			''' <summary>Namibia</summary>
			<FieldDisplayName("Namibia")> <XmlEnum("NAM")> Namibia
			''' <summary>New Caledonia</summary>
			<FieldDisplayName("New Caledonia")> <XmlEnum("NCL")> NewCaledonia
			''' <summary>Niger</summary>
			<FieldDisplayName("Niger")> <XmlEnum("NER")> Niger
			''' <summary>Norfolk Island</summary>
			<FieldDisplayName("Norfolk")> <XmlEnum("NFK")> Norfolk
			''' <summary>Nigeria</summary>
			<FieldDisplayName("Nigeria")> <XmlEnum("NGA")> Nigeria
			''' <summary>Nicaragua</summary>
			<FieldDisplayName("Nicaragua")> <XmlEnum("NIC")> Nicaragua
			''' <summary>Niue</summary>
			<FieldDisplayName("Niue")> <XmlEnum("NIU")> Niue
			''' <summary>Netherlands</summary>
			<FieldDisplayName("Netherlands")> <XmlEnum("NLD")> Netherlands
			''' <summary>Norway</summary>
			<FieldDisplayName("Norway")> <XmlEnum("NOR")> Norway
			''' <summary>Nepal</summary>
			<FieldDisplayName("Nepal")> <XmlEnum("NPL")> Nepal
			''' <summary>Nauru</summary>
			<FieldDisplayName("Nauru")> <XmlEnum("NRU")> Nauru
			''' <summary>New Zealand</summary>
			<FieldDisplayName("New Zealand")> <XmlEnum("NZL")> NewZealand
			''' <summary>Oman</summary>
			<FieldDisplayName("Oman")> <XmlEnum("OMN")> Oman
			''' <summary>Pakistan</summary>
			<FieldDisplayName("Pakistan")> <XmlEnum("PAK")> Pakistan
			''' <summary>Panama</summary>
			<FieldDisplayName("Panama")> <XmlEnum("PAN")> Panama
			''' <summary>Pitcairn</summary>
			<FieldDisplayName("Pitcairn")> <XmlEnum("PCN")> Pitcairn
			''' <summary>Peru</summary>
			<FieldDisplayName("Peru")> <XmlEnum("PER")> Peru
			''' <summary>Philippines</summary>
			<FieldDisplayName("Philippines")> <XmlEnum("PHL")> Philippines
			''' <summary>Palau</summary>
			<FieldDisplayName("Palau")> <XmlEnum("PLW")> Palau
			''' <summary>Papua New Guinea</summary>
			<FieldDisplayName("Papua New Guinea")> <XmlEnum("PNG")> PapuaNewGuinea
			''' <summary>Poland</summary>
			<FieldDisplayName("Poland")> <XmlEnum("POL")> Poland
			''' <summary>Puerto Rico</summary>
			<FieldDisplayName("Puerto Rico")> <XmlEnum("PRI")> PuertoRico
			''' <summary>Korea, Democratic People's Republic of</summary>
			<FieldDisplayName("North Korea")> <XmlEnum("PRK")> NortKorea
			''' <summary>Portugal</summary>
			<FieldDisplayName("Portugal")> <XmlEnum("PRT")> Portugal
			''' <summary>Paraguay</summary>
			<FieldDisplayName("Paraguay")> <XmlEnum("PRY")> Paraguay
			''' <summary>Palestinian Territory, Occupied</summary>
			<FieldDisplayName("Palestina")> <XmlEnum("PSE")> Palestina
			''' <summary>French Polynesia</summary>
			<FieldDisplayName("French Polynesia")> <XmlEnum("PYF")> FrenchPolynesia
			''' <summary>Qatar</summary>
			<FieldDisplayName("Qatar")> <XmlEnum("QAT")> Qatar
			''' <summary>Réunion</summary>
			<FieldDisplayName("Réunion")> <XmlEnum("REU")> Réunion
			''' <summary>Romania</summary>
			<FieldDisplayName("Romania")> <XmlEnum("ROU")> Romania
			''' <summary>Russian Federation</summary>
			<FieldDisplayName("Russia")> <XmlEnum("RUS")> Russiaa
			''' <summary>Rwanda</summary>
			<FieldDisplayName("Rwanda")> <XmlEnum("RWA")> Rwanda
			''' <summary>Saudi Arabia</summary>
			<FieldDisplayName("Saudi Arabia")> <XmlEnum("SAU")> SaudiArabia
			''' <summary>Sudan</summary>
			<FieldDisplayName("Sudan")> <XmlEnum("SDN")> Sudan
			''' <summary>Senegal</summary>
			<FieldDisplayName("Senegal")> <XmlEnum("SEN")> Senegal
			''' <summary>Singapore</summary>
			<FieldDisplayName("Singapore")> <XmlEnum("SGP")> Singapore
			''' <summary>South Georgia and the South Sandwich Islands</summary>
			<FieldDisplayName("South Georgia and the South Sandwich Islands")> <XmlEnum("SGS")> SouthGeorgiaAndTheSouthSandwichIslands
			''' <summary>Saint Helena</summary>
			<FieldDisplayName("Saint Helena")> <XmlEnum("SHN")> SaintHelena
			''' <summary>Svalbard and Jan Mayen</summary>
			<FieldDisplayName("Svalbard and Jan Mayen")> <XmlEnum("SJM")> SvalbardAndJanMayen
			''' <summary>Solomon Islands</summary>
			<FieldDisplayName("Solomon Islands")> <XmlEnum("SLB")> SolomonIslands
			''' <summary>Sierra Leone</summary>
			<FieldDisplayName("Sierra Leone")> <XmlEnum("SLE")> SierraLeone
			''' <summary>El Salvador</summary>
			<FieldDisplayName("El Salvador")> <XmlEnum("SLV")> Salvador
			''' <summary>San Marino</summary>
			<FieldDisplayName("San Marino")> <XmlEnum("SMR")> SanMarino
			''' <summary>Somalia</summary>
			<FieldDisplayName("Somalia")> <XmlEnum("SOM")> Somalia
			''' <summary>Saint Pierre and Miquelon</summary>
			<FieldDisplayName("Saint Pierre and Miquelon")> <XmlEnum("SPM")> SaintPierreAndMiquelon
			''' <summary>Serbia</summary>
			<FieldDisplayName("Serbia")> <XmlEnum("SRB")> Serbia
			''' <summary>Sao Tome and Principe</summary>
			<FieldDisplayName("Sao Tome and Principe")> <XmlEnum("STP")> SaoTomeAndPrincipe
			''' <summary>Suriname</summary>
			<FieldDisplayName("Suriname")> <XmlEnum("SUR")> Suriname
			''' <summary>Slovakia</summary>
			<FieldDisplayName("Slovakia")> <XmlEnum("SVK")> Slovakia
			''' <summary>Slovenia</summary>
			<FieldDisplayName("Slovenia")> <XmlEnum("SVN")> Slovenia
			''' <summary>Sweden</summary>
			<FieldDisplayName("Sweden")> <XmlEnum("SWE")> Sweden
			''' <summary>Swaziland</summary>
			<FieldDisplayName("Swaziland")> <XmlEnum("SWZ")> Swaziland
			''' <summary>Seychelles</summary>
			<FieldDisplayName("Seychelles")> <XmlEnum("SYC")> Seychelles
			''' <summary>Syrian Arab Republic</summary>
			<FieldDisplayName("Syria")> <XmlEnum("SYR")> Syria
			''' <summary>Turks and Caicos Islands</summary>
			<FieldDisplayName("Turks and Caicos")> <XmlEnum("TCA")> TurksAndCaicos
			''' <summary>Chad</summary>
			<FieldDisplayName("Chad")> <XmlEnum("TCD")> Chad
			''' <summary>Togo</summary>
			<FieldDisplayName("Togo")> <XmlEnum("TGO")> Togo
			''' <summary>Thailand</summary>
			<FieldDisplayName("Thailand")> <XmlEnum("THA")> Thailand
			''' <summary>Tajikistan</summary>
			<FieldDisplayName("Tajikistan")> <XmlEnum("TJK")> Tajikistan
			''' <summary>Tokelau</summary>
			<FieldDisplayName("Tokelau")> <XmlEnum("TKL")> Tokelau
			''' <summary>Turkmenistan</summary>
			<FieldDisplayName("Turkmenistan")> <XmlEnum("TKM")> Turkmenistan
			''' <summary>Timor-Leste</summary>
			<FieldDisplayName("Timor-Leste")> <XmlEnum("TLS")> TimorLeste
			''' <summary>Tonga</summary>
			<FieldDisplayName("Tonga")> <XmlEnum("TON")> Tonga
			''' <summary>Trinidad and Tobago</summary>
			<FieldDisplayName("Trinidad and Tobago")> <XmlEnum("TTO")> TrinidadAndTobago
			''' <summary>Tunisia</summary>
			<FieldDisplayName("Tunisia")> <XmlEnum("TUN")> Tunisia
			''' <summary>Turkey</summary>
			<FieldDisplayName("Turkey")> <XmlEnum("TUR")> Turkey
			''' <summary>Tuvalu</summary>
			<FieldDisplayName("Tuvalu")> <XmlEnum("TUV")> Tuvalu
			''' <summary>Taiwan (ROC)</summary>
			<FieldDisplayName("Taiwan")> <XmlEnum("TWN")> Taiwan
			''' <summary>Tanzania, United Republic of</summary>
			<FieldDisplayName("Tanzania")> <XmlEnum("TZA")> Tanzania
			''' <summary>Uganda</summary>
			<FieldDisplayName("Uganda")> <XmlEnum("UGA")> Uganda
			''' <summary>Ukraine</summary>
			<FieldDisplayName("Ukraine")> <XmlEnum("UKR")> Ukraine
			''' <summary>United States Minor Outlying Islands</summary>
			<FieldDisplayName("United States Minor Outlying Islands")> <XmlEnum("UMI")> UnitedStatesMinorOutlyingIslands
			''' <summary>Uruguay</summary>
			<FieldDisplayName("Uruguay")> <XmlEnum("URY")> Uruguay
			''' <summary>United States</summary>
			<FieldDisplayName("United States")> <XmlEnum("USA")> USA
			''' <summary>Uzbekistan</summary>
			<FieldDisplayName("Uzbekistan")> <XmlEnum("UZB")> Uzbekistan
			''' <summary>Vatican City State (Holy See)</summary>
			<FieldDisplayName("Vatican")> <XmlEnum("VAT")> Vatican
			''' <summary>Saint Vincent and the Grenadines</summary>
			<FieldDisplayName("Saint Vincent and the Grenadines")> <XmlEnum("VCT")> SaintVincentAndTheGrenadines
			''' <summary>Venezuela</summary>
			<FieldDisplayName("Venezuela")> <XmlEnum("VEN")> Venezuela
			''' <summary>Virgin Islands, British</summary>
			<FieldDisplayName("Virgin Islands, British")> <XmlEnum("VGB")> BritishVirginIslands
			''' <summary>Virgin Islands, U.S.</summary>
			<FieldDisplayName("Virgin Islands, U.S.")> <XmlEnum("VIR")> AmericanVirginIslands
			''' <summary>Viet Nam</summary>
			<FieldDisplayName("Viet Nam")> <XmlEnum("VNM")> VietNam
			''' <summary>Vanuatu</summary>
			<FieldDisplayName("Vanuatu")> <XmlEnum("VUT")> Vanuatu
			''' <summary>Wallis and Futuna</summary>
			<FieldDisplayName("Wallis and Futuna")> <XmlEnum("WLF")> WallisAndFutuna
			''' <summary>Samoa</summary>
			<FieldDisplayName("Samoa")> <XmlEnum("WSM")> Samoa
			''' <summary>Yemen</summary>
			<FieldDisplayName("Yemen")> <XmlEnum("YEM")> Yemen
			''' <summary>South Africa</summary>
			<FieldDisplayName("South Africa")> <XmlEnum("ZAF")> SouthAfrica
			''' <summary>Zambia</summary>
			<FieldDisplayName("Zambia")> <XmlEnum("ZMB")> Zambia
			''' <summary>Zimbabwe</summary>
			<FieldDisplayName("Zimbabwe")> <XmlEnum("ZWE")> Zimbabwe
			''' <summary>Ascension Island — Reserved on request of UPU, also used by ITU</summary>
			<FieldDisplayName("Ascension")> <XmlEnum("ASC")> Ascension
			''' <summary>Clipperton Island — Reserved on request of ITU</summary>
			<FieldDisplayName("Clipperton")> <XmlEnum("CPT")> Clipperton
			''' <summary>Diego Garcia — Reserved on request of ITU</summary>
			<FieldDisplayName("Diego Garcia")> <XmlEnum("DGA")> DiegoGarcia
			''' <summary>France, Metropolitan — Reserved on request of France</summary>
			<FieldDisplayName("France, Metropolitan")> <XmlEnum("FXX")> FranceMetropolitan
			''' <summary>Tristan da Cunha — Reserved on request of UPU</summary>
			<FieldDisplayName("Tristan da Cunha")> <XmlEnum("TAA")> TristanDaCunha
			''' <summary>United Nations</summary>
			<FieldDisplayName("United Nations")> <XmlEnum("XUN")> UnitedNations
			''' <summary>European Union (formerly known as the EC and before that the EEC)</summary>
			<FieldDisplayName("European Union")> <XmlEnum("XEU")> EuropeanUnion
			''' <summary>Space</summary>
			<FieldDisplayName("Space")> <XmlEnum("XSP")> Space
			''' <summary>at Sea</summary>
			<FieldDisplayName("at Sea")> <XmlEnum("XSE")> AtSea
			''' <summary>In Flight</summary>
			<FieldDisplayName("In Flight")> <XmlEnum("XIF")> InFlight
			''' <summary>England (where greater granularity than Great Britain is desired)</summary>
			<FieldDisplayName("England")> <XmlEnum("XEN")> England
			''' <summary>- Scotland</summary>
			<FieldDisplayName("- Scotland")> <XmlEnum("XSC")> Scotland
			''' <summary>Northern Ireland</summary>
			<FieldDisplayName("NorthernIreland")> <XmlEnum("XNI")> NorthernIreland
			''' <summary>Wales</summary>
			<FieldDisplayName("Wales")> <XmlEnum("XWA")> Wales
		End Enum
		''' <summary>Values of <see cref="ObjectCycle"/></summary>
		<Restrict(True)> Public Enum ObjectCycleValues
			''' <summary>Morning</summary>
			<FieldDisplayName("morning")> <XmlEnum("a")> Morning
			''' <summary>Evening</summary>
			<FieldDisplayName("evening")> <XmlEnum("p")> Evening
			''' <summary>Both</summary>
			<FieldDisplayName("both")> <XmlEnum("b")> Both
		End Enum
		''' <summary>Possible orientations of image</summary>
		<Restrict(True)> Public Enum Orientations
			''' <summary>Portrait</summary>
			<FieldDisplayName("Portrait")> <XmlEnum("P")> Portrait
			''' <summary>Landscape</summary>
			<FieldDisplayName("Landscape")> <XmlEnum("L")> Landscape
			''' <summary>Square</summary>
			<FieldDisplayName("Square")> <XmlEnum("S")> Square
		End Enum
#End Region
#Region "Tag types"
		''' <summary>Gets details about tag format by tag record and number</summary>
		''' <param name="Record">Recor number</param>
		''' <param name="TagNumber">Number of tag within <paramref name="Record"/></param>
		''' <exception cref="InvalidEnumArgumentException"><paramref name="Record"/> is not member of <see cref="RecordNumbers"/> -or- <paramref name="TagNumber"/> is not tag within <paramref name="record"/></exception>
		Public Shared Function GetTag(ByVal Record As RecordNumbers, TagNumber As Byte) As IPTCTag
			Select Case Record
				Case RecordNumbers.Envelope
					Select Case TagNumber
						Case EnvelopeTags.ModelVersion : Return New IPTCTag(Number:=EnvelopeTags.ModelVersion, Record:=RecordNumbers.Envelope, Name:="ModelVersion", HumanName:="Model Version", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Internal", Description:="A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.", Lock:=True)
						Case EnvelopeTags.Destination : Return New IPTCTag(Number:=EnvelopeTags.Destination, Record:=RecordNumbers.Envelope, Name:="Destination", HumanName:="Destination", Type:=IPTCTypes.GraphicCharacters, Mandatory:=false, Repeatable:=true, Length:=1024, Fixed:=false, Category:="Old IPTC", Description:="This DataSet is to accommodate some providers who require routing information above the appropriate OSI layers.", Lock:=True)
						Case EnvelopeTags.FileFormat : Return New IPTCTag(Number:=EnvelopeTags.FileFormat, Record:=RecordNumbers.Envelope, Name:="FileFormat", HumanName:="File Format", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Old IPTC", Description:="A number representing the file format.", [Enum]:=GetType(FileFormats), Lock:=True)
						Case EnvelopeTags.FileFormatVersion : Return New IPTCTag(Number:=EnvelopeTags.FileFormatVersion, Record:=RecordNumbers.Envelope, Name:="FileFormatVersion", HumanName:="File Format Version", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Old IPTC", Description:="A binary number representing the particular version of the FileFormat", [Enum]:=GetType(FileFormatVersions), Lock:=True)
						Case EnvelopeTags.ServiceIdentifier : Return New IPTCTag(Number:=EnvelopeTags.ServiceIdentifier, Record:=RecordNumbers.Envelope, Name:="ServiceIdentifier", HumanName:="Service Identifier", Type:=IPTCTypes.GraphicCharacters, Mandatory:=true, Repeatable:=false, Length:=10, Fixed:=false, Category:="Old IPTC", Description:="Identifies the provider and product.", Lock:=True)
						Case EnvelopeTags.EnvelopeNumber : Return New IPTCTag(Number:=EnvelopeTags.EnvelopeNumber, Record:=RecordNumbers.Envelope, Name:="EnvelopeNumber", HumanName:="Envelope Number", Type:=IPTCTypes.NumericChar, Mandatory:=true, Repeatable:=false, Length:=8, Fixed:=true, Category:="Old IPTC", Description:="The characters form a number that will be unique for the date specified in DateSent and for the Service Identifier specified in ServiceIdentifier.", Lock:=True)
						Case EnvelopeTags.ProductID : Return New IPTCTag(Number:=EnvelopeTags.ProductID, Record:=RecordNumbers.Envelope, Name:="ProductID", HumanName:="Product I.D.", Type:=IPTCTypes.GraphicCharacters, Mandatory:=false, Repeatable:=true, Length:=32, Fixed:=false, Category:="Old IPTC", Description:="Allows a provider to identify subsets of its overall service.", Lock:=True)
						Case EnvelopeTags.EnvelopePriority : Return New IPTCTag(Number:=EnvelopeTags.EnvelopePriority, Record:=RecordNumbers.Envelope, Name:="EnvelopePriority", HumanName:="Envelope Priority", Type:=IPTCTypes.NumericChar, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Specifies the envelope handling priority and not the editorial urgency (see 2:10, Urgency).", Lock:=True)
						Case EnvelopeTags.DateSent : Return New IPTCTag(Number:=EnvelopeTags.DateSent, Record:=RecordNumbers.Envelope, Name:="DateSent", HumanName:="Date Sent", Type:=IPTCTypes.CCYYMMDD, Mandatory:=true, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="Indicates year, month and day the service sent the material.", Lock:=True)
						Case EnvelopeTags.TimeSent : Return New IPTCTag(Number:=EnvelopeTags.TimeSent, Record:=RecordNumbers.Envelope, Name:="TimeSent", HumanName:="Time Sent", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="This is the time the service sent the material.", Lock:=True)
						Case EnvelopeTags.CodedCharacterSet : Return New IPTCTag(Number:=EnvelopeTags.CodedCharacterSet, Record:=RecordNumbers.Envelope, Name:="CodedCharacterSet", HumanName:="CodedCharacterSet", Type:=IPTCTypes.ByteArray, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Old IPTC", Description:="Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.", Lock:=True)
						Case EnvelopeTags.UNO : Return New IPTCTag(Number:=EnvelopeTags.UNO, Record:=RecordNumbers.Envelope, Name:="UNO", HumanName:="UNO", Type:=IPTCTypes.UNO, Mandatory:=false, Repeatable:=false, Length:=80, Fixed:=false, Category:="Old IPTC", Description:="UNO Unique Name of Object, providing eternal, globally unique identification for objects as specified in the IIM, independent of provider and for any media form.", Lock:=True)
						Case EnvelopeTags.ARMIdentifier : Return New IPTCTag(Number:=EnvelopeTags.ARMIdentifier, Record:=RecordNumbers.Envelope, Name:="ARMIdentifier", HumanName:="ARM Identifier", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Old IPTC", Description:="The DataSet identifies the Abstract Relationship Method (ARM) which is described in a document registered by the originator of the ARM with the IPTC and NAA.", [Enum]:=GetType(ARMMethods), Lock:=True)
						Case EnvelopeTags.ARMVersion : Return New IPTCTag(Number:=EnvelopeTags.ARMVersion, Record:=RecordNumbers.Envelope, Name:="ARMVersion", HumanName:="ARM Version", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Old IPTC", Description:="A number representing the particular version of the ARM specified in DataSet <see cref='ARMIdentifier'/>.", [Enum]:=GetType(ARMVersions), Lock:=True)
						Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(EnvelopeTags))
					End Select
				Case RecordNumbers.Application
					Select Case TagNumber
						Case ApplicationTags.RecordVersion : Return New IPTCTag(Number:=ApplicationTags.RecordVersion, Record:=RecordNumbers.Application, Name:="RecordVersion", HumanName:="Record Version", Type:=IPTCTypes.UShort_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Internal", Description:="A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.", Lock:=True)
						Case ApplicationTags.ObjectTypeReference : Return New IPTCTag(Number:=ApplicationTags.ObjectTypeReference, Record:=RecordNumbers.Application, Name:="ObjectTypeReference", HumanName:="Object Type Reference", Type:=IPTCTypes.Num2_Str, Mandatory:=false, Repeatable:=false, Length:=67, Fixed:=false, Category:="Category", Description:="The Object Type is used to distinguish between different types of objects within the IIM.", [Enum]:=GetType(ObjectTypes), Lock:=True)
						Case ApplicationTags.ObjectAttributeReference : Return New IPTCTag(Number:=ApplicationTags.ObjectAttributeReference, Record:=RecordNumbers.Application, Name:="ObjectAttributeReference", HumanName:="Object Attribute Reference", Type:=IPTCTypes.Num3_Str, Mandatory:=false, Repeatable:=true, Length:=68, Fixed:=false, Category:="Category", Description:="The Object Attribute defines the nature of the object independent of the Subject.", [Enum]:=GetType(ObjectAttributes), Lock:=True)
						Case ApplicationTags.EditStatus : Return New IPTCTag(Number:=ApplicationTags.EditStatus, Record:=RecordNumbers.Application, Name:="EditStatus", HumanName:="Edit Status", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=64, Fixed:=false, Category:="Status", Description:="Status of the objectdata, according to the practice of the provider.", Lock:=True)
						Case ApplicationTags.EditorialUpdate : Return New IPTCTag(Number:=ApplicationTags.EditorialUpdate, Record:=RecordNumbers.Application, Name:="EditorialUpdate", HumanName:="Editorial Update", Type:=IPTCTypes.Enum_NumChar, Mandatory:=false, Repeatable:=false, Length:=2, Fixed:=true, Category:="Status", Description:="Indicates the type of update that this object provides to a previous object. The link to the previous object is made using the ARM (DataSets 1:120 and 1:122 (<see cref='ARM'/>)), according to the practices of the provider.", [Enum]:=GetType(EditorialUpdateValues), Lock:=True)
						Case ApplicationTags.Urgency : Return New IPTCTag(Number:=ApplicationTags.Urgency, Record:=RecordNumbers.Application, Name:="Urgency", HumanName:="Urgency", Type:=IPTCTypes.NumericChar, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Specifies the editorial urgency of content and not necessarily the envelope handling priority (see 1:60, EnvelopePriority).", Lock:=True)
						Case ApplicationTags.SubjectReference : Return New IPTCTag(Number:=ApplicationTags.SubjectReference, Record:=RecordNumbers.Application, Name:="SubjectReference", HumanName:="Subject Reference", Type:=IPTCTypes.SubjectReference, Mandatory:=false, Repeatable:=true, Length:=236, Fixed:=false, Category:="Old IPTC", Description:="The Subject Reference is a structured definition of the subject matter.", Lock:=True)
						Case ApplicationTags.Category : Return New IPTCTag(Number:=ApplicationTags.Category, Record:=RecordNumbers.Application, Name:="Category", HumanName:="Category", Type:=IPTCTypes.Alpha, Mandatory:=false, Repeatable:=false, Length:=3, Fixed:=false, Category:="Category", Description:="Identifies the subject of the objectdata in the opinion of the provider.", Lock:=True)
						Case ApplicationTags.SupplementalCategory : Return New IPTCTag(Number:=ApplicationTags.SupplementalCategory, Record:=RecordNumbers.Application, Name:="SupplementalCategory", HumanName:="Supplemental Category", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=true, Length:=32, Fixed:=false, Category:="Category", Description:="Supplemental categories further refine the subject of an objectdata.", Lock:=True)
						Case ApplicationTags.FixtureIdentifier : Return New IPTCTag(Number:=ApplicationTags.FixtureIdentifier, Record:=RecordNumbers.Application, Name:="FixtureIdentifier", HumanName:="Fixture Identifier", Type:=IPTCTypes.GraphicCharacters, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Category", Description:="Identifies objectdata that recurs often and predictably.", Lock:=True)
						Case ApplicationTags.Keywords : Return New IPTCTag(Number:=ApplicationTags.Keywords, Record:=RecordNumbers.Application, Name:="Keywords", HumanName:="Keywords", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=true, Length:=64, Fixed:=false, Category:="Category", Description:="Used to indicate specific information retrieval words.", Lock:=True)
						Case ApplicationTags.ContentLocationCode : Return New IPTCTag(Number:=ApplicationTags.ContentLocationCode, Record:=RecordNumbers.Application, Name:="ContentLocationCode", HumanName:="Content Location Code", Type:=IPTCTypes.StringEnum, Mandatory:=true, Repeatable:=false, Length:=3, Fixed:=true, Category:="Location", Description:="Indicates the code of a country/geographical location referenced by the content of the object.", [Enum]:=GetType(ISO3166), Lock:=True)
						Case ApplicationTags.ContentLocationName : Return New IPTCTag(Number:=ApplicationTags.ContentLocationName, Record:=RecordNumbers.Application, Name:="ContentLocationName", HumanName:="Content Location Name", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=64, Fixed:=false, Category:="Location", Description:="Provides a full, publishable name of a country/geographical location referenced by the content of the object, according to guidelines of the provider.", Lock:=True)
						Case ApplicationTags.ReleaseDate : Return New IPTCTag(Number:=ApplicationTags.ReleaseDate, Record:=RecordNumbers.Application, Name:="ReleaseDate", HumanName:="Release Date", Type:=IPTCTypes.CCYYMMDD, Mandatory:=false, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="The earliest date the provider intends the object to be used.", Lock:=True)
						Case ApplicationTags.ReleaseTime : Return New IPTCTag(Number:=ApplicationTags.ReleaseTime, Record:=RecordNumbers.Application, Name:="ReleaseTime", HumanName:="Release Time", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="The earliest time the provider intends the object to be used.", Lock:=True)
						Case ApplicationTags.ExpirationDate : Return New IPTCTag(Number:=ApplicationTags.ExpirationDate, Record:=RecordNumbers.Application, Name:="ExpirationDate", HumanName:="Expiration Date", Type:=IPTCTypes.CCYYMMDD, Mandatory:=false, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="The latest date the provider or owner intends the objectdata to be used.", Lock:=True)
						Case ApplicationTags.ExpirationTime : Return New IPTCTag(Number:=ApplicationTags.ExpirationTime, Record:=RecordNumbers.Application, Name:="ExpirationTime", HumanName:="Expiration Time", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="The latest time the provider or owner intends the objectdata to be used.", Lock:=True)
						Case ApplicationTags.SpecialInstructions : Return New IPTCTag(Number:=ApplicationTags.SpecialInstructions, Record:=RecordNumbers.Application, Name:="SpecialInstructions", HumanName:="Special Instructions", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=256, Fixed:=false, Category:="Other", Description:="Other editorial instructions concerning the use of the objectdata, such as embargoes and warnings.", Lock:=True)
						Case ApplicationTags.ActionAdvised : Return New IPTCTag(Number:=ApplicationTags.ActionAdvised, Record:=RecordNumbers.Application, Name:="ActionAdvised", HumanName:="Action Advised", Type:=IPTCTypes.Enum_NumChar, Mandatory:=false, Repeatable:=false, Length:=2, Fixed:=true, Category:="Other", Description:="Indicates the type of action that this object provides to a previous object.", [Enum]:=GetType(AdvisedActions), Lock:=True)
						Case ApplicationTags.ReferenceService : Return New IPTCTag(Number:=ApplicationTags.ReferenceService, Record:=RecordNumbers.Application, Name:="ReferenceService", HumanName:="Reference Service", Type:=IPTCTypes.GraphicCharacters, Mandatory:=true, Repeatable:=false, Length:=10, Fixed:=false, Category:="Old IPTC", Description:="Identifies the Service Identifier of a prior envelope to which the current object refers.", Lock:=True)
						Case ApplicationTags.ReferenceDate : Return New IPTCTag(Number:=ApplicationTags.ReferenceDate, Record:=RecordNumbers.Application, Name:="ReferenceDate", HumanName:="Reference Date", Type:=IPTCTypes.CCYYMMDD, Mandatory:=true, Repeatable:=false, Length:=8, Fixed:=true, Category:="Old IPTC", Description:="Identifies the date of a prior envelope to which the current object refers.", Lock:=True)
						Case ApplicationTags.ReferenceNumber : Return New IPTCTag(Number:=ApplicationTags.ReferenceNumber, Record:=RecordNumbers.Application, Name:="ReferenceNumber", HumanName:="Reference Number", Type:=IPTCTypes.NumericChar, Mandatory:=true, Repeatable:=false, Length:=8, Fixed:=true, Category:="Old IPTC", Description:="Identifies the Envelope Number of a prior envelope to which the current object refers.", Lock:=True)
						Case ApplicationTags.DateCreated : Return New IPTCTag(Number:=ApplicationTags.DateCreated, Record:=RecordNumbers.Application, Name:="DateCreated", HumanName:="Date Created", Type:=IPTCTypes.CCYYMMDDommitable, Mandatory:=false, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="The date the intellectual content of the objectdata was created rather than the date of the creation of the physical representation.", Lock:=True)
						Case ApplicationTags.TimeCreated : Return New IPTCTag(Number:=ApplicationTags.TimeCreated, Record:=RecordNumbers.Application, Name:="TimeCreated", HumanName:="Time Created", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.", Lock:=True)
						Case ApplicationTags.DigitalCreationDate : Return New IPTCTag(Number:=ApplicationTags.DigitalCreationDate, Record:=RecordNumbers.Application, Name:="DigitalCreationDate", HumanName:="Digital Creation Date", Type:=IPTCTypes.CCYYMMDD, Mandatory:=false, Repeatable:=false, Length:=8, Fixed:=true, Category:="Date", Description:="The date the digital representation of the objectdata was created.", Lock:=True)
						Case ApplicationTags.DigitalCreationTime : Return New IPTCTag(Number:=ApplicationTags.DigitalCreationTime, Record:=RecordNumbers.Application, Name:="DigitalCreationTime", HumanName:="Digital Creation Time", Type:=IPTCTypes.HHMMSS_HHMM, Mandatory:=false, Repeatable:=false, Length:=11, Fixed:=true, Category:="Date", Description:="The time the digital representation of the objectdata was created.", Lock:=True)
						Case ApplicationTags.OriginatingProgram : Return New IPTCTag(Number:=ApplicationTags.OriginatingProgram, Record:=RecordNumbers.Application, Name:="OriginatingProgram", HumanName:="Originating Program", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Other", Description:="Identifies the type of program used to originate the objectdata.", Lock:=True)
						Case ApplicationTags.ProgramVersion : Return New IPTCTag(Number:=ApplicationTags.ProgramVersion, Record:=RecordNumbers.Application, Name:="ProgramVersion", HumanName:="Program Version", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=10, Fixed:=false, Category:="Other", Description:="Identifies the type of program used to originate the objectdata.", Lock:=True)
						Case ApplicationTags.ObjectCycle : Return New IPTCTag(Number:=ApplicationTags.ObjectCycle, Record:=RecordNumbers.Application, Name:="ObjectCycle", HumanName:="Object Cycle", Type:=IPTCTypes.StringEnum, Mandatory:=false, Repeatable:=false, Length:=1, Fixed:=true, Category:="Status", Description:="Virtually only used in North America.", [Enum]:=GetType(ObjectCycleValues), Lock:=True)
						Case ApplicationTags.ByLine : Return New IPTCTag(Number:=ApplicationTags.ByLine, Record:=RecordNumbers.Application, Name:="ByLine", HumanName:="By-line", Type:=IPTCTypes.TextWithSpaces, Mandatory:=true, Repeatable:=false, Length:=32, Fixed:=false, Category:="Author", Description:="Contains name of the creator of the objectdata, e.g. writer, photographer or graphic artist.", Lock:=True)
						Case ApplicationTags.ByLineTitle : Return New IPTCTag(Number:=ApplicationTags.ByLineTitle, Record:=RecordNumbers.Application, Name:="ByLineTitle", HumanName:="By-line Title", Type:=IPTCTypes.TextWithSpaces, Mandatory:=false, Repeatable:=false, Length:=32, Fixed:=false, Category:="Author", Description:="A by-line title is the title of the creator or creators of an objectdata. Where used, a by-line title should follow the by-line it modifies.", Lock:=True)
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
						Case ApplicationTags.ObjectDataPreviewFileFormat : Return New IPTCTag(Number:=ApplicationTags.ObjectDataPreviewFileFormat, Record:=RecordNumbers.Application, Name:="ObjectDataPreviewFileFormat", HumanName:="ObjectData Preview File Format", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Embeded object", Description:="The file format of the ObjectData Preview.", [Enum]:=GetType(FileFormats), Lock:=True)
						Case ApplicationTags.ObjectDataPreviewFileFormatVersion : Return New IPTCTag(Number:=ApplicationTags.ObjectDataPreviewFileFormatVersion, Record:=RecordNumbers.Application, Name:="ObjectDataPreviewFileFormatVersion", HumanName:="ObjectData Preview File Format Version", Type:=IPTCTypes.Enum_binary, Mandatory:=true, Repeatable:=false, Length:=2, Fixed:=true, Category:="Embeded object", Description:="The particular version of the ObjectData Preview File Format specified in ObjectDataPreviewFileFormat", [Enum]:=GetType(FileFormatVersions), Lock:=True)
						Case ApplicationTags.ObjectDataPreviewData : Return New IPTCTag(Number:=ApplicationTags.ObjectDataPreviewData, Record:=RecordNumbers.Application, Name:="ObjectDataPreviewData", HumanName:="ObjectData Preview Data", Type:=IPTCTypes.ByteArray, Mandatory:=true, Repeatable:=false, Length:=0, Fixed:=false, Category:="Embeded object", Description:="Maximum size of 256000 octets consisting of binary data.", Lock:=True)
						Case Else : Throw New InvalidEnumArgumentException("TagNumber",TagNumber,GetType(ApplicationTags))
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
#End Region
#Region "Groups"
		''' <summary>Groups of tags</summary>
		Public Enum Groups
			''' <summary>Abstract Relation Method</summary>
			<Category("Old IPTC")> <FieldDisplayName("ARM")> ARM
			''' <summary>Country/geographical location referenced by the content of the object</summary>
			<Category("Location")> <FieldDisplayName("Content Location")> ContentLocation
			''' <summary>Identifies a prior envelope to which the current object refers.</summary>
			<Category("Old IPTC")> <FieldDisplayName("Reference")> Reference
			''' <summary>Creator of the object data</summary>
			<Category("Author")> <FieldDisplayName("By-line info")> ByLineInfo
			''' <summary>Preview of embeded object</summary>
			<Category("Embeded object")> <FieldDisplayName("ObjectData Preview")> ObjectDataPreview
		End Enum
		''' <summary>Gets information about known group of IPTC tags</summary>
		''' <param name="Group">Code of group to get information about</param>
		Public Shared Function GetGroup(ByVal Group As Groups) As GroupInfo
			Select Case Group
				Case Groups.ARM : Return New GroupInfo("ARM", "ARM", Groups.ARM, GetType(ARMGroup), "Old IPTC", "Abstract Relation Method", false, false, GetTag(RecordNumbers.Envelope, EnvelopeTags.ARMIdentifier), GetTag(RecordNumbers.Envelope, EnvelopeTags.ARMVersion))
				Case Groups.ContentLocation : Return New GroupInfo("ContentLocation", "ContentLocation", Groups.ContentLocation, GetType(ContentLocationGroup), "Location", "Country/geographical location referenced by the content of the object", false, true, GetTag(RecordNumbers.Application, ApplicationTags.ContentLocationCode), GetTag(RecordNumbers.Application, ApplicationTags.ContentLocationName))
				Case Groups.Reference : Return New GroupInfo("Reference", "Reference", Groups.Reference, GetType(ReferenceGroup), "Old IPTC", "Identifies a prior envelope to which the current object refers.", false, true, GetTag(RecordNumbers.Application, ApplicationTags.ReferenceService), GetTag(RecordNumbers.Application, ApplicationTags.ReferenceDate), GetTag(RecordNumbers.Application, ApplicationTags.ReferenceNumber))
				Case Groups.ByLineInfo : Return New GroupInfo("ByLineInfo", "ByLineInfo", Groups.ByLineInfo, GetType(ByLineInfoGroup), "Author", "Creator of the object data", false, true, GetTag(RecordNumbers.Application, ApplicationTags.ByLine), GetTag(RecordNumbers.Application, ApplicationTags.ByLineTitle))
				Case Groups.ObjectDataPreview : Return New GroupInfo("ObjectDataPreview", "ObjectDataPreview", Groups.ObjectDataPreview, GetType(ObjectDataPreviewGroup), "Embeded object", "Preview of embeded object", false, false, GetTag(RecordNumbers.Application, ApplicationTags.ObjectDataPreviewFileFormat), GetTag(RecordNumbers.Application, ApplicationTags.ObjectDataPreviewFileFormatVersion), GetTag(RecordNumbers.Application, ApplicationTags.ObjectDataPreviewData))
				Case Else : Throw New InvalidEnumArgumentException("Group", Group, GetType(Groups))
			End Select
		End Function
#Region "Classes"
		''' <summary>Abstract Relation Method</summary>
		<FieldDisplayName("ARM")> <Category("Old IPTC")> <TypeConverter(GetType(ExpandableObjectConverter))> Partial Public NotInheritable Class ARMGroup : Inherits Group
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
				<Category("Old IPTC")> <FieldDisplayName("ARM Identifier")> <CLSCompliant(False)>Public Property ARMIdentifier As ARMMethods
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
				<Category("Old IPTC")> <FieldDisplayName("ARM Version")> <CLSCompliant(False)>Public Property ARMVersion As ARMVersions
					Get
						Return _ARMVersion
					End Get
					Set
						_ARMVersion = value
					End Set
				End Property

		End Class
		''' <summary>Country/geographical location referenced by the content of the object</summary>
		<FieldDisplayName("Content Location")> <Category("Location")> <TypeConverter(GetType(ExpandableObjectConverter))> Partial Public NotInheritable Class ContentLocationGroup : Inherits Group
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
				<Category("Location")> <FieldDisplayName("Content Location Code")> <CLSCompliant(False)>Public Property ContentLocationCode As StringEnum(Of ISO3166)
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
				<Category("Location")> <FieldDisplayName("Content Location Name")> Public Property ContentLocationName As String
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
		<FieldDisplayName("Reference")> <Category("Old IPTC")> <TypeConverter(GetType(ExpandableObjectConverter))> Partial Public NotInheritable Class ReferenceGroup : Inherits Group
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
				<Category("Old IPTC")> <FieldDisplayName("Reference Service")> Public Property ReferenceService As String
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
				<Category("Old IPTC")> <FieldDisplayName("Reference Date")> Public Property ReferenceDate As Date
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
				<Category("Old IPTC")> <FieldDisplayName("Reference Number")> Public Property ReferenceNumber As Decimal
					Get
						Return _ReferenceNumber
					End Get
					Set
						_ReferenceNumber = value
					End Set
				End Property

		End Class
		''' <summary>Creator of the object data</summary>
		<FieldDisplayName("By-line info")> <Category("Author")> <TypeConverter(GetType(ExpandableObjectConverter))> Partial Public NotInheritable Class ByLineInfoGroup : Inherits Group
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
				<Category("Author")> <FieldDisplayName("By-line")> Public Property ByLine As String
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
				<Category("Author")> <FieldDisplayName("By-line Title")> Public Property ByLineTitle As String
					Get
						Return _ByLineTitle
					End Get
					Set
						_ByLineTitle = value
					End Set
				End Property

		End Class
		''' <summary>Preview of embeded object</summary>
		<FieldDisplayName("ObjectData Preview")> <Category("Embeded object")> <TypeConverter(GetType(ExpandableObjectConverter))> Partial Public NotInheritable Class ObjectDataPreviewGroup : Inherits Group
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
				<Category("Embeded object")> <FieldDisplayName("ObjectData Preview File Format")> <CLSCompliant(False)>Public Property ObjectDataPreviewFileFormat As FileFormats
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
				<Category("Embeded object")> <FieldDisplayName("ObjectData Preview File Format Version")> <CLSCompliant(False)>Public Property ObjectDataPreviewFileFormatVersion As FileFormatVersions
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
				<Category("Embeded object")> <FieldDisplayName("ObjectData Preview Data")> Public Property ObjectDataPreviewData As Byte()
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
		<FieldDisplayName("ARM")> <Category("Old IPTC")> Public Property ARM As ARMGroup
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
		<FieldDisplayName("Content Location")> <Category("Location")> Public Property ContentLocation As ContentLocationGroup()
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
		<FieldDisplayName("Reference")> <Category("Old IPTC")> Public Property Reference As ReferenceGroup()
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
		<FieldDisplayName("By-line info")> <Category("Author")> Public Property ByLineInfo As ByLineInfoGroup()
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
		<FieldDisplayName("ObjectData Preview")> <Category("Embeded object")> Public Property ObjectDataPreview As ObjectDataPreviewGroup
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
#End Region
#Region "Properties"
		''' <summary>A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.</summary>
		''' <remarks>Version numbers are assigned by IPTC and NAA. The version number of this record is four (4).</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number identifying the version of the Information Interchange Model, Part I, utilised by the provider.")> _
		<Category("Internal")> <FieldDisplayName("Model Version")> <CLSCompliant(False)>Public Property ModelVersion As UShort
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
		<Category("Old IPTC")> <FieldDisplayName("Destination")> Public Property Destination As String()
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
		<Category("Old IPTC")> <FieldDisplayName("File Format")> <CLSCompliant(False)>Public Property FileFormat As FileFormats
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
		<Category("Old IPTC")> <FieldDisplayName("File Format Version")> <CLSCompliant(False)>Public Property FileFormatVersion As FileFormatVersions
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
		<Category("Old IPTC")> <FieldDisplayName("Service Identifier")> Public Property ServiceIdentifier As String
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
		<Category("Old IPTC")> <FieldDisplayName("Envelope Number")> Public Property EnvelopeNumber As Decimal
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
		<Category("Old IPTC")> <FieldDisplayName("Product I.D.")> Public Property ProductID As String()
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
		<Category("Status")> <FieldDisplayName("Envelope Priority")> Public Property EnvelopePriority As Decimal
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
		<Category("Date")> <FieldDisplayName("Date Sent")> Public Property DateSent As Date
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
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("This is the time the service sent the material.")> _
		<Category("Date")> <FieldDisplayName("Time Sent")> Public Property TimeSent As Time
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
		''' <remarks>The control functions apply to character oriented DataSets in records 2-6. They also apply to record 8, unless the objectdata explicitly, or the File Format implicitly, defines character sets otherwise. If this DataSet contains the designation function for Unicode in UTF-8 then no other announcement, designation or invocation functions are permitted in this DataSet or in records 2-6. For all other character sets, one or more escape sequences are used: for the announcement of the code extension facilities used in the data which follows, for the initial designation of the G0, G1, G2 and G3 graphic character sets and for the initial invocation of the graphic set (7 bits) or the lefthand and the right-hand graphic set (8 bits) and for the initial invocation of the C0 (7 bits) or of the C0 and the C1 control character sets (8 bits). The announcement of the code extension facilities, if transmitted, must appear in this data set. Designation and invocation of graphic and control function sets (shifting) may be transmitted anywhere where the escape and the other necessary control characters are permitted. However, it is recommended to transmit in this DataSet an initial designation and invocation, i.e. to define all designations and the shift status currently in use by transmitting the appropriate escape sequences and locking-shift functions. If is omitted, the default for records 2-6 and 8 is ISO 646 IRV (7 bits) or ISO 4873 DV (8 bits). Record 1 shall always use ISO 646 IRV or ISO 4873 DV respectively. ECMA as the ISO Registration Authority for escape sequences maintains the International Register of Coded Character Sets to be used with escape sequences, a register of Codes and allocated standardised escape sequences, which are recognised by IPTC-NAA without further approval procedure. The registration procedure is defined in ISO 2375. IPTC-NAA maintain a Register of Codes and allocated private escape sequences, which are shown in paragraph 1.2. IPTC may, as Sponsoring Authority, submit such private sequence Codes for approval as standardised sequence Codes. The registers consist of a Graphic repertoire, a Control function repertoire and a Repertoire of other coding systems (e.g. complete Codes). Together they represent the IPTC-NAA Code Library. Graphic Repertoire94-character sets (intermediate character 2/8 to 2/11)002ISO 646 IRV 4/0004ISO 646 British Version 4/1006ISO 646 USA Version (ASCII) 4/2008-1NATS Primary Set for Finland and Sweden 4/3008-2NATS Secondary Set for Finland and Sweden 4/4009-1NATS Primary Set for Denmark and Norway 4/5009-2NATS Secondary Set for Denmark and Norway 4/6010ISO 646 Swedish Version (SEN 850200) 4/7015ISO 646 Italian Version (ECMA) 5/9016ISO 646 Portuguese Version (ECMA Olivetti) 4/12017ISO 646 Spanish Version (ECMA Olivetti) 5/10018ISO 646 Greek Version (ECMA) 5/11021ISO 646 German Version (DIN 66003) 4/11037Basic Cyrillic Character Set (ISO 5427) 4/14060ISO 646 Norwegian Version (NS 4551) 6/0069ISO 646 French Version (NF Z 62010-1982) 6/6084ISO 646 Portuguese Version (ECMA IBM) 6/7085ISO 646 Spanish Version (ECMA IBM) 6/8086ISO 646 Hungarian Version (HS 7795/3) 6/9121Alternate Primary Graphic Set No. 1 (Canada CSA Z 243.4-1985) 7/7122Alternate Primary Graphic Set No. 2 (Canada CSA Z 243.4-1985) 7/896-character sets (intermediate character 2/12 to 2/15):100Right-hand Part of Latin Alphabet No. 1 (ISO 8859-1) 4/1101Right-hand Part of Latin Alphabet No. 2 (ISO 8859-2) 4/2109Right-hand Part of Latin Alphabet No. 3 (ISO 8859-3) 4/3110Right-hand Part of Latin Alphabet No. 4 (ISO 8859-4) 4/4111Right-hand Part of Latin/Cyrillic Alphabet (ISO 8859-5) 4/0125Right-hand Part of Latin/Greek Alphabet (ISO 8859-7) 4/6127Right-hand Part of Latin/Arabic Alphabet (ISO 8859-6) 4/7138Right-hand Part of Latin/Hebrew Alphabet (ISO 8859-8) 4/8139Right-hand Part of Czechoslovak Standard (ČSN 369103) 4/9Multiple-Byte Graphic Character Sets (1st intermediate character 2/4, 2nd intermediate character 2/8 to 2/11)87Japanese characters (JIS X 0208-1983) 4/2Control Function RepertoireC0 Control Function Sets (intermediate character 2/1)001C0 Set of ISO 646 4/0026IPTC C0 Set for newspaper text transmission 4/3036C0 Set of ISO 646 with SS2 instead of IS4 4/4104Minimum C0 Set for ISO 4873 4/7 C1 Control Function Sets (intermediate character 2/2)077C1 Control Set of ISO 6429 4/3105Minimum C1 Set for ISO 4873 4/7 Single Additional Control Functions062Locking-Shift Two (LS2), ISO 2022 6/14063Locking-Shift Three (LS3), ISO 2022 6/15064Locking-Shift Three Right (LS3R), ISO 2022 7/12065Locking-Shift Two Right (LS2R), ISO 2022 7/13066Locking-Shift One Right (LS1R), ISO 2022 7/14Repertoire of Other Coding Systems (e.g. complete Codes, intermediate character 2/5 )196UCS Transformation Format (UTF-8) 4/7 --></remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Array of bytes consisting of one or more control functions used for the announcement, invocation or designation of coded character sets. The control functions follow the ISO 2022 standard and may consist of the escape control character and one or more graphic characters. For more details see Appendix C, the IPTC-NAA Code Library.")> _
		<Category("Old IPTC")> <FieldDisplayName("CodedCharacterSet")> Public Property CodedCharacterSet As Byte()
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
		<Category("Old IPTC")> <FieldDisplayName("UNO")> Public Property UNO As iptcUNO
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
		''' <remarks>Version numbers are assigned by IPTC and NAA. The version number of this record is four (4).</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("A number identifying the version of the Information Interchange Model, Part II (Record 2:xx), utilised by the provider.")> _
		<Category("Internal")> <FieldDisplayName("Record Version")> <CLSCompliant(False)>Public Property RecordVersion As UShort
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
		<Category("Category")> <FieldDisplayName("Object Type Reference")> <CLSCompliant(False)>Public Property ObjectTypeReference As NumStr2(Of ObjectTypes)
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
		<Category("Category")> <FieldDisplayName("Object Attribute Reference")> <CLSCompliant(False)>Public Property ObjectAttributeReference As NumStr3(Of ObjectAttributes)()
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
		''' <summary>Status of the objectdata, according to the practice of the provider.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("Status of the objectdata, according to the practice of the provider.")> _
		<Category("Status")> <FieldDisplayName("Edit Status")> Public Property EditStatus As String
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
		<Category("Status")> <FieldDisplayName("Editorial Update")> Public Property EditorialUpdate As EditorialUpdateValues
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
		<Category("Status")> <FieldDisplayName("Urgency")> Public Property Urgency As Decimal
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
		<Category("Old IPTC")> <FieldDisplayName("Subject Reference")> Public Property SubjectReference As iptcSubjectReference()
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
		<Category("Category")> <FieldDisplayName("Category")> <Obsolete("Use of this DataSet is Deprecated. It is likely that this DataSet will not be included in further versions of the IIM.")> Public Property Category As String
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
		<Category("Category")> <FieldDisplayName("Supplemental Category")> <Obsolete("Use of this DataSet is Deprecated. It is likely that this DataSet will not be included in further versions of the IIM.")> Public Property SupplementalCategory As String()
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
		<Category("Category")> <FieldDisplayName("Fixture Identifier")> Public Property FixtureIdentifier As String
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
		<Category("Category")> <FieldDisplayName("Keywords")> Public Property Keywords As String()
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
		<Category("Date")> <FieldDisplayName("Release Date")> Public Property ReleaseDate As Date
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
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The earliest time the provider intends the object to be used.")> _
		<Category("Date")> <FieldDisplayName("Release Time")> Public Property ReleaseTime As Time
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
		<Category("Date")> <FieldDisplayName("Expiration Date")> Public Property ExpirationDate As Date
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
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The latest time the provider or owner intends the objectdata to be used.")> _
		<Category("Date")> <FieldDisplayName("Expiration Time")> Public Property ExpirationTime As Time
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
		<Category("Other")> <FieldDisplayName("Special Instructions")> Public Property SpecialInstructions As String
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
		<Category("Other")> <FieldDisplayName("Action Advised")> Public Property ActionAdvised As AdvisedActions
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
		<Category("Date")> <FieldDisplayName("Date Created")> Public Property DateCreated As OmmitableDate
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
		''' <remarks>Where the time cannot be precisely determined, the closest approximation should be used.</remarks>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The time the intellectual content of the objectdata current source material was created rather than the creation of the physical representation.")> _
		<Category("Date")> <FieldDisplayName("Time Created")> Public Property TimeCreated As Time
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
		<Category("Date")> <FieldDisplayName("Digital Creation Date")> Public Property DigitalCreationDate As Date
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
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The time the digital representation of the objectdata was created.")> _
		<Category("Date")> <FieldDisplayName("Digital Creation Time")> Public Property DigitalCreationTime As Time
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
		<Category("Other")> <FieldDisplayName("Originating Program")> Public Property OriginatingProgram As String
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
		<Category("Other")> <FieldDisplayName("Program Version")> Public Property ProgramVersion As String
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
		<Category("Status")> <FieldDisplayName("Object Cycle")> <CLSCompliant(False)>Public Property ObjectCycle As StringEnum(Of ObjectCycleValues)
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
		<Category("Location")> <FieldDisplayName("City")> Public Property City As String
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
		<Category("Location")> <FieldDisplayName("Sublocation")> Public Property SubLocation As String
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
		<Category("Location")> <FieldDisplayName("Province/State")> Public Property ProvinceState As String
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
		<Category("Location")> <FieldDisplayName("Country/Primary Location Code")> <CLSCompliant(False)>Public Property CountryPrimaryLocationCode As StringEnum(Of ISO3166)
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
		<Category("Location")> <FieldDisplayName("Country/Primary Location Name")> Public Property CountryPrimaryLocationName As String
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
		<Category("Location")> <FieldDisplayName("Original Transmission Refrence")> Public Property OriginalTransmissionReference As String
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
		<Category("Title")> <FieldDisplayName("Headline")> Public Property Headline As String
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
		<Category("Author")> <FieldDisplayName("Credit")> Public Property Credit As String
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
		<Category("Author")> <FieldDisplayName("Source")> Public Property Source As String
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
		<Category("Author")> <FieldDisplayName("Copyright Notice")> Public Property CopyrightNotice As String
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
		<Category("Author")> <FieldDisplayName("Contact")> Public Property Contact As String()
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
		<Category("Title")> <FieldDisplayName("Caption/Abstract")> Public Property CaptionAbstract As String
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
		<Category("Author")> <FieldDisplayName("Writer/Editor")> Public Property WriterEditor As String()
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
		<Category("Title")> <FieldDisplayName("Rasterized Caption")> Public Property RasterizedeCaption As Drawing.Bitmap
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
		<Category("Image")> <FieldDisplayName("Image Type")> Public Property ImageType As iptcImageType
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
		<Category("Image")> <FieldDisplayName("Image Orientation")> <CLSCompliant(False)>Public Property ImageOrientation As StringEnum(Of Orientations)
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
		<Category("Other")> <FieldDisplayName("Language Identifier")> Public Property LanguageIdentifier As String
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
		<Category("Audio")> <FieldDisplayName("Audio Type")> Public Property AudioType As iptcAudioType
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
		<Category("Audio")> <FieldDisplayName("Audio Sampling Rate")> Public Property AudioSamplingRate As Decimal
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
		<Category("Audio")> <FieldDisplayName("Audio Sampling Resolution")> Public Property AudioSamplingResolution As Decimal
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
		<Category("Audio")> <FieldDisplayName("Audio Duration")> Public Property AudioDuration As TimeSpan
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
		<Category("Audio")> <FieldDisplayName("Audio Outcue")> Public Property AudioOutcue As String
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
		''' <summary>The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.</summary>
		''' <returns>If this instance contains this tag retuns it. Otherwise returns null</returns>
		''' <exception cref="IPTCGetException">Tag exists in this instance but it's value is invalid.</exception>
		''' <exception cref="IPTCSetException">Invalid value pased to property or other serialization error occured</exception>
		<Description("The octet is set to the binary value of '0' if the size of the objectdata is not known and is set to '1' if the size of the objectdata is known at the beginning of transfer.")> _
		<Category("Embeded object")> <FieldDisplayName("Size Mode")> Public Property SizeMode As Boolean
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
		<Category("Embeded object")> <FieldDisplayName("Max Subfile Size")> <CLSCompliant(False)>Public Property MaxSubfileSize As ULong
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
		<Category("Embeded object")> <FieldDisplayName("ObjectData Size Announced")> <CLSCompliant(False)>Public Property ObjectDataSizeAnnounced As ULong
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
		<Category("Embeded object")> <FieldDisplayName("Maximum ObjectData Size")> <CLSCompliant(False)>Public Property MaximumObjectDataSize As ULong
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
		<Category("Embeded object")> <FieldDisplayName("Subfile")> Public Property Subfile As Byte()
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
		<Category("Embeded object")> <FieldDisplayName("Confirmed ObjectData Size")> <CLSCompliant(False)>Public Property ConfirmedObjectDataSize As ULong
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
		<Category("Old IPTC")> <FieldDisplayName("ARM Identifier")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ARMIdentifier As ARMMethods
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
		<Category("Old IPTC")> <FieldDisplayName("ARM Version")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ARMVersion As ARMVersions
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
		<Category("Location")> <FieldDisplayName("Content Location Code")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ContentLocationCode As StringEnum(Of ISO3166)()
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
		<Category("Location")> <FieldDisplayName("Content Location Name")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ContentLocationName As String()
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
		<Category("Old IPTC")> <FieldDisplayName("Reference Service")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ReferenceService As String()
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
		<Category("Old IPTC")> <FieldDisplayName("Reference Date")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ReferenceDate As Date()
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
		<Category("Old IPTC")> <FieldDisplayName("Reference Number")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ReferenceNumber As Decimal()
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
		<Category("Author")> <FieldDisplayName("By-line")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ByLine As String()
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
		<Category("Author")> <FieldDisplayName("By-line Title")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ByLineTitle As String()
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
		<Category("Embeded object")> <FieldDisplayName("ObjectData Preview File Format")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ObjectDataPreviewFileFormat As FileFormats
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
		<Category("Embeded object")> <FieldDisplayName("ObjectData Preview File Format Version")> <CLSCompliant(False)><EditorBrowsable(EditorBrowsableState.Never)> Private Property ObjectDataPreviewFileFormatVersion As FileFormatVersions
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
		<Category("Embeded object")> <FieldDisplayName("ObjectData Preview Data")> <EditorBrowsable(EditorBrowsableState.Never)> Private Property ObjectDataPreviewData As Byte()
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
#End Region
	End Class
#End If
End Namespace
