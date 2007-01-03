Imports System.Windows.Forms
#If Config <= Nightly Then 'Stage: Nightly
Namespace Windows.Forms
    ''' <summary><see cref="System.Windows.Forms.LinkLabel"/> with improved design-time behavior</summary>
    <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChange:="1/2/2007")> _
    Public Class LinkLabel : Inherits System.Windows.Forms.LinkLabel
        ''' <summary>Gets text currently displayed by this <see cref="LinkLabel"/></summary>
        ''' <value>Property is read-only, exception <see cref="NotSupportedException"/> will be thrown when trying to set it</value>
        ''' <exception cref="NotSupportedException">Trying to set this property</exception>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
        Public Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Set(ByVal value As String)
                Throw New NotSupportedException("Text cannot be changed via the Text property")
            End Set
        End Property
        ''' <summary>Gets the range in the text treated as a link.</summary>
        ''' <value>Property is read-only, exception <see cref="NotSupportedException"/> will be thrown when trying to set it</value>
        ''' <returns>A System.Windows.Forms.LinkArea that represents the area treated as a link.</returns>
        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)> _
        Public Shadows Property LinkArea() As LinkArea
            Get
                Return MyBase.LinkArea
            End Get
            Set(ByVal value As LinkArea)
                Throw New NotSupportedException("LinkArea cannot be set.")
            End Set
        End Property
#Region "Item classes"
        ''' <summary>Common base class for items of <see cref="LinkLabel"/></summary>
        <DebuggerDisplay("{ToString}"), DefaultProperty("Text")> _
        Public MustInherit Class LinkLabelItem
            ''' <summary>Text to be shown</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Text As String
            ''' <summary>Gets or sets text shown in place of this item</summary>
            <Description("Text to be show in place of this item"), Category("Appearance")> _
            <DefaultValue("")> _
            Public Overridable Property Text() As String
                Get
                    Return _Text
                End Get
                Set(ByVal value As String)
                    _Text = value
                End Set
            End Property
            ''' <summary>String representation of this instance</summary>
            Public Overrides Function ToString() As String
                Return Text
            End Function
        End Class

        ''' <summary>Non-link (text only) item of <see cref="LinkLabel"/></summary>
        Public Class TextItem : Inherits LinkLabelItem
            ''' <summary>CTor (initializes with an empty string)</summary>
            Sub New()
                Text = ""
            End Sub
            ''' <summary>CTor (initializes with text to display)</summary>
            ''' <param name="Text">Text to be displayed</param>
            Sub New(ByVal Text As String)
                Me.Text = Text
            End Sub
        End Class

        ''' <summary>Generic link</summary>
        Public Class LinkItem : Inherits LinkLabelItem
            ''' <summary>CTor (initializes with an empty string as <see cref="Text"/> and null as <see cref="LinkData"/></summary>
            Public Sub New()
                Me.New("")
            End Sub
            ''' <summary>CTor (initializes <see cref="Text"/> and optionally <see cref="LinkData"/></summary>
            ''' <param name="Text">Text to be shown</param>
            ''' <param name="LinkData">Data associtaed with new link</param>
            Public Sub New(ByVal Text As String, Optional ByVal LinkData As Object = Nothing)
                Me.Text = Text
                Me.LinkData = LinkData
            End Sub
            ''' <summary>Data associated with the link</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _LinkData As Object
            ''' <summary>Gets or sets data associated with the link</summary>
            <Category("Behavior"), Description("Data associated with this link")> _
            <DefaultValue("")> _
            Public Overridable Property LinkData() As Object
                Get
                    Return _LinkData
                End Get
                Set(ByVal value As Object)
                    _LinkData = value
                End Set
            End Property
        End Class

        ''' <summary>Link that performs navigation automatically</summary>
        <DebuggerDisplay("{ToString} ({LinkPath})")> _
        Public Class AutoLink : Inherits LinkItem
#Region "CTors"
            ''' <summary>CTor</summary>
            ''' <param name="Text">Text to display</param>
            Public Sub New(Optional ByVal Text As String = "")
                Me.Text = Text
                LinkURI = Nothing
            End Sub
            ''' <summary>CTor</summary>
            ''' <param name="Text">Text to display</param>
            ''' <param name="LinkURI">URI of target of new link</param>
            Public Sub New(ByVal Text As String, ByVal LinkURI As Uri)
                Me.Text = Text
                Me.LinkURI = LinkURI
            End Sub
            ''' <summary>CTor</summary>
            ''' <param name="Text">Text to display</param>
            ''' <param name="LinkPath">Path of target of new link</param>
            ''' <exception cref="System.ArgumentNullException"><paramref name="value"/> is null -and- type of <paramref name="value"/> is <see cref="String"/></exception>
            ''' <exception cref="System.UriFormatException">Type of <paramref name="value"/> is <see cref="String"/> -and- <paramref name="value"/> is empty.-or- The scheme specified in <paramref name="value"/> is not correctly formed. See <see cref="System.Uri.CheckSchemeName"/>.-or- <paramref name="value"/> contains too many slashes.-or- The password specified in <paramref name="value"/> is not valid.-or- The host name specified in <paramref name="value"/> is not valid.-or- The file name specified in <paramref name="value"/> is not valid. -or- The user name specified in <paramref name="value"/> is not valid.-or- The host or authority name specified in <paramref name="value"/> cannot be terminated by backslashes.-or- The port number specified in <paramref name="value"/> is not valid or cannot be parsed.-or- The length of <paramref name="value"/> exceeds 65534 characters.-or- The length of the scheme specified in <paramref name="value"/> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="value"/>.-or- The MS-DOS path specified in <paramref name="value"/> must start with c:\\.</exception>
            Public Sub New(ByVal Text As String, ByVal LinkPath As String)
                Me.Text = Text
                Me.LinkPath = LinkPath
            End Sub
            ''' <summary>CTor - initializes with URI and uses URI's string representation as text</summary>
            ''' <param name="LinkURI">URI of terger of new link</param>
            Public Sub New(ByVal LinkURI As Uri)
                Me.New(LinkURI.ToString, LinkURI)
            End Sub
#End Region
            ''' <summary>Target of link</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _LinkURI As Uri
            ''' <summary>Data associated with the link</summary>
            ''' <value>New associated data (NOTE: value must be of type <see cref="Uri"/> or of type <see cref="String"/> that cab be used as parameter of <see cref="Uri"/>'s CTor)</value>
            ''' <returns>Data associated with the link</returns>
            ''' <exception cref="System.ArgumentNullException"><paramref name="value"/> is null -and- type of <paramref name="value"/> is <see cref="String"/></exception>
            ''' <exception cref="System.UriFormatException">Type of <paramref name="value"/> is <see cref="String"/> -and- <paramref name="value"/> is empty.-or- The scheme specified in <paramref name="value"/> is not correctly formed. See <see cref="System.Uri.CheckSchemeName"/>.-or- <paramref name="value"/> contains too many slashes.-or- The password specified in <paramref name="value"/> is not valid.-or- The host name specified in <paramref name="value"/> is not valid.-or- The file name specified in <paramref name="value"/> is not valid. -or- The user name specified in <paramref name="value"/> is not valid.-or- The host or authority name specified in <paramref name="value"/> cannot be terminated by backslashes.-or- The port number specified in <paramref name="value"/> is not valid or cannot be parsed.-or- The length of <paramref name="value"/> exceeds 65534 characters.-or- The length of the scheme specified in <paramref name="value"/> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="value"/>.-or- The MS-DOS path specified in <paramref name="value"/> must start with c:\\.</exception>
            ''' <exception cref="InvalidCastException">Type of <paramref name="value"/> is neither <see cref="Uri"/> nor <see cref="String"/></exception>
            <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type safe variant (LinkURI or LinkPath) instead")> _
            <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            Public Overrides Property LinkData() As Object
                Get
                    Return MyBase.LinkData
                End Get
                Set(ByVal value As Object)
                    If TypeOf value Is Uri Then
                        LinkURI = value
                    ElseIf TypeOf value Is String Then
                        LinkPath = value
                    Else
                        Throw New InvalidCastException("Value can be converter neither to Uri nor to String")
                    End If
                End Set
            End Property
            ''' <summary>Gets or sets URI to navigate to</summary>
            ''' <value>Actuall URI or target of the link</value>
            ''' <returns>New URI of target of the link</returns>
            <Category("Behavior"), Description("URI of target of the link")> _
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            <DefaultValue("")> _
            Public Property LinkURI() As Uri
                Get
                    Return _LinkURI
                End Get
                Set(ByVal value As Uri)
                    _LinkURI = value
                    MyBase.LinkData = _LinkURI
                End Set
            End Property
            ''' <summary>Gets or sets URI (in form of path string) to navigate to</summary>
            ''' <returns>Actuall path of target of the link</returns>
            ''' <value>New path of target of the link</value>
            ''' <exception cref="System.UriFormatException"><paramref name="value"/> is empty.-or- The scheme specified in <paramref name="value"/> is not correctly formed. See <see cref="System.Uri.CheckSchemeName"/>.-or- <paramref name="value"/> contains too many slashes.-or- The password specified in <paramref name="value"/> is not valid.-or- The host name specified in <paramref name="value"/> is not valid.-or- The file name specified in <paramref name="value"/> is not valid. -or- The user name specified in <paramref name="value"/> is not valid.-or- The host or authority name specified in <paramref name="value"/> cannot be terminated by backslashes.-or- The port number specified in <paramref name="value"/> is not valid or cannot be parsed.-or- The length of <paramref name="value"/> exceeds 65534 characters.-or- The length of the scheme specified in <paramref name="value"/> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="value"/>.-or- The MS-DOS path specified in <paramref name="value"/> must start with c:\\.</exception>
            ''' <remarks>Exceptions thrown by <see cref="Uri"/>'s CTor</remarks>
            <Category("Behavior"), Description("Path (string representation of URI) of target of the link")> _
            <DefaultValue("")> _
            Public Property LinkPath() As String
                Get
                    Return _LinkURI.ToString
                End Get
                Set(ByVal value As String)
                    If value = "" Then
                        _LinkURI = Nothing
                    Else
                        _LinkURI = New Uri(value)
                    End If
                    MyBase.LinkData = _LinkURI
                End Set
            End Property
        End Class
#End Region
    End Class
End Namespace
#End If