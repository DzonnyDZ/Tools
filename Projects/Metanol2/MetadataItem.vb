Imports Tools.DrawingT.MetadataT
Imports System.ComponentModel, Tools.DrawingT.ImageTools

''' <summary>Represents <see cref="ListViewItem"/> which contains metedata for given file</summary>
Public NotInheritable Class MetadataItem : Inherits ListViewItem
#Region "CTors"
    ''' <exception cref="ArgumentNullException"><paramref name="FilePath"/> is null</exception>
    ''' <exception cref="ArgumentException">
    ''' <paramref name="FilePath"/> is an empty string or consists only of whitespace characters
    ''' -or-
    ''' <paramref name="FilePath"/> contains invalid character as defined in <see cref="System.IO.Path.GetInvalidPathChars"/>
    ''' </exception>
    Friend Sub New(ByVal FilePath As String)
        Me.New(New IOt.Path(FilePath))
    End Sub
    ''' <summary>CTor from path</summary>
    ''' <param name="FilePath">Path of file</param>
    ''' <exception cref="ArgumentNullException"><paramref name="FilePath"/> is null</exception>
    Friend Sub New(ByVal FilePath As IOt.Path)
        If FilePath Is Nothing Then Throw New ArgumentNullException("FilePath")
        Me._Path = FilePath
        With Path.FileName
            Me.Text = .self
            Me.ImageKey = .self
            Me.Name = .self
        End With
    End Sub
#End Region
    ''' <summary>Contains value of the <see cref="Path"/> property</summary>
    Private ReadOnly _Path As IOt.Path
    ''' <summary>Gets path of file this instance represents</summary>
    Public ReadOnly Property Path() As IOt.Path
        Get
            Return _Path
        End Get
    End Property
#Region "Metadata"
#Region "IPTC"
    ''' <summary>Contains value of the <see cref="IPTC"/> property</summary>
    Private WithEvents _IPTC As IPTCInternal
    ''' <summary>Gets or sets IPTC data for file represented by this instance</summary>
    Public Property IPTC() As IPTCInternal
        Get
            If IPTCContains Then
                Return _IPTC
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As IPTCInternal)
            _IPTC = value
            _IPTCContains = True
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="IPTCContains"/> and <see cref="IPTCLoaded"/> properties</summary>
    Private _IPTCContains As Boolean? = Nothing
    ''' <summary>Gets value idicating if this instance contains IPTC information</summary>
    Public ReadOnly Property IPTCContains() As Boolean
        Get
            If Not _IPTCContains.HasValue Then
                IPTCLoad(True)
            Else
                Return _IPTCContains
            End If
        End Get
    End Property
    ''' <summary>Gets value indicating if IPTC for this instance was altready loaded</summary>
    ''' <remarks>Getting <see cref="IPTC"/> or <see cref="IPTCContains"/> properties causes IPTC information to load automatically</remarks>
    Public ReadOnly Property IPTCLoaded() As Boolean
        Get
            Return _IPTCContains.HasValue
        End Get
    End Property
    ''' <summary>Loads (or reloads) IPTC information for this instance</summary>
    ''' <param name="SuppressExceptions">When true any exception thrown by CTor of <see cref="IPTCInternal"/> is suppressed and <see cref="IPTCContains"/> is set to false when an exception occurs.</param>
    ''' <remarks>This method is called automatically when <see cref="IPTC"/> or <see cref="IPTCContains"/> property is got for the first time.</remarks>
    Public Sub IPTCLoad(Optional ByVal SuppressExceptions As Boolean = False)
        Try
            Me.IPTC = New IPTCInternal(Me.Path)
        Catch ex As Exception
            _IPTCContains = False
            If Not SuppressExceptions Then Throw
        End Try
    End Sub
    Private Sub IPTC_Saved(ByVal sender As IPTCInternal) Handles _IPTC.Saved
        OnPartSaved(New PartEventArgs(Parts.IPTC))
    End Sub

    Private Sub IPTC_ValueChanged(ByVal sender As IPTCInternal, ByVal e As System.EventArgs) Handles _IPTC.ValueChanged
        OnChanged(New PartEventArgs(Parts.IPTC))
    End Sub
#End Region
#Region "Exif"
    ''' <summary>Contains value of the <see cref="IPTC"/> property</summary>
    Private WithEvents _Exif As ExifInternal
    ''' <summary>Gets or sets IPTC data for file represented by this instance</summary>
    Public Property Exif() As ExifInternal
        Get
            If ExifContains Then
                Return _Exif
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As ExifInternal)
            _Exif = value
            _ExifContains = True
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="IPTCContains"/> and <see cref="IPTCLoaded"/> properties</summary>
    Private _ExifContains As Boolean? = Nothing
    ''' <summary>Gets value idicating if this instance contains IPTC information</summary>
    Public ReadOnly Property ExifContains() As Boolean
        Get
            If Not _ExifContains.HasValue Then
                ExifLoad(False)
            Else
                Return _ExifContains
            End If
        End Get
    End Property
    ''' <summary>Gets value indicating if IPTC for this instance was altready loaded</summary>
    ''' <remarks>Getting <see cref="IPTC"/> or <see cref="IPTCContains"/> properties causes IPTC information to load automatically</remarks>
    Public ReadOnly Property ExifLoaded() As Boolean
        Get
            Return _ExifContains.HasValue
        End Get
    End Property
    ''' <summary>Loads (or reloads) IPTC information for this instance</summary>
    ''' <param name="SuppressExceptions">When true any exception thrown by CTor of <see cref="IPTCInternal"/> is suppressed and <see cref="IPTCContains"/> is set to false when an exception occurs.</param>
    ''' <remarks>This method is called automatically when <see cref="IPTC"/> or <see cref="IPTCContains"/> property is got for the first time.</remarks>
    Public Sub ExifLoad(Optional ByVal SuppressExceptions As Boolean = False)
        Try
            Me.Exif = New ExifInternal(Me.Path)
        Catch ex As Exception
            _ExifContains = False
            If Not SuppressExceptions Then Throw
        End Try
    End Sub
    Private Sub Exif_Saved(ByVal sender As ExifInternal) 'Handles _Exif.Saved
        OnPartSaved(New PartEventArgs(Parts.Exif))
    End Sub

    Private Sub Exif_ValueChanged(ByVal sender As ExifInternal, ByVal e As System.EventArgs) 'Handles _Exif.ValueChanged
        OnChanged(New PartEventArgs(Parts.Exif))
    End Sub
#End Region
#End Region
    ''' <summary>Gets value indicating if any part is in usaved state</summary>
    Public ReadOnly Property Changed() As Boolean
        Get
            'TODO: Exif
            'Try
            If IPTC Is Nothing Then Return False
            Return IPTC.Changed
            'Catch ex As NullReferenceException 'Why this happens?
            'Return False
            'End Try
        End Get
    End Property
    ''' <summary>Raised when any metadata part changes</summary>
    Public Event Change As EventHandler(Of MetadataItem, PartEventArgs)
    ''' <summary>Raised when any metadata pert is saved</summary>
    Public Event PartSaved As EventHandler(Of MetadataItem, PartEventArgs)
    ''' <summary>Event argumets with metadata pert identification</summary>
    Public NotInheritable Class PartEventArgs : Inherits EventArgs
        ''' <summary>Contains value of the <see cref="Source"/> property</summary>
        Private ReadOnly _Source As Parts
        ''' <summary>Identifies metadata part which is source of the event</summary>
        Public ReadOnly Property Source() As Parts
            Get
                Return _Source
            End Get
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="Source">Metadata pert which is originally source of the event</param>
        Friend Sub New(ByVal Source As Parts)
            Me._Source = Source
        End Sub
    End Class
    ''' <summary>Known metadata parts</summary>
    Public Enum Parts
        ''' <summary>IPTC</summary>
        IPTC = 1
        ''' <summary>Exif</summary>
        Exif = 2
    End Enum
    ''' <summary>Saves all changed metadata parts</summary>
    Public Sub Save()
        IPTC.Save()
    End Sub
    ''' <summary>Raises the <see cref="PartSaved"/> event</summary>
    ''' <param name="e">Event arguments</param>
    Protected Sub OnPartSaved(ByVal e As PartEventArgs)
        OnSaveStatusChanged()
        RaiseEvent PartSaved(Me, e)
    End Sub
    ''' <summary>Raises the <see cref="Change"/> event</summary>
    ''' <param name="e">Event arguments</param>
    Protected Sub OnChanged(ByVal e As PartEventArgs)
        OnSaveStatusChanged()
        RaiseEvent Change(Me, e)
    End Sub
    ''' <summary>Handles any possible change of the <see cref="Changed"/> property</summary>
    Private Sub OnSaveStatusChanged()
        Me.ListView.Invalidate(Me.Bounds)
    End Sub
#Region "Colors"
    ''' <summary><see cref="Selected"/> property</summary>
    ''' <remarks>Must be called from outside of this class. Changes are not tracked automatically.</remarks>
    Friend Sub OnSelectedChanged()
        If Me.Selected Then
            MyBase.BackColor = Me.SelectedBackColor
            MyBase.ForeColor = Me.SelectedForeColor
        Else
            MyBase.BackColor = Me.BackColor
            MyBase.ForeColor = Me.ForeColor
        End If
    End Sub
    ''' <summary>Contains value of the <see cref="ForeColor"/> property</summary>
    <EditorBrowsable()> Private _ForeColor As Color = SystemColors.WindowText
    ''' <summary>Contains value of the <see cref="BackColor"/> property</summary>
    <EditorBrowsable()> Private _BackColor As Color = Color.Transparent
    ''' <summary>Contains value of the <see cref="SelectedForeColor"/> property</summary>
    <EditorBrowsable()> Private _SelectedForeColor As Color = SystemColors.HighlightText
    ''' <summary>Contains value of the <see cref="SelectedBackColor"/> property</summary>
    <EditorBrowsable()> Private _SelectedBackColor As Color = SystemColors.Highlight
    ''' <summary>Gets or sets foreground color if item when it is not selected</summary>
    Public Shadows Property ForeColor() As Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As Color)
            Dim Old As Color = ForeColor
            _ForeColor = value
            If Old <> value AndAlso Not Selected Then OnSelectedChanged()
        End Set
    End Property
    ''' <summary>Gets or sets background color if item when it is not selected</summary>
    Public Shadows Property BackColor() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            Dim old As Color = BackColor
            _BackColor = value
            If old <> value AndAlso Not Selected Then OnSelectedChanged()
        End Set
    End Property
    ''' <summary>Gets or sets foreground color if item when it is selected</summary>
    Public Property SelectedForeColor() As Color
        Get
            Return _SelectedForeColor
        End Get
        Set(ByVal value As Color)
            Dim old As Color = SelectedForeColor
            _SelectedForeColor = value
            If old <> value AndAlso Selected Then OnSelectedChanged()
        End Set
    End Property
    ''' <summary>Gets or sets background color if item when it is selected</summary>
    Public Property SelectedBackColor() As Color
        Get
            Return _SelectedBackColor
        End Get
        Set(ByVal value As Color)
            Dim old As Color = SelectedBackColor
            _SelectedBackColor = value
            If old <> value AndAlso Selected Then OnSelectedChanged()
        End Set
    End Property
#End Region
    ''' <summary>Draws item in list view</summary>
    ''' <param name="e">Argument of the <see cref="ListView.DrawItem"/> event</param>
    ''' <remarks>Can be used as handler of the <see cref="ListView.DrawItem"/> event</remarks>
    ''' <exception cref="InvalidOperationException"><see cref="ListView"/> is null</exception>
    Public Sub Draw(ByVal e As DrawListViewItemEventArgs)
        If Me.ListView Is Nothing Then Throw New InvalidOperationException(My.Resources.CannotDrawItemWithoutListView)
        e.DrawDefault = True
        If (e.State And ListViewItemStates.Selected) = ListViewItemStates.Selected OrElse (e.State And ListViewItemStates.Focused) = ListViewItemStates.Focused Then
            e.DrawBackground()
            Dim ImageBounds = Me.GetBounds(ItemBoundsPortion.Icon)
            Dim Image As Image = If(Me.ImageList IsNot Nothing, Me.ImageList.Images(Me.ImageKey), Nothing)
            If Image IsNot Nothing Then
                'e.Graphics.DrawImageInPixels(Image, ImageBounds.X + ImageBounds.Width / 2 - Image.Width / 2, ImageBounds.Y + ImageBounds.Height / 2 - Image.Height / 2)
                e.Graphics.DrawImageUnscaled(Image, ImageBounds.X + ImageBounds.Width / 2 - Image.Width / 2, ImageBounds.Y + ImageBounds.Height / 2 - Image.Height / 2)
            End If
            If (e.State And ListViewItemStates.Focused) = ListViewItemStates.Focused Then
                Dim sf As New StringFormat()
                sf.Alignment = StringAlignment.Center
                If Me.ListView.RightToLeftLayout Then sf.FormatFlags = sf.FormatFlags Or StringFormatFlags.DirectionRightToLeft
                sf.LineAlignment = StringAlignment.Center
                sf.Trimming = StringTrimming.Character
                e.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(MyBase.ForeColor), Me.GetBounds(ItemBoundsPortion.Label), sf)
            Else
                Dim TextFlags As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.Bottom Or TextFormatFlags.EndEllipsis Or TextFormatFlags.SingleLine
                If Me.ListView.RightToLeftLayout Then TextFlags = TextFlags Or TextFormatFlags.RightToLeft
                e.DrawText(TextFlags)
            End If
            e.DrawFocusRectangle()
            e.DrawDefault = False
        End If
        If Changed Then _
            e.Graphics.DrawString("✹", Me.Font, New SolidBrush(MyBase.ForeColor), e.Bounds.Location)
    End Sub
End Class
