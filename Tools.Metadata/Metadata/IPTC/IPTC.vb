Imports Tools.CollectionsT.GenericT, Tools.ExtensionsT, System.Linq, Tools.ReflectionT
Imports Tools.DataStructuresT.GenericT
Namespace MetadataT.IptcT
#If Congig <= Alpha Then 'Stage: Alpha
    ''' <summary>Provides high-level access to IPTC metadata</summary>
    ''' <remarks>Value key format for this <see cref="IMetadata"/> is "RecordNumber:DatasetNumber".</remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2">The <see cref="IMetadata"/> interface implemented</version>
    ''' <version version="1.5.2">The <see cref="AuthorAttribute"/>, <see cref="VersionAttribute"/> and <see cref="FirstVersionAttribute"/> attributes removed</version>
    ''' <version version="1.5.3">Added limited auto-detection of UTF-8 encoding based on <see cref="Iptc.CodedCharacterSet"/>. See <see cref="Iptc.Encoding"/> for details.</version>
    ''' <version version="1.5.3">Changed encoding used when none is specified for <see cref="Iptc.GraphicCharacters_Value"/>, <see cref="Iptc.TextWithSpaces_Value"/>, <see cref="Iptc.Text_Value"/>, <see cref="Iptc.Num2_Str_Value"/>, <see cref="Iptc.Num3_Str_Value"/>, <see cref="Iptc.SubjectReference_Value"/>, <see cref="Iptc.Alpha_Value"/>: <see cref="Iptc.Encoding"/> is used for records 2 - 6 and 8, <see cref="System.Text.Encoding.ASCII"/> is used otherwise. (Previously: <see cref="Iptc.Encoding"/> is used whenever encoding is not specified).</version>
    ''' <version version="1.5.3">Added the <see cref="Iptc.IgnoreLenghtConstraints"/> property which when set to true allows values violating length constraints (according to IPTC standard) to be stored.</version>
    ''' <version version="1.5.3">Properties of type HHMMSS_HHMM are also read when stored as HHMMSS only (before 1.5.3 an exception was thrown)</version>
    Partial Public Class Iptc
        Implements IMetadata
        ''' <summary>Name identifying IPTC metadata in <see cref="IMetadataProvider"/></summary>
        ''' <version version="1.5.2">Constant introduced</version>
        Public Const IptcName$ = "IPTC"
        ''' <summary>Do nothing CTor</summary>
        Public Sub New()
            AddHandler _Tags.Adding, AddressOf _Tags_Change
            AddHandler _Tags.ItemChanging, AddressOf _Tags_Change
            _Tags.AllowAddCancelableEventsHandlers = True
        End Sub
        ''' <summary>Handles <see cref="ListWithEvents(Of KeyValuePair(Of DataSetIdentification, Byte())).Adding"/> and <see cref="ListWithEvents(Of KeyValuePair(Of DataSetIdentification, Byte())).ItemChanging"/> events and checks if added item data are not so long</summary>
        ''' <param name="sender"><see cref="Tag"/></param>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>The <see cref="CollectionsT.GenericT.ListWithEvents(Of System.Collections.Generic.KeyValuePair(Of DataSetIdentification, Byte())).CancelableItemIndexEventArgs.Cancel"/> is set tor true when lenght of data is longer than 32767 which causet <see cref="OperationCanceledException"/> to be thrown by caller</remarks>
        Private Sub _Tags_Change(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of System.Collections.Generic.KeyValuePair(Of DataSetIdentification, Byte())), ByVal e As CollectionsT.GenericT.ListWithEvents(Of System.Collections.Generic.KeyValuePair(Of DataSetIdentification, Byte())).CancelableItemIndexEventArgs)
            If e.Item.Value.Length > 32767 Then
                e.Cancel = True
                e.CancelMessage = ResourcesT.Exceptions.DataSetsLongerThan32767BAreNotSupported
            End If
        End Sub
        ''' <summary>Contains value of the <see cref="Tags"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Tags As New ListWithEvents(Of KeyValuePair(Of DataSetIdentification, Byte()))(True, True)

        ''' <summary>CTor from <see cref="IPTCReader"/></summary>
        ''' <param name="Reader"><see cref="IPTCReader"/> to read all tags from. No data loaded when null.</param>
        ''' <version version="1.5.4">Parameter <c>Reader</c> renamed to <c>reader</c></version>
        ''' <version version="1.5.4">Exception is no longer thrown when <paramref name="reader"/> is null</version>
        Public Sub New(ByVal reader As IptcReader)
            Me.New()
            If reader IsNot Nothing Then
                For Each t As IptcRecord In reader.Records
                    Tags.Add(New KeyValuePair(Of DataSetIdentification, Byte())(DataSetIdentification.GetKnownDataSet(t.RecordNumber, t.Tag), t.Data))
                Next t
            End If
        End Sub
        ''' <summary>Gets details about tag format by tag record and number</summary>
        ''' <param name="Record">Recor number</param>
        ''' <param name="TagNumber">Number of tag within <paramref name="Record"></paramref></param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="Record"/> is not member of <see cref="RecordNumbers"/> -or- <paramref name="TagNumber"/> is not tag within <paramref name="record"/>.
        ''' </exception>
        ''' <version version="1.5.4">Parameters renamed: <c>Record</c> to <c>record</c>, <c>TagNumber</c> to <c>tagNumber</c></version>
        Public Shared Function GetTag(ByVal record As RecordNumbers, ByVal tagNumber As Byte) As IptcTag
            Dim lUseThisGroup As GroupInfo = Nothing
            Return GetTag(record, tagNumber, lUseThisGroup)
        End Function
        ''' <summary>CTor from <see cref="IIPTCGetter"/></summary>
        ''' <param name="Getter"><see cref="IIPTCGetter"/> that contains IPTC stream. No data loaded when null.</param>
        ''' <exception cref="IO.InvalidDataException">Tag marker other than 1Ch found</exception>
        ''' <exception cref="NotSupportedException">Extended-size tag found</exception>
        ''' <version version="1.5.4">Parameter <c>Getter</c> renamed to <c>getter</c></version>
        ''' <version version="1.5.4">Exception is no longer thrown when <paramref name="getter"/> is null.</version>
        Public Sub New(ByVal getter As IIptcGetter)
            Me.New(If(getter Is Nothing, Nothing, New IptcReader(getter)))
        End Sub
        ''' <summary>Removes all occurences of specified tag</summary>         
        ''' <param name="Key">Tag to remove</param>                            
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Overridable Sub Clear(ByVal key As DataSetIdentification)
            _Tags.RemoveAll(DataSetIdentification.PairMatch.GetPredicate(Of Byte())(key))
        End Sub
        ''' <summary>Removes all tags</summary>
        Public Sub Clear()
            Tags.Clear()
        End Sub
        ''' <summary>Gets count of tags with specified key</summary>
        ''' <param name="Key">DataSet identification to count tags with</param>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Function Contains(ByVal key As DataSetIdentification) As Integer
            Return _Tags.FindAll(DataSetIdentification.PairMatch.GetPredicate(Of Byte())(key)).Count
        End Function
        ''' <summary>Called when value of any tag changes</summary>
        ''' <param name="Tag">Recod and dataset number</param>
        ''' <remarks>
        ''' <para>Called by <see cref="Tag"/>'s setter.</para>
        ''' <para>Note for inheritors: Call base class method in order to automatically compute size of embdeded file and invalidate cache for <see cref="BW460_Value"/></para>
        ''' </remarks>
        ''' <version version="1.5.4">Parameter <c>Tag</c> renamed to <c>tag</c></version>
        Protected Overridable Sub OnValueChanged(ByVal tag As DataSetIdentification)
            Select Case tag
                Case DataSetIdentification.Subfile 'Subfile - change corresponding tags
                    Dim Subfile As Byte() = Me.Subfile
                    Dim SubFileLen As Integer
                    If Subfile Is Nothing Then SubFileLen = 0 Else SubFileLen = Subfile.Length
                    If Subfile Is Nothing Then
                        Clear(DataSetIdentification.ConfirmedObjectDataSize)
                        Clear(DataSetIdentification.MaxSubfileSize)
                        Clear(DataSetIdentification.MaximumObjectDataSize)
                        Clear(DataSetIdentification.ObjectDataSizeAnnounced)
                        Clear(DataSetIdentification.SizeMode)
                    Else
                        ConfirmedObjectDataSize = SubFileLen
                        MaxSubfileSize = SubFileLen
                        Clear(DataSetIdentification.MaximumObjectDataSize)
                        ObjectDataSizeAnnounced = SubFileLen
                        SizeMode = True
                    End If
                Case DataSetIdentification.CodedCharacterSet 'Coded Caharcter Set - reset encoding
                    If Not encodingSetExternally Then _Encoding = Nothing
            End Select
            If Cache.ContainsKey(tag) Then Cache.Remove(tag)
        End Sub
        ''' <summary>Gets or sets values associated with particular tag</summary>
        ''' <param name="Key">Tag identification</param>
        ''' <remarks>This property does no checks if tag <paramref name="Key"/> is repeatable or not and does not checks structure of byte arrays that represents values of tags, so you can totally corrupt structure if some fields. Also tag grouping is not checked. You should use this property very carefully or you can damage internal structure of IPTC data</remarks>
        ''' <value>New values for particular tag. Values of tags are replaced with new values. If there was more tags with same <paramref name="Key"/> than is being set then the next tags are removed. If there was less tags with same <paramref name="Key"/> necessary items are added at the end of the stream</value>
        ''' <returns>List of values of tag or null if tag is missing</returns>
        ''' <exception cref="NotSupportedException">Setting byte array longer then 32767</exception>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Default Protected Property Tag(ByVal key As DataSetIdentification) As List(Of Byte())
            Get
                Dim ret As List(Of KeyValuePair(Of DataSetIdentification, Byte())) = _Tags.FindAll(DataSetIdentification.PairMatch.GetPredicate(Of Byte())(key))
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
                For Each item As Byte() In value
                    If item.Length > 32767 Then Throw New NotSupportedException(ResourcesT.Exceptions.IPTCDataSetsLongerThat32767BytesAreNotSupported)
                Next item
                Dim Already As IReadOnlyList(Of Integer) = New DataSetIdentification.PairMatch(key).GetIndices(Tags)
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
                        Tags.RemoveAt(Already(j))
                    Next j
                Else
                    'Add tags (if value is longer than Already)
                    For j As Integer = i To value.Count - 1
                        Tags.Add(New KeyValuePair(Of DataSetIdentification, Byte())(key, value(j)))
                    Next j
                End If
                OnValueChanged(key)
            End Set
        End Property
        ''' <summary>All tags and their values in IPTC stream</summary>
        ''' <remarks><see cref="IList(Of KeyValuePair(Of DataSetIdentification, Byte())).Add"/> and <see cref="IList(Of KeyValuePair(Of DataSetIdentification, Byte())).Item"/>' setter throws an <see cref="OperationCanceledException"/> when trying to arr or set item that consists of more than 32767 bytes</remarks>
        Protected Friend ReadOnly Property Tags() As IList(Of KeyValuePair(Of DataSetIdentification, Byte()))
            <DebuggerStepThrough()> Get
                Return _Tags
            End Get
        End Property
        ''' <summary>Sorts tags so they are ordered in IPTC-standard-non-violating manner</summary>
        ''' <remarks>That means that they are ordered by recod number. Order of individual datasets inside records is kept</remarks>
        Protected Overridable Sub SortTags()
            Dim taglist(9) As List(Of KeyValuePair(Of DataSetIdentification, Byte()))
            For Each item As KeyValuePair(Of DataSetIdentification, Byte()) In Tags
                If taglist(item.Key.RecordNumber) Is Nothing Then taglist(item.Key.RecordNumber) = New List(Of KeyValuePair(Of DataSetIdentification, Byte()))
                taglist(item.Key.RecordNumber).Add(item)
            Next item
            Dim NewList As New List(Of KeyValuePair(Of DataSetIdentification, Byte()))
            For i As Integer = 0 To 9
                If taglist(i) IsNot Nothing Then _
                    NewList.AddRange(taglist(i))
            Next i
            _Tags.Clear()
            _Tags.AddRange(NewList)
        End Sub
        ''' <summary>Gets bytes of IPTC stream</summary>
        ''' <returns>IPTC data encoded according to the IPTC standard in set of tags</returns>
        ''' <remarks>The tag format is following:
        ''' B1: 0x1C, B2: Record Number, B3 DataSet Number, B4&amp;5 Length of data, B6+ Data
        ''' </remarks>
        Public Function GetBytes() As Byte()
            SortTags()
            Dim str As New System.IO.MemoryStream()
            Dim bw As New System.IO.BinaryWriter(str)
            For Each Tag As KeyValuePair(Of DataSetIdentification, Byte()) In Tags
                bw.Write(CByte(&H1C))
                bw.Write(CByte(Tag.Key.RecordNumber))
                bw.Write(CByte(Tag.Key.DatasetNumber))
                If Tag.Value IsNot Nothing Then
                    bw.Write(MathT.LEBE(CUShort(Tag.Value.Length)))
                    bw.Write(Tag.Value)
                Else
                    bw.Write(MathT.LEBE(CUShort(0)))
                End If
            Next Tag
            Dim pos As Integer = str.Position
            str.Position = 0
            Return New System.IO.BinaryReader(str).ReadBytes(pos)
        End Function
#Region "IMetadata"
        ''' <summary>Parses key from its string value or predefined name</summary>
        ''' <param name="Key">Key to parse</param>
        ''' <param name="TryPredefined">True to try get predefined dataset identification by name; false to use code only.</param>
        ''' <returns>Parsed key</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> is not predefined key (or <paramref name="TryPredefined"/> is false) and is in invalid format for <see cref="DataSetIdentification"/>.</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter names converted to camelCase</version>
        Private Function ParseKey(ByVal key As String, Optional ByVal tryPredefined As Boolean = True) As DataSetIdentification
            If tryPredefined Then
                Dim Ret As DataSetIdentification? = KeyFromPredefinedName(key)
                If Ret.HasValue Then Return Ret.Value
            End If
            Try
                Return New DataSetIdentification(key)
            Catch ex As Exception
                Throw New ArgumentException(ResourcesT.Exceptions.KeyIsInvalidForIPTCMetadata, ex)
            End Try
        End Function
        ''' <summary>Gets <see cref="DataSetIdentification"/> from <see cref="DataSetIdentification.PropertyName"/></summary>
        ''' <param name="Key">Name of dataset identification to get</param>
        ''' <returns>Dataset identification or null if no dataset identification with given <see cref="DataSetIdentification.PropertyName"/> is in <see cref="DataSetIdentification.KnownDataSets"/>.</returns>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Private Function KeyFromPredefinedName(ByVal key$) As DataSetIdentification?
            Return (From dsi In DataSetIdentification.KnownDataSets(True) Where dsi.PropertyName = key Select New DataSetIdentification?(dsi)).FirstOrDefault
        End Function

        ''' <summary>Gets value indicating wheather metadata value with given key is present in current instance</summary>
        ''' <param name="Key">Key (or name) to check presence of (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>True when value for given key is present; false otherwise</returns>
        ''' <remarks>The <paramref name="Key"/> parameter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format and it is not one of predefined names</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Function ContainsKey(ByVal key As String) As Boolean Implements IMetadata.ContainsKey
            Return Me.Contains(ParseKey(key))
        End Function

        ''' <summary>Gets keys of all the metadata present in curent instance</summary>
        ''' <returns>Keys in metadata-specific format of all the metadata present in curent instance. Never returns null; may return anempt enumeration.</returns>
        ''' <version version="1.5.2">Function introduced</version>
        Public Function GetContainedKeys() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetContainedKeys
            Return (From tag In Me.Tags Select Key = tag.Key.ToString).Distinct
        End Function

        ''' <summary>Gets localized description for given key (or name)</summary>
        ''' <param name="Key">Key (or name) to get description of</param>
        ''' <returns>Localized description of purpose of metadata item identified by <paramref name="Key"/>; nul when description is not available.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> is in invalid format or it is not one of predefined names.</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Function GetDescription(ByVal key As String) As String Implements IMetadata.GetDescription
            Dim KeyDsi = ParseKey(key)
            Dim PropertyName = GetNameOfKey(KeyDsi.ToString)
            If PropertyName Is Nothing Then Return Nothing
            Dim Prp = GetType(Iptc).GetProperty(PropertyName)
            If Prp Is Nothing Then Return Nothing
            Dim DescA = Prp.GetAttribute(Of DescriptionAttribute)()
            If DescA Is Nothing Then Return Nothing
            Return DescA.Description
        End Function

        ''' <summary>Gets localized human-readable name for given key (or name)</summary>
        ''' <param name="Key">Key (or name) to get name for</param>
        ''' <returns>Human-readable descriptive name of metadata item identified by <paramref name="Key"/>; null when no such name is defined/known.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid formar or it is not one of predefined names.</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Function GetHumanName(ByVal key As String) As String Implements IMetadata.GetHumanName
            Dim KeyDsi = ParseKey(key)
            Return (From dsi In DataSetIdentification.KnownDataSets(True) Where dsi.DatasetNumber = KeyDsi.DatasetNumber AndAlso dsi.RecordNumber = KeyDsi.RecordNumber Select dsi.DisplayName).FirstOrDefault
        End Function

        ''' <summary>Gets key for predefined name</summary>
        ''' <param name="Name">Name to get key for</param>
        ''' <returns>Key in metadata-specific format for given predefined metadata item name</returns>
        ''' <exception cref="ArgumentException"><paramref name="Name"/> is not one of predefined names retuened by <see cref="GetPredefinedNames"/>.</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter <c>Name</c> renamed to <c>name</c></version>
        Public Function GetKeyOfName(ByVal name As String) As String Implements IMetadata.GetKeyOfName
            Return (From dsi In DataSetIdentification.KnownDataSets(True) Where dsi.PropertyName = name Select Key = dsi.ToString).FirstOrDefault
        End Function

        ''' <summary>Gets name for key</summary>
        ''' <param name="Key">Key to get name for</param>
        ''' <returns>One of predefined names to use instead of <paramref name="Key"/>; null when given key has no corresponding name.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format</exception>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Function GetNameOfKey(ByVal key As String) As String Implements IMetadata.GetNameOfKey
            Dim KeyDsi = ParseKey(key, False)
            Return (From dsi In DataSetIdentification.KnownDataSets(True) Where dsi.DatasetNumber = KeyDsi.DatasetNumber AndAlso dsi.RecordNumber = KeyDsi.RecordNumber Select dsi.PropertyName).FirstOrDefault
        End Function

        ''' <summary>Gets all keys predefined for curent metadata format</summary>
        ''' <returns>Eumeration containing all predefined (well-known) keys of metadata for this metadata format. Returns always the same enumeration event when values for some keys are not present. Never returns null; may return an empty enumeration.</returns>
        ''' <remarks>Not all predefined keys are required to have corresponding names obtainable via <see cref="GetNameOfKey"/>.</remarks>
        ''' <version version="1.5.2">Function introduced</version>
        Public Function GetPredefinedKeys() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetPredefinedKeys
            Return From k In DataSetIdentification.KnownDataSets(True) Select Key = k.ToString
        End Function

        ''' <summary>Gets all predefined names for metadata keys</summary>
        ''' <returns>Enumeration containing all predefined names of metadata items for this metadada format. Never returns null; may return an empty enumeration.</returns>
        ''' <remarks>Metadata format may support 2 formats of retrieving of metadata values: By key and by name. The by-name format is optional.
        ''' Keys are typically computer-friendly strings (like tag numbers or addresses) and metedata format may support values with non-predefined keys.
        ''' Names are typically human-friendly (not-localized) string (like names) and only predefined names are supported (if any). Each name must have its corresponding key. Names are only aliases to certain important keys.</remarks>
        ''' <version version="1.5.2">Function introduced</version>
        Public Function GetPredefinedNames() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetPredefinedNames
            Return From k In DataSetIdentification.KnownDataSets(True) Select Key = k.PropertyName
        End Function

        ''' <summary>Gets name of metadata format represented by implementation</summary>
        ''' <returns><see cref="IptcName"/></returns>
        ''' <remarks>All <see cref="IMetadataProvider">IMetadataProviders</see> returning IPTC format should identify the format by this name.</remarks>
        ''' <version version="1.5.2">Property introduced</version>
        Private ReadOnly Property Name() As String Implements IMetadata.Name
            Get
                Return IptcName
            End Get
        End Property

        ''' <summary>Gets medata value with given key</summary>
        ''' <param name="Key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key; or null if given metadata value is not supported</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format and it is not one of predefined names</exception>
        ''' <remarks>The <paramref name="Key"/> peremeter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        ''' <version version="1.5.2">Property introduced</version>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Overloads ReadOnly Property Value(ByVal key As String) As Object Implements IMetadata.Value
            Get
                Return Me.GetTypedValue(ParseKey(key))
            End Get
        End Property
        ''' <summary>Gets metadata value with given key as string</summary>
        ''' <param name="Key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key as string; or null if given metadata value is not supported</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format and it is not one of predefined names</exception>
        ''' <remarks>The <paramref name="Key"/> peremeter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter <c>Key</c> renamed to <c>key</c></version>
        Public Function GetStringValue(ByVal key As String) As String Implements IMetadata.GetStringValue
            Return GetStringValue(key, Nothing)
        End Function

        ''' <summary>Gets metadata value with given key as string</summary>
        ''' <param name="key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <param name="provider">Culture to be used. If null current is used.</param>
        ''' <returns>Value of metadata item with given key as string; or null if given metadata value is not supported</returns>
        ''' <exception cref="ArgumentException"><paramref name="key"/> has invalid format and it is not one of predefined names</exception>
        ''' <remarks>The <paramref name="key"/> peremeter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        ''' <version version="1.5.4">This overload is new in version 1.5.4</version>
        Public Function GetStringValue(ByVal key As String, provider As IFormatProvider) As String Implements IMetadata.GetStringValue
            If provider Is Nothing Then provider = Globalization.CultureInfo.CurrentCulture
            Dim ret = Value(key)
            If ret Is Nothing Then Return Nothing
            If TypeOf ret Is IEnumerable(Of Byte) Then
                Dim r2 As New System.Text.StringBuilder
                For Each b In DirectCast(ret, IEnumerable(Of Byte))
                    r2.Append(b.ToString("x2"))
                Next
                Return r2.ToString
            ElseIf TypeOf ret Is IEnumerable AndAlso Not TypeOf ret Is String Then
                Dim r2 As New System.Text.StringBuilder
                Dim ti = provider.GetTextInfo(Globalization.CultureInfo.CurrentCulture.TextInfo)
                For Each item In DirectCast(ret, IEnumerable)
                    If r2.Length <> 0 Then r2.Append(ti.ListSeparator & " ")
                    r2.Append(If(TypeOf item Is IFormattable, DirectCast(item, IFormattable).ToString(provider), item.ToString))
                Next
                Return r2.ToString
            ElseIf TypeOf ret Is IFormattable Then
                Return DirectCast(ret, IFormattable).ToString(provider)
            Else
                Return ret.ToString
            End If
        End Function

        ''' <summary>Gets value indicating if this instance was already disposed or not</summary>
        ''' <returns>This implementation always returns false because this class does not implement <see cref="IDisposable"/></returns>
        ''' <version version="1.5.4">This property is new in version 1.5.4</version>
        Protected Overridable ReadOnly Property Disposed As Boolean Implements IMetadata.Disposed
            Get
                Return False
            End Get
        End Property
#End Region


    End Class
    ''' <summary>Represents common base for <see cref="IPTCGetException"/> and <see cref="IPTCSetException"/></summary>
    Public MustInherit Class IptcException : Inherits Exception
        ''' <summary>CTor</summary>
        ''' <param name="InnerException">Inner exception</param>
        ''' <version version="1.5.4">Parameter <c>InnerException</c> renamed to <c>innerException</c></version>
        Friend Sub New(ByVal innerException As Exception)
            MyBase.New(InnerException.Message, InnerException)
        End Sub
    End Class
    ''' <summary>Thrown when an error occurs when geting IPTC tag value</summary>
    Public Class IptcGetException : Inherits IptcException
        ''' <summary>CTor</summary>
        ''' <param name="InnerException">Inner exception</param>
        ''' <version version="1.5.4">Parameter <c>InnerException</c> renamed to <c>innerException</c></version>
        Public Sub New(ByVal innerException As Exception)
            MyBase.New(InnerException)
        End Sub
    End Class
    ''' <summary>Thrown when an error occurs when setting IPTC tag value</summary>
    Public Class IptcSetException : Inherits IptcException
        ''' <summary>CTor</summary>
        ''' <param name="InnerException">Inner exception</param>
        ''' <version version="1.5.4">Parameter <c>InnerException</c> renamed to <c>innerException</c></version>
        Public Sub New(ByVal InnerException As Exception)
            MyBase.New(InnerException)
        End Sub
    End Class

    ''' <summary>Identifies IPTC tag (DataSet). Used for indexing.</summary>
    ''' <completionlist cref="DataSetIdentification"/>
    <DebuggerDisplay("{RecordNumber}:{DatasetNumber} {DisplayName}")> _
    Partial Public Structure DataSetIdentification
        Implements IPair(Of RecordNumbers, Byte), IEquatable(Of IPair(Of RecordNumbers, Byte)), ICloneable(Of DataSetIdentification)
#Region "Parse"
        ''' <summary>CTor form string representation</summary>
        ''' <param name="RecordAndDataSet">String representation in format "RecordNumber:DatasetNumber"</param>
        ''' <exception cref="argumentnullexception"><paramref name="RecordAndDataSet"/> is null</exception>
        ''' <exception cref="formatexception"><paramref name="RecordAndDataSet"/> does not contain 2 :-separated parts -or- any part of <paramref name="RecordAndDataSet"/> is not in correct format for <see cref="Int32.Parse"/> in invariant culture.</exception>
        ''' <exception cref="overflowexception">Any part of <paramref name="RecordAndDataSet"/> does not fit to <see cref="Int32"/> data type or to <see cref="Byte"/> data type.</exception>
        ''' <exception cref="ArgumentOutOfRangeException">RecordNumber-part is greater than 9</exception> 
        ''' <seelaso cref="Parse"/>
        ''' <version version="1.5.2">Constructor introduced</version>
        ''' <version version="1.5.4">Parameter <c>RecordAndDataSet</c> renamed to <c>recordAndDataSet</c></version>
        Public Sub New(ByVal recordAndDataSet As String)
            If RecordAndDataSet Is Nothing Then Throw New ArgumentNullException("RecordAndDataSet")
            Dim parts = RecordAndDataSet.Split(":"c)
            If parts.Length <> 2 Then Throw New FormatException(ResourcesT.Exceptions.String0IsNotValid1.f(RecordAndDataSet, GetType(DataSetIdentification).Name))
            Me.RecordNumber = Integer.Parse(parts(0), System.Globalization.CultureInfo.InvariantCulture)
            Me.DatasetNumber = Integer.Parse(parts(1), System.Globalization.CultureInfo.InvariantCulture)
        End Sub
        ''' <summary>Parses <see cref="DataSetIdentification"/> from string representation</summary>
        ''' <param name="Value">String representation in format "RecordNumber:DatasetNumber"</param>
        ''' <exception cref="argumentnullexception"><paramref name="Value"/> is null</exception>
        ''' <exception cref="formatexception"><paramref name="Value"/> does not contain 2 :-separated parts -or- any part of <paramref name="Value"/> is not in correct format for <see cref="Int32.Parse"/> in invariant culture.</exception>
        ''' <exception cref="overflowexception">Any part of <paramref name="Value"/> does not fit to <see cref="Int32"/> data type or to <see cref="Byte"/> data type.</exception>
        ''' <exception cref="ArgumentOutOfRangeException">RecordNumber-part is greater than 9</exception> 
        ''' <seelaso cref="TryParse"/>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter <c>Value</c> renamed to <c>value</c></version>
        Public Shared Function Parse(ByVal Value As String) As DataSetIdentification
            Return New DataSetIdentification(Value)
        End Function
        ''' <summary>Attempts to parse <see cref="DataSetIdentification"/> from string representation</summary>
        ''' <param name="Value">String representation in format "RecordNumber:DatasetNumber"</param>
        ''' <param name="ParsedValue">When successfull contains parsed value when function returns</param>
        ''' <returns>True when successfull; false when <paramref name="Value"/> is invalid.</returns>
        ''' <seelaso cref="Parse"/>
        ''' <version version="1.5.2">Function introduced</version>
        ''' <version version="1.5.4">Parameter names changed: <c>Value</c> to <c>value</c>, <c>ParsedValue</c> to <c>parsedValue</c></version>
        Public Shared Function TryParse(ByVal value$, ByRef parsedValue As DataSetIdentification) As Boolean
            Try
                ParsedValue = Parse(Value)
                Return True
            Catch ex As Exception When TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is FormatException OrElse TypeOf ex Is OverflowException OrElse TypeOf ex Is ArgumentOutOfRangeException
                Return False
            End Try
        End Function
        ''' <summary>Gets string representation of current <see cref="DataSetIdentification"/></summary>
        ''' <returns>String in format "RecordNumber:DatasetNumber" in invariant culture</returns>
        ''' <version version="1.5.2">Override introduced</version>
        Public Overrides Function ToString() As String
            Return String.Format(Globalization.CultureInfo.InvariantCulture, "{0:d}:{1}", RecordNumber, DatasetNumber)
        End Function
#End Region
        ''' <summary>Contains value of the <see cref="RecordNumber"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _RecordNumber As RecordNumbers
        ''' <summary>Contains value of the <see cref="DatasetNumber"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _DataSetNumber As Byte
        ''' <summary>Contains balue of the <see cref="PropertyName"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _PropertyName$
        ''' <summary>Contains value of the <see cref="DisplayName"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _DisplayName$
        ''' <summary>Record (tag group) number</summary>
        ''' <exception cref="ArgumentOutOfRangeException">Value being set is greater than 9</exception>
        Public Property RecordNumber() As RecordNumbers Implements DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte).Value1
            Get
                Return _RecordNumber
            End Get
            <DebuggerStepThrough()> Set(ByVal value As RecordNumbers)
                If value > 9 Then Throw New ArgumentOutOfRangeException(ResourcesT.Exceptions.DataSetNumberMustBeLessThanOrEqualTo9)
                _RecordNumber = value
            End Set
        End Property
        ''' <summary>DataSet (tag) number</summary>
        Public Property DatasetNumber() As Byte Implements DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte).Value2
            Get
                Return _DataSetNumber
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Byte)
                _DataSetNumber = value
            End Set
        End Property
        ''' <summary>Gets or sets name of property of the <see cref="IPTC"/> class this dataset is accessible via. Null if this is not known dataset.</summary>
        ''' <returns>Name of property of the <see cref="IPTC"/> class this data set is accessible via. Null if this is not standard dataset.</returns>
        ''' <value>Name of property of the <see cref="IPTC"/> class this dataset is accessible via. Such property must exist.</value>
        ''' <seelaso cref="IPTCTag.Name"/>
        Public Property PropertyName$()
            <DebuggerStepThrough()> Get
                Return _PropertyName
            End Get
            Set(ByVal value$)
                'If value IsNot Nothing Then
                '    If GetType(IPTC).GetProperty(value) Is Nothing Then
                '        Dim found As Boolean = False
                '        For Each Group In GroupInfo.GetAllGroups
                '            If Group.Type.GetProperty(value) IsNot Nothing Then _
                '                found = True : Exit For
                '        Next
                '        If Not found Then Throw New MissingMemberException(GetType(IPTC).FullName, value)
                '    End If
                'End If
                _PropertyName = value
            End Set
        End Property
        ''' <summary>Gets or localized display name of dataset.</summary>
        ''' <returns>Localized display name of data set. Null if data set has no name.</returns>
        ''' <value>Localized display name of data set. Null if data set has no name.</value>
        ''' <seelaso cref="IPTCTag.HumanName"/>
        Public Property DisplayName$()
            <DebuggerStepThrough()> Get
                Return _DisplayName
            End Get
            <DebuggerStepThrough()> Set(ByVal value$)
                _DisplayName = value
            End Set
        End Property
        ''' <summary>Copy CTor from <see cref="IPair(Of RecordNumbers, Byte)"/>[<see cref="RecordNumbers"/>, <see cref="Byte"/>]</summary>
        ''' <param name="From">Instance to be cloned</param>
        ''' <version version="1.5.4">Parameter <c>From</c> renamed to <c>from</c></version>
        Public Sub New(ByVal from As IPair(Of RecordNumbers, Byte))
            Me.RecordNumber = From.Value1
            Me.DatasetNumber = From.Value2
        End Sub
        ''' <summary>Copy CTor</summary>
        ''' <param name="From">Instance to be cloned</param>
        ''' <version version="1.5.4">Parameter <c>From</c> renamed to <c>from</c></version>
        Public Sub New(ByVal from As DataSetIdentification)
            Me.New(DirectCast(From, IPair(Of RecordNumbers, Byte)))
            Me.DisplayName = From.DisplayName
            Me.PropertyName = From.PropertyName
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="RecordNumber">Number of record (tag group)</param>
        ''' <param name="DataSetNumber">Number of dataset (tag)</param>
        ''' <param name="DisplayName">Localized display name of data set property (null if data set has no name)</param>
        ''' <param name="PropertyName">Name of property of the <see cref="IPTC"/> class this data set is accessible via (null if this is not known data set accessible via property of the <see cref="IPTC"/> class)</param>
        <DebuggerStepThrough()> _
        Public Sub New(ByVal RecordNumber As RecordNumbers, ByVal DataSetNumber As Byte, ByVal PropertyName$, ByVal DisplayName$)
            Me.RecordNumber = RecordNumber
            Me.DatasetNumber = DataSetNumber
            Me.PropertyName = PropertyName
            Me.DisplayName = DisplayName
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        <Obsolete("Use type-safe Clone instead")> _
        Private Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function
        ''' <summary>Swaps values <see cref="IPair(Of RecordNumbers, Byte).Value1"/> and <see cref="IPair(Of RecordNumbers, Byte).Value2"/></summary>
        Private Function Swap() As DataStructuresT.GenericT.IPair(Of Byte, RecordNumbers) Implements IPair(Of RecordNumbers, Byte).Swap
            Return New Pair(Of Byte, RecordNumbers)(DatasetNumber, RecordNumber)
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Private Function ICloneable__IPair__RecordNumbers_Byte_____Clone() As DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte) Implements ICloneable(Of DataStructuresT.GenericT.IPair(Of RecordNumbers, Byte)).Clone
            Return New DataSetIdentification(Me)
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function Clone() As DataSetIdentification Implements ICloneable(Of DataSetIdentification).Clone
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
        ''' <summary>Contains value of the <see cref="KnownDataSetsInternal"/> property (null when property has not been initialized yet)</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Shared _KnownDataSetsInternal As Dictionary(Of DataSetIdentification, DataSetIdentification)
        ''' <summary>Synchronisation object for initializaion rountine of the <see cref="KnownDataSetsInternal"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Shared KnownDataSetsInternalSyncLock As New Object
        ''' <summary>Contains cached value of the <see cref="KnownDataSets"/> (with hidden ones)</summary>
        Private Shared ReadOnly Property KnownDataSetsInternal() As Dictionary(Of DataSetIdentification, DataSetIdentification)
            Get
                If _KnownDataSetsInternal Is Nothing Then
                    SyncLock KnownDataSetsInternalSyncLock
                        If _KnownDataSetsInternal IsNot Nothing Then Return _KnownDataSetsInternal
                        _KnownDataSetsInternal = New Dictionary(Of DataSetIdentification, DataSetIdentification)
                        For Each dataset In KnownDataSets(False)
                            _KnownDataSetsInternal.Add(dataset, dataset)
                        Next
                    End SyncLock
                End If
                Return _KnownDataSetsInternal
            End Get
        End Property
        ''' <summary>Gets known data set from record number and tag number</summary>
        ''' <param name="RecordNumber">Recor number</param>
        ''' <param name="Tag">Tag number</param>
        ''' <remarks>If dataset with given <paramref name="RecordNumber"/> and <paramref name="Tag"/> exists in <see cref="KnownDataSets"/> returns item from there, otherwise returns newly created instance of <see cref="DataSetIdentification"/> initialized with <paramref name="RecordNumber"/> nad <paramref name="Tag"/> (with null <see cref="DisplayName"/> and <see cref="PropertyName"/>).</remarks>
        ''' <version version="1.5.4">Parameters renamed: <c>RecordNumber</c> to <c>recordNumber</c>, <c>Tag</c> to <c>tag</c></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function GetKnownDataSet(ByVal recordNumber As RecordNumbers, ByVal tag As Byte) As DataSetIdentification
            Dim WithSameHash As New DataSetIdentification(RecordNumber, Tag, Nothing, Nothing)
            If KnownDataSetsInternal.ContainsKey(WithSameHash) Then Return KnownDataSetsInternal(WithSameHash)
            Return WithSameHash
        End Function
        ''' <summary>Gives acctess to <see cref="System.Predicate"/> that matches <see cref="KeyValuePair(Of DataSetIdentification, T)"/> which's <see cref="KeyValuePair.Key"/> is same as given <see cref="DataSetIdentification"/></summary>
        <DebuggerDisplay("{RecordNumber}:{DatasetNumber}")> _
        Public Class PairMatch
            ''' <summary><see cref="DataSetIdentification"/> to compare <see cref="KeyValuePair.Key"/> with</summary>
            Public ReadOnly Match As DataSetIdentification
            ''' <summary>CTor</summary>
            ''' <param name="Match"><see cref="DataSetIdentification"/> to compare <see cref="KeyValuePair.Key"/> with</param>
            ''' <version version="1.5.4">Parameter <c>Match</c> renamed to <c>match</c></version>
            <DebuggerStepThrough()> _
            Public Sub New(ByVal match As DataSetIdentification)
                Me.Match = Match
            End Sub
            ''' <summary>Function which's delegate can be passed for example to <see cref="List.FindAll"/></summary>
            ''' <param name="Pair">Item to match with <see cref="Match"/></param>
            ''' <typeparam name="T">Type of value stored in <see cref="KeyValuePair"/></typeparam>
            ''' <version version="1.5.4">Parameter <c>Pair</c> renamed to <c>pair</c></version>
            Public Function Predicate(Of T)(ByVal pair As KeyValuePair(Of DataSetIdentification, T)) As Boolean
                Return Pair.Key = Match
            End Function
            ''' <summary>Returns delegate of <see cref="PairMatch.Predicate"/> of newly created instance of <see cref="PairMatch"/></summary>
            ''' <param name="Match">Key to compare with</param>
            ''' <returns>Delegate of <see cref="PairMatch.Predicate"/></returns>
            ''' <typeparam name="T">Type of value of <see cref="KeyValuePair"/> that can be passed to returned <see cref="System.Predicate"/></typeparam>
            ''' <version version="1.5.4">Parameter <c>Match</c> renamed to <c>match</c></version>
            <DebuggerStepThrough()> _
            Public Shared Function GetPredicate(Of T)(ByVal match As DataSetIdentification) As System.Predicate(Of KeyValuePair(Of DataSetIdentification, T))
                Return AddressOf New PairMatch(Match).Predicate(Of T)
            End Function
            ''' <summary>Gets indices of items in given <see cref="IEnumerable(Of KeyValuePair(Of DataSetIdentification, T))"/> which's <see cref="KeyValuePair.Key"/> matches <see cref="Match"/></summary>
            ''' <param name="List">List to search within</param>
            ''' <returns>List of indices of items which's <see cref="KeyValuePair.Key"/> matches <see cref="Match"/></returns>
            ''' <typeparam name="T">Type of value of <see cref="KeyValuePair(Of DataSetIdentification, T)"/></typeparam>
            ''' <version version="1.5.4">Parameter <c>List</c> renamed to <c>list</c></version>
            Public Function GetIndices(Of T)(ByVal list As IEnumerable(Of KeyValuePair(Of DataSetIdentification, T))) As IReadOnlyList(Of Integer)
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
    ''' <summary>Describes IPTC dataset's (tag) properties</summary>
    Public Class IptcTag
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
        ''' <param name="Group">Group the tag ius member of (or null if tag is member of no group)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Record"/> is greater than 9 -or- <paramref name="Length"/> is less than zero</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Type"/> is not member of <see cref="IPTCTypes"/></exception>
        ''' <version version="1.5.4">Parameter names changed to camelCase</version>
        Public Sub New(
                ByVal number As Byte,
                ByVal record As RecordNumbers,
                ByVal name As String,
                ByVal humanName As String,
                ByVal type As IptcTypes,
                ByVal mandatory As Boolean,
                ByVal repeatable As Boolean,
                ByVal length As Short,
                ByVal fixed As Boolean,
                ByVal category As String,
                ByVal description As String,
                Optional ByVal [enum] As Type = Nothing,
                Optional ByVal lock As Boolean = False,
                Optional ByVal group As GroupInfo = Nothing
        )
            Me.Number = number
            Me.Record = record
            Me.Name = name
            Me.HumanName = humanName
            Me.Type = type
            Me.Mandatory = mandatory
            Me.Repeatable = repeatable
            If fixed Then
                Me.Length = length
                Me.Fixed = fixed
            Else
                Me.Fixed = fixed
                Me.Length = length
            End If
            Me.Category = category
            Me.Description = description
            Me.Enum = [enum]
            Me.Group = group
            Me._Locked = lock
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
                Return New DataSetIdentification(Me.Record, Me.Number, Me.Name, Me.HumanName)
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
                If Locked And value <> Number Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                _Number = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Group"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Group As GroupInfo
        ''' <summary>If the tag is member of some group, gets the group</summary>
        ''' <returns>If the tag is member of some group returns the group otherwise returns null</returns>
        ''' <value>Group the tag is member of</value>
        Public Property Group() As GroupInfo
            Get
                Return _Group
            End Get
            Set(ByVal value As GroupInfo)
                If Locked And value IsNot Group Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                _Group = value
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
                If Locked And value <> Record Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                If value < 0 OrElse value > 9 Then Throw New ArgumentOutOfRangeException(ResourcesT.Exceptions.RecordMustBeFrom0To9)
                _Record = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Name"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Name As String
        ''' <summary>Tag name as used in object structure</summary>
        ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
        ''' <seelaso cref="DataSetIdentification.PropertyName"/>
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                If Locked And value <> Name Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                _Name = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="HumanName"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _HumanName As String
        ''' <summary>Human-friendly name of tag</summary>
        ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
        ''' <seelaso cref="DataSetIdentification.DisplayName"/>
        Public Property HumanName() As String
            Get
                Return _HumanName
            End Get
            Set(ByVal value As String)
                If Locked And value <> HumanName Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                _HumanName = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Type"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Type As IptcTypes
        ''' <summary>Type of tag</summary>
        ''' <remarks>You can get type of value of tag by <see cref="Iptc.GetUnderlyingType"/> function or <see cref="UnderlyingType"/> property</remarks>
        ''' <exception cref="InvalidOperationException">Changing value when <see cref="Locked"/> is true</exception>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="IPTCTypes"/></exception>
        Public Property Type() As IptcTypes
            Get
                Return _Type
            End Get
            Set(ByVal value As IptcTypes)
                If Locked And value <> Type Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                If Not IsEnumMember(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(IptcTypes))
                _Type = value
            End Set
        End Property
        ''' <summary>Returns value indicating if givel value if member of enumeration</summary>
        ''' <param name="value">Value to be checked</param>
        ''' <returns>True if <paramref name="value"/> is member of <paramref name="T"/></returns>
        ''' <typeparam name="T">Type of enumeration to be searched</typeparam>
        ''' <exception cref="ArgumentException"><paramref name="T"/> is not <see cref="[Enum]"/></exception>
        Private Shared Function IsEnumMember(Of T As {IComparable, Structure})(ByVal value As T) As Boolean
            Return Array.IndexOf(System.[Enum].GetValues(GetType(T)), value) >= 0
        End Function
        ''' <summary>Underlying type for <see cref="Type"/></summary>
        Public ReadOnly Property UnderlyingType() As Type
            Get
                Return Iptc.GetUnderlyingType(Type)
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
                If Locked And value IsNot [Enum] Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                If value IsNot Nothing AndAlso Not value.IsSubclassOf(GetType([Enum])) Then Throw New ArgumentException(ResourcesT.Exceptions.GivenTypeIsNotEnum)
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
                If Locked And value <> Mandatory Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
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
                If Locked And value <> Repeatable Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
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
                If Locked And value <> Fixed Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                If value AndAlso Length = 0 Then Throw New InvalidOperationException(ResourcesT.Exceptions.CannotSetFixedToTrueWhenLengthIs0)
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
                If Locked And value <> Length Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                If value = 0 AndAlso Fixed Then Throw New InvalidOperationException(ResourcesT.Exceptions.CannotSetLengthTo0WhenFixedIsTrue)
                If value < 0 Then Throw New ArgumentOutOfRangeException(String.Format(ResourcesT.Exceptions.MustBeNonNegative, "Lenght"))
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
                If Locked And value <> Category Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
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
                If Locked And value <> Description Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceIsLocked)
                _Description = value
            End Set
        End Property
    End Class

#End If
End Namespace