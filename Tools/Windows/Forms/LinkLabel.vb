Imports System.Windows.Forms, Tools.Collections.Generic
#If Config <= Alpha Then 'Stage: Alpha
Namespace Windows.Forms
    ''' <summary><see cref="System.Windows.Forms.LinkLabel"/> with improved design-time behavior</summary>
    <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChange:="1/2/2007")> _
    Public Class LinkLabel : Inherits System.Windows.Forms.LinkLabel
        ''' <summary>Contains value of the <see cref="Items"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private WithEvents _Items As New ListWithEvents(Of LinkLabelItem)(True)
        ''' <summary>CTor</summary>
        Public Sub New()
            Items.AllowAddCancelableEventsHandlers = False
        End Sub
        ''' <summary>Gets text currently displayed by this <see cref="LinkLabel"/></summary>
        ''' <value>Property is read-only, exception <see cref="NotSupportedException"/> will be thrown when trying to set it</value>
        ''' <exception cref="NotSupportedException">Trying to set this property</exception>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
        Public NotOverridable Overrides Property Text() As String
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
        ''' <summary>List of all items in label</summary>
        ''' <remarks><see cref="ListWithEvents(Of LinkLabelItem).AllowAddCancelableEventsHandlers"/> is set to False</remarks>
        <Category("Appearance"), Description("List of all items in label")> _
        Public ReadOnly Property Items() As ListWithEvents(Of LinkLabelItem)
            Get
                Return _Items
            End Get
        End Property
#Region "List event handlers"
        Private Sub _Items_Added(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).ItemIndexEventArgs) Handles _Items.Added
            RegenerateContent()
        End Sub

        Private Sub _Items_Cleared(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).CountEventArgs) Handles _Items.Cleared
            RegenerateContent()
        End Sub

        Private Sub _Items_ItemChanged(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).ItemIndexEventArgs) Handles _Items.ItemChanged
            RegenerateContent()
        End Sub

        Private Sub _Items_ItemValueChanged(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).ItemValueChangedEventArgs) Handles _Items.ItemValueChanged
            RegenerateContent()
        End Sub

        Private Sub _Items_Removed(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).ItemIndexEventArgs) Handles _Items.Removed
            RegenerateContent()
        End Sub
        ''' <summary>Regenerates content of <see cref="LinkLabel"/></summary>
        ''' <remarks>Called by handlers of events of the <see cref="Items"/> <see cref="ListWithEvents(Of LinkLabelItem)"/></remarks>
        Protected Overridable Sub RegenerateContent()
            MyBase.Text = ""
            Dim tb As New System.Text.StringBuilder
            For Each itm As LinkLabelItem In Items
                tb.Append(itm.Text)
            Next itm
            MyBase.Text = tb.ToString
            Dim start As Integer = 0
            For Each itm As LinkLabelItem In Items
                If TypeOf itm Is LinkItem Then
                    MyBase.Links.Add(start, itm.Text.Length, itm)
                End If
                start += itm.Text.Length
            Next itm
        End Sub
#End Region
        ''' <summary>Raises the <see cref="System.Windows.Forms.LinkLabel.LinkClicked"/> event.</summary>
        ''' <param name="e">A <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> that contains the event data.</param>
        ''' <remarks>Note for inheritors: Call base class <see cref="OnLinkClicked"/> in order to raise <see cref="LinkClicked"/> and <see cref="System.Windows.Forms.LinkLabel.LinkClicked"/> events and <see cref="AutoLink"/> to be followed</remarks>
        Protected Overrides Sub OnLinkClicked(ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
            MyBase.OnLinkClicked(e)
            RaiseEvent LinkClicked(Me, New LinkClickedEventArgs(e.Link, e.Button))
            If TypeOf e.Link.LinkData Is AutoLink AndAlso (e.Button = System.Windows.Forms.MouseButtons.Left OrElse e.Button = System.Windows.Forms.MouseButtons.None) AndAlso CType(e.Link.LinkData, AutoLink).LinkURI IsNot Nothing Then
                Process.Start(CType(e.Link.LinkData, AutoLink).LinkPath)
                e.Link.Visited = True
            End If
        End Sub
        ''' <summary>Raised after link is clicked</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters (contains info about link and item being clicked)</param>
        ''' <remarks><list type="1">
        ''' <listheader>The order of events is following</listheader>
        ''' <item><see cref="System.Windows.Forms.LinkLabel.LinkClicked"/></item>
        ''' <item><see cref="LinkClicked"/></item>
        ''' <item>If the item being clicked is <see cref="AutoLink"/> and its <see cref="AutoLink.LinkURI"/> is not null and <see cref="LinkClickedEventArgs.Button"/> is <see cref="System.Windows.Forms.MouseButtons.Left"/> or <see cref="System.Windows.Forms.MouseButtons.None"/> then Uri <see cref="AutoLink.LinkURI"/> is opened via <see cref="Process.Start"/></item>
        ''' </list>
        ''' </remarks>
        Public Shadows Event LinkClicked(ByVal sender As LinkLabel, ByVal e As LinkClickedEventArgs)

        ''' <summary>Arguments of the <see cref="LinkClicked"/> event</summary>
        Public Class LinkClickedEventArgs : Inherits LinkLabelLinkClickedEventArgs
            ''' <summary>Contains value of the <see cref="Item"/> property</summary>
            <EditorBrowsable()> _
            Private ReadOnly _Item As LinkLabelItem
            ''' <summary>Contains va lue of the <see cref="LinkURI"/> property</summary>
            <EditorBrowsable()> _
            Private ReadOnly _LinkURI As Uri = Nothing
            ''' <summary>CTor</summary>
            ''' <param name="link"><see cref="Link"/> that was clicked</param>
            ''' <param name="button">The mouse button used to click</param>
            Public Sub New(ByVal link As Link, ByVal button As MouseButtons)
                MyBase.New(link, button)
                _Item = link.LinkData
                If TypeOf link.LinkData Is AutoLink Then _LinkURI = CType(link.LinkData, AutoLink).LinkURI
            End Sub
            ''' <summary>The item that was clicked</summary>
            Public ReadOnly Property Item() As LinkLabelItem
                Get
                    Return _Item
                End Get
            End Property
            ''' <summary>In case the item is of the type <see cref="AutoLink"/> then contains value of the <see cref="AutoLink.LinkURI"/> of the item.</summary>
            Public ReadOnly Property LinkURI() As Uri
                Get
                    Return _LinkURI
                End Get
            End Property
        End Class

        ''' <summary>Performs click event on <see cref="LinkLabel"/>. Causes raising the <see cref="Click"/> event</summary>
        ''' <remarks><see cref="LinkLabel.MouseDown"/>, <see cref="LinkLabel.MouseUp"/> and <see cref="MouseClick"/> events are not raised</remarks>
        Public Overridable Sub PerformClick()
            OnClick(EventArgs.Empty)
        End Sub
        ''' <summary>Perform click event on specified item of <see cref="LinkLabel"/></summary>
        ''' <param name="Item">Item on which perform the click</param>
        ''' <param name="button">Mouse button to be simulated</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Item"/> is null</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Item"/> not found in the <see cref="Items"/> collection</exception>
        ''' <exception cref="InvalidOperationException">
        ''' <paramref name="Item"/> found in the <see cref="Items"/> colletion but not found as <see cref="Link.LinkData"/> in the <see cref="Links"/> collection. Note: This is internal error of <see cref="LinkLabel"/> and thus should not be thrown if <see cref="LinkLabel"/> is implemented properly.
        ''' Do not change value of <see cref="Link.LinkData"/> or this exception will be thrown though there is no bug in <see cref="LinkLabel"/>.
        ''' </exception>
        ''' <remarks>
        ''' <para>Causes raising <see cref="Click"/> and <see cref="LinkClicked"/> events</para>
        ''' <para><see cref="LinkLabel.MouseDown"/>, <see cref="LinkLabel.MouseUp"/> and <see cref="MouseClick"/> events are not raised</para>
        ''' </remarks>
        Public Overridable Sub PerformClick(ByVal Item As LinkItem, Optional ByVal button As MouseButtons = System.Windows.Forms.MouseButtons.None)
            If Item Is Nothing Then Throw New ArgumentNullException("Item", "Item cannot be null")
            If Items.Contains(Item) Then
                For Each l As Link In MyBase.Links
                    If l.LinkData Is Item Then
                        OnLinkClicked(New LinkLabelLinkClickedEventArgs(l, button))
                        Return
                    End If
                Next l
                Throw New InvalidOperationException("LinkLabel internal exception: Link for item not found")
            Else
                Throw New ArgumentOutOfRangeException("Item", "Cannot locate Item in Items")
            End If
        End Sub

        ''' <summary>Gets the collection of links contained within the <see cref="LinkLabel"/>.</summary>
        ''' <remarks>
        ''' <para>This shadowes property is read-only. Do not use unshadowing workarounds to obtain read-write acces to te <see cref="System.Windows.Forms.LinkLabel.Links"/> property - it will cause unxpected behaviour. Use the <see cref="Items"/> collection instead.</para>
        ''' <para>Note that any change to the <see cref="Items"/> collections causes complete change of <see cref="Links"/> collection</para>
        ''' <para>Do not change <see cref="Link.Start"/> or <see cref="Link.Length"/> properties unless you know what you are doing.</para>
        ''' </remarks>
        <Browsable(False)> _
        Public Shadows ReadOnly Property Links() As IReadOnlyList(Of Link)
            Get
                Return New ReadOnlyListAdapter(Of Link)(New List(Of Link)(MyBase.Links))
            End Get
        End Property
        ''' <summary>Gets acces to link associated with <see cref="LinkItem"/> contained in <see cref="Items"/></summary>
        ''' <param name="Item">Item which link to obtain</param>
        ''' <returns><see cref="Link"/> which <paramref name="Item"/> is represented by</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Item"/> not found in <see cref="Items"/></exception>
        ''' <exception cref="InvalidOperationException">
        ''' <paramref name="Item"/> found in the <see cref="Items"/> colletion but not found as <see cref="Link.LinkData"/> in the <see cref="Links"/> collection. Note: This is internal error of <see cref="LinkLabel"/> and thus should not be thrown if <see cref="LinkLabel"/> is implemented properly.
        ''' Do not change value of <see cref="Link.LinkData"/> or this exception will be thrown though there is no bug in <see cref="LinkLabel"/>.</exception>
        ''' <remarks>
        ''' <para>Do not make any changes in the <see cref="Items"/> collection after modyfiyng any link or after creating reference to it. Also do not change values of items contained in the <see cref="Items"/> collection. These changes causes invalidation of all links.</para>
        ''' <para>Do not change <see cref="Link.Start"/> or <see cref="Link.Length"/> properties unless you know what you are doing.</para>
        ''' </remarks>
        <Browsable(False)> _
        Public Overridable Shadows ReadOnly Property Links(ByVal Item As LinkItem) As Link
            Get
                If Items.Contains(Item) Then
                    For Each l As Link In MyBase.Links
                        If l.LinkData Is Item Then Return l
                    Next l
                    Throw New InvalidOperationException("LinkLabel internal error: Item found in Items but not found in Links")
                Else
                    Throw New ArgumentOutOfRangeException("Item", "Item not found in Items")
                End If
            End Get
        End Property

#Region "Item classes"

        <DebuggerDisplay("{ToString}"), DefaultProperty("Text")> _
        Public MustInherit Class LinkLabelItem : Implements IReportsChange
            ''' <summary>Text to be shown</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Text As String
            ''' <summary>Gets or sets text shown in place of this item</summary>
            ''' <remarks>Note for inheritors: Call <see cref="OnChanged"/> after change of value (unless calling base class setter <see cref="Text"/>)</remarks>
            <Description("Text to be show in place of this item"), Category("Appearance")> _
            <DefaultValue("")> _
            Public Overridable Property Text() As String
                Get
                    Return _Text
                End Get
                Set(ByVal value As String)
                    Dim OldVal As String = Text
                    _Text = value
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of String)(OldVal, value, "Text"))
                End Set
            End Property
            ''' <summary>String representation of this instance</summary>
            Public Overrides Function ToString() As String
                Return Text
            End Function
            ''' <summary>Raised when value of member changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Event information</param>
            ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code</remarks>
            Public Event Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Implements IReportsChange.Changed
            ''' <summary>Raises the <see cref="Changed"/> event</summary>
            ''' <param name="e">Event parameters</param>
            ''' <remarks>Note for inheritors: Always call base class <see cref="OnChanged"/> method in order the event to be raised</remarks>
            Public Overridable Sub OnChanged(ByVal e As EventArgs)
                RaiseEvent Changed(Me, e)
            End Sub
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
            ''' <remarks>Note for inheritors: Call <see cref="OnChanged"/> after tha value is changed (unless calling base class setter <see cref="LinkData"/>)</remarks>
            <Category("Behavior"), Description("Data associated with this link")> _
            <DefaultValue("")> _
            Public Overridable Property LinkData() As Object
                Get
                    Return _LinkData
                End Get
                Set(ByVal value As Object)
                    Dim old As Object = LinkData
                    _LinkData = value
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, "LinkData"))
                End Set
            End Property
            Private _Description As String
            Private _Name As String
            Private _Tag As Object
            Private _Visited As Boolean
            Private _Enabled As Boolean

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
            ''' <remarks>Note for inheritors: Call <see cref="OnChanged"/> after value is changed (unless calling base class setter <see cref="LinkURI"/>)</remarks>
            <Category("Behavior"), Description("URI of target of the link")> _
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            <DefaultValue("")> _
            Public Overridable Property LinkURI() As Uri
                Get
                    Return _LinkURI
                End Get
                Set(ByVal value As Uri)
                    Dim old As Uri = LinkURI
                    _LinkURI = value
                    MyBase.LinkData = _LinkURI
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of Uri)(old, value, "LinkURI"))
                End Set
            End Property
            ''' <summary>Gets or sets URI (in form of path string) to navigate to</summary>
            ''' <returns>Actuall path of target of the link</returns>
            ''' <value>New path of target of the link</value>
            ''' <exception cref="System.UriFormatException"><paramref name="value"/> is empty.-or- The scheme specified in <paramref name="value"/> is not correctly formed. See <see cref="System.Uri.CheckSchemeName"/>.-or- <paramref name="value"/> contains too many slashes.-or- The password specified in <paramref name="value"/> is not valid.-or- The host name specified in <paramref name="value"/> is not valid.-or- The file name specified in <paramref name="value"/> is not valid. -or- The user name specified in <paramref name="value"/> is not valid.-or- The host or authority name specified in <paramref name="value"/> cannot be terminated by backslashes.-or- The port number specified in <paramref name="value"/> is not valid or cannot be parsed.-or- The length of <paramref name="value"/> exceeds 65534 characters.-or- The length of the scheme specified in <paramref name="value"/> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="value"/>.-or- The MS-DOS path specified in <paramref name="value"/> must start with c:\\.</exception>
            ''' <remarks>
            ''' <para>Exceptions thrown by <see cref="Uri"/>'s CTor</para>
            ''' <para>Note for inheritors: Call <see cref="OnChanged"/> (unless callin base class setter <see cref="LinkPath"/> or <see cref="LinkURI"/></para>
            ''' <para>Change of this value causes raising <see cref="Changed"/> event with <see cref="IReportsChange.ValueChangedEventArgs(Of Uri).ValueName"/> set to "LinkURI"</para>
            ''' </remarks>
            <Category("Behavior"), Description("Path (string representation of URI) of target of the link")> _
            <DefaultValue("")> _
            Public Property LinkPath() As String
                Get
                    Return _LinkURI.ToString
                End Get
                Set(ByVal value As String)
                    Dim old As Uri = LinkURI
                    If value = "" Then
                        _LinkURI = Nothing
                        OnChanged(New IReportsChange.ValueChangedEventArgs(Of Uri)(old, Nothing, "LinkURI"))
                    Else
                        _LinkURI = New Uri(value)
                        OnChanged(New IReportsChange.ValueChangedEventArgs(Of Uri)(old, LinkURI, "LinkURI"))
                    End If
                    MyBase.LinkData = _LinkURI
                End Set
            End Property
        End Class
#End Region
    End Class
End Namespace
#End If