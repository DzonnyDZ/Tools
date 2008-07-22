Imports System.Linq
Namespace DrawingT.MetadataT
#If Config <= Nightly Then
    '''' <summary>Provides read-write acces to block of Exif data</summary>
    '<Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    '<Version(1, 0, GetType(Exif), LastChange:="04/29/2007")> _
    Partial Public Class Exif
        '#Region "CTors"
        '''' <summary>CTor</summary>
        'Public Sub New()
        'End Sub
        '''' <summary>CTor - loads data from <see cref="ExifReader"/></summary>
        '''' <param name="reader"><see cref="ExifReader"/> to load data from</param>
        '''' <exception cref="ArgumentNullException">Unable to find predecessor of any of <see cref="ExifReader.OtherSubIFDs"/></exception>
        'Public Sub New(ByVal reader As ExifReader)
        '    Me.New()
        '    If reader Is Nothing Then Exit Sub
        '    Dim i As Integer = 0
        '    For Each IFD As ExifIFDReader In reader.IFDs
        '        If i = 0 Then
        '            _MainIFDs.Add(New IFDMain(IFD))
        '        ElseIf i = 1 Then
        '            _MainIFDs.Add(New IFDMain(IFD))
        '        Else
        '            _MainIFDs.Add(New IFD(IFD))
        '        End If
        '        i += 1
        '    Next IFD
        '    _ExifSubIFD = New IFDExif(reader.ExifSubIFD)
        '    _InteropSubIFD = New IFDInterop(reader.ExifInteroperabilityIFD)
        '    _GPSSubIFD = New IFDGPS(reader.GPSSubIFD)
        '    For Each SubIFD As ExifReader.SubIFD In reader.OtherSubIFDs
        '        _AdditionalIFDs.Add(RetrieveParent(SubIFD, reader), New IFD(SubIFD))
        '    Next SubIFD
        'End Sub
        '''' <summary>Tryes to determine IFD that precedes passed IFD in line</summary>
        '''' <param name="SubIfd">IFD to find predecessor for</param>
        '''' <param name="Reader"><see cref="ExifReader"/> used to resolve IFDs</param>
        '''' <returns>Predecessof of <paramref name="SubIfd"/> if found or null</returns>
        'Private Function RetrieveParent(ByVal SubIfd As ExifReader.SubIFD, ByVal Reader As ExifReader) As IFD
        '    If SubIfd.PreviousSubIFD Is Reader.ExifSubIFD Then
        '        Return _ExifSubIFD
        '    ElseIf SubIfd.PreviousSubIFD Is Reader.ExifInteroperabilityIFD Then
        '        Return _InteropSubIFD
        '    ElseIf SubIfd.PreviousSubIFD Is Reader.GPSSubIFD Then
        '        Return _GPSSubIFD
        '    Else
        '        For Each Srch As ExifReader.SubIFD In Reader.OtherSubIFDs
        '            If Srch Is SubIfd.PreviousSubIFD Then
        '                Return RetrieveParent(Srch, Reader)
        '            End If
        '        Next Srch
        '        Return Nothing
        '    End If
        'End Function
        '#End Region
        '#Region "IFD propertires"
        '        ''' <summary>Contains value of the <see cref="AdditionalIFDs"/> property</summary>
        '        Private _AdditionalIFDs As New Dictionary(Of IFD, IFD)
        '        ''' <summary>Contains value of the <see cref="MainIFDs"/> property</summary>
        '        Private _MainIFDs As New List(Of IFD)
        '        ''' <summary>Contains value of the <see cref="ExifSubIFD"/> property</summary>
        '        Private _ExifSubIFD As IFDExif
        '        ''' <summary>Contains value of the <see cref="InteropSubIFD"/> property</summary>
        '        Private _InteropSubIFD As IFDInterop
        '        ''' <summary>Contains value of the <see cref="GPSSubIFD"/> property</summary>
        '        Private _GPSSubIFD As IFDGPS
        '        ''' <summary>Returns GPS Sub IFD, if there is no GPS Sub IFD then an enmty is created</summary>
        '        Public ReadOnly Property GPSSubIFD() As IFDGPS
        '            Get
        '                If _GPSSubIFD Is Nothing Then _GPSSubIFD = New IFDGPS()
        '                Return _GPSSubIFD
        '            End Get
        '        End Property
        '        ''' <summary>Returns Exif Sub IFD, if there is no Exif Sub IFD then an empty is created</summary>
        '        Public ReadOnly Property ExifSubIFD() As IFDExif
        '            Get
        '                If _ExifSubIFD Is Nothing Then _ExifSubIFD = New IFDExif
        '                Return _ExifSubIFD
        '            End Get
        '        End Property
        '        ''' <summary>Returns Exif Interoperability Sub IFD, if thre is no Interop Sub IFD then an empty is created</summary>
        '        Public ReadOnly Property InteropSubIFD() As IFDInterop
        '            Get
        '                If _InteropSubIFD Is Nothing Then _InteropSubIFD = New IFDInterop
        '                Return _InteropSubIFD
        '            End Get
        '        End Property
        '        ''' <summary>Returns main IFD with given index. If IFD0 or IFD1 is missing then an empty is created</summary>
        '        ''' <param name="index">Index of IFD to retrieve. Standard values are 0 and 1</param>
        '        ''' <exception cref="ArgumentOutOfRangeException">
        '        ''' <paramref name="index"/> is less than zero -or-
        '        ''' <paramref name="index"/> is greater than 1 and main IFD with such index does not exist
        '        ''' </exception>
        '        Public ReadOnly Property MainIFDs(ByVal index As Integer) As IFD
        '            Get
        '                If index < 0 Then Throw (New ArgumentOutOfRangeException(String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "Index")))
        '                If index <= 1 AndAlso _MainIFDs.Count = 0 Then _MainIFDs.Add(New IFDMain)
        '                If index = 1 AndAlso _MainIFDs.Count = 1 Then _MainIFDs.Add(New IFDMain)
        '                If index >= _MainIFDs.Count Then Throw New ArgumentException(ResourcesT.Exceptions.IndexMustBeInRangeDefinedByCounfOfIFDs)
        '                Return _MainIFDs(index)
        '            End Get
        '        End Property
        '        ''' <summary>Returns main Exif IFD, if there is no IFD0 an empty is created</summary>
        '        Public ReadOnly Property MainIFD() As IFDMain
        '            Get
        '                Return MainIFDs(0)
        '            End Get
        '        End Property
        '        ''' <summary>Returns thumbnail IFD, if there is no IFD1 an empty is created</summary>
        '        Public ReadOnly Property ThumbnailIFD() As IFDMain
        '            Get
        '                Return MainIFDs(1)
        '            End Get
        '        End Property
        '        ''' <summary>Cound of main IFDs currently present</summary>
        '        ''' <returns>Determines possible range of the index parameter of the <see cref="MainIFDs"/> property, but 0 and 1 are always valid values for index even when value of this property is 0 or 1</returns>
        '        Public ReadOnly Property MainIFDsCount() As Integer
        '            Get
        '                Return _MainIFDs.Count
        '            End Get
        '        End Property
        '        ''' <summary>List of additional IFDs retrieved from stream when initializing</summary>
        '        Public ReadOnly Property AdditionalIFDs() As Dictionary(Of IFD, IFD).ValueCollection
        '            Get
        '                Return _AdditionalIFDs.Values
        '            End Get
        '        End Property
        '#End Region
        ''' <summary>Provides read-write access to Image File Directory of Exif data</summary>
        Public Class IFD
            ''' <summary>CTor - empty IFD</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - reads content from <see cref="ExifIFDReader"/></summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD. Can be null</param>
            Public Sub New(ByVal Reader As ExifIFDReader)
                Me.New()
                If Reader Is Nothing Then Exit Sub
                For Each rec As ExifIFDReader.DirectoryEntry In Reader.Entries
                    Records.Add(rec.Tag, New ExifRecord(rec.Data, rec.DataType, rec.Components, rec.DataType <> ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII AndAlso rec.DataType <> ExifIFDReader.DirectoryEntry.ExifDataTypes.Byte AndAlso rec.DataType <> ExifIFDReader.DirectoryEntry.ExifDataTypes.NA))
                Next rec
                ReadStandardSubIFDs(Reader)
            End Sub
            ''' <summary>If overriden in derived class reads known subIFDs nested within this IFD.</summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD.</param>
            ''' <exception cref="ArgumentNullException">Can be thrown by overriden method when <paramref name="Reader"/> is null.</exception>
            ''' <remarks>This implementation does nothing.
            ''' <para>Note for inheritors: This method is called by CTor if the <see cref="IFD"/> class after all records have been initialized. This method is not intended to be called directly from user code.</para></remarks>
            Protected Overridable Sub ReadStandardSubIFDs(ByVal Reader As ExifIFDReader)
            End Sub
            ''' <summary>Contains value of the <see cref="Records"/> property</summary>
            Private _Records As New Dictionary(Of UShort, ExifRecord)
            ''' <summary>Contains value of the <see cref="Exif"/> property</summary>
            Private _Exif As Exif
            ''' <summary>Gets instance of <see cref="Exif"/> this IPTC behaves as instance of</summary>
            ''' <value>Setting this property changes <see cref="Exif"/> property of all subsequent IFDs in <see cref="Following"/> linked-list</value>
            ''' <returns>Instance of the <see cref="MetadataT.Exif"/> class this instance is associated with; or null if this instance is not associated with instance of <see cref="Exif"/>.</returns>
            ''' <exception cref="ArgumentException">Value being set differs from value of the <see cref="Exif"/> property of <see cref="Previous"/> IFD (when <see cref="Previous"/> is non-null)</exception>
            Public Property Exif() As Exif
                Get
                    Return _Exif
                End Get
                Friend Set(ByVal value As Exif)
                    If value Is Exif Then Exit Property
                    If Me.Previous IsNot Nothing AndAlso Me.Previous.Exif IsNot value Then _
                        Throw New ArgumentException(ResourcesT.Exceptions.CannotSetValueOfTheExifPropertyToOtherInstanceThenIsValueOfExifPropertyOfPreviousIFD)
                    _Exif = value
                    If Me.Following IsNot Nothing Then Me.Following.Exif = value
                End Set
            End Property
            ''' <summary>Records in this Image File Directory</summary>
            <CLSCompliant(False)> _
            Public ReadOnly Property Records() As Dictionary(Of UShort, ExifRecord)
                Get
                    Return _Records
                End Get
            End Property
            ''' <summary>Contains value of the <see cref="Following"/> property</summary>
            Private _Following As IFD
            ''' <summary>Gets or sets IFD that follows this IFD in Exif block</summary>
            ''' <returns><see cref="IFD"/> which follows current IFD or null if there is no folowing IFD</returns>
            ''' <value>Sets following IFD of current IFD.</value>
            ''' <remarks>By setting this property you detaches all the following IFDs from linked-list of IFDs and replaces them by value being set (this may optionally have set the <see cref="Following"/> property).
            ''' <para>This property is not initialized automatically when instance of <see cref="IFD"/> is created directly. Code that wants to utilize linked lists of IFDs must initialize this property itself - such as <see cref="MetadataT.Exif"/> does.</para></remarks>
            ''' <exception cref="ArgumentException">Value being set have current instance as one of its <see cref="Following"/> IFDs =or= Value being set has non-null value of the <see cref="Previous"/> property. =or= Value being set has non-null <see cref="Exif"/> property which is different of <see cref="Exif"/> of this instance (including situation when <see cref="Exif"/> property of this instance is null and <see cref="Exif"/> property of value being set is non-null). =or= <see cref="Exif"/> is not null and value being set is already used as IFD at another position in <see cref="Exif"/>.</exception>
            ''' <exception cref="TypeMismatchException">Value being set is of type <see cref="SubIFD"/> -or- <see cref="Exif"/> is not null and this instance if <see cref="Exif">Exif</see>.<see cref="MetadataT.Exif.IFD0">IFD0</see> and value being set is not of type <see cref="IFDMain"/>.</exception>
            Public Property Following() As IFD
                Get
                    Return _Following
                End Get
                Set(ByVal value As IFD)
                    If value Is Following Then Exit Property
                    If value.Previous IsNot Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.ThePreviousPropertyOfIFDBeingSetAsFollowingMustBeNull)
                    If TypeOf value Is SubIFD Then Throw New TypeMismatchException("value", value, GetType(IFD), ResourcesT.Exceptions.FollowingIFDCannotBeSubIFD)
                    Dim Current As IFD = value.Following
                    While Current IsNot Nothing
                        If Current Is Me Then Throw New ArgumentException(ResourcesT.Exceptions.AttemptToCreateCyclicLinkedListOfIFDs)
                        Current = Current.Following
                    End While
                    If value.Exif IsNot Nothing AndAlso value.Exif IsNot Exif Then _
                        Throw New ArgumentException(ResourcesT.Exceptions.FollowingIFDMustBeMemberOfSameExifOrBeMemberOfNoExifAsCurrentIFD)
                    If Exif IsNot Nothing AndAlso Exif.ContainsIFD(value) Then _
                        Throw New ArgumentException(ResourcesT.Exceptions.GivenIFDIsAlreadyInUse)
                    VerifyFollowing(value)
                    If Following IsNot Nothing Then Following.Previous = Nothing
                    _Following = value
                    value.Previous = Me
                    Current = value
                    value.Exif = Me.Exif
                End Set
            End Property
            ''' <summary>Performs additional verification of value being passed to the <see cref="Following"/> prooperty</summary>
            ''' <param name="Following">Value to verify</param>
            ''' <exception cref="Exception">Overriden method can throw any exception when it refuses to accept given value ias new value of the <see cref="Following"/> property</exception>
            ''' <exception cref="TypeMismatchException">This implementation throws <see cref="TypeMismatchException"/> when <see cref="Exif"/> is not null and this instance is <see cref="Exif">Exif</see>.<see cref="MetadataT.Exif.IFD0">IFD0</see> and <paramref name="Following"/> is not of type <see cref="IFDMain"/> (so it is not necessary to perform this verification in <see cref="IFDMain"/> derived class).</exception>
            Protected Overridable Sub VerifyFollowing(ByVal Following As IFD)
                If Me.Exif IsNot Nothing AndAlso Me Is Me.Exif.IFD0 AndAlso Not TypeOf Following Is IFDMain Then _
                    Throw New TypeMismatchException(ResourcesT.Exceptions.TypeOfIFDFollowingAfterIFD0MustBeIFDMain, Following, GetType(IFDMain))
            End Sub
            ''' <summary>Contains value of the <see cref="Previous"/> property</summary>
            Private _Previous As IFD
            ''' <summary>Gets IFD that precedes current IFD</summary>
            ''' <returns><see cref="IFD"/> which precedes current IFD; null when this IFD is first in linked-list of IFDs</returns>
            ''' <value>Set accessor of this property is not publicly accessible, so it cannot be set diretly. This property is set when instance is passed to <see cref="Following"/> property (and unset when it is removed from there).</value>
            ''' <remarks>Internal note: Value being set of this property is nohow checked. You should always ensure that <see cref="Following"/> of previous IFD is same as <see cref="Previous"/> of following IFD.</remarks>
            Public Property Previous() As IFD
                Get
                    Return _Previous
                End Get
                Friend Set(ByVal value As IFD)
                    _Previous = value
                End Set
            End Property
            ''' <summary>Gets or sets value of specified record</summary>
            ''' <param name="Type">Type of record specifies data types of recor as well as number of components</param>
            ''' <value>New value for record. New value is assigned even if old value is of incompatible type. If value is null an item is deleted.</value>
            ''' <returns>Value of record with tag number specified in <paramref name="Type"/> if type specifies that number of components can vary or if number of components match actual number of components in record. If there is no tag with specified number present in this IFD or number of components constraint is being violated null is returned.</returns>
            ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
            <CLSCompliant(False)> _
            Default Public Overridable Property Record(ByVal Type As ExifTagFormat) As ExifRecord
                Get
                    If Type Is Nothing Then Throw New ArgumentNullException("Type")
                    If Records.ContainsKey(Type.Tag) Then
                        With Records(Type.Tag)
                            If Array.IndexOf(Type.DataTypes, Records(Type.Tag).DataType.DataType) >= 0 Then
                                If Type.NumberOfElements = 0 OrElse Type.NumberOfElements = .DataType.NumberOfElements Then
                                    Return Records(Type.Tag)
                                Else
                                    Return Nothing
                                End If
                            Else
                                Return Nothing
                            End If
                        End With
                    Else
                        Return Nothing
                    End If
                End Get
                Set(ByVal value As ExifRecord)
                    If Type Is Nothing Then Throw New ArgumentNullException("value", String.Format(ResourcesT.Exceptions.CannotBeSetToNull, "Record"))
                    If value Is Nothing Then
                        If Records.ContainsKey(Type.Tag) Then Records.Remove(Type.Tag)
                    Else
                        If Records.ContainsKey(Type.Tag) Then
                            Records(Type.Tag) = value
                        Else
                            Records.Add(Type.Tag, value)
                        End If
                    End If
                End Set
            End Property
            Public ReadOnly Property SubIFDs() As DictionaryWithEvents(Of UShort, SubIFD)
                Get
                    'TODO:Implement
                End Get
            End Property
        End Class
        ''' <summary>Describes one Exif record</summary>
        ''' <remarks>Descibes which data type record actually contains, how many items of such datatype. For recognized tags also possible format is specified via <see cref="ExifTagFormat"/></remarks>
        <CLSCompliant(False)> _
        Public Class ExifRecordDescription
            ''' <summary>Contains value of the <see cref="DataType"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _DataType As ExifIFDReader.DirectoryEntry.ExifDataTypes
            ''' <summary>Contains value of the <see cref="NumberOfElements"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _NumberOfElements As UShort
            ''' <summary>Number of elements of type <see cref="DataType"/> contained in record</summary>
            Public Property NumberOfElements() As UShort
                Get
                    Return _NumberOfElements
                End Get
                Protected Friend Set(ByVal value As UShort)
                    _NumberOfElements = value
                End Set
            End Property
            ''' <summary>Data type of items in record</summary>
            Public Property DataType() As ExifIFDReader.DirectoryEntry.ExifDataTypes
                Get
                    Return _DataType
                End Get
                Protected Set(ByVal value As ExifIFDReader.DirectoryEntry.ExifDataTypes)
                    _DataType = value
                End Set
            End Property
            ''' <summary>CTor</summary>
            ''' <param name="DataType">Data type of record</param>
            ''' <param name="NumberOfElements">Number of elements of type <paramref name="DataType"/> in record.</param>
            ''' <exception cref="ArgumentOutOfRangeException"><paramref name="NumberOfElements"/> is 0</exception>
            Public Sub New(ByVal DataType As ExifIFDReader.DirectoryEntry.ExifDataTypes, ByVal NumberOfElements As UShort)
                If NumberOfElements = 0 Then Throw New ArgumentOutOfRangeException("NumberOfElements", ResourcesT.Exceptions.NumberOfElementsCannotBe0)
                Me.DataType = DataType
                Me.NumberOfElements = NumberOfElements
            End Sub
            ''' <summary>Protected CTor that allows <see cref="NumberOfElements"/> to be zero</summary>
            ''' <param name="NumberOfElements">Number of elements of type <paramref name="DataType"/> in record.</param>
            ''' <param name="DataType"></param>
            Protected Sub New(ByVal NumberOfElements As UShort, ByVal DataType As ExifIFDReader.DirectoryEntry.ExifDataTypes)
                Me.DataType = DataType
                Me.NumberOfElements = NumberOfElements
            End Sub
        End Class

        ''' <summary>Describes which data can be stored in recognized Exif tag</summary>
        ''' <remarks>Describas which datatype(s) and lengt if allowed for specific recognized Exif record. Actual content of record is described by <see cref="ExifRecordDescription"/></remarks>
        <CLSCompliant(False)> _
        Public Class ExifTagFormat : Inherits ExifRecordDescription
            ''' <summary>CTor</summary>
            ''' <param name="NumberOfElements">Number of elemets that must exactly be in tag. If number of elements can varry pass 0 here</param>
            ''' <param name="Tag">Number of tag</param>
            ''' <param name="Name">Short name of tag</param>
            ''' <param name="DataTypes">Possible datatypes of tag. First datatype specified must be the widest and must be always specified and will be used as default</param>
            ''' <exception cref="ArgumentNullException"><paramref name="DataTypes"/> is null or contains no element</exception>
            Public Sub New(ByVal NumberOfElements As UShort, ByVal Tag As UShort, ByVal Name As String, ByVal ParamArray DataTypes As ExifIFDReader.DirectoryEntry.ExifDataTypes())
                MyBase.New(NumberOfElements, TestThrowReturn(DataTypes)(0))
                _Tag = Tag
                _Name = Name
                OtherDatatypes.AddRange(DataTypes)
                OtherDatatypes.RemoveAt(0)
            End Sub
            ''' <summary>Test if <paramref name="DataTypes"/> is null or containc no element</summary>
            ''' <param name="DataTypes">Array to test</param>
            ''' <returns><paramref name="DataTypes"/></returns>
            ''' <remarks>Used by ctor</remarks>
            ''' <exception cref="ArgumentNullException"><paramref name="DataTypes"/> is null or contains no element</exception>
            Private Shared Function TestThrowReturn(ByVal DataTypes As ExifIFDReader.DirectoryEntry.ExifDataTypes()) As ExifIFDReader.DirectoryEntry.ExifDataTypes()
                If DataTypes Is Nothing OrElse DataTypes.Length = 0 Then Throw New ArgumentNullException("DataTypes", ResourcesT.Exceptions.DataTypesCannotBeNullAndMustContainAtLeastOneElement)
                Return DataTypes
            End Function
            ''' <summary>Contains value of the <see cref="Tag"/> property</summary>
            Private _Tag As UShort
            ''' <summary>Contains value of the <see cref="Name"/> property</summary>
            Private _Name As String
            ''' <summary>Contains list of possible datatypes for tag excepting datatype specified in <see cref="DataType"/></summary>
            Private OtherDatatypes As New List(Of ExifIFDReader.DirectoryEntry.ExifDataTypes)
            ''' <summary>Represents short unique name of tag used to reference it</summary>
            Public ReadOnly Property Name() As String
                Get
                    Return _Name
                End Get
            End Property
            ''' <summary>Represents tag code in Exif</summary>
            Public ReadOnly Property Tag() As UShort
                Get
                    Return _Tag
                End Get
            End Property
            ''' <summary>Datatypes allowed for this tag</summary>
            ''' <returns>Array of datatypes allowed for this tag. First element of the array is same as <see cref="DataType"/> amd represents default and preffered datatype</returns>
            Public ReadOnly Property DataTypes() As ExifIFDReader.DirectoryEntry.ExifDataTypes()
                Get
                    Dim arr(OtherDatatypes.Count) As ExifIFDReader.DirectoryEntry.ExifDataTypes
                    If OtherDatatypes.Count > 0 Then _
                        OtherDatatypes.CopyTo(0, arr, 1, OtherDatatypes.Count)
                    arr(0) = DataType
                    Return arr
                End Get
            End Property
        End Class

        ''' <summary>Represents one Exif record</summary>
        Public Class ExifRecord
            ''' <summary>Contains value of the <see cref="Data"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Data As Object
            ''' <summary>Contains value of the <see cref="DataType"/> property</summary>
            Private _DataType As ExifRecordDescription
            ''' <summary>Contains value of the <see cref="Fixed"/> property</summary>
            Private _Fixed As Boolean
            ''' <summary>True if <see cref="ExifRecordDescription.NumberOfElements"/> of this record is fixed</summary>
            Public ReadOnly Property Fixed() As Boolean
                Get
                    Return Fixed
                End Get
            End Property
            ''' <summary>Datatype and number of items of record</summary>
            <CLSCompliant(False)> _
            Public ReadOnly Property DataType() As ExifRecordDescription
                Get
                    Return _DataType
                End Get
            End Property
            ''' <summary>Value of record</summary>
            ''' <remarks>Actual type depends on <see cref="DataType"/></remarks>
            ''' <exception cref="InvalidCastException">Setting value of incompatible type</exception>
            ''' <exception cref="ArgumentException">Attempt to assigne value with other number of components when <see cref="Fixed"/> set to true</exception>
            ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
            Public Property Data() As Object
                Get
                    Return _Data
                End Get
                Protected Set(ByVal value As Object)
                    If value Is Nothing Then Throw New ArgumentNullException("value")
                    Select Case DataType.DataType
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII
                            If TypeOf value Is Char Then value = CStr(CChar(value))
                            If TryCast(value, String) IsNot Nothing Then
                                Dim newV As String = System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(CStr(value)))
                                If Me.DataType.NumberOfElements = newV.Length OrElse Not Fixed Then
                                    _Data = newV
                                    Me.DataType.NumberOfElements = CStr(_Data).Length
                                Else
                                    Throw New ArgumentException(ResourcesT.Exceptions.CannotChangeNumberOfComponentsOfThisRecord)
                                End If
                            Else
                                Throw New InvalidCastException(ResourcesT.Exceptions.ValueOfIncompatibleTypePassedToASCIIRecord)
                            End If
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.Byte
                            SetDataValue(Of Byte)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.Double
                            SetDataValue(Of Double)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.Int16
                            SetDataValue(Of Int16)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.Int32
                            SetDataValue(Of Int32)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.NA
                            SetDataValue(Of Byte)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.SByte
                            SetDataValue(Of SByte)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.Single
                            SetDataValue(Of Single)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.SRational
                            SetDataValue(Of SRational)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16
                            SetDataValue(Of UInt16)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32
                            SetDataValue(Of UInt32)(value)
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.URational
                            SetDataValue(Of URational)(value)
                    End Select
                    _Data = value
                End Set
            End Property
            ''' <summary>Sets <paramref name="value"/> to <see cref="_Data"/> according to <see cref="DataType"/></summary>
            ''' <param name="value">Value to be set</param>
            ''' <typeparam name="T">Type of value to be set</typeparam>
            ''' <exception cref="InvalidCastException">Setting value of incompatible type</exception>
            ''' <exception cref="ArgumentException">Attempt to assigne value with other number of components when <see cref="Fixed"/> set to true</exception>
            Private Sub SetDataValue(Of T)(ByVal value As Object)
                Try
                    Dim newV As T = CType(value, T)
                    If Me.DataType.NumberOfElements = 1 OrElse Not Me.Fixed Then
                        _Data = newV
                        Me.DataType.NumberOfElements = 1
                    Else
                        Throw New ArgumentException(ResourcesT.Exceptions.CannotChangeNumberOfComponentsOfThisRecord)
                    End If
                Catch
                    Try
                        Dim newV As T() = CType(value, T())
                        If Me.DataType.NumberOfElements = newV.Length OrElse Not Fixed Then
                            _Data = newV
                            Me.DataType.NumberOfElements = newV.Length
                        Else
                            Throw New ArgumentException(ResourcesT.Exceptions.CannotChangeNumberOfComponentsOfThisRecord)
                        End If
                    Catch ex As Exception
                        Throw New InvalidCastException(ResourcesT.Exceptions.ValueOfIncompatibleTypePassedToExifRecord)
                    End Try
                End Try
            End Sub
            ''' <summary>CTor</summary>
            ''' <param name="Data">Initial value of this record</param>
            ''' <param name="Type">Describes type of data contained in this flag</param>
            ''' <param name="Fixed">Determines if length of data can be changed</param>
            ''' <exception cref="InvalidCastException">Value passed to <paramref name="Data"/> is not compatible with <paramref name="Type"/> specified</exception>
            ''' <exception cref="ArgumentException"><paramref name="Fixed"/> is set to true and lenght of <paramref name="Data"/> violates this constaint</exception>
            ''' <exception cref="ArgumentNullException"><paramref name="Data"/> is null</exception>
            <CLSCompliant(False)> _
            Public Sub New(ByVal Type As ExifRecordDescription, ByVal Data As Object, Optional ByVal Fixed As Boolean = False)
                Me._DataType = Type
                Me._Fixed = Fixed
                Me.Data = Data
            End Sub
            ''' <summary>CTor</summary>
            ''' <param name="Data">Initial value of this record</param>
            ''' <param name="Type">Data type of record</param>
            ''' <param name="NumberOfComponents">Number of components of <paramref name="Type"/></param>
            ''' <param name="fixed">Determines if length of data can be changed</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Data"/> is null</exception>
            <CLSCompliant(False)> _
             Public Sub New(ByVal Data As Object, ByVal Type As ExifIFDReader.DirectoryEntry.ExifDataTypes, Optional ByVal NumberOfComponents As UShort = 1, Optional ByVal Fixed As Boolean = False)
                Me.New(New ExifRecordDescription(Type, NumberOfComponents), Data, Fixed)
            End Sub
        End Class

#Region "IFD classes"
        ''' <summary>Exif main and thumbnail IFD</summary>
        Partial Class IFDMain : Inherits IFD
            ''' <summary>CTor - empty IFD</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - reads content from <see cref="ExifIFDReader"/></summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD. Can be null</param>
            Public Sub New(ByVal Reader As ExifIFDReader)
                MyBase.New(Reader)
            End Sub
            ''' <summary>Gets or sets value of specified record</summary>
            ''' <param name="Type">Recognized tagname of record that determines data type as well as number of components</param>
            ''' <value>New value for record. New value is assigned even if old value is of incompatible type. If value is null an item is deleted.</value>
            ''' <returns>Value of record with tag number specified by <paramref name="Type"/> if type for this tag number specifies that number of components can vary or if number of components match actual number of components in record. If there is no tag with specified number present in this IFD or number of components constraint is being violated null is returned.</returns>
            ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
            <CLSCompliant(False)> _
            Default Public Overridable Overloads Property Record(ByVal Type As Tags) As ExifRecord
                Get
                    Return MyBase.Record(Me.TagFormat(Type))
                End Get
                Set(ByVal value As ExifRecord)
                    MyBase.Record(Me.TagFormat(Type)) = value
                End Set
            End Property
            Protected Overrides Sub ReadStandardSubIFDs(ByVal Reader As ExifIFDReader)
                Dim ExifIfd = Me.Record(Tags.ExifIFD)
                If ExifIfd IsNot Nothing Then
                    Dim ExifSubIFDReader As New ExifReader.SubIFDReader(Reader.ExifReader, ExifIfd.Data, ExifSubIFDName, Reader, Reader.Entries.FindIndex(Function(a As ExifIFDReader.DirectoryEntry) a.Tag = Tags.ExifIFD))

                End If

            End Sub
        End Class
        ''' <summary>Exif Sub IFD</summary>
        Partial Class IFDExif : Inherits SubIFD
            ''' <summary>CTor - empty IFD</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - reads content from <see cref="ExifIFDReader"/></summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD. Can be null</param>
            Public Sub New(ByVal Reader As ExifIFDReader)
                MyBase.New(Reader)
            End Sub
            ''' <summary>Gets or sets value of specified record</summary>
            ''' <param name="Type">Recognized tagname of record that determines data type as well as number of components</param>
            ''' <value>New value for record. New value is assigned even if old value is of incompatible type. If value is null an item is deleted.</value>
            ''' <returns>Value of record with tag number specified by <paramref name="Type"/> if type for this tag number specifies that number of components can vary or if number of components match actual number of components in record. If there is no tag with specified number present in this IFD or number of components constraint is being violated null is returned.</returns>
            ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
            <CLSCompliant(False)> _
            Default Public Overridable Overloads Property Record(ByVal Type As Tags) As ExifRecord
                Get
                    Return MyBase.Record(Me.TagFormat(Type))
                End Get
                Set(ByVal value As ExifRecord)
                    MyBase.Record(Me.TagFormat(Type)) = value
                End Set
            End Property
        End Class
        ''' <summary>Exif GPS IFD</summary>
        Partial Class IFDGPS : Inherits SubIFD
            ''' <summary>CTor - empty IFD</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - reads content from <see cref="ExifIFDReader"/></summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD. Can be null</param>
            Public Sub New(ByVal Reader As ExifIFDReader)
                MyBase.New(Reader)
            End Sub
            ''' <summary>Gets or sets value of specified record</summary>
            ''' <param name="Type">Recognized tagname of record that determines data type as well as number of components</param>
            ''' <value>New value for record. New value is assigned even if old value is of incompatible type. If value is null an item is deleted.</value>
            ''' <returns>Value of record with tag number specified by <paramref name="Type"/> if type for this tag number specifies that number of components can vary or if number of components match actual number of components in record. If there is no tag with specified number present in this IFD or number of components constraint is being violated null is returned.</returns>
            ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
            <CLSCompliant(False)> _
            Default Public Overridable Overloads Property Record(ByVal Type As Tags) As ExifRecord
                Get
                    Return MyBase.Record(Me.TagFormat(Type))
                End Get
                Set(ByVal value As ExifRecord)
                    MyBase.Record(Me.TagFormat(Type)) = value
                End Set
            End Property
        End Class
        ''' <summary>Exif Interoperability IFD</summary>
        Partial Class IFDInterop : Inherits IFD
            ''' <summary>CTor - empty IFD</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - reads content from <see cref="ExifIFDReader"/></summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD. Can be null</param>
            Public Sub New(ByVal Reader As ExifIFDReader)
                MyBase.New(Reader)
            End Sub
            ''' <summary>Gets or sets value of specified record</summary>
            ''' <param name="Type">Recognized tagname of record that determines data type as well as number of components</param>
            ''' <value>New value for record. New value is assigned even if old value is of incompatible type. If value is null an item is deleted.</value>
            ''' <returns>Value of record with tag number specified by <paramref name="Type"/> if type for this tag number specifies that number of components can vary or if number of components match actual number of components in record. If there is no tag with specified number present in this IFD or number of components constraint is being violated null is returned.</returns>
            ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
            <CLSCompliant(False)> _
            Default Public Overridable Overloads Property Record(ByVal Type As Tags) As ExifRecord
                Get
                    Return MyBase.Record(Me.TagFormat(Type))
                End Get
                Set(ByVal value As ExifRecord)
                    MyBase.Record(Me.TagFormat(Type)) = value
                End Set
            End Property
        End Class
        ''' <summary>Represents any Exif Sub-IFD (an IFD embdeded somewhere in IFD block and pointed by some tag from another IFD)</summary>
        Public Class SubIFD : Inherits IFD
            ''' <summary>CTor - empty IFD</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - reads content from <see cref="ExifIFDReader"/></summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD. Can be null</param>
            Public Sub New(ByVal Reader As ExifIFDReader)
                MyBase.New(Reader)
            End Sub
            ''' <summary>Gets IFD this subIFD is nested within</summary>
            ''' <returns>IFD this subIFD is nested within or null when this subIFD have not been associated with parent IFD yet.</returns>
            ''' <value>Setter of this property is not bublicly accessible. Value of this property is set when subIFD is associted with parent IFD.</value>
            ''' <seelaso cref="ParentRecord"/>
            Public Property ParentIFD() As IFD
                <DebuggerStepThrough()> Get
                    Return _ParentIFD
                End Get
                Friend Set(ByVal value As IFD)
                    _ParentIFD = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="ParentIFD"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _ParentIFD As IFD
            ''' <summary>Contains value of the <see cref="ParentRecord"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _ParentRecord As UShort
            ''' <summary>Gets tag number of record this subIFD is pointed by</summary>
            ''' <returns>Tag number of record this subIFD is pointed by</returns>
            ''' <value>Setter of this property is not publicly accessible. Value of this property is set when subIFD is associated with parent IFD</value>
            ''' <remarks>This property is of type which is not CLS-compliant. Function <see cref="getParentRecord"/> returns value of this property in CLS-compliant type.
            ''' <para>When some Exif tag is referenced as parent record of some subIFD its value is meaningless - actually it is address of start of subIFD in Exif stream which can change after saving. Although you can change value of such tag, it has no effect. Value of this the tag is automatically computed when Exif is about to be saved.</para></remarks>
            ''' <seelaso cref="getParentRecord"/><seelaso cref="ParentIFD"/>
            <CLSCompliant(False)> _
            Public Property ParentRecord() As UShort
                <DebuggerStepThrough()> Get
                    Return _ParentRecord
                End Get
                Friend Set(ByVal value As UShort)
                    _ParentRecord = value
                End Set
            End Property
            ''' <summary>Returns value of the <see cref="ParentRecord"/> property in CLS-compliant type <see cref="Integer"/></summary>
            ''' <returns><see cref="ParentRecord"/></returns>
            ''' <remarks>This function is provided only for CLS-compliance reasons. You'd better use <see cref="ParentRecord"/> property.</remarks>
            ''' <seelaso cref="ParentRecord"/>
            <EditorBrowsable(EditorBrowsableState.Advanced)> _
            Public Function getParentRecord() As Integer
                Return ParentRecord
            End Function
            ''' <summary>Contains value of the <see cref="Desc"/> property</summary>
            Private _Desc As String
            ''' <summary>Descriptive name of this Sub IFD</summary>
            ''' <returns>Usually contain an empty string for non starndard Sub IFDs and comon English name for standard Sub IFDs. For non-standard Sub IFDs only when library have some ideda what can this Sub IFD mean this Sub IFD is captioned somehow</returns>
            ''' <remarks>Currently there are no Non Standard Sub IFDs that have any caption, Captions of standard Sub IFDs are public constants declared in <see cref="ExifReader"/></remarks>
            Public Property Desc() As String
                Get
                    Return _Desc
                End Get
                Friend Set(ByVal value As String)
                    _Desc = value
                End Set
            End Property
        End Class
#End Region
    End Class

    ''' <summary>Generic (not tags-aware read/write Exif implementation)</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(Exif), LastChange:="07/21/2008")> _
    Partial Public Class Exif
        ''' <summary>Do nothing CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor - loads data from <see cref="ExifReader"/></summary>
        ''' <param name="reader"><see cref="ExifReader"/> to load data from</param>
        Public Sub New(ByVal Reader As ExifReader)
            Me.New()
            If Reader Is Nothing Then Exit Sub
            Dim i As Integer = 0
            Dim LastIFD As IFD = Nothing
            For Each IFDReader As ExifIFDReader In Reader.IFDs
                If i = 0 Then
                    LastIFD = New IFDMain(IFDReader)
                    _IFD0 = LastIFD
                ElseIf i = 1 Then
                    LastIFD.Following = New IFDMain(IFDReader)
                    LastIFD = LastIFD.Following
                Else
                    LastIFD.Following = New IFD(IFDReader)
                    LastIFD = LastIFD.Following
                End If
                LastIFD.Exif = Me
                i += 1
            Next IFDReader
            '    _ExifSubIFD = New IFDExif(reader.ExifSubIFD)
            '    _InteropSubIFD = New IFDInterop(reader.ExifInteroperabilityIFD)
            '    _GPSSubIFD = New IFDGPS(reader.GPSSubIFD)
            '    For Each SubIFD As ExifReader.SubIFD In reader.OtherSubIFDs
            '        _AdditionalIFDs.Add(RetrieveParent(SubIFD, reader), New IFD(SubIFD))
            '    Next SubIFD
        End Sub
        ''' <summary>Contains value of the <see cref="IFD0"/> property</summary>
        Private _IFD0 As IFDMain
        ''' <summary>Gets or sets firts IFD of this instance (so-called Main IFD)</summary>
        ''' <returns>First IFD (so-called IFD0 or Main IFD) of current Exif metadata</returns>
        ''' <value>Sets firts IFD (so-called IFD0 or Main  IFD). It must be of type <see cref="IFDMain"/></value>
        ''' <exception cref="ArgumentException"><see cref="IFD.Exif"/> of value being set is non-null and is not current instance -or-
        ''' <see cref="IFD.Previous"/> of value being set is non-null. -or-
        ''' Value being set is already used somewhere else in this <see cref="Exif"/>.</exception>
        ''' <exception cref="TypeMismatchException">Value being set has <see cref="IFD.Following"/> set but it is not of type <see cref="IFDMain"/>.</exception>
        ''' <seelaso cref="ThumbnailIFD"/>
        Public Property IFD0() As IFDMain
            Get
                Return _IFD0
            End Get
            Set(ByVal value As IFDMain)
                If value.Exif IsNot Nothing AndAlso value.Exif IsNot Me Then _
                    Throw New ArgumentException(ResourcesT.Exceptions.IFDPassedToTheIFD0PropertyMustEitherHaveNoExifAsociatedOrMustHaveAssociatedCurrrentInstance)
                If value.Previous IsNot Nothing Then _
                    Throw New ArgumentException(ResourcesT.Exceptions.IFDPassedToTheIFD0PropertyCannotHaveThePreviousPropertySet)
                If Me.ContainsIFD(value) Then _
                    Throw New ArgumentException(ResourcesT.Exceptions.GivenIFDIsAlreadyInUse)
                If value.Following IsNot Nothing AndAlso Not TypeOf value.Following Is IFDMain Then _
                    Throw New TypeMismatchException(ResourcesT.Exceptions.TypeOfIFDFollowingAfterIFD0MustBeIFDMain, value.Following, GetType(IFDMain))
                _IFD0 = value
                value.Exif = Me
            End Set
        End Property
        ''' <summary>Gets or sets IFD1 - so called Thumbnail IFD</summary>
        ''' <returns>IFD1 if there is any; null otherwise</returns>
        ''' <value>Sets IFD1 - the <see cref="IFD.Following">following</see> IFD of <see cref="IFD0"/>. If <see cref="IFD0"/> is null it is set to an empty instance of <see cref="IFDMain"/></value>
        ''' <exception cref="ArgumentException">Value being set have <see cref="IFD0"/> as one of its <see cref="IFD.Following"/> IFDs =or= Value being set has non-null value of the <see cref="IFD.Previous"/> property. =or= Value being set has non-null <see cref="IFD.Exif"/> property which is different from current instance. =or= Value being set is already used as IFD at another position in this instance.</exception>
        ''' <seelaso cref="IFD0"/><seelaso cref="IFD.Following"/>
        Public Property ThumbnailIFD() As IFDMain
            Get
                If IFD0 IsNot Nothing Then Return IFD0.Following Else Return Nothing
            End Get
            Set(ByVal value As IFDMain)
                If IFD0 Is Nothing Then IFD0 = New IFDMain
                IFD0.Following = value
            End Set
        End Property
        ''' <summary>Gets all subIFDs and sub-subIFDs etc. present in this instance</summary>
        ''' <remarks>Collection of all subIFDs in this instance. This collection does not contain IFDs folowing (linked-list connected) to subIFDs, but contains any possible subIFDs linked from such subIFD-following IFD.</remarks>
        Public ReadOnly Property SubIFDs() As IEnumerable(Of SubIFD)
            Get
                Dim ret As IEnumerable(Of SubIFD) = New List(Of SubIFD)
                Dim Current As IFD = IFD0
                Dim CurrentStack As New Stack(Of IFD)
                While Current IsNot Nothing
                    ret = ret.union(Current.SubIFDs)
                    If Current.SubIFDs.Count > 0 Then
                        If Current.Following IsNot Nothing Then CurrentStack.Push(Current.Following)
                        For Each si In Current.SubIFDs
                            CurrentStack.Push(si)
                        Next
                        Current = CurrentStack.Pop
                    ElseIf Current.Following IsNot Nothing Then
                        Current = Current.Following
                    ElseIf CurrentStack.Count > 0 Then
                        Current = CurrentStack.Pop
                    Else
                        Current = Nothing
                    End If
                End While
                Return ret
            End Get
        End Property
        ''' <summary>Gets value indicationg if given <see cref="IFD"/> is used somewhere in this <see cref="Exif"/></summary>
        ''' <param name="IFD">Instance to look for</param>
        ''' <remarks>True if given instance is used somewhere in current instance</remarks>
        Protected Friend Function ContainsIFD(ByVal IFD As IFD) As Boolean
            Dim Current As IFD = Me.IFD0
            While Current IsNot Nothing
                If Current Is IFD Then Return True
                Current = Current.Following
            End While
            For Each SubIfd As SubIFD In SubIFDs
                Current = SubIfd
                While Current IsNot Nothing
                    If Current Is IFD Then Return True
                    Current = Current.Following
                End While
            Next SubIfd
            Return False
        End Function
    End Class
#End If
End Namespace
