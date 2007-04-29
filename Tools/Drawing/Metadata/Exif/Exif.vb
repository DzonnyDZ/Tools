Namespace Drawing.Metadata
#If Config <= Nightly Then
    ''' <summary>Provides read-write acces to block of Exif data</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(Exif), LastChange:="2007/04/29")> _
    Partial Public Class Exif 'ASAP:Wiki
#Region "CTors"
        ''' <summary>CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor - loads data from <see cref="ExifReader"/></summary>
        ''' <param name="reader"><see cref="ExifReader"/> to load data from</param>
        ''' <exception cref="ArgumentNullException">Unable to find predecessor of any of <see cref="ExifReader.OtherSubIFDs"/></exception>
        Public Sub New(ByVal reader As ExifReader)
            Dim i As Integer = 0
            For Each IFD As ExifIFDReader In reader.IFDs
                If i = 0 Then
                    _MainIFDs.Add(New IFDMain(IFD))
                ElseIf i = 1 Then
                    _MainIFDs.Add(New IFDMain(IFD))
                Else
                    _MainIFDs.Add(New IFD(IFD))
                End If
                i += 1
            Next IFD
            _ExifSubIFD = New IFDExif(reader.ExifSubIFD)
            _InteropSubIFD = New IFDInterop(reader.ExifInteroperabilityIFD)
            _GPSSubIFD = New IFDGPS(reader.GPSSubIFD)
            For Each SubIFD As ExifReader.SubIFD In reader.OtherSubIFDs
                _AdditionalIFDs.Add(RetrieveParent(SubIFD, reader), New IFD(SubIFD))
            Next SubIFD
        End Sub
        ''' <summary>Tryes to determine IFD that precedes passed IFD in line</summary>
        ''' <param name="SubIfd">IFD to find predecessor for</param>
        ''' <param name="Reader"><see cref="ExifReader"/> used to resolve IFDs</param>
        ''' <returns>Predecessof of <paramref name="SubIfd"/> if found or null</returns>
        Private Function RetrieveParent(ByVal SubIfd As ExifReader.SubIFD, ByVal Reader As ExifReader) As IFD
            If SubIfd.PreviousSubIFD Is Reader.ExifSubIFD Then
                Return _ExifSubIFD
            ElseIf SubIfd.PreviousSubIFD Is Reader.ExifInteroperabilityIFD Then
                Return _InteropSubIFD
            ElseIf SubIfd.PreviousSubIFD Is Reader.GPSSubIFD Then
                Return _GPSSubIFD
            Else
                For Each Srch As ExifReader.SubIFD In Reader.OtherSubIFDs
                    If Srch Is SubIfd.PreviousSubIFD Then
                        Return RetrieveParent(Srch, Reader)
                    End If
                Next Srch
                Return Nothing
            End If
        End Function
#End Region
#Region "IFD propertires"
        ''' <summary>Contains value of the <see cref="AdditionalIFDs"/> property</summary>
        Private _AdditionalIFDs As New Dictionary(Of IFD, IFD)
        ''' <summary>Contains value of the <see cref="MainIFDs"/> property</summary>
        Private _MainIFDs As New List(Of IFD)
        ''' <summary>Contains value of the <see cref="ExifSubIFD"/> property</summary>
        Private _ExifSubIFD As IFDExif
        ''' <summary>Contains value of the <see cref="InteropSubIFD"/> property</summary>
        Private _InteropSubIFD As IFDInterop
        ''' <summary>Contains value of the <see cref="GPSSubIFD"/> property</summary>
        Private _GPSSubIFD As IFDGPS
        ''' <summary>Returns GPS Sub IFD, if there is no GPS Sub IFD then an enmty is created</summary>
        Public ReadOnly Property GPSSubIFD() As IFDGPS
            Get
                If _GPSSubIFD Is Nothing Then _GPSSubIFD = New IFDGPS()
                Return _GPSSubIFD
            End Get
        End Property
        ''' <summary>Returns Exif Sub IFD, if there is no Exif Sub IFD then an empty is created</summary>
        Public ReadOnly Property ExifSubIFD() As IFDExif
            Get
                If _ExifSubIFD Is Nothing Then _ExifSubIFD = New IFDExif
                Return _ExifSubIFD
            End Get
        End Property
        ''' <summary>Returns Exif Interoperability Sub IFD, if thre is no Interop Sub IFD then an empty is created</summary>
        Public ReadOnly Property InteropSubIFD() As IFDInterop
            Get
                If _InteropSubIFD Is Nothing Then _InteropSubIFD = New IFDInterop
                Return _InteropSubIFD
            End Get
        End Property
        ''' <summary>Returns main IFD with given index. If IFD0 or IFD1 is missing then an empty is created</summary>
        ''' <param name="index">Index of IFD to retrieve. Standard values are 0 and 1</param>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' <paramref name="index"/> is less than zero -or-
        ''' <paramref name="index"/> is greater than 1 and main IFD with such index does not exist
        ''' </exception>
        Public ReadOnly Property MainIFDs(ByVal index As Integer) As IFD
            Get
                If index < 0 Then Throw (New ArgumentOutOfRangeException("Index must be greater than or equal to zero"))
                If index <= 1 AndAlso _MainIFDs.Count = 0 Then _MainIFDs.Add(New IFDMain)
                If index = 1 AndAlso _MainIFDs.Count = 1 Then _MainIFDs.Add(New IFDMain)
                If index >= _MainIFDs.Count Then Throw New ArgumentException("Index must be in range defined by counf of IFDs")
                Return _MainIFDs(index)
            End Get
        End Property
        ''' <summary>Returns main Exif IFD, if there is no IFD0 an empty is created</summary>
        Public ReadOnly Property MainIFD() As IFDMain
            Get
                Return MainIFDs(0)
            End Get
        End Property
        ''' <summary>Returns thumbnail IFD, if there is no IFD1 an empty is created</summary>
        Public ReadOnly Property ThumbnailIFD() As IFDMain
            Get
                Return MainIFDs(1)
            End Get
        End Property
        ''' <summary>Cound of main IFDs currently present</summary>
        ''' <returns>Determines possible range of the index parameter of the <see cref="MainIFDs"/> property, but 0 and 1 are always valid values for index even when value of this property is 0 or 1</returns>
        Public ReadOnly Property MainIFDsCount() As Integer
            Get
                Return _MainIFDs.Count
            End Get
        End Property
        ''' <summary>List of additional IFDs retrieved from stream when initializing</summary>
        Public ReadOnly Property AdditionalIFDs() As Dictionary(Of IFD, IFD).ValueCollection
            Get
                Return _AdditionalIFDs.Values
            End Get
        End Property
#End Region
        ''' <summary>Provides read-write access to Image File Directory of Exif data</summary>
        Public Class IFD
            ''' <summary>CTor - empty IFD</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - reads content from <see cref="ExifIFDReader"/></summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD. Can be null</param>
            Public Sub New(ByVal Reader As ExifIFDReader)
                If Reader Is Nothing Then Exit Sub
                For Each rec As ExifIFDReader.DirectoryEntry In Reader.Entries
                    Records.Add(rec.Tag, New ExifRecord(rec.Data, rec.DataType, rec.Components, rec.DataType <> ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII AndAlso rec.DataType <> ExifIFDReader.DirectoryEntry.ExifDataTypes.Byte AndAlso rec.DataType <> ExifIFDReader.DirectoryEntry.ExifDataTypes.NA))
                Next rec
            End Sub
            ''' <summary>Contains value of the <see cref="Records"/> property</summary>
            Private _Records As New Dictionary(Of UShort, ExifRecord)
            ''' <summary>Records in this Image File Directory</summary>
            <CLSCompliant(False)> _
            Public ReadOnly Property Records() As Dictionary(Of UShort, ExifRecord)
                Get
                    Return _Records
                End Get
            End Property
        End Class
        ''' <summary>Describes one Exif record</summary>
        ''' <remarks>Descibes which data type record actually contains, how many items of such datatype</remarks>
        <CLSCompliant(False)> _
        Public Class ExifRecordDescription
            ''' <summary>Contains value of the <see cref="DataType"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _DataType As ExifIFDReader.DirectoryEntry.ExifDataTypes
            ''' <summary>Contains value of the <see cref="NumberOfElements"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _NumberOfElements As UShort
            ''' <summary>Number of elements of type <see cref="DataType"/> contained in record</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Setting value to 0.</exception>
            Public Property NumberOfElements() As UShort
                Get
                    Return _NumberOfElements
                End Get
                Protected Friend Set(ByVal value As UShort)
                    If value = 0 Then Throw New ArgumentOutOfRangeException("value", "Number of elements cannot be 0.")
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
                If NumberOfElements = 0 Then Throw New ArgumentOutOfRangeException("NumberOfElements", "Number of elements cannot be 0")
                Me.DataType = DataType
                Me.NumberOfElements = NumberOfElements
            End Sub
        End Class

        ''' <summary>Represents one Exif record</summary>
        Public Class ExifRecord
            ''' <summary>Contains value of the <see cref="Data"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Data As Object
            ''' <summary>Contains value of the <see cref="DataType"/> property</summary>
            Private _DataType As ExifRecordDescription
            ''' <summary>Contains value of the <see cref="ConstantNumberOfComponents"/> property</summary>
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
                    If value Is Nothing Then Throw New ArgumentNullException("Value cannot be null")
                    Select Case DataType.DataType
                        Case ExifIFDReader.DirectoryEntry.ExifDataTypes.ASCII
                            If TryCast(value, String) IsNot Nothing Then
                                Dim newV As String = System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(CStr(value)))
                                If Me.DataType.NumberOfElements = newV.Length OrElse Not Fixed Then
                                    _Data = newV
                                    Me.DataType.NumberOfElements = CStr(_Data).Length
                                Else
                                    Throw New ArgumentException("Cannot change number of components of this record")
                                End If
                            Else
                                Throw New InvalidCastException("Value of incompatible type passed to ASII record")
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
                        Throw New ArgumentException("Cannot change number of components of this record")
                    End If
                Catch
                    Try
                        Dim newV As T() = CType(value, T())
                        If Me.DataType.NumberOfElements = newV.Length OrElse Not Fixed Then
                            _Data = newV
                            Me.DataType.NumberOfElements = newV.Length
                        Else
                            Throw New ArgumentException("Cannot change number of components of this record")
                        End If
                    Catch ex As Exception
                        Throw New InvalidCastException("Value of incompatible type passed to Exif record")
                    End Try
                End Try
            End Sub
            ''' <summary>CTor</summary>
            ''' <param name="Data">Initila value of this record</param>
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
        End Class
        ''' <summary>Exif Sub IFD</summary>
        Partial Class IFDExif : Inherits IFD
            ''' <summary>CTor - empty IFD</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - reads content from <see cref="ExifIFDReader"/></summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD. Can be null</param>
            Public Sub New(ByVal Reader As ExifIFDReader)
                MyBase.New(Reader)
            End Sub
        End Class
        ''' <summary>Exif GPS IFD</summary>
        Partial Class IFDGPS : Inherits IFD
            ''' <summary>CTor - empty IFD</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - reads content from <see cref="ExifIFDReader"/></summary>
            ''' <param name="Reader"><see cref="ExifIFDReader"/> that has read data of this IFD. Can be null</param>
            Public Sub New(ByVal Reader As ExifIFDReader)
                MyBase.New(Reader)
            End Sub
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
        End Class
#End Region
    End Class
#End If
End Namespace
