Imports Tools.ComponentModelT, Tools.DrawingT.MetadataT.IptcT.IptcDataTypes
Imports Tools.DrawingT.DesignT, System.Drawing.Design

#If Config <= Nightly Then 'Stage:Nightly
Namespace DrawingT.MetadataT.IptcT
    ''' <summary>Information about group of tags</summary>
    Public Class GroupInfo
        ''' <summary>Contains value of the <see cref="Tags"/> Proeprty</summary>
        Private _Tags() As IptcTag
        ''' <summary>Contains value of the <see cref="Mandatory"/> Proeprty</summary>
        Private _Mandatory As Boolean
        ''' <summary>Contains value of the <see cref="Repeatable"/> Proeprty</summary>
        Private _Repeatable As Boolean
        ''' <summary>Contains value of the <see cref="Name"/> Proeprty</summary>
        Private _Name As String
        ''' <summary>Contains value of the <see cref="HumanName"/> Proeprty</summary>
        Private _HumanName As String
        ''' <summary>Contains value of the <see cref="Group"/> Proeprty</summary>
        Private _Group As Groups
        ''' <summary>Contains value of the <see cref="Category"/> Proeprty</summary>
        Private _Category As String
        ''' <summary>Contains value of the <see cref="Description"/> Proeprty</summary>
        Private _Description As String
        ''' <summary>Contains value of the <see cref="Type"/> Proeprty</summary>
        Private _Type As Type
        ''' <summary>CTor</summary>
        ''' <param name="Name">Name of group used in object structure</param>
        ''' <param name="HumanName">Human-friendly name of group</param>
        ''' <param name="Group">Group number</param>
        ''' <param name="Type">Type that represents this group</param>
        ''' <param name="Category">Category of this group</param>
        ''' <param name="Description">Description</param>
        ''' <param name="Mandatory">Group is mandatory according to IPTC standard</param>
        ''' <param name="Repeatable">Group is repeatable</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Group"/> is not member of <see cref="Groups"/></exception>
        ''' <remarks>The <see cref="SetTags"/> method must be used to complete initialization using this CTor.</remarks>
        Public Sub New(ByVal Name As String, ByVal HumanName As String, ByVal Group As Groups, ByVal Type As Type, ByVal Category As String, ByVal Description As String, ByVal Mandatory As Boolean, ByVal Repeatable As Boolean) ', ByVal ParamArray Tags As IPTCTag())
            'If Tags Is Nothing OrElse Tags.Length < 2 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.EachGroupMustHaveAtLeast0Tags, 2))
            If Not InEnum(Group) Then Throw New InvalidEnumArgumentException("Group", Group, GetType(Groups))
            If Type Is Nothing Then Throw New ArgumentNullException("Type")
            '_Tags = Tags.Clone
            _Mandatory = Mandatory
            _Repeatable = Repeatable
            _Name = Name
            _HumanName = HumanName
            _Group = Group
            _Category = _Category
            _Description = Description
            _Type = Type
        End Sub
        ''' <summary>Initializes the value of the <see cref="Tags"/> property</summary>
        ''' <exception cref="InvalidOperationException">The <see cref="Tags"/> property have already been initialized</exception>
        ''' <param name="Tags">Tags the group consists of</param>
        ''' <exception cref="ArgumentException"><paramref name="Tags"/> is null or have less than 2 items</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Sub SetTags(ByVal ParamArray Tags As IptcTag())
            If _Tags IsNot Nothing Then Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.The0PropertyHaveAlreadyBeenInitialized, "Tags"))
            If Tags Is Nothing OrElse Tags.Length < 2 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.EachGroupMustHaveAtLeast0Tags, 2))
            _Tags = Tags.Clone
        End Sub
        ''' <summary>Type that realizes object representation of this group</summary>
        Public ReadOnly Property Type() As Type
            Get
                Return _Type
            End Get
        End Property
        ''' <summary>Tags present in this group</summary>
        Public ReadOnly Property Tags() As IptcTag()
            Get
                If _Tags Is Nothing Then Throw New InvalidOperationException(ResourcesT.Exceptions.TheTagsPropertyHaveNotBeenInitializedUseTheSetTagsMethodToInitializeIt)
                Dim Arr(_Tags.Length - 1) As IptcTag
                _Tags.CopyTo(Arr, 0)
                Return Arr
            End Get
        End Property
        ''' <summary>True if this group is mandatory according to IPTC standard</summary>
        Public ReadOnly Property Mandatory() As Boolean
            Get
                Return _Mandatory
            End Get
        End Property
        ''' <summary>True if this group is repeatable</summary>
        Public ReadOnly Property Repeatable() As Boolean
            Get
                Return _Repeatable
            End Get
        End Property
        ''' <summary>Name of group used in object structure</summary>
        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property
        ''' <summary>Human-friendly name of this group</summary>
        Public ReadOnly Property HumanName() As String
            Get
                Return _HumanName
            End Get
        End Property
        ''' <summary>Code of this group</summary>
        Public ReadOnly Property Group() As Groups
            Get
                Return _Group
            End Get
        End Property
        ''' <summary>Name of category of this group</summary>
        Public ReadOnly Property Category() As String
            Get
                Return _Category
            End Get
        End Property
        ''' <summary>Description of this group</summary>
        Public ReadOnly Property Description() As String
            Get
                Return _Description
            End Get
        End Property
    End Class
    ''' <summary>Common base for all tag groups</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Public MustInherit Class Group
        ''' <summary>Gets assignmenst between group indexes of tags and indexes of groups</summary>
        ''' <param name="Tags">Tags contained in this group</param>
        ''' <param name="IPTC"><see cref="IPTC"/> to create map for</param>
        ''' <returns><see cref="List(Of Integer())"/> where each item of list means one group instance and contains indexes of tags when obtained via tag properties</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Tags"/> is null or contains less then 2 items -or- <paramref name="IPTC"/> is null</exception>
        Protected Shared Function GetGroupMap(ByVal IPTC As Iptc, ByVal ParamArray Tags As IptcTag()) As List(Of Integer())
            If Tags Is Nothing OrElse Tags.Length < 2 Then Throw New ArgumentException("tags", String.Format(ResourcesT.Exceptions.MustContainAtLeast1Items, "Tags", 2))
            If IPTC Is Nothing Then Throw New ArgumentNullException("IPTC")
            Dim Counters(Tags.Length - 1) As Integer 'Count of searched tags
            Dim Stage As Integer = -1 'Index of tag expected to be found
            Dim ListIndex As Integer = -1 'Current index in ret
            Dim ret As New List(Of Integer())
            For Each item As KeyValuePair(Of DataSetIdentification, Byte()) In IPTC.Tags
                'Is this tag that I'm searching for?
                Dim Searching As Integer = -1
                For i As Integer = 0 To Tags.Length - 1
                    If Tags(i).Identification = item.Key Then Searching = i : Exit For
                Next i
                If Searching >= 0 Then
                    If Stage < Searching OrElse Stage >= Tags.Length Then 'New group
                        Dim arr(Tags.Length - 1) As Integer
                        For j As Integer = 0 To Tags.Length - 1 : arr(j) = -1 : Next j
                        ret.Add(arr)
                        ListIndex += 1
                    End If
                    ret(ListIndex)(Searching) = Counters(Searching)
                    Counters(Searching) += 1
                    Stage = Searching + 1
                End If
            Next item
            If ret.Count = 0 Then Return Nothing Else Return ret
        End Function
    End Class

#Region "Extending group classes"
    Partial Class Iptc
        Partial Public Class ObjectDataPreviewGroup
            ''' <summary>CTor</summary>
            Public Sub New()
                Me.ObjectDataPreviewFileFormat = FileFormats.NoObjectData
                Me.ObjectDataPreviewFileFormatVersion = FileFormatVersions.V0
                Me.ObjectDataPreviewData = New Byte() {}
            End Sub
            ''' <summary>String representation (number of bytes)</summary>
            Public Overrides Function ToString() As String
                Dim Bytes As Integer
                If ObjectDataPreviewData Is Nothing Then Bytes = 0 Else Bytes = ObjectDataPreviewData.Length
                Return String.Format("{0}B", Bytes)
            End Function
            ''' <summary><see cref="System.ComponentModel.TypeConverter"/> for <see cref="ObjectDataPreviewGroup"/></summary>
            Public Class Converter : Inherits ExpandableObjectConverter(Of ObjectDataPreviewGroup)
                ''' <summary>Returns whether changing a value on this object requires a call to the <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)"/> method to create a new value.</summary>
                ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
                ''' <remarks>True</remarks>
                Public Overrides Function GetCreateInstanceSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                    Return True
                End Function
                ''' <summary>Creates an instance of the type that this <see cref="System.ComponentModel.TypeConverter"/> is associated with, using the specified context, given a set of property values for the object.</summary>
                ''' <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> of new property values.</param>
                ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
                ''' <returns>New instance of <see cref="ObjectDataPreviewGroup"/> initialized from <paramref name="propertyValues"/></returns>
                Public Overrides Function CreateInstance(ByVal propertyValues As System.Collections.IDictionary, ByVal context As System.ComponentModel.ITypeDescriptorContext) As ObjectDataPreviewGroup
                    Dim ret As New ObjectDataPreviewGroup
                    With propertyValues
                        ret.ObjectDataPreviewData = !ObjectDataPreviewData
                        ret.ObjectDataPreviewFileFormat = !ObjectDataPreviewFileFormat
                        ret.ObjectDataPreviewFileFormatVersion = !ObjectDataPreviewFileFormatVersion
                    End With
                    Return ret
                End Function
            End Class
        End Class
        <DebuggerDisplay("{ToString}")> _
        Partial Class ContentLocationGroup
            ''' <summary>String representation</summary>
            Public Overrides Function ToString() As String
                Return String.Format("{0} {1}", Me.ContentLocationCode, Me.ContentLocationName)
            End Function
            ''' <summary>CTor</summary>
            Public Sub New()
                Me.ContentLocationCode = New StringEnum(Of ISO3166)(ISO3166.Afghanistan)
                Me.ContentLocationName = DirectCast(GetConstant(ISO3166.Afghanistan).GetCustomAttributes(GetType(DisplayNameAttribute), True)(0), DisplayNameAttribute).DisplayName
            End Sub
        End Class
        <Editor(GetType(NewEditor), GetType(UITypeEditor))> _
        Partial Class ARMGroup
            ''' <summary>CTor</summary>
            Public Sub New()
                Me.ARMIdentifier = ARMMethods.IPTCMethod1
                Me.ARMVersion = ARMVersions.ARM1
            End Sub
            ''' <summary><see cref="System.ComponentModel.TypeConverter"/> for <see cref="ARMGroup"/></summary>
            Public Class Converter : Inherits ExpandableObjectConverter(Of ARMGroup)
                ''' <summary>Returns whether changing a value on this object requires a call to the <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)"/> method to create a new value.</summary>
                ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
                ''' <remarks>True</remarks>
                Public Overrides Function GetCreateInstanceSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
                    Return True
                End Function
                ''' <summary>Creates an instance of the type that this <see cref="System.ComponentModel.TypeConverter"/> is associated with, using the specified context, given a set of property values for the object.</summary>
                ''' <param name="propertyValues">An <see cref="System.Collections.IDictionary"/> of new property values.</param>
                ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
                ''' <returns>New instance of <see cref="ARMGroup"/> initialized from <paramref name="propertyValues"/></returns>
                Public Overrides Function CreateInstance(ByVal propertyValues As System.Collections.IDictionary, ByVal context As System.ComponentModel.ITypeDescriptorContext) As ARMGroup
                    Dim ret As New ARMGroup
                    With propertyValues
                        ret.ARMIdentifier = !ARMIdentifier
                        ret.ARMVersion = !ARMVersion
                    End With
                    Return ret
                End Function
            End Class
        End Class
    End Class
#End Region
End Namespace
#End If