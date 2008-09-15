Imports System.Linq
Imports RecordDic = Tools.CollectionsT.GenericT.DictionaryWithEvents(Of UShort, Tools.DrawingT.MetadataT.ExifT.ExifRecord)
Imports SubIFDDic = Tools.CollectionsT.GenericT.DictionaryWithEvents(Of UShort, Tools.DrawingT.MetadataT.ExifT.SubIFD)
Imports RecordList = Tools.CollectionsT.GenericT.ListWithEvents(Of Tools.DrawingT.MetadataT.ExifT.ExifRecord)
Imports SubIFDList = Tools.CollectionsT.GenericT.ListWithEvents(Of Tools.DrawingT.MetadataT.ExifT.SubIFD)
Imports Tools.ComponentModelT

Namespace DrawingT.MetadataT.ExifT
#If Config <= Nightly Then
    ''' <summary>Provides high-level acces to Exif metadata</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(Exif), LastChange:="07/21/2008")> _
    Public Class Exif
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
                    ret = ret.Union(Current.SubIFDs)
                    If Current.SubIFDs.Count > 0 Then
                        If Current.Following IsNot Nothing Then CurrentStack.Push(Current.Following)
                        For Each si In Current.SubIFDs
                            CurrentStack.Push(si.Value)
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

    ''' <summary>Describes one Exif record</summary>
    ''' <remarks>Descibes which data type record actually contains, how many items of such datatype. For recognized tags also possible format is specified via <see cref="ExifTagFormat"/></remarks>
    <CLSCompliant(False)> _
    Public Class ExifRecordDescription
        ''' <summary>Contains value of the <see cref="DataType"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _DataType As ExifDataTypes
        ''' <summary>Contains value of the <see cref="NumberOfElements"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _NumberOfElements As UShort
        ''' <summary>Number of elements of type <see cref="DataType"/> contained in record</summary>
        ''' <remarks>Note for inheritors: Do not expose setter of this property. Do not change value of this property during live-time of instance. This restriction is here because <see cref="ExifRecord"/> cannot track changes of this property.</remarks>
        Public Property NumberOfElements() As UShort
            Get
                Return _NumberOfElements
            End Get
            Protected Friend Set(ByVal value As UShort)
                _NumberOfElements = value
            End Set
        End Property
        ''' <summary>Data type of items in record</summary>
        ''' <remarks>Note for inheritors: Do not expose setter of this property. Do not change value of this property during live-time of instance. This restriction is here because <see cref="ExifRecord"/> cannot track changes of this property.</remarks>
        Public Property DataType() As ExifDataTypes
            Get
                Return _DataType
            End Get
            Protected Set(ByVal value As ExifDataTypes)
                _DataType = value
            End Set
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="DataType">Data type of record</param>
        ''' <param name="NumberOfElements">Number of elements of type <paramref name="DataType"/> in record.</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="NumberOfElements"/> is 0</exception>
        Public Sub New(ByVal DataType As ExifDataTypes, ByVal NumberOfElements As UShort)
            If NumberOfElements = 0 Then Throw New ArgumentOutOfRangeException("NumberOfElements", ResourcesT.Exceptions.NumberOfElementsCannotBe0)
            Me.DataType = DataType
            Me.NumberOfElements = NumberOfElements
        End Sub
        ''' <summary>Protected CTor that allows <see cref="NumberOfElements"/> to be zero</summary>
        ''' <param name="NumberOfElements">Number of elements of type <paramref name="DataType"/> in record.</param>
        ''' <param name="DataType"></param>
        Protected Sub New(ByVal NumberOfElements As UShort, ByVal DataType As ExifDataTypes)
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
        Public Sub New(ByVal NumberOfElements As UShort, ByVal Tag As UShort, ByVal Name As String, ByVal ParamArray DataTypes As ExifDataTypes())
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
        Private Shared Function TestThrowReturn(ByVal DataTypes As ExifDataTypes()) As ExifDataTypes()
            If DataTypes Is Nothing OrElse DataTypes.Length = 0 Then Throw New ArgumentNullException("DataTypes", ResourcesT.Exceptions.DataTypesCannotBeNullAndMustContainAtLeastOneElement)
            Return DataTypes
        End Function
        ''' <summary>Contains value of the <see cref="Tag"/> property</summary>
        Private _Tag As UShort
        ''' <summary>Contains value of the <see cref="Name"/> property</summary>
        Private _Name As String
        ''' <summary>Contains list of possible datatypes for tag excepting datatype specified in <see cref="DataType"/></summary>
        Private OtherDatatypes As New List(Of ExifDataTypes)
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
        Public ReadOnly Property DataTypes() As ExifDataTypes()
            Get
                Dim arr(OtherDatatypes.Count) As ExifDataTypes
                If OtherDatatypes.Count > 0 Then _
                    OtherDatatypes.CopyTo(0, arr, 1, OtherDatatypes.Count)
                arr(0) = DataType
                Return arr
            End Get
        End Property
    End Class

    ''' <summary>Represents one Exif record</summary>
    Public Class ExifRecord
        Implements IReportsChange
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
                    Case ExifDataTypes.ASCII
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
                    Case ExifDataTypes.Byte
                        SetDataValue(Of Byte)(value)
                    Case ExifDataTypes.Double
                        SetDataValue(Of Double)(value)
                    Case ExifDataTypes.Int16
                        SetDataValue(Of Int16)(value)
                    Case ExifDataTypes.Int32
                        SetDataValue(Of Int32)(value)
                    Case ExifDataTypes.NA
                        SetDataValue(Of Byte)(value)
                    Case ExifDataTypes.SByte
                        SetDataValue(Of SByte)(value)
                    Case ExifDataTypes.Single
                        SetDataValue(Of Single)(value)
                    Case ExifDataTypes.SRational
                        SetDataValue(Of SRational)(value)
                    Case ExifDataTypes.UInt16
                        SetDataValue(Of UInt16)(value)
                    Case ExifDataTypes.UInt32
                        SetDataValue(Of UInt32)(value)
                    Case ExifDataTypes.URational
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
            Dim old As Object = _Data
            Dim changed As Boolean = False
            If Not IsArray(value) Then
                Try
                    Dim newV As T = CType(value, T)
                    If Me.DataType.NumberOfElements = 1 OrElse Not Me.Fixed Then
                        _Data = newV
                        Me.DataType.NumberOfElements = 1
                        changed = True
                    Else
                        Throw New ArgumentException(ResourcesT.Exceptions.CannotChangeNumberOfComponentsOfThisRecord)
                    End If
                Catch ex As Exception
                    Throw New InvalidCastException(ResourcesT.Exceptions.ValueOfIncompatibleTypePassedToExifRecord)
                End Try
            Else
                'Catch
                Try
                    Dim newV As T() = CType(value, T())
                    If Me.DataType.NumberOfElements = newV.Length OrElse Not Fixed Then
                        _Data = newV
                        Me.DataType.NumberOfElements = newV.Length
                        changed = True
                    Else
                        Throw New ArgumentException(ResourcesT.Exceptions.CannotChangeNumberOfComponentsOfThisRecord)
                    End If
                Catch ex As Exception
                    Throw New InvalidCastException(ResourcesT.Exceptions.ValueOfIncompatibleTypePassedToExifRecord)
                End Try
                'End Try
            End If
            If changed Then
                OnChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(old, _Data, "Data"))
            End If
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
         Public Sub New(ByVal Data As Object, ByVal Type As ExifDataTypes, Optional ByVal NumberOfComponents As UShort = 1, Optional ByVal Fixed As Boolean = False)
            Me.New(New ExifRecordDescription(Type, NumberOfComponents), Data, Fixed)
        End Sub
        ''' <summary>Raises the <see cref="Changed"/> event</summary>
        ''' <param name="e">Event argument. Should be <see cref="IReportsChange.ValueChangedEventArgsBase"/>.</param>
        ''' <remarks>Changes of properties of <see cref="DataType"/> are not tracked.</remarks>
        Protected Overridable Sub OnChanged(ByVal e As EventArgs)
            RaiseEvent Changed(Me, e)
        End Sub
        ''' <summary>Raised when value of member changes</summary>
        ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code (e.g. use <see cref="ireportschange.ValueChangedEventArgs(Of T)"/> class)
        ''' <para>Changes of properties of <see cref="DataType"/> are not tracked.</para></remarks>
        Public Event Changed As IReportsChange.ChangedEventHandler Implements IReportsChange.Changed
    End Class
#End If
End Namespace
