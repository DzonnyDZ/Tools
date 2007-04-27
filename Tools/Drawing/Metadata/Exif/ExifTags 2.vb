'TODO:Delete this file
'TProposed structure for code generated from  ExifTags.xml follows
Namespace Drawing.Metadata
    Partial Public Class Exif
        ''' <summary>Information about exif tag</summary>
        Partial Public Class ExifTagDefinition
            ''' <summary>Name according to Exit standard</summary>
            Public ReadOnly Name As String
            ''' <summary>Code</summary>
            <CLSCompliant(False)> _
            Public ReadOnly Code As UShort
            ''' <summary>Number of components</summary>
            ''' <remarks>0 means that thre can be any number of components</remarks>
            Public ReadOnly Components As Integer
            ''' <summary>Possible datatypes. First datatype is preffered.</summary>
            <CLSCompliant(False)> _
            Public ReadOnly Types As ExifIFDReader.DirectoryEntry.ExifDataTypes()
            ''' <summary>CTor</summary>
            ''' <param name="Name">Name according to Exit standard</param>
            ''' <param name="Code">Tag code</param>
            ''' <param name="Components">Number of components; 0 means that thre can be any number of components</param>
            ''' <param name="Types">Possible datatypes. First datatype is preffered.</param>
            <CLSCompliant(False)> _
            Public Sub New(ByVal Name As String, ByVal Code As UShort, ByVal Components As Integer, ByVal ParamArray Types As ExifIFDReader.DirectoryEntry.ExifDataTypes())
                If Components < 0 Then Throw New ArgumentOutOfRangeException("Components", "Number of components must be greater then or equal to zero")
                If Types Is Nothing OrElse Types.Length = 0 Then Throw New ArgumentException("At least one type must be specified", "Types")
                Me.Name = Name
                Me.Code = Code
                Me.Components = Components
                Me.Types = Types
            End Sub
        End Class
        ''' <summary>Tags used in IFD0 and IFD1</summary>
        Partial Public NotInheritable Class IFDTags
            ''' <summary>Codes of tags used in IFD0 and IFD1</summary>
            <CLSCompliant(False)> _
            Public Enum TagCodes As UShort
#Region "Group name"
                ''' <summary>Tag Summary</summary>
                TagName = &H113
                '...
#End Region
                '...
            End Enum
            ''' <summary>Details for storing tag TagName</summary>
            Public Shared ReadOnly Property TagName() As ExifTagDefinition
                Get
                    Return New ExifTagDefinition("TagName", &H147, 8, ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32, ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt16)
                End Get
            End Property
            '...
            '<CLSCompliant()> if needed
            Public Enum enmTagNameValues As Byte 'Or other datatype
                ''' <summary>summary</summary>
                ItemName = 14
            End Enum
            'TODO:String (Char) enums
            '...
            <CLSCompliant(False)> _
            Public Shared ReadOnly Property TagDefinition(ByVal Tag As TagCodes) As ExifTagDefinition
                Get
                    Select Case Tag
                        Case TagCodes.TagName : Return TagName
                            '...
                        Case Else : Return Nothing
                    End Select
                End Get
            End Property
        End Class
        ''' <summary>Tags used in Exif Sub IFD</summary>
        Partial Public Class ExifTags
            '...
        End Class
        ''' <summary>Tags used in GPS Sub IFD</summary>
        Partial Public Class GPSTags
            '...
        End Class
        ''' <summary>Tags used in Interop Sub IFD</summary>
        Partial Public Class InteropTags
            '...
        End Class

        Public Property TagName() As IFDTags.enmTagNameValues
            Get
                Return Me(IFDTags.TagCodes.TagName)
            End Get
            Set(ByVal value As IFDTags.enmTagNameValues)
                Me(IFDTags.TagCodes.TagName) = value
            End Set
        End Property
        '...
        <CLSCompliant(False)> _
        Default Public Property Tag(ByVal TagCode As IFDTags.TagCodes) As Object
            Get
                'TODO: Return tag value
            End Get
            Set(ByVal value As Object)
                'TODO: Stote tag value
            End Set
        End Property
        'TODO:More 3 times forn other IFDs
    End Class
End Namespace