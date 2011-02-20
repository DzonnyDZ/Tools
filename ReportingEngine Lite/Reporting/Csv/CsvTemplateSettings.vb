
Namespace ReportingT.ReportingEngineLite
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.208"), _
     System.SerializableAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="urn:eos-ksi/KolUni/Reporting/Csv"), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="urn:eos-ksi/KolUni/Reporting/Csv", IsNullable:=False)> _
    Partial Public Class CsvTemplateSettings
        Implements System.ComponentModel.INotifyPropertyChanging

        Private encodingField As String

        Private cultureField As String

        Private numberformatField As String

        Private dateformatField As String

        Private timeformatField As String

        Private separator_Field As String

        Private textqualifierField As String

        Private textqualifierusageField As CsvTemplateSettingsTextqualifierusage

        Private textqualifierescapeField As CsvTemplateSettingsTextqualifierescape

        Private newlineField As CsvTemplateSettingsNewline

        Private nlescapeField As CsvTemplateSettingsNlescape

        Private specialstringField As String

        Private headerField As Boolean

        Private footerField As Boolean

        Private headersizeField As String

        Private nullvalueField As String

        Public Sub New()
            MyBase.New()
            Me.encodingField = ""
            Me.cultureField = ""
            Me.numberformatField = ""
            Me.dateformatField = ""
            Me.timeformatField = ""
            Me.textqualifierusageField = CsvTemplateSettingsTextqualifierusage.asneeded
            Me.textqualifierescapeField = CsvTemplateSettingsTextqualifierescape.[double]
            Me.newlineField = CsvTemplateSettingsNewline.CrLf
            Me.nlescapeField = CsvTemplateSettingsNlescape.replaceallwithescape
            Me.headerField = False
            Me.footerField = False
            Me.headersizeField = "0"
            Me.OnAfterInit()
        End Sub

        <System.Xml.Serialization.XmlAttributeAttribute(), _
         System.ComponentModel.DefaultValueAttribute("")> _
        Public Property encoding() As String
            Get
                Return Me.encodingField
            End Get
            Set(value As String)
                Me.OnencodingChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("encoding", value))
                Me.encodingField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(), _
         System.ComponentModel.DefaultValueAttribute("")> _
        Public Property culture() As String
            Get
                Return Me.cultureField
            End Get
            Set(value As String)
                Me.OncultureChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("culture", value))
                Me.cultureField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("number-format"), _
         System.ComponentModel.DefaultValueAttribute("")> _
        Public Property numberformat() As String
            Get
                Return Me.numberformatField
            End Get
            Set(value As String)
                Me.OnnumberformatChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("numberformat", value))
                Me.numberformatField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("date-format"), _
         System.ComponentModel.DefaultValueAttribute("")> _
        Public Property dateformat() As String
            Get
                Return Me.dateformatField
            End Get
            Set(value As String)
                Me.OndateformatChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("dateformat", value))
                Me.dateformatField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("time-format"), _
         System.ComponentModel.DefaultValueAttribute("")> _
        Public Property timeformat() As String
            Get
                Return Me.timeformatField
            End Get
            Set(value As String)
                Me.OntimeformatChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("timeformat", value))
                Me.timeformatField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(), _
         System.ComponentModel.BrowsableAttribute(False), _
         System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> _
        Public Property separator_() As String
            Get
                Return Me.separator_Field
            End Get
            Set(value As String)
                Me.Onseparator_Changing(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("separator_", value))
                Me.separator_Field = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("text-qualifier")> _
        Public Property textqualifier() As String
            Get
                Return Me.textqualifierField
            End Get
            Set(value As String)
                Me.OntextqualifierChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("textqualifier", value))
                Me.textqualifierField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("text-qualifier-usage"), _
         System.ComponentModel.DefaultValueAttribute(CsvTemplateSettingsTextqualifierusage.asneeded)> _
        Public Property textqualifierusage() As CsvTemplateSettingsTextqualifierusage
            Get
                Return Me.textqualifierusageField
            End Get
            Set(value As CsvTemplateSettingsTextqualifierusage)
                Me.OntextqualifierusageChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsTextqualifierusage)("textqualifierusage", value))
                Me.textqualifierusageField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("text-qualifier-escape"), _
         System.ComponentModel.DefaultValueAttribute(CsvTemplateSettingsTextqualifierescape.[double])> _
        Public Property textqualifierescape() As CsvTemplateSettingsTextqualifierescape
            Get
                Return Me.textqualifierescapeField
            End Get
            Set(value As CsvTemplateSettingsTextqualifierescape)
                Me.OntextqualifierescapeChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsTextqualifierescape)("textqualifierescape", value))
                Me.textqualifierescapeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("new-line"), _
         System.ComponentModel.DefaultValueAttribute(CsvTemplateSettingsNewline.CrLf)> _
        Public Property newline() As CsvTemplateSettingsNewline
            Get
                Return Me.newlineField
            End Get
            Set(value As CsvTemplateSettingsNewline)
                Me.OnnewlineChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsNewline)("newline", value))
                Me.newlineField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("nl-escape"), _
         System.ComponentModel.DefaultValueAttribute(CsvTemplateSettingsNlescape.replaceallwithescape)> _
        Public Property nlescape() As CsvTemplateSettingsNlescape
            Get
                Return Me.nlescapeField
            End Get
            Set(value As CsvTemplateSettingsNlescape)
                Me.OnnlescapeChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsNlescape)("nlescape", value))
                Me.nlescapeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("special-string")> _
        Public Property specialstring() As String
            Get
                Return Me.specialstringField
            End Get
            Set(value As String)
                Me.OnspecialstringChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("specialstring", value))
                Me.specialstringField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(), _
         System.ComponentModel.DefaultValueAttribute(False)> _
        Public Property header() As Boolean
            Get
                Return Me.headerField
            End Get
            Set(value As Boolean)
                Me.OnheaderChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean)("header", value))
                Me.headerField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute(), _
         System.ComponentModel.DefaultValueAttribute(False)> _
        Public Property footer() As Boolean
            Get
                Return Me.footerField
            End Get
            Set(value As Boolean)
                Me.OnfooterChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean)("footer", value))
                Me.footerField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("header-size", DataType:="integer"), _
         System.ComponentModel.DefaultValueAttribute("0")> _
        Public Property headersize() As String
            Get
                Return Me.headersizeField
            End Get
            Set(value As String)
                Me.OnheadersizeChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("headersize", value))
                Me.headersizeField = value
            End Set
        End Property

        <System.Xml.Serialization.XmlAttributeAttribute("null-value")> _
        Public Property nullvalue() As String
            Get
                Return Me.nullvalueField
            End Get
            Set(value As String)
                Me.OnnullvalueChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("nullvalue", value))
                Me.nullvalueField = value
            End Set
        End Property

        '''<summary>Raised when value of property encoding is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event encodingChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Raised when value of property culture is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event cultureChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Raised when value of property numberformat is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event numberformatChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Raised when value of property dateformat is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event dateformatChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Raised when value of property timeformat is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event timeformatChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Raised when value of property separator_ is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event separator_Changing As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Raised when value of property textqualifier is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event textqualifierChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Raised when value of property textqualifierusage is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event textqualifierusageChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsTextqualifierusage))

        '''<summary>Raised when value of property textqualifierescape is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event textqualifierescapeChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsTextqualifierescape))

        '''<summary>Raised when value of property newline is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event newlineChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsNewline))

        '''<summary>Raised when value of property nlescape is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event nlescapeChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsNlescape))

        '''<summary>Raised when value of property specialstring is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event specialstringChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Raised when value of property header is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event headerChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))

        '''<summary>Raised when value of property footer is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event footerChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))

        '''<summary>Raised when value of property headersize is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event headersizeChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Raised when value of property nullvalue is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event nullvalueChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))

        '''<summary>Occurs when a property value is changing.</summary>
        Public Event PropertyChanging As System.ComponentModel.PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging

        '''<summary>Raises the <see cref='encodingChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnencodingChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent encodingChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='cultureChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OncultureChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent cultureChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='numberformatChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnnumberformatChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent numberformatChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='dateformatChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OndateformatChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent dateformatChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='timeformatChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OntimeformatChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent timeformatChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='separator_Changing'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub Onseparator_Changing(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent separator_Changing(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='textqualifierChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OntextqualifierChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent textqualifierChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='textqualifierusageChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OntextqualifierusageChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsTextqualifierusage))
            RaiseEvent textqualifierusageChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='textqualifierescapeChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OntextqualifierescapeChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsTextqualifierescape))
            RaiseEvent textqualifierescapeChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='newlineChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnnewlineChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsNewline))
            RaiseEvent newlineChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='nlescapeChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnnlescapeChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of CsvTemplateSettingsNlescape))
            RaiseEvent nlescapeChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='specialstringChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnspecialstringChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent specialstringChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='headerChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnheaderChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
            RaiseEvent headerChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='footerChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnfooterChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
            RaiseEvent footerChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='headersizeChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnheadersizeChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent headersizeChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='nullvalueChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnnullvalueChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent nullvalueChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub

        '''<summary>Raises the <see cref='PropertyChanging'/> event</summary>
        '''<param name='e'>Event arguments</summary>
        Protected Overridable Sub OnPropertyChanging(ByVal e As System.ComponentModel.PropertyChangingEventArgs)
            RaiseEvent PropertyChanging(Me, e)
        End Sub
    End Class

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.208"), _
     System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:eos-ksi/KolUni/Reporting/Csv")> _
    Public Enum CsvTemplateSettingsTextqualifierusage

        <System.Xml.Serialization.XmlEnumAttribute("as-needed")> _
        asneeded

        always

        <System.Xml.Serialization.XmlEnumAttribute("always-on-text")> _
        alwaysontext
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.208"), _
     System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:eos-ksi/KolUni/Reporting/Csv")> _
    Public Enum CsvTemplateSettingsTextqualifierescape

        [double]

        escape

        html

        <System.Xml.Serialization.XmlEnumAttribute("do-nothing")> _
        donothing
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.208"), _
     System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:eos-ksi/KolUni/Reporting/Csv")> _
    Public Enum CsvTemplateSettingsNewline

        CrLf

        LineFeed

        CarriageReturn

        NextLine

        FormFeed

        LineSeparator

        ParagraphSeparator

        System
    End Enum

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.208"), _
     System.SerializableAttribute(), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:eos-ksi/KolUni/Reporting/Csv")> _
    Public Enum CsvTemplateSettingsNlescape

        <System.Xml.Serialization.XmlEnumAttribute("replace-all-with-escape")> _
        replaceallwithescape

        <System.Xml.Serialization.XmlEnumAttribute("replace-with-escape")> _
        replacewithescape

        <System.Xml.Serialization.XmlEnumAttribute("escape-all")> _
        escapeall

        escape

        <System.Xml.Serialization.XmlEnumAttribute("special-replace")> _
        specialreplace

        <System.Xml.Serialization.XmlEnumAttribute("special-replace-all")> _
        specialreplaceall

        strip

        <System.Xml.Serialization.XmlEnumAttribute("strip-all")> _
        stripall

        html

        <System.Xml.Serialization.XmlEnumAttribute("html-all")> _
        htmlall

        <System.Xml.Serialization.XmlEnumAttribute("do-nothing")> _
        donothing
    End Enum
End Namespace
