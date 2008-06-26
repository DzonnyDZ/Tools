Imports System.Linq, Tools.DrawingT, Tools.DataStructuresT.GenericT, Tools.CollectionsT.SpecializedT, Tools.IOt.FileTystemTools, Tools.CollectionsT.GenericT
Imports System.ComponentModel, Tools.WindowsT, Tools.ExtensionsT
Imports Tools.DrawingT.MetadataT, Tools.DrawingT.DrawingIOt
Imports System.Reflection
Imports MBox = Tools.WindowsT.IndependentT.MessageBox, MButton = Tools.WindowsT.IndependentT.MessageBox.MessageBoxButton
Imports Tools.WindowsT.FormsT
Partial Class frmMain
    ''' <summary>Extends <see cref="IPTC"/> with some extra stuff</summary>
    <DebuggerDisplay("{ImagePath}")> _
    Private Class IPTCInternal
        Inherits IPTC
        ''' <summary>CTor</summary>
        ''' <param name="ImagePath">Path of JPEG file</param>
        Public Sub New(ByVal ImagePath As String)
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
        ''' <summary>Gets value indicating if this instance is dirty</summary>
        ''' <returns>True if instance was changed since save/load</returns>
        <Browsable(False)> _
        Public ReadOnly Property Changed() As Boolean
            Get
                Return _Changed
            End Get
        End Property
        ''' <summary>Raised when value of any tag changes</summary>
        Public Event ValueChanged As EventHandler(Of IPTCInternal, EventArgs)
        ''' <summary>Gets or sets value of common property identified by value of <see cref="CommonProperties"/></summary>
        ''' <param name="Property">Property tpo get/set</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Property"/> is none of predefined <see cref="CommonProperties"/> values or it is <see cref="CommonProperties.None"/> or <see cref="CommonProperties.All"/>.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="Property"/> is <see cref="CommonProperties.Keywords"/></exception>
        Friend Property Common(ByVal [Property] As CommonProperties) As String
            Get
                Select Case [Property]
                    Case CommonProperties.Caption : Return CaptionAbstract
                    Case CommonProperties.City : Return City
                    Case CommonProperties.Copyright : Return CopyrightNotice
                    Case CommonProperties.Country : Return CountryPrimaryLocationName
                    Case CommonProperties.CountryCode : Return CountryPrimaryLocationCode
                    Case CommonProperties.Credit : Return Credit
                    Case CommonProperties.EditStatus : Return EditStatus
                    Case CommonProperties.Keywords : Throw New NotSupportedException(My.Resources.KeywordsAreNotSupportedByCommponProperty_internalError)
                    Case CommonProperties.ObjectName : Return ObjectName
                    Case CommonProperties.Province : Return ProvinceState
                    Case CommonProperties.Sublocation : Return SubLocation
                    Case CommonProperties.ObjectName : Return ObjectName
                    Case Else : Throw New InvalidEnumArgumentException("Property", [Property], [Property].GetType)
                End Select
            End Get
            Set(ByVal value As String)
                Select Case [Property]
                    Case CommonProperties.Caption : If value <> CaptionAbstract Then CaptionAbstract = value
                    Case CommonProperties.City : If value <> City Then City = value
                    Case CommonProperties.Copyright : If value <> CopyrightNotice Then CopyrightNotice = value
                    Case CommonProperties.Country : If value <> CountryPrimaryLocationName Then CountryPrimaryLocationName = value
                    Case CommonProperties.CountryCode : If value <> CountryPrimaryLocationCode Then CountryPrimaryLocationCode = value
                    Case CommonProperties.Credit : If value <> Credit Then Credit = value
                    Case CommonProperties.EditStatus : If value <> EditStatus Then EditStatus = value
                    Case CommonProperties.Keywords : Throw New NotSupportedException(My.Resources.KeywordsAreNotSupportedByCommponProperty_internalError)
                    Case CommonProperties.ObjectName : If value <> ObjectName Then ObjectName = value
                    Case CommonProperties.Province : If value <> ProvinceState Then ProvinceState = value
                    Case CommonProperties.Sublocation : If value <> SubLocation Then SubLocation = value
                    Case CommonProperties.Urgency : If value <> Urgency Then Urgency = value
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
    End Class
End Class