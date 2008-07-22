Imports System.Linq, Tools.DrawingT, Tools.DataStructuresT.GenericT, Tools.CollectionsT.SpecializedT, Tools.IOt.FileTystemTools, Tools.CollectionsT.GenericT
Imports System.ComponentModel, Tools.WindowsT, Tools.ExtensionsT
Imports Tools.DrawingT.MetadataT, Tools.DrawingT.DrawingIOt
Imports System.Reflection
Imports MBox = Tools.WindowsT.IndependentT.MessageBox, MButton = Tools.WindowsT.IndependentT.MessageBox.MessageBoxButton
Imports Tools.WindowsT.FormsT
''' <summary>Extends <see cref="IPTC"/> with some extra stuff</summary>
<DebuggerDisplay("{ImagePath}")> _
Public Class IPTCInternal
    Inherits IPTC
    ''' <summary>CTor</summary>
    ''' <param name="ImagePath">Path of JPEG file</param>
    ''' <exception cref="System.IO.DirectoryNotFoundException">The specified <paramref name="ImagePath"/> is invalid, such as being on an unmapped drive.</exception>
    ''' <exception cref="System.ArgumentNullException"><paramref name="ImagePath"/> is null.</exception>
    ''' <exception cref="System.UnauthorizedAccessException">The access requested (readonly) is not permitted by the operating system for the specified path.</exception>
    ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
    ''' <exception cref="System.ArgumentException"><paramref name="ImagePath"/> is an empty string (""), contains only white space, or contains one or more invalid characters.</exception>
    ''' <exception cref="System.IO.FileNotFoundException">The file cannot be found.</exception>
    ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
    ''' <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
    ''' <exception cref="IO.InvalidDataException">
    ''' Invalid JPEG marker found (code doesn't start with FFh, length set to 0 or 2) -or-
    ''' JPEG stream doesn't start with corect SOI marker -or-
    ''' JPEG stream doesn't end with corect EOI marker -or-
    ''' Tag marker other than 1Ch found.
    ''' </exception>
    ''' <exception cref="NotSupportedException">Extended-size tag found</exception>
    Friend Sub New(ByVal ImagePath As String)
        MyBase.New(New JPEG.JPEGReader(ImagePath, False))
        _ImagePath = ImagePath
        _Changed = False
    End Sub
    ''' <summary>Contains value of the <see cref="ImagePath"/> property</summary>
    Private _ImagePath As String
    ''' <summary>Path of image this instance holds information for</summary>
    <Browsable(False)> _
    Public ReadOnly Property ImagePath() As String
        Get
            Return _ImagePath
        End Get
    End Property
    ''' <summary>String representation</summary>
    ''' <returns><see cref="ImagePath"/></returns>
    Public Overrides Function ToString() As String
        Return _ImagePath
    End Function
    ''' <summary>Raises the <see cref="ValueChanged"/> event</summary>
    ''' <param name="Tag">Recod and dataset number</param>
    ''' <remarks>
    ''' <para>Called by <see cref="Tag"/>'s setter.</para>
    ''' <para>Note for inheritors: Call base class method in order to automatically compute size of embdeded file and invalidate cache for <see cref="BW460_Value"/></para>
    ''' </remarks>
    Protected Overrides Sub OnValueChanged(ByVal Tag As DrawingT.MetadataT.IPTC.DataSetIdentification)
        MyBase.OnValueChanged(Tag)
        _Changed = True
        RaiseEvent ValueChanged(Me, EventArgs.Empty)
    End Sub
    ''' <summary>Contains value of the <see cref="Changed"/> property</summary>
    Private _Changed As Boolean
    ''' <summary>Gets value indicating if this instance is dirty (changed)</summary>
    ''' <returns>True if instance was changed since save/load</returns>
    <Browsable(False)> _
    Public ReadOnly Property Changed() As Boolean
        Get
            Return _Changed
        End Get
    End Property
    '''' <summary>Gets value indicating if IPTC data of this instance have changed</summary>
    '''' <remarks>True if IPTC data of this instance changed since last load</remarks>
    'Public ReadOnly Property IPTCChanged() As Boolean
    '    Get
    '        Return _IPTCChanged
    '    End Get
    'End Property
    ''' <summary>Raised when value of any tag changes</summary>
    Public Event ValueChanged As EventHandler(Of IPTCInternal, EventArgs) ', TagChangedEventArgs)
    ''' <summary>Gets or sets value of common property identified by value of <see cref="CommonIPTCProperties"/></summary>
    ''' <param name="Property">Property tpo get/set</param>
    ''' <exception cref="InvalidEnumArgumentException"><paramref name="Property"/> is none of predefined <see cref="CommonIPTCProperties"/> values or it is <see cref="CommonIPTCProperties.None"/> or <see cref="CommonIPTCProperties.All"/>.</exception>
    ''' <exception cref="NotSupportedException"><paramref name="Property"/> is <see cref="CommonIPTCProperties.Keywords"/></exception>
    Friend Property Common(ByVal [Property] As CommonIPTCProperties) As String
        Get
            Select Case [Property]
                Case CommonIPTCProperties.Caption : Return CaptionAbstract
                Case CommonIPTCProperties.City : Return City
                Case CommonIPTCProperties.Copyright : Return CopyrightNotice
                Case CommonIPTCProperties.Country : Return CountryPrimaryLocationName
                Case CommonIPTCProperties.CountryCode : Return CountryPrimaryLocationCode
                Case CommonIPTCProperties.Credit : Return Credit
                Case CommonIPTCProperties.EditStatus : Return EditStatus
                Case CommonIPTCProperties.Keywords : Throw New NotSupportedException(My.Resources.KeywordsAreNotSupportedByCommponProperty_internalError)
                Case CommonIPTCProperties.ObjectName : Return ObjectName
                Case CommonIPTCProperties.Province : Return ProvinceState
                Case CommonIPTCProperties.Sublocation : Return SubLocation
                Case CommonIPTCProperties.ObjectName : Return ObjectName
                Case Else : Throw New InvalidEnumArgumentException("Property", [Property], [Property].GetType)
            End Select
        End Get
        Set(ByVal value As String)
            Select Case [Property]
                Case CommonIPTCProperties.Caption : If value <> CaptionAbstract Then CaptionAbstract = value
                Case CommonIPTCProperties.City : If value <> City Then City = value
                Case CommonIPTCProperties.Copyright : If value <> CopyrightNotice Then CopyrightNotice = value
                Case CommonIPTCProperties.Country : If value <> CountryPrimaryLocationName Then CountryPrimaryLocationName = value
                Case CommonIPTCProperties.CountryCode : If value <> CountryPrimaryLocationCode Then CountryPrimaryLocationCode = value
                Case CommonIPTCProperties.Credit : If value <> Credit Then Credit = value
                Case CommonIPTCProperties.EditStatus : If value <> EditStatus Then EditStatus = value
                Case CommonIPTCProperties.Keywords : Throw New NotSupportedException(My.Resources.KeywordsAreNotSupportedByCommponProperty_internalError)
                Case CommonIPTCProperties.ObjectName : If value <> ObjectName Then ObjectName = value
                Case CommonIPTCProperties.Province : If value <> ProvinceState Then ProvinceState = value
                Case CommonIPTCProperties.Sublocation : If value <> SubLocation Then SubLocation = value
                Case CommonIPTCProperties.Urgency : If value <> Urgency Then Urgency = value
                Case Else : Throw New InvalidEnumArgumentException("Property", [Property], [Property].GetType)
            End Select
        End Set
    End Property
    ''' <summary>Saves current IPTC stream to file <see cref="ImagePath"/></summary>
    ''' <exception cref="System.IO.DirectoryNotFoundException">The specified <see cref="ImagePath"/> is invalid, such as being on an unmapped drive.</exception>
    ''' <exception cref="System.UnauthorizedAccessException">The access requested (readonly) is not permitted by the operating system for the specified path.</exception>
    ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
    ''' <exception cref="System.IO.FileNotFoundException">The file cannot be found.</exception>
    ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
    ''' <exception cref="IO.InvalidDataException">
    ''' Invalid JPEG marker found (code doesn't start with FFh, length set to 0 or 2) -or-
    ''' JPEG stream doesn't start with corect SOI marker -or-
    ''' JPEG stream doesn't end with corect EOI marker
    ''' </exception>
    ''' <exception cref="InvalidOperationException">No JPEG marker found</exception>
    Friend Sub Save()
        If Me.Changed Then
            Using jw As New JPEG.JPEGReader(Me.ImagePath, True)
                jw.IPTCEmbed(Me.GetBytes)
            End Using
            _Changed = False
            RaiseEvent Saved(Me)
        End If
    End Sub
    ''' <summary>Raised after the <see cref="Save"/> method does its work successfully</summary>
    Friend Event Saved(ByVal sender As IPTCInternal)
    '''' <summary>Contains value of the <see cref="Exif"/> property</summary>
    'Private _Exif As Exif
    '<Browsable(False)> _
    'Public Property Exif() As Exif
    '    Get
    '        Return _Exif
    '    End Get
    '    Set(ByVal value As Exif)

    '    End Set
    'End Property
End Class

'''' <summary>Argument of <see cref="IPTCInternal.ValueChanged"/> event</summary>
'Public Class TagChangedEventArgs : Inherits EventArgs
'    ''' <summary>Contains value of the <see cref="TagSource"/> property</summary>
'    Private ReadOnly _TagSource As TagSources
'    ''' <summary>CTor</summary>
'    ''' <param name="TagSource">Sets value of the <see cref="TagSource"/> property</param>
'    Friend Sub New(ByVal TagSource As TagSources)
'        Me._TagSource = TagSource
'    End Sub
'    ''' <summary>Source of tag that was changed</summary>
'    Public ReadOnly Property TagSource() As TagSources
'        Get
'            Return _TagSource
'        End Get
'    End Property
'    ''' <summary>Possible tag sources for the <see cref="TagSource"/> property</summary>
'    Public Enum TagSources
'        ''' <summary>IPTC (see <see cref="MetadataT.IPTC"/>)</summary>
'        IPTC
'        ''' <summary>Exif (see <see cref="MetadataT.Exif"/>)</summary>
'        Exif
'    End Enum
'End Class