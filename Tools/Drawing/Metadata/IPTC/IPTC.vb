Imports Tools.CollectionsT.GenericT
Imports Tools.DataStructuresT.GenericT
Namespace DrawingT.MetadataT
#If Congig <= Nightly Then 'Stage: Nightly
    'ASAP:Wiki, Forum, Mark
    Partial Public Class IPTC
        ''' <summary>Do nothing CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>Contains value of the <see cref="Tags"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Tags As New List(Of KeyValuePair(Of DataSetIdentification, Byte()))

        ''' <summary>CTor from <see cref="IPTCReader"/></summary>
        ''' <param name="Reader"><see cref="IPTCReader"/> to read all tags from</param>
        Public Sub New(ByVal Reader As IPTCReader)
            For Each t As IPTCReader.IPTCRecord In Reader.Records
                Tags.Add(New KeyValuePair(Of DataSetIdentification, Byte())(New DataSetIdentification(t.RecordNumber, t.Tag), t.Data))
            Next t
        End Sub
        ''' <summary>Removes all occurences of specified tag</summary>         
        ''' <param name="Key">Tag to remove</param>                            
        Public Sub Clear(ByVal Key As DataSetIdentification)
            Tags.RemoveAll(DataSetIdentification.PairMatch.GetPredicate(Of Byte())(Key))
        End Sub
        ''' <summary>Removes all tags</summary>
        Public Sub Clear()
            Tags.Clear()
        End Sub
        ''' <summary>Gets count of tags with specified key</summary>
        ''' <param name="Key">DataSet identification to count tags with</param>
        Public Function Contains(ByVal Key As DataSetIdentification) As Integer
            Return Tags.FindAll(DataSetIdentification.PairMatch.GetPredicate(Of Byte())(Key)).Count
        End Function
        ''' <summary>Gets or sets values associated with particular tag</summary>
        ''' <param name="Key">Tag identification</param>
        ''' <remarks>This property does no checks if tag <paramref name="Key"/> is repeatable or not and does not checks structure of byte arrays that represents values of tags, so you can totally corrupt structure if some fields. Also tag grouping is not checked. You should use this property very carefully or you can damage internal structure of IPTC data</remarks>
        ''' <value>New values for particular tag. Values of tags are replaced with new values. If there was more tags with same <paramref name="Key"/> than is being set then the next tags are removed. If there was less tags with same <paramref name="Key"/> necessary items are added at the end of the stream</value>
        ''' <returns>List of values of tag or null if tag is missing</returns>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Default Protected Property Tag(ByVal Key As DataSetIdentification) As List(Of Byte())
            Get
                Dim ret As List(Of KeyValuePair(Of DataSetIdentification, Byte())) = Tags.FindAll(DataSetIdentification.PairMatch.GetPredicate(Of Byte())(Key))
                If ret IsNot Nothing AndAlso ret.Count > 0 Then
                    Dim ret2 As New List(Of Byte())
                    For Each Item As KeyValuePair(Of DataSetIdentification, Byte()) In ret
                        ret2.Add(Item.Value)
                    Next Item
                    Return ret2
                Else
                    Return Nothing
                End If
            End Get
            Set(ByVal value As List(Of Byte()))
                Dim Already As IReadOnlyList(Of Integer) = New DataSetIdentification.PairMatch(DataSetIdentification.Keywords).GetIndices(Tags)
                Dim i As Integer = 0
                'Replace values of existing tags with same key
                For Each Item As Byte() In value
                    If i < Already.Count Then
                        Tags(Already(i)) = New KeyValuePair(Of DataSetIdentification, Byte())(Tags(Already(i)).Key, value(i))
                        i += 1
                    Else
                        Exit For
                    End If
                Next Item
                If i < Already.Count Then
                    'Remove tags (if value is shorter than Already)
                    For j As Integer = Already.Count - 1 To i Step -1
                        Tags.RemoveAt(j)
                    Next j
                Else
                    'Add tags (if value is longer than Already)
                    For j As Integer = i To value.Count - 1
                        Tags.Add(New KeyValuePair(Of DataSetIdentification, Byte())(Key, value(j)))
                    Next j
                End If
            End Set
        End Property
        ''' <summary>All tags and their values in IPTC stream</summary>
        Protected ReadOnly Property Tags() As List(Of KeyValuePair(Of DataSetIdentification, Byte()))
            Get
                Return _Tags
            End Get
        End Property
        ''' <summary>Identifies IPTC tag (DataSet). Used for indexing.</summary>
        ''' <completionlist cref="DataSetIdentification"/>
        Partial Public Structure DataSetIdentification : Implements IPair(Of RecordNumbers, Byte), IEquatable(Of IPair(Of RecordNumbers, Byte))
            ''' <summary>Contains value of the <see cref="RecordNumber"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _RecordNumber As RecordNumbers
            ''' <summary>Contains value of the <see cref="DatasetNumber"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _DataSetNumber As Byte
            ''' <summary>Record (tag group) number</summary>
            ''' <exception cref="ArgumentOutOfRangeException">Value being set is greater than 9</exception>
            Public Property RecordNumber() As RecordNumbers Implements DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte).Value1
                Get
                    Return _RecordNumber
                End Get
                Set(ByVal value As RecordNumbers)
                    If value > 9 Then Throw New ArgumentOutOfRangeException("DataSetNumber must be less than or equal to 9")
                    _RecordNumber = value
                End Set
            End Property
            ''' <summary>DataSet (tag) number</summary>
            Public Property DatasetNumber() As Byte Implements DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte).Value2
                Get
                    Return _DataSetNumber
                End Get
                Set(ByVal value As Byte)
                    _DataSetNumber = value
                End Set
            End Property
            ''' <summary>Copy CTor</summary>
            ''' <param name="From">Instance to be cloned</param>
            Public Sub New(ByVal From As IPair(Of RecordNumbers, Byte))
                Me.RecordNumber = From.Value1
                Me.DatasetNumber = From.Value2
            End Sub
            ''' <summary>CTor</summary>
            ''' <param name="RecordNumber">Number of record (tag group)</param>
            ''' <param name="DataSetNumber">Number of dataset (tag)</param>
            Public Sub New(ByVal RecordNumber As RecordNumbers, ByVal DataSetNumber As Byte)
                Me.RecordNumber = RecordNumber
                Me.DatasetNumber = DataSetNumber
            End Sub
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            <Obsolete("Use type-safe Clone instead")> _
            Private Function Clone1() As Object Implements System.ICloneable.Clone
                Return Clone()
            End Function
            ''' <summary>Swaps values <see cref="IPair(Of RecordNumbers, Byte).Value1"/> and <see cref="IPair(Of RecordNumbers, Byte).Value2"/></summary>
            Private Function Swap() As DataStructuresT.GenericT.IPair(Of Byte, RecordNumbers) Implements IPair(Of RecordNumbers, Byte).Swap
                Return New Pair(Of Byte, RecordNumbers)(DatasetNumber, RecordNumber)
            End Function
            ''' <summary>Creates a new object that is a copy of the current instance.</summary>
            ''' <returns>A new object that is a copy of this instance</returns>
            Public Function Clone() As DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte) Implements ICloneable(Of DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte)).Clone
                Return New DataSetIdentification(Me)
            End Function
            ''' <summary>Compares two <see cref="DataSetIdentification"/>s</summary>
            ''' <param name="a">A <see cref="DataSetIdentification"/> to be compared</param>
            ''' <param name="b">A <see cref="DataSetIdentification"/> to be compared</param>
            ''' <returns>True when <see cref="DatasetNumber"/> and <see cref="RecordNumber"/> equals</returns>
            Public Shared Operator =(ByVal a As DataSetIdentification, ByVal b As DataSetIdentification) As Boolean
                Return a.DatasetNumber = b.DatasetNumber AndAlso a.RecordNumber = b.RecordNumber
            End Operator
            ''' <summary>Compares two <see cref="DataSetIdentification"/>s</summary>
            ''' <param name="a">A <see cref="DataSetIdentification"/> to be compared</param>
            ''' <param name="b">A <see cref="DataSetIdentification"/> to be compared</param>
            ''' <returns>False when <see cref="DatasetNumber"/> and <see cref="RecordNumber"/> equals</returns>
            Public Shared Operator <>(ByVal a As DataSetIdentification, ByVal b As DataSetIdentification) As Boolean
                Return Not a = b
            End Operator
            ''' <summary>Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="System.Object"/>.</summary>
            ''' <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="System.Object"/>.</param>
            ''' <returns>true if the specified <see cref="System.Object"/> is equal to the current <see cref="System.Object"/>; otherwise, false</returns>
            ''' <remarks>Obsolete: Use type safe overload instead</remarks>
            <Obsolete("Use type safe overload instead")> _
            Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
                If TypeOf obj Is IPair(Of RecordNumbers, Byte) Then
                    Return Equals(DirectCast(obj, IPair(Of RecordNumbers, Byte)))
                Else
                    Return False
                End If
            End Function
            ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
            ''' <param name="other">An object to compare with this object.</param>
            ''' <returns>True if <see cref="DatasetNumber"/> and <see cref="RecordNumber"/> of this instance and <paramref name="other"/> equals</returns>
            Public Overloads Function Equals(ByVal other As DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte)) As Boolean Implements System.IEquatable(Of DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte)).Equals
                Return other = Me
            End Function
            ''' <summary>Serves as a hash function for a particular type. <see cref="System.Object.GetHashCode"/> is suitable for use in hashing algorithms and data structures like a hash table.</summary>
            ''' <returns>A hash code for the current System.Object</returns>
            Public Overrides Function GetHashCode() As Integer
                Return Me.DatasetNumber * 256 + Me.RecordNumber
            End Function
            ''' <summary>Gives acctess to <see cref="System.Predicate"/> that matches <see cref="KeyValuePair(Of DataSetIdentification, T)"/> which's <see cref="KeyValuePair.Key"/> is same as given <see cref="DataSetIdentification"/></summary>
            Public Class PairMatch
                ''' <summary><see cref="DataSetIdentification"/> to compare <see cref="KeyValuePair.Key"/> with</summary>
                Public ReadOnly Match As DataSetIdentification
                ''' <summary>CTor</summary>
                ''' <param name="Match"><see cref="DataSetIdentification"/> to compare <see cref="KeyValuePair.Key"/> with</param>
                Public Sub New(ByVal Match As DataSetIdentification)
                    Me.Match = Match
                End Sub
                ''' <summary>Function which's delegate can be passed for example to <see cref="List.FindAll"/></summary>
                ''' <param name="Pair">Item to match with <see cref="Match"/></param>
                ''' <typeparam name="T">Type of value stored in <see cref="KeyValuePair"/></typeparam>
                Public Function Predicate(Of T)(ByVal Pair As KeyValuePair(Of DataSetIdentification, T)) As Boolean
                    Return Pair.Key = Match
                End Function
                ''' <summary>Returns delegate of <see cref="PairMatch.Predicate"/> of newly created instance of <see cref="PairMatch"/></summary>
                ''' <param name="Match">Key to compare with</param>
                ''' <returns>Delegate of <see cref="PairMatch.Predicate"/></returns>
                ''' <typeparam name="T">Type of value of <see cref="KeyValuePair"/> that can be passed to returned <see cref="System.Predicate"/></typeparam>
                Public Shared Function GetPredicate(Of T)(ByVal Match As DataSetIdentification) As System.Predicate(Of KeyValuePair(Of DataSetIdentification, T))
                    Return AddressOf New PairMatch(Match).Predicate(Of T)
                End Function
                ''' <summary>Gets indices of items in given <see cref="IEnumerable(Of KeyValuePair(Of DataSetIdentification, T))"/> which's <see cref="KeyValuePair.Key"/> matches <see cref="Match"/></summary>
                ''' <param name="List">List to search within</param>
                ''' <returns>List of indices of items which's <see cref="KeyValuePair.Key"/> matches <see cref="Match"/></returns>
                ''' <typeparam name="T">Type of value of <see cref="KeyValuePair(Of DataSetIdentification, T)"/></typeparam>
                Public Function GetIndices(Of T)(ByVal List As IEnumerable(Of KeyValuePair(Of DataSetIdentification, T))) As IReadOnlyList(Of Integer)
                    Dim i As Integer = 0
                    Dim ret As New List(Of Integer)
                    For Each Item As KeyValuePair(Of DataSetIdentification, T) In List
                        If Item.Key = Match Then ret.Add(i)
                        i += 1
                    Next Item
                    Return New ReadOnlyListAdapter(Of Integer)(ret)
                End Function
            End Class
        End Structure

        ''' <summary>Returns value indicating if givel value if member of enumeration</summary>
        ''' <param name="value">Value to be checked</param>
        ''' <returns>True if <paramref name="value"/> is member of <paramref name="T"/></returns>
        ''' <typeparam name="T">Type of enumeration to be searched</typeparam>
        ''' <exception cref="ArgumentException"><paramref name="T"/> is not <see cref="[Enum]"/></exception>
        Private Shared Function IsEnumMember(Of T As {IComparable, Structure})(ByVal value As T) As Boolean
            'TODO:Extract as separate tool
            Return Array.IndexOf([Enum].GetValues(GetType(T)), value) >= 0
        End Function
        ''' <summary>Describes IPTC dataset's (tag) properties</summary>
        Public Class IPTCTag
            ''' <summary>CTor</summary>
            ''' <param name="Number">Tag (dataset) number</param>
            ''' <param name="Record">Group (record) number</param>
            ''' <param name="Name">Name of tag used in object structure</param>
            ''' <param name="HumanName">Human-friendly name of tga</param>
            ''' <param name="Type">Type of tag data</param>
            ''' <param name="Mandatory">mandatority according to IPTC standard</param>
            ''' <param name="Repeatable">Repeatability</param>
            ''' <param name="Length">Maximal length of taga data</param>
            ''' <param name="Fixed">True <paramref name="Length"/> to be only lenght allowed</param>
            ''' <param name="Category">Category of tag</param>
            ''' <param name="Description">Description of tag</param>
            ''' <param name="Enum">Type of enumeration if tag is enumeration</param>
            ''' <param name="Lock">Lock properties after construction</param>
            ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Record"/> is greater than 9 -or- <paramref name="Length"/> is less than zero</exception>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Type"/> is not member of <see cref="IPTCTypes"/></exception>
            Public Sub New( _
                    ByVal Number As Byte, _
                    ByVal Record As RecordNumbers, _
                    ByVal Name As String, _
                    ByVal HumanName As String, _
                    ByVal Type As IPTCTypes, _
                    ByVal Mandatory As Boolean, _
                    ByVal Repeatable As Boolean, _
                    ByVal Length As Short, _
                    ByVal Fixed As Boolean, _
                    ByVal Category As String, _
                    ByVal Description As String, _
                    Optional ByVal [Enum] As Type = Nothing, _
                    Optional ByVal Lock As Boolean = False _
            )
                Me.Number = Number
                Me.Record = Record
                Me.Name = Name
                Me.HumanName = HumanName
                Me.Type = Type
                Me.Mandatory = Mandatory
                Me.Repeatable = Repeatable
                If Fixed Then
                    Me.Length = Length
                    Me.Fixed = Fixed
                Else
                    Me.Fixed = Fixed
                    Me.Length = Length
                End If
                Me.Category = Category
                Me.Description = Description
                Me.Enum = [Enum]
                Me._Locked = Lock
            End Sub
            ''' <summary>Contains value of the <see cref="Locked"/> property</summary>
            Private _Locked As Boolean
            ''' <summary>Indicates if this instance is locked, so nothing can be changed</summary>
            <EditorBrowsable(EditorBrowsableState.Advanced)> _
            Public ReadOnly Property Locked() As Boolean
                Get
                    Return _Locked
                End Get
            End Property
            ''' <summary>Gets or sets record and dataset number</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            Public Property Identification() As DataSetIdentification
                Get
                    Return New DataSetIdentification(Me.Record, Me.Number)
                End Get
                Set(ByVal value As DataSetIdentification)
                    Me.Record = value.RecordNumber
                    Me.Number = value.DatasetNumber
                End Set
            End Property

            ''' <summary>Contains value of the <see cref="Number"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Number As Byte
            ''' <summary>Number of tag (dataset)</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            Public Property Number() As Byte
                Get
                    Return _Number
                End Get
                Set(ByVal value As Byte)
                    If Locked And value <> Number Then Throw New InvalidOperationException("This instance is locked")
                    _Number = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Record"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Record As RecordNumbers
            ''' <summary>Tag's number of record (group number)</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            ''' <exception cref="ArgumentOutOfRangeException">Value being set is greater than 9</exception>
            Public Property Record() As RecordNumbers
                Get
                    Return _Record
                End Get
                Set(ByVal value As RecordNumbers)
                    If Locked And value <> Record Then Throw New InvalidOperationException("This instance is locked")
                    If value < 0 OrElse value > 9 Then Throw New ArgumentOutOfRangeException("Record must be from 0 to 9")
                    _Record = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Name"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Name As String
            ''' <summary>Tag name as used in object structure</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            Public Property Name() As String
                Get
                    Return _Name
                End Get
                Set(ByVal value As String)
                    If Locked And value <> Name Then Throw New InvalidOperationException("This instance is locked")
                    _Name = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="HumanName"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _HumanName As String
            ''' <summary>Human-friendly name of tag</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            Public Property HumanName() As String
                Get
                    Return _HumanName
                End Get
                Set(ByVal value As String)
                    If Locked And value <> HumanName Then Throw New InvalidOperationException("This instance is locked")
                    _HumanName = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Type"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Type As IPTCTypes
            ''' <summary>Type of tag</summary>
            ''' <remarks>You can get type of value of tag by <see cref="GetUnderlyingType"/> function or <see cref="UnderlyingType"/> property</remarks>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="IPTCTypes"/></exception>
            Public Property Type() As IPTCTypes
                Get
                    Return _Type
                End Get
                Set(ByVal value As IPTCTypes)
                    If Locked And value <> Type Then Throw New InvalidOperationException("This instance is locked")
                    If Not IsEnumMember(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(IPTCTypes))
                    _Type = value
                End Set
            End Property
            ''' <summary>Underlying type for <see cref="Type"/></summary>
            Public ReadOnly Property UnderlyingType() As Type
                Get
                    Return GetUnderlyingType(Type)
                End Get
            End Property
            ''' <summary>Contains value of the <see cref="[Enum]"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Enum As Type
            ''' <summary>Type of enumeration of tag (if type of tag is enumeration)</summary>
            ''' <returns>If tag is enumeration then returns type of enumeration, null otherwise</returns>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            ''' <exception cref="ArgumentException">Type being set does not inherit from the <see cref="[Enum]"/> class</exception>
            Public Property [Enum]() As Type
                Get
                    Return _Enum
                End Get
                Set(ByVal value As Type)
                    If Locked And value IsNot [Enum] Then Throw New InvalidOperationException("This instance is locked")
                    If value IsNot Nothing AndAlso Not value.IsSubclassOf(GetType([Enum])) Then Throw New ArgumentException("Given type is not enum")
                    _Enum = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Mandatory"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Mandatory As Boolean
            ''' <summary>Indicate if tag is mandatory according to IPTC standard</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            Public Property Mandatory() As Boolean
                Get
                    Return _Mandatory
                End Get
                Set(ByVal value As Boolean)
                    If Locked And value <> Mandatory Then Throw New InvalidOperationException("This instance is locked")
                    _Mandatory = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Repeatable"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Repeatable As Boolean
            ''' <summary>Indicates if tag is repeatable</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            Public Property Repeatable() As Boolean
                Get
                    Return _Repeatable
                End Get
                Set(ByVal value As Boolean)
                    If Locked And value <> Repeatable Then Throw New InvalidOperationException("This instance is locked")
                    _Repeatable = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Fixed"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Fixed As Boolean
            ''' <summary>Indicates if length of tag data is fixed</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true -or- setting value to true when <see cref="Length"/> is 0</exception>
            Public Property Fixed() As Boolean
                Get
                    Return _Fixed
                End Get
                Set(ByVal value As Boolean)
                    If Locked And value <> Fixed Then Throw New InvalidOperationException("This instance is locked")
                    If value AndAlso Length = 0 Then Throw New InvalidOperationException("Cannot set Fixed to True when Length is 0")
                    _Fixed = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Length"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Length As Short
            ''' <summary>Maximal length of tag data in bytes. If <see cref="Fixed"/> is true that this is also actual length of tagat data in bytes.</summary>
            ''' <value>You can set length to 0 in order to mark length as unlimited. In such case <see cref="Fixed"/> must be false</value>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true -or- setting value to 0 when <see cref="Fixed"/> is true</exception>
            ''' <exception cref="ArgumentOutOfRangeException">setting value to negative</exception>
            Public Property Length() As Short
                Get
                    Return _Length
                End Get
                Set(ByVal value As Short)
                    If Locked And value <> Length Then Throw New InvalidOperationException("This instance is locked")
                    If value = 0 AndAlso Fixed Then Throw New InvalidOperationException("Cannot set Length to 0 when Fixed is true")
                    If value < 0 Then Throw New ArgumentOutOfRangeException("Length mus be non-negative")
                    _Length = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Category"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Category As String
            ''' <summary>Tag catagory</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            Public Property Category() As String
                Get
                    Return _Category
                End Get
                Set(ByVal value As String)
                    If Locked And value <> Category Then Throw New InvalidOperationException("This instance is locked")
                    _Category = value
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Description"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Description As String
            ''' <summary>Description of tag</summary>
            ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
            Public Property Description() As String
                Get
                    Return _Description
                End Get
                Set(ByVal value As String)
                    If Locked And value <> Description Then Throw New InvalidOperationException("This instance is locked")
                    _Description = value
                End Set
            End Property
        End Class

        ''' <summary>Indicates if enum may allow values that are not member of it or not</summary>
        <AttributeUsage(AttributeTargets.Enum)> _
        Public Class RestrictAttribute : Inherits Attribute
            ''' <summary>Contains value of the <see cref="Restrict"/> property</summary>
            Private _Restrict As Boolean
            ''' <summary>CTor</summary>
            ''' <param name="Restrict">State of restriction</param>
            Public Sub New(ByVal Restrict As Boolean)
                _Restrict = Restrict
            End Sub
            ''' <summary>Inidicates if values should be restricted to enum members</summary>
            Public ReadOnly Property Restrict() As Boolean
                Get
                    Return _Restrict
                End Get
            End Property
        End Class
    End Class
#End If
End Namespace