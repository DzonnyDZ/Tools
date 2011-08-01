Imports System.ComponentModel
Imports Tools.MetadataT.ExifT
Imports Tools.MetadataT

''' <summary>Holds instances of metadata classes</summary>
Public Class MetadataCollection
    Implements INotifyPropertyChanged
    Implements IMetadataProvider

    Private _iptc As IptcInternal
    Private _exif As Exif
    Private _system As SystemMetadata

    ''' <summary>Gets or sets instance of IPTC metadata</summary>
    Public Property Iptc() As IptcInternal
        Get
            Return _iptc
        End Get
        Set(value As IptcInternal)
            If value IsNot Iptc Then
                _iptc = value
                OnPropertyChanged("Iptc")
            End If
        End Set
    End Property

    ''' <summary>Gets or sets instance of Exif metadata</summary>
    Public Property Exif() As Exif
        Get
            Return _exif
        End Get
        Set(value As Exif)
            If value IsNot Exif Then
                _exif = value
                OnPropertyChanged("Exif")
            End If
        End Set
    End Property
    ''' <summary>Gets or sets instance of system metadata</summary>
    Public Property System() As SystemMetadata
        Get
            Return _system
        End Get
        Set(value As SystemMetadata)
            If value IsNot System Then
                _system = value
                OnPropertyChanged("System")
            End If
        End Set
    End Property

    ''' <summary>Pupulates current instance with metadata</summary>
    ''' <param name="from">Source of metadata</param>
    Public Sub Populate(from As IMetadataProvider)
        Iptc = from.Metadata(IptcT.Iptc.IptcName)
        Exif = from.Metadata(ExifT.Exif.ExifName)
        System = from(SystemMetadata.SystemName)
    End Sub

    ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
    ''' <param name="e">Evcent arguments</param>
    Protected Overridable Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub
    ''' <summary>Raises the <see cref="PropertyChanged"/> event using property name</summary>
    ''' <param name="propertyName">Name of changed proeprty</param>
    Protected Sub OnPropertyChanged(propertyName$)
        OnPropertyChanged(New PropertyChangedEventArgs(propertyName))
    End Sub

    ''' <summary>Occurs when a property value changes.</summary>
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "IMetadataProvider"
    ''' <summary>Gets value indicating if metadata of particular type are provided by this provider</summary>
    ''' <param name="MetadataName">Name of metadata type</param>
    ''' <returns>True if this provider contains metadata with given name</returns>
    Private Function Contains(metadataName As String) As Boolean Implements IMetadataProvider.Contains
        Return (StringComparer.InvariantCultureIgnoreCase.Compare(MetadataName, IptcT.Iptc.IptcName) = 0 AndAlso Iptc IsNot Nothing) OrElse
               (StringComparer.InvariantCultureIgnoreCase.Compare(MetadataName, ExifT.Exif.ExifName) = 0 AndAlso Exif IsNot Nothing) OrElse
               (StringComparer.InvariantCultureIgnoreCase.Compare(MetadataName, SystemMetadata.SystemName) = 0 AndAlso System IsNot Nothing)
    End Function

    ''' <summary>Gets names of metadata actually contained in metadata source represented by this provider</summary>
    ''' <returns>Names of metadata usefull with the <see cref="Metadata"/> function. Never returns null; may return an empty enumeration.</returns>
    Private Function GetContainedMetadataNames() As IEnumerable(Of String) Implements IMetadataProvider.GetContainedMetadataNames
        Dim ret As New List(Of String)
        If Iptc IsNot Nothing Then ret.Add(IptcT.Iptc.IptcName)
        If Exif IsNot Nothing Then ret.Add(ExifT.Exif.ExifName)
        If System IsNot Nothing Then ret.Add(SystemMetadata.SystemName)
        Return ret.ToArray
    End Function

    ''' <summary>Get all the names of metadata supported by this provider (even when some of the metadata cannot be provided by current instance)</summary>
    ''' <returns>Name sof metadata usefull with the <see cref="Metadata"/> function. Never returns null; should not return an empty enumeration.</returns>
    Private Function GetSupportedMetadataNames() As IEnumerable(Of String) Implements IMetadataProvider.GetSupportedMetadataNames
        Return {IptcT.Iptc.IptcName, ExifT.Exif.ExifName, SystemMetadata.SystemName}
    End Function

    ''' <summary>Gets metadata of particular type</summary>
    ''' <param name="MetadataName">Name of metadata to get (see <see cref="GetSupportedMetadataNames"/> for possible values)</param>
    ''' <returns>Metadata of requested type; or null if metadata of type <paramref name="MetadataName"/> are not contained in this instance or are not supported by this provider.</returns>
    Private ReadOnly Property Metadata(metadataName As String) As IMetadata Implements MetadataT.IMetadataProvider.Metadata
        Get
            Select Case metadataName.ToLowerInvariant
                Case IptcT.Iptc.IptcName.ToLowerInvariant : Return Iptc
                Case ExifT.Exif.ExifName : Return Exif
                Case SystemMetadata.SystemName : Return System
            End Select
            Return Nothing
        End Get
    End Property
#End Region
End Class
