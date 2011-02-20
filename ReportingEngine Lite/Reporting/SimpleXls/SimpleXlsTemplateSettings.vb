
Namespace ReportingT.ReportingEngineLite
    
    <System.Xml.Serialization.XmlIncludeAttribute(GetType(RepeatedXlsSettings)),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.208"),  _
     System.SerializableAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="urn:eos-ksi/KolUni/Reporting/SimpleXls"),  _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="urn:eos-ksi/KolUni/Reporting/SimpleXls", IsNullable:=false)>  _
    Partial Public Class SimpleXlsSettings
        Implements System.ComponentModel.INotifyPropertyChanging
        
        Private postProcessingCodeField As String
        
        Private listField As String
        
        Private row1Field As String
        
        Private col1Field As String
        
        Private skipFilledField As Boolean
        
        Private insertRowsField As Boolean
        
        Private columnNameRowField As String
        
        Private skipFilledNamesField As Boolean
        
        Private autoWidthField As String
        
        Private printAreaField As String
        
        Private copyColumnsFromField As String
        
        Private skipColumnsField As String
        
        Private nameColumnField As String
        
        Private nameFormatField As String
        
        Private selectListField As String
        
        Private suspendRecalculationsField As Boolean
        
        Private runMacroAfterField As String
        
        Public Sub New()
            MyBase.New
            Me.row1Field = "2"
            Me.col1Field = "1"
            Me.skipFilledField = false
            Me.insertRowsField = false
            Me.columnNameRowField = "0"
            Me.skipFilledNamesField = false
            Me.autoWidthField = ""
            Me.printAreaField = ""
            Me.copyColumnsFromField = "0"
            Me.skipColumnsField = ""
            Me.nameColumnField = ""
            Me.nameFormatField = "{0}"
            Me.selectListField = ""
            Me.suspendRecalculationsField = false
        End Sub
        
        Public Property PostProcessingCode() As String
            Get
                Return Me.postProcessingCodeField
            End Get
            Set
                Me.OnPostProcessingCodeChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("PostProcessingCode", value))
                Me.postProcessingCodeField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute()>  _
        Public Property List() As String
            Get
                Return Me.listField
            End Get
            Set
                Me.OnListChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("List", value))
                Me.listField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer"),  _
         System.ComponentModel.DefaultValueAttribute("2")>  _
        Public Property Row1() As String
            Get
                Return Me.row1Field
            End Get
            Set
                Me.OnRow1Changing(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("Row1", value))
                Me.row1Field = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer"),  _
         System.ComponentModel.DefaultValueAttribute("1")>  _
        Public Property Col1() As String
            Get
                Return Me.col1Field
            End Get
            Set
                Me.OnCol1Changing(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("Col1", value))
                Me.col1Field = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute(false)>  _
        Public Property SkipFilled() As Boolean
            Get
                Return Me.skipFilledField
            End Get
            Set
                Me.OnSkipFilledChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean)("SkipFilled", value))
                Me.skipFilledField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute(false)>  _
        Public Property InsertRows() As Boolean
            Get
                Return Me.insertRowsField
            End Get
            Set
                Me.OnInsertRowsChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean)("InsertRows", value))
                Me.insertRowsField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="integer"),  _
         System.ComponentModel.DefaultValueAttribute("0")>  _
        Public Property ColumnNameRow() As String
            Get
                Return Me.columnNameRowField
            End Get
            Set
                Me.OnColumnNameRowChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("ColumnNameRow", value))
                Me.columnNameRowField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute(false)>  _
        Public Property SkipFilledNames() As Boolean
            Get
                Return Me.skipFilledNamesField
            End Get
            Set
                Me.OnSkipFilledNamesChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean)("SkipFilledNames", value))
                Me.skipFilledNamesField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("")>  _
        Public Property AutoWidth() As String
            Get
                Return Me.autoWidthField
            End Get
            Set
                Me.OnAutoWidthChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("AutoWidth", value))
                Me.autoWidthField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("")>  _
        Public Property PrintArea() As String
            Get
                Return Me.printAreaField
            End Get
            Set
                Me.OnPrintAreaChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("PrintArea", value))
                Me.printAreaField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("0")>  _
        Public Property CopyColumnsFrom() As String
            Get
                Return Me.copyColumnsFromField
            End Get
            Set
                Me.OnCopyColumnsFromChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("CopyColumnsFrom", value))
                Me.copyColumnsFromField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("")>  _
        Public Property SkipColumns() As String
            Get
                Return Me.skipColumnsField
            End Get
            Set
                Me.OnSkipColumnsChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("SkipColumns", value))
                Me.skipColumnsField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("")>  _
        Public Overridable Property NameColumn() As String
            Get
                Return Me.nameColumnField
            End Get
            Set
                Me.OnNameColumnChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("NameColumn", value))
                Me.nameColumnField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("{0}")>  _
        Public Property NameFormat() As String
            Get
                Return Me.nameFormatField
            End Get
            Set
                Me.OnNameFormatChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("NameFormat", value))
                Me.nameFormatField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("")>  _
        Public Property SelectList() As String
            Get
                Return Me.selectListField
            End Get
            Set
                Me.OnSelectListChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("SelectList", value))
                Me.selectListField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute(false)>  _
        Public Property SuspendRecalculations() As Boolean
            Get
                Return Me.suspendRecalculationsField
            End Get
            Set
                Me.OnSuspendRecalculationsChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean)("SuspendRecalculations", value))
                Me.suspendRecalculationsField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(DataType:="token")>  _
        Public Property RunMacroAfter() As String
            Get
                Return Me.runMacroAfterField
            End Get
            Set
                Me.OnRunMacroAfterChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("RunMacroAfter", value))
                Me.runMacroAfterField = value
            End Set
        End Property
        
        '''<summary>Raised when value of property PostProcessingCode is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event PostProcessingCodeChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property List is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event ListChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property Row1 is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event Row1Changing As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property Col1 is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event Col1Changing As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property SkipFilled is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event SkipFilledChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
        
        '''<summary>Raised when value of property InsertRows is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event InsertRowsChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
        
        '''<summary>Raised when value of property ColumnNameRow is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event ColumnNameRowChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property SkipFilledNames is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event SkipFilledNamesChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
        
        '''<summary>Raised when value of property AutoWidth is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event AutoWidthChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property PrintArea is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event PrintAreaChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property CopyColumnsFrom is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event CopyColumnsFromChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property SkipColumns is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event SkipColumnsChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property NameColumn is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event NameColumnChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property NameFormat is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event NameFormatChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property SelectList is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event SelectListChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property SuspendRecalculations is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event SuspendRecalculationsChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
        
        '''<summary>Raised when value of property RunMacroAfter is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event RunMacroAfterChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Occurs when a property value is changing.</summary>
        Public Event PropertyChanging As System.ComponentModel.PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
        
        '''<summary>Raises the <see cref='PostProcessingCodeChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnPostProcessingCodeChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent PostProcessingCodeChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='ListChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnListChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent ListChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='Row1Changing'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnRow1Changing(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent Row1Changing(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='Col1Changing'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnCol1Changing(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent Col1Changing(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='SkipFilledChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnSkipFilledChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
            RaiseEvent SkipFilledChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='InsertRowsChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnInsertRowsChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
            RaiseEvent InsertRowsChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='ColumnNameRowChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnColumnNameRowChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent ColumnNameRowChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='SkipFilledNamesChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnSkipFilledNamesChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
            RaiseEvent SkipFilledNamesChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='AutoWidthChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnAutoWidthChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent AutoWidthChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='PrintAreaChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnPrintAreaChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent PrintAreaChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='CopyColumnsFromChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnCopyColumnsFromChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent CopyColumnsFromChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='SkipColumnsChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnSkipColumnsChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent SkipColumnsChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='NameColumnChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnNameColumnChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent NameColumnChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='NameFormatChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnNameFormatChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent NameFormatChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='SelectListChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnSelectListChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent SelectListChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='SuspendRecalculationsChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnSuspendRecalculationsChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
            RaiseEvent SuspendRecalculationsChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='RunMacroAfterChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnRunMacroAfterChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent RunMacroAfterChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='PropertyChanging'/> event</summary>
        '''<param name='e'>Event arguments</summary>
        Protected Overridable Sub OnPropertyChanging(ByVal e As System.ComponentModel.PropertyChangingEventArgs)
            RaiseEvent PropertyChanging(Me, e)
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.208"),  _
     System.SerializableAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="urn:eos-ksi/KolUni/Reporting/SimpleXls"),  _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="urn:eos-ksi/KolUni/Reporting/SimpleXls", IsNullable:=false)>  _
    Partial Public Class RepeatedXlsSettings
        Inherits SimpleXlsSettings
        Implements System.ComponentModel.INotifyPropertyChanging
        
        Private breakColumnField As String
        
        Private writeBreakField As Boolean
        
        Private nameColumn1Field As String
        
        Private writeNameField As Boolean
        
        Public Sub New()
            MyBase.New
            Me.writeBreakField = false
            Me.writeNameField = false
        End Sub
        
        <System.Xml.Serialization.XmlAttributeAttribute()>  _
        Public Property BreakColumn() As String
            Get
                Return Me.breakColumnField
            End Get
            Set
                Me.OnBreakColumnChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("BreakColumn", value))
                Me.breakColumnField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute(false)>  _
        Public Property WriteBreak() As Boolean
            Get
                Return Me.writeBreakField
            End Get
            Set
                Me.OnWriteBreakChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean)("WriteBreak", value))
                Me.writeBreakField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute("NameColumn")>  _
        Public Overrides Property NameColumn() As String
            Get
                Return Me.nameColumn1Field
            End Get
            Set
                Me.OnNameColumn1Changing(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String)("NameColumn1", value))
                Me.nameColumn1Field = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute(false)>  _
        Public Property WriteName() As Boolean
            Get
                Return Me.writeNameField
            End Get
            Set
                Me.OnWriteNameChanging(New Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean)("WriteName", value))
                Me.writeNameField = value
            End Set
        End Property
        
        '''<summary>Raised when value of property BreakColumn is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event BreakColumnChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property WriteBreak is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event WriteBreakChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
        
        '''<summary>Raised when value of property NameColumn1 is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event NameColumn1Changing As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
        
        '''<summary>Raised when value of property WriteName is about to change.</summary>
        '''<remarks>Throw exception to avoid this change. The exception will be passed to source of change.</remarks>
        Public Event WriteNameChanging As System.EventHandler(Of Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
        
        '''<summary>Occurs when a property value is changing.</summary>
        Public Event PropertyChanging As System.ComponentModel.PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
        
        '''<summary>Raises the <see cref='BreakColumnChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnBreakColumnChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent BreakColumnChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='WriteBreakChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnWriteBreakChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
            RaiseEvent WriteBreakChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='NameColumn1Changing'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnNameColumn1Changing(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of String))
            RaiseEvent NameColumn1Changing(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='WriteNameChanging'/> event</summary>
        '''<param name='e'>Event erguments</param>
        Protected Overridable Sub OnWriteNameChanging(ByVal e As Tools.ComponentModelT.PropertyChangingEventArgsEx(Of Boolean))
            RaiseEvent WriteNameChanging(Me, e)
            Me.OnPropertyChanging(e)
        End Sub
        
        '''<summary>Raises the <see cref='PropertyChanging'/> event</summary>
        '''<param name='e'>Event arguments</summary>
        Protected Overridable Sub OnPropertyChanging(ByVal e As System.ComponentModel.PropertyChangingEventArgs)
            RaiseEvent PropertyChanging(Me, e)
        End Sub
    End Class
End Namespace
