Imports System.ComponentModel
Imports Tools.ReportingT.ReportingEngineLite.ReportingResources
Imports System.Xml.Serialization
Imports Tools.ComponentModelT

Namespace ReportingT.ReportingEngineLite
    ''' <summary>Settings of <see cref="CsvTemplate"/></summary>
    <System.ComponentModel.TypeDescriptionProvider(GetType(ComponentModelT.SingleTypeDescriptionProvider(Of CsvTemplateSettings, CsvTemplateSettings.CsvTemplateSettingsTypeDescriptor)))> _
    Partial Public Class CsvTemplateSettings
        Implements ICloneable

        ''' <summary>Gets or sets column separator in human-friendly way</summary>
        ''' <value>Separator, same values as <see cref="separator_"/>, only \t instead of tab and \s instead of space.</value>
        ''' <remarks><see cref="separator_"/>. If it is tab returns \t, if spare \s and if it is "System" returns <see cref="Globalization.TextInfo.ListSeparator"/>.</remarks>
        <XmlIgnore()> _
        Public Property Separator() As Char
            Get
                Select Case separator_
                    Case "\t" : Return vbTab
                    Case "\s" : Return " "c
                    Case "System" : Return GetCulture.TextInfo.ListSeparator
                    Case Else : Return separator_
                End Select
            End Get
            Set(ByVal value As Char)
                Select Case value
                    Case vbTab : separator_ = "\t"
                    Case " "c : separator_ = "\s"
                    Case Else : separator_ = value
                End Select
            End Set
        End Property
        ''' <summary>Called by CTor</summary>
        Private Sub OnAfterInit()
            Me.Separator = ","c
            Me.textqualifier = """"c
        End Sub
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function Clone() As CsvTemplateSettings
            Dim s As New Xml.Serialization.XmlSerializer(Me.GetType)
            Dim str As New IO.MemoryStream
            s.Serialize(str, Me)
            str.Position = 0
            Return s.Deserialize(str)
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function

#Region "Changing"
        Private Sub CSVTemplateSettings_cultureChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.cultureChanging
            If e.ProposedNewValue = "" OrElse e.ProposedNewValue = "System" OrElse e.ProposedNewValue = "Current" OrElse e.ProposedNewValue = "CurrentUI" OrElse e.ProposedNewValue = "Invariant" Then Return
            Dim c As New Globalization.CultureInfo(e.ProposedNewValue)
        End Sub

        Private Sub CSVTemplateSettings_dateformatChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.dateformatChanging
            Now.ToString(e.ProposedNewValue)
        End Sub

        Private Sub CSVTemplateSettings_encodingChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.encodingChanging
            If e.ProposedNewValue = "" OrElse e.ProposedNewValue = "System" Then Return
            System.Text.Encoding.GetEncoding(e.ProposedNewValue)
        End Sub

        Private Sub CSVTemplateSettings_headersizeChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.headersizeChanging
            If e.ProposedNewValue < 0 Then Throw New ArgumentOutOfRangeException("Velikost záklaví nemùže být záporná")
        End Sub

        Private Sub CSVTemplateSettings_newlineChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of CSVTemplateSettingsNewline)) Handles Me.newlineChanging
            If Not Tools.TypeTools.IsDefined(e.ProposedNewValue) Then Throw New System.ComponentModel.InvalidEnumArgumentException("value", e.ProposedNewValue, e.ProposedNewValue.GetType)
        End Sub

        Private Sub CSVTemplateSettings_nlescapeChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of CSVTemplateSettingsNlescape)) Handles Me.nlescapeChanging
            If Not Tools.TypeTools.IsDefined(e.ProposedNewValue) Then Throw New System.ComponentModel.InvalidEnumArgumentException("value", e.ProposedNewValue, e.ProposedNewValue.GetType)
        End Sub

        Private Sub CSVTemplateSettings_numberformatChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of String)) Handles Me.numberformatChanging
            Dim s$ = (14.5D).ToString(e.ProposedNewValue)
        End Sub

        Private Sub CSVTemplateSettings_textqualifierescapeChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of CSVTemplateSettingsTextqualifierescape)) Handles Me.textqualifierescapeChanging
            If Not Tools.TypeTools.IsDefined(e.ProposedNewValue) Then Throw New System.ComponentModel.InvalidEnumArgumentException("value", e.ProposedNewValue, e.ProposedNewValue.GetType)
        End Sub

        Private Sub CSVTemplateSettings_textqualifierusageChanging(ByVal sender As Object, ByVal e As PropertyChangingEventArgsEx(Of CSVTemplateSettingsTextqualifierusage)) Handles Me.textqualifierusageChanging
            If Not Tools.TypeTools.IsDefined(e.ProposedNewValue) Then Throw New System.ComponentModel.InvalidEnumArgumentException("value", e.ProposedNewValue, e.ProposedNewValue.GetType)
        End Sub
#End Region
        ''' <summary>Gets encoding from <see cref="encoding"/></summary>
        Public Function GetEncoding() As System.Text.Encoding
            If Me.encoding = "" Or Me.encoding = "System" Then Return System.Text.Encoding.Default
            Return System.Text.Encoding.GetEncoding(Me.encoding)
        End Function

        ''' <summary>Get culture from <see cref="culture"/></summary>
        Public Function GetCulture() As Globalization.CultureInfo
            If Me.culture = "" OrElse Me.culture = "Invariant" Then Return Globalization.CultureInfo.InvariantCulture
            If Me.culture = "Current" Then Return Globalization.CultureInfo.CurrentCulture
            If Me.culture = "CurrentUI" Then Return Globalization.CultureInfo.CurrentUICulture
            If Me.culture = "System" Then Return Globalization.CultureInfo.InstalledUICulture
            Return New Globalization.CultureInfo(Me.culture)
        End Function
        ''' <summary>Get new line string from <see  cref="newline"/></summary>
        Public Function GetNewLine() As String
            Select Case Me.newline
                Case CSVTemplateSettingsNewline.CarriageReturn : Return vbCr
                Case CSVTemplateSettingsNewline.CrLf : Return vbCrLf
                Case CSVTemplateSettingsNewline.LineFeed : Return vbLf
                Case CSVTemplateSettingsNewline.FormFeed : Return ChrW(&HC)
                Case CSVTemplateSettingsNewline.LineSeparator : Return ChrW(&H2028)
                Case CSVTemplateSettingsNewline.NextLine : Return ChrW(&H85)
                Case CSVTemplateSettingsNewline.ParagraphSeparator : Return ChrW(&H2029)
                Case CSVTemplateSettingsNewline.System : Return Environment.NewLine
                Case Else : Throw New InvalidOperationException(My.Resources.ex_NewLineInvalidValue)
            End Select
        End Function

#Region "TypeDescriptor"

        ''' <summary>Describes the <see cref="SimpleXlsSettings"/> type</summary>
        Friend Class CsvTemplateSettingsTypeDescriptor
            Inherits CustomTypeDescriptor
            Private ReadOnly instance As CsvTemplateSettings
            ''' <summary>CTor - creates a new instance of the <see cref="CSVTemplateSettingsTypeDescriptor"/> class</summary>
            ''' <param name="instance">An instance of <see cref="CSVTemplateSettings"/> to describe</param>
            ''' <param name="Parent">Original <see cref="ICustomTypeDescriptor"/></param>
            Public Sub New(ByVal instance As CsvTemplateSettings, ByVal Parent As ICustomTypeDescriptor)
                MyBase.New(Parent)
                Me.instance = instance
            End Sub
            ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
            ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
            Public Overrides Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection
                Return ChangeProperties(MyBase.GetProperties())
            End Function
            ''' <summary>Alters property attributes</summary>
            ''' <param name="properties">Properties to change attributes of</param>
            ''' <returns><paramref name="properties"/> with attributes changed</returns>
            Private Function ChangeProperties(ByVal properties As PropertyDescriptorCollection) As PropertyDescriptorCollection
                Dim ret As New List(Of PropertyDescriptor)
                For Each prp As PropertyDescriptor In properties
                    Select Case prp.Name
                        Case "encoding"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_Encoding),
                                New DescriptionAttribute(My.Resources.dn_EncodingCsv),
                                New CategoryAttribute(My.Resources.cat_Basic)}))
                        Case "culture"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_Culture),
                                New DescriptionAttribute(My.Resources.d_Culture),
                                New CategoryAttribute(My.Resources.cat_Format)}))
                        Case "numberformat"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_NumberFormat),
                                New DescriptionAttribute(My.Resources.d_NumberFormat),
                                New CategoryAttribute(My.Resources.cat_Format)}))
                        Case "dateformat"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_DateFormat),
                                New DescriptionAttribute(My.Resources.d_DateFormat),
                                New CategoryAttribute(My.Resources.cat_Format)}))
                        Case "Separator"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_Separator),
                                New DescriptionAttribute(My.Resources.d_Separator),
                                New CategoryAttribute(My.Resources.cat_Basic)}))
                        Case "textqualifier"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_TextQualifier),
                                New DescriptionAttribute(My.Resources.d_TextQualifier),
                                New CategoryAttribute(My.Resources.cat_Basic)}))
                        Case "textqualifierusage"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_TextQualifierUsage),
                                New DescriptionAttribute(My.Resources.d_TextQualifierUsage),
                                New CategoryAttribute(My.Resources.cat_Basic)}))
                        Case "textqualifierescape"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_TextQualifierEscape),
                                New DescriptionAttribute(My.Resources.d_TextQualifierEscape),
                                New CategoryAttribute(My.Resources.cat_SpecialCases)}))
                        Case "newline"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.d_NewLine),
                                New DescriptionAttribute(My.Resources.dn_NewLine),
                                New CategoryAttribute(My.Resources.cat_Basic)}))
                        Case "nlescape"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_NlEscape),
                                New DescriptionAttribute(My.Resources.d_NlEscape),
                                New CategoryAttribute(My.Resources.cat_SpecialCases)}))
                        Case "specialstring"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_SpecialString),
                                New DescriptionAttribute(My.Resources.d_SpecialString),
                                New CategoryAttribute(My.Resources.cat_SpecialCases)}))
                        Case "header"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_Header),
                                New DescriptionAttribute(My.Resources.d_Header),
                                New CategoryAttribute(My.Resources.cat_Basic)}))
                        Case "footer"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_Footer),
                                New DescriptionAttribute(My.Resources.d_Footer),
                                New CategoryAttribute(My.Resources.cat_Basic)}))
                        Case "headersize"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_HeaderSize),
                                New DescriptionAttribute(My.Resources.d_HeaderSize),
                                New CategoryAttribute(My.Resources.cat_Template)}))
                        Case "nullvalue"
                            ret.Add(New WrapPropertyDescriptor(prp, {
                                New DisplayNameAttribute(My.Resources.dn_NullValue),
                                New DescriptionAttribute(My.Resources.d_NullValue),
                                New CategoryAttribute(My.Resources.cat_SpecialCases)}))
                        Case Else : ret.Add(prp)
                    End Select
                Next
                Return New PropertyDescriptorCollection(ret.ToArray)
            End Function
            ''' <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
            ''' <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
            Public Overrides Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
                Return ChangeProperties(MyBase.GetProperties(attributes))
            End Function
            ''' <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
            ''' <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.</returns>
            Public Overrides Function GetAttributes() As System.ComponentModel.AttributeCollection
                Dim ret As New List(Of Attribute)
                For Each attr As Attribute In MyBase.GetAttributes()
                    ret.Add(attr)
                Next
                ret.Add(New DefaultPropertyAttribute("Name"))
                Return New AttributeCollection(ret.ToArray)
            End Function
        End Class
#End Region

        ''' <summary>Fills Excel-friendly values to this instance</summary>
        ''' <seelaso cref="ExcelFriendly"/>
        Public Sub MakeExcelFriendly()
            With Me
                .culture = "System"
                .dateformat = Nothing
                .timeformat = Nothing
                .encoding = "System"
                .newline = "System"
                .nlescape = CSVTemplateSettingsNlescape.donothing
                .numberformat = Nothing
                .Separator = "System"
                .textqualifier = """"c
                .textqualifierescape = CSVTemplateSettingsTextqualifierescape.double
                .textqualifierusage = CSVTemplateSettingsTextqualifierusage.asneeded
            End With
        End Sub

        ''' <summary>Gets a new instance of CSV template settings preffiled with Excel-friendly values</summary>
        ''' <seelaso cref="MakeExcelFriendly"/>
        Public Shared ReadOnly Property ExcelFriendly As CsvTemplateSettings
            Get
                Dim ret As New CsvTemplateSettings
                ret.MakeExcelFriendly()
                Return ret
            End Get
        End Property
    End Class
End Namespace
