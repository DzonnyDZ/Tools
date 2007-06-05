Imports Tools.CollectionsT.GenericT
Namespace DrawingT.MetadataT
#If Congig <= Nightly Then 'Stage: Nightly
    'ASAP:Wiki, Forum, Mark
    Partial Public Class IPTC
        ''' <summary>Describes IPTC dataset's (tag) properties</summary>
        Public Class IPTCTag
            'TODO: CTor, Properies
            Private _Number As Byte
            Private _Record As RecordNumbers
            Private _Name As String
            Private _HumanName As String
            Private _Type As IPTCTypes
            Private _Enum As Type
            Private _Mandatory As Boolean
            Private _Repeatable As Boolean
            Private _Fixed As Boolean
            Private _Length As Short
            Private _Category As String
            Private _Description As String
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