
Namespace ReportingT.ReportingEngineLite
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.208"),  _
     System.SerializableAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="urn:eos-ksi/KolUni/Reporting/Xml"),  _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="urn:eos-ksi/KolUni/Reporting/Xml", IsNullable:=false)>  _
    Partial Public Class XmlTemplateSettings
        
        Private enableDocumentFunctionField As Boolean
        
        Private enableScriptField As Boolean
        
        Private dataSetNamespaceField As String
        
        Private dataSetNameField As String
        
        Private tableNameField As String
        
        Private datasetNamespacePrefixField As String
        
        Private defaultExtensionField As String
        
        Private filterField As String
        
        Public Sub New()
            MyBase.New
            Me.enableDocumentFunctionField = false
            Me.enableScriptField = false
            Me.dataSetNamespaceField = ""
            Me.dataSetNameField = ""
            Me.tableNameField = ""
            Me.datasetNamespacePrefixField = ""
            Me.defaultExtensionField = "xml"
            Me.filterField = "XML files (*.xml)|*.xml"
            Me.OnAfterInit
        End Sub
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute(false)>  _
        Public Property EnableDocumentFunction() As Boolean
            Get
                Return Me.enableDocumentFunctionField
            End Get
            Set
                Me.enableDocumentFunctionField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute(false)>  _
        Public Property EnableScript() As Boolean
            Get
                Return Me.enableScriptField
            End Get
            Set
                Me.enableScriptField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("")>  _
        Public Property DataSetNamespace() As String
            Get
                Return Me.dataSetNamespaceField
            End Get
            Set
                Me.dataSetNamespaceField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("")>  _
        Public Property DataSetName() As String
            Get
                Return Me.dataSetNameField
            End Get
            Set
                Me.dataSetNameField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("")>  _
        Public Property TableName() As String
            Get
                Return Me.tableNameField
            End Get
            Set
                Me.tableNameField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("")>  _
        Public Property DatasetNamespacePrefix() As String
            Get
                Return Me.datasetNamespacePrefixField
            End Get
            Set
                Me.datasetNamespacePrefixField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("xml")>  _
        Public Property DefaultExtension() As String
            Get
                Return Me.defaultExtensionField
            End Get
            Set
                Me.defaultExtensionField = value
            End Set
        End Property
        
        <System.Xml.Serialization.XmlAttributeAttribute(),  _
         System.ComponentModel.DefaultValueAttribute("XML files (*.xml)|*.xml")>  _
        Public Property Filter() As String
            Get
                Return Me.filterField
            End Get
            Set
                Me.filterField = value
            End Set
        End Property
    End Class
End Namespace
